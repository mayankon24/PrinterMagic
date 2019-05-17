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
    public partial class Issue : Form
    {
        public Issue()
        {
            InitializeComponent();
            Common objCommon = new Common();
            objCommon.SetStyle(this.panel1);
        }
        SQLHelper objSQLHelper;

        private void Issue_Load(object sender, EventArgs e)
        {
           clearControl();
           lblCompany.Text = Global.GloCompanyName;
           lblPaper.Text = Global.GloPaperName;
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
                    int cpmpanyId = objSQLHelper.ExecuteUpdateProcedure("InsertIssue", objSqlTransaction
                                                                      , objSQLHelper.SqlParam("@project_name", textBookName.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@book_name", textBookName.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@book_quantity", float.Parse(textIssueQuantity.Text.Trim()), SqlDbType.Decimal)
                                                                      , objSQLHelper.SqlParam("@book_page",1, SqlDbType.Decimal)
                                                                      , objSQLHelper.SqlParam("@form_use", float.Parse(textFormUsed.Text.Trim()), SqlDbType.Decimal)
                                                                      , objSQLHelper.SqlParam("@westage_percentage", float.Parse(textWestagePercentage.Text.Trim()), SqlDbType.Decimal)
                                                                      , objSQLHelper.SqlParam("@westage_form", float.Parse(textFormWastage.Text.Trim()), SqlDbType.Decimal)
                                                                      , objSQLHelper.SqlParam("@company_paper_id", Global.GloPaperId, SqlDbType.BigInt)
                                                                      , objSQLHelper.SqlParam("@date",DateTime.Parse(dateTimePicker1.Text.Trim()), SqlDbType.DateTime)
                                                                      , objSQLHelper.SqlParam("@challan_no",textChallanNo.Text.Trim(), SqlDbType.NVarChar)
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
                    int cpmpanyId = objSQLHelper.ExecuteDeleteProcedure("DeleteIssue", objSqlTransaction
                                                                      , objSQLHelper.SqlParam("@issue_id", Int32.Parse(dataGridView1.SelectedRows[0].Cells["issue_id"].Value.ToString()), SqlDbType.Int)
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
                    int cpmpanyId = objSQLHelper.ExecuteUpdateProcedure("UpdateIssue", objSqlTransaction
                                                                      , objSQLHelper.SqlParam("@project_name", textBookName.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@book_name", textBookName.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@book_quantity", float.Parse(textIssueQuantity.Text.Trim()), SqlDbType.Decimal)
                                                                      , objSQLHelper.SqlParam("@book_page", 1, SqlDbType.Decimal)
                                                                      , objSQLHelper.SqlParam("@form_use", float.Parse(textFormUsed.Text.Trim()), SqlDbType.Decimal)
                                                                      , objSQLHelper.SqlParam("@wastage_percentage", float.Parse(textWestagePercentage.Text.Trim()), SqlDbType.Decimal)
                                                                      , objSQLHelper.SqlParam("@wastage_form", float.Parse(textFormWastage.Text.Trim()), SqlDbType.Decimal)
                                                                     
                                                                      , objSQLHelper.SqlParam("@date", DateTime.Parse(dateTimePicker1.Text.Trim()), SqlDbType.DateTime)
                                                                      , objSQLHelper.SqlParam("@challan_no", textChallanNo.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@issue_quantity", float.Parse(textIssueQuantity.Text.Trim()), SqlDbType.Decimal)
                                                                      , objSQLHelper.SqlParam("@issue_id", Int32.Parse(dataGridView1.SelectedRows[0].Cells["issue_id"].Value.ToString()), SqlDbType.Int)
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

        #endregion

        #region Grid Operation

        void GriBind()
        {
            try
            {
                objSQLHelper = new SQLHelper();
                DataTable dt = objSQLHelper.ExecuteSelectProcedure("SelectIssueByComPaperId"
                                                                      , objSQLHelper.SqlParam("@company_paper_id", Global.GloPaperId, SqlDbType.BigInt)
                                                                      );
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["issue_id"].Visible = false;


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

            objSQLHelper = new SQLHelper();
            DataTable dt = objSQLHelper.ExecuteSelectProcedure("GetConfig"
                                                                  , objSQLHelper.SqlParam("@company_paper_id", Global.GloPaperId, SqlDbType.BigInt)
                                                                  );

            textWestagePercentage.Text = dt.Rows[0]["default_westage_percentage"].ToString();
            textFormWastage.Text = dt.Rows[0]["default_westage_form"].ToString();
            textChallanNo.Text = "";
            textIssueQuantity.Text = "";
            textBookName.Text = "";
            textFormUsed.Text = "";
            dateTimePicker1.Text = "";
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            Common objCommon = new Common();
            objCommon.ExcelExport(dataGridView1, saveFileDialog1);
        }

        #endregion

        #region Method

        private void clearControl()
        {
            
            GriBind();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;

            objSQLHelper = new SQLHelper();
            DataTable dt = objSQLHelper.ExecuteSelectProcedure("GetConfig"
                                                                  , objSQLHelper.SqlParam("@company_paper_id", Global.GloPaperId, SqlDbType.BigInt)
                                                                  );

            textWestagePercentage.Text = dt.Rows[0]["default_westage_percentage"].ToString();
            textFormWastage.Text = dt.Rows[0]["default_westage_form"].ToString();
            textChallanNo.Text = "";
            textIssueQuantity.Text = "";
            textBookName.Text = "";
            textFormUsed.Text = "";
            dateTimePicker1.Text = "";
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
                    DataTable dt = objSQLHelper.ExecuteSelectProcedure("SelectIssueById"
                                       , objSQLHelper.SqlParam("@issue_id", Int64.Parse(dataGridView1.SelectedRows[0].Cells["issue_id"].Value.ToString()), SqlDbType.BigInt)
                                       );
                    if (dt.Rows.Count > 0)
                    {
                        textWestagePercentage.Text = dt.Rows[0]["westage_percentage"].ToString();
                        textFormWastage.Text = dt.Rows[0]["westage_form"].ToString();
                        textChallanNo.Text = dt.Rows[0]["challan_no"].ToString();
                        textIssueQuantity.Text = dt.Rows[0]["issue_quantity"].ToString();
                        textBookName.Text = dt.Rows[0]["book_name"].ToString();
                        textFormUsed.Text = dt.Rows[0]["form_use"].ToString();
                        dateTimePicker1.Text = dt.Rows[0]["date"].ToString();
                    }
                }
            }
            catch
            { }

        }
        private bool ValidateControl()
        {
            //try
            //{

            //    float temp = float.Parse(textWastagePercentage.Text.Trim());

            //    if ((textProjectName.Text.Trim().Equals("")) || (textBookName.Text.Trim().Equals("")) || (temp >= 100.00) || (temp < 0))
            //    {
            //        //MessageBox.Show("Not a Numeric Input ! Try again");
            //        if (textProjectName.Text.Trim().Equals(""))
            //        {
            //            textProjectName.Focus();
            //        }
            //        else if (textBookName.Text.Trim().Equals(""))
            //        {
            //            textBookName.Focus();
            //        }
            //        else
            //        {
            //            textWastagePercentage.Focus();
            //        }

            //        return false;
            //    }
            //    else
            //    {
            //        return true;
            //    }
            //}
            //catch
            //{
            //    return false;
            //}
            return true;

        }

        #endregion

        private void Previous_Click(object sender, EventArgs e)
        {
            SelectOption objSelectOption = new SelectOption();
            objSelectOption.TopLevel = false;
            objSelectOption.Parent = this.Parent;
            objSelectOption.Show();

            this.Dispose();
        }

        //private void Issue_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    this.DialogResult = DialogResult.OK;
        //}


    }
}



