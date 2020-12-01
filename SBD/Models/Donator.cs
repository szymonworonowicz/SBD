using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SBD.Models
{
    public partial class Donator
    {
        public Donator()
        {
            Donacja = new HashSet<Donacja>();
        }

        public int Donatorid { get; set; }
        [MaxLength(3,ErrorMessage ="Za dlugie")]
        [Required(ErrorMessage = "Wymagane")]

        public string GrupaKrwi { get; set; }
        [MaxLength(1, ErrorMessage = "Za dlugie")]
        [Required(ErrorMessage = "Wymagane")]

        public string Rh { get; set; }
        [RegularExpression(@"^\d{0,5}$", ErrorMessage = "Format XXXXX")]
        [Required(ErrorMessage = "Wymagane")]

        public decimal? Waga { get; set; }
        [RegularExpression(@"^\d{0,5}$", ErrorMessage = "Format XXXXX")]
        [Required(ErrorMessage = "Wymagane")]

        public decimal? Wzrost { get; set; }

        [Required(ErrorMessage = "Wymagane")]
        [DataType(DataType.Date)]
        public DateTime? Nastepnadonacja { get; set; }
        [Required(ErrorMessage = "Wymagane")]

        public int? Osobaid { get; set; }

        public virtual Osoba Osoba { get; set; }
        public virtual ICollection<Donacja> Donacja { get; set; }

        public string Info
        {
            get
            {
                if(Osoba!=null)
                    return $"{Osoba.Imie} {Osoba.Nazwisko}";
                return "";
            }
        }
    }
}
