using System;
using Dapper;
using MyApiProject.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MyApiProject.Repositories
{
    public class PersonRepository
    {
        private readonly string _connectionString;

        public PersonRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Task: หมายถึงเป็นการบ่งชี้ว่าเมธอดนี้เป็น asynchronous 
        public async Task<IEnumerable<Person>> GetAllPersons()
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                return await db.QueryAsync<Person>("SELECT * FROM Person");
            }
        }

        public async Task<Person> GetPersonById(int id)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<Person>("SELECT * FROM Person WHERE Id = @Id", new { Id = id });
            }
        }
        // person เพื่อระบุว่าเป็นตัวแทนของข้อมูล
        // Person เพื่อเป็นModelsที่ทำการจัดเก็บ
        public async Task AddPerson(Person person)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sqlQuery = "INSERT INTO Person (name, age) VALUES(@name, @age)";
                await db.ExecuteAsync(sqlQuery, person);
            }
        }

        public async Task UpdatePerson(Person person)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sqlQuery = "UPDATE Person SET Name = @Name, Age = @Age WHERE Id = @Id";
                await db.ExecuteAsync(sqlQuery, person);
            }
        }

        public async Task DeletePerson(int id)
        {
            using (IDbConnection db = new MySqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM Person WHERE Id = @Id";
                await db.ExecuteAsync(sqlQuery, new { Id = id });
            }
        }
    }
}

