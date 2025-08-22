using System;
using System.ComponentModel.DataAnnotations;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.DC;

namespace MedicalApplication.Module.BusinessObjects
{
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
        public virtual Guid Id { get; set; }

        [Required]
        [Display(Name = "Data și ora")]
        public virtual DateTime DataOra { get; set; }

        [Required]
        public virtual StatusProgramare Status { get; set; }

        // Foreign keys and navigation properties
        [Required]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public virtual Guid SpecializareId { get; set; }

        [ImmediatePostData]
        public virtual Specializare Specializare { get; set; }

        // Constrain Medic list to the selected Specializare via DataSourceProperty
        [Required]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public virtual Guid MedicId { get; set; }

        [DataSourceProperty(nameof(MediciDisponibili))]
        public virtual Medic Medic { get; set; }

        [Required]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        public virtual Guid PacientId { get; set; }
        public virtual Pacient Pacient { get; set; }

        // Calculated collection used as data source for Medic selection
        [NonPersistent]
        public virtual IList<Medic> MediciDisponibili => Specializare?.MedicList ?? new List<Medic>();
    }
}
