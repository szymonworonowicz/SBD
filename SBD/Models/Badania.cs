using System;
using System.Collections.Generic;

namespace SBD.Models
{
    public partial class Badania
    {
        public int Badaniaid { get; set; }
        public decimal? Hemoglobina { get; set; }
        public decimal? Temperatura { get; set; }
        public string Cisnienie { get; set; }
        public decimal? Tetno { get; set; }
        public int Kartaid { get; set; }

        public virtual Kartazdrowia Karta { get; set; }
        public virtual Donacja Donacja { get; set; }
        public virtual Transfuzja Transfuzja { get; set; }


    }
}
