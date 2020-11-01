using System;
using System.Collections.Generic;

namespace SBD.Models
{
    public partial class TypDonacji
    {
        public TypDonacji()
        {
            Donacja = new HashSet<Donacja>();
        }

        public int Typid { get; set; }
        public string Typ { get; set; }
        public decimal? Czestotliwosc { get; set; }

        public virtual ICollection<Donacja> Donacja { get; set; }
    }
}
