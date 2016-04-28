using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TotlerRepository.Models.Entity;
using TotlerRepository.Models.Repository;

namespace TotlerRepository.Interfaces
    {
    public interface IRepository
    {
        Task<IQueryable<T>> QueryAsync<T>() where T: BaseEntity;
        Task<IQueryable<T>> QueryAsync<T>(Expression<Func<T>> expression) where T: BaseEntity;
        Task<T> GetAsync<T>(Expression<Func<T>> expression) where T : BaseEntity;
        Task<RepositoryResult> CreateAsync<T>(T value) where T : BaseEntity;
        Task<RepositoryResult> CreateAsync<T>(IEnumerable<T> value) where T : BaseEntity;
        Task<RepositoryResult> UpdateAsync<T>(T value) where T : BaseEntity;
        Task<RepositoryResult> UpdateAsync<T>(IEnumerable<T> value) where T : BaseEntity;
        Task<RepositoryResult> DeleteAsync<T>(T value) where T : BaseEntity;
        Task<RepositoryResult> DeleteAsync<T>(IEnumerable<T> value) where T : BaseEntity;
        }
    }
