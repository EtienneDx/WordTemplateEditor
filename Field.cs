using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordTemplateEditor
{
    public class Field
    {
        private string name;

        public Field(string name)
        {
            this.name = name;
        }

        public string DisplayName
        {
            get
            {
                string str = name.Replace('_', ' ').ToLower();
                return (str[0] + "").ToUpper() + str.Substring(1);
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }
    }
}
