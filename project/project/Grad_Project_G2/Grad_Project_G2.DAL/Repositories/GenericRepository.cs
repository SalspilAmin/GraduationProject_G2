using Grad_Project_G2.DAL.Data;
using Grad_Project_G2.DAL.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.DAL.Repositories
{
    public class GenericRepositories<T>(AppDbContext context) : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext context = context;
        public IEnumerable<T> GetAll()
        {
            return context.Set<T>();
        }

        public IEnumerable<T> GetAllWithPagination(int page, int pageSize)
        {
            return context.Set<T>().Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        public T? GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public void Delete(int Id)
        {
            T? entity = GetById(Id);
            if (entity != null) context.Remove(entity);

        }
        public int GetTotalCount()
        {
            return GetAll().Count();
        }


    }

}
