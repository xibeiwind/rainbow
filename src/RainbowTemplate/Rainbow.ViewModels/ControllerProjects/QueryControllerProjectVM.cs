using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core;
using Yunyong.Core.Attributes;
using Rainbow.Common;
using Rainbow.Common.Enums;


namespace Rainbow.ViewModels.ControllerProjects
{
	/// <summary>
    ///     查询Controller项目
    /// </summary>
    [Display(Name = "查询Controller项目")]
	[BindModel("ControllerProject", VMType.Query)]
    public class QueryControllerProjectVM : PagingQueryOption
    {
		
        /// <summary>
        ///     项目名称
        /// </summary>
        [Display(Name = "项目名称")]
        [QueryColumn("ProjectName", CompareEnum.Like)]
        public string ProjectName {get;set;}

        /// <summary>
        ///     是否默认项目
        /// </summary>
        [Display(Name = "是否默认项目")]
        public bool? IsDefault {get;set;}

    }
}
