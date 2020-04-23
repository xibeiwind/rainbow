using System.ComponentModel.DataAnnotations;

using Rainbow.Common.Enums;

namespace Rainbow.Common
{
    public class MessageTplConfig
    {
        /// <summary>
        ///     手机模板号码
        /// </summary>
        [Display(Name = "手机模板号码")]
        public string TplId { get; set; }

        /// <summary>
        ///     参考变量示例：#code#=1234&#company#=聚合数据在POST参数时，无特殊字符的代码示例:tpl_value=urlencode("#code#=1234&#company#=聚合数据")
        ///     带特殊字符的代码示例:tpl_value=urlencode("#code#=1234&#company#=urlencode('聚#合#数#据')")
        /// </summary>
        public string TplValue { get; set; }

        /// <summary>
        ///     模板类型
        /// </summary>
        [Display(Name = "模板类型")]
        public TplType TplType { get; set; }
    }
}