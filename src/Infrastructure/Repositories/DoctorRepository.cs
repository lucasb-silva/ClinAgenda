using System.Text;
using ClinAgenda.src.Application.DTOs.Doctor;
using ClinAgenda.src.Core.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;

namespace ClinAgenda.src.Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly MySqlConnection _connection;

        public DoctorRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<(int total, IEnumerable<DoctorListDTO> doctor)> GetPatientsAsync(string? name, int? specialtyId, int? statusId, int offset, int pageSize)
        {
            var queryBase = new StringBuilder(@"     
                    FROM DOCTOR D
                    INNER JOIN STATUS S ON S.ID = D.STATUSID
                    INNER JOIN DOCTOR_SPECIALTY DS ON DS.DOCTORID = D.ID
                    WHERE 1 = 1");

            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(name))
            {
                queryBase.Append(" AND D.NAME LIKE @Name");
                parameters.Add("Name", $"%{name}%");
            }

            if (specialtyId.HasValue)
            {
                queryBase.Append(" AND DS.SPECIALTYID = @specialtyId");
                parameters.Add("SpecialtyId", specialtyId.Value);
            }

            if (statusId.HasValue)
            {
                queryBase.Append(" AND S.ID = @StatusId");
                parameters.Add("StatusId", statusId.Value);
            }

            var countQuery = $"SELECT COUNT(DISTINCT D.ID) {queryBase}";
            int total = await _connection.ExecuteScalarAsync<int>(countQuery, parameters);

            var dataQuery = $@"
                    SELECT 
                        D.ID, 
                        D.NAME,
                        D.STATUSID AS STATUSID, 
                        S.NAME AS STATUSNAME
                    {queryBase}
                    ORDER BY D.ID
                    LIMIT @Limit OFFSET @Offset";

            parameters.Add("Limit", offset);
            parameters.Add("Offset", (pageSize - 1) * offset);

            var doctor = await _connection.QueryAsync<DoctorListDTO>(dataQuery, parameters);

            return (total, doctor);
        }


    }
}