using System;
using System.Collections.Generic;

namespace SBD.Models
{
    public partial class Pacjent
    {
        public Pacjent()
        {
            Transfuzja = new HashSet<Transfuzja>();
        }

        public int Pacjentid { get; set; }
        public string GrupaKrwi { get; set; }
        public string Priorytet { get; set; }
        public decimal? Waga { get; set; }
        public int Osobaid { get; set; }

        public virtual Osoba Osoba { get; set; }
        public virtual ICollection<Transfuzja> Transfuzja { get; set; }
    }
}
