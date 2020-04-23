using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Rainbow.Common.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum InputControlType
    {
        Input = 0,
        Checkbox = 1,
        Select = 2,
        FileSelect = 3,
        Html = 4
    }
}