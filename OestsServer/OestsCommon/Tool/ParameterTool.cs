using LitJson;
using System;
using System.Collections.Generic;
using System.Text;

namespace OestsCommon.Tool
{
    /// <summary>
    /// 工具类
    /// </summary>
    public static class ParameterTool
    {
        public static T GetParameter<T>(Dictionary<byte, object> parameters, OperationCode operationCode)
        {
            object o = null;
            parameters.TryGetValue((byte)operationCode, out o);
            return JsonMapper.ToObject<T>(o.ToString());
        }
    }
}
