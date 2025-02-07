using Newtonsoft.Json;

namespace OrderManagement.Model.Orders
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class OrderDto : BaseDto
    {
        private string? _productName;
        public string? ProductName
        {
            get { return _productName; }
            set
            {
                _productName = value;
                OnPropertyChanged();
            }
        }

        private decimal? _price;
        public decimal? Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged();
            }
        }

        private string? _status;
        public string? Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }
    }
}
