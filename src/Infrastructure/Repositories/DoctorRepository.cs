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

        public async Task<IEnumerable<DoctorListDTO>> GetDoctorsAsync(string? name, int? specialtyId, int? statusId, int offset, int itemsPerPage)
        {
            var innerJoins = new StringBuilder(@"     
                    FROM DOCTOR D
                    INNER JOIN STATUS S ON S.ID = D.STATUSID
                    INNER JOIN DOCTOR_SPECIALTY DS ON DS.DOCTORID = D.ID
                    WHERE 1 = 1");

            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(name))
            {
                innerJoins.Append(" AND D.NAME LIKE @Name");
                parameters.Add("Name", $"%{name}%");
            }

            if (specialtyId.HasValue)
            {
                innerJoins.Append(" AND DS.SPECIALTYID = @specialtyId");
                parameters.Add("SpecialtyId", specialtyId.Value);
            }

            if (statusId.HasValue)
            {
                innerJoins.Append(" AND S.ID = @StatusId");
                parameters.Add("StatusId", statusId.Value);
            }

            parameters.Add("LIMIT", itemsPerPage);
            parameters.Add("OFFSET", offset);

            var query = $@"
                SELECT DISTINCT
                    D.ID AS ID,
                    D.NAME AS NAME,
                    S.ID AS STATUSID,
                    S.NAME AS STATUSNAME
                {innerJoins}
                ORDER BY D.ID
                LIMIT @Limit OFFSET @Offset";

            return await _connection.QueryAsync<DoctorListDTO>(query.ToString(), parameters);
        }

        public async Task<IEnumerable<SpecialtyDoctorDTO>> GetDoctorSpecialtiesAsync(int[] doctorIds)
        {
            var query = @"
                SELECT 
                     DS.DOCTORID AS DOCTORID,
                     SP.ID AS SPECIALTYID,
                     SP.NAME AS SPECIALTYNAME,
                     SP.SCHEDULEDURATION 
                FROM DOCTOR_SPECIALTY DS
                INNER JOIN SPECIALTY SP ON DS.SPECIALTYID = SP.ID
                WHERE DS.DOCTORID IN @DOCTORIDS";

            var parameters = new { DoctorIds = doctorIds };
 
            return await _connection.QueryAsync<SpecialtyDoctorDTO>(query, parameters);
        }


    }
}