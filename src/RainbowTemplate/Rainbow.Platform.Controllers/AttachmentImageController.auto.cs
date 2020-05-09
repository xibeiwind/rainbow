using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rainbow.Services.AttachmentImages;
using Rainbow.ViewModels.AttachmentImages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Yunyong.Core;
using Controller = Yunyong.Mvc.Controller;

namespace Rainbow.Platform.Controllers
{
    /// <summary>
    ///     AttachmentImage Controller
    /// </summary>
    [Display(Name = "AttachmentImage Controller")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "CustomerService,SysAdmin")]
    public partial class AttachmentImageController : Controller
    {
        /// <summary>
        ///     AttachmentImage Controller构造函数
        /// </summary>
        public AttachmentImageController(
                IManageAttachmentImageActionService actionService,
                IManageAttachmentImageQueryService queryService
            )
        {
            ActionService = actionService;
            QueryService = queryService;
        }

        private IManageAttachmentImageActionService ActionService { get; }
        private IManageAttachmentImageQueryService QueryService { get; }


        #region Overrides of Controller

        /// <summary>
        /// 
        /// </summary>
        protected override void Controller_BeforeAction()
        {
            base.Controller_BeforeAction();
        }

        #endregion

        /// <summary>
        ///     获取
        /// </summary>
        [Display(Name = "获取")]
        [HttpGet]
        [Route("")]
        [ProducesDefaultResponseType(typeof(AttachmentImageVM))]
        public async Task<AttachmentImageVM> GetAsync(Guid id)
        {
            return await QueryService.GetAsync(id);
        }

        /// <summary>
        ///     查询附件图片列表（分页）
        /// </summary>
        [Display(Name = "查询附件图片列表（分页）")]
        [HttpGet]
        [Route("Query")]
        [ProducesDefaultResponseType(typeof(PagingList<AttachmentImageVM>))]
        public async Task<PagingList<AttachmentImageVM>> QueryAsync([FromQuery] QueryAttachmentImageVM option)
        {
            return await QueryService.QueryAsync(option);
        }

        /// <summary>
        ///     上传图片到云
        /// </summary>
        [Display(Name = "上传图片到云")]
        [HttpPost]
        [Route("UploadPictureFileToCloud")]
        [ProducesDefaultResponseType(typeof(AsyncTaskResult))]
        public async Task<IActionResult> UploadPictureFileToCloudAsync([FromBody] UploadPictureFileToCloudRequestVM vm)
        {
            return Ok(await ActionService.UploadPictureFileToCloudAsync(vm));
        }

        /// <summary>
        ///     上传图片
        /// </summary>
        [Display(Name = "上传图片")]
        [HttpPost]
        [Route("UploadPicture")]
        [ProducesDefaultResponseType(typeof(AsyncTaskTResult<string>))]
        public async Task<IActionResult> UploadPictureAsync([FromForm] UploadPictureRequestVM vm)
        {
            return Ok(await ActionService.UploadPictureAsync(vm));
        }
    }

}
