using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MChat
{
    /// <summary>
    /// 即时通讯用户模型
    /// </summary>
    public class ChatUserModel
    {
        /// <summary>
        /// 头像
        /// </summary>
        public string ImageSrc { get; set; }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; set; }
    }
}
