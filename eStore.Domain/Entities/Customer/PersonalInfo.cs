using System;
using eStore.Domain.Shared;

namespace eStore.Domain.Entities.Customer
{
    public class PersonalInfo : IValueObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CityOfBirth { get; set; }
    }
}