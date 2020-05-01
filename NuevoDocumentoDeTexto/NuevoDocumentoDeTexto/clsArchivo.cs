using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace NuevoDocumentoDeTexto
{
    class clsArchivo
    {


        private Regex regex;
        private string patroncompleto;
        private List<string> listaTokens = new List<string>();

        private List<string> NombreTipo;
        private List<string> Caracter;
        private List<int> Inicio;
        private List<int> Linea;
        private List<int> Columna;





        public void AddToken(string patron, string nomToken, bool inicio = false)
        {
            if (inicio)
            {
                patroncompleto += string.Format("(?<{0}>{1})", nomToken, patron);
            }
            else
            {
                patroncompleto += string.Format("|(?<{0}>{1})", nomToken, patron);
            }
            listaTokens.Add(nomToken);
        }

        public string Debug(string cadena, DataGridView dgv1)
        {

            regex = new Regex(patroncompleto);
            Match match = regex.Match(cadena);
            DataTable dt = new DataTable();
            //    MatchCollection matt= Regex.Match(cadena, patroncompleto);
            string cad = "";

            NombreTipo = new List<string>(); ;
            Caracter = new List<string>();
            Inicio = new List<int>();
            Linea = new List<int>();
            Columna = new List<int>();
            NombreTipo.Clear();
            Caracter.Clear();
            Inicio.Clear();
            Linea.Clear();
            Columna.Clear();
            while (match.Success)
            {
                for (int i = 1; i < match.Groups.Count; i++)
                {
                     if (match.Index > (match.Index + match.Length))
                    {
                        string error1 = match.Groups[i].Value.ToString();
                        string error2 = error1.Substring((match.Index + match.Length), (match.Index - (match.Index + match.Length)));
                        NombreTipo.Add("Error");
                        Caracter.Add(error2);
                        Inicio.Add(match.Groups[i].Index);
                        Linea.Add(0);
                        Columna.Add(0);
                    }


                    if (match.Groups[i].Success)
                    {
                        NombreTipo.Add(regex.GroupNameFromNumber(i));
                        Caracter.Add(match.Groups[i].Value);
                        Inicio.Add(match.Groups[i].Index);
                        Linea.Add(0);
                        Columna.Add(0);

                    }
                  
                   

                }
                match = match.NextMatch();
            }
            Token(dgv1);
            return cad;
        }
        DataTable datos;
        public void Token(DataGridView dgv)
        {
            datos = new DataTable();
            for (int i = 0; i < NombreTipo.Count; i++)
            {
                
                dgv.Rows.Add(NombreTipo[i],Caracter[i],Inicio[i],Linea[i].ToString(),Columna[i].ToString());

            }
          //  dgv.DataSource=datos;
        }
    }
}
