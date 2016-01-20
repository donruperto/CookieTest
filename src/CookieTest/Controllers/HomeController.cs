using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;

namespace CookieTest.Controllers
{
    public class HomeController : Controller
    {
        private const string CookieName = "rm.clients";

        public IActionResult Index()
        {
            var clients = new List<string> {"client1", "client2"};
            SetClients(clients);

            return View();
        }

        void SetClients(IEnumerable<string> clients)
        {
            string value = null;
            if (clients != null && clients.Any())
            {
                value = JsonConvert.SerializeObject(clients);
            }
            SetCookie(value);
        }

        void SetCookie(string value)
        {
            DateTime? expires = null;
            if (String.IsNullOrWhiteSpace(value))
            {
                var existingValue = GetCookie();
                if (existingValue == null)
                {
                    // no need to write cookie to clear if we don't already have one
                    return;
                }

                value = ".";
                expires = DateTime.Now.AddYears(-1);
            }

            var opts = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                Path = "/",
                Expires = expires
            };

            Response.Cookies.Append(CookieName, value, opts);
        }

        string GetCookie()
        {
            return Request.Cookies[CookieName];
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
        
    }
}
