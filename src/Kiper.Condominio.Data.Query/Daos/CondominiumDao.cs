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
    public class CondominiumDao
    {
        private readonly IConfiguration _configurations;
        private readonly IUser _user;

        public CondominiumDao(IConfiguration configurations, IUser user)
        {
            _configurations = configurations;
            _user = user;
        }

        public IEnumerable<CondominiumDto> ListOfCondominiums(string name)
        {
            using (MySqlConnection connection = new MySqlConnection(
                _configurations.GetConnectionString("kiperCondominioConnectionString")))
            {
                var query = @"SELECT c.Id, c.Name, 
                                     CONCAT(CONCAT(CONCAT(CONCAT(c.street, ' ', c.number), ', ', c.City), '. ', c.State), '. CEP: ', c.ZipCode) AS Address, 
                                     (SELECT COUNT(a.Id) FROM Apartment a WHERE a.CondominiumId = c.Id AND a.Status = 1) AS ApartmentsNumber,
                                     CASE WHEN c.Status = 1 THEN 'Ativo' ELSE 'Inativo' END AS Status
                              FROM Condominium c WHERE c.Status <> 3 AND c.CreatedBy = ?userId
                              ORDER BY c.Name";

                if (!string.IsNullOrEmpty(name))
                    query = $"{query} AND c.Name like '%{name}%'";

                var result = connection.Query<CondominiumDto>(query, new { userId = _user.GetUserId() }).ToList();

                return result;
            }
        }

        public IEnumerable<ComboboxDto> ListOfAllCondominiums()
        {
            using (MySqlConnection connection = new MySqlConnection(
                _configurations.GetConnectionString("kiperCondominioConnectionString")))
            {
                var query = @"SELECT c.Id, c.Name AS Text
                              FROM Condominium c WHERE c.Status = 1 AND c.CreatedBy = ?userId
                              ORDER BY c.Name";

                var result = connection.Query<ComboboxDto>(query, new { userId = _user.GetUserId() }).ToList();

                return result;
            }
        }

        public CondominiumDto GetCondominiumById(Guid id)
        {
            using (MySqlConnection connection = new MySqlConnection(
                _configurations.GetConnectionString("kiperCondominioConnectionString")))
            {
                var query = @"SELECT c.Id, c.Name, 
                                     CONCAT(CONCAT(CONCAT(CONCAT(c.street, ' ', c.number), ', ', c.City), '. ', c.State), '. CEP: ', c.ZipCode) AS Address, 
                                     (SELECT COUNT(a.Id) FROM Apartment a WHERE a.CondominiumId = c.Id AND c.Status = 1) AS ApartmentsNumber FROM Condominium c WHERE c.Id = ?id AND c.CreatedBy = ?userId
                                  ORDER BY c.Name";

                var result = connection.Query<CondominiumDto>(query, new { id, userId = _user.GetUserId() }).FirstOrDefault();

                return result;
            }
        }
    }
}
