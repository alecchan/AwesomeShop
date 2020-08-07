using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AwesomeShop.WebApp.Models;
using AwesomeShop.AzureQueueLibrary.QueueConnection;
using System.ComponentModel.DataAnnotations;
using AwesomeShop.AzureQueueLibrary.Messages;

namespace AwesomeShop.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQueueCommunicator _queueCommunicator;

        public HomeController(ILogger<HomeController> logger, IQueueCommunicator queueCommunicator)
        {
            _logger = logger;
            this._queueCommunicator = queueCommunicator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactUs(string contactName, string emailAddress)
        {
            // Send thank you email to contact
            var thankYouEmail = new SendEmailCommand()
            {
                To = emailAddress,
                Subject = "Thank you for reaching about",
                Body = "We will contact you shortly",
            };

            await _queueCommunicator.SendAsync(thankYouEmail);

            // Send new contact email to admin
            var adminEmail = new SendEmailCommand
            {
                To = "ach2017@zoho.eu",
                Subject = "We have a new contact",
                Body = $"{contactName} has reached out view the contact form. Please repond back at {emailAddress}"
            };

            await _queueCommunicator.SendAsync(adminEmail);


            ViewBag.Message = "Thank you we can received your message ^__^";

            return View();
        }
    }
}
