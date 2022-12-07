using System.Text.Json;
using Newtonsoft.Json;

namespace PgAPI.Helpers
{
    public static class ObjectExtensions
    {
        public static object Concat(this object source, object target)
        {
            string sourceJson = JsonConvert.SerializeObject(source);
            string targetJson = JsonConvert.SerializeObject(target);
            sourceJson = sourceJson.Remove(sourceJson.Length - 1) + ",";
            targetJson = targetJson.Remove(0, 1);
            return System.Text.Json.JsonSerializer.Deserialize<object>(sourceJson + targetJson, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}