using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalApplication.Module.Rapoarte.DTOs
{
    public class AppointmentParameterDto
    {
        public Guid IdMedic { get; set; }
        public Guid IdSpecializare { get; set; }
        public DateTime Data { get; set; }
    }
}
