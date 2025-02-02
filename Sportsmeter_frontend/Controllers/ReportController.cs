using CRUD_Design.Contracts;
using CRUD_Design.Models.DBModel;
using CRUD_Design.Models.DTO.Reports;
using Microsoft.AspNetCore.Mvc;
using Sportsmeter_frontend.Models;
using System.Security.Claims;

namespace Sportsmeter_frontend.Controllers
{
    public class ReportController : Controller
    {
        private readonly IRunInfoRepository _runInfoRepository;
        private Claim? UserID => User.Claims.FirstOrDefault(c => c.Type == "uid");

        public ReportController(IRunInfoRepository runInfoRepository) {
            _runInfoRepository = runInfoRepository;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Weekly");
        }

        public async Task<IActionResult> Weekly()
        {
            List<RunInfo> all = await _runInfoRepository.GetAllAsync(); // data needs to come filtered by user and select only relevant columns
            List<WeeklyReport> weeklyReports = new();

            if (all.Count == 0)
                return View("Index", weeklyReports);

            DateTime mostRecent = all.Select(r => r.Date).Max();
            DateTime oldest = all.Select(all => all.Date).Min();

            DateTime tempDate = mostRecent;  // 21/01/2025

            while (tempDate > oldest.AddDays(-7))         //ignore: check condition for when there is like 5 day difference
            {
                // get avg of this week
                DateTime dateTo = tempDate;               //ignore: 1) 21/01/2025      2) 13/01/2025 (this needs to be 13 instead)       
                DateTime dateFrom = tempDate.AddDays(-7); //ignore  1) 14/01/2025      2) 07/01/2025
                tempDate = dateFrom.AddDays(-1); // ignore: is there a way to avoid this?

                float avgDist = 0;
                float avgTime = 0;

                Func<RunInfo, bool> query = r => r.Date >= dateFrom && r.Date <= dateTo && r.ApplicationUserId == UserID.Value.ToString();

                if (!all.Any(query))
                    continue;

                avgDist = all.Where(query)
                    .Select(r => r.Time).Average();

                avgTime = all.Where(query)
                    .Select(r => r.Distance).Average();


                weeklyReports.Add(
                    new WeeklyReport
                    {
                        from = dateTo,
                        to = dateFrom,
                        avgDist = avgDist,
                        avgTime = avgTime
                    });
            }

            return View("Index", weeklyReports);
        }
    }
}
