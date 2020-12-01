using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SBD.Models
{
    public partial class Osoba
    {
        public int Osobaid { get; set; }
        [MaxLength(20,ErrorMessage ="Za dlugie")]
        [Required(ErrorMessage = "Wymagane")]

        public string Imie { get; set; }
        [MaxLength(20, ErrorMessage = "Za dlugie")]
        [Required(ErrorMessage = "Wymagane")]

        public string Nazwisko { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Wymagane")]

        public DateTime? DataUrodzenia { get; set; }

        public string FormatDate
        {
            get
            {
                if(DataUrodzenia!=null)
                {
                    DataUrodzenia.Value.Date.ToShortDateString();
                }
                return "";
            }
        }

        public virtual Donator Donator { get; set; }
        public virtual Pacjent Pacjent { get; set; }
        public virtual Pielegniarka Pielegniarka { get; set; }
        public string Info => $"{Imie} {Nazwisko}";
    }
}
