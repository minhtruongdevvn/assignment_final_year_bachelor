using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Dynamic;

namespace AAM.API;

public static class AppExtension
{
    private static readonly JsonSerializerSettings SnakeCaseSettings =
    new()
    {
        Formatting = Formatting.Indented,
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        }
    };

    public static void Then<T>(this T source, Action<T> callback) => callback(source);
    public static dynamic ToDynamic<T>(this T obj, IEnumerable<ObjectField>? fields = null)
    {
        IDictionary<string, object?> expando = new ExpandoObject()!;

        foreach (var propertyInfo in typeof(T).GetProperties())
        {
            var currentValue = propertyInfo.GetValue(obj);
            expando.Add(
                propertyInfo.Name.Any(c => char.IsLower(c)) ?
                    char.ToLower(propertyInfo.Name[0]) + propertyInfo.Name.Substring(1):
                    propertyInfo.Name.ToLower(), 
                currentValue
            );
        }

        if(fields != null)
        {
            foreach (var field in fields)
            {
                expando.Add(field.Name, field.Value);
            }
        }

        return (expando as ExpandoObject)!;
    }

    public static object ToSnakeCaseProps<T>(this T obj) where T : class
    {
        var snakeCaseJsonString = JsonConvert.SerializeObject(obj, SnakeCaseSettings);
        var snakeCasePropsObject = JsonConvert.DeserializeObject(snakeCaseJsonString);
        return snakeCasePropsObject!;
    }
}

public class ObjectField
{
    public string Name { get; set; } = string.Empty;
    public object? Value { get; set; }

   public ObjectField(string name, object? value)
    {
        Name = name;
        Value = value;
    }
}
