using System;

namespace Rainbow.Services.Utils
{
    public class SecurityUtil
    {
        public string Encoding(string source)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(source);
            //转成 Base64 形式的 System.String  
            return Convert.ToBase64String(b);
        }
    }
}