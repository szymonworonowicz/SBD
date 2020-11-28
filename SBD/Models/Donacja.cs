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

        public string FormatDate => Datadonacji.Value.Date.ToShortDateString();
        public string FormatIloscDonacji 
        {
            get
            {
                int donacje =(int) Math.Floor(IloscDonacji.Value);
                if (donacje == 1)
                    return $"{donacje} donacja";
                else if (donacje >= 2 || donacje <=4)
                    return $"{donacje} donacje";

                return $"{donacje} donacji";
            }

        }

        public virtual Badania Badania { get; set; }
        public virtual Donator Donator { get; set; }
        public virtual Pielegniarka Pielegniarka { get; set; }
        public virtual TypDonacji Typ { get; set; }
        public virtual ICollection<Worek> Worek { get; set; }
    }
}
