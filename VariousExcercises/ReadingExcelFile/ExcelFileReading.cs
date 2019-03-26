using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Xml;
using System.Globalization;
using Newtonsoft.Json.Linq;
using ReadingExcelFile.Helper;

namespace ReadingExcelFile
{
    public class Text {
        public int? MyProperty { get; set; }
    }
    [TestClass]
    public class ExcelFileReading
    {
        [TestMethod]
        public void PrenNext()
        {
            //int i = 0;
            //int j = 0;
            Text text = new Text();

            var k = text.MyProperty.Value;
            //int newI = ++i;
            //int newJ = j++;
            //string kkk = "abc";
            ////var kkkkk = kkk.Substring(, 1);
            ////NumberStyles style = NumberStyles.AllowDecimalPoint;
            //CultureInfo provider = new CultureInfo("en-US");

            //string k = "Historic Payment Behavior #Weight = 1.0";
            //var kk = k.Split('#');
            //var wvalue = kk.Single(i => i.ToLower().Contains(("Weigh7t").ToLower())).Split('=')[1];
            //var k1 = k.Contains("Weight");
            //var dd = decimal.Parse(wvalue, provider);
        }

        [TestMethod]
        public void ReadingExcelFile()
        {
            var file = File.OpenRead(@"C:\Users\m.hoshen\source\repos\Exercises\VariousExcercises\ReadingExcelFile\Files\QualitativeAssessmentTemplateSample.xlsx");
            var template = ExcelHelper.ExcelToRiskCategoryTemplate(file, Guid.Parse("22F2A5C6-D200-4BF9-967E-0B6571EB5721"));
        }
        public string GetXML(string filename)
        {
            using (DataSet ds = new DataSet())
            {
                ds.Tables.Add(this.ReadExcelFile(filename));
                return ds.GetXml();
            }
        }

        private DataTable ReadExcelFile(string filename)
        {
            // Initialize an instance of DataTable
            DataTable dt = new DataTable();

            try
            {
                // Use SpreadSheetDocument class of Open XML SDK to open excel file
                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(filename, false))
                {
                    // Get Workbook Part of Spread Sheet Document
                    WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;

                    // Get all sheets in spread sheet document 
                    IEnumerable<Sheet> sheetcollection = spreadsheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().Elements<Sheet>();

                    // Get relationship Id
                    string relationshipId = sheetcollection.First().Id.Value;

                    // Get sheet1 Part of Spread Sheet Document
                    WorksheetPart worksheetPart = (WorksheetPart)spreadsheetDocument.WorkbookPart.GetPartById(relationshipId);

                    // Get Data in Excel file
                    SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();
                    IEnumerable<Row> rowcollection = sheetData.Descendants<Row>();

                    var coll = sheetData.Descendants<Column>();

                    if (rowcollection.Count() == 0)
                    {
                        return dt;
                    }

                    // Add columns
                    foreach (Cell cell in rowcollection.ElementAt(0))
                    {
                        dt.Columns.Add(GetValueOfCell(spreadsheetDocument, cell));
                    }

                    // Add rows into DataTable
                    foreach (Row row in rowcollection)
                    {
                        DataRow temprow = dt.NewRow();
                        int columnIndex = 0;
                        foreach (Cell cell in row.Descendants<Cell>())
                        {
                            // Get Cell Column Index
                            int cellColumnIndex = GetColumnIndex(GetColumnName(cell.CellReference));

                            if (columnIndex < cellColumnIndex)
                            {
                                do
                                {
                                    temprow[columnIndex] = string.Empty;
                                    columnIndex++;
                                }

                                while (columnIndex < cellColumnIndex);
                            }

                            temprow[columnIndex] = GetValueOfCell(spreadsheetDocument, cell);
                            columnIndex++;
                        }

                        // Add the row to DataTable
                        // the rows include header row
                        dt.Rows.Add(temprow);
                    }
                }

                // Here remove header row
                dt.Rows.RemoveAt(0);
                return dt;
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message);
            }
        }
        /// <summary>
        ///  Get Value of Cell 
        /// </summary>
        /// <param name="spreadsheetdocument">SpreadSheet Document Object</param>
        /// <param name="cell">Cell Object</param>
        /// <returns>The Value in Cell</returns>
        private static string GetValueOfCell(SpreadsheetDocument spreadsheetdocument, Cell cell)
        {
            // Get value in Cell
            SharedStringTablePart sharedString = spreadsheetdocument.WorkbookPart.SharedStringTablePart;
            if (cell.CellValue == null)
            {
                return string.Empty;
            }

            string cellValue = cell.CellValue.InnerText;

            // The condition that the Cell DataType is SharedString
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return sharedString.SharedStringTable.ChildElements[int.Parse(cellValue)].InnerText;
            }
            else
            {
                return cellValue;
            }
        }

        /// <summary>
        /// Get Column Name From given cell name
        /// </summary>
        /// <param name="cellReference">Cell Name(For example,A1)</param>
        /// <returns>Column Name(For example, A)</returns>
        private string GetColumnName(string cellReference)
        {
            // Create a regular expression to match the column name of cell
            Regex regex = new Regex("[A-Za-z]+");
            Match match = regex.Match(cellReference);
            return match.Value;
        }

        /// <summary>
        /// Get Index of Column from given column name
        /// </summary>
        /// <param name="columnName">Column Name(For Example,A or AA)</param>
        /// <returns>Column Index</returns>
        private int GetColumnIndex(string columnName)
        {
            int columnIndex = 0;
            int factor = 1;

            // From right to left
            for (int position = columnName.Length - 1; position >= 0; position--)
            {
                // For letters
                if (Char.IsLetter(columnName[position]))
                {
                    columnIndex += factor * ((columnName[position] - 'A') + 1) - 1;
                    factor *= 26;
                }
            }

            return columnIndex;
        }
    }
}
