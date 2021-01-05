using IdentityServer4.Services;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strata.Exchange.IdentityServer.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        private readonly IIdentityServerInteractionService interaction;
        public HomeController(ILogger<HomeController> logger, IIdentityServerInteractionService interaction)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.interaction = interaction ?? throw new ArgumentNullException(nameof(interaction));
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Error(string errorId)
        {
            var exceptionHandlerPathFeature = this.HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature != null && exceptionHandlerPathFeature.Error != null)
            {
                this.logger.LogError(exceptionHandlerPathFeature.Error, exceptionHandlerPathFeature.Error.Message);
            }

            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await this.interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                this.logger.LogError(message.Error, message.ErrorDescription);

                vm.Error = message;
            }

            return this.View(nameof(this.Error), vm);
        }
    }
}
