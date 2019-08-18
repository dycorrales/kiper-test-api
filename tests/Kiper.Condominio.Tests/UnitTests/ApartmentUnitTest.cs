using Kiper.Condominio.Core.Helpers.ValueObjects;
using Kiper.Condominio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Kiper.Condominio.Tests.UnitTests
{
    public class ApartmentUnitTest
    {
        [Fact]
        public void CreateNewValidApartment()
        {
            var residents = new List<Resident>() { Resident.ResidentFactory.NewResident(Guid.NewGuid(), "Residente Teste", DateTime.Now.AddYears(-10), new ContactInfo("(48)99134-5321", "teste@teste.com"), new DocumentInfo("37701988408"), Guid.NewGuid()) };           

            var apartment = Apartment.ApartmentFactory.NewApartment(Guid.NewGuid(), 123, "A32", 2, Guid.NewGuid(), residents);

            var isAValidApartment = apartment.IsValid();

            Assert.True(isAValidApartment);
        }

        [Fact]
        public void CreateNewInvalidApartmentResidentsEmpty()
        {
            var residents = new List<Resident>();

            var apartment = Apartment.ApartmentFactory.NewApartment(Guid.NewGuid(), 123, "A32", 2, Guid.NewGuid(), residents);

            var isAValidApartment = apartment.IsValid();

            Assert.True(!isAValidApartment);
        }

        [Fact]
        public void CreateNewInvalidApartmentResidentDocumentNumberInvalid()
        {
            var residents = new List<Resident>() { Resident.ResidentFactory.NewResident(Guid.NewGuid(), "Residente Teste", DateTime.Now.AddYears(-10), new ContactInfo("(48)99134-5321", "teste@teste.com"), new DocumentInfo("70988408"), Guid.NewGuid()) };

            var apartment = Apartment.ApartmentFactory.NewApartment(Guid.NewGuid(), 123, "A32", 2, Guid.NewGuid(), residents);

            var isAValidApartment = apartment.IsValid();

            Assert.True(!isAValidApartment);
        }
    }
}
