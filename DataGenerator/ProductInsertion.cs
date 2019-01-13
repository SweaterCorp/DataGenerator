using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using DataParser;
using static System.String;

namespace DataGenerator
{

  public class InsertionMain
  {
    static void Run(string[] args)
    {

      List<string> lines = new List<string>();
      List<string> sizes = new List<string>();
      List<string> colors = new List<string>();
      List<string> photos = new List<string>();

      ProductInsertion insertion = new ProductInsertion();

      var databaseDataFolder = @"../../../../../DatabaseData/";
      var parsedData = "ParsedData";
      var preparedData = "PreparedData";

      var blouses = Path.Combine(databaseDataFolder, parsedData, "blouses_shirts.csv");
      CsvParser parse = new CsvParser();
      CsvProductLine productParser = new CsvProductLine();
      var products = parse.ParseFile(blouses, productParser);

      foreach (var product in products)
      {

        float price = float.Parse(product.Price, CultureInfo.InvariantCulture.NumberFormat);
        lines.Add(insertion.ExecutableLine(product.VendorCode, product.Brand, "blouses_shirts", product.Color,
          product.Print, price, product.MadeInCountry, product.Link, product.PhotosUrls.First()));
        foreach (var productSize in product.Sizes)
        {
          sizes.Add(insertion.AddSize(product.VendorCode, productSize.RussianSize, productSize.IsAvailable,
            productSize.OtherCountry, productSize.CountryCode));
        }
        foreach (var productPhoto in product.PhotosUrls)
        {
          photos.Add(insertion.AddPhotos(product.VendorCode, productPhoto));
        }

      }

      var t = lines;

      var path = Path.Combine(databaseDataFolder, preparedData, "blouses.sql");
      var path1 = Path.Combine(databaseDataFolder, preparedData, "sizes.sql");
      var path2 = Path.Combine(databaseDataFolder, preparedData, "photos.sql");



      WriteToFile(path, lines);
      WriteToFile(path1, sizes);
      WriteToFile(path2, photos);

    }

    public static void WriteToFile(string path, List<string> list)
    {
      using (var sw = new StreamWriter(path))
      {
        foreach (var item in list)
        {
          sw.WriteLine(item);
        }

      }
    }
  }

  public class ProductInsertion
  {
    public string ExecutableLine(string vendorCode, string brandName, string categoryName, string colorName, string printName, float price, string madeInCountry, string link, string previewPhotoUrl)
    {
      var pricedFormat = Format(new System.Globalization.CultureInfo("en-GB"), "{0:f2}", price);
      return $@"EXECUTE [dbo].[uspAddProductBot] N'{vendorCode}', N'{brandName}', N'{categoryName}', N'{colorName}', N'{printName}', {pricedFormat}, N'{madeInCountry}', N'{link}', N'{previewPhotoUrl}'
        GO";
    }

    public string AddSize(string vendorCode, string russianSize, bool isAvailable, string otherCountrySize, string countryType)
    {
      var isAvailableInt = isAvailable ? 1 : 0;
      return $@"EXECUTE [dbo].[uspAddProductSizeTypeBot] N'{vendorCode}', {isAvailableInt},  N'{russianSize}', N'{otherCountrySize}', N'{countryType}'
              GO";
    }

    public string AddColor(string vendorCode, string russianColorName)
    {
      return $@"EXECUTE [dbo].[uspAddProductColorTypeBot] N'{vendorCode}', N'{russianColorName}'
              GO";
    }

    public string AddPhotos(string vendorCode, string photoUrl)
    {
      return $@"EXECUTE [dbo].[uspAddProductPhotoBot] N'{vendorCode}', N'{photoUrl}'
              GO";
    }


  }
}
