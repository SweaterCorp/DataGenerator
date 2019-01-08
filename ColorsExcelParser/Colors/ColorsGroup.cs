using System.Collections.Generic;

namespace ColorsExcelParser.Colors
{
  public class ColorsGroup
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ColorType> BaseColors { get; set; }
    public List<string> GoodColors { get; set; } = new List<string>();
    public List<string> BadColors { get; set; } = new List<string>();

    public ColorsGroup() { }

    public ColorsGroup(int id, string name, List<ColorType> colors)
    {
      Id = id;
      Name = name;
      BaseColors = colors;
    }
  }
}
