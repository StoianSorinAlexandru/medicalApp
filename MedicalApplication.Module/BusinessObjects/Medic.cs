using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalApplication.Module.BusinessObjects
{
    public class Medic
    {
        public virtual Guid Id { get; set; }

        [Required]
        public virtual string Nume { get; set; }

        // Optional: convenience property for Specializare name via navigation
        public virtual Specializare Specializare { get; set; }
        public virtual Guid? SpecializareId { get; set; }
    }
}
