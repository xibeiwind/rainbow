namespace Rainbow.ViewModels.Utils
{
    public class VerfyCodeNumLimitVM
    {
        /// <summary>
        ///     是否限制
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        ///     手机号.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     错误次数
        /// </summary>
        public int ErrorNum { get; set; }
    }
}