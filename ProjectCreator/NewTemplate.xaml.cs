using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ProjectCreator
{
    /// <summary>
    /// Interaction logic for NewTemplate.xaml
    /// </summary>
    public partial class NewTemplate : Window
    {

        private string newTemplateName;

        public NewTemplate()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public string NewTemplateName
        {
            get { return newTemplateName; }
            set { newTemplateName = value; }
        }


        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(templateName.Text) == false)
                {
                    NewTemplateName = templateName.Text;
                    Close();
                }
                else
                {
                    MessageBox.Show("Please enter a template name");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
