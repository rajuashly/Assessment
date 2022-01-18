using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Express.Helpers
{
    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

        public static string DoesTokenExist(this ISession session, string key)
        {
            var token =session.GetString(key);
            if (!string.IsNullOrWhiteSpace(token))
            {
                return token;
            }
            else
            {
                return null;
            }
        }
        public static string DoesUserEmailExist(this ISession session, string key)
        {
            var email = session.GetString(key);
            if (!string.IsNullOrWhiteSpace(email))
            {
                return email;
            }
            else
            {
                return null;
            }
        }

    }
}
