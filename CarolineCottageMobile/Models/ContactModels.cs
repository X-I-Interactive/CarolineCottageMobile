using CarolineCottage.Repository.CarolineCottageClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarolineCottageMobile.Models
{
    public class ContactUsData
    {
        [Required(ErrorMessage = "Your email address is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter a valid email address")]
        [Display(Name = "Your email address")]
        public string From { get; set; }
        public MessageType MessageType { get; set; }

        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter a message")]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Display(Name = "Any comments/questions")]
        public string Comment { get; set; }

        public int BookingWeekID { get; set; }
        public string WeekDate { get; set; }

        public BookingStatus BookingStatus { get; set; }
    }

    public enum MessageType
    {
        Contact,
        Enquiry
    }
}