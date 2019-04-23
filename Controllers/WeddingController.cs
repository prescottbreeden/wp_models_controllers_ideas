using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
    public class WeddingController : Controller
    {
        private WeddingContext dbContext;
        private int UserId;
        private bool AuthenticatedUser
        {
            get {
                int? userId = HttpContext.Session.GetInt32("UserId");
                if (userId is null) return false;
                UserId = (int)userId;
                return true;               
            }
        }

        public WeddingController(WeddingContext context)
        {
            dbContext = context;
        }


        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            if (AuthenticatedUser)
            {
                User currentUser = dbContext.Users
                    .FirstOrDefault(u => u.UserId == UserId);

                List<Wedding> allWeddings = dbContext.Wedding
                    .Include(w => w.RSVPs)
                    .ThenInclude(r => r.User)
                    .ToList();

                Dashboard model = new Dashboard()
                {
                    AllWeddings = allWeddings,
                    CurrentUser = currentUser
                };
                return View("Dashboard", model);
            }
            return RedirectToAction("LoginPage", "Login");
        }


        [HttpGet("addwedding")]
        public IActionResult AddWedding()
        {
            if (AuthenticatedUser)
                return View("AddWedding");

            return RedirectToAction("LoginPage", "Login");
        }


        [HttpPost("addthewedding")]
        public IActionResult AddTheWedding(NewWedding wedding)
        {
            if (AuthenticatedUser)
            {
                if (ModelState.IsValid)
                {
                    dbContext.CreateWedding(wedding, UserId);
                    return RedirectToAction("Dashboard");
                }
                else
                    return View("AddWedding");
            }
            return RedirectToAction("LoginPage", "Login");

        }


        [HttpGet("viewwedding/{id}")]
        public IActionResult ViewWedding(int id)
        {
            if (AuthenticatedUser)
            {
                Wedding querywedding = dbContext.Wedding
                    .Include(q => q.RSVPs)
                    .ThenInclude(u => u.User)
                    .FirstOrDefault(w => w.WeddingId == id);

                return View("ViewWedding", querywedding);
            }
            return RedirectToAction("LoginPage", "Login");
        }


        [HttpGet("delete/{id}")]
        public IActionResult DeleteWedding(int id)
        {
            if (AuthenticatedUser)
            {
                Wedding querywedding = dbContext.Wedding
                    .FirstOrDefault(w => w.WeddingId == id);

                if (UserId == querywedding.UserId)
                {
                    dbContext.Remove(querywedding);
                    dbContext.SaveChanges();
                }
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("LoginPage", "Login");
        }


        [HttpGet("rsvp/{id}")]
        public IActionResult RSVPWedding(int id)
        {
            if (AuthenticatedUser)
            {
                Reservation newRSVP = new Reservation()
                {
                    UserId = UserId,
                    WeddingId = id
                };
                dbContext.Reservations.Add(newRSVP);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("LoginPage", "Login");
            
        }


        [HttpGet("unrsvp/{id}")]
        public IActionResult UNRSVPWedding(int id)
        {
            if (AuthenticatedUser)
            {
                Reservation rsvp = dbContext.Reservations
                    .FirstOrDefault(r => r.UserId == UserId);

                dbContext.Reservations.Remove(rsvp);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return RedirectToAction("LoginPage", "Login");
        }
    }
}
