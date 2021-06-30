using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rainbow.Services.ModelTypeMetas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yunyong.Mvc;
using Controller = Yunyong.Mvc.Controller;

namespace Rainbow.Platform.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ModelTypeMetaController:Controller
    {
        public ModelTypeMetaController(IModelTypeMetaService service)
        {
            Service = service;
        }

        private IModelTypeMetaService Service { get; }
    }
}
