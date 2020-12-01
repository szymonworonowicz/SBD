using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SBD.Models
{
    public partial class TypDonacji
    {
        public TypDonacji()
        {
            Donacja = new HashSet<Donacja>();
        }

        public int Typid { get; set; }
        [Required(ErrorMessage = "Wymagane")]

        [MaxLength(20,ErrorMessage ="Za dlugie")]
        public string Typ { get; set; }
        [Required(ErrorMessage = "Wymagane")]

        public decimal? Czestotliwosc { get; set; }

        public virtual ICollection<Donacja> Donacja { get; set; }

    }
}
