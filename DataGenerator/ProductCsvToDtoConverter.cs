using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CommonLibraries.CommonTypes;
using CommonLibraries.Infrastructures;
using CommonLibraries.Resources;
using CommonLibraries.Resources.DeserializerTypes;
using DataParser;
using ProductDatabase.DTOs;

namespace DataGenerator
{
  public class ProductCsvToDtoConverter
  {
    public static List<AddProductDto> ConvertCsvProductToDto(List<CsvProduct> scvProducts, CategoryType categoryType)
    {
      return scvProducts.Select(x => FromCsvToDto(categoryType, x)).ToList();
    }

    private static AddProductDto FromCsvToDto(CategoryType category, CsvProduct product)
    {
      var shopColor = LamodaColorType.StringToLamodaColorType(product.Color, true);
      ContentPath path = new ContentPath();
      var handler = new ResourceHandler();
      //TODO вынести это и чтобы сразу при запуске программы все ресурсы подгрузились
      var lamodaColors = handler.ReadeResourceFile<LamodaColorsDeserializer>(path.LamodaColors);

      var printIds = product.Print.Split(",").Select(x => PrintType.StringToPrintType(x.Trim(), true).Id).ToList();
      var printTypeId = printIds[0];
      var extraPrintTypeId = printIds.Count >= 2 ? printIds[1] : 0;
      if (printIds.Count > 2) throw new System.Exception("There are more than 2 printType");

      var result = new AddProductDto
      {
        BrandName = product.Brand,
        ShopColorId = shopColor.Id,
        ShopTypeId = 0,
        ColorIds = lamodaColors.Colors.First(x => x.LamodaColorType == shopColor.Id).HexColors.Select(ServerColor.ToInt).ToList(),
        CategoryTypeId = category.Id,
        Country = product.MadeInCountry,
        Link = product.Link,
        Photos = product.PhotosUrls.ToList(),
        Price = decimal.Parse(product.Price, CultureInfo.InvariantCulture.NumberFormat),
        PrintTypeId = printTypeId,
        ExtraPrintTypeId = extraPrintTypeId,
        Sizes = product.Sizes.Select(x => new SizeDto
        {
          CountryCode = x.CountryCode,
          IsAvailable = x.IsAvailable,
          OtherCountry = x.OtherCountry,
          RussianSize = x.RussianSize
        }).ToList(),
        VendorCode = product.VendorCode
      };
      return result;
    }
  }
}