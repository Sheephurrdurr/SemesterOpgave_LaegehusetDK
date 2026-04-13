using System;
using System.Collections.Generic;
using System.Text;

namespace UseCases.Interfaces
{
    public interface IUnitOfWork
    {
        // Commit all tracked changes to database in one transaction
        Task<int> SaveChangesAsync();
    }
}
