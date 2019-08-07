using System;
using System.ComponentModel.DataAnnotations;

namespace Rainbow.Common
{
    /// <summary>
    ///     跳过生成TypeScript
    /// </summary>
    [Display(Name = "跳过生成TypeScript")]
    [AttributeUsage(AttributeTargets.Class)]
    public class SkipTSAttribute : Attribute
    {
    }
}