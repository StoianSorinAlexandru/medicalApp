using DevExpress.ExpressApp;
using DevExpress.ExpressApp.EFCore;
using DevExpress.ExpressApp.ReportsV2;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.XtraReports.UI;
using MedicalApplication.Module.BusinessObjects;
using MedicalApplication.Module.Rapoarte.DTOs;
using MedicalApplication.Module.Rapoarte.Manager;
using MedicalApplication.Module.Rapoarte.Reports;
using MedicalApplication.Module.Rapoarte.RPOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApplication.Module.Service
{
    public class ReportService : IDisposable
    {

        private IReportDataSourceHelper reportDataSourceHelper;
        private Frame _Frame;
        private List<object> _CurrentObjects;
        private bool AfiseazaDiagnosticeSecundare = true;
        private Medic medici;

        public void Dispose()
        {
            if (reportDataSourceHelper != null)
            {
                reportDataSourceHelper.BeforeShowPreview -= ReportDataSourceHelper_BeforeShowPreview;
            }

            _CurrentObjects = null;
            _Frame = null;
        }


        public void ShowReport(Frame frame, List<object> obiecteCurente)
        {
            string denumireRaport = "RaportProgramariReport";
            IReportDataV2 reportData = InitailizeReportDataV2(denumireRaport, frame, obiecteCurente); 
            if(reportData == null)
            {
                frame.Application.ShowViewStrategy.ShowMessage("Raportul nu a fost gasit in baza de date!", InformationType.Error);
                return;
            }
            string handler = ReportDataProvider.GetReportStorage(_Frame.Application.ServiceProvider).GetReportContainerHandle(reportData);
            ReportServiceController controller = _Frame.GetController<ReportServiceController>();
            ReportsModuleV2 reportsModule = ReportsModuleV2.FindReportsModule(_Frame.Application.Modules);
            reportDataSourceHelper = reportsModule.ReportsDataSourceHelper;

            if(reportDataSourceHelper != null)
            {
                reportDataSourceHelper.BeforeShowPreview += ReportDataSourceHelper_BeforeShowPreview;
            }

            if(controller != null)
            {
                controller.ShowPreview(handler, null, null);
            }
        }

        private IReportDataV2 InitailizeReportDataV2(string denumireRaport, Frame frame, List<object> obiecteCurente)
        {
            _CurrentObjects = obiecteCurente;
            _Frame = frame;
            using IObjectSpace objectSpace = _Frame.Application.CreateObjectSpace(typeof(ReportDataV2));
            MedicalApplicationEFCoreDbContext dbContext = ((EFCoreObjectSpace)objectSpace).DbContext as MedicalApplicationEFCoreDbContext;
            IReportDataV2 reportData = objectSpace.FirstOrDefault<ReportDataV2>(data => data.PredefinedReportTypeName == denumireRaport);
            return reportData;
        }

        private void ReportDataSourceHelper_BeforeShowPreview(object sender, BeforeShowPreviewEventArgs e)
        {
            XtraReport report = e.Report as XtraReport;
            SeteazaDateRaport(report);
        }

        private void SeteazaDateRaport(XtraReport report)
        {
            using IObjectSpace objectSpace = _Frame.Application.CreateObjectSpace(typeof(ReportDataV2));
            MedicalApplicationEFCoreDbContext dbContext = (((DevExpress.ExpressApp.EFCore.EFCoreObjectSpace)(objectSpace)).DbContext as MedicalApplicationEFCoreDbContext);

            switch (report.GetType().Name)
            {
                case nameof(RaportProgramariReport):
                    RaportProgramariManager reportManager = new RaportProgramariManager();
                    if (report.Parameters["XafReportParameterObject"]?.Value == null)
                    {
                        AppointmentRowRpo rpo = new AppointmentRowRpo(ReportDataProvider.GetReportObjectSpaceProvider(_Frame.Application.ServiceProvider));
                        reportManager.ConfigurareRPO(dbContext, rpo, _CurrentObjects[0]);
                        reportDataSourceHelper.SetXafReportParametersObject(report, rpo);
                    }
                    reportManager.ConfigurareRaport(dbContext, report);
                    break;
                default:
                    break;
            }
        }
    }
}
