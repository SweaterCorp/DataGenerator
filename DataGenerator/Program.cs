using System;
using DataParser;

namespace DataGenerator
{
  class Program
  {
    static void Main(string[] args)
    {

      var path = @"../../../../../DatabaseData/ParsedData/blouses_shirts.csv";
      CsvParser parse = new CsvParser();
      ProductParser productParser = new ProductParser();
      var products = parse.ParseFile(path, productParser);
      var t = products;

    }
  }
}
