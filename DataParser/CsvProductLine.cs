using System.Collections.Generic;
using System.Linq;

namespace DataParser
{
  public class CsvProductLine : ICsvLineFormatter<CsvProduct>
  {
    public CsvProduct ParseScvLine(string line)
    {
      var product = new CsvProduct();

      var elements = line.Split(";");
      product.VendorCode = elements[0].Trim();
      product.Brand = elements[2].Trim();
      product.Color = elements[3].Trim();
      product.Print = elements[4].Trim();
      product.Price = elements[5].Trim();
      product.MadeInCountry = elements[6].Trim();
      product.Link = elements[7].Trim();
      product.Sizes = ParseSizes(elements[8]);
      product.PhotosUrls = ParseCsvArray(elements[9]);

      return product;
    }

    public List<Size> ParseSizes(string line)
    {
      return ParseCsvArray(line).Select(element => ParseSize(element.Trim())).ToList();
    }

    public Size ParseSize(string line)
    {
      var elements = line.Replace("(", "").Replace(")", "").Replace("|", "").Replace(",", "").Split(" ");
      var isAvailable = elements[3].Contains("True");
      var rusSize = elements[0].Split(":")[1];
      var otherSize = elements[1].Split(":")[1];
      var countryType = elements[1].Split(":")[0];

      var size = new Size { IsAvailable  = isAvailable, Russian = rusSize, OtherCountry = otherSize, CountryType = countryType};
      return size;
    }

    public List<string> ParseCsvArray(string line)
    {
      return line.Replace("[", "").Replace("]", "").Split(",").ToList();
    }
  }
}