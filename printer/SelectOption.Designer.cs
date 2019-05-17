namespace printer
{
    partial class SelectOption
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPaper = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.Previous = new System.Windows.Forms.Button();
            this.btnReceving = new System.Windows.Forms.Button();
            this.btnIssue = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SkyBlue;
            this.panel1.Controls.Add(this.lblPaper);
            this.panel1.Controls.Add(this.lblCompany);
            this.panel1.Controls.Add(this.Previous);
            this.panel1.Controls.Add(this.btnReceving);
            this.panel1.Controls.Add(this.btnIssue);
            this.panel1.Controls.Add(this.btnReport);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(953, 616);
            this.panel1.TabIndex = 0;
            // 
            // lblPaper
            // 
            this.lblPaper.AccessibleName = "SetStyle2";
            this.lblPaper.AutoSize = true;
            this.lblPaper.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaper.ForeColor = System.Drawing.Color.Black;
            this.lblPaper.Location = new System.Drawing.Point(410, 34);
            this.lblPaper.Name = "lblPaper";
            this.lblPaper.Size = new System.Drawing.Size(46, 17);
            this.lblPaper.TabIndex = 63;
            this.lblPaper.Text = "Paper";
            // 
            // lblCompany
            // 
            this.lblCompany.AccessibleName = "SetStyle1";
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompany.ForeColor = System.Drawing.Color.Black;
            this.lblCompany.Location = new System.Drawing.Point(408, 9);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(97, 25);
            this.lblCompany.TabIndex = 59;
            this.lblCompany.Text = "Company";
            // 
            // Previous
            // 
            this.Previous.AccessibleName = "SetStyle2";
            this.Previous.Location = new System.Drawing.Point(691, 465);
            this.Previous.Name = "Previous";
            this.Previous.Size = new System.Drawing.Size(75, 23);
            this.Previous.TabIndex = 62;
            this.Previous.Text = "Previous";
            this.Previous.UseVisualStyleBackColor = true;
            this.Previous.Click += new System.EventHandler(this.Previous_Click);
            // 
            // btnReceving
            // 
            this.btnReceving.BackColor = System.Drawing.Color.SlateGray;
            this.btnReceving.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReceving.ForeColor = System.Drawing.Color.White;
            this.btnReceving.Location = new System.Drawing.Point(677, 86);
            this.btnReceving.Name = "btnReceving";
            this.btnReceving.Size = new System.Drawing.Size(159, 161);
            this.btnReceving.TabIndex = 61;
            this.btnReceving.Text = "Receving";
            this.btnReceving.UseVisualStyleBackColor = false;
            this.btnReceving.Click += new System.EventHandler(this.btnReceving_Click);
            // 
            // btnIssue
            // 
            this.btnIssue.BackColor = System.Drawing.Color.SlateGray;
            this.btnIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIssue.ForeColor = System.Drawing.Color.White;
            this.btnIssue.Location = new System.Drawing.Point(368, 340);
            this.btnIssue.Name = "btnIssue";
            this.btnIssue.Size = new System.Drawing.Size(159, 161);
            this.btnIssue.TabIndex = 60;
            this.btnIssue.Text = "Issue";
            this.btnIssue.UseVisualStyleBackColor = false;
            this.btnIssue.Click += new System.EventHandler(this.btnIssue_Click);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.SlateGray;
            this.btnReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.White;
            this.btnReport.Location = new System.Drawing.Point(71, 69);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(159, 161);
            this.btnReport.TabIndex = 59;
            this.btnReport.Text = "Report";
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // SelectOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 616);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectOption";
            this.Text = "SelectOption";
            this.Load += new System.EventHandler(this.SelectOption_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button btnReceving;
        private System.Windows.Forms.Button btnIssue;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button Previous;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.Label lblPaper;
    }
}