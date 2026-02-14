using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.VisualBasic.FileIO;

namespace ReTEXT
{
    public partial class MainForm: Form
    {

        PrintDocument printDocument = new PrintDocument();
        private bool isOverwriteMode = false;
        private string currentFilePath = null;

        private string labelCharsPrefix = "Chars: ";
        private string linePrefix = "Line: ";
        private string columnPrefix = "Column: ";

        public MainForm()
        {
            string currentFilePath = null;
            InitializeComponent();
            printDocument.PrintPage += PrintDocument_PrintPage;

            encodingCombo.Items.Add("UTF-8");
            encodingCombo.Items.Add("Windows-1251"); 
            encodingCombo.Items.Add("ASCII");
            encodingCombo.Items.Add("UTF-16 LE");
            encodingCombo.Items.Add("UTF-16 BE");
            encodingCombo.Items.Add("KOI8-R");
            encodingCombo.Items.Add("ISO-8859-5");

         

            ApplyTheme();
            ApplyLanguage();


        }
        private string GetFileTypeByExtension(string path)
        {
            string ext = Path.GetExtension(path).ToLower();

            switch (ext)
            {
                case ".txt": return "Text File";
                case ".bat": return "Batch File";
                case ".cmd": return "Command Script";
                case ".cs": return "C# Source File";
                case ".cpp": return "C++ Source File";
                case ".html": return "HTML Document";
                case ".htm": return "HTML Document";
                case ".xml": return "XML Document";
                case ".json": return "JSON File";
                case ".ini": return "Configuration INI File";
                case ".log": return "Log File";
                case ".yml": return "YAML File";
                case ".yaml": return "YAML File";
                case ".pro": return "Visual Prolog File";
                case ".vhd": return "VHSIC Hardware File";
                case ".v": return "Verilog File";
                case ".ts": return "TypeScript File";
                case ".t2t": return "Txt2Tags File";
                case ".vb": return "Visual Basic File";
                case ".vba": return "Visual Basic File";
                case ".vbs": return "Visual Script File";
                case ".tex": return "TeX File";
                case ".tek": return "Tektronix File";
                case ".tcl": return "Tool Command Language File";
                case ".swift": return "Swift File";
                case ".mot": return "Motorola S-Record File";
                case ".sql": return "SQL File";
                case ".scp": return "Spice File";
                case ".st": return "Smalltalk File";
                case ".scm": return "Scheme File";
                case ".rs": return "Rust File";
                case ".rb": return "Ruby File";
                case ".rc": return "Resource File";
                case ".reg": return "Registry File";
                case ".r2": return "REBOL File";
                case ".raku": return "Raku Source File";
                case ".r": return "R File";
                case ".gd": return "GDScript File";
                case ".py": return "Python File";
                case ".pb": return "PureBasic File";
                case ".properties": return "Properties File";
                case ".conf": return "Properties File";
                case ".cfg": return "Properties File";
                case ".ps1": return "PowerShell File";
                case ".ps": return "PostScript File";
                case ".php": return "PHP File";
                case ".pl": return "Perl File";
                case ".pas": return "Pascal File";
                case ".mm": return "Objective-C File";
                case ".osx": return "OScript File";
                case ".nsi": return "Nullsoft File";
                case ".nfo": return "MS-DOS Style/ASCII-Art File";
                case ".mak": return "Makefile";
                case ".lua": return "Lua File";
                case ".lsp": return "Lisp File";
                case ".java": return "Java File";
                case ".hex": return "HEX File";
                case ".go": return "Go File";
                case ".bas": return "Basic File";
                case ".c": return "C File";
                case ".as": return "ActionScript File";

                default: return ext.ToUpper() + " File";
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawString(
                RichTextBox.Text,
                RichTextBox.Font,
                Brushes.Black,
                e.MarginBounds,
                StringFormat.GenericTypographic
            );
        }


        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            labelChars.Text = labelCharsPrefix + RichTextBox.Text.Length;
        }

        private void RichTextBox_SelectionChanged(object sender, EventArgs e)
        {
            int pos = RichTextBox.SelectionStart;
            int line = RichTextBox.GetLineFromCharIndex(pos);
            int column = RichTextBox.SelectionStart - RichTextBox.GetFirstCharIndexFromLine(line);
            // int line = RichTextBox.GetLineFromCharIndex(RichTextBox.SelectionStart) + 1;
            LinesLable.Text = $"{linePrefix}{line + 1}, {columnPrefix}{column + 1}";
        }

        private void NewToolStrip_Click(object sender, EventArgs e)
        {
            if  (RichTextBox.Text !=  string.Empty)
            {
                DialogResult result = MessageBox.Show("Would you like to save your changes?", "Save Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result ==  DialogResult.Yes)
                {

                    SaveToolStrip_Click(sender, e);

                    RichTextBox.ResetText();
                    RichTextBox.Focus();

                }

                else if (result == DialogResult.No)
                {
                    RichTextBox.ResetText();
                    RichTextBox.Focus();
                }
            }
            else 
            {
                 
                RichTextBox.ResetText();
                RichTextBox.Focus();
            }
            // RichTextBox.Clear();
        }

        private void OpenToolStrip_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files|*.txt|Все файлы|*.*";


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string text;

                currentFilePath = ofd.FileName;

                using (var reader = new StreamReader(currentFilePath, true))
                {
                    text = reader.ReadToEnd();
                }

                RichTextBox.Text = File.ReadAllText(currentFilePath);

                DetectLineBreaks(text);

                FileLable.Text = GetFileTypeByExtension(currentFilePath);
            }
        }

