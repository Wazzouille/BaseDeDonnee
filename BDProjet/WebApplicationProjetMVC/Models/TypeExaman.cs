//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplicationProjetMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TypeExaman
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TypeExaman()
        {
            this.ExamenParticuliers = new HashSet<ExamenParticulier>();
        }
    
        public decimal idExamen { get; set; }
        public string libelle { get; set; }
        public string periodicite { get; set; }
        public decimal prixTS { get; set; }
        public decimal prixNS { get; set; }
        public decimal CompteDeProduit { get; set; }
        public decimal idRisque { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExamenParticulier> ExamenParticuliers { get; set; }
        public virtual Risque Risque { get; set; }
    }
}