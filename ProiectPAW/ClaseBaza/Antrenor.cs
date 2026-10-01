using System;

namespace Seminar7
{
    public class Antrenor
    {
        private int _idAntrenor;
        private string _nume;
        private string _prenume;
        private decimal _salariu;
        public int IdAntrenor
        {
            get { return _idAntrenor; }
            set { _idAntrenor = value; }
        }

        public string Nume
        {
            get { return _nume; }
            set { _nume = value; }
        }

        public string Prenume
        {
            get { return _prenume; }
            set { _prenume = value; }
        }

        public decimal Salariu
        {
            get { return _salariu; }
            set { _salariu = value; }
        }

        public Antrenor()
        {
        }
        public Antrenor(int idAntrenor, string nume, string prenume, decimal salariu)
        {
            IdAntrenor = idAntrenor;
            Nume = nume;
            Prenume = prenume;
            Salariu = salariu;
        }
    }
}