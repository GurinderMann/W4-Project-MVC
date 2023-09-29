using Polizia.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Polizia.Controllers
{
    public class InformazioniController : Controller
    {
        // GET: Informazioni
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TotVerbali()
        {
            List<ListaTot> Totale = new List<ListaTot>();

            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaDB"].ConnectionString.ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT V.IdAnagrafica, A.Nome, COUNT(*) AS NumeroVerbali FROM VERBALE V INNER JOIN ANAGRAFICA A ON V.IdAnagrafica = A.IdAnagrafica GROUP BY V.IdAnagrafica, A.Nome", conn);
                    SqlDataReader sqlData = cmd.ExecuteReader();
                    while (sqlData.Read())
                    {
                        int idAnagrafica = Convert.ToInt32(sqlData["IdAnagrafica"]);
                        string nomeTrasgressore = sqlData["Nome"].ToString();
                        int numeroVerbali = Convert.ToInt32(sqlData["NumeroVerbali"]);

                        // Creare un oggetto ListaTot e aggiungerlo alla lista Totale
                        ListaTot tot = new ListaTot
                        {
                            Id = idAnagrafica,
                            Nome = nomeTrasgressore,
                            Tot = numeroVerbali
                        };
                        Totale.Add(tot);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }

            return View(Totale);
        }
        public ActionResult TotPunti()
        {
            List<ListaTot> PuntiTotali = new List<ListaTot>();

            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaDB"].ConnectionString.ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT V.IdAnagrafica, A.Nome, SUM (V.DecretamentoPunti) AS PuntiTOT FROM VERBALE V INNER JOIN ANAGRAFICA A ON V.IdAnagrafica = A.IdAnagrafica GROUP BY V.IdAnagrafica, A.Nome", conn);

                    SqlDataReader sqlData = cmd.ExecuteReader();
                    while (sqlData.Read())
                    {
                        int idAnagrafica = Convert.ToInt32(sqlData["IdAnagrafica"]);
                        string nomeTrasgressore = sqlData["Nome"].ToString();
                        int decretamento = Convert.ToInt32(sqlData["PuntiTOT"].ToString());


                        ListaTot punti = new ListaTot
                        {
                            Id = idAnagrafica,
                            Nome = nomeTrasgressore,
                            PuntiTotali = decretamento
                        };
                        PuntiTotali.Add(punti);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }

            return View(PuntiTotali);
        }

        public ActionResult Punti()
        {
            List<ListaTot> Violazioni = new List<ListaTot>();

            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaDB"].ConnectionString.ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT V.Importo, A.Cognome, A.Nome, V.DataViolazione, V.DecretamentoPunti " +
                                                     "FROM VERBALE V " +
                                                     "INNER JOIN ANAGRAFICA A ON V.IdAnagrafica = A.IdAnagrafica " +
                                                     "WHERE V.DecretamentoPunti >= 10", conn);

                    SqlDataReader sqlData = cmd.ExecuteReader();
                    while (sqlData.Read())
                    {
                        decimal importo = Convert.ToDecimal(sqlData["Importo"]);
                        string cognome = sqlData["Cognome"].ToString();
                        string nome = sqlData["Nome"].ToString();
                        DateTime dataViolazione = Convert.ToDateTime(sqlData["DataViolazione"]);
                        int decretamento = Convert.ToInt32(sqlData["DecretamentoPunti"]);

                        ListaTot violazione = new ListaTot
                        {
                            Importo = importo,
                            Cognome = cognome,
                            Nome = nome,
                            DataViolazione = dataViolazione,
                            DecurtamentoPunti = decretamento
                        };
                        Violazioni.Add(violazione);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }

            return View(Violazioni);
        }

        public ActionResult ImportoTot()
        {
            List<ListaTot> Importo = new List<ListaTot>();

            string connectionString = ConfigurationManager.ConnectionStrings["PoliziaDB"].ConnectionString.ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT V.Importo, A.Cognome, A.Nome, V.DataViolazione, V.DecretamentoPunti " +
                                                     "FROM VERBALE V " +
                                                     "INNER JOIN ANAGRAFICA A ON V.IdAnagrafica = A.IdAnagrafica " +
                                                     "WHERE V.Importo > 400", conn);

                    SqlDataReader sqlData = cmd.ExecuteReader();
                    while (sqlData.Read())
                    {
                        decimal importo = Convert.ToDecimal(sqlData["Importo"]);
                        string cognome = sqlData["Cognome"].ToString();
                        string nome = sqlData["Nome"].ToString();
                        DateTime dataViolazione = Convert.ToDateTime(sqlData["DataViolazione"]);
                        int decretamento = Convert.ToInt32(sqlData["DecretamentoPunti"]);

                        ListaTot violazione = new ListaTot
                        {
                            Importo = importo,
                            Cognome = cognome,
                            Nome = nome,
                            DataViolazione = dataViolazione,
                            DecurtamentoPunti = decretamento
                        };
                        Importo.Add(violazione);
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
            }

            return View(Importo);
        }


    }
}
