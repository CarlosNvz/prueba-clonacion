using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace NuevoDocumentoDeTexto
{
    public partial class Form1 : Form
    {
        clsArchivo objn = new clsArchivo();
        public Form1()
        {
            InitializeComponent();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {

                switch (MessageBox.Show("¿Descartar cambios?", "Advertencia", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        richTextBox1.Clear();
                        break;
                    case DialogResult.No:
                        guardar();

                        break;
                    case DialogResult.Cancel:
                        break;
                    default:

                        break;
                }


            }
            else
            {
                richTextBox1.Clear();

            }
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guardar();

        }
        public void guardar()
        {
            Stream myStream;
            SaveFileDialog save = new SaveFileDialog();

            save.Filter = "txt files (*.txt)|*.txt";
            save.FilterIndex = 2;
            save.RestoreDirectory = true;

            if (save.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SaveFile(save.FileName, RichTextBoxStreamType.PlainText);

            }

        }
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                OpenFileDialog open = new OpenFileDialog();

                open.ShowDialog();
                if (open.FileName!="")
                {
                    richTextBox1.LoadFile(open.FileName, RichTextBoxStreamType.PlainText);

                }
                else
                {
                    
                }

            }
            else
            {
                switch (MessageBox.Show("¿Descartar cambios?", "Advertencia", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:

                        OpenFileDialog open = new OpenFileDialog();

                        open.ShowDialog();
                        if (open.FileName!="")
                        {
                            richTextBox1.LoadFile(open.FileName, RichTextBoxStreamType.PlainText);

                        }
                        break;
                    case DialogResult.No:
                        guardar();
                        richTextBox1.Clear();
                        break;
                    case DialogResult.Cancel:
                        break;
                    default:
                        break;
                }
            }
        }

        private void COPIAR_Click(object sender, EventArgs e)
        {
            MessageBox.Show("");
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pegarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void cortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }



        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            // btnSalir.PerformClick();
            if (richTextBox1.Text != "")
            {

                switch (MessageBox.Show("¿Descartar cambios?", "Advertencia", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        Application.Exit();
                        break;
                    case DialogResult.No:

                        guardar();
                        e.Cancel = true;
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'tabla_de_simbolosDataSet.Tabla1' Puede moverla o quitarla según sea necesario.
            this.tabla1TableAdapter.Fill(this.tabla_de_simbolosDataSet.Tabla1);
            // ClsExcel objn = new ClsExcel();
            //objn.Excel(dataGridView1);
            objn.AddToken(@"\s+", "Espacio", true);
            objn.AddToken(@"\b[_a-zA-Z][\w]*\b", "Identificador");
            objn.AddToken("\".*?\"", "Cadena");
            objn.AddToken(@"'\\.'|'[^\\]'", "CARACTER");
            objn.AddToken("//[^\r\n]*", "Comentario1");
            objn.AddToken("/[*].*?[*]/", "Comentario2");
            objn.AddToken(@"\d*\.?\d+", "Numero");
            objn.AddToken(@"[\(\)\{\}\[\];,]", "Delimitador");
            objn.AddToken(@"[\.=\+\-/*%]", "Operador");
            objn.AddToken(@">|<|==|>=|<=|!", "Comparador");
            objn.AddToken(@"[_i][_f]", "Condicional");



        }

        private void richTextBox1_TextChanged(object sender, EventArgs e) 
        {
            dataGridView2.Rows.Clear();

            objn.Debug(richTextBox1.Text,dataGridView2);

        }
    }
}
