using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCases.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient?> GetByIdAsync(Guid id);

        Task AddAsync(Patient patient);
    }
}
