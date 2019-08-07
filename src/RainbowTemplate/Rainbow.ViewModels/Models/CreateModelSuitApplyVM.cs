using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rainbow.ViewModels.Models
{
    public class CreateModelSuitApplyVM
    {
        /// <summary>
        ///     Model名称
        /// </summary>
        [Display(Name = "Model名称")]
        public string ModelName { get; set; }
        /// <summary>
        ///     Model全名
        /// </summary>
        [Display(Name = "Model全名")]
        public string ModelFullName { get; set; }
        /// <summary>
        ///     目录名称
        /// </summary>
        [Display(Name = "目录名称")]
        public string FolderName { get; set; }
        /// <summary>
        ///     生成删除VM
        /// </summary>
        [Display(Name = "生成删除VM")]
        public bool EnableDelete { get; set; }
        /// <summary>
        ///     生成服务
        /// </summary>
        [Display(Name = "生成服务")]
        public bool GenerateService { get; set; }
        /// <summary>
        ///     生成Controller
        /// </summary>
        [Display(Name = "生成Controller")]
        public bool GenerateController { get; set; }
        /// <summary>
        ///     生成Angular组件页面
        /// </summary>
        [Display(Name = "生成Angular组件页面")]
        public bool GenerateNgModuleComponent { get; set; }
        public bool UpdateTsServices { get; set; }
        public List<CreateViewModelApplyVM> Items { get; set; }
    }
}