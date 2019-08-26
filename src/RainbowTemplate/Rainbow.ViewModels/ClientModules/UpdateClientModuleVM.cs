using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core.ViewModels;
using Rainbow.Common;
using Rainbow.Common.Enums;


namespace Rainbow.ViewModels.ClientModules
{
	/// <summary>
    ///     更新客户端模块
    /// </summary>
    [Display(Name = "更新客户端模块")]
	[BindModel("ClientModule", VMType.Update)]
    public class UpdateClientModuleVM : UpdateVM
    {
		
        /// <summary>
        ///     模块名称
        /// </summary>
        [Display(Name = "模块名称"),Required]
        public string Name {get;set;}

        /// <summary>
        ///     模块描述
        /// </summary>
        [Display(Name = "模块描述"),Required]
        [DataType(DataType.Html)]
        public string Description {get;set;}

    }
}
