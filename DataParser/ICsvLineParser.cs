using System;
using System.Collections.Generic;
using System.Text;

namespace DataParser
{
  public interface  ICsvLineParser<T>
  {
    T ParseScvLine(string line);
  }
}
