using System;
using System.Collections.Generic;
using System.Linq;
using ColorsExcelParser.Colors;
using Independentsoft.Office.Spreadsheet;

namespace ColorsExcelParser
{
  public class ColorsParser
  {
    public ColorsMatching ReadColors(string path)
    {
      var result = new ColorsMatching();
      var book = new Workbook(path);
      var sheets = book.Sheets;
      var questionSheet = sheets.FirstOrDefault(x => x.Name == "ColorsMatching");

      if (questionSheet is Worksheet worksheet)
      {
        var cells = worksheet.GetCells();

        var startIndex = 0;
        PrintCurrentCell(cells, startIndex);
        FillColorsGroups(cells, startIndex, result.Autumn);

        startIndex += 150;
        PrintCurrentCell(cells, startIndex);
        FillColorsGroups(cells, startIndex, result.Spring);

        startIndex += 150;
        PrintCurrentCell(cells, startIndex);
        FillColorsGroups(cells, startIndex, result.Summer);

        startIndex += 150;
        PrintCurrentCell(cells, startIndex);
        FillColorsGroups(cells, startIndex, result.Winter);
      }
      return result;
    }

    public void PrintCurrentCell(IList<Cell> cells, int index)
    {
      var currentCellValue = cells[index].Value;
      Console.WriteLine($"Current Cell: {index}: '{currentCellValue}'");
    }

    public void FillColorsGroups(IList<Cell> cells, int startIndex, ColorsGroups groups)
    {
      startIndex += 45;
      PrintCurrentCell(cells, startIndex);
      FillColorsGroup(cells, startIndex + 1, groups.RedPink);
      startIndex += 15;
      PrintCurrentCell(cells, startIndex);
      FillColorsGroup(cells, startIndex + 1, groups.OrangeYellow);
      startIndex += 15;
      PrintCurrentCell(cells, startIndex);
      FillColorsGroup(cells, startIndex + 1, groups.Green);
      startIndex += 15;
      PrintCurrentCell(cells, startIndex);
      FillColorsGroup(cells, startIndex + 1, groups.Blue);
      startIndex += 15;
      PrintCurrentCell(cells, startIndex);
      FillColorsGroup(cells, startIndex + 1, groups.Purple);
      startIndex += 15;
      PrintCurrentCell(cells, startIndex);
      FillColorsGroup(cells, startIndex + 1, groups.BrownBeige);
      startIndex += 15;
      PrintCurrentCell(cells, startIndex);
      FillColorsGroup(cells, startIndex + 1, groups.GrayBlackWhite);
    }

    public void FillColorsGroup(IList<Cell> cells, int startIndex, ColorsGroup group)
    {
      (var good, var bad) = GetGoodBadColors(cells, startIndex);
      group.GoodColors.AddRange(good);
      group.BadColors.AddRange(bad);
    }

    public (List<string> good, List<string> bad) GetGoodBadColors(IList<Cell> cells, int startIndex)
    {
      var good = new List<string>();
      var bad = new List<string>();
      var cellIndex = startIndex - 1;
      for (var i = 0; i < 7; i++)
      {
        cellIndex += 1;
        PrintCurrentCell(cells, startIndex);
        var cellValue = cells[cellIndex].Value.Trim();
        good.Add(cellValue);
      }
      for (var i = 0; i < 7; i++)
      {
        cellIndex += 1;
        PrintCurrentCell(cells, startIndex);
        var cellValue = cells[cellIndex].Value.Trim();
        bad.Add(cellValue);
      }
      return (good, bad);
    }
  }
}