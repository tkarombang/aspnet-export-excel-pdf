using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using ExportDemo.DAL.Repositories.Interfaces;

namespace ExportDemo.Controllers
{
  public class ExportController : Controller
  {

    private readonly IDataRepository _dataRepo;

    public ExportController(IDataRepository dataRepo)
    {
      _dataRepo = dataRepo;
    }

    public IActionResult Index()
    {
      var data = _dataRepo.GetAllData();
      return View(data);
    }

    public IActionResult DownloadPdf()
    {
      var data = _dataRepo.GetAllData();
      return new ViewAsPdf("PdfTemplate", data)
      {
        FileName = "LaporanData.pdf"
      };
    }

    public IActionResult DownloadExcel()
    {
      var data = _dataRepo.GetAllData();

      using var workbook = new XLWorkbook();
      var worksheet = workbook.Worksheets.Add("Data");

      worksheet.Cell(1, 1).Value = "Nama";
      worksheet.Cell(1, 2).Value = "Umur";

      for (int i = 0; i < data.Count; i++)
      {
        worksheet.Cell(i + 2, 1).Value = data[i].Nama;
        worksheet.Cell(i + 2, 2).Value = data[i].Umur;
      }

      using var stream = new MemoryStream();
      workbook.SaveAs(stream);
      var content = stream.ToArray();

      return File(content,
      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Data.xlsx");
    }





  }

}