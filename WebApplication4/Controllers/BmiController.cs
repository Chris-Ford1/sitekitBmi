using System;
using System.Threading.Tasks;
using BMI.Service.Queries;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class BmiController : Controller
    {

        private readonly IBmiQueries _bmiQueries;

        public BmiController(IBmiQueries bmiQueries)
        {

            _bmiQueries = bmiQueries ?? throw new ArgumentNullException(nameof(bmiQueries));
        }

        public async Task<IActionResult> Index()
        {
            var data = await _bmiQueries.GetAggregatedRecords();
            var vm = new BmiAggregationViewModel(data);
            return View(vm);
        }
    }
}
