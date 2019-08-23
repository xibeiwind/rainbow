using System;
using System.ComponentModel.DataAnnotations;
using Rainbow.Common;
using Rainbow.Common.Enums;
using Yunyong.Core.ViewModels;

namespace Rainbow.ViewModels.CustomerServices
{
	/// <summary>
    ///     创建客服人员
    /// </summary>
    [Display(Name = "创建客服人员")]
	[BindModel("CustomerService", VMType.Create)]
    public class CreateCustomerServiceVM : CreateVM
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
