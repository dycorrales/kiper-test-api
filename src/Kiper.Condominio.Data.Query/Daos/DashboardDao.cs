using Dapper;
using Kiper.Condominio.Core.Interfaces;
using Kiper.Condominio.Data.Query.Dtos;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kiper.Condominio.Data.Query.Daos
{
    public class DashboardDao
    {
        private readonly IConfiguration _configurations;
        private readonly IUser _user;

        public DashboardDao(IConfiguration configurations, IUser user)
        {
            _configurations = configurations;
            _user = user;
        }

        public DashboardDto DashboardInfo()
        {
            using (MySqlConnection connection = new MySqlConnection(
                _configurations.GetConnectionString("kiperCondominioConnectionString")))
            {
                string query = @"SELECT COUNT(Id) FROM Condominium WHERE Status <> 3 AND CreatedBy = ?userId;
                                 SELECT COUNT(Id) FROM Apartment WHERE Status <> 3 AND CreatedBy = ?userId;
                                 SELECT COUNT(Id) FROM Resident WHERE Status = 1 AND CreatedBy = ?userId;";

                var condominiumsNumber = 0;
                var apartmentsNumber = 0;
                var residentsNumber = 0;

                using (var multi = connection.QueryMultiple(query, new { userId = _user.GetUserId() }))
                {
                    condominiumsNumber = multi.Read<int>().Single();
                    apartmentsNumber = multi.Read<int>().Single();
                    residentsNumber = multi.Read<int>().Single();
                }

                return new DashboardDto()
                {
                    CondominiumsNumber = condominiumsNumber,
                    ApartmentsNumber = apartmentsNumber,
                    ResidentsNumber = residentsNumber
                };
            }
        }
    }
}
