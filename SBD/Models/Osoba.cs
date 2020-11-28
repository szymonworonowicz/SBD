using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SBD.Models
{
    public partial class Osoba
    {
        public int Osobaid { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataUrodzenia { get; set; }

        public string FormatDate => DataUrodzenia.Value.Date.ToShortDateString();

        public virtual Donator Donator { get; set; }
        public virtual Pacjent Pacjent { get; set; }
        public virtual Pielegniarka Pielegniarka { get; set; }
        public string Info => $"{Imie} {Nazwisko}";
    }
}
