﻿using ParticipantsOfWar.DAL;
using ParticipantsOfWar.Dto;
using ParticipantsOfWar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;

namespace ParticipantsOfWar.BLL
{
    public class ArchiveRepository : IArchiveRepository, IDisposable
    {
        private ArchiveContext db = new ArchiveContext();

        public IQueryable<Participant> GetAll()
        {
            return db.Set<Participant>().Include(x => x.type).Include(x => x.Photos).Include(x => x.Documents).AsNoTracking();
           
        }
        public IEnumerable<ParticipantType> GetAllTypes()
        {
            return db.Set<ParticipantType>();
        }
        public ArchiveContext Dbcontext()
        {
            return db;
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
        public IQueryable<T> Get<T>() where T : class
        {
            return db.Set<T>();
        }
        public T Get<T>(decimal id) where T : class
        {
            return db.Set<T>().Find(id);
        }
        public T Get<T>(Guid id) where T : class
        {
            return db.Set<T>().Find(id);
        }
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

            return include.Aggregate(query, (current, includes) => current.Include(includes));
        }
        public IQueryable<T> Include<T>(IQueryable<T> query, params string[] include) where T : class
        {
            if (include == null) return query;

            return include.Aggregate(query, (current, includes) => current.Include(includes));
        }
        public List<T> List<T>(Expression<Func<T, bool>> filters) where T : class
        {
            return List(filters, null);
        }
        public List<T> List<T>(Expression<Func<T, bool>> filters, string sorting) where T : class
        {
            return List(filters, sorting, new Expression<Func<T, object>>[0]);
        }
        public virtual List<T> List<T>(Expression<Func<T, bool>> filters,string sorting, params Expression<Func<T, object>>[] include) where T : class
        {
            var objectSet = db.Set<T>().AsQueryable();
            var query = objectSet.Where(filters ?? (t => true));

            query = Include(query, include);

            if (!string.IsNullOrEmpty(sorting))
                query = query.OrderBy(sorting);

            return query.ToList();
        }
        public virtual List<T> List<T>(Expression<Func<T, bool>> filters,string sorting, params string[] include) where T : class
        {
            var objectSet = db.Set<T>().AsQueryable();
            var query = objectSet.Where(filters ?? (t => true));

            query = Include(query, include);

            if (!string.IsNullOrEmpty(sorting))
                query = query.OrderBy(sorting);

            return query.ToList();
        }
        public virtual PagedList<T> Search<T>(Expression<Func<T, bool>> filters,string sorting,int currentPageNumber,int pageSize, params Expression<Func<T, object>>[] include) where T : class
        {
            var objectSet = db.Set<T>().AsQueryable();
            var query = objectSet.Where(filters ?? (t => true));

            query = Include(query, include);

            if (!string.IsNullOrEmpty(sorting))
                query = query.OrderBy(sorting);

            return query.ToPagedList(currentPageNumber, pageSize);
        }
        public virtual PagedList<T> Search<T>(Expression<Func<T, bool>> filters,string sorting,int currentPageNumber,int pageSize,params string[] include) where T : class
        {
            var objectSet = db.Set<T>().AsQueryable();
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
        public List<ParticipantsDto> GetFiltered(TableFilter filter)
        {
            IQueryable<Participant> participants;
            List<ParticipantsDto> participantsDto = new List<ParticipantsDto>();
            try
            {
                participants = this.GetAll().OrderBy(x=>x.type.Priority).ThenBy(x=>x.Surname);


                if (!String.IsNullOrEmpty(filter.firstname))
                {
                    participants = participants.Where(x => x.Firstname.ToLower().StartsWith(filter.firstname.ToLower()));
                }
               
                if (!String.IsNullOrEmpty(filter.middlename))
                {
                    participants = participants.Where(x => x.Middlename.ToLower().StartsWith(filter.middlename.ToLower()));
                }
                
                if (!String.IsNullOrEmpty(filter.surname))
                {
                    participants = participants.Where(x => x.Surname.ToLower().StartsWith(filter.surname.ToLower()));
                }
                if (filter.birthday != null && filter.birthday != DateTime.MinValue)
                {
                    participants = participants.Where(x => x.Birthday != null);
                    participants = participants.Where(x => x.Birthday.Value.Date == filter.birthday.Date);
                }

                if (filter.ParticipantsTypes > 0)
                {
                    participants = participants.Where(x => x.type.Priority == filter.ParticipantsTypes);
                }

                foreach (var item in participants.ToArray())
                {
                    participantsDto.Add(ParticipantsDto.MapToDto(item));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return participantsDto;
        }
        public List<ParticipantsDto> GetFiltered(TableFilter filter, List<Guid> guidscache)
        {
            IQueryable<Participant> participants;
            List<ParticipantsDto> participantsDto = new List<ParticipantsDto>();
            try
            {
                participants = this.GetAll().OrderBy(x => x.type.Priority).ThenBy(x => x.Surname);

                participants = participants.Where(x => !guidscache.Contains(x.ParticipantId)).AsQueryable();

                if (!String.IsNullOrEmpty(filter.firstname))
                {
                    participants = participants.Where(x => x.Firstname.ToLower().StartsWith(filter.firstname.ToLower()));
                }

                if (!String.IsNullOrEmpty(filter.middlename))
                {
                    participants = participants.Where(x => x.Middlename.ToLower().StartsWith(filter.middlename.ToLower()));
                }

                if (!String.IsNullOrEmpty(filter.surname))
                {
                    participants = participants.Where(x => x.Surname.ToLower().StartsWith(filter.surname.ToLower()));
                }
                if (filter.birthday != null && filter.birthday != DateTime.MinValue)
                {
                    participants = participants.Where(x => x.Birthday != null);
                    participants = participants.Where(x => x.Birthday.Value.Date == filter.birthday.Date);
                }

                if (filter.ParticipantsTypes > 0)
                {
                    participants = participants.Where(x => x.type.Priority == filter.ParticipantsTypes);
                }

                foreach (var item in participants.ToArray())
                {
                    participantsDto.Add(ParticipantsDto.MapToDto(item));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return participantsDto;
        }
        public int GetFilteredTotal(TableFilter filter)
        {
            IQueryable<Participant> participants = this.GetAll();
            try
            {
                if (!String.IsNullOrEmpty(filter.firstname))
                {
                    participants = participants.Where(x => x.Firstname.ToLower().StartsWith(filter.firstname.ToLower()));
                }

                if (!String.IsNullOrEmpty(filter.middlename))
                {
                    participants = participants.Where(x => x.Middlename.ToLower().StartsWith(filter.middlename.ToLower()));
                }

                if (!String.IsNullOrEmpty(filter.surname))
                {
                    participants = participants.Where(x => x.Surname.ToLower().StartsWith(filter.surname.ToLower()));
                }
                if (filter.birthday != null && filter.birthday != DateTime.MinValue)
                {
                    participants = participants.Where(x => x.Birthday != null);
                    participants = participants.Where(x => x.Birthday.Value.Date == filter.birthday.Date);
                }

                if (filter.ParticipantsTypes > 0)
                {
                    participants = participants.Where(x => x.type.Priority == filter.ParticipantsTypes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return participants.Count();
        }
        public void Delete<T>(decimal id) where T : class
        {
            var dbEntityEntry = db.Set<T>().Find(id);
            db.Set<T>().Remove(dbEntityEntry);
        }
        public void Delete<T>(Guid id) where T : class
        {
            var dbEntityEntry = db.Set<T>().Find(id);
            db.Set<T>().Remove(dbEntityEntry);
        }
        public void Delete<T>(T entity) where T : class
        {
            db.Set<T>().Remove(entity);
        }
    }

}