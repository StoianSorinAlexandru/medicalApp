using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Xpo;
using MedicalApplication.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApplication.Module.Rapoarte.RPOs
{
    [DomainComponent]
    public class AppointmentRowRpo : ReportParametersObjectBase
    {
        public AppointmentRowRpo(IObjectSpaceCreator provider) : base(provider)
        {
        }
        public virtual DateTime Data { get; set; }
        public virtual Guid IdMedic { get; set; }
        public virtual Guid IdSpecializare { get; set; }

        public override CriteriaOperator GetCriteria()
        {
            return CriteriaOperator.Parse("");
        }

        public override SortProperty[] GetSorting()
        {
            return Array.Empty<SortProperty>();
        }

        public override string ToString()
        {
            return "AppointmentRow";
        }

        protected override IObjectSpace CreateObjectSpace()
        {
            return objectSpaceCreator.CreateObjectSpace(typeof(Programare));
        }
    }
}
