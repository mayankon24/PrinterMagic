using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Configuration;
using Excel = Microsoft.Office.Interop.Excel;



namespace printer
{
    public partial class Default : Form
    {
        public Default()
        {
            InitializeComponent();
        }

        private void Default_Load(object sender, EventArgs e)
        {
            this.Height = Screen.PrimaryScreen.Bounds.Height-50;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Top = 0;
            this.Left = 0;

            //this.MdiChildren[0].Close();
            Company newMDIChild = new Company();
            // Set The Parent Form Of The Child Window.
            newMDIChild.TopLevel = false;
            newMDIChild.Parent = this.panel1;
            // Display the new form.


            newMDIChild.Show();
        }

        void CloseAllChild()
        {

            foreach (Control child in this.panel1.Controls)
            {
                child.Dispose();
            }
           
        }
    }
}

