using System;
using System.Collections.Generic;

namespace Core_Istat.Models
{
    public partial class ZonaMontana
    {
        public ZonaMontana()
        {
            Comunes = new HashSet<Comune>();
        }

        public string Id { get; set; } = null!;
        public string Denominazione { get; set; } = null!;

        public virtual ICollection<Comune> Comunes { get; set; }
    }
}
