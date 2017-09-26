using MateralTools.MChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Materal.WebAPI.Controllers.Handler
{
    /// <summary>
    /// WebChatHandler 的摘要说明
    /// </summary>
    public class WebChatHandler : IHttpHandler
    {
        /// <summary>
        /// 入口
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(ChatManager.Start);
            }
        }
        /// <summary>
        /// 可重复使用
        /// </summary>
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}