using DevExpress.Persistent.BaseImpl.EF;
using MedicalApplication.Module.Rapoarte.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApplication.Module.DatabaseUpdate
{
    public static class ReportDbInitializer
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReportDataV2>().HasData(new ReportDataV2[] {
                new ReportDataV2
                {
                    ID = Guid.Parse("62E3248D-36B5-4E4E-8027-D4C0EC26C0C8"),
                    DisplayName = "Raport Programari",
                    PredefinedReportTypeName = "RaportProgramariReport",
                    IsInplaceReport = false,
                    DataTypeName = "",
                    ParametersObjectTypeName = typeof(AppointmentRowDto).ToString()
                }
            });
        }
    }
}
