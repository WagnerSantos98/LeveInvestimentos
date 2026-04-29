using System;

namespace LeveInvestimentos.Domain.ValueObjects
{
    public class Address
    {
        public string ZipCode {get; private set;}
        public string Street {get; private set;}
        public string Number {get; private set;}
        public string Neighborhood {get; private set;}
        public string City {get; private set;}
        public string State {get; private set;}

        public Address(string zipCode, string street, string number, string neighborhood, string city, string state)
        {
            ZipCode = zipCode;
            Street = street;
            Number = number;
            Neighborhood = neighborhood;
            City = city;
            State = state;
        }

        // Parametros do constutor para EF Core
        protected Address() { }

        public override string ToString()
        {
            return $"{Street}, {Number} - {Neighborhood}, {City}/{State} - CEP: {ZipCode}";
        }
    }
}