using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yunyong.Core;

namespace Rainbow.Models
{
    /// <summary>
    ///     商品信息
    /// </summary>
    [Display(Name = "商品信息")]
    [Table(nameof(ProductInfo))]
    public class ProductInfo : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
