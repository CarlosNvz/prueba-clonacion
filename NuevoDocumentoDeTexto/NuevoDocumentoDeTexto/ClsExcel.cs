using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace NuevoDocumentoDeTexto
{
    class ClsExcel
    {
        OleDbConnection conn;
        OleDbDataAdapter MyDataAdapter;
        DataTable dt;

        public void Excel(DataGridView dgv)
        {
            string ruta = "C:\\Users\\carlo\\source\\repos\\NuevoDocumentoDeTexto\\NuevoDocumentoDeTexto";
            string nombreHoja = "Libro1";
            try
            {

                conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;data source=" + ruta + ";Extended Properties='Excel 12.0 XML;HDR=Yes'");
                MyDataAdapter = new OleDbDataAdapter("Select * from [" + nombreHoja + "$]", conn);
                dt = new DataTable();
                MyDataAdapter.Fill(dt);
                dgv.DataSource = dt;

            }
            catch (Exception ex)
            {
                
            }
             
        }

        }
    }
