using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordTemplateEditor
{
    public partial class MainForm : Form
    {
        private List<FieldContainer> fieldContainers = new List<FieldContainer>();

        public MainForm(List<Field> fields)
        {
            InitializeComponent();
            foreach (var field in fields)
            {
                FieldContainer f = new FieldContainer(field);
                container.Controls.Add(f);
                fieldContainers.Add(f);
            }
        }

        private void create_Click(object sender, EventArgs e)
        {
            using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "template.docx"), FileMode.Open, FileAccess.Read))
            {
                XWPFDocument doc = new XWPFDocument(stream);
                foreach (var p in doc.Paragraphs)
                {
                    if (p.ElementType == BodyElementType.PARAGRAPH && p.PartType == BodyType.DOCUMENT)
                    {
                        foreach (var field in fieldContainers)
                        {
                            try
                            {
                                p.ReplaceText("%" + field.Field.Name + "%", field.Value);
                            }
                            catch(System.NullReferenceException)
                            {

                            }
                        }
                    }
                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.FileName = "out.docx";
                saveFileDialog.Filter = "Document Word (*.docx)|*.docx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (!Directory.Exists(Directory.GetParent(saveFileDialog.FileName).FullName))
                        Directory.CreateDirectory(Directory.GetParent(saveFileDialog.FileName).FullName);
                    using (var outstream = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        doc.Write(outstream);
                    }
                }
            }
        }
    }
}
