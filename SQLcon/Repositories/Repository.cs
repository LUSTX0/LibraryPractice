using SQLcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SQLcon.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            return filter == null ? _dbSet.AsEnumerable() : _dbSet.Where(filter).AsEnumerable();
        }

        

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        //public virtual void Update(T entity)              // для  добавления параметров
        //{
        //    _dbSet.Attach(entity);
        //    _context.Entry(entity).State = EntityState.Modified;
        //    _context.SaveChanges();
        //}

        public virtual void Update(T entity, int id)
        {
            var updated = GetById(id);
            updated = entity;
            _context.SaveChanges();            
        }

        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }
    }
}
