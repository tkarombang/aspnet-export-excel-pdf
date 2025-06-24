using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using ExportDemo.Models;
using System.IO;

namespace ExportDemo.Controllers
{
  public class ExportController : Controller
  {
    public IActionResult Index()
    {
      var data = GetSampleData();
      return View(data);
    }

    public IActionResult DownloadPdf()
    {
      var data = GetSampleData();
      return new ViewAsPdf("PdfTemplate", data)
      {
        FileName = "LaporanData.pdf"
      };
    }

    public IActionResult DownloadExcel()
    {
      var data = GetSampleData();

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





    private List<DataModel> GetSampleData()
    {
      return new List<DataModel>
      {
        new DataModel {Nama = "Muhammad", Umur = 33},
        new DataModel {Nama = "Azwar", Umur = 30},
        new DataModel {Nama = "Anas", Umur = 31}
      };
    }

  }

}