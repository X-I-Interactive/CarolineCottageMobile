using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CarolineCottageMobile.Application.CarolineCottageDisplay
{
    public class CarouselDisplay
    {
        public string ImagePath { get; set; }
        public CarouselType CarouselType { get; set; }
        public List<ImageDisplay> ImageList { get; set; }
        public CarouselDisplay()
        {
            ImageList = new List<ImageDisplay>();
        }

        public void GetImageDisplayList(string path)
        {
            List<ImageDisplay> imageList = new List<ImageDisplay>();
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (var file in di.GetFiles("*.jp*"))
            {
                imageList.Add(new ImageDisplay { ImageName = file.Name });
            }
            ImageList = imageList;
        }
    }
    public class ImageDisplay
    {
        public string ImageName { get; set; }

    }


    public enum CarouselType
    {
        Heading,
        Cottage,
        Mousehole,
        PlacesToVisit
    }
}