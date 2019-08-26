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

        public bool GenerateVM { get; set; }

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
        ///     Controller项目名称
        /// </summary>
        [Display(Name = "Controller项目名称")]
        public string ControllerProjectName { get; set; }

        /// <summary>
        ///     生成Angular组件页面
        /// </summary>
        [Display(Name = "生成Angular组件页面")]
        public bool GenerateNgModuleComponent { get; set; }
        /// <summary>
        ///     所采用NgModule的名称
        /// </summary>
        [Display(Name = "所采用NgModule的名称")]
        public string NgModuleName { get; set; }
        /// <summary>
        ///     Angular组件为List组件
        /// </summary>
        [Display(Name = "Angular组件为List组件")]
        public bool IsNgModelListComponent { get; set; }
        /// <summary>
        ///     是否更新呢生成TsService
        /// </summary>
        [Display(Name = "是否更新呢生成TsService")]
        public bool UpdateTsServices { get; set; }
        public List<CreateViewModelApplyVM> Items { get; set; }
    }
}