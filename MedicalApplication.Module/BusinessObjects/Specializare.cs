using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.Base;

namespace MedicalApplication.Module.BusinessObjects
{
    /// <summary>
    /// Represents a medical specialty entity. Holds doctors list assigned to it.
    /// </summary>
    [DefaultClassOptions]
    [NavigationItem("Specializări")]
    public class Specializare 
    {
        #region Public Properties
        /// <summary>
        /// Unique key generated as GUID.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Specialty name. Required.
        /// </summary>
        [Required]
        public virtual string Denumire { get; set; }

        /// <summary>
        /// Navigation: doctors assigned to this specialization. Uses ObservableCollection to support EF Core change tracking notifications.
        /// </summary>
        public virtual ObservableCollection<Medic> MedicList { get; set; } = new();
        #endregion
    }
}
