using Microsoft.AspNetCore.Mvc;
using Rainbow.Services;
using Rainbow.ViewModels.Messages;
using System;
using System.Threading.Tasks;
using Yunyong.Core;
using Controller = Yunyong.Mvc.Controller;

namespace Rainbow.Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MessageController : Controller
    {
        private IMessageService Service { get; }

        public MessageController(IMessageService service)
        {
            Service = service;
        }

        protected override void Controller_BeforeAction()
        {
            Service.UserId = this.GetUserId();
        }

        [Route("Query")]
        [HttpGet]
        [ProducesDefaultResponseType(typeof(PagingList<MessageVM>))]
        public async Task<PagingList<MessageVM>> QueryAsync([FromQuery] MessageQueryOption option)
        {
            return await Service.QueryAsync(option);
        }

        [Route("Get/{msgId}")]
        [HttpGet]
        [ProducesDefaultResponseType(typeof(MessageVM))]
        public async Task<MessageVM> GetAsync(Guid msgId)
        {
            return await Service.GetAsync(msgId);
        }

        [HttpPut]
        [Route("Readed")]
        [ProducesDefaultResponseType(typeof(AsyncTaskResult))]
        public async Task<AsyncTaskResult> ReadedAsync(Guid msgId)
        {
            return await Service.ReadedAsync(msgId);
        }
    }
}