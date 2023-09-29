using Polizia.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Polizia.Controllers
{

    public class HomeController : Controller
    {
        public  List<Verbali> Verbalilist = new List<Verbali>();
        public List <Trasgressori> trasgressoriList = new List<Trasgressori>();
        
       
        public ActionResult Index()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try 
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM ANAGRAFICA", conn);
                SqlDataReader sqlData;
                sqlData = cmd.ExecuteReader();
                while (sqlData.Read()) 
                {
                   int id = Convert.ToInt32(sqlData["IdAnagrafica"].ToString());
                   string nome = sqlData["Nome"].ToString();
                   string Cognome = sqlData["Cognome"].ToString();
                   string Codicefiscale = sqlData["CodiceFisc"].ToString();
                    Trasgressori trasgressori = new Trasgressori
                    {
                        IdAnagrafica = id,
                        Nome = nome,
                        Cognome = Cognome,
                        CodiceFiscale = Codicefiscale
                    };
                    trasgressoriList.Add(trasgressori);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }

            finally { conn.Close(); }


            return View(trasgressoriList);
        }

        public ActionResult Anagrafe()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Anagrafe (Trasgressori t)
        {
            if (ModelState.IsValid) 
            {
              
                string connectionString = ConfigurationManager.ConnectionStrings["PoliziaDB"].ConnectionString.ToString();
                SqlConnection conn = new SqlConnection(connectionString);
                try 
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO ANAGRAFICA (Nome, Cognome, Indirizzo, Citta, Cap, CodiceFisc) VALUES (@Nome, @Cognome, @Indirizzo, @Citta, @Cap, @CodiceFisc)", conn);


                    cmd.Parameters.AddWithValue("@Nome", t.Nome);
                    cmd.Parameters.AddWithValue("@Cognome", t.Cognome);
                    cmd.Parameters.AddWithValue("@Indirizzo", t.Indirizzo);
                    cmd.Parameters.AddWithValue("@Citta", t.Città);
                    cmd.Parameters.AddWithValue("@Cap", t.Cap);
                    cmd.Parameters.AddWithValue("@CodiceFisc", t.CodiceFiscale);



                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        ViewBag.MessaggioConferma = "Prodotto creato con successo";
                    }
                    else
                    {
                        ViewBag.MessaggioConferma = "Nessun prodotto creato";
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                    
                }
                finally
                {
                    conn.Close();
                }

            }

            return RedirectToAction("Index");
        }

        public ActionResult Verbale()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verbale(Verbali v, int idAnagrafica )
        {
            if (ModelState.IsValid)
            {
               
                string connectionString = ConfigurationManager.ConnectionStrings["PoliziaDB"].ConnectionString.ToString();
                SqlConnection conn = new SqlConnection(connectionString);
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO VERBALE (DataViolazione, Indirizzo, NominativoAgente, DataTrascrizione, Importo, DecretamentoPunti, IdViolazione, IdAnagrafica) VALUES (@DataViolazione, @Indirizzo, @NominativoAgente, @DataTrascrizione, @Importo, @DecretamentoPunti,@IdViolazione, @IdAnagrafica)", conn);


                    cmd.Parameters.AddWithValue("@DataViolazione", v.DataViolazione);
                    cmd.Parameters.AddWithValue("@Indirizzo", v.Indirizzo);
                    cmd.Parameters.AddWithValue("@NominativoAgente", v.NominativoAgente);
                    cmd.Parameters.AddWithValue("@DataTrascrizione", v.DataTrascrizione);
                    cmd.Parameters.AddWithValue("@Importo", v.Importo);
                    cmd.Parameters.AddWithValue("@DecretamentoPunti", v.DecretamentoPunti);
                    cmd.Parameters.AddWithValue("@IdViolazione", v.IdViolazione);
                    cmd.Parameters.AddWithValue("@IdAnagrafica", idAnagrafica);



                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        ViewBag.MessaggioConferma = "Prodotto creato con successo";
                    }
                    else
                    {
                        ViewBag.MessaggioConferma = "Nessun prodotto creato";
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());

                }
                finally
                {
                    conn.Close();
                }

            }
            return RedirectToAction("Index");
        }

        public ActionResult ListaVerbali()
        {
            
            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
             
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM VERBALE", conn);
                SqlDataReader sqlData;
                sqlData = cmd.ExecuteReader();
                while (sqlData.Read())
                {
                    DateTime dataViolazione = Convert.ToDateTime(sqlData["DataViolazione"].ToString());
                    string indirizzo = sqlData["Indirizzo"].ToString();
                    DateTime dataTrascrizione = Convert.ToDateTime(sqlData["DataTrascrizione"].ToString());
                    decimal importo = Convert.ToDecimal(sqlData["Importo"].ToString());
                    int decretamento = Convert.ToInt32(sqlData["DecretamentoPunti"].ToString());

                    Verbali verbali = new Verbali
                    {
                        DataViolazione = dataViolazione,
                        Indirizzo = indirizzo,
                        DataTrascrizione = dataTrascrizione,
                        Importo = importo,
                        DecretamentoPunti = decretamento
                    };
                    Verbalilist.Add(verbali);
                }
              
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
           
            return View(Verbalilist); 
        }

    

    }
}
        