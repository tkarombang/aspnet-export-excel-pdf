using ExportDemo.Models;

namespace ExportDemo.DAL.Repositories.Interfaces
{
  public interface IDataRepository
  {
    List<DataModel> GetAllData();
  }
}

