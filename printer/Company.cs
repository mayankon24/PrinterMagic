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
    public partial class Company : Form
    {
        SQLHelper objSQLHelper;
        public Company()
        {
            InitializeComponent();
            Common objCommon = new Common();
            objCommon.SetStyle(this.panel1);
        }

        private void Company_Load(object sender, EventArgs e)
        {
            clearControl();

        }

        #region Event

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            objSQLHelper = new SQLHelper();
            SqlTransaction objSqlTransaction = objSQLHelper.BeginTrans();
            if (ValidateControl())
            {
                try
                {
                    int cpmpanyId = objSQLHelper.ExecuteInsertProcedure("InsertCompany", objSqlTransaction
                                                                      , objSQLHelper.SqlParam("@tin_no", txtTin.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@company_name", txtCompanyName.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@address1", textAddress1.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@pan_no", textPan.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@city", textCity.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@state", textState.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@pincode", textpinCode.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@email", textEmail.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@phone", textPhone.Text.Trim(), SqlDbType.NVarChar)
                                                                     );

                    objSqlTransaction.Commit();
                }
                catch
                {
                    objSqlTransaction.Rollback();

                }
                clearControl();
            }
   
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string str = MessageBox.Show("Are you want to delete this Item", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning).ToString();

            if (str.Equals("Yes"))
            {
                objSQLHelper = new SQLHelper();
                SqlTransaction objSqlTransaction = objSQLHelper.BeginTrans();
                try
                {
                    int cpmpanyId = objSQLHelper.ExecuteDeleteProcedure("DeleteCompany", objSqlTransaction
                                                                      , objSQLHelper.SqlParam("@company_id", Int32.Parse(dataGridView1.SelectedRows[0].Cells["company_id"].Value.ToString()), SqlDbType.Int)
                                                                      );
                    objSqlTransaction.Commit();

                }
                catch
                {
                    objSqlTransaction.Rollback();
                    MessageBox.Show("First delete depanded entry Of this company", "", MessageBoxButtons.OK, MessageBoxIcon.Warning).ToString();
                 
                }
            }
            clearControl();           
            
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            objSQLHelper = new SQLHelper();
            SqlTransaction objSqlTransaction = objSQLHelper.BeginTrans();
            if (ValidateControl())
            {
                try
                {
                    int cpmpanyId = objSQLHelper.ExecuteUpdateProcedure("UpdateCompany", objSqlTransaction
                                                                      , objSQLHelper.SqlParam("@tin_no", txtTin.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@company_name", txtCompanyName.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@address1", textAddress1.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@pan_no", textPan.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@city", textCity.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@state", textState.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@pincode", textpinCode.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@email", textEmail.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@phone", textPhone.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@company_id", Int32.Parse(dataGridView1.SelectedRows[0].Cells["company_id"].Value.ToString()), SqlDbType.Int)
                                                                     );

                    objSqlTransaction.Commit();
                }
                catch
                {
                    objSqlTransaction.Rollback();

                }
                clearControl();
            }

        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
           clearControl();

        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                Global.GloCopmanyId = Int32.Parse(dataGridView1.SelectedRows[0].Cells["company_id"].Value.ToString());
                Global.GloCompanyName = dataGridView1.SelectedRows[0].Cells["Company Name"].Value.ToString();
                Paper objPaper = new Paper();
                objPaper.TopLevel = false;
                objPaper.Parent = this.Parent;
                objPaper.Show();
                this.Dispose();
            }
            catch
            { }

        }

        #endregion

        #region Grid operation

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Controlbind();
        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            Controlbind();
        }
        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;

            txtTin.Text = "";
            txtCompanyName.Text = "";
            textAddress1.Text = "";
            textPan.Text = "";
            textCity.Text = "";
            textState.Text = "";
            textpinCode.Text = "";
            textEmail.Text = "";
            textPhone.Text = "";
        }
        void GriBind()
        {
            try
            {
                objSQLHelper = new SQLHelper();
                DataTable dt = objSQLHelper.ExecuteSelectProcedure("SelectCompanyAll");
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["company_id"].Visible = false;


            }
            catch
            {
            }

        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            Common objCommon = new Common();
            objCommon.ExcelExport(dataGridView1, saveFileDialog1);
        }

        #endregion 

        #region method

        private void clearControl()
        {
            GriBind();
            
           
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;

            txtTin.Text = "";
            txtCompanyName.Text = "";
            textAddress1.Text = "";
            textPan.Text = "";
            textCity.Text = "";
            textState.Text = "";
            textpinCode.Text = "";
            textEmail.Text = "";
            textPhone.Text = "";

        }       
        private void Controlbind()
        {
            try
            {
                if (true)
                {
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;

                    objSQLHelper = new SQLHelper();
                    DataTable dt = objSQLHelper.ExecuteSelectProcedure("SelectCompanyById"
                                       , objSQLHelper.SqlParam("@company_id", Int32.Parse(dataGridView1.SelectedRows[0].Cells["company_id"].Value.ToString()), SqlDbType.Int)
                                       );
                    if (dt.Rows.Count > 0)
                    {
                        txtTin.Text = dt.Rows[0]["tin_no"].ToString();
                        txtCompanyName.Text = dt.Rows[0]["company_name"].ToString();
                        textAddress1.Text = dt.Rows[0]["address1"].ToString();
                        textPan.Text = dt.Rows[0]["pan_no"].ToString();
                        textCity.Text = dt.Rows[0]["city"].ToString();
                        textState.Text = dt.Rows[0]["state"].ToString();
                        textpinCode.Text = dt.Rows[0]["pincode"].ToString();
                        textEmail.Text = dt.Rows[0]["email"].ToString();
                        textPhone.Text = dt.Rows[0]["phone"].ToString();


                    }
                }
            }
            catch
            { }

        }
        private bool ValidateControl()
        {

            if (txtCompanyName.Text.Trim().Equals(""))
            {
                //MessageBox.Show("Not a Numeric Input ! Try again");
                txtCompanyName.Focus();
                return false;
            }
            else
            {
                return true;
            }

        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    SQLHelper objSQLHelper = new SQLHelper();
        //    string path = Path.GetDirectoryName(Application.ExecutablePath);
        //    objSQLHelper.BackupDatabase(path);
        //}
       

        #endregion       
       
    }
}
