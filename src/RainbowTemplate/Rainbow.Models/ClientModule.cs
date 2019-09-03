using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yunyong.Core;

namespace Rainbow.Models
{
    /// <summary>
    ///     客户端模块
    /// </summary>
    [Display(Name = "客户端模块")]
    [Table(nameof(ClientModule))]
    public class ClientModule : Entity
    {
        /// <summary>
        ///     模块名称
        /// </summary>
        [Display(Name = "模块名称")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     中文标题
        /// </summary>
        [Display(Name = "Title")]
        public string Title { get; set; }

        /// <summary>
        ///     是否扩展样式
        /// </summary>
        [Display(Name = "是否扩展样式")]
        public bool IsCustomLayout { get; set; }

        /// <summary>
        ///     路由路径
        /// </summary>
        [Display(Name = "路由路径")]
        [Required]
        public string Path { get; set; }
    }
}