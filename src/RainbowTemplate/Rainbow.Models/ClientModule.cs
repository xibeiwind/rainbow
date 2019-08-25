using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        ///     模块描述
        /// </summary>
        [Display(Name = "模块描述")]
        [Required]
        [DataType(DataType.Html)]
        public string Description { get; set; }
    }
}