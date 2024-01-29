using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkyLux.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace SkyLux.Controllers
{

    [Authorize(Roles = "Şəbnəm")]

    public class AdminController : Controller
    {
            private readonly AirlineContext _context;

            public AdminController(AirlineContext context)
            {
                _context = context;
            }

        public IActionResult AdminPanel(string? search = "")
        {
            var b = _context.Tickets.Include(x => x.TicketEconomy).Where(x => x.TicketFrom.Contains(search)).ToList();
            return View(b);
        }


        public IActionResult NewsPanel()
        {
            var b = _context.News.ToList();
            return View(b);
        }


        public IActionResult Add()
        {
            Random random = new Random();
            var r = random.Next(100000000, 900000000);
;           TempData["Token"] = DateTime.Now.ToString();
            ViewBag.Economy = new SelectList(_context.Economies.ToList(), "EconomyId", "EconomyName");
            return View();
        }

        [HttpPost]

        public IActionResult Add(Ticket ticket,string TicketToken)
        {
            if (TicketToken == TempData["PostToken"].ToString())
            {
                ticket.TicketStatus = "Deactive";
                _context.Tickets.Add(ticket);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Remove(int id)
        {
            var a = _context.Tickets.SingleOrDefault(x => x.TicketId == id);
            _context.Tickets.Remove(a);
            _context.SaveChanges();
            return RedirectToAction("Ticket", "Home");
        }


        public IActionResult NewsRemove(int id)
        {
            var a = _context.News.SingleOrDefault(x => x.NewsId == id);
            _context.News.Remove(a);
            _context.SaveChanges();
            return RedirectToAction("News", "Home");
        }


        public IActionResult AddNews()
        {
            Random random = new Random();
            var r = random.Next(100000000, 900000000);
            ; TempData["Token"] = DateTime.Now.ToString();
            return View();
        }

        [HttpPost]

        public IActionResult AddNews(News news, IFormFile Sekil,string NewsToken)
        {
            if (Sekil != null)
            {
                string random = Path.GetRandomFileName() + Path.GetExtension(Sekil.FileName);
                news.NewsImage = random;
                using (FileStream s = new FileStream("wwwroot/img/" + random, FileMode.Create))
                {
                    Sekil.CopyTo(s);
                }
            }
            if (NewsToken == TempData["PostToken"].ToString())
            {
                _context.News.Add(news);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Active(int id)
        {
            if (_context.Tickets.SingleOrDefault(x => x.TicketId == id).TicketStatus == "active")
            {
                _context.Tickets.SingleOrDefault(x => x.TicketId == id).TicketStatus = "Deactive";

            }
            else
            {
                _context.Tickets.SingleOrDefault(x => x.TicketId == id).TicketStatus = "active";

            }
            _context.SaveChanges();
            return RedirectToAction("AdminPanel", "Admin");
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Economy = new SelectList(_context.Economies.ToList(), "EconomyId", "EconomyName");
            return View(_context.Tickets.SingleOrDefault(x => x.TicketId == id));
        }

        [HttpPost]

        public IActionResult Edit(int id, Ticket ticket)
        {

            var old = _context.Tickets.SingleOrDefault(x => x.TicketId == id);
            old.TicketDepartureDate = ticket.TicketDepartureDate;
            old.TicketFrom = ticket.TicketFrom;
            old.TicketDepartureTime = ticket.TicketDepartureTime;
            old.TicketArrivalTime = ticket.TicketArrivalTime;
            old.TicketTo = ticket.TicketTo;
            old.TicketArrivalDate = ticket.TicketArrivalDate;
            old.TicketHour = ticket.TicketHour;
            old.TicketStatus = ticket.TicketStatus;
            old.TicketEconomy = ticket.TicketEconomy;
            old.TicketPrice = ticket.TicketPrice;
            _context.SaveChanges();
            return RedirectToAction("Ticket", "Home");
        }

        public IActionResult Contact()
        {
            var d = _context.Contacts.ToList();
            return View(d);
        }


        [HttpPost]
        public IActionResult Contact(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Profil(int id)
        {
            var element = _context.Users.Where(x => x.UserId == id).ToList();
            return View(element);
        }


        public IActionResult UserActive(int id)
        {
            if (_context.Users.SingleOrDefault(x => x.UserId == id).UserBlock == "blocked")
            {
                _context.Users.SingleOrDefault(x => x.UserId == id).UserBlock = "unblocked";

            }
            else
            {
                _context.Users.SingleOrDefault(x => x.UserId == id).UserBlock = "blocked";

            }
            _context.SaveChanges();
            return RedirectToAction("UserPanel", "Admin");
        }

        public IActionResult UserPanel(string? search = "")
        {
            var a = _context.Users.Where(x => x.UserName.Contains(search)).ToList();
            return View(a);
        }

        public IActionResult Basket(string? search = "")
        {
            var b = _context.Baskets.Include(x => x.BasketTicket).Include(x=>x.BasketUser).Include(x => x.BasketTicket.TicketEconomy).Where(x => x.BasketTicket.TicketFrom.Contains(search) || x.BasketUser.UserName.Contains(search)).ToList();
            return View(b);
        }

    }
    }
