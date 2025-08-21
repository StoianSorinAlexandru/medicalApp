using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalApplication.Module.BusinessObjects
{
    public enum StatusProgramare
    {
        Planificata = 0,
        Confirmata = 1,
        Anulata = 2
    }

    public class Programare
    {
        public virtual Guid Id { get; set; }

        [Required]
        public virtual DateTime DataOra { get; set; }

        [Required]
        public virtual StatusProgramare Status { get; set; }

        // Foreign keys and navigation properties
        [Required]
        public virtual Guid MedicId { get; set; }
        public virtual Medic Medic { get; set; }

        [Required]
        public virtual Guid PacientId { get; set; }
        public virtual Pacient Pacient { get; set; }

        [Required]
        public virtual Guid SpecializareId { get; set; }
        public virtual Specializare Specializare { get; set; }
    }
}
