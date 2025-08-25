using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace MedicalApplication.Module.Rapoarte.Reports
{
    public partial class RaportProgramariReport : DevExpress.XtraReports.UI.XtraReport
    {
        public RaportProgramariReport()
        {
            // Parameters (names must match what we’ll read later)
            var pData = new Parameter
            {
                Name = "Data",
                Type = typeof(DateTime),
                Value = DateTime.Today,
                Visible = true // set false if you never want the parameter UI
            };
            var pIdMedic = new Parameter { Name = "IdMedic", Type = typeof(Guid), Value = null, Visible = true, AllowNull = true };
            var pIdSpec = new Parameter { Name = "IdSpecializare", Type = typeof(Guid), Value = null, Visible = true, AllowNull = true };

            Parameters.AddRange(new Parameter[] { pData, pIdMedic, pIdSpec });

            // Simple layout — you can refine in the Designer:
            var detail = new DetailBand { HeightF = 20 };
            Bands.Add(detail);

            var table = new XRTable { WidthF = PageWidth - Margins.Left - Margins.Right };
            var row = new XRTableRow();

            row.Cells.AddRange(new[]
            {
            MakeCell("[DenumireSpecializare]", 150),
            MakeCell("[NumeMedic]", 150),
            MakeCell("[NumePacient]", 150),
            MakeCell("[Status]", 100),
            MakeCell("[DataOra]", 150, "{0:HH:mm}")
        });
            table.Rows.Add(row);
            detail.Controls.Add(table);

            // Group header for date
            var gh = new GroupHeaderBand { HeightF = 28 };
            var title = new XRLabel { ExpressionBindings = { new ExpressionBinding("BeforePrint", "Text", "'Programări din: ' + FormatString('{0:yyyy-MM-dd}', ?Data)") } };
            gh.Controls.Add(title);
            Bands.Add(gh);
        }
        private static XRTableCell MakeCell(string expression, float width, string? format = null)
        {
            var cell = new XRTableCell { WidthF = width };
            cell.ExpressionBindings.Add(new ExpressionBinding("BeforePrint", "Text", expression));
            if (!string.IsNullOrEmpty(format))
                cell.TextFormatString = format;
            return cell;
        }
    }
}
