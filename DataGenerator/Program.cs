using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using CommonLibraries;
using CommonLibraries.ColorAlgos;
using CommonLibraries.CommonTypes;
using CommonLibraries.Infrastructures;
using CommonLibraries.Resources;
using CommonLibraries.Resources.DeserializerTypes;
using DataParser;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductDatabase;
using ProductDatabase.Repositories;

namespace DataGenerator
{
  internal class Program
  {
   
    private static void Main(string[] args)
    {
      const string path = @"E:\Projects\Zebra\Project\Database\DatabaseData\ParsedData\blouses_shirts.csv";

      var builder = new DbContextOptionsBuilder();
      builder.UseSqlServer(GetConnectionString());
      var context = new ProductContext(builder.Options);

      var productRepo = new ProductRepository(context);
      var colorRepo = new ColorGoodnessRepository(context);
      var productColorRepo = new ProductColorGoodnessRepository(context);

      //InitDb(path, CategoryType.BlousesShirts, productRepo, colorRepo, productColorRepo);

      QualifyColorGoodnesses(colorRepo);
      QualifyProductColorGoodnesses(productColorRepo, colorRepo);
    }

    public static void InitDb(string fromFilePath, CategoryType categoryType, ProductRepository productRepository, ColorGoodnessRepository colorGoodnessRepo, ProductColorGoodnessRepository productColorRepo)
    {
      FillProduct(fromFilePath, categoryType, productRepository);
      colorGoodnessRepo.FillColorGoodnessesFromProducts().GetAwaiter().GetResult();
      productColorRepo.FillProductColorGoodnessesFromProducts().GetAwaiter().GetResult();
    }

    public static void FillProduct(string fromFilePath, CategoryType categoryType, ProductRepository productRepository)
    {
      var csvProducts = ReadCsvPtoducts(fromFilePath);
      var dtoProducts = ProductCsvToDtoConverter.ConvertCsvProductToDto(csvProducts, categoryType);

      foreach (var addProductDto in dtoProducts)
      {
        productRepository.AddProduct(addProductDto).GetAwaiter().GetResult();
      }
    }

    public static void QualifyColorGoodnesses(ColorGoodnessRepository colorRepository)
    {
      var colorClassifier = new ColorGoodnessQualifier();
      colorClassifier.Init();

      var colors = colorRepository.GetColorGoodnesses().GetAwaiter().GetResult();
      foreach (var color in colors)
      {
        color.Goodness = (float)colorClassifier.GetColorGoodness((PersonalColorType) color.PersonalColorTypeId, new ServerColor(color.ColorId));
      }

      colorRepository.UpdateColorGoodnesses(colors).GetAwaiter().GetResult();
    }

    public static void QualifyProductColorGoodnesses(ProductColorGoodnessRepository productColorRepository, ColorGoodnessRepository colorRepository)
    {
      var productColorGoodness = productColorRepository.GetProductColorGoodnesses().GetAwaiter().GetResult();
      var productWithColorGoodness = productColorRepository.GetProductWithColorGoodnessAsync().GetAwaiter().GetResult();


      foreach (var colorGoodness in productColorGoodness)
      {
        var product = productWithColorGoodness.Find(x => x.ProductId == colorGoodness.ProductId);
        var goodnesses = product.ProductColorGoodnesses.Where(x => x.PersonalColorTypeId == colorGoodness.PersonalColorTypeId).Select(x=>x.Goodness);
        colorGoodness.Goodness = goodnesses.Max();
      }

      productColorRepository.UpdateColorGoodnesses(productColorGoodness).GetAwaiter().GetResult();
    }

    public static string GetPath()
    {
      var databaseDataFolder = @"../../../../../DatabaseData/";
      var parsedData = "ParsedData";
      var preparedData = "PreparedData";

      return Path.Combine(databaseDataFolder, parsedData, "blouses_shirts.csv");
    }

    public static List<CsvProduct> ReadCsvPtoducts(string path)
    {
      var parse = new CsvParser();
      var productParser = new CsvProductLine();
      return parse.ParseFile(path, productParser);
    }

    public static string GetConnectionString()
    {
      var baseFolder = AppContext.BaseDirectory;
      var settingsPath = baseFolder + "hiddensettings.json";
      var str = "";
      using (var sr = new StreamReader(settingsPath))
      {
        str = sr.ReadToEnd();
      }

      var connectionsStrings = JsonConvert.DeserializeObject<Configuration>(str).ConnectionStrings;
      return connectionsStrings.SweaterMainConnection;
    }

    public class Configuration
    {
      public ConnectionStrings ConnectionStrings { get; set; }
    }
  }
}