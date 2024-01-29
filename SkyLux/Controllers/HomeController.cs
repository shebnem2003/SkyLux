using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SkyLux.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace SkyLux.Controllers
{
    public class HomeController : Controller
    {
        private readonly AirlineContext _sql;

        public char ArrivalTime { get; private set; }
        public char DepartureDate { get; private set; }

        public HomeController(AirlineContext sql)
        {
            _sql = sql;
        }



        public IActionResult Index()
        {
            var a = _sql.Positions.ToList();
            return View(a);
        }

      

        public IActionResult News()
        {
            var a = _sql.News.ToList();
            return View(a);
        }

        [Authorize]

        public IActionResult Ticket()
        {
            ViewBag.Economies =new SelectList( _sql.Economies.ToList(), "EconomyId", "EconomyName");
            //TicketModel ticketModel = new TicketModel();
            //ticketModel.Economies = _sql.Economies.ToList();
            return View();
        }

        [Authorize]
        public IActionResult TicketData(string from,string to,int catId,DateTime fromDate,DateTime toDate)
        {
            var tickets = _sql.Tickets.Include(x => x.TicketEconomy).Where(x => x.TicketFrom.Contains(from) && x.TicketTo.Contains(to) && x.TicketEconomyId == catId && x.TicketDepartureDate>=fromDate&&x.TicketArrivalDate<=toDate).ToList();
            return Ok(tickets);
        }

        [Authorize]
        public IActionResult Payment()
        {
            var tickets = _sql.Baskets.Include(x => x.BasketTicket).Where(x => x.BasketUserId == int.Parse(User.FindFirstValue(ClaimTypes.Sid))).ToList();
            return View(tickets);
        }

        [Authorize]
        public IActionResult Basket()
        {
            var b = _sql.Baskets.Include(x => x.BasketTicket).Where(x => x.BasketUserId == int.Parse(User.FindFirstValue(ClaimTypes.Sid))).ToList();
            return View(b);
        }

        [Authorize]
        public IActionResult BasketData(int id)
        {
            Basket basket = new Basket();
            basket.BasketTicketId = id;
            basket.BasketUserId = int.Parse(User.FindFirstValue(ClaimTypes.Sid));
            _sql.Baskets.Add(basket);
            _sql.SaveChanges();
            return RedirectToAction("Basket", "Home");
        }

        public IActionResult BasketRemove(int id) 
        {
            var a = _sql.Baskets.SingleOrDefault(x => x.BasketId== id);
            _sql.Baskets.Remove(a);
            _sql.SaveChanges();
            return RedirectToAction("Basket","Home");
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