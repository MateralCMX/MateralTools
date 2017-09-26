using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MChat
{
    /// <summary>
    /// 离线消息模型
    /// </summary>
    public class OffLineMessageModel
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="msgTime">消息时间</param>
        /// <param name="msgContent">消息内容</param>
        public OffLineMessageModel(DateTime msgTime, ArraySegment<byte> msgContent)
        {
            MsgTime = msgTime;
            MsgContent = msgContent;
        }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime MsgTime { get; private set; }
        /// <summary>
        /// 发送内容
        /// </summary>
        public ArraySegment<byte> MsgContent { get; private set; }
    }
}
