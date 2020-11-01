using System;
using System.Collections.Generic;

namespace SBD.Models
{
    public partial class Osoba
    {
        public int Osobaid { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public DateTime? DataUrodzenia { get; set; }

        public virtual Donator Donator { get; set; }
        public virtual Pacjent Pacjent { get; set; }
        public virtual Pielegniarka Pielegniarka { get; set; }
    }
}
