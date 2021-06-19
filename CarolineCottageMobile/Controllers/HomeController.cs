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
        private string _cCM;
        private string _emailAccount;
        public HomeController()
        {
            _dbContext = new CarolineCottageDbContext(ConfigurationManager.ConnectionStrings["CCConnectionString"].ConnectionString);
            _carolineCottageRepository = new CarolineCottageRepository(_dbContext);
            _cCM = WebConfigurationManager.AppSettings["CCM"];
            _emailAccount = WebConfigurationManager.AppSettings["emac"];
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
        public ActionResult CottageOverview()
        {
            return PartialView("CottageOverview");
        }
        
        [HttpPost]
        public ActionResult CalendarList()
        {
            string endDateForDisplay = WebConfigurationManager.AppSettings["EndDateForDisplay"];            
            DateTime endDate = Convert.ToDateTime(endDateForDisplay, new CultureInfo("en-GB"));
            bool debugSQLConnection = Convert.ToBoolean(WebConfigurationManager.AppSettings["DebugSQLConnection"]);
            //   load booking view

            //BookingReturn bookings = _carolineCottageRepository.GetCurrentBookings(false, endDate, debugSQLConnection);
            BookingReturn bookings = new BookingReturn();


            return PartialView("CalendarList", bookings);
        }
        
        public ActionResult ContactUs()
        {
            return PartialView("ContactUsForm");
        }
                
        public ActionResult EnquiryForm(int weekID)
        {
            Booking booking = _carolineCottageRepository.GetBookingByID(weekID);
            ContactUs contactUsData = SetupEnquiryData(booking);
            
            return PartialView("EnquiryForm", contactUsData);
        }

        [HttpPost]
        public ActionResult EnquiryMessage(ContactUs contactUsData)
        {
            contactUsData.MessageType = MessageType.Enquiry;
            contactUsData.Subject = "Enquiry for week " + contactUsData.WeekDate;
            contactUsData.Message = "Comment: " + contactUsData.Comment;
            string success = contactUsData.SendMessage(_cCM, _emailAccount);
            contactUsData.SendSuccess = success == string.Empty;
            contactUsData.ErrorMessage = success;
            var enquiryResult = this.RenderPartialViewToString("ContactEnquiryResult", contactUsData);
            return Json(new { replyText = "OK", enquiryResult });
        }

        [HttpPost]
        public ActionResult ContactUsMessage(ContactUs contactUsData)
        {
            contactUsData.MessageType = MessageType.Contact;
            contactUsData.Subject = "Caroline Cottage enquiry";            
            string success = contactUsData.SendMessage(_cCM, _emailAccount);
            contactUsData.SendSuccess = success == string.Empty;
            contactUsData.ErrorMessage = success;
            var enquiryResult = this.RenderPartialViewToString("ContactEnquiryResult", contactUsData);
            return Json(new { replyText = "OK", enquiryResult });
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
        private ContactUs SetupEnquiryData(Booking booking)
        {
            ContactUs contactUsData = new ContactUs();
            contactUsData.BookingStatus = booking.BookingStatus;
            contactUsData.BookingWeekID = booking.BookingID;
            contactUsData.WeekDate = booking.WeekStartDate.ToShortDateString();

            return contactUsData;
        }


    }
}