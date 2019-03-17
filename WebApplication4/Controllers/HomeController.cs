using System;
using System.Diagnostics;
using System.Threading.Tasks;
using BMI.Service.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if(!ModelState.IsValid)
            {
                return null;
            }
            if (file == null || file.Length == 0)
            {
                return Content("file not selected");
            }

            var command = new ImportBmiRecordsCommand(file);

            var result = await _mediator.Send(command);

            if (!result)
            {
                throw new InvalidOperationException("Upload file was not successful");
            }

            return RedirectToAction("Index");
        }

    }
}
