using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordTemplateEditor
{
    public partial class FieldContainer : UserControl
    {
        public Field Field { get; }

        public FieldContainer(Field field)
        {
            InitializeComponent();
            label.Text = field.DisplayName;
            Field = field;
        }

        public string Value
        {
            get
            {
                return textBox.Text;
            }
        }
    }
}
