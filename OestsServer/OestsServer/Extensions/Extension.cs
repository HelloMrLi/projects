using LitJson;
using OestsCommon;
using System;
using System.Collections.Generic;
namespace OestsServer.Extensions
{
    public static class Extensions
    {
        public static Treturn TryGetValueEx<TKey,Tvalue,Treturn>(this Dictionary<TKey, Tvalue> dic, TKey key)
        {

            if (dic == null)
                throw new Exception("Dictionary is NULL.");
            Tvalue value = default(Tvalue);
            if (!dic.TryGetValue(key, out value))
                throw new Exception("The given key:" + key + " was not present in the dictionary.");
            return LitJson.JsonMapper.ToObject<Treturn>(value.ToString());
        }
        public static T GetValue<T>(this Dictionary<byte, object> dict, ParameterCode code)
        {
            object o = null;
            dict.TryGetValue((byte)code, out o);
            return JsonMapper.ToObject<T>(o.ToString());
        }
    }   
}
