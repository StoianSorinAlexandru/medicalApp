using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApplication.Module.Rapoarte.DTOs
{
    public class AppointmentRowDto
    {
        public string DenumireSpecializare { get; set; } = string.Empty;
        public string NumeMedic { get; set; } = string.Empty;
        public string NumePacient { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime Data { get; set; }
    }
}
