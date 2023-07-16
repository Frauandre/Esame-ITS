using System;
using System.Collections.Generic;

namespace Core_Istat.Models
{
    public partial class Provincium
    {
        public Provincium()
        {
            Comunes = new HashSet<Comune>();
        }

        public int Id { get; set; }
        public string Denominazione { get; set; }
        public string Sigla { get; set; }
        public int? CodiceCittaMetropolitana { get; set; }
        public int IdRegione { get; set; }

        public virtual Regione IdRegioneNavigation { get; set; }
        public virtual ICollection<Comune> Comunes { get; set; }
    }
}
