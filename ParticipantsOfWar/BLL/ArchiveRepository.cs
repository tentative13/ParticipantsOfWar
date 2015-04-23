using ParticipantsOfWar.DAL;
using ParticipantsOfWar.Models;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Web;
using System.Data;
using System.Data.Entity.Infrastructure;
using ParticipantsOfWar.Models;

namespace ParticipantsOfWar.BLL
{
    public class ArchiveRepository : IArchiveRepository, IDisposable
    {
        private ArchiveContext db = new ArchiveContext();

        public IEnumerable<Participant> GetAll()
        {
            return db.Participants;
        }

        public void Add<T>(T entity) where T : class
        {
            var dbEntityEntry = db.Entry(entity);

            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                db.Set<T>().Add(entity);
            }
        }

        public void Update<T>(T entity) where T : class
        {
            var entry = db.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                db.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        /// <summary>
        /// AsNoTracking ВЫКЛ. В контекст не попадает. -память +скорость
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Get<T>() where T : class
        {
            return db.Set<T>().AsNoTracking();
        }

        /// <summary>
        /// Моментально забирает из контекста если есть, если нет то из базы и добавляет в контекст
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get<T>(decimal id) where T : class
        {
            return db.Set<T>().Find(id);
        }

        /// <summary>
        /// AsNoTracking ВКЛ. Попадает в контекст. Для работы с сущностями
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IQueryable<T> Get<T>(Expression<Func<T, bool>> filter) where T : class
        {
            return db.Set<T>().Where(filter);
        }

        public void Commit()
        {
            db.SaveChanges();
        }


        public IEnumerable<TElement> ExecuteSql<TElement>(string sql, params object[] parameters)
        {
            return db.Database.SqlQuery<TElement>(sql, parameters);
        }

        public IQueryable<T> Include<T>(IQueryable<T> query, params Expression<Func<T, object>>[] include) where T : class
        {
            if (include == null) return query;

            return include.Aggregate(query,(current, includes) => current.Include(includes));
        }

        public IQueryable<T> Include<T>(IQueryable<T> query, params string[] include) where T : class
        {
            if (include == null)return query;

            return include.Aggregate(query,(current, includes) => current.Include(includes));
        }


        public List<T> List<T>(Expression<Func<T, bool>> filters) where T : class
        {
            return List(filters, null);
        }

        public List<T> List<T>(Expression<Func<T, bool>> filters, string sorting) where T : class
        {
            return List(filters, sorting, new Expression<Func<T, object>>[0]);
        }


        public virtual List<T> List<T>(
            Expression<Func<T, bool>> filters,
            string sorting,
            params Expression<Func<T, object>>[] include) where T : class
        {
            var objectSet = db.Set<T>().AsNoTracking().AsQueryable();
            var query = objectSet.Where(filters ?? (t => true));

            query = Include(query, include);

            if (!string.IsNullOrEmpty(sorting))
                query = query.OrderBy(sorting);

            return query.ToList();
        }


        public virtual List<T> List<T>(
            Expression<Func<T, bool>> filters,
            string sorting, params string[] include) where T : class
        {
            var objectSet = db.Set<T>().AsNoTracking().AsQueryable();
            var query = objectSet.Where(filters ?? (t => true));

            query = Include(query, include);

            if (!string.IsNullOrEmpty(sorting))
                query = query.OrderBy(sorting);

            return query.ToList();
        }

        public virtual PagedList<T> Search<T>(
            Expression<Func<T, bool>> filters,
            string sorting,
            int currentPageNumber,
            int pageSize,
            params Expression<Func<T, object>>[] include) where T : class
        {
            var objectSet = db.Set<T>().AsNoTracking().AsQueryable();
            var query = objectSet.Where(filters ?? (t => true));

            query = Include(query, include);

            if (!string.IsNullOrEmpty(sorting))
                query = query.OrderBy(sorting);

            return query.ToPagedList(currentPageNumber, pageSize);
        }


        public virtual PagedList<T> Search<T>(
            Expression<Func<T, bool>> filters,
            string sorting,
            int currentPageNumber,
            int pageSize,
            params string[] include) where T : class
        {
            var objectSet = db.Set<T>().AsNoTracking().AsQueryable();
            var query = objectSet.Where(filters ?? (t => true));

            query = Include(query, include);

            if (!string.IsNullOrEmpty(sorting))
                query = query.OrderBy(sorting);

            return query.ToPagedList(currentPageNumber, pageSize);
        }



        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}