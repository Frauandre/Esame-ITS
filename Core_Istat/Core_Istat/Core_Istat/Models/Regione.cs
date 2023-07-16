using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core_Istat.Models
{
    public partial class Regione
    {
        public Regione()
        {
            Provincia = new HashSet<Provincium>();
        }

        public int Id { get; set; }
        [Display(Name ="Regione")]
        public string Denominazione { get; set; }
        public int IdRipartizione { get; set; }

        public virtual RipartizioneGeografica IdRipartizioneNavigation { get; set; } = null!;
        public virtual ICollection<Provincium> Provincia { get; set; }
    }
}
