namespace ReTEXT
{
    partial class FindForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindForm));
            this.SearchLable = new System.Windows.Forms.Label();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.FindButtion = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            this.PrevButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.CountLabel = new System.Windows.Forms.Label();
            this.CountButton = new System.Windows.Forms.Button();
            this.CaseCheck = new System.Windows.Forms.CheckBox();
            this.AdvancedBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchLable
            // 
            this.SearchLable.AutoSize = true;
            this.SearchLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.SearchLable.Location = new System.Drawing.Point(6, 18);
            this.SearchLable.Name = "SearchLable";
            this.SearchLable.Size = new System.Drawing.Size(57, 17);
            this.SearchLable.TabIndex = 0;
            this.SearchLable.Text = "Search:";
            // 
            // SearchBox
            // 
            this.SearchBox.Location = new System.Drawing.Point(69, 15);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(508, 20);
            this.SearchBox.TabIndex = 1;
            // 
            // FindButtion
            // 
            this.FindButtion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.FindButtion.Location = new System.Drawing.Point(6, 69);
            this.FindButtion.Name = "FindButtion";
            this.FindButtion.Size = new System.Drawing.Size(114, 50);
            this.FindButtion.TabIndex = 2;
            this.FindButtion.Text = "Find";
            this.FindButtion.UseVisualStyleBackColor = true;
            this.FindButtion.Click += new System.EventHandler(this.FindButtion_Click);
            // 
            // NextButton
            // 
            this.NextButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.NextButton.Location = new System.Drawing.Point(149, 69);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(114, 50);
            this.NextButton.TabIndex = 3;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // PrevButton
            // 
            this.PrevButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.PrevButton.Location = new System.Drawing.Point(297, 69);
            this.PrevButton.Name = "PrevButton";
            this.PrevButton.Size = new System.Drawing.Size(114, 50);
            this.PrevButton.TabIndex = 4;
            this.PrevButton.Text = "Prev";
            this.PrevButton.UseVisualStyleBackColor = true;
            this.PrevButton.Click += new System.EventHandler(this.PrevButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AdvancedBox);
            this.groupBox1.Controls.Add(this.CaseCheck);
            this.groupBox1.Controls.Add(this.CountLabel);
            this.groupBox1.Controls.Add(this.CountButton);
            this.groupBox1.Controls.Add(this.PrevButton);
            this.groupBox1.Controls.Add(this.NextButton);
            this.groupBox1.Controls.Add(this.FindButtion);
            this.groupBox1.Controls.Add(this.SearchBox);
            this.groupBox1.Controls.Add(this.SearchLable);
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(665, 251);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Find";
            // 
            // CountLabel
            // 
            this.CountLabel.AutoSize = true;
            this.CountLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.CountLabel.Location = new System.Drawing.Point(5, 219);
            this.CountLabel.Name = "CountLabel";
            this.CountLabel.Size = new System.Drawing.Size(56, 20);
            this.CountLabel.TabIndex = 6;
            this.CountLabel.Text = "Count:";
            // 
            // CountButton
            // 
            this.CountButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.CountButton.Location = new System.Drawing.Point(445, 69);
            this.CountButton.Name = "CountButton";
            this.CountButton.Size = new System.Drawing.Size(114, 50);
            this.CountButton.TabIndex = 5;
            this.CountButton.Text = "Count";
            this.CountButton.UseVisualStyleBackColor = true;
            this.CountButton.Click += new System.EventHandler(this.CountButton_Click);
            // 
            // CaseCheck
            // 
            this.CaseCheck.AutoSize = true;
            this.CaseCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.CaseCheck.Location = new System.Drawing.Point(9, 140);
            this.CaseCheck.Name = "CaseCheck";
            this.CaseCheck.Size = new System.Drawing.Size(133, 24);
            this.CaseCheck.TabIndex = 7;
            this.CaseCheck.Text = "Case Sensitive";
            this.CaseCheck.UseVisualStyleBackColor = true;
            // 
            // AdvancedBox
            // 
            this.AdvancedBox.AutoSize = true;
            this.AdvancedBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.AdvancedBox.Location = new System.Drawing.Point(9, 170);
            this.AdvancedBox.Name = "AdvancedBox";
            this.AdvancedBox.Size = new System.Drawing.Size(272, 24);
            this.AdvancedBox.TabIndex = 8;
            this.AdvancedBox.Text = "Advanced Search (\\n , \\r, \\t, other..)\r\n";
            this.AdvancedBox.UseVisualStyleBackColor = true;
            // 
            // FindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 268);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FindForm";
            this.Text = "Find";
            this.Load += new System.EventHandler(this.FindForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label SearchLable;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Button FindButtion;
        private System.Windows.Forms.Button NextButton;
        private System.Windows.Forms.Button PrevButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button CountButton;
        private System.Windows.Forms.Label CountLabel;
        private System.Windows.Forms.CheckBox CaseCheck;
        private System.Windows.Forms.CheckBox AdvancedBox;
    }
}