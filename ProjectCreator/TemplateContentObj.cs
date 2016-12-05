using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCreator
{
    public class TemplateContentObj
    {
        private string _contentName;
        private string _contentInGroupOf;

        public TemplateContentObj(string Name)
        {
            ContentName = Name;
            ContentInGroupOf = "";
        }

        public TemplateContentObj(string Name, string Group)
        {
            ContentName = Name;
            ContentInGroupOf = Group;
        }

        public string ContentInGroupOf
        {
            get { return _contentInGroupOf; }
            set { _contentInGroupOf = value; }
        }

        public string ContentName
        {
            get { return _contentName; }
            set { _contentName = value; }
        }

        
        public override string ToString()
        {
            return ContentName;
        }
    }
}
