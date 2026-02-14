using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReTEXT
{
    public partial class FindForm : Form
    {
        private RichTextBox Editor;

        public FindForm(RichTextBox editor)
        {
            InitializeComponent();
            Editor = editor;

            var mode = CaseCheck.Checked
           ? StringComparison.Ordinal
           : StringComparison.OrdinalIgnoreCase;

            ApplyLanguage();
            ApplyTheme();
        }

        private void FindForm_Load(object sender, EventArgs e)
        {

        }

        private string UnescapeString(string s)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '\\' && i + 1 < s.Length)
                {
                    i++;
                    char c = s[i];

                    switch (c)
                    {
                        case 'n': sb.Append('\n'); break;
                        case 'r': sb.Append('\r'); break;
                        case 't': sb.Append('\t'); break;
                        case '0': sb.Append('\0'); break;
                        case '\\': sb.Append('\\'); break;
                        case 'x': // \xNN
                            if (i + 2 < s.Length)
                            {
                                string hex = s.Substring(i + 1, 2);
                                if (byte.TryParse(hex, System.Globalization.NumberStyles.HexNumber, null, out byte b))
                                {
                                    sb.Append((char)b);
                                    i += 2;
                                }
                            }
                            break;

                        case 'u': // \uFFFF
                            if (i + 4 < s.Length)
                            {
                                string hex = s.Substring(i + 1, 4);
                                if (ushort.TryParse(hex, System.Globalization.NumberStyles.HexNumber, null, out ushort b16))
                                {
                                    sb.Append((char)b16);
                                    i += 4;
                                }
                            }
                            break;

                        default:
                            sb.Append(c);
                            break;
                    }
                }
                else
                {
                    sb.Append(s[i]);
                }
            }
            return sb.ToString();
        }


        private void FindNext(bool fromStart)
        {
            string text = SearchBox.Text;
            if (string.IsNullOrEmpty(text) || Editor == null) return;

            if (AdvancedBox.Checked)
                text = UnescapeString(text);

            var mode = CaseCheck.Checked ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            int startPos = fromStart ? 0 : Editor.SelectionStart + Editor.SelectionLength;
            int index = Editor.Text.IndexOf(text, startPos, mode);
            if (index == -1 && !fromStart)
                index = Editor.Text.IndexOf(text, 0, mode);

            if (index != -1)
            {
                Editor.Select(index, text.Length);
                Editor.ScrollToCaret();
                Editor.Focus();
            }
        }

        private void FindPrevious()
        {
            string text = SearchBox.Text;
            if (string.IsNullOrEmpty(text) || Editor == null) return;

            var mode = CaseCheck.Checked ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            int startPos = Editor.SelectionStart - 1;
            if (startPos < 0) startPos = Editor.Text.Length - 1;

            int index = Editor.Text.LastIndexOf(text, startPos, mode);
            if (index == -1)
                index = Editor.Text.LastIndexOf(text, Editor.Text.Length - 1, mode);

            if (index != -1)
            {
                Editor.Select(index, text.Length);
                Editor.ScrollToCaret();
                Editor.Focus();
            }
        }

        private void FindButtion_Click(object sender, EventArgs e)
        {
            FindNext(true);
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            FindNext(false);
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            FindPrevious();
        }

        private void CountButton_Click(object sender, EventArgs e)
        {
            string text = SearchBox.Text;
            if (string.IsNullOrEmpty(text) || Editor == null)
            {
                CountLabel.Text = "Count: 0";
                return;
            }

            var mode = CaseCheck.Checked ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            string editorText = Editor.Text;

            int count = 0;
            int index = 0;

            while (true)
            {
                index = editorText.IndexOf(text, index, mode);
                if (index == -1)
                    break;

                count++;
                index += text.Length;
            }

            CountLabel.Text = $"Count: {count}";
        }

        private void ApplyLanguage()
        {
            string lang = Properties.Settings.Default.Language;

            if (lang == "ru")
            {
                groupBox1.Text = "Найти";
                SearchLable.Text = "Поиск:";
                FindButtion.Text = "Поиск";
                NextButton.Text = "Следующее";
                PrevButton.Text = "Прошлое";
                CountButton.Text = "Посчитать";
                CaseCheck.Text = "Учитывать регистр";
                AdvancedBox.Text = "Расширенный поиск (n, r, t, другое..)";
                CountLabel.Text = "Подсчитано: ";
            }
            else
            {
                groupBox1.Text = "Find";
                SearchLable.Text = "Search";
                FindButtion.Text = "Find";
                NextButton.Text = "Next";
                PrevButton.Text = "Prev";
                CountButton.Text = "Count";
                CaseCheck.Text = "Case Sensitive";
                AdvancedBox.Text = "Advanced Search (n, r, t, other..)";
                CountLabel.Text = "Count: ";
            }
        }

        private void ApplyTheme()
        {
            string theme = Properties.Settings.Default.Theme;

            if (theme == "Dark")
            {
                this.BackColor = Color.FromArgb(40, 40, 40);
                groupBox1.ForeColor = SystemColors.Control;
                SearchLable.ForeColor = SystemColors.Control;
                FindButtion.ForeColor = SystemColors.Control;
                FindButtion.BackColor = SystemColors.ControlDarkDark;
                NextButton.ForeColor = SystemColors.Control;
                NextButton.BackColor = SystemColors.ControlDarkDark;
                PrevButton.ForeColor = SystemColors.Control;
                PrevButton.BackColor = SystemColors.ControlDarkDark;
                CountButton.ForeColor = SystemColors.Control;
                CountButton.BackColor = SystemColors.ControlDarkDark;
                CaseCheck.ForeColor = SystemColors.Control;
                CaseCheck.BackColor = SystemColors.ControlDarkDark;
                AdvancedBox.ForeColor = SystemColors.Control;
                AdvancedBox.BackColor = SystemColors.ControlDarkDark;
                CountLabel.ForeColor = SystemColors.Control;
            }
            else
            {
                this.BackColor = SystemColors.Control;
                groupBox1.ForeColor = SystemColors.ControlText;
                SearchLable.ForeColor = SystemColors.ControlText;
                FindButtion.ForeColor = SystemColors.ControlText;
                FindButtion.BackColor = SystemColors.Control;
                NextButton.ForeColor = SystemColors.ControlText;
                NextButton.BackColor = SystemColors.Control;
                PrevButton.ForeColor = SystemColors.ControlText;
                PrevButton.BackColor = SystemColors.Control;
                CountButton.ForeColor = SystemColors.ControlText;
                CountButton.BackColor = SystemColors.Control;
                CaseCheck.ForeColor = SystemColors.ControlText;
                CaseCheck.BackColor = SystemColors.Control;
                AdvancedBox.ForeColor = SystemColors.ControlText;
                AdvancedBox.BackColor = SystemColors.Control;
                CountLabel.ForeColor = SystemColors.ControlText;
            }
        }

    }
}
