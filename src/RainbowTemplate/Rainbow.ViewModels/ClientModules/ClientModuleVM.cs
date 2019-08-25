using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core.ViewModels;
using Rainbow.Common;
using Rainbow.Common.Enums;


namespace Rainbow.ViewModels.ClientModules
{
	/// <summary>
    ///     客户端模块
    /// </summary>
    [Display(Name = "客户端模块")]
	[BindModel("ClientModule", VMType.Display)]
    public class ClientModuleVM : VMBase
    {
		
        /// <summary>
        ///     模块名称
        /// </summary>
        [Display(Name = "模块名称"),Required]
        public String Name {get;set;}

        /// <summary>
        ///     模块描述
        /// </summary>
        [Display(Name = "模块描述"),Required]
        [DataType(DataType.Html)]
        public String Description {get;set;}

    }
}
