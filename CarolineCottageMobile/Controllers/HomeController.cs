using CarolineCottageMobile.Application.CarolineCottageDisplay;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using CarolineCottage.Domain;
using CarolineCottage.Domain.CarolineCottageRepository;
using CarolineCottage.Repository.CarolineCottageDatabase;
using CarolineCottageMobile.Models;
using System.Globalization;

namespace CarolineCottageMobile.Controllers
{
    public class HomeController : Controller
    {
        private CarolineCottageRepository _carolineCottageRepository;
        private CarolineCottageDbContext _dbContext;

        public HomeController()
        {
            _dbContext = new CarolineCottageDbContext(ConfigurationManager.ConnectionStrings["CCConnectionString"].ConnectionString);
            _carolineCottageRepository = new CarolineCottageRepository(_dbContext);
        }
        public ActionResult Index()
        {
            Dictionary<CarouselType, CarouselDisplay> carousels = new Dictionary<CarouselType, CarouselDisplay>();
            carousels.Add(CarouselType.Heading, GetCarouselSettings("~/Content/CarouselMain", CarouselType.Heading));
            carousels.Add(CarouselType.Cottage, GetCarouselSettings("~/Content/CarouselCottage", CarouselType.Cottage));
            carousels.Add(CarouselType.Mousehole, GetCarouselSettings("~/Content/CarouselMousehole", CarouselType.Mousehole));
            carousels.Add(CarouselType.PlacesToVisit, GetCarouselSettings("~/Content/CarouselPlacesToVisit", CarouselType.PlacesToVisit));

            return View(carousels);
        }

        [HttpPost]
        public ActionResult PrivacyStatement()
        {
            return PartialView("PrivacyStatement");
        }

        [HttpPost]
        public ActionResult CalendarList()
        {
            string endDateForDisplay = WebConfigurationManager.AppSettings["EndDateForDisplay"];            
            DateTime endDate = Convert.ToDateTime(endDateForDisplay, new CultureInfo("en-GB"));
            bool debugSQLConnection = Convert.ToBoolean(WebConfigurationManager.AppSettings["DebugSQLConnection"]);
           //   load booking view

            BookingReturn bookings = _carolineCottageRepository.GetCurrentBookings(false, endDate, debugSQLConnection);
            
            return PartialView("CalendarList", bookings);
        }

        [HttpPost]
        public ActionResult ContactUs()
        {
            return PartialView("ContactUsForm");
        }

        [HttpPost]
        public ActionResult ContactUsMessage(ContactUsData contactUsData)
        {
            return Json(new { replyText = "OK" });
        }

        private CarouselDisplay GetCarouselSettings(string location, CarouselType carouselType)
        {
            string path = Server.MapPath(location);
            CarouselDisplay carouselDisplay = new CarouselDisplay();
            carouselDisplay.ImagePath = location;
            carouselDisplay.CarouselType = carouselType;
            carouselDisplay.GetImageDisplayList(path);
            return carouselDisplay;
        }
    }
}