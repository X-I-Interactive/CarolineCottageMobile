using CarolineCottage.Repository.CarolineCottageClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace CarolineCottageMobile.Models
{
    public class ContactUs 
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
        public bool SendSuccess { get; set; }
        public string ErrorMessage { get; set;  }

        public string SendMessage(string cCM, string account)
        {
            string sender = account + "@pugwash.com";
            string returnMessage = "";
            this.Message += "\n\nFrom\n\n" + this.From;
            MailMessage mail = new MailMessage(
                sender, //  from
                sender, //   to
                this.Subject,
                this.Message);
            mail.ReplyToList.Add(this.From);

            try
            {
                using (SmtpClient smtpClient = new SmtpClient("SMTP.Livemail.co.uk"))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(sender, cCM);
                    smtpClient.Port = 587;
                    smtpClient.Send(mail);
                }
            }
            catch (Exception e)
            {

                return  $"Error sending: 1: {e.Message}";
            }

            return returnMessage;
        }
    }

    public enum MessageType
    {
        Contact,
        Enquiry
    }
}