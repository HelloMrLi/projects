using LitJson;
using OestsCommon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static T GetValue<T>(this Dictionary<byte, object> dict, ParameterCode code) where T : class
    {
        object o = null;
        dict.TryGetValue((byte)code, out o);
        return JsonMapper.ToObject<T>(o.ToString());
    }
}
