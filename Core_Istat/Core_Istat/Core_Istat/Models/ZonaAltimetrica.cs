using System;
using System.Collections.Generic;

namespace Core_Istat.Models
{
    public partial class ZonaAltimetrica
    {
        public ZonaAltimetrica()
        {
            Comunes = new HashSet<Comune>();
        }

        public int Id { get; set; }
        public string Denominazione { get; set; } = null!;

        public virtual ICollection<Comune> Comunes { get; set; }
    }
}
