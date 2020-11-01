using System;
using System.Collections.Generic;

namespace SBD.Models
{
    public partial class Bankkrwi
    {
        public Bankkrwi()
        {
            Worek = new HashSet<Worek>();
        }

        public int Bankid { get; set; }
        public int Adresid { get; set; }
        public string Typkrwi { get; set; }

        public virtual Adres Adres { get; set; }
        public virtual ICollection<Worek> Worek { get; set; }
    }
}
