using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Rainbow.Common.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum NotifyType
    {
        Metting,
    }
}