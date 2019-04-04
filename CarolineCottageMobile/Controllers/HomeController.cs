using CarolineCottageMobile.Application.CarolineCottageDisplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarolineCottageMobile.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Dictionary<CarouselType, CarouselDisplay> carousels = new Dictionary<CarouselType, CarouselDisplay>();
            string path = Server.MapPath("~/Content/CarouselMain");
            CarouselDisplay carouselDisplay = new CarouselDisplay();
            carouselDisplay.ImagePath = "~/Content/ImagesCarousel";
            carouselDisplay.GetImageDisplayList(path);
            carousels.Add(CarouselType.Heading, carouselDisplay);

            return View(carousels);
        }        
    }
}