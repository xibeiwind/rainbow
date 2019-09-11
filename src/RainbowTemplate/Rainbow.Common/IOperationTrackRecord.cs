using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rainbow.Common
{
    public interface IOperationTrackRecord
    {
        /// <summary>
        ///     相关记录Id
        /// </summary>
        Guid? RecordId { get; set; }

        /// <summary>
        ///     操作对象类型
        /// </summary>
        string TargetEntity { get; set; }

        /// <summary>
        ///     执行操作类型
        /// </summary>
        string Option { get; set; }

        /// <summary>
        ///     执行操作备注
        /// </summary>
        string OptionComments { get; set; }
    }
}
