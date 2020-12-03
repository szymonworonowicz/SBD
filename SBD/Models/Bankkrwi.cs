using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SBD.Models
{
    public partial class Bankkrwi
    {
        public Bankkrwi()
        {
            Worek = new HashSet<Worek>();
        }

        public int? Bankid { get; set; }
        [Required(ErrorMessage = "Wymagane")]

        public int? Adresid { get; set; }
        [Required(ErrorMessage = "Wymagane")]

        [MaxLength(20, ErrorMessage = "Za dlugie")]
        public string Typkrwi { get; set; }

        public virtual Adres Adres { get; set; }
        public virtual ICollection<Worek> Worek { get; set; }

        public string Info
        {
            get
            {
                if (Adres != null && Adresid != null)
                    return $"{Adres.Miasto} {Adres.Ulica} {Adres.Nrbudynku}";
                return "";
            }
        }
    }
}
