using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Polizia.Models
{
    public class ListaTot
    {
        public string Nome { get; set; }
        public int Id { get; set; }
        public int Tot { get; set; }
        public decimal Importo { get; set; }
        public string Cognome { get; set; }
        public DateTime DataViolazione { get; set; }
        public int DecurtamentoPunti { get; set; }
       
        public int PuntiTotali { get; set; }
    }
}