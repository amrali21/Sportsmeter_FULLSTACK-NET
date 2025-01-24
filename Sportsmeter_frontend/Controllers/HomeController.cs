using AutoMapper;
using CRUD_Design.Contracts;
using CRUD_Design.Models.DBModel;
using CRUD_Design.Models.DTO.RunInfo;
using CRUD_Design.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sportsmeter_frontend.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Sportsmeter_frontend.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRunInfoRepository _runInfoRepository;
        private readonly IMapper _mapper;

        private readonly UserRepository _userRepository;

        private Claim? UserID => User.Claims.FirstOrDefault(c => c.Type == "uid");
        
        public HomeController(IRunInfoRepository runInfoRepository, UserRepository userRepository, IMapper mapper)
        {
            _runInfoRepository = runInfoRepository;
            _userRepository = userRepository;
            
            _mapper = mapper;
        }

        //[Authorize]
        public IActionResult Index()
        {
            return RedirectToAction("Search");
        }

        public IActionResult Search([Bind("dateFrom,dateTo,distance,time")] DateSearchDTO dateSearchDTO)
        {
            if (UserID == null)
                return RedirectToAction("Report");

            ViewBag.message = TempData["message"];
            return View("Search");
        }

        public async Task<IActionResult> Entries([Bind("dateFrom,dateTo,distance,time")] DateSearchDTO dateSearchDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }


            if (!DateTime.TryParse(dateSearchDTO.dateFrom, out DateTime dateFrom))
                return BadRequest();

            DateTime.TryParse(dateSearchDTO.dateTo, out DateTime dateTo);


            List<RunInfo> runInfos = await _runInfoRepository.GetAsync(t => 
                t.ApplicationUserId == UserID.Value.ToString() 
                && (dateSearchDTO.distance == null || t.Distance == dateSearchDTO.distance)
                && (dateSearchDTO.time == null || t.Time == dateSearchDTO.time)
                && t.Date >= dateFrom
                && (dateSearchDTO.dateTo == null || t.Date <= dateTo));

            if (!runInfos.Any())
            {
                TempData["message"] = "No Records found";
                return RedirectToAction("Search", dateSearchDTO);
                //return Search(dateSearchDTO);//RedirectToAction("Search"); /*View("Search")*/
            }

            return View(runInfos);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddItem([Bind("Distance,Time,Date")]CreateRunInfoDTO runInfoDTO)
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            RunInfo runInfo = new()
            {
                Distance = runInfoDTO.Distance,
                Date = runInfoDTO.Date,
                Time = runInfoDTO.Time,
                ApplicationUserId = UserID.Value.ToString()
            };

            try
            {
                await _runInfoRepository.AddAsync(runInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int Id)
        {
            RunInfo runINfo = await _runInfoRepository.GetAsync(Id);

            var test = runINfo.Date.ToString("yyyy-MM-dd");
            return View(runINfo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Distance,Time,Date,ApplicationUserId")] UpdateInfoDTO d)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();       
            }

            try
            {
                await _runInfoRepository.UpdateAsync(_mapper.Map<RunInfo>(d));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await _runInfoRepository.DeleteAsync(Id);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Report()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = "Please login first to see or add records." });
        }
    }
}