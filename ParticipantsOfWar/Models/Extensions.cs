using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParticipantsOfWar.Models
{
    public static class Extensions
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> allItems, int currentPageNumber, int pageSize)
        {
            if (currentPageNumber < 1)
                currentPageNumber = 1;

            var totalNumberOfItems = allItems.Count();
            var totalNumberOfPages = totalNumberOfItems == 0 ? 1 : (totalNumberOfItems / pageSize + (totalNumberOfItems % pageSize > 0 ? 1 : 0));
            if (currentPageNumber > totalNumberOfPages)
                currentPageNumber = totalNumberOfPages;

            var itemsToSkip = (currentPageNumber - 1) * pageSize;
            var pagedItems = allItems.Skip(itemsToSkip).Take(pageSize).ToList();

            return new PagedList<T>(pagedItems, currentPageNumber, pageSize, totalNumberOfItems);
        }

        public static PagedList<T> ToPagedList<T>(this IEnumerable<T> allItems, int currentPageNumber, int pageSize)
        {
            var allItemsList = allItems.ToList();

            if (currentPageNumber < 1)
                currentPageNumber = 1;

            var totalNumberOfItems = allItemsList.Count();
            var totalNumberOfPages = totalNumberOfItems == 0 ? 1 : (totalNumberOfItems / pageSize + (totalNumberOfItems % pageSize > 0 ? 1 : 0));
            if (currentPageNumber > totalNumberOfPages)
                currentPageNumber = totalNumberOfPages;

            var itemsToSkip = (currentPageNumber - 1) * pageSize;
            var pagedItems = allItemsList.Skip(itemsToSkip).Take(pageSize).ToList();

            return new PagedList<T>(pagedItems, currentPageNumber, pageSize, totalNumberOfItems);
        }

        public static IEnumerable<T> Flatten<T>(this T source, Func<T, IEnumerable<T>> selector)
        {
            return selector(source).SelectMany(c => Flatten(c, selector))
                                   .Concat(new[] { source });
        }

        public static List<T>[] SplitBy<T>(this IEnumerable<T> source, int splitSize)
        {
            var splitted = source.Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / splitSize)
                .Select(g => g.Select(x => x.Value).ToList<T>()).ToArray();
            return splitted;
        }
    }
}