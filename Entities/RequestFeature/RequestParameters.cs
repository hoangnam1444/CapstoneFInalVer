namespace Entities.RequestFeature
{
    public abstract class RequestParameters
    {
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 9;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }
    }

    public class PagingParameters : RequestParameters
    {
    }
}
