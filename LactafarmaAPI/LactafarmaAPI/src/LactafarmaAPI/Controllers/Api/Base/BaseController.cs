using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LactafarmaAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LactafarmaAPI.Controllers.Api.Base
{
    [Route("api/[controller]")]
    public class BaseController: Controller
    {
        public readonly IConfigurationRoot Config;
        public readonly ILactafarmaService LactafarmaService;
        public readonly IMailService MailService;

        public BaseController(ILactafarmaService lactafarmaService, IMailService mailService, IConfigurationRoot config)
        {
            LactafarmaService = lactafarmaService;
            MailService = mailService;
            Config = config;
        }
    }
}
