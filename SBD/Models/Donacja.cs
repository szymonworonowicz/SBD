using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SBD.Models
{
    public partial class Donacja
    {
        public Donacja()
        {
            Worek = new HashSet<Worek>();
        }

        public int Donacjaid { get; set; }
        [Required(ErrorMessage = "Wymagane")]

        public int Donatorid { get; set; }
        [Required(ErrorMessage = "Wymagane")]

        public int Badaniaid { get; set; }
        [Required(ErrorMessage = "Wymagane")]

        public int Pielegniarkaid { get; set; }
        [Required(ErrorMessage = "Wymagane")]

        public int Typid { get; set; }
        [RegularExpression(@"^\d{0,5}$", ErrorMessage = "Format XXXXX")]
        [Required(ErrorMessage = "Wymagane")]
        public decimal? IloscDonacji { get; set; }
        [Required(ErrorMessage = "Wymagane")]
        [DataType(DataType.Date)]
        public DateTime? Datadonacji { get; set; }

        public string FormatDate
        {
            get
            {
                if(Datadonacji!=null)
                    return Datadonacji.Value.Date.ToShortDateString();
                return "";
            }
        }

        public string FormatIloscDonacji 
        {
            get
            {
                if(IloscDonacji.HasValue)
                {
                    int donacje = (int)Math.Floor(IloscDonacji.Value);
                    if (donacje == 1)
                        return $"{donacje} donacja";
                    else if (donacje >= 2 || donacje <= 4)
                        return $"{donacje} donacje";

                    return $"{donacje} donacji";
                }
                return "";
            }

        }

        public string Info
        {
            get
            {
                if (Donator != null && Donator.Osoba != null)
                    return $"{Donacjaid} {Donator.Osoba.Info}";
                return "";
            }
        }

        public virtual Badania Badania { get; set; }
        public virtual Donator Donator { get; set; }
        public virtual Pielegniarka Pielegniarka { get; set; }
        public virtual TypDonacji Typ { get; set; }
        public virtual ICollection<Worek> Worek { get; set; }
    }
}
