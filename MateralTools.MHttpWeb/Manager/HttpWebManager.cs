using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MHttpWeb
{
    /// <summary>
    /// Http管理器
    /// </summary>
    public class HttpWebManager
    {
        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="URL">URL地址</param>
        /// <param name="Params">参数</param>
        /// <param name="methodType">发送类型</param>
        /// <param name="paramType">参数类型</param>
        /// <param name="dataEncode">编码类型</param>
        /// <returns>返回结果</returns>
        public static string SendRequest(string URL, string Params, MethodType methodType, ParamType paramType, Encoding dataEncode = null)
        {
            if(dataEncode == null)
            {
                dataEncode = Encoding.UTF8;
            }
            HttpWebRequest webReq;
            if (methodType == MethodType.Get)
            {
                webReq = (HttpWebRequest)WebRequest.Create(new Uri(URL + Params));
                webReq.Method = "GET";
                webReq.ContentType = "text/html;charset=UTF-8";
            }
            else
            {
                webReq = (HttpWebRequest)WebRequest.Create(new Uri(URL));
                webReq.Method = "POST";
                switch (paramType)
                {
                    case ParamType.Text:
                        webReq.ContentType = "text/html;charset=UTF-8";
                        break;
                    case ParamType.Form:
                        webReq.ContentType = "application/x-www-form-urlencoded";
                        break;
                    case ParamType.Json:
                        webReq.ContentType = "application/json;charset=UTF-8";
                        break;
                }
                byte[] byteArray = dataEncode.GetBytes(Params);
                Stream writer = webReq.GetRequestStream();
                writer.Write(byteArray, 0, byteArray.Length);
                writer.Close();
            }
            HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), dataEncode);
            string result = sr.ReadToEnd();
            sr.Close();
            response.Close();
            return result;
        }
    }
}
