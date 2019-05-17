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
    public partial class Paper : Form
    {
        public Paper()
        {
            InitializeComponent();
            Common objCommon = new Common();
            objCommon.SetStyle(this.panel1);
        }


        SQLHelper objSQLHelper;

        private void Paper_Load(object sender, EventArgs e)
        {
            clearControl();
            lblCompany.Text = Global.GloCompanyName;
        }

        #region event

        private void btnSave_Click(object sender, EventArgs e)
        {

            objSQLHelper = new SQLHelper();
            SqlTransaction objSqlTransaction = objSQLHelper.BeginTrans();
            if (ValidateControl())
            {

                try
                {

                    int cpmpanyId = objSQLHelper.ExecuteInsertProcedure("InsertCompanyPaper", objSqlTransaction
                                                                      , objSQLHelper.SqlParam("@company_id", Global.GloCopmanyId, SqlDbType.BigInt)
                                                                      , objSQLHelper.SqlParam("@paper_name", textPaperName.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@quality", textQuality.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@size", textSize.Text.Trim(), SqlDbType.NVarChar)                                                                      
                                                                      , objSQLHelper.SqlParam("@weight", textWeight.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@color", "White", SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@default_westage_percentage", textDefaultWestagePercentage.Text.Trim(), SqlDbType.Decimal)
                                                                      , objSQLHelper.SqlParam("@default_westage_form", textDefaultWestageForm.Text.Trim(), SqlDbType.Decimal)                                                                      
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
                    int cpmpanyId = objSQLHelper.ExecuteDeleteProcedure("DeleteCompanyPaper", objSqlTransaction
                                                                      , objSQLHelper.SqlParam("@company_paper_id", Int64.Parse(dataGridView1.SelectedRows[0].Cells["company_paper_id"].Value.ToString()), SqlDbType.BigInt));
                    objSqlTransaction.Commit();

                }
                catch
                {
                    objSqlTransaction.Rollback();

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
                    int cpmpanyId = objSQLHelper.ExecuteUpdateProcedure("UpdateCompanyPaper", objSqlTransaction
                                                                      , objSQLHelper.SqlParam("@paper_name", textPaperName.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@quality", textQuality.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@size", textSize.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@weight", textWeight.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@color", "White", SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@default_westage_percentage", textDefaultWestagePercentage.Text.Trim(), SqlDbType.Decimal)
                                                                      , objSQLHelper.SqlParam("@default_westage_form", textDefaultWestageForm.Text.Trim(), SqlDbType.Decimal)
                                                                      , objSQLHelper.SqlParam("@company_paper_id", Int64.Parse(dataGridView1.SelectedRows[0].Cells["company_paper_id"].Value.ToString()), SqlDbType.BigInt)
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
                Global.GloPaperId = Int64.Parse(dataGridView1.SelectedRows[0].Cells["company_paper_id"].Value.ToString());
                Global.GloPaperName = dataGridView1.SelectedRows[0].Cells["Paper Name"].Value.ToString();
                SelectOption objSelectOption = new SelectOption();
                objSelectOption.TopLevel = false;
                objSelectOption.Parent = this.Parent;
                objSelectOption.Show();
                this.Dispose();
            }
            catch { }
        }
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                Company objCompany = new Company();
                objCompany.TopLevel = false;
                objCompany.Parent = this.Parent;
                objCompany.Show();
                this.Dispose();
            }
            catch
            { }
        }

        #endregion

        #region Grid operation

        void GriBind()
        {
            try
            {
                objSQLHelper = new SQLHelper();
                DataTable dt = objSQLHelper.ExecuteSelectProcedure("SelectCompanyPaperByCompanyId"
                                                                , objSQLHelper.SqlParam("@company_id", Global.GloCopmanyId, SqlDbType.BigInt)
                                                                );

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["company_paper_id"].Visible = false;


            }
            catch
            {
            }

        }
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

            textPaperName.Text = "";
            textQuality.Text = "";
            textSize.Text = "";
            textWeight.Text = "";
            textDefaultWestageForm.Text = "";
            textDefaultWestagePercentage.Text = "";

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

            textPaperName.Text = "";
            textQuality.Text = "";
            textSize.Text = "";
            textWeight.Text = "";  
            textDefaultWestageForm.Text ="";
            textDefaultWestagePercentage.Text = "";

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
                    DataTable dt = objSQLHelper.ExecuteSelectProcedure("SelectCompanyPaperById"
                                       , objSQLHelper.SqlParam("@company_paper_id", Int64.Parse(dataGridView1.SelectedRows[0].Cells["company_paper_id"].Value.ToString()), SqlDbType.BigInt)
                                       );
                    if (dt.Rows.Count > 0)
                    {
                        textPaperName.Text = dt.Rows[0]["paper_name"].ToString();
                        textQuality.Text = dt.Rows[0]["quality"].ToString();
                        textSize.Text = dt.Rows[0]["size"].ToString();
                        textWeight.Text = dt.Rows[0]["weight"].ToString();
                        textDefaultWestagePercentage.Text = dt.Rows[0]["default_westage_percentage"].ToString();
                        textDefaultWestageForm.Text = dt.Rows[0]["default_westage_form"].ToString(); 
                    }
                }
            }
            catch
            { }

        }
        private bool ValidateControl()
        {
            try
            {

                float temp = float.Parse(textDefaultWestagePercentage.Text.Trim());

                if ((textPaperName.Text.Trim().Equals("")) || (temp >= 100.00) || (temp < 0))
                {
                    //MessageBox.Show("Not a Numeric Input ! Try again");
                    if (textPaperName.Text.Trim().Equals(""))
                    {
                        textPaperName.Focus();
                    }
                    else
                    {
                        textDefaultWestagePercentage.Focus();
                    }

                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }            

        }
       
        //private void Paper_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    this.DialogResult = DialogResult.OK;
        //}

        #endregion

    }
}
