using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPAW
{
    public enum TipAbonament
    {
        Bronze,
        Silver,
        Gold,
        PRO
    }

    public class Client
    {
        
        private int _idClient;
        private string _numeClient;
        private string _prenumeClient;
        private TipAbonament _tipAbonament;
        private DateTime _dataInscriere;
        private int? _idAntrenor; 

        public int IdClient
        {
            get { return _idClient; }
            set { _idClient = value; }
        }

        public string NumeClient
        {
            get { return _numeClient; }
            set { _numeClient = value; }
        }

        public string PrenumeClient
        {
            get { return _prenumeClient; }
            set { _prenumeClient = value; }
        }

        public TipAbonament TipAbonament
        {
            get { return _tipAbonament; }
            set { _tipAbonament = value; }
        }

        public DateTime DataInscriere
        {
            get { return _dataInscriere; }
            set { _dataInscriere = value; }
        }

        public int? idAntrenor
        {
            get { return _idAntrenor; }
            set { _idAntrenor = value; }
        }

        public Client()
        {
        }

        public Client(int idClient, string numeClient, string prenumeClient, TipAbonament tipAbonament, DateTime dataInscriere, int? idAntrenor)
        {
            this.IdClient = idClient;
            this.NumeClient = numeClient;
            this.PrenumeClient = prenumeClient;
            this.TipAbonament = tipAbonament;
            this.DataInscriere = dataInscriere;
            this.idAntrenor = idAntrenor; 
        }
    }
}
