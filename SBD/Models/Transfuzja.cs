using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SBD.Models
{
    public partial class Transfuzja
    {
        public Transfuzja()
        {
            Worek = new HashSet<Worek>();
        }

        public int Transfuzjaid { get; set; }
        [Required(ErrorMessage = "Wymagane")]

        public int? Pielegniarkaid { get; set; }
        [Required(ErrorMessage = "Wymagane")]

        public int? Badaniaid { get; set; }
        [Required(ErrorMessage = "Wymagane")]

        public int? Pacjentid { get; set; }

        [RegularExpression(@"^(?:\d{0,3}\,\d{1,2})$|^\d{0,3}$", ErrorMessage = "Format XXX,XX")]
        [Required(ErrorMessage = "Wymagane")]

        public decimal? PotrzebnaIlosc { get; set; }

        [Required(ErrorMessage = "Wymagane")]
        [DataType(DataType.Date)]
        public DateTime? DataTransfuzji { get; set; }

        public virtual Badania Badania { get; set; }
        public virtual Pacjent Pacjent { get; set; }
        public virtual Pielegniarka Pielegniarka { get; set; }
        public virtual ICollection<Worek> Worek { get; set; }


    }
}
