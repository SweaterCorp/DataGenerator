using System.Collections.Generic;
using System.Text;
using static System.String;

namespace DataGenerator
{
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
