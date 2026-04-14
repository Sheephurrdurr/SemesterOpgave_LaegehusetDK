using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.DTOs
{
    public class BookConsultationRequest
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public Guid ConsultationTypeId { get; set; }
        public DateTime StartTime { get; set; }
    }
}
