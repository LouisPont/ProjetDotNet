//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Webmanga.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class scenariste
    {
        public scenariste()
        {
            this.manga = new HashSet<manga>();
        }
    
        public int id_scenariste { get; set; }
        public string nom_scenariste { get; set; }
        public string prenom_scenariste { get; set; }
    
        public virtual ICollection<manga> manga { get; set; }
    }
}