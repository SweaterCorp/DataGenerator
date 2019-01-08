using System.Collections.Generic;

namespace ColorsExcelParser.Colors
{
  public class ColorType
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Hex { get; set; }

    public static ColorType None { get; } = new ColorType(0, "None", "000000");

    public static ColorType Black { get; } = new ColorType(1, "Black", "000000");

    public static ColorType White { get; } = new ColorType(2, "White", "ffffff");

    public static ColorType Red { get; } = new ColorType(3, "Red", "ff0000");

    public static ColorType Pink { get; } = new ColorType(4, "Pink", "ffffff");

    public static ColorType Orange { get; } = new ColorType(5, "Orange", "ffa500");

    public static ColorType Yellow { get; } = new ColorType(6, "Yellow", "ffff00");

    public static ColorType Green { get; } = new ColorType(7, "Green", "008000");

    public static ColorType Blue { get; } = new ColorType(8, "Blue", "42aaff");

    public static ColorType Purple { get; } = new ColorType(9, "Purple", "800080");

    public static ColorType Brown { get; } = new ColorType(10, "Brown", "964b00");

    public static ColorType Beige { get; } = new ColorType(11, "Beige", "f5f5dc");

    public static ColorType Gray { get; } = new ColorType(12, "Gray", "808080");

    public ColorType(int id, string name, string hex)
    {
      Id = id;
      Name = name;
      Hex = hex;
    }

    public static IEnumerable<ColorType> List()
    {
      return new List<ColorType>
      {
        None,
        White,
        Black,
        Red,
        Pink,
        Orange,
        Yellow,
        Green,
        Blue,
        Purple,
        Brown,
        Beige,
        Gray
      };
    }
    //}
    //  return serverColor.Id;
    //{

    //public static explicit operator int(ColorType serverColor)
    //}
    //         throw new InvalidCastException($"Cannot cast int x:{x} to enumeration {nameof(ColorType)}");
    //  return List().FirstOrDefault(item => item.Id == x) ??
    //{

    //public static implicit operator ColorType(int x)
  }
}