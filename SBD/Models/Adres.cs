using System;
using System.Collections.Generic;

namespace SBD.Models
{
    public partial class Adres
    {
        public int Adresid { get; set; }
        public string Miasto { get; set; }
        public string Ulica { get; set; }
        public decimal? Nrbudynku { get; set; }
        public string Kodpocztowy { get; set; }

        public virtual Bankkrwi Bankkrwi { get; set; }

        public string Info
        {
            get { return $"{Miasto} {Kodpocztowy} {Ulica} {Nrbudynku} "; }
        }
    }
}
