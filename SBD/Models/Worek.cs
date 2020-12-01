using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SBD.Models
{
    public partial class Worek
    {
        public int Worekid { get; set; }
        [Required(ErrorMessage = "Wymagane")]

        public int Bankid { get; set; }
        [Required(ErrorMessage = "Wymagane")]

        public int? Transfuzjaid { get; set; }
        [Required(ErrorMessage = "Wymagane")]

        public int? Donacjaid { get; set; }

        [RegularExpression(@"^(?:\d{0,3}\,\d{1,2})$|^\d{0,3}$", ErrorMessage = "Format XXX,XX")]
        [Required(ErrorMessage = "Wymagane")]

        public decimal? Wielkosc { get; set; }
        [MaxLength(2,ErrorMessage ="Za dlugie")]
        [Required(ErrorMessage = "Wymagane")]
        [RegularExpression("A|B|AB|0",ErrorMessage ="Tylko A,B,AB,0")]
        public string Grupakrwi { get; set; }

        [MaxLength(1,ErrorMessage ="za dlugie")]
        [Required(ErrorMessage = "Wymagane")]

        public string Rh { get; set; }

        public virtual Bankkrwi Bank { get; set; }
        public virtual Donacja Donacja { get; set; }
        public virtual Transfuzja Transfuzja { get; set; }
    }
}
