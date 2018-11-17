using System.Collections.Generic;
using System.Linq;

namespace DataParser
{
  public class ProductParser : ICsvLineParser<CsvProduct>
  {
    public CsvProduct ParseScvLine(string line)
    {
      var product = new CsvProduct();

      var elements = line.Split(";");
      product.VendorCode = elements[0].Trim();
      product.Brand = elements[1].Trim();
      product.Color = elements[2].Trim();
      product.Print = elements[3].Trim();
      product.Price = elements[4].Trim();
      product.MadeInCountry = elements[5].Trim();
      product.Site = elements[6].Trim();
      product.Sizes = ParseSizes(elements[7]);
      product.PhotosUrls = ParseCsvArray(elements[8]);

      return product;
    }

    public List<Size> ParseSizes(string line)
    {
      return ParseCsvArray(line).Select(element => ParseSize(element.Trim())).ToList();
    }

    public Size ParseSize(string line)
    {
      var elements = line.Split(" ");
      var rusSize = elements[0];
      var otherSize = elements[2].Replace("(", "");
      var countryType = elements[3].Replace(")", "");

      var size = new Size {Russian = rusSize, OtherCountry = otherSize, CountryType = countryType};
      return size;
    }

    public List<string> ParseCsvArray(string line)
    {
      return line.Replace("[", "").Replace("]", "").Split(",").ToList();
    }
  }
}