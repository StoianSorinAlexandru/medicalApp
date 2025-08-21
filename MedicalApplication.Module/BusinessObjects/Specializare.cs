using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalApplication.Module.BusinessObjects
{
    public class Specializare 
    {
        public virtual Guid Id { get; set; }

        [Required]
        public virtual string Denumire { get; set; }

        // Navigation: MedicList (doctors assigned to this specialization)
        public virtual IList<Medic> MedicList { get; set; } = new List<Medic>();
    }
}
