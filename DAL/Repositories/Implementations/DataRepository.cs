using System.Data;
using Microsoft.Data.SqlClient;
using ExportDemo.Models;
using ExportDemo.DAL.Repositories.Interfaces;


namespace ExportDemo.DAL.Repositories.Implementations
{

  using Microsoft.Extensions.Configuration;
  public class DataRepository(IConfiguration configuration) : IDataRepository
  {
    private readonly string? _connectionString = configuration.GetConnectionString("DefaultConnection")!;

    public List<DataModel> GetAllData()
    {
      var dataList = new List<DataModel>();
      
      using var conn = new SqlConnection(_connectionString);
      using var cmd = new SqlCommand("sp_GetDataModelWithCTE", conn);
      cmd.CommandType = CommandType.StoredProcedure;

      conn.Open();
      using var reader = cmd.ExecuteReader();
      while (reader.Read())
      {
        dataList.Add(new DataModel
        {
          Nama = reader["Nama"].ToString(),
          Umur = Convert.ToInt32(reader["Umur"])
        });
      }

      return dataList;
    }


  } 
}