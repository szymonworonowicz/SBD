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
        public int Pielegniarkaid { get; set; }
        public int Badaniaid { get; set; }
        public int Pacjentid { get; set; }
        public decimal? PotrzebnaIlosc { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DataTransfuzji { get; set; }

        public virtual Badania Badania { get; set; }
        public virtual Pacjent Pacjent { get; set; }
        public virtual Pielegniarka Pielegniarka { get; set; }
        public virtual ICollection<Worek> Worek { get; set; }


    }
}
