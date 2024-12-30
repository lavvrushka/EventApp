using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace EventApp.Infrastructure.Persistence.Converters
{
    public class DictionaryConverter : ValueConverter<Dictionary<Guid, DateTime>, string>
    {
        public DictionaryConverter()
            : base(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<Guid, DateTime>>(v) ?? new Dictionary<Guid, DateTime>()
            )
        { }
    }
}
