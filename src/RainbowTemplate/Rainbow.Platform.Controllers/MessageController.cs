using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rainbow.Services.Abstractions;
using Rainbow.ViewModels.Messages;
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
        [ProducesResponseType(typeof(PagingList<MessageVM>), 200)]
        public async Task<PagingList<MessageVM>> QueryAsync([FromQuery] MessageQueryOption option)
        {
            return await Service.QueryAsync(option);
        }

        [Route("Get/{msgId}")]
        [HttpGet]
        [ProducesResponseType(typeof(MessageVM), 200)]
        public async Task<MessageVM> GetAsync(Guid msgId)
        {
            return await Service.GetAsync(msgId);
        }

        [HttpPut]
        [Route("Readed/{msgId}")]
        [ProducesResponseType(typeof(AsyncTaskResult), 200)]
        public async Task<AsyncTaskResult> ReadedAsync(Guid msgId)
        {
            return await Service.ReadedAsync(msgId);
        }
    }
}