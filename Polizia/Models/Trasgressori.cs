using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Polizia.Models
{
    public class Trasgressori
    {
        public int IdAnagrafica { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Indirizzo { get; set; }
        public string Città { get; set; }
        public int Cap { get; set; }
        public string CodiceFiscale { get; set; }

    }
}