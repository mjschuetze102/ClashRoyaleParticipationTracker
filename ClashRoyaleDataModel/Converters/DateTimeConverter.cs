using Newtonsoft.Json.Converters;

namespace ClashRoyaleDataModel.Converters
{
    class DateTimeConverter : IsoDateTimeConverter
    {
        public DateTimeConverter()
        {
            DateTimeFormat = "yyyyMMddTHHmmss.fffZ";
        }
    }
}
