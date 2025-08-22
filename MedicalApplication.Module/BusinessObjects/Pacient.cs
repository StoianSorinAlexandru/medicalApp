using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using DevExpress.ExpressApp;
using DevExpress.Persistent.Base;

namespace MedicalApplication.Module.BusinessObjects
{
    /// <summary>
    /// Represents a patient entity with contact and identification data. Validation is performed for Email, Phone, and Romanian CNP.
    /// </summary>
    [DefaultClassOptions]
    [NavigationItem("Pacienți")]
    public class Pacient
    {

        #region Public Properties
        /// <summary>
        /// Unique key generated as GUID.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Patient full name. Required for CRUD operations and list views.
        /// </summary>
        [Required]
        public virtual string Nume { get; set; }

        /// <summary>
        /// Optional e-mail address. If provided, must be in RFC-compliant format.
        /// </summary>
        [EmailAddress]
        public virtual string Email { get; set; }

        /// <summary>
        /// Optional phone number. If provided, must be E.164 or a Romanian format starting with +40 or 0 followed by 9 digits.
        /// </summary>
        public virtual string NumarTelefon { get; set; }

        /// <summary>
        /// Romanian National Identification Number. Must pass strict validation.
        /// </summary>
        public virtual string CNP { get; set; }
        #endregion

 
    }
}
