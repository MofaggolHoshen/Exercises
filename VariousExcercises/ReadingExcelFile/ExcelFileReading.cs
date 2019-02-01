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
        public void TestMethod1()
        {
            SpreadsheetDocument myWorkbook = SpreadsheetDocument.Open(@"C:\Users\m.hoshen\source\repos\Exercises\VariousExcercises\ReadingExcelFile\Files\Book1.xlsx", true);
            WorkbookPart workbookPart = myWorkbook.WorkbookPart;
            Workbook workbook = workbookPart.Workbook;
            IEnumerable<Sheet> sheets = workbook.Descendants<Sheet>();

            foreach (var sheet in sheets)
            {
                WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                IEnumerable<Row> rows = worksheetPart.Worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                SharedStringTablePart sharedStringPart = workbookPart.SharedStringTablePart;
                SharedStringItem[] values = sharedStringPart.SharedStringTable.Elements<SharedStringItem>().ToArray();

                RiskCategoryTemplate template = template = new RiskCategoryTemplate();

                int relatedRiskSubCategoryTemplateIndex = 0;
                int relatedIndicatorTemplateIndex = 0;
                int relatedSubIndicatorTemplateIndex = 0;

                //var mergeCells = worksheetPart.Worksheet.Descendants<MergeCells>().OrderBy(i => i.ChildElements).ToList();

                //foreach (MergeCells mergeCell in mergeCells)
                //{
                //    var mergeCel = mergeCell.ChildElements.OrderBy(i=> (i as MergeCell).Reference.Value);

                //    foreach (MergeCell item in mergeCel)
                //    {
                //        var refValue = item.Reference.Value;
                //    }
                //}

                foreach (var row in rows)
                {
                    if (row.RowIndex > 1)
                    {
                        foreach (Cell cell in row.ChildElements)
                        {
                            try
                            {
                                if (cell.CellReference.Value.StartsWith("A")) // RiskCategoryTemplate
                                {
                                    var riskCategoryTemplateValue = GetValue(cell, values);

                                    if (!String.IsNullOrEmpty(riskCategoryTemplateValue))
                                    {
                                        template.Name = riskCategoryTemplateValue;
                                    }

                                }
                                else if (cell.CellReference.Value.StartsWith("B")) // RelatedRiskSubCategoryTemplate
                                {
                                    var relatedRiskSubCategoryTemplateValue = GetValue(cell, values);

                                    if (!String.IsNullOrEmpty(relatedRiskSubCategoryTemplateValue))
                                    {
                                        template.RelatedRiskSubCategoriesTemplate.Add(new RelatedRiskSubCategoryTemplate()
                                        {
                                            Name = relatedRiskSubCategoryTemplateValue
                                        });
                                    }

                                }
                                else if (cell.CellReference.Value.StartsWith("C")) // RelatedIndicatorTemplate
                                {
                                    var relatedIndicatorTemplateVlaue = GetValue(cell, values);

                                    if (!String.IsNullOrEmpty(relatedIndicatorTemplateVlaue))
                                    {
                                        relatedRiskSubCategoryTemplateIndex = template.RelatedRiskSubCategoriesTemplate.Count() - 1;

                                        template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                .RelatedIndicatorsTemplate.Add(new RelatedIndicatorTemplate()
                                                                                {
                                                                                    Name = relatedIndicatorTemplateVlaue
                                                                                });

                                    }

                                }
                                else if (cell.CellReference.Value.StartsWith("D")) // RelatedSubIndicatorTemplate
                                {
                                    var relatedSubIndicatorTemplateValue = GetValue(cell, values);

                                    if (!String.IsNullOrEmpty(relatedSubIndicatorTemplateValue))
                                    {
                                        relatedIndicatorTemplateIndex = template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                                                 .RelatedIndicatorsTemplate.Count() - 1;

                                        template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                 .RelatedIndicatorsTemplate[relatedIndicatorTemplateIndex]
                                                                                 .RelatedSubIndicatorTemplates.Add(new RelatedSubIndicatorTemplate()
                                                                                 {
                                                                                     Name = relatedSubIndicatorTemplateValue
                                                                                 });


                                    }
                                }
                                else if (cell.CellReference.Value.StartsWith("E")) // RelatedRiskParameterTemplate
                                {
                                    var relatedRiskParameterTemplateValue = GetValue(cell, values);

                                    if (!String.IsNullOrEmpty(relatedRiskParameterTemplateValue))
                                    {
                                        relatedSubIndicatorTemplateIndex = template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                                                    .RelatedIndicatorsTemplate[relatedIndicatorTemplateIndex]
                                                                                                                    .RelatedSubIndicatorTemplates.Count() - 1;

                                        template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                 .RelatedIndicatorsTemplate[relatedIndicatorTemplateIndex]
                                                                                 .RelatedSubIndicatorTemplates[relatedSubIndicatorTemplateIndex]
                                                                                 .RelatedRiskParametersTemplate.Add(new RelatedRiskParameterTemplate()
                                                                                 {
                                                                                     Name = relatedRiskParameterTemplateValue
                                                                                 });
                                    }
                                }
                                //else if (cell.CellReference.Value.StartsWith("F")) // Mitigarion
                                //{
                                //    var mitigationValue = GetValue(cell, values);
                                //}
                                //else if (cell.CellReference.Value.StartsWith("G")) // Mitigatrion Parameter
                                //{
                                //    var mitigationParameterValue = GetValue(cell, values);
                                //}
                            }
                            catch (Exception ex)
                            {

                                throw;
                            }
                        }
                    }
                }

            }


            //var xmlStr = GetXML(@"C:\Users\m.hoshen\source\repos\Exercises\VariousExcercises\ReadingExcelFile\Files\Book1.xlsx");

        }

        private String GetValue(Cell cell, SharedStringItem[] values)
        {
            string value = String.Empty;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                int index = int.Parse(cell.CellValue.Text);
                value = values[index].InnerText;
            }
            else if (cell.DataType != null && cell.DataType.Value == CellValues.InlineString)
            {
                value = cell.InnerText;
            }
            else
            {
                if (cell.CellValue != null)
                {
                    value = cell.CellValue.Text;
                }
            }

            if (cell.CellFormula != null)
            {
                value = cell.CellFormula.Text;
            }

            return value.Trim();
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
