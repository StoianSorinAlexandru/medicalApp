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
    public class Pacient : IValidatableObject
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

        #region Public - Validation
        /// <summary>
        /// Implements entity-level validation for CNP and Phone rules.
        /// </summary>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // CNP: optional in model? Business requirement says it must be valid when present
            if (!string.IsNullOrWhiteSpace(CNP))
            {
                var cnpError = Utilities.CnpValidator.Validate(CNP);
                if (cnpError is not null)
                {
                    yield return new ValidationResult(cnpError, new[] { nameof(CNP) });
                }
            }

            // Phone: optional; if present allow either E.164 or Romanian local format
            if (!string.IsNullOrWhiteSpace(NumarTelefon))
            {
                if (!IsValidPhone(NumarTelefon))
                {
                    yield return new ValidationResult("Număr de telefon invalid. Folosiți formatul +40123456789 sau 0XXXXXXXXX.", new[] { nameof(NumarTelefon) });
                }
            }
        }
        #endregion

        #region Private - Helpers
        /// <summary>
        /// Accepts E.164 (max 15 digits) or Romanian formats beginning with +40 or 0 and 9 digits following.
        /// </summary>
        private static bool IsValidPhone(string value)
        {
            value = value.Trim();
            // E.164
            var e164 = Regex.IsMatch(value, "^\\+[1-9]\\d{7,14}$");
            // Romanian specific
            var ro = Regex.IsMatch(value, "^(?:\\+40|0)\\d{9}$");
            return e164 || ro;
        }
        #endregion
    }
}
