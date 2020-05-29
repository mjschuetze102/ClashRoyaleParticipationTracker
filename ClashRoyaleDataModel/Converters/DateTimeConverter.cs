using Newtonsoft.Json.Converters;

namespace ClashRoyaleDataModel.Converters
{
    /// <summary>
    /// Formats DateTime objects received from the API.
    /// </summary>
    class DateTimeConverter : IsoDateTimeConverter
    {
        /// <summary>
        /// Specifies the format for incoming DateTime objects.
        /// </summary>
        public DateTimeConverter()
        {
            DateTimeFormat = "yyyyMMddTHHmmss.fffZ";
        }
    }
}
