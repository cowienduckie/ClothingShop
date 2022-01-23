using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace ClothingShop.BusinessLogic.Helpers
{
    public static class ExportExcelHelper
    {
        public static string ExcelContentType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public static DataTable ToDataTable<T>(List<T> items)
        {
            var dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            var Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in Props)
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (var item in items)
            {
                var values = new object[Props.Length];
                for (var i = 0; i < Props.Length; i++)
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                dataTable.Rows.Add(values);
            }

            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static byte[] ExportExcel(DataTable dataTable, Dictionary<string, string> condition, bool showSequenceNo,
            string tableHeaderColor, string[] columnDisplayName, string[] columnsToTake, string sheetName = "")
        {
            byte[] result;
            using (var package = new ExcelPackage())
            {
                // Set the name of curent sheet
                var Name = string.IsNullOrEmpty(sheetName) ? "NewSheet" : sheetName;
                var workSheet = package.Workbook.Worksheets.Add(Name);

                var startRowFrom = condition.Count == 0 ? 1 : condition.Count + 3;

                if (showSequenceNo)
                {
                    var dataColumn = dataTable.Columns.Add("TT", typeof(int));
                    dataColumn.SetOrdinal(0);
                    var index = 1;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        item[0] = index;
                        index++;
                    }
                }

                // add the content into the Excel file
                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);

                // correct date format for excel  2007+
                // format curency column to VietNamese format
                for (var i = 0; i < dataTable.Columns.Count; i++)
                    if (dataTable.Columns[i].DataType == typeof(DateTime))
                        workSheet.Column(i + 1).Style.Numberformat.Format = "dd/MM/yyyy";

                // format table header
                using (var r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                {
                    r.Style.Font.Color.SetColor(Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(tableHeaderColor));
                }

                // format cells - add borders
                if (dataTable.Rows.Count > 0)
                {
                    using var r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count,
                        dataTable.Columns.Count];
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    r.Style.Border.Top.Color.SetColor(Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(Color.Black);
                    r.Style.Border.Left.Color.SetColor(Color.Black);
                    r.Style.Border.Right.Color.SetColor(Color.Black);
                }

                // removed ignored columns
                if (columnsToTake.Length > 0)
                    for (var i = dataTable.Columns.Count - 1; i >= 0; i--)
                    {
                        if (i == 0 && showSequenceNo)
                            continue;
                        if (!columnsToTake.Contains(dataTable.Columns[i].ColumnName))
                            workSheet.DeleteColumn(i + 1);
                    }

                // Update table header with column display name
                if (columnDisplayName.Length > 0)
                    for (var i = 0; i < columnDisplayName.Length; i++)
                        workSheet.Cells[startRowFrom, i + 2].Value = columnDisplayName[i];

                // autofit width of cells with small content
                var columnIndex = 1;
                for (var i = 0; i < columnDisplayName.Length + 1; i++)
                {
                    var columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex,
                        workSheet.Dimension.End.Row, columnIndex];
                    var maxLength = columnCells.Max(cell => cell.Value?.ToString().Length ?? 0);
                    if (maxLength < 150)
                        workSheet.Column(columnIndex).AutoFit();

                    columnIndex++;
                }

                if (condition.Count > 0)
                    for (var i = 1; i <= condition.Count; i++)
                        workSheet.Cells["A" + i].Value =
                            $"{condition.Keys.ElementAt(i - 1)} : {condition.Values.ElementAt(i - 1)}";

                result = package.GetAsByteArray();
            }

            return result;
        }
    }
}