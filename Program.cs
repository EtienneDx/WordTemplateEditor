using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordTemplateEditor
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            List<Field> fields = new List<Field>();
            using(var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "template.docx"), FileMode.Open, FileAccess.Read))
            {
                XWPFDocument doc = new XWPFDocument(stream);
                foreach (var p in doc.Paragraphs)
                {
                    if (p.ElementType == BodyElementType.PARAGRAPH)
                    {
                        MatchCollection matches = Regex.Matches(p.Text, "%([A-Z0-9_]+)%");
                        foreach (Match match in matches)
                        {
                            fields.Add(new Field(match.Groups[1].Value));
                        }
                    }
                }
            }

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new MainForm(fields));
        }

    }
}
