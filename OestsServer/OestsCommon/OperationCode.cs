using System;
using System.Collections.Generic;
using System.Text;

namespace OestsCommon
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum OperationCode:byte
    {
        /// <summary>
        /// 登录
        /// </summary>
        Login,
        /// <summary>
        /// 加载服务器
        /// </summary>
        LoadServer
    }
}
