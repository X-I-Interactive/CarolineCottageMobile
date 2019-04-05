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
            carousels.Add(CarouselType.Heading, GetCarouselSettings("~/Content/CarouselMain", CarouselType.Heading));
            carousels.Add(CarouselType.Cottage, GetCarouselSettings("~/Content/CarouselCottage", CarouselType.Cottage));
            carousels.Add(CarouselType.Mousehole, GetCarouselSettings("~/Content/CarouselMousehole", CarouselType.Mousehole));
            carousels.Add(CarouselType.PlacesToVisit, GetCarouselSettings("~/Content/CarouselPlacesToVisit", CarouselType.PlacesToVisit));

            return View(carousels);
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