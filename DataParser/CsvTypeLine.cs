namespace DataParser
{
  public class CsvTypeLine : ICsvLineFormatter<CsvType>
  {
    public CsvType ParseScvLine(string line)
    {
      return new CsvType {Name = line.Replace(";", "")};
    }
  }
}