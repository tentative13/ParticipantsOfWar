using ParticipantsOfWar.DAL;
using ParticipantsOfWar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ParticipantsOfWar.BLL
{
    public interface IArchiveRepository
    {
        IEnumerable<Participant> GetAll();

        IEnumerable<ParticipantType> GetAllTypes();

        ArchiveContext Dbcontext();

        void Add<T>(T entity) where T : class;

        T Get<T>(decimal id) where T : class;

        T Get<T>(Guid id) where T : class;

        void Update<T>(T entity) where T : class;

        IQueryable<T> Get<T>() where T : class;

        IQueryable<T> Get<T>(Expression<Func<T, bool>> filter) where T : class;

        void Commit();

        IQueryable<T> Include<T>(IQueryable<T> query, params Expression<Func<T, object>>[] include) where T : class;

        IQueryable<T> Include<T>(IQueryable<T> query, params string[] include) where T : class;


        List<T> List<T>(Expression<Func<T, bool>> filters) where T : class;

        List<T> List<T>(Expression<Func<T, bool>> filters, string sorting) where T : class;

        List<T> List<T>(Expression<Func<T, bool>> filters, string sorting, params string[] include) where T : class;

        List<T> List<T>(Expression<Func<T, bool>> filters, string sorting, params Expression<Func<T, object>>[] include) where T : class;


        PagedList<T> Search<T>(
            Expression<Func<T, bool>> filters,
            string sorting,
            int currentPageNumber,
            int pageSize,
            params Expression<Func<T, object>>[] include) where T : class;

        PagedList<T> Search<T>(
            Expression<Func<T, bool>> filters,
            string sorting,
            int currentPageNumber,
            int pageSize,
            params string[] include) where T : class;

        IEnumerable<TElement> ExecuteSql<TElement>(string sql, params object[] parameters);

    }   
}