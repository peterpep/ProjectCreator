using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectCreator
{
    public class TemplateObj : IEnumerable<TemplateContentObj>
    {
        private List<TemplateContentObj> _templateList;
        private string _templateName;

        public TemplateObj(string templateName)
        {
            TemplateName = templateName;
            _templateList = new List<TemplateContentObj>();
        }

        public string TemplateName
        {
            get { return _templateName; }
            set { _templateName = value; }
        }

        public void Add(TemplateContentObj newContentItem)
        {
            _templateList.Add(newContentItem);
        }

        public void Remove(TemplateContentObj newContentItem)
        {
            _templateList.Remove(newContentItem);
        }

        public void Remove(int removeAtIndex)
        {
            _templateList.RemoveAt(removeAtIndex);
        }

        public override string ToString()
        {
            return TemplateName;
        }

        public TemplateObj EcoNumUpdate(string ecoNumber)
        {
            var newName = $"{ecoNumber} {TemplateName}";
            var templateWithECO = new TemplateObj(newName);

            foreach (var content in _templateList)
            {
                if (content.ContentInGroupOf == "")
                {
                    templateWithECO.Add(new TemplateContentObj($"{ecoNumber} {content.ContentName}", $""));
                }
                else
                {
                    templateWithECO.Add(new TemplateContentObj($"{ecoNumber} {content.ContentName}", $"{ecoNumber} {content.ContentInGroupOf}"));
                }
            }

            return templateWithECO;
        }

        public IEnumerator<TemplateContentObj> GetEnumerator()
        {
            return _templateList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
