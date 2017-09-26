using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.WebSockets;

namespace MateralTools.MChat
{
    /// <summary>
    /// 即时通讯管理类
    /// </summary>
    public class ChatManager
    {
        /// <summary>
        /// 分割消息字符
        /// </summary>
        public const char DIVISIONCHAR = '|';
        /// <summary>
        /// 链接用户唯一标识名称
        /// </summary>
        public const string USERIDNAME = "UserID";
        /// <summary>
        /// 用户连接池
        /// </summary>
        private static Dictionary<string, WebSocket> CONNECT_POOL = new Dictionary<string, WebSocket>();
        /// <summary>
        /// 离线消息池
        /// </summary>
        private static Dictionary<string, List<OffLineMessageModel>> MESSAGE_POOL = new Dictionary<string, List<OffLineMessageModel>>();
        /// <summary>
        /// 即时通讯
        /// </summary>
        /// <param name="context">WebSocket主体</param>
        /// <returns></returns>
        public static async Task StartChat(AspNetWebSocketContext context)
        {
            WebSocket socket = context.WebSocket;
            string userID = context.QueryString[USERIDNAME].ToString();
            try
            {
                #region 用户添加连接池
                //第一次open时，添加到连接池中
                if (!CONNECT_POOL.ContainsKey(userID))
                {
                    CONNECT_POOL.Add(userID, socket);//不存在，添加
                }
                else
                {
                    if (socket != CONNECT_POOL[userID])//当前对象不一致，更新
                    {
                        CONNECT_POOL[userID] = socket;
                    }
                }
                #endregion
                #region 离线消息处理
                if (MESSAGE_POOL.ContainsKey(userID))
                {
                    List<OffLineMessageModel> msgs = MESSAGE_POOL[userID];
                    foreach (OffLineMessageModel item in msgs)
                    {
                        await socket.SendAsync(item.MsgContent, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    MESSAGE_POOL.Remove(userID);//移除离线消息
                }
                #endregion
                string descUser = string.Empty;//目的用户
                while (true)
                {
                    if (socket.State == WebSocketState.Open)
                    {
                        ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[2048]);
                        WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None);

                        #region 消息处理（字符截取、消息转发）
                        try
                        {
                            #region 关闭Socket处理，删除连接池
                            if (socket.State != WebSocketState.Open)
                            {
                                if (CONNECT_POOL.ContainsKey(userID))
                                { 
                                    CONNECT_POOL.Remove(userID);
                                }
                                break;
                            }
                            #endregion

                            string userMsg = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);//发送过来的消息
                            string[] msgList = userMsg.Split(DIVISIONCHAR);
                            if (msgList.Length >= 2)
                            {
                                if (msgList[0].Trim().Length > 0)
                                {
                                    descUser = msgList[0].Trim();
                                }
                                if (msgList.Length == 2)
                                {
                                    userMsg = msgList[1];
                                }
                                else
                                {
                                    userMsg = "";
                                    for (int i = 1; i < msgList.Length; i++)
                                    {
                                        userMsg += msgList[i] + "|";
                                    }
                                    userMsg = userMsg.Substring(0, userMsg.Length - 1);
                                }
                            }
                            buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(userMsg));
                            if (CONNECT_POOL.ContainsKey(descUser))//判断客户端是否在线
                            {
                                WebSocket destSocket = CONNECT_POOL[descUser];//目的客户端
                                if (destSocket != null && destSocket.State == WebSocketState.Open)
                                {
                                    await destSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                                }
                            }
                            else
                            {
                                await Task.Run(() =>
                                {
                                    if (!MESSAGE_POOL.ContainsKey(descUser))//将用户添加至离线消息池中
                                    {
                                        MESSAGE_POOL.Add(descUser, new List<OffLineMessageModel>());
                                    }
                                    MESSAGE_POOL[descUser].Add(new OffLineMessageModel(DateTime.Now, buffer));//添加离线消息
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                        #endregion
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                if (CONNECT_POOL.ContainsKey(userID))
                {
                    CONNECT_POOL.Remove(userID);
                }
            }
        }
    }
}