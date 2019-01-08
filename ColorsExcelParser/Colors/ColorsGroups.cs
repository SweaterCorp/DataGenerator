using System.Collections.Generic;

namespace ColorsExcelParser.Colors
{
  public class ColorsGroups
  {
    public ColorsGroup RedPink { get; set; }
    public ColorsGroup OrangeYellow { get; set; }
    public ColorsGroup Green { get; set; }
    public ColorsGroup Blue { get; set; }
    public ColorsGroup Purple { get; set; }
    public ColorsGroup BrownBeige { get; set; }
    public ColorsGroup GrayBlackWhite { get; set; }

    public ColorsGroups()
    {
      RedPink = new ColorsGroup(1, "RedPink", new List<ColorType> {ColorType.Red, ColorType.Pink});
      OrangeYellow = new ColorsGroup(2, "OrangeYellow", new List<ColorType> {ColorType.Orange, ColorType.Yellow});
      Green = new ColorsGroup(3, "Green", new List<ColorType> {ColorType.Green});
      Blue = new ColorsGroup(4, "Blue", new List<ColorType> {ColorType.Blue});
      Purple = new ColorsGroup(5, "Purple", new List<ColorType> {ColorType.Purple});
      BrownBeige = new ColorsGroup(6, "BrownBeige", new List<ColorType> {ColorType.Brown, ColorType.Beige});
      GrayBlackWhite = new ColorsGroup(7, "GrayBlackWhite", new List<ColorType> {ColorType.Gray, ColorType.Black, ColorType.White});
    }
  }
}