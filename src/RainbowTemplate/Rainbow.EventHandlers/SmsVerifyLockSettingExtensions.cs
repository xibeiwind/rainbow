using Rainbow.Common;
using Rainbow.ViewModels.Utils;

namespace Rainbow.EventHandlers
{
    internal static class SmsVerifyLockSettingExtensions
    {
        /// <summary>
        /// 验证手机短信验证是否出错次数过量
        /// </summary>
        /// <param name="target"></param>
        /// <param name="codeNumLimit"></param>
        /// <returns></returns>
        internal static bool IsVerifyOverdose(this SmsVerifyLockSetting target, VerfyCodeNumLimitVM codeNumLimit)
        {
            if (codeNumLimit.IsLocked)
            {
                return true;
            }
            if (codeNumLimit.ErrorNum >= target.LockTriggerErrorTimes)
            {
                return true;
            }


            return false;
        }
    }
}