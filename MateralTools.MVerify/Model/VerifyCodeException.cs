using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MateralTools.MVerify
{
    /// <summary>
    /// 验证码异常类
    /// </summary>
    public class VerifyCodeException : ApplicationException
    {
        public VerifyCodeException()
        {
        }

        public VerifyCodeException(string message) : base(message)
        {
        }

        public VerifyCodeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected VerifyCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
