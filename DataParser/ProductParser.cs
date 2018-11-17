using System;
using System.Collections.Generic;
using System.IO;

namespace DataParser
{
  public class ProductParser
  {

  

    public Product ParseProductLine(string line)
    {
      var product = new Product();

      var elements = line.Split(";");

    }

    public  List<(string russianSize, string otherSize)> ParseSizes(string line)
    {
      var elements = line.Split(",");
    }
  }
}
