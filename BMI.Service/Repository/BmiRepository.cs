using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BMI.Service.Models;
using Dapper;

namespace BMI.Service.Repository
{
    public class BmiRepository : IBmiRepository
    {
        private readonly string _connectionString;

        public BmiRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<bool> InsertRecords(IEnumerable<BmiModel> records)
        {
            var success = true;
            foreach(var record in records)
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", Guid.NewGuid());
                parameters.Add("Forename", record.Forename);
                parameters.Add("Surname", record.Surname);
                parameters.Add("Height", record.Height);
                parameters.Add("Weight", record.Weight);
                parameters.Add("Bmi", record.Bmi);
                parameters.Add("Category", record.Category);

                const string sql = @"INSERT INTO [dbo].[Records]
                               ([Id]
                               ,[Forename]
                               ,[Surname]
                               ,[Height]
                               ,[Weight]
                               ,[Bmi]
                               ,[Category])
                         VALUES
                               (@Id
                               ,@Forename
                               ,@Surname
                               ,@Height
                               ,@Weight
                               ,@Bmi
                               ,@Category)";

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var response = await connection.ExecuteAsync(sql, parameters);

                    if (response <= 0)
                    {
                        success = false;
                    }
                }
            }

            return success;

        }

        public async Task<IEnumerable<BmiModel>> GetRecords()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var result = await connection.QueryAsync<BmiModel>(
                    @"SELECT  r.Id,
		                            r.Forename,
		                            r.Surname,
		                            r.Height,
		                            r.Weight,
		                            r.Bmi,
		                            r.Category
                            FROM dbo.Records r (NOLOCK)"
                );

                return result.ToList();
            }
        }
    }
}
