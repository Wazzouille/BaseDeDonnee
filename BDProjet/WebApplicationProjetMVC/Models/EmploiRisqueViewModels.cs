using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationProjetMVC.Models
{
    public class EmploiRisqueViewModels
    {
        public Models.Emploi EmploiModel { get; set; }
        public List<Models.Risque> RisqueModel { get; set; }
    }
}