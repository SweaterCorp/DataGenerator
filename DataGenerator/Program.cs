using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using CommonLibraries;
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
      var builder = new DbContextOptionsBuilder();
      builder.UseSqlServer(GetConnectionString());
      var context = new ProductContext(builder.Options);

      var productRepo = new ProductRepository(context);
      var colorRepo = new ColorRepository(context);

      //colorRepo.FillColorMatchingFromProducts().GetAwaiter().GetResult();

      //FillProductCategory(productRepo);
      //FillColorMatching(colorRepo);

    }

    public static void FillColorMatching(ColorRepository colorRepository)
    {
      var colorClassifier = new ColorClassifier();
      colorClassifier.Init();

      var colors = colorRepository.GetColorMatching().GetAwaiter().GetResult();
      foreach (var color in colors)
      {
        color.Blue = (float)colorClassifier.GetColorGoodness((PersonalColorType) color.PersonalColorTypeId, ColorGroupType.Blue, new ServerColor(color.ColorId));
        color.BrownBeige = (float)colorClassifier.GetColorGoodness((PersonalColorType) color.PersonalColorTypeId, ColorGroupType.BrownBeige, new ServerColor(color.ColorId));
        color.GrayBlackWhite = (float)colorClassifier.GetColorGoodness((PersonalColorType) color.PersonalColorTypeId, ColorGroupType.GrayBlackWhite, new ServerColor(color.ColorId));
        color.Green = (float)colorClassifier.GetColorGoodness((PersonalColorType) color.PersonalColorTypeId, ColorGroupType.Green, new ServerColor(color.ColorId));
        color.OrangeYellow = (float)colorClassifier.GetColorGoodness((PersonalColorType) color.PersonalColorTypeId, ColorGroupType.OrangeYellow, new ServerColor(color.ColorId));
        color.Purple = (float)colorClassifier.GetColorGoodness((PersonalColorType) color.PersonalColorTypeId, ColorGroupType.Purple, new ServerColor(color.ColorId));
        color.RedPink = (float)colorClassifier.GetColorGoodness((PersonalColorType) color.PersonalColorTypeId, ColorGroupType.RedPink, new ServerColor(color.ColorId));
      }

      colorRepository.UpdateColorsMatchin(colors).GetAwaiter().GetResult();
    }

    public static void FillProductCategory(ProductRepository productRepository)
    {
      var path = @"E:\Projects\Zebra\Project\Database\DatabaseData\ParsedData\blouses_shirts.csv";
      var csvProducts = ReadCsvPtoducts(path);
      var dtoProducts = ProductCsvToDtoConverter.ConvertCsvProductToDto(csvProducts, CategoryType.BlousesShirts);

      //  ContentPath content = new ContentPath(AppContext.BaseDirectory);
      //var resource = new ResourceHandler();
      //  var lamodaColors = resource.ReadeResourceFile<LamodaColorsDeserializer>(content.LamodaColors);

      foreach (var addProductDto in dtoProducts)
      {
        productRepository.AddProduct(addProductDto).GetAwaiter().GetResult();
      }

      //var products = productRrepo.AddProduct(dtoProducts[0]).GetAwaiter().GetResult();
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