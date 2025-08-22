using System;
using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.Base;

namespace MedicalApplication.Module.BusinessObjects
{
    [DefaultClassOptions]
    [NavigationItem("Medici")]
    public class Medic
    {
        public virtual Guid Id { get; set; }

        [Required]
        public virtual string Nume { get; set; }

        public virtual Specializare Specializare { get; set; }
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public virtual Guid? SpecializareId { get; set; }
    }
}
