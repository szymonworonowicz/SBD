using System;
using System.Collections.Generic;

namespace SBD.Models
{
    public partial class Donator
    {
        public Donator()
        {
            Donacja = new HashSet<Donacja>();
        }

        public int Donatorid { get; set; }
        public string GrupaKrwi { get; set; }
        public string Rh { get; set; }
        public decimal? Waga { get; set; }
        public decimal? Wzrost { get; set; }
        public DateTime? Nastepnadonacja { get; set; }
        public int Osobaid { get; set; }

        public virtual Osoba Osoba { get; set; }
        public virtual ICollection<Donacja> Donacja { get; set; }

        public string Info => $"{Osoba.Imie} {Osoba.Nazwisko}";
    }
}