        private void SaveToolStrip_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(currentFilePath))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Text File|*.txt|All Files|*.*";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = sfd.FileName;
                }
                else
                {
                    return;
                }

            }

            File.WriteAllText(currentFilePath, RichTextBox.Text);
            FileLable.Text = GetFileTypeByExtension(currentFilePath);
            DetectLineBreaks(RichTextBox.Text);


            // string currentFilePath = null;

            // string text;

            // if (currentFilePath != null)
            // {
            //   File.WriteAllText(currentFilePath, RichTextBox.Text);

            //  FileLable.Text = GetFileTypeByExtension(currentFilePath);

            //   using (var reader = new StreamReader(currentFilePath, true))
            //  {
            //      text = reader.ReadToEnd();
            //  }

            // DetectLineBreaks(text);
            // }
            // else
            //{
            //SaveAsToolStrip_Click(sender, e);
            // }
        }

        private void SaveAsToolStrip_Click(object sender, EventArgs e)
        {
            string text;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text File|*.txt|Все файлы|*.*";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                currentFilePath = sfd.FileName;
                File.WriteAllText(currentFilePath, RichTextBox.Text);

                FileLable.Text = GetFileTypeByExtension(currentFilePath);

                using (var reader = new StreamReader(currentFilePath, true))
                {
                    text = reader.ReadToEnd();
                }

                DetectLineBreaks(text);
            }
        }

        private void PrintToolStrip_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void PrintPreviewToolStrip_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDocument;
            previewDialog.Width = 800;
            previewDialog.Height = 600;
            previewDialog.ShowDialog();
        }

        private void ExitToolStrip_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UndoToolStrip_Click(object sender, EventArgs e)
        {
            RichTextBox.Undo();
        }

        private void RedoToolStrip_Click(object sender, EventArgs e)
        {
            RichTextBox.Redo();
        }

        private void CutToolStrip_Click(object sender, EventArgs e)
        {
            RichTextBox.Cut();
        }

        private void CopyToolStrip_Click(object sender, EventArgs e)
        {
            RichTextBox.Copy();
        }

        private void PasteToolStrip_Click(object sender, EventArgs e)
        {
            RichTextBox.Paste();
        }

        private void DeleteToolStrip_Click(object sender, EventArgs e)
        {
            RichTextBox.SelectedText = "";
            RichTextBox.Focus();
        }

        private void SelectAllToolStrip_Click(object sender, EventArgs e)
        {
            RichTextBox.SelectAll();
        }

        private void ClearToolStrip_Click(object sender, EventArgs e)
        {
            RichTextBox.Clear();
        }

        private void FindToolStrip_Click(object sender, EventArgs e)
        {
            FindForm ff = new FindForm(RichTextBox); 
            ff.Show();
        }

        private void SaveStripButton_Click(object sender, EventArgs e)
        {
            SaveToolStrip_Click(sender, e);
        }

        private void PrintStripButton_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }

        private void UndoStripButton_Click(object sender, EventArgs e)
        {
            RichTextBox.Undo();
        }

        private void RedoStripButton_Click(object sender, EventArgs e)
        {
            RichTextBox.Redo();
        }

        private void NewStripButton_Click(object sender, EventArgs e)
        {
            if (RichTextBox.Text != string.Empty)
            {
                DialogResult result = MessageBox.Show("Would you like to save your changes?", "Save Changes?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    SaveToolStrip_Click(sender, e);

                    RichTextBox.ResetText();
                    RichTextBox.Focus();

                }

                else if (result == DialogResult.No)
                {
                    RichTextBox.ResetText();
                    RichTextBox.Focus();
                }
            }
            else
            {

                RichTextBox.ResetText();
                RichTextBox.Focus();
            }
        }

        private void OpenStripButton_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Text Files|*.txt|Все файлы|*.*";


            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string text;

                currentFilePath = ofd.FileName;

                using (var reader = new StreamReader(currentFilePath, true))
                {
                    text = reader.ReadToEnd();
                }

                RichTextBox.Text = File.ReadAllText(currentFilePath);

                DetectLineBreaks(text);

                FileLable.Text = GetFileTypeByExtension(currentFilePath);

            }
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {
            fontDialog1.FontMustExist = true;    

            RichTextBox.Font = fontDialog1.Font;    
            RichTextBox.ForeColor = fontDialog1.Color;    


            foreach (Control containedControl in RichTextBox.Controls)
            {
                containedControl.Font = fontDialog1.Font;
            }
        }

        private void FontToolStrip_Click(object sender, EventArgs e)
        {
            try
            {
                fontDialog1.ShowDialog();    
                System.Drawing.Font oldFont = this.Font;    

                if (fontDialog1.ShowDialog() == DialogResult.OK)
                {
                    fontDialog1_Apply(RichTextBox, new System.EventArgs());
                }

                else if (fontDialog1.ShowDialog() == DialogResult.Cancel)
                {

                    this.Font = oldFont;


                    foreach (Control containedControl in RichTextBox.Controls)
                    {
                        containedControl.Font = oldFont;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Information", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
            }
        }

        private Encoding GetEncodingByName(string name)
        {
            return name switch
            {
                "UTF-8" => new UTF8Encoding(false),
                "Windows-1251" => Encoding.GetEncoding(1251),
                "ASCII" => Encoding.ASCII,
                "UTF-16 LE" => Encoding.Unicode,
                "UTF-16 BE" => Encoding.BigEndianUnicode,
                "KOI8-R" => Encoding.GetEncoding("koi8-r"),
                "ISO-8859-5" => Encoding.GetEncoding("iso-8859-5"),
                _ => new UTF8Encoding(false)
            };
        }

        private Encoding DetectEncoding(byte[] bytes)
        {
            if (bytes.Length > 3 &&
                bytes[0] == 0xEF && bytes[1] == 0xBB && bytes[2] == 0xBF)
                return new UTF8Encoding(true);

            if (bytes.Length > 2 &&
                bytes[0] == 0xFF && bytes[1] == 0xFE)
                return Encoding.Unicode;

            if (bytes.Length > 2 &&
                bytes[0] == 0xFE && bytes[1] == 0xFF)
                return Encoding.BigEndianUnicode;

            return Encoding.GetEncoding(1251);
        }

        private void SetEncodingInCombo(Encoding enc)
        {
            if (enc == Encoding.Unicode)
                encodingCombo.SelectedItem = "UTF-16 LE";
            else if (enc == Encoding.BigEndianUnicode)
                encodingCombo.SelectedItem = "UTF-16 BE";
            else if (enc is UTF8Encoding)
                encodingCombo.SelectedItem = "UTF-8";
            else if (enc.CodePage == 1251)
                encodingCombo.SelectedItem = "Windows-1251";
            else
                encodingCombo.SelectedIndex = 0; 
        }

        private void encodingCombo_Click(object sender, EventArgs e)
        {

        }

        private void encodingCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string encName = encodingCombo.SelectedItem.ToString();
            Encoding newEnc = GetEncodingByName(encName);

            byte[] oldBytes = Encoding.Default.GetBytes(RichTextBox.Text);

            string newText = newEnc.GetString(oldBytes);

            RichTextBox.Text = newText;
        }

        private void FontStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                fontDialog1.ShowDialog();
                System.Drawing.Font oldFont = this.Font;

                if (fontDialog1.ShowDialog() == DialogResult.OK)
                {
                    fontDialog1_Apply(RichTextBox, new System.EventArgs());
                }

                else if (fontDialog1.ShowDialog() == DialogResult.Cancel)
                {

                    this.Font = oldFont;


                    foreach (Control containedControl in RichTextBox.Controls)
                    {
                        containedControl.Font = oldFont;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BoldStripButton_Click(object sender, EventArgs e)
        {
            if (BoldStripButton.Checked == false)
            {
                BoldStripButton.Checked = true; 
            }
            else if (BoldStripButton.Checked == true)
            {
                BoldStripButton.Checked = false;    
            }

            if (RichTextBox.SelectionFont == null)
            {
                return;
            }

            FontStyle style = RichTextBox.SelectionFont.Style;

            if (RichTextBox.SelectionFont.Bold)
            {
                style &= ~FontStyle.Bold;
            }
            else
            {
                style |= FontStyle.Bold;

            }
            RichTextBox.SelectionFont = new Font(RichTextBox.SelectionFont, style);
        }

        private void ItalicStripButton_Click(object sender, EventArgs e)
        {

            if (ItalicStripButton.Checked == false)
            {
                ItalicStripButton.Checked = true;   
            }
            else if (ItalicStripButton.Checked == true)
            {
                ItalicStripButton.Checked = false;    
            }

            if (RichTextBox.SelectionFont == null)
            {
                return;
            }

            FontStyle style = RichTextBox.SelectionFont.Style;


            if (RichTextBox.SelectionFont.Italic)
            {
                style &= ~FontStyle.Italic;
            }
            else
            {
                style |= FontStyle.Italic;
            }
            RichTextBox.SelectionFont = new Font(RichTextBox.SelectionFont, style);    

        }

        private void underlineStripButton_Click(object sender, EventArgs e)
        {
            if (underlineStripButton.Checked == false)
            {
                underlineStripButton.Checked = true;     
            }
            else if (underlineStripButton.Checked == true)
            {
                underlineStripButton.Checked = false;    
            }

            if (RichTextBox.SelectionFont == null)
            {
                return;
            }


            FontStyle style = RichTextBox.SelectionFont.Style;


            if (RichTextBox.SelectionFont.Underline)
            {
                style &= ~FontStyle.Underline;
            }
            else
            {
                style |= FontStyle.Underline;
            }
            RichTextBox.SelectionFont = new Font(RichTextBox.SelectionFont, style);   
        }

        private void leftAlignStripButton_Click(object sender, EventArgs e)
        {
            centerAlignStripButton.Checked = false;
            rightAlignStripButton.Checked = false;
            if (leftAlignStripButton.Checked == false)
            {
                leftAlignStripButton.Checked = true;    
            }
            else if (leftAlignStripButton.Checked == true)
            {
                leftAlignStripButton.Checked = false;    
            }
            RichTextBox.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void centerAlignStripButton_Click(object sender, EventArgs e)
        {
            leftAlignStripButton.Checked = false;
            rightAlignStripButton.Checked = false;
            if (centerAlignStripButton.Checked == false)
            {
                centerAlignStripButton.Checked = true;    
            }
            else if (centerAlignStripButton.Checked == true)
            {
                centerAlignStripButton.Checked = false;    
            }
            RichTextBox.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void rightAlignStripButton_Click(object sender, EventArgs e)
        {
            leftAlignStripButton.Checked = false;
            centerAlignStripButton.Checked = false;

            if (rightAlignStripButton.Checked == false)
            {
                rightAlignStripButton.Checked = true;    
            }
            else if (rightAlignStripButton.Checked == true)
            {
                rightAlignStripButton.Checked = false;    
            }
            RichTextBox.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void bulletListStripButton_Click(object sender, EventArgs e)
        {
            if (bulletListStripButton.Checked == false)
            {
                bulletListStripButton.Checked = true;
                RichTextBox.SelectionBullet = true;    
            }
            else if (bulletListStripButton.Checked == true)
            {
                bulletListStripButton.Checked = false;
                RichTextBox.SelectionBullet = false;    
            }
        }

        private void zoomDropDownButton_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            RichTextBox.ZoomFactor = 0.2f;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            RichTextBox.ZoomFactor = 0.5f;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            RichTextBox.ZoomFactor = 1.0f;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            RichTextBox.ZoomFactor = 1.5f;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            RichTextBox.ZoomFactor = 2.0f;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            RichTextBox.ZoomFactor = 3.0f;
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            RichTextBox.ZoomFactor = 4.0f;
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            RichTextBox.ZoomFactor = 5.0f;
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.N)
            {
                NewStripButton_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.O)
            {
                OpenStripButton_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.S)
            {
                SaveStripButton_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.P)
            {
                PrintStripButton_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.Z)
            {
                UndoStripButton_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.Y)
            {
                RedoStripButton_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.X)
            {
                CutToolStrip_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.C)
            {
                CopyToolStrip_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.V)
            {
                PasteToolStrip_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.A)
            {
                SelectAllToolStrip_Click(sender, e);
            }

            if (e.Control && e.KeyCode == Keys.F)
            {
                FindToolStrip_Click(sender, e);
            }
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(5, 5);
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                gfx.FillEllipse(Brushes.Black, 1, 1, 3, 3);
            }

            if (RichTextBox.WordWrap == false)
            {
                RichTextBox.WordWrap = true;    
                wordWrapToolStripMenuItem.Image = bmp;   
            }
            else if (RichTextBox.WordWrap == true)
            {
                RichTextBox.WordWrap = false;    
                wordWrapToolStripMenuItem.Image = null;    
            }
        }

        private void colorOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();

            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                RichTextBox.ForeColor = colorDialog1.Color;
            }
        }

        private void GithubToolStrip_Click(object sender, EventArgs e)
        {
            string url = "https://github.com/Sek13H";

            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        }

        private void AboutToolStrip_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();
            af.Show();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingForm sf = new SettingForm();
            sf.Show();
        }


        private void ApplyTheme()
        {
            string theme = Properties.Settings.Default.Theme;

            if (theme == "Dark")
            {
                this.BackColor = Color.FromArgb(40, 40, 40);
                RichTextBox.BackColor = Color.FromArgb(30, 30, 30);
                RichTextBox.ForeColor = Color.White;
                menuStrip1.BackColor = Color.FromArgb(50, 50, 50);
                menuStrip1.ForeColor = Color.White;
                toolStrip1.BackColor = Color.FromArgb(55, 55, 55);
                toolStrip1.ForeColor = Color.White;
                toolStripSeparator1.ForeColor = Color.FromArgb(30, 30, 30);
                toolStripSeparator2.ForeColor = Color.FromArgb(30, 30, 30);
                toolStripSeparator3.ForeColor = Color.FromArgb(30, 30, 30);
                toolStripSeparator4.ForeColor = Color.FromArgb(30, 30, 30);
                toolStripSeparator5.ForeColor = Color.FromArgb(30, 30, 30);
                FileToolStrip.ForeColor = SystemColors.Control;
                EditToolStrip.ForeColor = SystemColors.Control;
                OptionsToolStrip.ForeColor = SystemColors.Control;
                FormatToolStrip.ForeColor = SystemColors.Control;
                HelpToolStrip.ForeColor = SystemColors.Control;
                LinesLable.ForeColor = SystemColors.Control;
                LabelMode.ForeColor = SystemColors.Control;
                labelLineBreak.ForeColor = SystemColors.Control;
                labelChars.ForeColor = SystemColors.Control;
                FileLable.ForeColor = SystemColors.Control;
            }
            else 
            {
                this.BackColor = SystemColors.Control;
                RichTextBox.BackColor = SystemColors.Control;
                RichTextBox.ForeColor = SystemColors.WindowText;
                menuStrip1.BackColor = SystemColors.Control;
                menuStrip1.ForeColor = SystemColors.WindowText;
                toolStrip1.BackColor = SystemColors.Control;
                toolStrip1.ForeColor = SystemColors.WindowText;
                toolStripSeparator1.ForeColor = SystemColors.ControlDark;
                toolStripSeparator2.ForeColor = SystemColors.ControlDark;
                toolStripSeparator3.ForeColor = SystemColors.ControlDark;
                toolStripSeparator4.ForeColor = SystemColors.ControlDark;
                toolStripSeparator5.ForeColor = SystemColors.ControlDark;
                FileToolStrip.ForeColor = SystemColors.ControlText;
                EditToolStrip.ForeColor = SystemColors.ControlText;
                OptionsToolStrip.ForeColor = SystemColors.ControlText;
                FormatToolStrip.ForeColor = SystemColors.ControlText;
                HelpToolStrip.ForeColor = SystemColors.ControlText;
                LinesLable.ForeColor = SystemColors.ControlText;
                LabelMode.ForeColor = SystemColors.ControlText;
                labelLineBreak.ForeColor = SystemColors.ControlText;
                labelChars.ForeColor = SystemColors.ControlText;
                FileLable.ForeColor = SystemColors.ControlText;
            }
        }

        private void ApplyLanguage()
        {
            string lang = Properties.Settings.Default.Language;



            if (lang == "ru")
            {
                FileToolStrip.Text = "Файл";
                NewToolStrip.Text = "Новый                   Crtl+N";
                OpenToolStrip.Text = "Открыть                Crtl+O";
                SaveToolStrip.Text = "Сохранить              Crtl+S";
                SaveAsToolStrip.Text = "Сохранить как..";
                PrintToolStrip.Text = "Распечатать           Ctrl+P";
                PrintPreviewToolStrip.Text = "Предпросмотр печати";
                ExitToolStrip.Text = "Выход";
                EditToolStrip.Text = "Правка";
                UndoToolStrip.Text = "Отмена                 Ctrl+Z";
                RedoToolStrip.Text = "Повтор                 Ctrl+Y";
                CutToolStrip.Text = "Вырезать                Ctrl+X";
                CopyToolStrip.Text = "Скопировать            Ctrl+C";
                PasteToolStrip.Text = "Вставить              Ctrl+V";
                DeleteToolStrip.Text = "Удалить";
                SelectAllToolStrip.Text = "Выделить всё      Ctrl+A";
                ClearToolStrip.Text = "Удалить всё";
                FindToolStrip.Text = "Поиск                  Ctrl+F";
                OptionsToolStrip.Text = "Опции";
                settingsToolStripMenuItem.Text = "Настройки";
                FormatToolStrip.Text = "Формат";
                FontToolStrip.Text = "Шрифт";
                wordWrapToolStripMenuItem.Text = "WordWrap";
                colorOptionsToolStripMenuItem.Text = "Настройки цвета";
                HelpToolStrip.Text = "Помощь и т.д.";
                AboutToolStrip.Text = "О программе..";
                GithubToolStrip.Text = "GitHub";
                SaveStripButton.Text = "Сохранить..";
                OpenStripButton.Text = "Открыть";
                NewStripButton.Text = "Новый";
                PrintStripButton.Text = "Распечатать";
                UndoStripButton.Text = "Отмена";
                RedoStripButton.Text = "Повтор";
                encodingCombo.Text = "Кодировки:";
                FontStripButton.Text = "Шрифт";
                BoldStripButton.Text = "Жирный";
                ItalicStripButton.Text = "Курсив";
                underlineStripButton.Text = "Подчёркнутый";
                leftAlignStripButton.Text = "Выровнять по левому краю";
                centerAlignStripButton.Text = "Выровнять по центру";
                rightAlignStripButton.Text = "Выровнять по правому краю";
                bulletListStripButton.Text = "Маркированный список";
                zoomDropDownButton.Text = "Зум";
                CaseDropDown.Text = "Поменять регистр";
                lowercaseToolStripMenuItem.Text = "нижный регистр";
                uPPERCASEToolStripMenuItem.Text = "ВЕРХНИЙ РЕГИСТР";
                deleteFileToolStripMenuItem.Text = "Удалить Файл";
                renameToolStripMenuItem.Text = "Переименовать";
                openFileFolderToolStripMenuItem.Text = "Открыть Папку Файла";
                casesToolStripMenuItem.Text = "Регистры..";
                uPPERCASEToolStripMenuItem1.Text = "ВЕРХНИЙ";
                lowercaseToolStripMenuItem1.Text = "нижний";
                alignToolStripMenuItem.Text = "Выровнять по..";
                leftToolStripMenuItem.Text = "Левому краю";
                centerToolStripMenuItem.Text = "Центру";
                rightToolStripMenuItem.Text = "Правому краю";
                textToolStripMenuItem.Text = "Текст..";
                boldToolStripMenuItem.Text = "Жирный";
                italicToolStripMenuItem.Text = "Курсивный";
                underlineToolStripMenuItem.Text = "С подчеркиванием";
                listToolStripMenuItem.Text = "Список..";
                bulletToolStripMenuItem.Text = "Маркированный";
                pasteToolStripMenuItem.Text = "Вставить..";
                pasteDateToolStripMenuItem.Text = "Вставить дату";
                copyToolStripMenuItem.Text = "Скопировать..";
                copyFileNameToolStripMenuItem.Text = "Скопировать Имя Файла";
                copyFilePathToolStripMenuItem.Text = "Скопировать Путь Файла";
                copyFileNameAndPathToolStripMenuItem.Text = "Скопировать Имя и Путь Файла";
                pasteIndentToolStripMenuItem.Text = "Вставить табуляцию";
                removeIndentToolStripMenuItem.Text = "Убрать табуляцию";
                allCapitalizedToolStripMenuItem.Text = "Всё С Большой";
                iNVERTCASEToolStripMenuItem.Text = "иНВЕРТИРОВАТЬ рЕГИСТР";
                rANdoMCaSEToolStripMenuItem.Text = "СлуЧАЙнЫй рЕГисТР";
                convertToolStripMenuItem.Text = "Конвертировать..";
                copyAsBinaryCodeToolStripMenuItem.Text = "Скопировать Как Двоичный Код";
                pasteAsBinaryCodeToolStripMenuItem.Text = "Вставить Как Двоичный Код";
                otherToolStripMenuItem.Text = "Другое..";
                cutAsBinaryCodeToolStripMenuItem.Text = "Вырезать Как Двоичный Код";
                // .Text = "";
                labelCharsPrefix = "Символов: ";
                linePrefix = "Строка: ";
                columnPrefix = "Столбец: ";

            }

            else 
            {
                FileToolStrip.Text = "File";
                NewToolStrip.Text = "New                   Crtl+N";
                OpenToolStrip.Text = "Open                 Crtl+O";
                SaveToolStrip.Text = "Save                   Crtl+S";
                SaveAsToolStrip.Text = "Save As..";
                PrintToolStrip.Text = "Print                   Ctrl+P";
                PrintPreviewToolStrip.Text = "Print Preview";
                ExitToolStrip.Text = "Exit";
                EditToolStrip.Text = "Edit";
                UndoToolStrip.Text = "Undo                   Ctrl+Z";
                RedoToolStrip.Text = "Redo                   Ctrl+Y";
                CutToolStrip.Text = "Cut                      Ctrl+X";
                CopyToolStrip.Text = "Copy                   Ctrl+C";
                PasteToolStrip.Text = "Paste                   Ctrl+V";
                DeleteToolStrip.Text = "Delete";
                SelectAllToolStrip.Text = "Select All             Ctrl+A";
                ClearToolStrip.Text = "Clear All";
                FindToolStrip.Text = "Find                   Ctrl+F";
                OptionsToolStrip.Text = "Options";
                settingsToolStripMenuItem.Text = "Settings";
                FormatToolStrip.Text = "Format";
                FontToolStrip.Text = "Font";
                wordWrapToolStripMenuItem.Text = "WordWrap";
                colorOptionsToolStripMenuItem.Text = "Color Options";
                HelpToolStrip.Text = "Help/About";
                AboutToolStrip.Text = "About...";
                GithubToolStrip.Text = "GitHub";
                SaveStripButton.Text = "Save..";
                OpenStripButton.Text = "Open";
                NewStripButton.Text = "New";
                PrintStripButton.Text = "Print";
                UndoStripButton.Text = "Undo";
                RedoStripButton.Text = "Redo";
                encodingCombo.Text = "Encodings:";
                FontStripButton.Text = "Font";
                BoldStripButton.Text = "Bold";
                ItalicStripButton.Text = "Italic";
                underlineStripButton.Text = "Underline";
                leftAlignStripButton.Text = "Left Align";
                centerAlignStripButton.Text = "Center Align";
                rightAlignStripButton.Text = "Right Align";
                bulletListStripButton.Text = "Bullet List";
                zoomDropDownButton.Text = "Zoom";
                CaseDropDown.Text = "Change Case";
                lowercaseToolStripMenuItem.Text = "lowercase";
                uPPERCASEToolStripMenuItem.Text = "UPPERCASE";
                deleteFileToolStripMenuItem.Text = "Delete File";
                renameToolStripMenuItem.Text = "Rename";
                openFileFolderToolStripMenuItem.Text = "Open File Folder";
                casesToolStripMenuItem.Text = "Cases..";
                uPPERCASEToolStripMenuItem1.Text = "UPPERCASE";
                lowercaseToolStripMenuItem1.Text = "lowercase";
                alignToolStripMenuItem.Text = "Align..";
                leftToolStripMenuItem.Text = "Left";
                centerToolStripMenuItem.Text = "Center";
                rightToolStripMenuItem.Text = "Right";
                textToolStripMenuItem.Text = "Text..";
                boldToolStripMenuItem.Text = "Bold";
                italicToolStripMenuItem.Text = "Italic";
                underlineToolStripMenuItem.Text = "Underline";
                listToolStripMenuItem.Text = "List..";
                bulletToolStripMenuItem.Text = "Bullet";
                pasteToolStripMenuItem.Text = "Paste..";
                pasteDateToolStripMenuItem.Text = "Paste Date";
                copyToolStripMenuItem.Text = "Copy..";
                copyFileNameToolStripMenuItem.Text = "Copy File Name";
                copyFilePathToolStripMenuItem.Text = "Copy File Path";
                copyFileNameAndPathToolStripMenuItem.Text = "Copy File Name and Path";
                pasteIndentToolStripMenuItem.Text = "Paste Indent";
                removeIndentToolStripMenuItem.Text = "Remove Indent";
                allCapitalizedToolStripMenuItem.Text = "All Capitalized";
                iNVERTCASEToolStripMenuItem.Text = "iNVERT cASE";
                rANdoMCaSEToolStripMenuItem.Text = "rANdoM CaSE";
                convertToolStripMenuItem.Text = "Convert..";
                copyAsBinaryCodeToolStripMenuItem.Text = "Copy As Binary Code";
                pasteAsBinaryCodeToolStripMenuItem.Text = "Paste As Binary Code";
                otherToolStripMenuItem.Text = "Other..";
                cutAsBinaryCodeToolStripMenuItem.Text = "Cut As Binary Code";
                labelCharsPrefix = "Chars: ";
                linePrefix = "Line: ";
                columnPrefix = "Column: ";
            }
        }

        private void RichTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Insert)
            {
                isOverwriteMode = !isOverwriteMode;

                if (isOverwriteMode)
                    LabelMode.Text = "OVR";
                else
                    LabelMode.Text = "INS";
            }
        }
        private void DetectLineBreaks(string text)
        {
            if (text.Contains("\r\n"))
            {
                labelLineBreak.Text = "Windows (CR LF)";
            }
            else if (text.Contains("\n"))
            {
                labelLineBreak.Text = "Unix (LF)";
            }
            else if (text.Contains("\r"))
            {
                labelLineBreak.Text = "Mac (CR)";
            }
            else
            {
                labelLineBreak.Text = "Unknown";
            }
        }

        private void lowercaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.SelectedText = RichTextBox.SelectedText.ToLower();
        }

        private void uPPERCASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox.SelectedText = RichTextBox.SelectedText.ToUpper();
        }

        private void deleteFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath) || !File.Exists(currentFilePath))
            {
                MessageBox.Show("File not open or doesn't exist.", 
                    "Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to delete the file?\n\n{currentFilePath}",
                "Delete file",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes)
                return;

            try
            {
                FileSystem.DeleteFile(
                    currentFilePath,
                    UIOption.OnlyErrorDialogs,
                    RecycleOption.SendToRecycleBin
                );

                RichTextBox.Clear();
                currentFilePath = null;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath) || !File.Exists(currentFilePath))
            {
                MessageBox.Show("File not open or doesn't exist.",
                                "Rename", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Rename File";
            sfd.Filter = "All files|*.*";

            sfd.FileName = Path.GetFileName(currentFilePath);
            sfd.InitialDirectory = Path.GetDirectoryName(currentFilePath);

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string newPath = sfd.FileName;

                if (newPath == currentFilePath)
                    return;
                
                try
                {
                    File.Move(currentFilePath, newPath);

                    currentFilePath = newPath;

                    MessageBox.Show("File renamed successfully!", "Rename",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error renaming file:\n" + ex.Message,
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void openFileFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath) || !File.Exists(currentFilePath))
            {
                MessageBox.Show("File not open or doesn't exist.",
                                "Open Folder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string args = "/select,\"" + currentFilePath + "\"";

            try
            {
                Process.Start("explorer.exe", args);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening folder:\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void uPPERCASEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            uPPERCASEToolStripMenuItem_Click(sender, e);
        }

        private void lowercaseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            lowercaseToolStripMenuItem_Click(sender, e);
        }

        private void leftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            leftAlignStripButton_Click(sender, e);
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            centerAlignStripButton_Click(sender, e);
        }

        private void rightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rightAlignStripButton_Click(sender, e);
        }

        private void boldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BoldStripButton_Click(sender, e);
        }

        private void italicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItalicStripButton_Click(sender, e);
        }

        private void underlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            underlineStripButton_Click(sender, e);
        }

        private void bulletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bulletListStripButton_Click(sender, e);
        }

        private void pasteDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string dt = DateTime.Now.ToString("HH:mm dd.mm.yyyy");

            RichTextBox.SelectedText = dt;
        }

        private void copyFileNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath) || !File.Exists(currentFilePath))
            {
                MessageBox.Show("No file open.",
                                "Copy File Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fileName = Path.GetFileName(currentFilePath);

            Clipboard.SetText(fileName);

            MessageBox.Show("File name copied:\n" + fileName,
                    "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void copyFilePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath) || !File.Exists(currentFilePath))
            {
                MessageBox.Show("No file open.",
                                "Copy Full Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Clipboard.SetText(currentFilePath);

            MessageBox.Show("Full path copied:\n" + currentFilePath,
                    "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void copyFileNameAndPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentFilePath) || !File.Exists(currentFilePath))
            {
                MessageBox.Show("No file open.",
                                "Copy Name + Path", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fileName = Path.GetFileName(currentFilePath);

            string result = $"File: {fileName}\nPath: {currentFilePath}";

            Clipboard.SetText(result);

            MessageBox.Show("File name and path copied.",
                            "Copied", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void pasteIndentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RichTextBox.SelectionLength == 0)
            {
                RichTextBox.SelectedText = "\t";
            }
            else
            {
                int start = RichTextBox.SelectionStart;
                int length = RichTextBox.SelectionLength;

                string selected = RichTextBox.SelectedText;
                string[] lines = selected.Split('\n');

                for (int i = 0; i < lines.Length; i++)
                    lines[i] = "\t" + lines[i];

                string result = string.Join("\n", lines);

                RichTextBox.SelectedText = result;

                RichTextBox.Select(start, result.Length);
            }
        }

        private void removeIndentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RichTextBox.SelectionLength == 0)
                return;

            int start = RichTextBox.SelectionStart;

            string selected = RichTextBox.SelectedText;
            string[] lines = selected.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("\t"))
                    lines[i] = lines[i].Substring(1);

                else if (lines[i].StartsWith("    "))
                    lines[i] = lines[i].Substring(4);
            }

            string result = string.Join("\n", lines);

            RichTextBox.SelectedText = result;
            RichTextBox.Select(start, result.Length);
        }

        private void allCapitalizedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool noSelection = RichTextBox.SelectionLength == 0;

            string text = noSelection ? RichTextBox.Text : RichTextBox.SelectedText;

            if (string.IsNullOrWhiteSpace(text))
                return;

            var result = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());

            if (noSelection)
            {
                RichTextBox.Text = result;
            }
            else
            {
                RichTextBox.SelectedText = result;
            }
        }

        private void iNVERTCASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool noSelection = RichTextBox.SelectionLength == 0;

            string text = noSelection ? RichTextBox.Text : RichTextBox.SelectedText;

            if (string.IsNullOrEmpty(text))
                return;

            char[] result = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if (char.IsLetter(c))
                {
                    if (char.IsUpper(c))
                        result[i] = char.ToLower(c);
                    else
                        result[i] = char.ToUpper(c);
                }
                else
                {
                    result[i] = c;
                }
            }

            string finalText = new string(result);

            if (noSelection)
                RichTextBox.Text = finalText;
            else
                RichTextBox.SelectedText = finalText;
        }

        private void rANdoMCaSEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool noSelection = RichTextBox.SelectionLength == 0;

            string text = noSelection ? RichTextBox.Text : RichTextBox.SelectedText;

            if (string.IsNullOrEmpty(text))
                return;

            Random rnd = new Random();
            char[] result = new char[text.Length];

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                if (char.IsLetter(c))
                {

                    if (rnd.Next(2) == 0)
                        result[i] = char.ToLower(c);
                    else
                        result[i] = char.ToUpper(c);
                }
                else
                {
                    result[i] = c; 
                }
            }

            string finalText = new string(result);

            if (noSelection)
                RichTextBox.Text = finalText;
            else
                RichTextBox.SelectedText = finalText;
        }

        private void ConvertLineEndings(string newLine)
        {
            string text = RichTextBox.Text;

            text = text.Replace("\r\n", "\n");

            text = text.Replace("\r", "\n");

            text = text.Replace("\n", newLine);

            RichTextBox.Text = text;
        }

        private void windowsCRLFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertLineEndings("\r\n");
            labelLineBreak.Text = "Windows (CR LF)";
        }

        private void unixLFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertLineEndings("\n");
            labelLineBreak.Text = "Unix (LF)";
        }

        private void macCRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConvertLineEndings("\r");
            labelLineBreak.Text = "Mac (CR)";
        }

        private void copyAsBinaryCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = RichTextBox.Text;

            StringBuilder sb = new StringBuilder();

            foreach (char c in text)
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
                sb.Append(" "); 
            }

            Clipboard.SetText(sb.ToString().Trim());

            MessageBox.Show("Copy!",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void cutAsBinaryCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = RichTextBox.SelectedText;

            if (string.IsNullOrEmpty(text))
                return;

            StringBuilder sb = new StringBuilder();

            foreach (char c in text)
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
                sb.Append(" ");
            }

            Clipboard.SetText(sb.ToString().Trim());
         
            RichTextBox.SelectedText = "";
        }

        private void pasteAsBinaryCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Clipboard.ContainsText())
            {
                MessageBox.Show("There is no text in the clipboard!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string text = Clipboard.GetText(); 
            StringBuilder sb = new StringBuilder();

            foreach (char c in text)
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0')); 
                sb.Append(" "); 
            }

            int selectionStart = RichTextBox.SelectionStart;
            RichTextBox.Text = RichTextBox.Text.Insert(selectionStart, sb.ToString().Trim());
        }

        private void allCapitalizedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            allCapitalizedToolStripMenuItem_Click(sender, e);
        }

        private void iNVERTCASEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            iNVERTCASEToolStripMenuItem_Click(sender, e);
        }

        private void rANdoMCaSEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            rANdoMCaSEToolStripMenuItem_Click(sender, e);
        }

        private void labelChars_Click(object sender, EventArgs e)
        {

        }
    }
}
