//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TransactionMonitoring.Logger;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

namespace TransactionMonitoring.API.Controllers
{

    public class BaseController : ControllerBase
    {
        protected readonly ILoggerHelper _logger;

        public BaseController()
        {
               _logger = new LoggerHelper();
        }
    }
}
