using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core.ViewModels;
using Rainbow.Common;
using Rainbow.Common.Enums;


namespace Rainbow.ViewModels.ControllerProjects
{
	/// <summary>
    ///     Controller项目
    /// </summary>
    [Display(Name = "Controller项目")]
	[BindModel("ControllerProject", VMType.Display)]
    public partial class ControllerProjectVM : VMBase
    {
		
        /// <summary>
        ///     项目名称
        /// </summary>
        [Display(Name = "项目名称"),Required]
        public string ProjectName { get; set; }

        /// <summary>
        ///     项目描述
        /// </summary>
        [Display(Name = "项目描述")]
        [DataType(DataType.Html)]
        public string ProjectDescription { get; set; }

        /// <summary>
        ///     是否默认项目
        /// </summary>
        [Display(Name = "是否默认项目")]
        public bool IsDefault { get; set; }

    }
}
