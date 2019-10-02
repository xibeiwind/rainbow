using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rainbow.ViewModels.CustomerInfos;

namespace Rainbow.Services.CustomerInfos
{
    public interface ICustomerInfoQueryService
    {
        Guid CustomerId { get; set; }
        Task<CustomerInfoVM> GetAsync();
    }
}
