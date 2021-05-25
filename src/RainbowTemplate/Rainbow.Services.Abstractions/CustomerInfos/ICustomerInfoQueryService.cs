using Rainbow.ViewModels.CustomerInfos;
using System;
using System.Threading.Tasks;

namespace Rainbow.Services.CustomerInfos
{
    public interface ICustomerInfoQueryService
    {
        Guid CustomerId { get; set; }
        Task<CustomerInfoVM> GetAsync();
    }
}