using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataParser
{
  public class CsvParser
  {
    public List<T> ParseFile<T>(string path, ICsvLineFormatter<T> lineParser)
    {
      var products = new List<T>();
      using (var sr = new StreamReader(path, Encoding.Default))
      {
        string line;
        while ((line = sr.ReadLine()) != null)
        {
          line = line.Trim();
          if (line != string.Empty && !line.StartsWith("//")) products.Add(lineParser.ParseScvLine(line));
        }
      }
      return products;
    }
  }
}