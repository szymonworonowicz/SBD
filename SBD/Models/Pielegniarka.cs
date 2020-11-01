using System;
using System.Collections.Generic;

namespace SBD.Models
{
    public partial class Pielegniarka
    {
        public Pielegniarka()
        {
            Donacja = new HashSet<Donacja>();
            Transfuzja = new HashSet<Transfuzja>();
        }

        public int Pielegniarkaid { get; set; }
        public decimal? Doswiadczenie { get; set; }
        public int Osobaid { get; set; }

        public virtual Osoba Osoba { get; set; }
        public virtual ICollection<Donacja> Donacja { get; set; }
        public virtual ICollection<Transfuzja> Transfuzja { get; set; }
    }
}
