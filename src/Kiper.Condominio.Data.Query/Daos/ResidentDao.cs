using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Kiper.Condominio.Data.Query.Dtos;
using Kiper.Condominio.Core.Interfaces;

namespace Kiper.Condominio.Data.Query.Daos
{
    public class ResidentDao
    {
        private readonly IConfiguration _configurations;
        private readonly IUser _user;

        public ResidentDao(IConfiguration configurations, IUser user)
        {
            _configurations = configurations;
            _user = user;
        }

        public IEnumerable<ResidentDetailsDto> ListOfResidents(string filter)
        {
            using (MySqlConnection connection = new MySqlConnection(
                _configurations.GetConnectionString("kiperCondominioConnectionString")))
            {
                var query = @"SELECT a.Block AS ApartmentBlock, a.Number AS ApartmentNumber, a.Roof AS ApartmentRoof,
                                     r.Id, r.Name, r.Birthday, r.PhoneNumber, r.Email, r.Document,
                                     c.Name AS CondominiumName,
                                     CONCAT(CONCAT(CONCAT(CONCAT(c.street, ' ', c.number), ', ', c.City), '. ', c.State), '. CEP: ', c.ZipCode) AS CondominiumAddress, c.Id AS CondominiumId, a.Id AS ApartmentId
                              FROM Apartment a INNER JOIN Resident r ON a.Id = r.ApartmentId INNER JOIN Condominium c ON a.CondominiumId = c.Id
                               WHERE r.Status = 1 AND r.CreatedBy = ?userId";

                if (!string.IsNullOrEmpty(filter))
                {
                    query = $"{query} AND (r.Name like '%{filter}%' OR r.PhoneNumber like '%{filter}%' OR r.Email like '%{filter}%' OR r.Document like '%{filter}%' OR c.Name like '%{filter}%' OR a.Number like '%{filter}%')";
                }
                
                query = $"{query} ORDER BY r.Name";

                var result = connection.Query<ResidentDetailsDto>(query, new { userId = _user.GetUserId() }).ToList();

                return result;
            }
        }

        public IEnumerable<ResidentDto> ListOfResidentsByApartmentId(Guid apartmentId)
        {
            using (MySqlConnection connection = new MySqlConnection(
                _configurations.GetConnectionString("kiperCondominioConnectionString")))
            {
                var query = @"SELECT r.Id AS ResidentId, r.Name, r.Birthday, r.PhoneNumber, r.Email, r.Document
                              FROM Resident r
                              WHERE r.Status = 1 AND r.ApartmentId = ?apartmentId AND r.CreatedBy = ?userId
                              ORDER BY r.Name";
                
                var result = connection.Query<ResidentDto>(query, new { apartmentId, userId = _user.GetUserId() }).ToList();

                return result;
            }
        }

        public ResidentDto GetResidentById(Guid id)
        {
            using (MySqlConnection connection = new MySqlConnection(
                _configurations.GetConnectionString("kiperCondominioConnectionString")))
            {
                var query = @"SELECT r.Id, r.Name, r.Birthday, r.PhoneNumber, r.Email, r.Document, r.ApartmentId, a.CondominiumId
                              FROM Resident r INNER JOIN Apartment a ON r.ApartmentId = a.Id
                              WHERE r.Status = 1 AND r.Id = ?id AND r.CreatedBy = ?userId
                              ORDER BY r.Name";

                var result = connection.Query<ResidentDto>(query, new { id, userId = _user.GetUserId() }).FirstOrDefault();

                return result;
            }
        }
    }
}
