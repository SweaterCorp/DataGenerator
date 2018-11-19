using System.Collections.Generic;

namespace DataParser
{
  public class CsvProduct
  {
    public string VendorCode { get; set; }
    public string Brand { get; set; }
    public string Color { get; set; }
    public string Print { get; set; }
    public string Price { get; set; }
    public string MadeInCountry { get; set; }
    public string Link { get; set; }
    public IEnumerable<Size> Sizes { get; set; }
    public IEnumerable<string> PhotosUrls { get; set; }
  }

  public class Size
  {
    public string Russian { get; set; }
    public bool IsAvailable { get; set; }
    public string OtherCountry { get; set; }
    public string CountryType { get; set; }
  }
}