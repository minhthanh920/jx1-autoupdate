using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace VlWeb.Controllers;

public class GameController : Controller
{
    public IActionResult Index(string f)
    {
        f=HttpUtility.UrlDecode(f);
        var path = Path.Combine(Environment.CurrentDirectory, "Files", "Game", f);
        path = Path.Combine("C:\\Games\\CuuThienNhiTrong\\Game", f);
        var stream = System.IO.File.OpenRead(path);
        return new FileStreamResult(stream, "application/octet-stream");
    }
}

