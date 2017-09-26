using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace Materal.WebAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class WebApiExceptionAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public WebApiExceptionAttribute()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
        }
    }
}