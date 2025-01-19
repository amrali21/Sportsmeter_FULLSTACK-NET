using AutoMapper;
using CRUD_Design.Contracts;
using CRUD_Design.Models.DBModel;
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
        public async Task<IActionResult> Index()
        {
            if (UserID == null)
                return RedirectToAction("Report");

            List<RunInfo> runInfos = await _runInfoRepository.GetAsync(t => t.ApplicationUserId == UserID.Value.ToString());
            
            ViewBag.RunInfo = runInfos;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([Bind("Distance,Time,Date")]CreateRunInfoDTO runInfoDTO)
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