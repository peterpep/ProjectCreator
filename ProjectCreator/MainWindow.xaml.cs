using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace ProjectCreator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private TemplateObj test = new TemplateObj("Test");
        private string projectFolderPathTest = @"G:\Programming\CSharp\Test Folder\";

        private List<TemplateObj> allTemplates;

        public MainWindow()
        {
            InitializeComponent();
            allTemplates = new List<TemplateObj>();

            TemplateList.SelectionChanged += new SelectionChangedEventHandler(TemplateList_OnSelectionChanged);

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
           
        }

        private void TemplateList_OnSelectionChanged(object sender, SelectionChangedEventArgs args)
        {
            var indexOf = TemplateList.SelectedIndex;

            UpdateTreeView(allTemplates[indexOf]);
        }

        public void UpdateTreeView(TemplateObj updateWith)
        {
            List<TreeViewItem> bindingtoTree = new List<TreeViewItem>();

            foreach (var contentobj in updateWith)
            {
                if (contentobj.ContentInGroupOf == "")
                {
                    TreeViewItem group = new TreeViewItem();
                    group.Header = contentobj.ContentName;
                    bindingtoTree.Add(group);
                }
                else
                {
                    var subItemIs = new TreeViewItem {Header = contentobj.ContentName};
                    
                    var groupSubItemWillAddTo = bindingtoTree.First(item => (string) item.Header == contentobj.ContentInGroupOf);
                    groupSubItemWillAddTo.Items.Add(subItemIs);
                    
                }
            }

            TemplateContentView.ItemsSource = bindingtoTree;
            TemplateContentView.Items.Refresh();
        }

        private void CreateProject(TemplateObj projectTemp)
        {
            
            try
            {
                var ecoNum = GetEcoNumber();

                if (isEcoValid(ecoNum))
                {
                    var topLevelProjectFolder = Path.Combine(projectFolderPathTest, ecoNum);


                    if (!System.IO.Directory.Exists(topLevelProjectFolder))
                    {
                        System.IO.Directory.CreateDirectory(topLevelProjectFolder);

                        var ecoFolders = projectTemp.EcoNumUpdate(ecoNum);

                        foreach (var folder in ecoFolders)
                        {
                            if (folder.ContentInGroupOf == "")
                            {
                                System.IO.Directory.CreateDirectory(Path.Combine(topLevelProjectFolder, folder.ContentName));
                            }
                            else
                            {
                                var newPath = Path.Combine(topLevelProjectFolder, folder.ContentInGroupOf);
                                newPath = Path.Combine(newPath, folder.ContentName);

                                Directory.CreateDirectory(newPath);
                            }
                        }

                        MessageBox.Show($"Created Project for ECO {ecoNum}");
                    }
                    else
                    {
                        MessageBox.Show($"Project {ecoNum} folders already exist");
                    }
                }
                else
                {
                    MessageBox.Show("No ECO number Provided");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetEcoNumber()
        {
            if (!string.IsNullOrWhiteSpace(EcoNumber.Text))
            {
                var ecoNumber = EcoNumber.Text;
                return ecoNumber;
            }

            return "";
        }

        private bool isEcoValid(string ecoNum)
        {
            if (string.IsNullOrWhiteSpace(ecoNum))
            {
                return false;
            }

            return true;
        }

        private void AddTemplateBtn_OnClick(object sender, RoutedEventArgs e)
        {

            NewTemplate addingNewTemplate = new NewTemplate();

            addingNewTemplate.ShowDialog();

            if (!string.IsNullOrWhiteSpace(addingNewTemplate.NewTemplateName))
            {
                TemplateObj newTemplate = new TemplateObj(addingNewTemplate.NewTemplateName);
                newTemplate.Add(new TemplateContentObj("verification"));
                newTemplate.Add(new TemplateContentObj("Plan", "verification"));
                newTemplate.Add(new TemplateContentObj("Report", "verification"));
                newTemplate.Add(new TemplateContentObj("SAP"));
                newTemplate.Add(new TemplateContentObj("Approvals", "SAP"));
                newTemplate.Add(new TemplateContentObj("Documentation"));


                allTemplates.Add(newTemplate);

                TemplateList.ItemsSource = allTemplates;
                TemplateList.Items.Refresh();
            }


           

            //CreateProject(allTemplates.First(item => item.TemplateName == title));
            
        }

        private void CreateProjectFolders_OnClick(object sender, RoutedEventArgs e)
        {
            CreateProject((TemplateObj) TemplateList.SelectedItem);
        }

        private void RemoveTemplateBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var index = TemplateList.SelectedIndex;
            
            allTemplates.RemoveAt(index);
            TemplateList.Items.Refresh();
            TemplateList.SelectedIndex = 0;

            //TemplateContentView.Items.Clear();
            TemplateContentView.Items.Refresh();
        }
    }
}
