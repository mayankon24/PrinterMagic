using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace printer
{
    public partial class SelectOption : Form
    {
        public SelectOption()
        {
            InitializeComponent();
            Common objCommon = new Common();
            objCommon.SetStyle(this.panel1);
            lblCompany.Text = Global.GloCompanyName;
            lblPaper.Text = Global.GloPaperName;
        }

        private void SelectOption_Load(object sender, EventArgs e)
        {

        }

        private void Previous_Click(object sender, EventArgs e)
        {
            Paper objPaper = new Paper();
            objPaper.TopLevel = false;
            objPaper.Parent = this.Parent;
            objPaper.Show();
            this.Dispose();
        }

        private void btnReceving_Click(object sender, EventArgs e)
        {
            try
            {
                Receving objReceving = new Receving();
                objReceving.TopLevel = false;
                objReceving.Parent = this.Parent;
                objReceving.Show();
                this.Dispose();
            }
            catch { }

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            try
            {
                Issue objIssue = new Issue();
                objIssue.TopLevel = false;
                objIssue.Parent = this.Parent;
                objIssue.Show();
                this.Dispose();
            }
            catch { }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                Report objReport = new Report();
                objReport.TopLevel = false;
                objReport.Parent = this.Parent;
                objReport.Show();
                this.Dispose();
            }
            catch { }

        }

       
    }
}
