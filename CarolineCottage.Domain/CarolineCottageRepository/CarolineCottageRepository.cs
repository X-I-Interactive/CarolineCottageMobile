using AutoMapper;
using AutoMapper.EquivalencyExpression;
using CarolineCottage.Repository.CarolineCottageDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dB = CarolineCottage.Repository.CarolineCottageClasses;


namespace CarolineCottage.Domain.CarolineCottageRepository
{

    public class CarolineCottageProfile : Profile
    {

        public CarolineCottageProfile()
        {
            ShouldMapField = fieldInfo => true;

            CreateMap<Booking, dB.Booking>()
                .ReverseMap();

            CreateMap<User, dB.User>()
                .ReverseMap();
        }

    }
    public class CarolineCottageRepository
    {
        private readonly CarolineCottageDbContext _dbContext;

        public CarolineCottageRepository(CarolineCottageDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool SaveBooking(Booking booking)
        {
            return true;
        }
    }
}
