using MateralTools.MEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MLog
{
    /// <summary>
    /// 日志文件类型
    /// </summary>
    public enum LogStorageMode
    {
        /// <summary>
        /// 文本文件
        /// </summary>
        [EnumShowName("文本文件(*.TXT)")]
        Txt,
        /// <summary>
        /// XML文件
        /// </summary>
        [EnumShowName("XML文件(*.XML)")]
        XML,
        /// <summary>
        /// SQLServer数据库
        /// </summary>
        [EnumShowName("SQLServer数据库")]
        MSSSQL,
        /// <summary>
        /// SQLite数据库
        /// </summary>
        [EnumShowName("SQLite数据库")]
        SQLite
    }
}
