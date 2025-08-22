using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.Base;

namespace MedicalApplication.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Specializări")]
    public class Specializare 
    {
        public virtual Guid Id { get; set; }

        [Required]
        public virtual string Denumire { get; set; }

        // Navigation: MedicList (doctors assigned to this specialization)
        public virtual IList<Medic> MedicList { get; set; } = new List<Medic>();
    }
}
