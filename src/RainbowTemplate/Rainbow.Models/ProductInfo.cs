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
        /// <summary>
        ///     商品名称
        /// </summary>
        [Display(Name = "商品名称")]
        public string Name { get; set; }
        /// <summary>
        ///     商品描述
        /// </summary>
        [Display(Name = "商品描述")]
        public string Description { get; set; }
        /// <summary>
        ///     商品价格
        /// </summary>
        [Display(Name = "商品价格")]
        public decimal Price { get; set; }
    }
}
