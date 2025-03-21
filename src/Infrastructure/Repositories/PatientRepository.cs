using System.Text;
using ClinAgenda.src.Application.DTOs.Patient;
using Dapper;
using MySql.Data.MySqlClient;

namespace ClinAgenda.src.Infrastructure.Repositories
{
    public class PatientRepository
    {
        private readonly MySqlConnection _connection;

        public PatientRepository(MySqlConnection connection)
        {
            _connection = connection;
        }
        public async Task<PatientDTO> GetByIdAsync(int id)
        {
            const string query = @"
                SELECT 
                    ID, 
                    NAME,
                    PHONENUMBER,
                    DOCUMENTNUMBER,
                    STATUSID,
                    BIRTHDATE 
                FROM PATIENT
                WHERE ID = @Id";

            var patient = await _connection.QueryFirstOrDefaultAsync<PatientDTO>(query, new { Id = id });

            return patient;
        }
        public async Task<(int total, IEnumerable<PatientDTO> patients)> GetAllAsync(int? itemsPerPage, int? page)
        {
            var queryBase = new StringBuilder(@"
                FROM PATIENT S WHERE 1 = 1");

            var parameters = new DynamicParameters();

            var countQuery = $"SELECT COUNT(DISTINCT S.ID) {queryBase}";
            int total = await _connection.ExecuteScalarAsync<int>(countQuery, parameters);

            var dataQuery = $@"
            SELECT ID, 
            NAME, 
            PHONENUMBER,
            DOCUMENTNUMBER,
            STATUSID,
            BIRTHDATE
            {queryBase}
            LIMIT @Limit OFFSET @Offset";

            parameters.Add("Limit", itemsPerPage);
            parameters.Add("Offset", (page - 1) * itemsPerPage);

            var patients = await _connection.QueryAsync<PatientDTO>(dataQuery, parameters);

            return (total, patients);
        }
        public async Task<int> InsertPatientAsync(PatientInsertDTO patientInsertDTO)
        {
            string query = @"
            INSERT INTO PATIENT (NAME, PHONENUMBER, DOCUMENTNUMBER, STATUSID, BIRTHDATE) 
            VALUES (@Name, @Phonenumber, @Documentnumber, @Statusid, @Birthdate);
            SELECT LAST_INSERT_ID();";
            return await _connection.ExecuteScalarAsync<int>(query, patientInsertDTO);
        }
        public async Task<int> DeletePatientAsync(int id)
        {
            string query = @"
            DELETE FROM PATIENT
            WHERE ID = @Id";

            var parameters = new { Id = id };

            var rowsAffected = await _connection.ExecuteAsync(query, parameters);

            return rowsAffected;
        }
    }
}
