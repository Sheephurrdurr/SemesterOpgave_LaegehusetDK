using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace UseCases.Interfaces
{
    public interface IPatientRepository
    {
        public Task<Patient?> GetByIdAsync(Guid id);

        public Task AddAsync(Patient patient);
    }
}
