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
    public class ApartmentDao
    {
        private readonly IConfiguration _configurations;
        private readonly IUser _user;

        public ApartmentDao(IConfiguration configurations, IUser user)
        {
            _configurations = configurations;
            _user = user;
        }

        public IEnumerable<ApartmentDto> ListOfApartmentsByCondominiumId(Guid condominiumId)
        {
            using (MySqlConnection connection = new MySqlConnection(
                _configurations.GetConnectionString("kiperCondominioConnectionString")))
            {
                var query = @"SELECT a.Id, a.Block, a.Number, a.Roof,
                                     (SELECT COUNT(r.Id) FROM Resident r WHERE r.ApartmentId = a.Id AND r.Status = 1) AS ResidentsNumber,
                                     CASE WHEN a.Status = 1 THEN 'Ativo' ELSE 'Inativo' END AS Status
                              FROM Apartment a WHERE a.Status = 1 AND a.CondominiumId = ?condominiumId AND a.CreatedBy = ?userId
                              ORDER BY a.Number";
                
                var result = connection.Query<ApartmentDto>(query, new { condominiumId, userId = _user.GetUserId() }).ToList();

                return result;
            }
        }

        public IEnumerable<ComboboxDto> ListOfAllApartmentsByCondominiumId(Guid condominiumId)
        {
            using (MySqlConnection connection = new MySqlConnection(
                _configurations.GetConnectionString("kiperCondominioConnectionString")))
            {
                var query = @"SELECT a.Id, CONCAT('Bloco: ', a.Block, CONCAT(', Andar: ', a.Roof, 
                              CONCAT(', Numero: ', a.Number, ''))) AS Text
                              FROM Apartment a WHERE a.Status = 1 AND a.CondominiumId = ?condominiumId AND a.CreatedBy = ?userId
                              ORDER BY a.Block, a.Roof, a.Number";

                var result = connection.Query<ComboboxDto>(query, new { condominiumId, userId = _user.GetUserId() }).ToList();

                return result;
            }
        }

        public ApartmentDto GetApartmentById(Guid id)
        {
            using (MySqlConnection connection = new MySqlConnection(
                _configurations.GetConnectionString("kiperCondominioConnectionString")))
            {
                var query = @"SELECT a.Id, a.Block, a.Number, a.Roof, 
                              CASE WHEN a.Status = 1 THEN 'Ativo' ELSE 'Inativo' END AS Status, a.CondominiumId
                              FROM Apartment a
                              WHERE a.Status = 1 AND a.Id = ?id AND a.CreatedBy = ?userId;
                              SELECT r.Id AS ResidentId, r.Name, r.Birthday, r.PhoneNumber, r.Email, r.Document FROM Resident r
                              WHERE r.Status = 1 AND r.ApartmentId = ?id AND r.CreatedBy = ?userId
                              ORDER BY r.Name";
                
                var results = connection.QueryMultiple(query, new { id, userId = _user.GetUserId() });
                var apartment = results.ReadSingle<ApartmentDto>();
                apartment.Residents = results.Read<ResidentDto>().ToList();

                return apartment;
            }
        }
    }
}
