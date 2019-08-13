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
    class Common
    {
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


        public void ExcelExport(DataGridView dataGridView1, SaveFileDialog saveFileDialog1)
        {
            try
            {
                Excel._Application xlApp;
                Excel._Workbook xlWorkBook;
                Excel._Worksheet xlWorkSheet;
                object misValue = System.Reflection.Missing.Value;

                xlApp = new Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);
                xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                int i = 0;
                int j = 0;


                for (j = 1; j <= dataGridView1.ColumnCount - 1; j++)
                {
                    xlWorkSheet.Cells[1, j] = dataGridView1.Columns[j].HeaderText;
                }


                for (i = 0; i <= dataGridView1.RowCount - 1; i++)
                {
                    for (j = 1; j <= dataGridView1.ColumnCount - 1; j++)
                    {
                        DataGridViewCell cell = dataGridView1[j, i];
                        xlWorkSheet.Cells[i + 2, j] = cell.Value;
                    }
                }
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

        public void Clearcontrol(Form form1)
        {
             form1.Controls.OfType<TextBox>().ToList().ForEach(row => row.Text = ""); 
        }

        public void SetStyle(Panel Panel1)
        {
            Panel1.BackColor = System.Drawing.Color.SteelBlue;

            foreach (Control ctr in Panel1.Controls)
            {
                if(ctr.GetType().Equals(typeof(Button)))
                {
                    if (ctr.AccessibleName == "SetStyle1")
                    {
                        ctr.BackColor = System.Drawing.Color.DarkOliveGreen;
                        ctr.ForeColor = System.Drawing.Color.White;
                        ctr.Margin = new System.Windows.Forms.Padding(2);
                        ctr.Size = new System.Drawing.Size(80, 28);
                    }
                    if (ctr.AccessibleName == "SetStyle2")
                    {
                        ctr.BackColor = System.Drawing.Color.DarkSlateBlue;
                        ctr.ForeColor = System.Drawing.Color.White;
                        ctr.Margin = new System.Windows.Forms.Padding(2);
                        ctr.Size = new System.Drawing.Size(80, 28);
                    }
                }
               
                if (ctr.GetType().Equals(typeof(Label)))
                {
                    if (ctr.AccessibleName == "SetStyle1")
                    {
                        ctr.ForeColor = System.Drawing.Color.Gold;
                    }                   
                    if (ctr.AccessibleName == "SetStyle2")
                    {
                        ctr.ForeColor = System.Drawing.Color.PaleGoldenrod;
                    }
                    if (ctr.AccessibleName == "SetStyle3")
                    {
                        ctr.ForeColor = System.Drawing.Color.Turquoise;                       
                    }
                    if (ctr.AccessibleName != "SetStyle2" && ctr.AccessibleName != "SetStyle1" && ctr.AccessibleName != "SetStyle3")
                    {
                        ctr.ForeColor = System.Drawing.Color.White;
                    }
                   
                }

            }

        }

    }
}
