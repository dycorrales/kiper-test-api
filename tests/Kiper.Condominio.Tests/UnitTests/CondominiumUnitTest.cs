using Kiper.Condominio.Core.Helpers.ValueObjects;
using Kiper.Condominio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Kiper.Condominio.Tests.UnitTests
{
    public class CondominiumUnitTest
    {
        [Fact]
        public void CreateNewValidCondominium()
        {
            var condominium = Condominium.CondominiumFactory.NewCondominium(Guid.NewGuid(), "Teste", new AddressInfo("Rua Luiz Oscar de Carvalho", 22, "B34", "Santa Mónica", "Florianópolis", "Santa Catarina", "88036-400"));

            var isAValidCondominium = condominium.IsValid();

            Assert.True(isAValidCondominium);
        }

        [Fact]
        public void CreateNewInvalidAddressCondominium()
        {
            var condominium = Condominium.CondominiumFactory.NewCondominium(Guid.NewGuid(), "Teste", new AddressInfo("Rua Luiz Oscar de Carvalho", 22, "B34", "Santa Mónica", "Florianópolis", "Santa Catarina", "88036400"));

            var isAValidCondominium = condominium.IsValid();

            Assert.True(!isAValidCondominium);
        }
    }
}
