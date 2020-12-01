using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [RegularExpression(@"^\d{0,5}$", ErrorMessage = "Format XXXXX")]
        [Required(ErrorMessage = "Wymagane")]

        public decimal? Doswiadczenie { get; set; }
        [Required(ErrorMessage = "Wymagane")]

        public int? Osobaid { get; set; }

        public virtual Osoba Osoba { get; set; }
        public virtual ICollection<Donacja> Donacja { get; set; }
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
