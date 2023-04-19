using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AgenziaSpedizioni.Models;
using Microsoft.AspNetCore.Authorization;

namespace AgenziaSpedizioni.Controllers;


public class HomeController : Controller
{



    public IActionResult Index()
    {
        return View();
    }






}

