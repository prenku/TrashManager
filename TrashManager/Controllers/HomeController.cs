using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TrashManager.Data;
using TrashManager.Models;

namespace TrashManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository _repository;
        private readonly ApplicationDbContext _dbContext;


        public HomeController(ILogger<HomeController> logger , IRepository repository, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _repository = repository;
            _dbContext = dbContext;
        }

        public  string Index()
        {

            var users = _dbContext.Appointments.ToList();
            string view = string.Empty;

            foreach (var item in users)
            {
                view += $"name:  {item.UserName} , turn : {item.Turn} \n";
            }
            var current = _dbContext.Appointments.Select(app => app).Where(t => t.Turn == true).FirstOrDefault();


            //var turn = userwithturn.Select(t => t.Turn == true);
            var currentTurn = _repository.GetCurrentTurn();
           var userTurn = _repository.CheckForTurn(current);
           var updatedTurn = _repository.CheckNextTurn(current);


            Hangfire.InitializeJobs();

            return  $"this weeks turn is {currentTurn.UserName} , {userTurn} \n Next week turn is : {updatedTurn.UserName}  \n" +  view;
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
    }
}
