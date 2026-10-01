using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPAW.ClaseBaza
{
    public class Abonament
    {
        public string TipAbonament { get; set; }
        public decimal Pret { get; set; }
        public string Facilitati { get; set; }

        public Abonament() { }

        public Abonament(string tipAbonament, decimal pret, string facilitati)
        {
            TipAbonament = tipAbonament;
            Pret = pret;
            Facilitati = facilitati;
        }
    }
}
