﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MLog.Manager
{
    public class LogManager
    {
        /// <summary>
        /// Log保存类型
        /// </summary>
        private LogStorageMode _logStorageMode;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="lsm"></param>
        public LogManager(LogStorageMode lsm = LogStorageMode.Txt)
        {
            _logStorageMode = lsm;
            switch (_logStorageMode)
            {
                case LogStorageMode.Txt:
                    break;
                case LogStorageMode.XML:
                    break;
                case LogStorageMode.MSSSQL:
                    break;
                case LogStorageMode.SQLite:
                    break;
                default:
                    break;
            }
        }

    }
}
