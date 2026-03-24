using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using UseCases.Interfaces;

namespace Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DoctorsOfficeContext _context;

        public PatientRepository(DoctorsOfficeContext context)
        {
            _context = context;
        }

        public async Task<Patient?> GetByIdAsync(Guid id)
        {
            return await _context.Patients
                .FindAsync(id);
        }

        public async Task AddAsync(Patient patient)
        {
            await _context.Patients
                .AddAsync(patient);
        }

        public async Task SaveAsync() 
        { 
            await _context
                .SaveChangesAsync(); 
        }


    }
}
