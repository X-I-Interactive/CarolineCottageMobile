using CarolineCottageMobile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

    public class Email
    {
        public string Send(ContactUsData   contact)
        {
            string returnMessage = "";
            MailMessage mail = new MailMessage(
                "caroline@pugwash.com",
                contact.From ?? "caroline@pugwash.com",
                contact.Subject,
                contact.Message + "\n\nFrom\n\n" + contact.From);
            mail.ReplyToList.Add(contact.From);

            try
            {
                using (SmtpClient smtpClient = new SmtpClient("SMTP.Livemail.co.uk"))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("caroline@pugwash.com", "ARD1329");
                    smtpClient.Port = 587;
                    smtpClient.Send(mail);
                }
            }
            catch (Exception e)
            {

                returnMessage = $"Error sending: 1: {e.Message} 2: {e.InnerException.Message}";
            }

            return returnMessage;
        }
    }

    public enum CarouselType
    {
        Heading,
        Cottage,
        Mousehole,
        PlacesToVisit
    }
}