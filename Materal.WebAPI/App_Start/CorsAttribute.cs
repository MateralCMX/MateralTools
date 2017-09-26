using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Materal.WebAPI
{
    /// <summary>
    /// 跨域支持处理
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CorsAttribute : Attribute
    {
        /// <summary>
        /// 允许访问的URL
        /// </summary>
        public Uri[] AllowOrigins { get; private set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage { get; private set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="allowOrigins"></param>
        public CorsAttribute(params string[] allowOrigins)
        {
            if (allowOrigins.Length == 1 && allowOrigins[0] == "*")
            {
                AllowOrigins = null;
            }
            else
            {
                AllowOrigins = (allowOrigins ?? new string[0]).Select(origin => new Uri(origin)).ToArray();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public bool TryEvaluate(HttpRequestMessage request, out IDictionary<string, string> headers)
        {
            headers = null;
            string origin = request.Headers.GetValues("Origin").First();
            Uri originUri = new Uri(origin);
            if (AllowOrigins != null && AllowOrigins.Contains(originUri))
            {
                headers = this.GenerateResponseHeaders(request);
                return true;
            }
            this.ErrorMessage = "Cross-origin request denied";
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private IDictionary<string, string> GenerateResponseHeaders(HttpRequestMessage request)
        {
            //设置响应报头"Access-Control-Allow-Methods"
            string origin = request.Headers.GetValues("Origin").First();
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Access-Control-Allow-Origin", origin }
            };
            if (request.IsPreflightRequest())
            {
                //设置响应报头"Access-Control-Request-Headers"
                //和"Access-Control-Allow-Headers"
                headers.Add("Access-Control-Allow-Methods", "*");
                string requestHeaders = request.Headers.GetValues("Access-Control-Request-Headers").FirstOrDefault();
                if (!string.IsNullOrEmpty(requestHeaders))
                {
                    headers.Add("Access-Control-Allow-Headers", requestHeaders);
                }
            }
            return headers;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsPreflightRequest(this HttpRequestMessage request)
        {
            return request.Method == HttpMethod.Options &&
                request.Headers.GetValues("Origin").Any() &&
                request.Headers.GetValues("Access-Control-Request-Method").Any();
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class CorsMessageHandler : DelegatingHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //得到描述目标Action的HttpActionDescriptor
            HttpMethod originalMethod = request.Method;
            bool isPreflightRequest = request.IsPreflightRequest();
            if (isPreflightRequest)
            {
                string method = request.Headers.GetValues("Access-Control-Request-Method").First();
                request.Method = new HttpMethod(method);
            }
            HttpConfiguration configuration = request.GetConfiguration();
            HttpControllerDescriptor controllerDescriptor = configuration.Services.GetHttpControllerSelector().SelectController(request);
            HttpControllerContext controllerContext = new HttpControllerContext(request.GetConfiguration(), request.GetRouteData(), request)
            {
                ControllerDescriptor = controllerDescriptor
            };
            HttpActionDescriptor actionDescriptor = configuration.Services.GetActionSelector().SelectAction(controllerContext);

            //根据HttpActionDescriptor得到应用的CorsAttribute特性
            CorsAttribute corsAttribute = actionDescriptor.GetCustomAttributes<CorsAttribute>().FirstOrDefault() ??
                controllerDescriptor.GetCustomAttributes<CorsAttribute>().FirstOrDefault();
            if (null == corsAttribute)
            {
                return base.SendAsync(request, cancellationToken);
            }

            //利用CorsAttribute实施授权并生成响应报头
            request.Method = originalMethod;
            bool authorized = corsAttribute.TryEvaluate(request, out IDictionary<string, string> headers);
            HttpResponseMessage response;
            if (isPreflightRequest)
            {
                if (authorized)
                {
                    response = new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, corsAttribute.ErrorMessage);
                }
            }
            else
            {
                response = base.SendAsync(request, cancellationToken).Result;
            }

            //添加响应报头
            foreach (var item in headers)
            {
                response.Headers.Add(item.Key, item.Value);
            }
            return Task.FromResult(response);
        }
    }
}