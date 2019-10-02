using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Yunyong.Mvc;

namespace Rainbow.MP.Controllers
{
    /// <summary>
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        ///     获取客户编号
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Guid GetCustomerId(this Controller target)
        {
            return target.GetGuidValue("CustomerId");
        }

        /// <summary>
        ///     获取Sign Id
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static Guid GetSignId(this Controller target)
        {
            return target.GetGuidValue("signId");
        }

        /// <summary>
        ///     获取用户电话
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static string GetUserPhone(this Controller target)
        {
            return target.GetStringValue("Phone");
        }

        private static string GetStringValue(this Controller target, string claimType)
        {
            var tmp = target?.User.Claims.FirstOrDefault(a => a.Type == claimType);
            if (tmp != null)
                return tmp.Value;

            return string.Empty;
        }

        private static Guid GetGuidValue(this Controller target, string claimType)
        {
            var value = target.GetStringValue(claimType);
            if (Guid.TryParse(value, out var result))
                return result;

            return Guid.Empty;
        }

        /// <summary>
        ///     Gets the error string.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public static string GetErrorString(this ModelStateDictionary target)
        {
            if (target == null)
                return string.Empty;

            return string.Join("\r\n", target.Select(a => a.Value.Errors.FirstOrDefault()));
        }
    }
}