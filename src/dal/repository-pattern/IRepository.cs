using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace dal.repositorypattern
{
 
    public interface IRepository<T>
        // where T : Entity
    {
        Task AddAsync(T item);
        Task<IEnumerable<T>> GetAllAsync();
        // Update
        // Delete, DeleteById
        // GetAsQueryable etc. pp
    }
}