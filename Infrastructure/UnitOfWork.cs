using System;
using System.Collections.Generic;
using System.Text;
using UseCases.Interfaces;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DoctorsOfficeContext _doctorsOfficeContext;

        public UnitOfWork(DoctorsOfficeContext doctorsOfficeContext)
        {
            _doctorsOfficeContext = doctorsOfficeContext;
        }

        // Delegate to EF Core's SaveChangesAsync, which will commit all tracked changes to the database in one transaction
        public async Task<int> SaveChangesAsync()
        {
            return await _doctorsOfficeContext.SaveChangesAsync();
        }
    }
}
