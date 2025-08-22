using System;
using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp;

namespace MedicalApplication.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Pacienți")]
    public class Pacient
    {
        public virtual Guid id { get; set; }

        [Required]
        public virtual string Nume { get; set; }

        [EmailAddress]
        public virtual string Email { get; set; }

        [Phone]
        public virtual string NumarTelefon { get; set; }

        [RegularExpression(@"^\d{13}$", ErrorMessage = "CNP trebuie să conțină exact 13 cifre")]
        public virtual string CNP { get; set; }
    }
}
