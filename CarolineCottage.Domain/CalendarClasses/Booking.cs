using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarolineCottage.Repository.CarolineCottageClasses;
using CarolineCottage.Repository.CarolineCottageDatabase;
using AutoMapper;

namespace CarolineCottage.Domain
{
    public class BookingReturn
    {
        public List<Booking> BookingList { get; set; }
        public string ReturnError { get; set; }

        public BookingReturn()
        {
            BookingList = new List<Booking>();
        }
    }

    public class Booking
    {
        [Display(Name = "Available for short breaks")]
        public bool AvailableForShortBreaks { get; set; }
        public int BookingID { get; set; }

        [Display(Name = "Booking status")]
        public BookingStatus BookingStatus { get; set; }
        public string Comment { get; set; }

        [Display(Name = "Week's price")]
        [Required(ErrorMessage = "A price for the week is required")]
        [Range(300, 2000, ErrorMessage = "Price must be between 300 and 2000")]
        public int WeekPrice { get; set; }

        public DateTime WeekStartDate { get; set; }

        public bool IsLastRow { get; set; }

        public Booking()
        {

        }

        public Booking(DateTime weekStartDate)
        {
            BookingStatus = BookingStatus.Available;
            WeekStartDate = weekStartDate;
            IsLastRow = false;
            AvailableForShortBreaks = true;
            WeekPrice = 500;
        }

        /*
        public void Save(string connectionString)
        {
            try
            {
                using (CarolineCottageDbContext dbContext = new CarolineCottageDbContext(connectionString))
                {
                    dbContext.Entry(AutoMapper.Mapper.Map<Booking, Booking>(this)).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                //   flag the error to the controller
                BookingID = 0;
            }
        }

        public static Booking GetBookingByID(int bookingID, string connectionString)
        {
            using (CarolineCottageDbContext dbContext = new CarolineCottageDbContext(connectionString))
            {
                DateTime lastDate = dbContext.Bookings.Max(x => x.WeekStartDate);
                var bookingView = AutoMapper.Mapper.Map<Booking, Booking>(dbContext.Bookings.FirstOrDefault(x => x.BookingID == bookingID)) ?? new Booking();
                bookingView.IsLastRow = (lastDate - bookingView.WeekStartDate).Days == 0;
                return bookingView;
            }
        }

        public static BookingReturn GetCurrentBookings(string connectionString, bool addNewRows, DateTime endDateForDisplay, bool debugSQLConnection)
        {
            DateTime nextWeek = DateTimeExtensions.NextDayOfWeek(DateTime.Now, Repository.CarolineCottageClasses.Booking.ChangeoverDay);

            BookingReturn bookingViewReturn = new BookingReturn();

            try
            {
                using (CarolineCottageDbContext dbContext = new CarolineCottageDbContext(connectionString))
                {
                    if (addNewRows)
                    {
                        //  get the latest date in the database
                        DateTime lastWeekStored = dbContext.Bookings.OrderByDescending(x => x.WeekStartDate).FirstOrDefault()?.WeekStartDate ?? nextWeek;

                        //  add in any extra to make up to at least a year's worth
                        DateTime endDate = nextWeek.AddDays(78 * 7);
                        for (DateTime weekDate = lastWeekStored.AddDays(7); (endDate - weekDate).TotalDays > 7; weekDate = weekDate.AddDays(7))
                        {
                            dbContext.Bookings.Add(AutoMapper.Mapper.Map<Booking, Booking>(new Booking(weekDate)));
                        }

                        dbContext.SaveChanges();

                    }
                    //  then get list
                    List<Booking> currentBookings = AutoMapper.Mapper.Map<IEnumerable<Booking>, List<Booking>>(dbContext.Bookings.Where(x => x.WeekStartDate >= nextWeek));

                    if (!addNewRows)
                    {
                        currentBookings = currentBookings.Where(x => x.WeekStartDate < endDateForDisplay).ToList();
                    }

                    currentBookings.Last().IsLastRow = true;
                    bookingViewReturn.BookingList = currentBookings;
                    return bookingViewReturn;
                }
            }
            catch (Exception e)
            {
                bookingViewReturn.ReturnError = e.Message;
                if (!debugSQLConnection)
                {
                    bookingViewReturn.ReturnError = "List nor currently available";
                }
                return bookingViewReturn;
            }


        }

        public static Booking CopyLineToNextLine(int currentBookingID, string connectionString)
        {
            AutoMapper.Mapper.CreateMap<Repository.CarolineCottageClasses.Booking, Booking>();
            try
            {
                using (CarolineCottageDbContext dbContext = new CarolineCottageDbContext(connectionString))
                {
                    Repository.CarolineCottageClasses.Booking currentBooking = dbContext.Bookings.Find(currentBookingID);
                    Repository.CarolineCottageClasses.Booking nextBooking = dbContext.Bookings.OrderBy(x => x.WeekStartDate).Where(x => x.WeekStartDate > currentBooking.WeekStartDate).FirstOrDefault();

                    nextBooking.BookingStatus = currentBooking.BookingStatus;
                    nextBooking.AvailableForShortBreaks = currentBooking.AvailableForShortBreaks;
                    nextBooking.Comment = currentBooking.Comment;
                    nextBooking.WeekPrice = currentBooking.WeekPrice;

                    dbContext.Entry(nextBooking).State = EntityState.Modified;
                    dbContext.SaveChanges();

                    DateTime lastDate = dbContext.Bookings.Max(x => x.WeekStartDate);
                    var bookingView = AutoMapper.Mapper.Map<Booking, Booking>(nextBooking);
                    bookingView.IsLastRow = (lastDate - bookingView.WeekStartDate).Days == 0;
                    return bookingView;
                }

            }
            catch (Exception e)
            {

                return new Booking();
            }
        }
        */
    }

    public static class DateTimeExtensions
    {
        public static DateTime NextDayOfWeek(this DateTime from, DayOfWeek dayOfWeek)
        {
            int start = (int)from.DayOfWeek;
            int target = (int)dayOfWeek;
            if (target < start)
                target += 7;
            return from.AddDays(target - start);
        }
    }
}
