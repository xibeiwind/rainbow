using System.ComponentModel.DataAnnotations;
namespace Rainbow.TypeScript
{
    public class RainbowAction
    {
        /// <summary>
        ///     方法名称
        /// </summary>
        [Display(Name = "方法名称")]
        public string Name { get; set; }
        /// <summary>
        ///     方法描述
        /// </summary>
        [Display(Name = "方法描述")]
        public string Description { get; set; }
        /// <summary>
        ///     输入参数
        /// </summary>
        [Display(Name = "输入参数")]
        public string ArgParamsStr { get; set; }
        public string ArgsStr { get; set; }
        /// <summary>
        ///     http请求方法
        /// </summary>
        [Display(Name = "http请求方法")]
        public string Method { get; set; }
        /// <summary>
        ///     请求地址
        /// </summary>
        [Display(Name = "请求地址")]
        public string Url { get; set; }
        /// <summary>
        ///     返回类型
        /// </summary>
        [Display(Name = "返回类型")]
        public string ReturnStr { get; set; }

        public bool IsClassArguments { get; set; }
    }
}