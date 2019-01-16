using CommonLibraries.Infrastructures;
using System;
using System.Linq;
using CommonLibraries.CommonTypes;
using CommonLibraries.Resources;
using CommonLibraries.Resources.DeserializerTypes;

namespace MainConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      

      ContentPath content = new ContentPath();
      var resource = new ResourceHandler();
      var colorsMatching = resource.ReadeResourceFile<LamodaColorsDeserializer>(content.LamodaColors);

      Console.WriteLine(ServerColor.GetDifference(new ServerColor("000000"), new ServerColor("000000")));
      Console.WriteLine(ServerColor.GetDifference(new ServerColor("ffffff"), new ServerColor("ffffff")));
      Console.WriteLine(ServerColor.GetDifference(new ServerColor("dfbd93"), new ServerColor("dfbd93")));

      var baseColors = BaseColorType.AsList();

      ServerColor beige = new ServerColor("dfbd93");
      foreach (var baseColorType in baseColors)
      {
        var otherColor = new ServerColor(baseColorType.Hex);
        var diff = ServerColor.GetDifference(beige, otherColor);
        Console.WriteLine($"Base Color: {baseColorType.Name}:      {diff} | ({otherColor.ToRgb().R}, {otherColor.ToRgb().G}, {otherColor.ToRgb().B}) | ({beige.ToRgb().R}, {beige.ToRgb().G}, {beige.ToRgb().B})");
      }
    }
  }
}
