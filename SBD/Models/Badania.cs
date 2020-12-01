using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SBD.Models
{
    public partial class Badania
    {
        public int Badaniaid { get; set; }
        [RegularExpression(@"^(?:\d{0,3}\,\d{1,2})$|^\d{0,3}$",ErrorMessage ="Format XXX,XX")]
        [Required(ErrorMessage = "Wymagane")]

        public decimal? Hemoglobina { get; set; }
        [RegularExpression(@"^(?:\d{0,3}\,\d{1,2})$|^\d{0,3}$", ErrorMessage = "Format XXX,XX")]
        [Required(ErrorMessage = "Wymagane")]

        public decimal? Temperatura { get; set; }
        [MaxLength(8,ErrorMessage ="za dlugie")]
        [Required(ErrorMessage = "Wymagane")]

        public string Cisnienie { get; set; }
        [RegularExpression(@"^\d{0,5}$", ErrorMessage ="Format XXXXX")]
        [Required(ErrorMessage = "Wymagane")]

        public decimal? Tetno { get; set; }
        public int Kartaid { get; set; }

        public virtual Kartazdrowia Karta { get; set; }
        public virtual Donacja Donacja { get; set; }
        public virtual Transfuzja Transfuzja { get; set; }


    }
}
