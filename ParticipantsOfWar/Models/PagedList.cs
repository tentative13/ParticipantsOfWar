using System.Collections.Generic;


namespace ParticipantsOfWar.Models
{
    public class PagedList<T>
    {
        public int PageSize { get; private set; }
        public int TotalNumberOfItems { get; private set; }
        public IList<T> CurrentPage { get; private set; }
        public int CurrentPageNumber { get; private set; }

        public int TotalNumberOfPages
        {
            get
            {
                return TotalNumberOfItems == 0
                           ? 1
                           : (TotalNumberOfItems / PageSize + (TotalNumberOfItems % PageSize > 0 ? 1 : 0));
            }
        }

        public PagedList()
        {
            PageSize = 0;
            TotalNumberOfItems = 0;
            CurrentPage = new List<T>();
            CurrentPageNumber = 0;
        }
        
        public PagedList(IList<T> currentPage, int currentPageNumber, int pageSize, int totalNumberOfItems)
        {
            PageSize = pageSize;
            TotalNumberOfItems = totalNumberOfItems;
            CurrentPage = currentPage;
            CurrentPageNumber = currentPageNumber;
        }
    }
}
