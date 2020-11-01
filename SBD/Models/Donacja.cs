using System;
using System.Collections.Generic;

namespace SBD.Models
{
    public partial class Donacja
    {
        public Donacja()
        {
            Worek = new HashSet<Worek>();
        }

        public int Donacjaid { get; set; }
        public int Donatorid { get; set; }
        public int Badaniaid { get; set; }
        public int Pielegniarkaid { get; set; }
        public int Typid { get; set; }
        public decimal? IloscDonacji { get; set; }
        public DateTime? Datadonacji { get; set; }

        public virtual Badania Badania { get; set; }
        public virtual Donator Donator { get; set; }
        public virtual Pielegniarka Pielegniarka { get; set; }
        public virtual TypDonacji Typ { get; set; }
        public virtual ICollection<Worek> Worek { get; set; }
    }
}
