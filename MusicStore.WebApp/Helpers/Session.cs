using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MusicStore.WebApp.Helpers
{
    public static class Session
    {
            //Create cookie
            public static void SetObjectAsJson(this ISession session, string key, object value)
            {
                session.SetString(key, JsonConvert.SerializeObject(value));
            }
            //Get cookie
            public static T GetObjectFromJson<T>(this ISession session, string key)
            {
                var value = session.GetString(key);
                return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
            }
        
    }
}