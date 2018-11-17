namespace DataParser
{
  public class TypeParser : ICsvLineParser<CsvType>
  {
    public CsvType ParseScvLine(string line)
    {
      return new CsvType {Name = line.Replace(";", "")};
    }
  }
}