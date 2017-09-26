using MateralTools.MResult;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Materal.WebAPI
{
    /// <summary>
    /// 验证登录过滤器
    /// </summary>
    public class VerificationLoginAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public VerificationLoginAttribute()
        {
        }
        /// <summary>
        /// 执行Action之前触发
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ControllerContext.Controller.GetType().GetCustomAttributes(typeof(NotVerificationLoginAttribute), false).Length == 0)
            {
                string MeName = actionContext.ControllerContext.Request.RequestUri.Segments.Last();
                if (actionContext.ControllerContext.Controller.GetType().GetMethod(MeName).GetCustomAttributes(typeof(NotVerificationLoginAttribute), false).Length == 0)
                {
                    HttpRequest request = HttpContext.Current.Request;
                    if (request.Params["UserID"] != null && request.Params["Token"] != null)
                    {
                        //BasePageModel basePM = new BasePageModel
                        //{
                        //    UserID = request.Params["UserID"].ToString(),
                        //    Token = request.Params["Token"].ToString()
                        //};
                        //MResultModel<UserModel> resM = new UserManager().GetUserInfoByID(basePM.ObjectIdUserID);
                        //if (resM.ResultType == MResultType.Success)
                        //{
                        //    if (resM.Data.Token.Value == basePM.Token && !resM.Data.Token.IsOverdue)
                        //    {
                        //        base.OnActionExecuting(actionContext);
                        //    }
                        //    else//Token值不正确或者Token已过期401
                        //    {
                        //        throw new HttpResponseException(HttpStatusCode.Unauthorized);
                        //    }
                        //}
                        //else//用户不存在401
                        //{
                        //    throw new HttpResponseException(HttpStatusCode.Unauthorized);
                        //}
                    }
                    else//不包含所需参数400
                    {
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 不进行登录验证
    /// </summary>
    public class NotVerificationLoginAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public NotVerificationLoginAttribute() { }
    }
}