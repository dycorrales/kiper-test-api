namespace Kiper.Condominio.Core.Helpers.ValueObjects
{
    public class AddressInfo
    {
        public string Street { get; private set; }
        public int Number { get; private set; }
        public string Complement { get; private set; }
        public string Neighbourhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }

        public AddressInfo() { }

        public AddressInfo(string street, int number, string complement, string neighbourhood, string city, string state, string zipCode)
        {
            Street = street;
            Number = number;
            Complement = complement;
            Neighbourhood = neighbourhood;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public void Update(string street, int number, string complement, string neighbourhood, string city, string state, string zipCode)
        {
            Street = street;
            Number = number;
            Complement = complement;
            Neighbourhood = neighbourhood;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public bool IsValid()
        {
            return Number > 0 && !string.IsNullOrEmpty(Street) && !string.IsNullOrEmpty(Neighbourhood) && !string.IsNullOrEmpty(City) && !string.IsNullOrEmpty(State) && !string.IsNullOrEmpty(ZipCode) && ZipCode.Length == 9;
        }
    }
}
