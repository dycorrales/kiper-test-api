using Kiper.Condominio.Core.Helpers;
using Kiper.Condominio.Core.Helpers.Utils;
using Kiper.Condominio.Core.Helpers.ValueObjects;
using Kiper.Condominio.Domain.Entities;
using Kiper.Condominio.Domain.Interfaces;
using Kiper.Condominio.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kiper.Condominio.Infra.Data.Initializers
{
    public sealed class DataBaseInitializer
    {
        private readonly ApplicationContext _context;
        private readonly Guid _userId;

        public DataBaseInitializer(ApplicationContext context, Guid userId)
        {
            _context = context;
            _userId = userId;
        }

        public void Initialize()
        {
            _context.Database.Migrate();
            _context.Database.EnsureCreated();
            InitializeObjects();
        }

        public void InitializeObjects()
        {
            AddCondominiums();
        }
                
        private void AddCondominiums()
        {
            var i = 0;
            var condominiumNames = new []{ "Itambe", "Ilha", "Pedras Altas" };

            do
            {
                var state = Faker.Address.UsState();
                var number = Faker.RandomNumber.Next(1, 100);
                var province = Faker.Address.City();
                var provinceAbbr = Faker.Address.CityPrefix();
                var street = Faker.Address.StreetName();
                var zipCode = Faker.Address.ZipCode();

                var name = condominiumNames[i];

                CreateCondominium(name, new AddressInfo(street, number, null, provinceAbbr, province, state, zipCode));
            }
            while (++i < 3);
        }

        private IList<Resident> GenerateResidents(Guid apartmentId, int residentNumber)
        {
            var i = 0;
            var residents = new List<Resident>();
            var gen = new Random();
            var min = 100000;
            var max = 999999;
            var min1 = 10000;
            var max1 = 99999;

            do
            {
                var name = Faker.Name.FullName();
                var birthday = RamdomDate(gen);
                var phoneNumber1 = gen.Next(min, max);
                var phoneNumber2 = gen.Next(min1, max1);
                var email = Faker.Internet.Email();

                var contact = new ContactInfo($"{phoneNumber1}{phoneNumber2}", email);
                var document = new DocumentInfo(GenerateCpf());

                var resident = Resident.ResidentFactory.NewResident(_userId, name, birthday, contact, document, apartmentId);
                residents.Add(resident);
            }
            while (++i < residentNumber);

            return residents;
        }

        private void AddApartments(Guid condominiumId, int apartmentNumber)
        {
            var i = 0;

            do
            {
                var block = Faker.RandomNumber.Next(1, 5);
                var number = Faker.RandomNumber.Next(1, 100);
                var roof = Faker.RandomNumber.Next(1, 20);
                
                CreateApartment(condominiumId, number, roof, block.ToString());
            }
            while (++i < apartmentNumber);
        }
               
        private void CreateCondominium(string name, AddressInfo addressInfo)
        {
            if (!_context.Condominiums.Any(condominium => condominium.Name == name))
            {
                var address = new AddressInfo(addressInfo.Street, addressInfo.Number, addressInfo.Complement, addressInfo.Neighbourhood, addressInfo.City, addressInfo.State, addressInfo.ZipCode);

                var condominium = Condominium.CondominiumFactory.NewCondominium(_userId, name, address);
                
                _context.Condominiums.Add(condominium);
                _context.SaveChanges();

                AddApartments(condominium.Id, Faker.RandomNumber.Next(1, 30));
            }
        }

        private void CreateApartment(Guid condominiumId, int number, int roof, string block)
        {
            if (!_context.Apartments.Any(apartment => apartment.CondominiumId == condominiumId && apartment.Block == block && apartment.Roof == roof && apartment.Number == number))
            {
                var apartmentId = Guid.NewGuid();
                var residents = GenerateResidents(apartmentId, Faker.RandomNumber.Next(1, 4));

                var apartment = Apartment.ApartmentFactory.NewApartment(apartmentId, _userId, number, block, roof, condominiumId, residents);
                _context.Apartments.Add(apartment);
                _context.SaveChanges();
            }
        }

        public DateTime RamdomDate(Random gen)
        {
            DateTime start = new DateTime(1930, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        public string GenerateCpf()
        {
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string semente = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            return semente;
        }
    }
}
