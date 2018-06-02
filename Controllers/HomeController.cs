using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wedding_Planner.Models;

namespace Wedding_Planner.Controllers
{
    public class HomeController : Controller
    {
        private WeddingsContext _context;
    
        public HomeController(WeddingsContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {

            return View("Index");
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(UserViewModel model, string Email)
        {
            if(ModelState.IsValid)
            {
                User newUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                };  

                _context.users.Add(newUser);
                _context.SaveChanges();
                User user2 = _context.users.SingleOrDefault(user => user.Email == Email);
                HttpContext.Session.SetInt32("userId", user2.UserId);
                HttpContext.Session.SetString("userName", user2.FirstName);
                return RedirectToAction("Dashboard");
            }
            return View("Index");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string Email, string Password)
        {

            User user1 = _context.users.SingleOrDefault(user => user.Email == Email);
            if(user1 != null && user1.Password == Password)
            {
                HttpContext.Session.SetString("userName", user1.FirstName);
                HttpContext.Session.SetInt32("userId", user1.UserId);
                return RedirectToAction("Dashboard");
            }
            return View("Index");
        }

        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Home")]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("userId") != null)
            {
            List<Wedding> AllWeddings = _context.weddings.OrderByDescending(wed => wed.WeddDate)
                                        .Include(u => u.Attendees).ToList();
            @ViewBag.weddings = AllWeddings;
            @ViewBag.user= HttpContext.Session.GetInt32("userId");
            @ViewBag.userName = HttpContext.Session.GetString("userName");
            return View("Dashboard");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("NewWedding")]
        public IActionResult NewWedding()
        {
            return View("WedAdder");
        }

        [HttpPost]
        [Route("AddWedding")]
        public IActionResult AddWeddingToDb(WeddingViewModel model)
        {
            if(ModelState.IsValid)
            {
                Wedding newWedding = new Wedding
                {
                    WedderOne = model.WedderOne,
                    WedderTwo = model.WedderTwo,
                    WeddDate = model.WeddDate,
                    WedAddress = model.WedAddress,
                    CreatorId = HttpContext.Session.GetInt32("userId").Value
                };  
                _context.weddings.Add(newWedding);
                _context.SaveChanges();
                var weId = newWedding.WeddingId;
                return RedirectToAction("ShowWedding", new {WedId =weId});
            }
            return View("WedAdder");
        }

        [HttpGet]
        [Route("ShowWedding/{WedId}")]
        public IActionResult ShowWedding(int WedId)
        {
            Wedding showWed = _context.weddings.Include(x => x.Attendees).ThenInclude(u => u.User)
                                .Where(wed => wed.WeddingId == WedId).SingleOrDefault();
            ViewBag.showWed = showWed;
            return View("ShowWedding");
        }

        [HttpGet]
        [Route("RSVP/{WedId}")]
        public IActionResult RSVP(int WedId)
        {
            GuestList newGuest = new GuestList
            {
                UserId = HttpContext.Session.GetInt32("userId").Value,
                WeddingId = WedId
            };
            _context.users_weddings.Add(newGuest);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("Un-RSVP/{wId}")]
        public IActionResult Un_RSVP(int wId)
        {
            int useId = HttpContext.Session.GetInt32("userId").Value;
            GuestList guest = _context.users_weddings.SingleOrDefault(rev => (rev.UserId == useId && rev.WeddingId == wId));
            _context.users_weddings.Remove(guest);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("Delete/{wedId}")]
        public IActionResult RemoveReviewFromDb(int wedId)
        {   
            Wedding RetrievedWedding = _context.weddings.SingleOrDefault(rev => rev.WeddingId == wedId);
            _context.weddings.Remove(RetrievedWedding);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");

        }
    }
}
