using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalApplication.Module.BusinessObjects
{
    public class Pacient
    {
        public virtual Guid id { get; set; }

        [Required]
        public virtual string Nume { get; set; }

        [EmailAddress]
        public virtual string Email { get; set; }

        [Phone]
        public virtual string NumarTelefon { get; set; }

        [RegularExpression(@"^\d{13}$", ErrorMessage = "CNP must be exactly 13 digits")]
        public virtual string CNP { get; set; }
    }
}
