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
    public partial class SettingForm: Form
    {
        public SettingForm()
        {
            InitializeComponent();
            ThemeCombo.Items.Add("Light");
            ThemeCombo.Items.Add("Dark");

            ThemeCombo.SelectedItem = Properties.Settings.Default.Theme;

            LanguageCombo.Items.Add("English");
            LanguageCombo.Items.Add("Русский (Russian)");

            if (Properties.Settings.Default.Language == "ru")
                LanguageCombo.SelectedItem = "Русский (Russian)";
            else
                LanguageCombo.SelectedItem = "English";
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Theme = ThemeCombo.SelectedItem.ToString();

            if (LanguageCombo.SelectedItem.ToString() == "Русский (Russian)")
                Properties.Settings.Default.Language = "ru";
            else
                Properties.Settings.Default.Language = "en";

            Properties.Settings.Default.Save();

            MessageBox.Show("Settings Save! Restart Program.");
            this.Close();
        }
    }
}
