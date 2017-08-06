using MateralTools.MEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MLog.Model
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 记录
        /// </summary>
        [EnumShowName("记录")]
        Recode,
        /// <summary>
        /// 异常
        /// </summary>
        [EnumShowName("异常")]
        Exception
    }
}
