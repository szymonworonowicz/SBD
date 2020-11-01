using System;
using System.Collections.Generic;

namespace SBD.Models
{
    public partial class Worek
    {
        public int Worekid { get; set; }
        public int Bankid { get; set; }
        public int Transfuzjaid { get; set; }
        public int Donacjaid { get; set; }
        public decimal? Wielkosc { get; set; }
        public string Grupakrwi { get; set; }
        public string Rh { get; set; }

        public virtual Bankkrwi Bank { get; set; }
        public virtual Donacja Donacja { get; set; }
        public virtual Transfuzja Transfuzja { get; set; }
    }
}
