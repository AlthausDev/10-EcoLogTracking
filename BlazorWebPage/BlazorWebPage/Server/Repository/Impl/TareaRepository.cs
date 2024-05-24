using BlazorWebPage.Server.Repository.Interfaces;
using BlazorWebPage.Shared;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BlazorWebPage.Server.Repository.Impl
{
    public class TareaRepository : ITareaRepository
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString => _configuration.GetConnectionString("BlazorWebPageDB");

        public TareaRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Add(Tarea tarea)
        {        
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string query = @$"INSERT INTO Tarea (Nombre, Descripcion, Finalizado) VALUES ('{tarea.Nombre}', '{tarea.Descripcion}', {(tarea.Finalizado ? 1 : 0)});";
                dbConnection.Query(query);
            }
        }

        public IEnumerable<Tarea> GetAll()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string query = @"SELECT * FROM Tarea;";
                return dbConnection.Query<Tarea>(query);
            }
        }

        public Tarea GetById(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string query = $"SELECT * FROM Tarea WHERE Id = {id};";
                return dbConnection.QueryFirstOrDefault<Tarea>(query);
            }
        }
      
        public void Remove(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string query = $"DELETE FROM Tarea WHERE Id = {id};";
                dbConnection.Execute(query);
            }
        }


        public void Update(Tarea tarea)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string query = @$"UPDATE Tarea SET Nombre = '{tarea.Nombre}', Descripcion = '{tarea.Descripcion}', Finalizado = '{(tarea.Finalizado ? 1 : 0)}' WHERE Id = '{tarea.Id}';";
                dbConnection.Execute(query);
            }
        }

        public void LogicRemove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
