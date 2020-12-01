using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SBD.Models
{
    public partial class Kartazdrowia
    {
        public int Kartaid { get; set; }
        [RegularExpression(@"^T|N$",ErrorMessage ="T/N")]
        [Required(ErrorMessage = "Wymagane")]

        public string Syfilis { get; set; }
        [RegularExpression(@"^T|N$", ErrorMessage = "T/N")]
        [Required(ErrorMessage = "Wymagane")]

        public string Zapaleniewatrobyb { get; set; }
        [RegularExpression(@"^T|N$", ErrorMessage = "T/N")]
        [Required(ErrorMessage = "Wymagane")]

        public string Zapaleniewatrobyc { get; set; }
        [RegularExpression(@"^T|N$", ErrorMessage = "T/N")]
        [Required(ErrorMessage = "Wymagane")]

        public string Hiv { get; set; }
        [RegularExpression(@"^T|N$", ErrorMessage = "T/N")]
        [Required(ErrorMessage = "Wymagane")]

        public string Malaria { get; set; }

        public virtual Badania Badania { get; set; }


    }
}
