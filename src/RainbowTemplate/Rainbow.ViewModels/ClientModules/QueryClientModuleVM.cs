using System;
using System.ComponentModel.DataAnnotations;
using Yunyong.Core;
using Yunyong.Core.Attributes;
using Rainbow.Common;
using Rainbow.Common.Enums;


namespace Rainbow.ViewModels.ClientModules
{
	/// <summary>
    ///     查询客户端模块
    /// </summary>
    [Display(Name = "查询客户端模块")]
	[BindModel("ClientModule", VMType.Query)]
    public class QueryClientModuleVM : PagingQueryOption
    {
		
        /// <summary>
        ///     模块名称
        /// </summary>
        [Display(Name = "模块名称")]
        [QueryColumn("Name", CompareEnum.Like)]
        public String Name {get;set;}

        /// <summary>
        ///     模块描述
        /// </summary>
        [Display(Name = "模块描述")]
        [DataType(DataType.Html)]
        [QueryColumn("Description", CompareEnum.Like)]
        public String Description {get;set;}

    }
}
