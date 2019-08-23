using System;
using System.ComponentModel.DataAnnotations;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.CustomerServices
{
	/// <summary>
    ///     更新客服人员
    /// </summary>
    [Display(Name = "更新客服人员")]
	[BindModel("CustomerService", VMType.Update)]
    public class UpdateCustomerServiceVM : UpdateVM
    {
		
        /// <summary>
        ///     Name
        /// </summary>
        [Display(Name = "Name")]        
        public String Name { get; set; }

        /// <summary>
        ///     NickName
        /// </summary>
        [Display(Name = "NickName")]        
        public String NickName { get; set; }

        /// <summary>
        ///     Phone
        /// </summary>
        [Display(Name = "Phone")]        
        public String Phone { get; set; }

    }
}
