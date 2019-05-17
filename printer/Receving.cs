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
    public partial class Receving : Form
    {
        public Receving()
        {
            InitializeComponent();
            Common objCommon = new Common();
            objCommon.SetStyle(this.panel1);
        }
        SQLHelper objSQLHelper;

        private void Receving_Load(object sender, EventArgs e)
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
                    int cpmpanyId = objSQLHelper.ExecuteUpdateProcedure("InsertReceiving", objSqlTransaction
                                                                      , objSQLHelper.SqlParam("@company_paper_id", Global.GloPaperId, SqlDbType.BigInt)
                                                                      , objSQLHelper.SqlParam("@narration", textNarration.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@date", DateTime.Parse(dateTimePicker1.Text.Trim()), SqlDbType.DateTime)
                                                                      , objSQLHelper.SqlParam("@challan_no", textChallanNo.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@receipt_ream", textReamQuantity.Text.Trim(), SqlDbType.Decimal)
                                                                      , objSQLHelper.SqlParam("@ream_size", textFormPerReam.Text.Trim(), SqlDbType.Decimal)
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
                    int cpmpanyId = objSQLHelper.ExecuteDeleteProcedure("DeleteReceiving", objSqlTransaction
                                                                      , objSQLHelper.SqlParam("@receive_id", Int64.Parse(dataGridView1.SelectedRows[0].Cells["receive_id"].Value.ToString()), SqlDbType.BigInt)
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
                    int cpmpanyId = objSQLHelper.ExecuteUpdateProcedure("UpdateReceiving", objSqlTransaction
                                                                      , objSQLHelper.SqlParam("@narration", textNarration.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@date", DateTime.Parse(dateTimePicker1.Text.Trim()), SqlDbType.DateTime)
                                                                      , objSQLHelper.SqlParam("@challan_no", textChallanNo.Text.Trim(), SqlDbType.NVarChar)
                                                                      , objSQLHelper.SqlParam("@received_ream", float.Parse(textReamQuantity.Text.Trim()), SqlDbType.Decimal)
                                                                      , objSQLHelper.SqlParam("@ream_size", float.Parse(textFormPerReam.Text.Trim()), SqlDbType.Decimal)
                                                                      , objSQLHelper.SqlParam("@receive_id", Int64.Parse(dataGridView1.SelectedRows[0].Cells["receive_id"].Value.ToString()), SqlDbType.BigInt)
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

        #region Grid operation

        void GriBind()
        {
            try
            {
                objSQLHelper = new SQLHelper();
                DataTable dt = objSQLHelper.ExecuteSelectProcedure("SelectReceivingByComPaperId"
                                                                      , objSQLHelper.SqlParam("@company_paper_id", Global.GloPaperId, SqlDbType.BigInt)
                                                                      );
                dataGridView1.DataSource = dt;
                dataGridView1.Columns["receive_id"].Visible = false;


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

            textChallanNo.Text = "";
            textFormPerReam.Text = "500";
            dateTimePicker1.Text = "";
            textReamQuantity.Text = "";
            textNarration.Text = "From Publishers";
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

            textChallanNo.Text = "";
            textFormPerReam.Text = "500";
            dateTimePicker1.Text = "";
            textReamQuantity.Text = "";
            textNarration.Text = "From Publishers";
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

                    long temp = Int64.Parse(dataGridView1.SelectedRows[0].Cells["receive_id"].Value.ToString());
                    objSQLHelper = new SQLHelper();
                    DataTable dt = objSQLHelper.ExecuteSelectProcedure("SelectReceivingById"
                                                                   , objSQLHelper.SqlParam("@receive_id", temp, SqlDbType.BigInt)
                                                                   );
                    if (dt.Rows.Count > 0)
                    {
                        textChallanNo.Text = dt.Rows[0]["challan_no"].ToString();
                        textFormPerReam.Text = dt.Rows[0]["ream_size"].ToString();
                        dateTimePicker1.Text = dt.Rows[0]["date"].ToString();
                        textReamQuantity.Text = dt.Rows[0]["receipt_ream"].ToString();
                        textNarration.Text = dt.Rows[0]["narration"].ToString();  
                        
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

                if ((textNarration.Text.Trim().Equals("")) || (textChallanNo.Text.Trim().Equals("")))
                {
                    //MessageBox.Show("Not a Numeric Input ! Try again");
                    if (textNarration.Text.Trim().Equals(""))
                    {
                        textNarration.Focus();
                    }
                    else if (textChallanNo.Text.Trim().Equals(""))
                    {
                        textChallanNo.Focus();
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

        private void Previous_Click(object sender, EventArgs e)
        {
            SelectOption objSelectOption = new SelectOption();
            objSelectOption.TopLevel = false;
            objSelectOption.Parent = this.Parent;
            objSelectOption.Show();

            this.Dispose();

        }
        //private void Receving_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    this.DialogResult = DialogResult.OK;
        //}

        #endregion

    }
}



