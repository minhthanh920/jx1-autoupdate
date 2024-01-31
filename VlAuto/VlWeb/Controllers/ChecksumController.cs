using Microsoft.AspNetCore.Mvc;

namespace VlWeb.Controllers;

public class ChecksumController : Controller
{
    public IActionResult Index()
    {
        string folderPath = Path.Combine(Environment.CurrentDirectory, "Files");
        folderPath = @"C:\Games\CuuThienNhiTrong";
        string filePath = Path.Combine(folderPath, "checksum.txt");
        string text = System.IO.File.ReadAllText(filePath);
        return Content(text, contentType: "application/json");
    }
}

