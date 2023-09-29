using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Polizia.Models
{
    public class Verbali
    {
        public DateTime DataViolazione { get; set; }
        public string Indirizzo { get; set; }
        public string NominativoAgente { get; set; }
        public DateTime DataTrascrizione { get; set; }
        public decimal Importo { get; set; }
        public int DecretamentoPunti { get; set; }
        public int IdViolazione { get; set; }
        public int IdAnagrafica { get; set; }

        public static List <Verbali> listaVerbli { get; set; } = new List <Verbali> ();
    

    }
}