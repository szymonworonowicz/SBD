using System;
using System.Collections.Generic;

namespace SBD.Models
{
    public partial class Kartazdrowia
    {
        public int Kartaid { get; set; }
        public string Syfilis { get; set; }
        public string Zapaleniewatrobyb { get; set; }
        public string Zapaleniewatrobyc { get; set; }
        public string Hiv { get; set; }
        public string Malaria { get; set; }

        public virtual Badania Badania { get; set; }
    }
}
