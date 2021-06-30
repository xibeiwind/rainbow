using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Rainbow.Platform.WebAPP.HostingStartups;
using Rainbow.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

[assembly: HostingStartup(typeof(EnumDisplayStartup))]

namespace Rainbow.Platform.WebAPP.HostingStartups
{
    public class EnumDisplayStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dic = new Dictionary<DataType, string>
                {
                    {DataType.DateTime, "日期时间"},
                    {DataType.Date,"日期" },
                    {DataType.Time,"时间" },
                    {DataType.Duration,"日期间隔" },
                    {DataType.PhoneNumber,"电话号码" },
                    {DataType.Currency,"货币" },
                    {DataType.Text,"文本" },
                    {DataType.Html,"HTML" },
                    {DataType.MultilineText,"多行文本" },
                    {DataType.EmailAddress,"邮箱地址" },
                    {DataType.Password,"密码" },
                    {DataType.Url,"网址" },
                    {DataType.ImageUrl,"图片地址" },
                    {DataType.CreditCard,"信用卡" },
                    {DataType.PostalCode,"邮政编码" },
                    {DataType.Upload,"文件上传" },

                };

                var queryService = new EnumDisplayQueryService();
                queryService.Register(dic);
                services.AddSingleton<IEnumDisplayQueryService>(queryService);
            });
        }
    }
}