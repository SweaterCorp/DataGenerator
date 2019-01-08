using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace ColorsExcelParser
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");
      var path = "E:\\Projects\\Zebra\\Project\\Theory\\SuitableColors.xlsx";
      var savePath = "E:\\Projects\\Zebra\\Project\\Data\\ColorsMatching.json";
      var colors = new ColorsParser().ReadColors(path);
      var json = JsonConvert.SerializeObject(colors);

      using (var sw = new StreamWriter(savePath, false, Encoding.UTF8))
      {
        sw.WriteLine(json);
      }
    }
  }
}