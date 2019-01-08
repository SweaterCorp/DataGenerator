using System.Collections.Generic;
using System.Text;

namespace DataParser
{
  public static class StringExtenstion
  {
    public static string ReplaceSequence(this string str, IList<char> oldValues, string newValue)
    {
      var builder = new StringBuilder();
      foreach (var @char in str)
        if (oldValues.Contains(@char)) builder.Append(@char);
        else builder.Append(newValue);
      return builder.ToString();
    }
  }
}