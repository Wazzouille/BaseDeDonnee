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
    
    public partial class TravSoumi
    {
        public decimal idTravSoum { get; set; }
        public decimal idEmploi { get; set; }
        public decimal idRisque { get; set; }
    
        public virtual Emploi Emploi { get; set; }
        public virtual Risque Risque { get; set; }
    }
}
