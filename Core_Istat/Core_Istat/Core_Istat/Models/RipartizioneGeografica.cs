using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Core_Istat.Models
{
    public partial class RipartizioneGeografica
    {
        public RipartizioneGeografica()
        {
            Regiones = new HashSet<Regione>();
        }

        public int Id { get; set; }
        [Display(Name = "Ripartizione geografica")]
        public string Denominazione { get; set; }

        public virtual ICollection<Regione> Regiones { get; set; }
    }
}
