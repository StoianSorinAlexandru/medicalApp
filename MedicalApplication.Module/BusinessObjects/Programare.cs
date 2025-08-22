using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;

namespace MedicalApplication.Module.BusinessObjects
{
    /// <summary>
    /// Represents a medical appointment entity. Includes dependent selection of doctor based on specialty.
    /// </summary>
    public enum StatusProgramare
    {
        Planificata = 0,
        Confirmata = 1,
        Anulata = 2
    }

    [DefaultClassOptions]
    [NavigationItem("Programări")]
    public class Programare
    {
        #region Public Properties
        /// <summary>
        /// Unique key generated as GUID.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Appointment start date and time. Required.
        /// </summary>
        [Required]
        [Display(Name = "Data și ora")]
        public virtual DateTime DataOra { get; set; }

        /// <summary>
        /// Appointment status: Planificată, Confirmată, Anulată.
        /// </summary>
        [Required]
        public virtual StatusProgramare Status { get; set; }

        /// <summary>
        /// Foreign key to Specialty.
        /// </summary>
        [Required]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public virtual Guid SpecializareId { get; set; }

        /// <summary>
        /// Navigation to Specialty; ImmediatePostData reloads available doctors list.
        /// </summary>
        [ImmediatePostData]
        public virtual Specializare Specializare { get; set; }

        /// <summary>
        /// Foreign key to Doctor.
        /// </summary>
        [Required]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public virtual Guid MedicId { get; set; }

        /// <summary>
        /// Navigation to Doctor; options filtered by selected Specialty.
        /// </summary>
        [DataSourceProperty(nameof(MediciDisponibili))]
        public virtual Medic Medic { get; set; }

        /// <summary>
        /// Foreign key to Patient.
        /// </summary>
        [Required]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public virtual Guid PacientId { get; set; }

        /// <summary>
        /// Navigation to Patient.
        /// </summary>
        public virtual Pacient Pacient { get; set; }

        /// <summary>
        /// Non-persistent collection with doctors available for the selected specialty.
        /// </summary>
        [NotMapped]
        public virtual IList<Medic> MediciDisponibili => (IList<Medic>?)Specializare?.MedicList ?? new List<Medic>();
        #endregion
    }
}
