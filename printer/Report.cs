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
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
            Common objCommon = new Common();
            objCommon.SetStyle(this.panel1);
            lblCompany.Text = Global.GloCompanyName;
            lblPaper.Text = Global.GloPaperName;
        }

        SQLHelper objSQLHelper;
        

        private void Report_Load(object sender, EventArgs e)
        {           
            clearControl();
        }


        #region Event

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearControl();
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            GriBind();
        }
        private void dateTimePickerFromDate_ValueChanged(object sender, EventArgs e)
        {
            GriBind();
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            GriBind();
        }
        private void Previous_Click(object sender, EventArgs e)
        {
            SelectOption objSelectOption = new SelectOption();
            objSelectOption.TopLevel = false;
            objSelectOption.Parent = this.Parent;
            objSelectOption.Show();

            this.Dispose();
        }       

        #endregion

        #region Grid bind

        void GriBind()
        {
            try
            {
                objSQLHelper = new SQLHelper();
                SqlTransaction objSqlTransaction = objSQLHelper.BeginTrans();

                decimal OpeningBalance = objSQLHelper.ExecuteScalarProcedure("GetOpeningBalnce", objSqlTransaction
                                                                           , objSQLHelper.SqlParam("@company_paper_id", Global.GloPaperId, SqlDbType.BigInt)
                                                                           , objSQLHelper.SqlParam("@from_date", DateTime.Parse(dateTimePickerFromDate.Text.Trim()), SqlDbType.DateTime)
                                                                           , objSQLHelper.SqlParam("@to_date", DateTime.Parse(dateTimePickerToDate.Text.Trim()), SqlDbType.DateTime)
                                                                           );

                DataTable dt = objSQLHelper.ExecuteSelectProcedure("ReportFinal"
                                                                      , objSQLHelper.SqlParam("@company_paper_id", Global.GloPaperId, SqlDbType.BigInt)
                                                                      , objSQLHelper.SqlParam("@from_date", DateTime.Parse(dateTimePickerFromDate.Text.Trim()), SqlDbType.DateTime)
                                                                      , objSQLHelper.SqlParam("@to_date", DateTime.Parse(dateTimePickerToDate.Text.Trim()), SqlDbType.DateTime)
                                                                      );

                decimal temp = OpeningBalance;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if ((int)dt.Rows[i]["Type"] == 0)
                    {
                        temp = temp - Convert.ToDecimal(dt.Rows[i]["Final Quantity in Ream"]);
                    }
                    else if ((int)dt.Rows[i]["Type"] == 1)
                    {
                        temp = temp + Convert.ToDecimal(dt.Rows[i]["Receipt in Ream"]);
                    }
                    dt.Rows[i]["Balance"] = temp;
                }


                DataRow dr = dt.NewRow();
                dr["Particulars"] = "OpeningBalnce";
                dr["Balance"] = OpeningBalance;
                dt.Rows.InsertAt(dr, 0);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    decimal Balance = Convert.ToDecimal(dt.Rows[i]["Balance"]);
                    dt.Rows[i]["Balance in Ream"] = ConvertIntoString(Balance);
                }

                dt.Columns.Remove("Type");
                dt.Columns.Remove("Balance");

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["Id"].Visible = false;
                //dataGridView1.Columns["Balance"].Visible = false;

            }
            catch
            {
            }

        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                Excel.Application xlApp;
                Excel.Workbook xlWorkBook;
                Excel.Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.ApplicationClass();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                int i = 0;
                int j = 0;

                xlWorkSheet.Cells[2, 6] = Global.GloCompanyName;
                xlWorkSheet.Cells[3, 6] = Global.GloPaperName;

                for (j = 1; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    xlWorkSheet.Cells[6, j] = dataGridView1.Columns[j].HeaderText;
                }

                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    for (j = 1; j <= dataGridView1.ColumnCount - 1; j++)
                    {
                        DataGridViewCell cell = dataGridView1[j, i];
                        xlWorkSheet.Cells[i + 7, j] = cell.Value;
                    }
                }

                #region Excel Style formating
                //formate excel file cell header style
                Excel.Range rg = (Excel.Range)xlWorkSheet.Rows[6, Type.Missing];
                rg.HorizontalAlignment = Excel.Constants.xlCenter;
                rg.Font.Name = "Aerial";
                rg.Font.Size = 10;
                rg.WrapText = true;
                rg.Font.Bold = true;

                //formate excel Heading Style
                rg = (Excel.Range)xlWorkSheet.Cells[2, 6];
                rg.Font.Bold = true;
                rg.Font.Size = 15;

                //formate excel Heading Style
                rg = (Excel.Range)xlWorkSheet.Cells[3, 6];
                rg.Font.Size = 12;



                //formate excel file cell header width
                rg = (Excel.Range)xlWorkSheet.Cells[6, 1];
                rg.Cells.ColumnWidth = 11;

                rg = (Excel.Range)xlWorkSheet.Cells[6, 2];
                rg.Cells.ColumnWidth = 11;

                rg = (Excel.Range)xlWorkSheet.Cells[6, 3];
                rg.Cells.ColumnWidth = 25;

                rg = (Excel.Range)xlWorkSheet.Cells[6, 4];
                rg.Cells.ColumnWidth = 8;

                rg = (Excel.Range)xlWorkSheet.Cells[6, 5];
                rg.Cells.ColumnWidth = 10;

                rg = (Excel.Range)xlWorkSheet.Cells[6, 6];
                rg.Cells.ColumnWidth = 8;

                rg = (Excel.Range)xlWorkSheet.Cells[6, 7];
                rg.Cells.ColumnWidth = 13;
                rg = (Excel.Range)xlWorkSheet.Cells[6, 8];
                rg.Cells.ColumnWidth = 8;

                rg = (Excel.Range)xlWorkSheet.Cells[6, 9];
                rg.Cells.ColumnWidth = 12;

                rg = (Excel.Range)xlWorkSheet.Cells[6, 10];
                rg.Cells.ColumnWidth = 9;

                rg = (Excel.Range)xlWorkSheet.Cells[6, 11];
                rg.Cells.ColumnWidth = 10;

                rg = (Excel.Range)xlWorkSheet.Cells[6, 12];
                rg.Cells.ColumnWidth = 13;

                rg = (Excel.Range)xlWorkSheet.Cells[6, 13];
                rg.Cells.ColumnWidth = 11;

                rg = (Excel.Range)xlWorkSheet.Cells[6, 14];
                rg.Cells.ColumnWidth = 20;
                rg.HorizontalAlignment = HorizontalAlignment.Right;


                #endregion

                DialogResult result = saveFileDialog1.ShowDialog();

                if (result.Equals(DialogResult.OK))
                {

                    xlWorkBook.SaveAs(saveFileDialog1.FileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();

                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlApp);

                    MessageBox.Show("Excel file created , you can find the file " + saveFileDialog1.FileName);//c:\\csharp.net-informations.xls");
                }
            }
            catch
            { }
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion

        #region method

        private void clearControl()
        {
            dataGridView1.DataSource = null;
        }
        string ConvertIntoString(decimal Balance)
        {
            string str = "";

            string value = Balance.ToString();
            int startindex = value.IndexOf(".");
            int last = (value.Length) - startindex;
            decimal form = Convert.ToDecimal(value.Substring(startindex, last));
            form = form * 500;
            long Ream = (long)Balance;

            str = @"" + Ream + " Ream " + Convert.ToInt64(form) + " Sheet";
            return str;

        }

        #endregion

        


       
        
    }

        
}
