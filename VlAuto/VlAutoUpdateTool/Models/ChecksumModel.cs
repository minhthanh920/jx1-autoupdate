using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VlAutoUpdateTool.Models;
public class ChecksumModel
{
    public string Path { get; set; }
    public string Hash { get; set; }
    public string UrlPath { get; set; }
    public string AbsolutePath { get; set; }
}
