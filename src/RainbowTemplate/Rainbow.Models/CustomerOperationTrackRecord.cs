using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rainbow.Common;
using Yunyong.Core;

namespace Rainbow.Models
{
    /// <summary>
    ///     客户操作记录
    /// </summary>
    [Display(Name = "客户操作记录")]
    [SkipTS]
    [Table(nameof(CustomerOperationTrackRecord))]
    public class CustomerOperationTrackRecord : Entity, IOperationTrackRecord
    {
        /// <summary>
        ///     客户编号
        /// </summary>
        [Display(Name = "客户编号")]
        public Guid CustomerId { get; set; }

        /// <summary>
        ///     相关记录编号
        /// </summary>
        [Display(Name = "相关记录编号")]
        public Guid? RecordId { get; set; }

        /// <summary>
        ///     操作对象类型
        /// </summary>
        [Display(Name = "操作对象类型")]
        public string TargetEntity { get; set; }

        /// <summary>
        ///     执行操作类型
        /// </summary>
        [Display(Name = "执行操作类型")]
        public string Option { get; set; }

        /// <summary>
        ///     执行操作备注
        /// </summary>
        [Display(Name = "执行操作备注")]
        public string OptionComments { get; set; }
    }
}
