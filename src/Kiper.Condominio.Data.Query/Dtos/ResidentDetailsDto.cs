using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kiper.Condominio.Data.Query.Dtos
{
    public class ResidentDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get { return DateTime.Now.Year - Birthday.Year; } }
        public string PhoneNumber { get; set; }
        public string FormatedPhoneNumber
        {
            get
            {
                return string.IsNullOrEmpty(PhoneNumber) ? "" : (PhoneNumber.Length > 10 ? Convert.ToUInt64(PhoneNumber).ToString(@"(00) 00000\-0000") : Convert.ToUInt64(PhoneNumber).ToString(@"(00) 0000\-0000")); ;
            }
        }
        public string FormatedDocument
        {
            get
            {
                return Convert.ToUInt64(Document).ToString(@"000\.000\.000\-00");
            }
        }
        public int ApartmentNumber { get; set; }
        public string ApartmentBlock { get; set; }
        public int ApartmentRoof { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public string CondominiumName { get; set; }
        public string CondominiumAddress { get; set; }
        public Guid ApartmentId { get; set; }
        public Guid CondominiumId { get; set; }
    }
}
