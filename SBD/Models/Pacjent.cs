using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SBD.Models
{
    public partial class Pacjent
    {
        public Pacjent()
        {
            Transfuzja = new HashSet<Transfuzja>();
        }

        public int Pacjentid { get; set; }
        [MaxLength(3, ErrorMessage = "za dlugie")]
        [Required(ErrorMessage = "Wymagane")]

        public string GrupaKrwi { get; set; }
        [MaxLength(6, ErrorMessage = "za dlugie")]
        [Required(ErrorMessage = "Wymagane")]


        public string Priorytet { get; set; }

        [RegularExpression(@"^(?:\d{0,3}\,\d{1,2})$|^\d{0,3}$", ErrorMessage = "Format XXX,XX")]
        [Required(ErrorMessage = "Wymagane")]

        public decimal? Waga { get; set; }
        [Required(ErrorMessage = "Wymagane")]

        public int? Osobaid { get; set; }

        public virtual Osoba Osoba { get; set; }
        public virtual ICollection<Transfuzja> Transfuzja { get; set; }

        public string Info
        {
            get
            {
                if (Osoba != null)
                    return $"{Osoba.Imie} {Osoba.Nazwisko}";
                return "";
            }
        }

    }
}
