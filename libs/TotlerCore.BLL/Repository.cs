using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using TotlerDb.DAL;
using TotlerRepository.Interfaces;
using TotlerRepository.Models.Entity;
using TotlerRepository.Models.Identity;
using TotlerRepository.Models.Repository;
using System.Security.Claims;

namespace TotlerCore.BLL
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _accessor;

        public Repository(AppDbContext dbContext, IHttpContextAccessor accessor)
        {
            _dbContext = dbContext;
            _accessor = accessor;
        }

        public Task<IQueryable<T>> QueryAsync<T>() where T: BaseEntity
            => Task.Run(() => (IQueryable<T>) _dbContext.Set<T>());

        public Task<IQueryable<T>> QueryAsync<T>(Expression<Func<T>> expression) where T: BaseEntity
            {  
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(Expression<Func<T>> expression) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task<RepositoryResult> CreateAsync<T>(T value) where T : BaseEntity
            => CreateAsync<T>(new[] {value});

        public Task<RepositoryResult> CreateAsync<T>(IEnumerable<T> value) where T : BaseEntity
            => Task.Run(() =>
            {
                foreach (var item in value)
                {
                    item.CreationDate = DateTime.Now;
                    item.LastEditDate = DateTime.Now;
                    item.AuthorId = _accessor?.HttpContext.User.GetUserId() ?? "System"; 
                    item.LastEditorId = _accessor?.HttpContext.User.GetUserId() ?? "System";
                    }
                try
                {
                    _dbContext.AddRange(value);
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    return new RepositoryResult(false, new[] {ex.Message});
                }
                return new RepositoryResult(true, null);
            });

        public Task<RepositoryResult> UpdateAsync<T>(T value) where T : BaseEntity
            => UpdateAsync<T>(new[] {value});


        public Task<RepositoryResult> UpdateAsync<T>(IEnumerable<T> value) where T : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task<RepositoryResult> DeleteAsync<T>(T value) where T : BaseEntity
            => DeleteAsync<T>(new[] {value});

        public Task<RepositoryResult> DeleteAsync<T>(IEnumerable<T> value) where T : BaseEntity
        {
            throw new NotImplementedException();
        }
    }
}

