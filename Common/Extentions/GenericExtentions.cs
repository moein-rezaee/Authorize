using System.Reflection;
using Newtonsoft.Json;

namespace Extentions
{
    public static class GenericExtentions
    {
        public static TResult? GetPropValue<TResult, TEntity>(this TEntity item, string key)
        {
            if (item is null) throw new Exception("Extention Item Is Null!");
            Type t = item.GetType();
            PropertyInfo? prop = t.GetProperty(key) ?? throw new Exception("Propery Not Found!");
            var val = prop.GetValue(item);
            if (val is null) return default;
            string json = JsonConvert.SerializeObject(val);
            return JsonConvert.DeserializeObject<TResult>(json);
        }

        public static Guid GetIdAsGuid<TEntity>(this TEntity item) {
            string id = item.GetPropValue<string, TEntity>("Id") ?? throw new Exception("Id Not Found");
            return new Guid(id);
        }
    }
}
