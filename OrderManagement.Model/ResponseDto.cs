
using Newtonsoft.Json;

namespace OrderManagement.Model
{
    [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
    public class ResponseDto<T> : BaseDto where T : BaseDto
    {
        private bool? _hasError;
        /// <summary>
        /// Error happened?
        /// </summary>
        public bool? HasError
        {
            get { return  _hasError; }
            set
            {
                 _hasError = value;
                OnPropertyChanged();
            }
        }

        private string? _error;
        /// <summary>
        /// Error string if any error happened
        /// </summary>
        public string? Error
        {
            get { return _error; }
            set
            {
                _error = value;
                OnPropertyChanged();
            }
        }

        private T? _entity;
        /// <summary>
        /// Result container of <see cref="BaseDto"></see> or <see cref="PageDto{T}"></see>
        /// </summary>
        public T? Entity
        {
            get { return _entity; }
            set
            {
                _entity = value;
                OnPropertyChanged();
            }
        }
    }
}
