using System;
using System.Collections.Generic;
using System.Text;

namespace Facade.DTOs
{
    public record ConsultationDto(
        Guid Id,
        string DoctorName,
        string PatientName,
        string ConsultationTypeName,
        DateTime StartTime,
        DateTime EndTime
    );
}
