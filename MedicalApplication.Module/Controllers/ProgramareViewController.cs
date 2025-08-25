using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using MedicalApplication.Module.BusinessObjects;
using MedicalApplication.Module.Rapoarte.DTOs;
using MedicalApplication.Module.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MedicalApplication.Module.Controllers
{
    public partial class ProgramareViewController : ViewController
    {
        ReportService reportService;
        SimpleAction listareRaport;
        List<object> obiecteCurente = new List<object>();
        public ProgramareViewController()
        {
            InitializeComponent();
            this.TargetViewType = ViewType.ListView;
            this.TargetObjectType = typeof(Programare);
            listareRaport = new SimpleAction(this, "ListareRaport", DevExpress.Persistent.Base.PredefinedCategory.Reports);
            listareRaport.Caption = "Listare Raport Programari";
            listareRaport.ImageName = "Navigation_Item_Report";
            listareRaport.Execute += ListareRaport_Execute;
        }

        private void ListareRaport_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var currentObject = View.CurrentObject as Programare;
            AppointmentParameterDto parameterDto = new AppointmentParameterDto();
            parameterDto.Data = DateTime.Now;
            //parameterDto.IdSpecializare = currentObject.SpecializareId;
            //parameterDto.IdMedic = currentObject.MedicId;
            obiecteCurente.Add(parameterDto);
            reportService.ShowReport(Frame, obiecteCurente);
        }

        [ActivatorUtilitiesConstructor]
        public ProgramareViewController(ReportService reportService): this()
        {
            this.reportService = reportService;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
    }
}
