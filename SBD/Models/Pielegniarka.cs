using System;
using System.Collections.Generic;
using System.Security.Policy;

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

        public string Info => $"{Osoba.Imie} {Osoba.Nazwisko}";
    }
}
