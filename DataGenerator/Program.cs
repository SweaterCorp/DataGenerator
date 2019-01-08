using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using DataParser;

namespace DataGenerator
{
  class Program
  {
    static void Main(string[] args)
    {

      List<string> lines = new List<string>();
      List<string> sizes = new List<string>();
      List<string> colors = new List<string>();
      List<string> photos = new List<string>();

      ProductInsertion insertion = new ProductInsertion();

      var databaseDataFolder = @"../../../../../DatabaseData/";
      var parsedData = "ParsedData";
      var preparedData = "PreparedData";

      var blouses =  Path.Combine(databaseDataFolder, parsedData, "blouses_shirts.csv");
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
          sizes.Add(insertion.AddSize(product.VendorCode, productSize.Russian, productSize.IsAvailable,
            productSize.OtherCountry, productSize.CountryType));
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
}
