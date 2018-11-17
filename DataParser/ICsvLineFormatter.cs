namespace DataParser
{
  public interface ICsvLineFormatter<T>
  {
    T ParseScvLine(string line);
  }
}