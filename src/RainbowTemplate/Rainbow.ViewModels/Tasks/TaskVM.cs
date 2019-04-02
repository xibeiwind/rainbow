using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rainbow.Common.Enums;
using Yunyong.Core;
using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.Tasks
{
    public class TaskVM : VMBase
    {
        public string Title { get; set; }
        public decimal Progress { get; set; }
        public TaskState State { get; set; }
    }

    public class NotifyVM:VMBase
    {
        public NotifyType Type { get; set; }
        public string Title { get; set; }
    }

    public class NotifyQueryOption:PagingQueryOption
    {
        public NotifyType? Type { get; set; }
    }
}
