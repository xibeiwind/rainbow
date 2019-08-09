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
    [Table(nameof(DataFieldType))]
    public class DataFieldType:Entity
    {
        /// <summary>
        ///     DataType类型
        /// </summary>
        [Display(Name = "DataType类型")]
        [EnumDataType(typeof(DataType))]
        public DataType Type { get; set; }
        /// <summary>
        ///     显示类型
        /// </summary>
        [Display(Name = "显示类型")]
        public string FieldTypeDisplay { get; set; }
        /// <summary>
        ///     编辑类型
        /// </summary>
        [Display(Name = "编辑类型")]
        public string FieldTypeEdit { get; set; }
    }
}
