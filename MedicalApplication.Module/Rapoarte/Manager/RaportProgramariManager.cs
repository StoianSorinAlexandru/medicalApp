using DevExpress.XtraReports.UI;
using MedicalApplication.Module.BusinessObjects;
using MedicalApplication.Module.BusinessObjects.Utilities;
using MedicalApplication.Module.Rapoarte.DTOs;
using MedicalApplication.Module.Rapoarte.RPOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApplication.Module.Rapoarte.Manager
{
    internal class RaportProgramariManager
    {
        public const string PROCEDURA_STOCATA = "RaportProgramari";

        public void ConfigurareRaport(MedicalApplicationEFCoreDbContext dbContext, XtraReport report)
        {
            AppointmentRowRpo rowRpo = (AppointmentRowRpo)report.Parameters["XafReportParametersObject"].Value;

            List<AppointmentRowDto> dataSource = ConstruiesteDataSource(dbContext, rowRpo);
            report.DataSource = dataSource;
        }

        public List<AppointmentRowDto> ConstruiesteDataSource(MedicalApplicationEFCoreDbContext dbContext, AppointmentRowRpo rpo)
        {
            List<AppointmentRowDto> result = new List<AppointmentRowDto>();
            List<Parametru> parametrii = new List<Parametru>()
            {
                new Parametru()
                {
                    Nume = "Data",
                    Valoare = DateTime.Now
                },
                new Parametru()
                {
                    Nume = "IdMedic",
                    Valoare = rpo.IdMedic
                },
                new Parametru()
                {
                    Nume = "IdSpecializare",
                    Valoare = rpo.IdSpecializare
                }
            };

            DataSet dataSet = DataTableHelper.ExecutaProceduraStocata(PROCEDURA_STOCATA, parametrii, dbContext);
            DataTable data = dataSet.Tables[0];

            AppointmentRowDto row = new AppointmentRowDto();
            foreach(DataRow dataRow in data.Rows)
            {
                row = new AppointmentRowDto
                {
                    //Data = Convert.ToDateTime(dataRow[nameof(row.Data)]),
                    NumeMedic = dataRow[nameof(row.NumeMedic)].ToString(),
                    NumePacient = dataRow[nameof(row.NumePacient)].ToString(),
                    DenumireSpecializare = dataRow[nameof(row.DenumireSpecializare)].ToString(),
                    //Status = dataRow[nameof(row.Status)].ToString()
                };
                result.Add(row);
            }
            return result;
        }

        public void ConfigurareRPO(MedicalApplicationEFCoreDbContext dbContext, AppointmentRowRpo rpo, object obiectCurent)
        {
        }
    }
}
