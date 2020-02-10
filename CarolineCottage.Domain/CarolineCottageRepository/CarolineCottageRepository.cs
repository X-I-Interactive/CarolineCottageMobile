using AutoMapper;
using CarolineCottage.Repository.CarolineCottageDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dB = CarolineCottage.Repository.CarolineCottageClasses;


namespace CarolineCottage.Domain.CarolineCottageRepository
{

    //public class CarolineCottageProfile : Profile
    //{

    //    public CarolineCottageProfile()
    //    {
    //        ShouldMapField = fieldInfo => true;

    //        CreateMap<Booking, dB.Booking>()
    //            .ReverseMap();

    //        CreateMap<User, dB.User>()
    //            .ReverseMap();
    //    }

    //}
    public class CarolineCottageRepository
    {
        private CarolineCottageDbContext _dbContext;

        public CarolineCottageRepository(CarolineCottageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BookingReturn GetCurrentBookings(bool addNewRows, DateTime endDateForDisplay, bool debugSQLConnection)
        {
            DateTime nextWeek = DateTimeExtensions.NextDayOfWeek(DateTime.Now, Repository.CarolineCottageClasses.Booking.ChangeoverDay);

            BookingReturn bookingViewReturn = new BookingReturn();
            Mapper.CreateMap<dB.Booking, Booking>();
            try
            {
                if (addNewRows)
                {
                    //  get the latest date in the database
                    DateTime lastWeekStored = _dbContext.Bookings.OrderByDescending(x => x.WeekStartDate).FirstOrDefault()?.WeekStartDate ?? nextWeek;

                    //  add in any extra to make up to at least a year's worth
                    DateTime endDate = nextWeek.AddDays(78 * 7);
                    for (DateTime weekDate = lastWeekStored.AddDays(7); (endDate - weekDate).TotalDays > 7; weekDate = weekDate.AddDays(7))
                    {
                        //dbContext.Bookings.Add(AutoMapper.Mapper.Map<Booking, Booking>(new Booking(weekDate)));
                    }

                    _dbContext.SaveChanges();

                }
                //  then get list                
                List<Booking> currentBookings = Mapper.Map<List<Booking>>(_dbContext.Bookings.Where(x => x.WeekStartDate >= nextWeek));

                if (!addNewRows)
                {
                    currentBookings = currentBookings.Where(x => x.WeekStartDate < endDateForDisplay).ToList();
                }

                currentBookings.Last().IsLastRow = true;
                bookingViewReturn.BookingList = currentBookings;
                return bookingViewReturn;

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

        public Booking GetBookingByID(int bookingID)
        {
            DateTime lastDate = _dbContext.Bookings.Max(x => x.WeekStartDate);
            var bookingView = Mapper.Map<dB.Booking, Booking>(_dbContext.Bookings.FirstOrDefault(x => x.BookingID == bookingID)) ?? new Booking();
            bookingView.IsLastRow = (lastDate - bookingView.WeekStartDate).Days == 0;
            return bookingView;
        }
        public bool SaveBooking(Booking booking)
        {
            return true;
        }
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
