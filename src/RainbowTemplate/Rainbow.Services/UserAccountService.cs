using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rainbow.Common;
using Rainbow.Services.Abstractions;
using Rainbow.ViewModels.Users;
using Yunyong.Core;
using Yunyong.EventBus;

namespace Rainbow.Services
{
    public class UserAccountService : ServiceBase, IUserAccountService
    {
        public UserAccountService(ConnectionSettings connectionSettings, IConnectionFactory connectionFactory, ILoggerFactory loggerFactory, IEventBus eventBus)
            : base(connectionSettings, connectionFactory, loggerFactory, eventBus)
        {
        }

        public async Task<AsyncTaskTResult<UserVM>> RegisterAsync(RegisterUserVM vm)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResultVM> LoginAsync(LoginVM vm)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResultVM> SmsLoginAsync(SmsLoginVM vm)
        {
            throw new NotImplementedException();
        }

        public async Task<AsyncTaskResult> SendLoginSmsAsync(SendLoginSmsVM vm)
        {
            throw new NotImplementedException();
        }

        public async Task<AsyncTaskResult> ForgetPasswordAsync(ForgetPasswordVM vm)
        {
            throw new NotImplementedException();
        }

        public async Task<UserVM> GetUserAsync(Guid userId)
        {
            return new UserVM
            {

                Id = userId,
                Name = "Alex Chen",
                AvatarUrl = "http://qcloud.dpfile.com/pc/e8OoMdPk5qBpAfBjwfwF40kly4WCkN1W3WuOTSVCiN5oiBktvQII5H1EHxzyMYbmTYGVDmosZWTLal1WbWRW3A.jpg"
            };
            throw new NotImplementedException();
        }

        public async Task<AsyncTaskTResult<string>> GetUserAvatarUrlAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}