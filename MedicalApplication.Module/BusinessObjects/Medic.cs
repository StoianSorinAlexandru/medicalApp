using System;
using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.Base;

namespace MedicalApplication.Module.BusinessObjects
{
    /// <summary>
    /// Represents a doctor entity, assigned to a medical specialty.
    /// </summary>
    [DefaultClassOptions]
    [NavigationItem("Medici")]
    public class Medic
    {
        #region Public Properties
        /// <summary>
        /// Unique key generated as GUID.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Doctor full name. Required.
        /// </summary>
        [Required]
        public virtual string Nume { get; set; }

        /// <summary>
        /// Navigation to assigned Specialty.
        /// </summary>
        public virtual Specializare Specializare { get; set; }

        /// <summary>
        /// Foreign key to Specialty.
        /// </summary>
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public virtual Guid? SpecializareId { get; set; }
        #endregion
    }
}
