using Rainbow.Common.Enums;

using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.Tasks
{
    public class TaskVM : VMBase
    {
        public string Title { get; set; }
        public decimal Progress { get; set; }
        public TaskState State { get; set; }
    }
}