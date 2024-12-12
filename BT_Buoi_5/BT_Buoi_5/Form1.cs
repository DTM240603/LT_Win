using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT_Buoi_5
{
    public partial class Form1 : Form
    {
        public string path = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void địnhDạngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDlg = new FontDialog();
            fontDlg.ShowColor = true;
            fontDlg.ShowApply = true;
            fontDlg.ShowEffects = true;
            fontDlg.ShowHelp = true;
            if(fontDlg.ShowDialog() != DialogResult.Cancel)
            {
                richText.ForeColor = fontDlg.Color;
                richText.Font = fontDlg.Font;
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            NewVanBan();
        }

        private void NewVanBan()
        {
            foreach (FontFamily item in new InstalledFontCollection().Families)
            {
                tsbFont.Items.Add(item.Name);
                richText.Clear();
                richText.Font = new Font("Tahoma", 14, FontStyle.Regular);
                tsbSize.SelectedItem = "14";
                tsbFont.SelectedItem = "Tahoma";
                path = "";


            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewVanBan();
        }

        private void tsbBold_Click(object sender, EventArgs e)
        {
            if (richText.SelectionFont.Bold)
            {
                richText.SelectionFont = new Font(richText.SelectionFont, richText.SelectionFont.Style & ~FontStyle.Bold);
            }
            else {
                richText.SelectionFont = new Font(richText.SelectionFont, richText.SelectionFont.Style | FontStyle.Bold);
            }
        }

        private void tsbItalic_Click(object sender, EventArgs e)
        {
            if (richText.SelectionFont.Italic)
            {
                richText.SelectionFont = new Font(richText.SelectionFont, richText.SelectionFont.Style & ~FontStyle.Italic);
            }
            else
            {
                richText.SelectionFont = new Font(richText.SelectionFont, richText.SelectionFont.Style | FontStyle.Italic);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            if (richText.SelectionFont.Underline)
            {
                richText.SelectionFont = new Font(richText.SelectionFont, richText.SelectionFont.Style & ~FontStyle.Underline);
            }
            else
            {
                richText.SelectionFont = new Font(richText.SelectionFont, richText.SelectionFont.Style | FontStyle.Underline);
            }
        }

        private void tsbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            richText.SelectionFont = new Font(tsbFont.Text, float.Parse(tsbSize.Text));
        }

        private void tsbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            richText.SelectionFont = new Font(tsbSize.Text, float.Parse(tsbSize.Text));
        }

        private void tạoVănBảnMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewVanBan();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|Plain Text (*.txt)|*.txt",
                Title = "Lưu tập tin"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(saveFileDialog.FileName).ToLower() == ".rtf")
                {
                    // Lưu tập tin RTF
                    richText.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                }
                else if (Path.GetExtension(saveFileDialog.FileName).ToLower() == ".txt")
                {
                    // Lưu tập tin TXT
                    richText.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                }
            }
        }

        private void lưuNộiDungVănBảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|Plain Text (*.txt)|*.txt",
                Title = "Lưu tập tin"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(saveFileDialog.FileName).ToLower() == ".rtf")
                {
                    // Lưu tập tin RTF
                    richText.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.RichText);
                }
                else if (Path.GetExtension(saveFileDialog.FileName).ToLower() == ".txt")
                {
                    // Lưu tập tin TXT
                    richText.SaveFile(saveFileDialog.FileName, RichTextBoxStreamType.PlainText);
                }
            }

        }

        private void mởTậpTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|Plain Text (*.txt)|*.txt",
                Title = "Mở tập tin"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(openFileDialog.FileName).ToLower() == ".rtf")
                {
                    // Mở tập tin RTF
                    richText.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.RichText);
                }
                else if (Path.GetExtension(openFileDialog.FileName).ToLower() == ".txt")
                {
                    // Mở tập tin TXT
                    richText.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.PlainText);
                }
            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
