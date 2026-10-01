using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPAW.ClaseBaza
{
    public class Programare
    {
        private int _idProgramare;
        private int _durata; 
        private int _idClient;
        private int _idAngajat;

        public int IdRezervare
        {
            get { return _idProgramare; }
            set { _idProgramare = value; }
        }

        public int Durata
        {
            get { return _durata; }
            set { _durata = value; }
        }

        public int IdClient
        {
            get { return _idClient; }
            set { _idClient = value; }
        }

        public int IdAngajat
        {
            get { return _idAngajat; }
            set { _idAngajat = value; }
        }
        public Programare()
        {
        }
        public Programare(int idRezervare, int durata, int idClient, int idAngajat)
        {
            IdRezervare = idRezervare;
            Durata = durata;
            IdClient = idClient;
            IdAngajat = idAngajat;
        }
    }
}
