﻿using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class NotLoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
