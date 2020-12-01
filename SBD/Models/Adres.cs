using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SBD.Models
{
    public partial class Adres
    {
        public int Adresid { get; set; }
        [MaxLength(20, ErrorMessage = "Za dlugie")]
        [Required(ErrorMessage = "Wymagane")]

        public string Miasto { get; set; }
        [MaxLength(20, ErrorMessage = "Za dlugie")]
        [Required(ErrorMessage = "Wymagane")]

        public string Ulica { get; set; }

        [Required(ErrorMessage = "Wymagane")]

        public decimal? Nrbudynku { get; set; }

        [MaxLength(10, ErrorMessage = "Za dlugie")]
        [RegularExpression(@"^[0-9]{5}(?:-[0-9]{4})?$")]
        [Required(ErrorMessage = "Wymagane")]

        public string Kodpocztowy { get; set; }

        public virtual Bankkrwi Bankkrwi { get; set; }

        public string Info
        {
            get { return $"{Miasto} {Kodpocztowy} {Ulica} {Nrbudynku} "; }
        }
    }
}
