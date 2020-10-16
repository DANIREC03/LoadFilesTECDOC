using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoadArticles200
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private  void button1_Click(object sender, EventArgs e)
        {

            List<modLineaFichero> oListDlNR = new List<modLineaFichero>();
            oListDlNR = leerFicheroIni();

            foreach (modLineaFichero item in oListDlNR)
            {
                readFile209(item.Proveedor.Trim());
            }

            txtResultado.Text = "Finalizado";
        }

        public static List<string> ReadFile(string Filename)
        {
            List<string> Sl = new List<string>();
            int counter = 0;
            string line;

            System.IO.StreamReader file =      new System.IO.StreamReader(Filename);
            while ((line = file.ReadLine()) != null)
            {

                Sl.Add(line);
                //System.Console.WriteLine(line);
                counter++;
            }

            file.Close();


            return Sl;
        }


        public static List<modLineaFichero>  leerFicheroIni() 
       
        {

            string strInfo = "";




        string lcOptionIntegracion  = "";
            string strCarpetaProveedor=  "";

            string strNombreFile  = "";
            string strPathFiles   = "";

        string strPathExe  = Path.GetDirectoryName(Application.ExecutablePath);
           // Rutinas.logs("strPathExe:" + strPathExe)

        string  strFileIni   = strPathExe + @"\" + "Integrador.ini";


        //' --- Fichero de Variables ----------------------
        //' PathFiles=G:\Tecdoc\D_TAF24
        //' Proveedor = 316
        //' OptionIntegracion = 400

        List<modLineaFichero> oList_LineasFichero = new List<modLineaFichero>();


        
        int intPositionEqual = 0;
            
            string Line = "";

            List<string> List_Lineas = ReadFile(strFileIni);


            foreach (string item in List_Lineas)
            {
                if (item.Contains("CABECERA"))
                { 
                
                }
                else if (item.Contains("ConctionStringBBDD"))
                {

                }
                else if (item.Contains("Version"))
                {

                }
                else if (item.Contains("LINEA") && item.Contains("Proveedor") && item.Contains("OptionIntegracion")  )
                {

                    intPositionEqual = item.IndexOf("r=") + 1;
                    strCarpetaProveedor = item.Substring(intPositionEqual + 1, 4).Trim();

                    intPositionEqual = item.IndexOf("n=") + 1;
                    lcOptionIntegracion = item.Substring(intPositionEqual + 1, item.Trim().Length - intPositionEqual - 1).Trim();
                    modLineaFichero omodLineaFichero = new modLineaFichero();

                    omodLineaFichero.Proveedor = strCarpetaProveedor;
                    omodLineaFichero.TipoFichero = lcOptionIntegracion;
                    oList_LineasFichero.Add(omodLineaFichero);

                
                }

            }
            return oList_LineasFichero;



        }


        public  int readFile200(string tcDlNr) 
        {

            string line = "";
            int contador = 0;
            try
            {
               
                System.IO.StreamReader file = new System.IO.StreamReader(@"C:\TECDOC\2040\D_TAF24\"+ tcDlNr + @"\200." + tcDlNr);
                while ((line = file.ReadLine()) != null)
                {

                  
                    mod200 omod = new mod200();

                    omod.ArtNr = line.Substring(0, 22).Trim();
                    omod.DLNr = tcDlNr;
                    omod.SA = "200";
                    omod.BezNr = "";
                    omod.ArtNrLimipa = omod.ArtNr.Replace("/", "").Replace(" ", "").Replace(".", "").Replace("-","");

                    
                    insertDatos200(omod);
                    contador += 1;
                }
                file.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return contador;
            
        }


        public static int insertDatos200(mod200 omod)
        {
            int lnResult = -1;
            string lcSql = "";


        List<string> filtro = new List<string>();
            lcSql += "INSERT INTO P0000T200Article " ;
            lcSql += "        ( ArtNr, ArtNrLimpia ,DLNr , SA, BezNr, Version )";
            lcSql += " VALUES (@ArtNr ,@ArtNrLimpia , @DLNr     , @SA, @BezNr,'2039' ) ";

            try
            {
                AccesoDatosSQLCliente oAccesoDatosSQLCliente = new AccesoDatosSQLCliente("", lcSql);
                oAccesoDatosSQLCliente.AñadirParametro("ArtNr", omod.ArtNr.ToString());
                oAccesoDatosSQLCliente.AñadirParametro("ArtNrLimpia", omod.ArtNrLimipa.ToString());
                oAccesoDatosSQLCliente.AñadirParametro("DLNr", omod.DLNr.ToString());
                oAccesoDatosSQLCliente.AñadirParametro("SA", omod.SA.ToString());
                oAccesoDatosSQLCliente.AñadirParametro("BezNr", omod.BezNr.ToString());

                oAccesoDatosSQLCliente.ConsultaSQL = lcSql;
                lnResult = oAccesoDatosSQLCliente.Ejecucion_Insert_getId();

            }
            catch (Exception ex)
            {
                //dalRutinas.LogApp("ERROR", string.Concat("dalTipos.insertDatos: ", ex.Message.ToString(), "", lcSql));
            }
            finally
            { 
            
            
            }

            return lnResult;
        }

        /////////////////////////////////////////////////////////////////////////


        public int readFile209(string tcDlNr)
        {

            string line = "";
            int contador = 0;
            try
            {


                string lcFile209= @"C:\TECDOC\2039\D_TAF24\" + tcDlNr + @"\209." + tcDlNr;

                if (System.IO.File.Exists(lcFile209))
                {
                    System.IO.StreamReader file = new System.IO.StreamReader(lcFile209);
                    while ((line = file.ReadLine()) != null)
                    {


                        mod209 omod = new mod209();

                        omod.ArtNr = line.Substring(0, 22).Trim();
                        omod.DLNr = tcDlNr;
                        omod.SA = "209";
                        omod.LKZ = line.Substring(29, 3).Trim();
                        omod.ArtNrLimipa = omod.ArtNr.Replace("/", "").Replace(" ", "").Replace(".", "").Replace("-", "");
                        omod.GTIN = line.Substring(32, 13).Trim();
                        omod.Exclude = line.Substring(45, 1).Trim();
                        omod.LoschFlag = line.Substring(46, 1).Trim();


                        insertDatos209(omod);
                        contador += 1;
                    }
                    file.Close();



                }

             
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
            return contador;

        }


        public static int insertDatos209(mod209 omod)
        {
            int lnResult = -1;
            string lcSql = "";



            /*
             
             
            CREATE TABLE [dbo].[P0000T209ArticleGTIN](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ArtNr] [nvarchar](22) NULL,
	[DLNr] [nvarchar](4) NULL,
	[SA] [nvarchar](3) NULL,
	[LKZ] [nvarchar](3) NULL,
	[GTIN] [nvarchar](13) NULL,
	[Exclude] [nvarchar](1) NULL,
	[LoschFlag] [nvarchar](1) NULL,
	[ArtNrLimpia] [nvarchar](22) NULL,
	[Activo] [bit] NULL,
	[Created_date] [nvarchar](20) NULL,
	[Version] [nvarchar](4) NULL,


             */


            List<string> filtro = new List<string>();
            lcSql += "INSERT INTO P0000T209ArticleGTIN ";
            lcSql += "        ( ArtNr, ArtNrLimpia ,DLNr   , SA, LKZ, GTIN, Exclude, LoschFlag,  Version )";
            lcSql += " VALUES (@ArtNr ,@ArtNrLimpia , @DLNr, @SA, @LKZ,@GTIN ,@Exclude, @LoschFlag,   '2039' ) ";

            try
            {
                AccesoDatosSQLCliente oAccesoDatosSQLCliente = new AccesoDatosSQLCliente("", lcSql);
                oAccesoDatosSQLCliente.AñadirParametro("ArtNr", omod.ArtNr.ToString());
                oAccesoDatosSQLCliente.AñadirParametro("ArtNrLimpia", omod.ArtNrLimipa.ToString());
                oAccesoDatosSQLCliente.AñadirParametro("DLNr", omod.DLNr.ToString());
                oAccesoDatosSQLCliente.AñadirParametro("SA", omod.SA.ToString());
                oAccesoDatosSQLCliente.AñadirParametro("LKZ", omod.LKZ.ToString());
                oAccesoDatosSQLCliente.AñadirParametro("GTIN", omod.GTIN.ToString());
                oAccesoDatosSQLCliente.AñadirParametro("Exclude", omod.Exclude.ToString());
                oAccesoDatosSQLCliente.AñadirParametro("LoschFlag", omod.LoschFlag.ToString());

                oAccesoDatosSQLCliente.ConsultaSQL = lcSql;
                lnResult = oAccesoDatosSQLCliente.Ejecucion_Insert_getId();

            }
            catch (Exception ex)
            {
                //dalRutinas.LogApp("ERROR", string.Concat("dalTipos.insertDatos: ", ex.Message.ToString(), "", lcSql));
            }
            finally
            {


            }

            return lnResult;
        }








    }


    public class modLineaFichero
    {

        public string Proveedor { get; set; }
        public string TipoFichero { get; set; }

    }


}
