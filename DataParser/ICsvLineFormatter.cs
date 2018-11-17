namespace DataParser
{
  public interface ICsvLineParser<T>
  {
    T ParseScvLine(string line);
  }
}