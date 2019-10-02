using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.Common.Utils
{
    public class JsCode2Session
    {
        [DataMember(Name = "openid")]
        public string OpenId { get; set; }
        [DataMember(Name = "session_key")]
        public string SessionKey { get; set; }
        [DataMember(Name = "unionid")]
        public string UnionId { get; set; }
        [DataMember(Name = "errcode")]
        public int ErrorCode { get; set; }
        [DataMember(Name = "errmsg")]
        public string ErrorMessage { get; set; }
    }
}
