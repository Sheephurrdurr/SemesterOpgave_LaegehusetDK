using System;
using System.Collections.Generic;
using System.Text;
using Facade.DTOs;

namespace Facade.Interfaces
{
    public interface IConsultationQueries
    {
        Task<IReadOnlyList<ConsultationDto>> GetTodaysConsultationsAsync();
        Task<IReadOnlyList<PatientDto>> GetAllPatientsAsync();
        Task<IReadOnlyList<ConsultationTypeDto>> GetAllConsultationTypesAsync();
        Task<IReadOnlyList<DoctorDto>> GetAllDoctorsAsync();
    }
}
