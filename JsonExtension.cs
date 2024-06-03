using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionAddData;

public static class JsonExtension
{
    private static readonly JsonSerializerSettings JsonSettings;

    static JsonExtension()
    {
        JsonSettings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.None,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        };
    }

    public static string Json(this object request, JsonSerializerSettings jsonSettings = null)
    {
        if (request == null) { return null; }

        return JsonConvert.SerializeObject(request, jsonSettings ?? JsonSettings);
    }

    public static T Json<T>(this byte[] request, JsonSerializerSettings jsonSettings = null)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(request), jsonSettings ?? JsonSettings);
        }
        catch
        {
            return default(T);
        }
    }

    public static T Json<T>(this string request,
        bool @default = true,
        bool soft = true,
        JsonSerializerSettings jsonSettings = null)
        where T : new()
    {
        if (!soft)
        {
            return JsonConvert.DeserializeObject<T>(request, jsonSettings ?? JsonSettings);
        }

        try
        {
            return JsonConvert.DeserializeObject<T>(request, jsonSettings ?? JsonSettings);
        }
        catch
        {
            return @default ? default(T) : new T();
        }
    }
}
