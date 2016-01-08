using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationProjetMVC.Models
{
    public class EmploiExamenViewModel
    {
        public Models.Emploi EmploiModel { get; set; }
        public List<Models.TypeExaman> ExamenModel { get; set; }
    }
}