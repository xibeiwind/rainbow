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
        public string Name { get; set; }

        /// <summary>
        ///     Title
        /// </summary>
        [Display(Name = "Title")]
        [QueryColumn("Title", CompareEnum.Like)]
        public string Title { get; set; }

        /// <summary>
        ///     扩展样式
        /// </summary>
        [Display(Name = "扩展样式")]
        public bool? IsCustomLayout { get; set; }

        /// <summary>
        ///     路由路径
        /// </summary>
        [Display(Name = "路由路径")]
        [QueryColumn("Path", CompareEnum.Like)]
        public string Path { get; set; }

    }
}
