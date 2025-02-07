
using Newtonsoft.Json;

namespace OrderManagement.Model
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class PageDto<T> : BaseDto where T : BaseDto
    {
        private int _currentPage;
        /// <summary>
        /// Will hold the current page which is displayed
        /// </summary>
        public int CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }

        private int _pageSize;
        /// <summary>
        /// Page size to fetch from service (so N item will be displayed-return from service etc.)
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value;
                OnPropertyChanged();
            }
        }

        private int _count;
        /// <summary>
        /// Will hold the total count of the items in the query
        /// </summary>
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                OnPropertyChanged();
            }
        }

        private IList<T>? _collection;
        /// <summary>
        /// Will hold the data (<see cref="PageSize"/> rows)
        /// </summary>
        public IList<T>? Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                OnPropertyChanged();
            }
        }
    }
}
