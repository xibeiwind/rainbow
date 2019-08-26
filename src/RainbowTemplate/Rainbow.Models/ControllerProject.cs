

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yunyong.Core;
namespace Rainbow.Models
{
    /// <summary>
    ///     Controller项目
    /// </summary>
    [Display(Name = "Controller项目")]
    [Table(nameof(ControllerProject))]
    public class ControllerProject : Entity
    {
        /// <summary>
        ///     项目名称
        /// </summary>
        [Display(Name = "项目名称")]
        [Required]
        public string ProjectName { get; set; }
        /// <summary>
        ///     项目描述
        /// </summary>
        [Display(Name = "项目描述")]
        [DataType(DataType.Html)]
        public string ProjectDescription { get; set; }
        /// <summary>
        ///     是否默认项目
        /// </summary>
        [Display(Name = "是否默认项目")]
        public bool IsDefault { get; set; }

    }
}
