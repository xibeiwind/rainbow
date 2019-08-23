using System.ComponentModel.DataAnnotations;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.CustomerServices
{
	/// <summary>
    ///     删除CustomerService
    /// </summary>
    [Display(Name = "删除CustomerService")]
	[BindModel("CustomerService", VMType.Delete)]
    public class DeleteCustomerServiceVM : DeleteVM
    {
    }
}
