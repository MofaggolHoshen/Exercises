using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace ReadingExcelFile.Helper
{
    public static class ExcelHelper
    {
        public static RiskCategoryTemplate ExcelToRiskCategoryTemplate(Stream excelStream, Guid tenantId)
        {
            //SpreadsheetDocument myWorkbook = SpreadsheetDocument.Open(@"C:\Users\m.hoshen\source\repos\Exercises\VariousExcercises\ReadingExcelFile\Files\Book1.xlsx", true);
            SpreadsheetDocument myWorkbook = SpreadsheetDocument.Open(excelStream, false);
            WorkbookPart workbookPart = myWorkbook.WorkbookPart;
            Workbook workbook = workbookPart.Workbook;
            IEnumerable<Sheet> sheets = workbook.Descendants<Sheet>();
            RiskCategoryTemplate template = new RiskCategoryTemplate();

            //Building error messages 
            StringBuilder errorStrBuilder = new StringBuilder();
            bool isErrorOccurred = false;

            foreach (var sheet in sheets)
            {
                WorksheetPart worksheetPart = (WorksheetPart)workbookPart.GetPartById(sheet.Id);
                IEnumerable<Row> rows = worksheetPart.Worksheet.GetFirstChild<SheetData>().Descendants<Row>();
                SharedStringTablePart sharedStringPart = workbookPart.SharedStringTablePart;
                SharedStringItem[] values = sharedStringPart.SharedStringTable.Elements<SharedStringItem>().ToArray();

                int relatedRiskSubCategoryTemplateIndex = 0;
                int relatedIndicatorTemplateIndex = 0;
                int relatedSubIndicatorTemplateIndex = 0;
                int relatedSubIndicatorParameterTemplatesIndex = 0;
                int relatedMitigationTemplatesIndex = 0;

                int relatedRiskSubCategoriesTemplateOderId = 1;
                int relatedIndicatorTemplateOrderId = 1;
                int relatedSubIndicatorTemplateOrderId = 1;
                int relatedSubIndicatorParameterTemplatesOrderId = 1;
                int relatedRiskMitigationTemplateOrderId = 1;
                int relatedMitigationParameterTemplateOrderId = 1;

                //Give a fixed culture, because user culture can be German or something else then string to decimal conversion will be problem 
                CultureInfo provider = new CultureInfo("en-US");

                foreach (var row in rows)
                {
                    if (row.RowIndex > 1)
                    {
                        foreach (Cell cell in row.ChildElements)
                        {
                            // RelatedRiskCategoryTemplate
                            if (cell.CellReference.Value.StartsWith("A")) // RiskCategoryTemplate
                            {
                                var riskCategoryTemplateName = GetValue(cell, values);

                                if (!String.IsNullOrEmpty(riskCategoryTemplateName))
                                {
                                    try
                                    {
                                        // If error is occured, no need to assign properties
                                        if (!isErrorOccurred)
                                        {
                                            template.Name = riskCategoryTemplateName;
                                            template.TenantId = tenantId;
                                            template.IsActive = true;
                                            template.DateCreatedUTC = DateTime.UtcNow;
                                            template.DateModifiedUTC = DateTime.UtcNow;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {riskCategoryTemplateName}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RiskCategoryExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                }
                            }
                            // RelatedRiskCateforyTemplate Weight
                            else if (cell.CellReference.Value.StartsWith("B"))
                            {
                                var riskCategoryTemplateWeight = GetValue(cell, values);

                                if (!String.IsNullOrEmpty(riskCategoryTemplateWeight))
                                {
                                    try
                                    {
                                        var weight = decimal.Parse(riskCategoryTemplateWeight, provider);

                                        // If error is occured, no need to assign properties
                                        if (!isErrorOccurred)
                                        {
                                            template.Weight = weight;
                                        }
                                    }

                                    catch (FormatException ex)
                                    {
                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {riskCategoryTemplateWeight}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RiskCategoryFormatExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                    catch (Exception ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {riskCategoryTemplateWeight}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RiskCategoryExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                }

                            }
                            // RelatedRiskSubCategoryTemplate
                            else if (cell.CellReference.Value.StartsWith("C"))
                            {
                                var relatedRiskSubCategoryTemplateName = GetValue(cell, values);

                                if (!String.IsNullOrEmpty(relatedRiskSubCategoryTemplateName))
                                {
                                    try
                                    {
                                        // If error is occured, no need to create object
                                        if (!isErrorOccurred)
                                        {
                                            template.RelatedRiskSubCategoriesTemplate.Add(new RelatedRiskSubCategoryTemplate()
                                            {
                                                Name = relatedRiskSubCategoryTemplateName,
                                                OrderId = relatedRiskSubCategoriesTemplateOderId,
                                            });

                                            relatedRiskSubCategoriesTemplateOderId++;

                                            //Reset OrderId for relatedIndicatorTemplateOrderId, new set of RelatedIndicatorTemplate will be created.
                                            relatedIndicatorTemplateOrderId = 1;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedRiskSubCategoryTemplateName}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedRiskSubCategoryExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                }

                            }
                            // RelatedRiskSubCategoryTemplate Weight
                            else if (cell.CellReference.Value.StartsWith("D"))
                            {
                                var relatedRiskSubCategoryTemplateWeight = GetValue(cell, values);

                                if (!String.IsNullOrEmpty(relatedRiskSubCategoryTemplateWeight))
                                {
                                    try
                                    {
                                        var weight = decimal.Parse(relatedRiskSubCategoryTemplateWeight, provider);
                                        // If error is occured, no need to create object
                                        if (!isErrorOccurred)
                                        {
                                            template.RelatedRiskSubCategoriesTemplate.Last().Weight = weight;
                                        }
                                    }
                                    catch (FormatException ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedRiskSubCategoryTemplateWeight}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedRiskSubCategoryFormatExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                    catch (Exception ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedRiskSubCategoryTemplateWeight}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedRiskSubCategoryExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                }

                            }
                            // RelatedIndicatorTemplate
                            else if (cell.CellReference.Value.StartsWith("E"))
                            {
                                var relatedIndicatorTemplateVlaueName = GetValue(cell, values);

                                if (!String.IsNullOrEmpty(relatedIndicatorTemplateVlaueName))
                                {
                                    try
                                    {
                                        // If error is occured, no need to create object
                                        if (!isErrorOccurred)
                                        {
                                            // We need to know the last index for adding the value. Last index is equal to length of list -1 
                                            relatedRiskSubCategoryTemplateIndex = template.RelatedRiskSubCategoriesTemplate.Count() - 1;

                                            template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                    .RelatedIndicatorsTemplate.Add(new RelatedIndicatorTemplate()
                                                                                    {
                                                                                        Name = relatedIndicatorTemplateVlaueName,
                                                                                        OrderId = relatedIndicatorTemplateOrderId
                                                                                    });
                                            relatedIndicatorTemplateOrderId++;

                                            //Reset OrderId for relatedSubIndicatorTemplateOrderId, new set of RelatedSubIndicatorTemplate will be created.
                                            relatedSubIndicatorTemplateOrderId = 1;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedIndicatorTemplateVlaueName}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedIndicatorExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                }
                            }
                            // RelatedIndicatorTemplate Weight
                            else if (cell.CellReference.Value.StartsWith("F"))
                            {
                                var relatedIndicatorTemplateVlaueWeight = GetValue(cell, values);

                                if (!String.IsNullOrEmpty(relatedIndicatorTemplateVlaueWeight))
                                {
                                    try
                                    {
                                        var weight = decimal.Parse(relatedIndicatorTemplateVlaueWeight, provider);

                                        template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                        .RelatedIndicatorsTemplate.Last().Weight = weight;
                                    }
                                    catch (FormatException ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedIndicatorTemplateVlaueWeight}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedRiskSubCategoryFormatExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                    catch (Exception ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedIndicatorTemplateVlaueWeight}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedIndicatorExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                }
                            }
                            // RelatedSubIndicatorTemplate
                            else if (cell.CellReference.Value.StartsWith("G"))
                            {
                                var relatedSubIndicatorTemplateName = GetValue(cell, values);

                                if (!String.IsNullOrEmpty(relatedSubIndicatorTemplateName))
                                {
                                    try
                                    {
                                        // If error is occured, no need to create object
                                        if (!isErrorOccurred)
                                        {
                                            // We need to know the last index for adding the value. Last index is equal to length of list -1 
                                            relatedIndicatorTemplateIndex = template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                                                     .RelatedIndicatorsTemplate.Count() - 1;

                                            template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                    .RelatedIndicatorsTemplate[relatedIndicatorTemplateIndex]
                                                    .RelatedSubIndicatorTemplates.Add(new RelatedSubIndicatorTemplate()
                                                    {
                                                        Name = relatedSubIndicatorTemplateName, //Name
                                                        OrderId = relatedSubIndicatorTemplateOrderId,
                                                    });

                                            relatedSubIndicatorTemplateOrderId++;

                                            //Reset OrderId for relatedRiskParameterTemplateOrderId, new set of RelatedRiskParameterTemplate will be created.
                                            relatedSubIndicatorParameterTemplatesOrderId = 1;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedSubIndicatorTemplateName}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedSubIndicatorExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                }
                            }
                            // RelatedSubIndicatorTemplate Impact Text
                            else if (cell.CellReference.Value.StartsWith("H"))
                            {
                                var relatedSubIndicatorTemplateImpactText = GetValue(cell, values);

                                if (!String.IsNullOrEmpty(relatedSubIndicatorTemplateImpactText))
                                {
                                    try
                                    {
                                        template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                .RelatedIndicatorsTemplate[relatedIndicatorTemplateIndex]
                                                .RelatedSubIndicatorTemplates.Last()
                                                .ImpactText = relatedSubIndicatorTemplateImpactText;

                                    }
                                    catch (Exception ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedSubIndicatorTemplateImpactText}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedSubIndicatorExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                }
                            }
                            // RelatedSubIndicatorTemplate Weight
                            else if (cell.CellReference.Value.StartsWith("I"))
                            {
                                var relatedSubIndicatorTemplateWeight = GetValue(cell, values);

                                if (!String.IsNullOrEmpty(relatedSubIndicatorTemplateWeight))
                                {
                                    try
                                    {
                                        var weight = decimal.Parse(relatedSubIndicatorTemplateWeight, provider);

                                        template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                        .RelatedIndicatorsTemplate[relatedIndicatorTemplateIndex]
                                                                                        .RelatedSubIndicatorTemplates.Last().Weight = weight;

                                    }
                                    catch (FormatException ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedSubIndicatorTemplateWeight}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedSubIndicatorFormatExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                    catch (Exception ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedSubIndicatorTemplateWeight}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedSubIndicatorExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                }
                            }
                            // RelatedSubIndicatorParameterTemplate
                            else if (cell.CellReference.Value.StartsWith("J"))
                            {
                                var relatedRiskParameterTemplateName = GetValue(cell, values);

                                if (!String.IsNullOrEmpty(relatedRiskParameterTemplateName))
                                {
                                    try
                                    {
                                        // If error is occured, no need to create object
                                        if (!isErrorOccurred)
                                        {
                                            // We need to know the last index for adding the value. Last index is equal to length of list -1 
                                            relatedSubIndicatorTemplateIndex = template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                                                        .RelatedIndicatorsTemplate[relatedIndicatorTemplateIndex]
                                                                                                                        .RelatedSubIndicatorTemplates.Count() - 1;

                                            template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                     .RelatedIndicatorsTemplate[relatedIndicatorTemplateIndex]
                                                                                     .RelatedSubIndicatorTemplates[relatedSubIndicatorTemplateIndex]
                                                                                     .RelatedSubIndicatorParameterTemplates.Add(new RelatedSubIndicatorParameterTemplate()
                                                                                     {
                                                                                         Name = relatedRiskParameterTemplateName, //Name 
                                                                                         OrderId = relatedSubIndicatorParameterTemplatesOrderId,
                                                                                     });
                                            relatedSubIndicatorParameterTemplatesOrderId++;

                                            //Reset OrderId for relatedRiskMitigationTemplateOrderId, new set of RelatedMitigationTemplate will be created.
                                            relatedRiskMitigationTemplateOrderId = 1;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedRiskParameterTemplateName}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedSubIndicatorParameterExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                }

                            }
                            // RelatedSubIndicatorParameterTemplate RiskFactor
                            else if (cell.CellReference.Value.StartsWith("K"))
                            {
                                var relatedRiskParameterTemplateRiskFactor = GetValue(cell, values);

                                if (!String.IsNullOrEmpty(relatedRiskParameterTemplateRiskFactor))
                                {
                                    try
                                    {
                                        var riskFactor = decimal.Parse(relatedRiskParameterTemplateRiskFactor, provider);

                                        template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                             .RelatedIndicatorsTemplate[relatedIndicatorTemplateIndex]
                                                                                             .RelatedSubIndicatorTemplates[relatedSubIndicatorTemplateIndex]
                                                                                             .RelatedSubIndicatorParameterTemplates.Last().RiskFactor = riskFactor;
                                    }
                                    catch (InvalidOperationException ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedRiskParameterTemplateRiskFactor}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedSubIndicatorParameterInvalidOperationExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                    catch (FormatException ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedRiskParameterTemplateRiskFactor}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedSubIndicatorParameterFormatExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                    catch (Exception ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedRiskParameterTemplateRiskFactor}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedSubIndicatorParameterExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                }
                            }
                            // RelatedSubIndicatorParameterTemplate Eligibility Relavant
                            else if (cell.CellReference.Value.StartsWith("L"))
                            {
                                var relatedRiskParameterTemplateEligibility = GetValue(cell, values);

                                if (!String.IsNullOrEmpty(relatedRiskParameterTemplateEligibility))
                                {
                                    try
                                    {
                                        var isEligibilityCheck = relatedRiskParameterTemplateEligibility.ToLower().Trim().Equals("y");

                                        template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                             .RelatedIndicatorsTemplate[relatedIndicatorTemplateIndex]
                                                                                             .RelatedSubIndicatorTemplates[relatedSubIndicatorTemplateIndex]
                                                                                             .RelatedSubIndicatorParameterTemplates.Last().IsEligibilityCheckRelevant = isEligibilityCheck;
                                    }
                                    catch (Exception ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedRiskParameterTemplateEligibility}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedSubIndicatorParameterExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                }
                            }
                            // RelatedMitigationTemplate
                            else if (cell.CellReference.Value.StartsWith("M"))
                            {
                                var relatedMitigationTemplateValue = GetValue(cell, values);

                                if (!String.IsNullOrEmpty(relatedMitigationTemplateValue))
                                {
                                    try
                                    {
                                        // If error is occured, no need to create object
                                        if (!isErrorOccurred)
                                        {

                                            var isMitigation = relatedMitigationTemplateValue.Count() > 0;
                                            // Set isMitigation flag 
                                            if (isMitigation)
                                            {
                                                template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                             .RelatedIndicatorsTemplate[relatedIndicatorTemplateIndex]
                                                                                             .RelatedSubIndicatorTemplates[relatedSubIndicatorTemplateIndex]
                                                                                             .RelatedSubIndicatorParameterTemplates.Last().IsMitigatable = isMitigation;
                                            }

                                            // Setting up mitigation question
                                            if (relatedMitigationTemplateValue.Count() > 1)
                                            {
                                                // We need to know the last index for adding the value. Last index is equal to length of list -1 
                                                relatedSubIndicatorParameterTemplatesIndex = template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                         .RelatedIndicatorsTemplate[relatedIndicatorTemplateIndex]
                                                                                         .RelatedSubIndicatorTemplates[relatedSubIndicatorTemplateIndex]
                                                                                         .RelatedSubIndicatorParameterTemplates.Count() - 1;

                                                template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                         .RelatedIndicatorsTemplate[relatedIndicatorTemplateIndex]
                                                                                         .RelatedSubIndicatorTemplates[relatedSubIndicatorTemplateIndex]
                                                                                         .RelatedSubIndicatorParameterTemplates[relatedSubIndicatorParameterTemplatesIndex]
                                                                                         .RelatedMitigationTemplates.Add(new RelatedMitigationTemplate()
                                                                                         {
                                                                                             Name = relatedMitigationTemplateValue,
                                                                                             OrderId = relatedRiskMitigationTemplateOrderId
                                                                                         });
                                            }
                                            relatedRiskMitigationTemplateOrderId++;

                                            //Reset OrderId for relatedMitigationParameterTemplateOrderId, new set of RelatedMitigationParameterTemplate will be created.
                                            relatedMitigationParameterTemplateOrderId = 1;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedMitigationTemplateValue}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedMitigationExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                }
                            }
                            // RelatedMitigationParameterTemplate
                            else if (cell.CellReference.Value.StartsWith("N"))
                            {
                                var relatedMitigationParameterTemplateName = GetValue(cell, values);

                                if (!String.IsNullOrEmpty(relatedMitigationParameterTemplateName))
                                {
                                    try
                                    {
                                        // If error is occured, no need to create object
                                        if (!isErrorOccurred)
                                        {
                                            // We need to know the last index for adding the value. Last index is equal to length of list -1 
                                            relatedMitigationTemplatesIndex = template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                      .RelatedIndicatorsTemplate[relatedIndicatorTemplateIndex]
                                                                                      .RelatedSubIndicatorTemplates[relatedSubIndicatorTemplateIndex]
                                                                                      .RelatedSubIndicatorParameterTemplates[relatedSubIndicatorParameterTemplatesIndex]
                                                                                      .RelatedMitigationTemplates.Count() - 1;

                                            template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                     .RelatedIndicatorsTemplate[relatedIndicatorTemplateIndex]
                                                                                     .RelatedSubIndicatorTemplates[relatedSubIndicatorTemplateIndex]
                                                                                     .RelatedSubIndicatorParameterTemplates[relatedSubIndicatorParameterTemplatesIndex]
                                                                                     .RelatedMitigationTemplates[relatedMitigationTemplatesIndex]
                                                                                     .RelatedMitigationParameterTemplates.Add(new RelatedMitigationParameterTemplate()
                                                                                     {
                                                                                         Name = relatedMitigationParameterTemplateName,
                                                                                         OrderId = relatedMitigationParameterTemplateOrderId,
                                                                                         //MitigationFactor = mitigationFactor
                                                                                     });

                                            relatedMitigationParameterTemplateOrderId++;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedMitigationParameterTemplateName}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedMitigationParameterExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                }
                            }
                            // RelatedMitigationParameter Factor
                            else if (cell.CellReference.Value.StartsWith("O"))
                            {
                                var relatedMitigationParameterTemplateMitigationFactor = GetValue(cell, values);

                                if (!String.IsNullOrEmpty(relatedMitigationParameterTemplateMitigationFactor))
                                {
                                    try
                                    {
                                        string mitigationFactorTextSubStr;
                                        decimal mitigationFactor;

                                        //Checking, % has given with MitigationFactor value or not 
                                        if (relatedMitigationParameterTemplateMitigationFactor.Contains('%'))
                                        {
                                            mitigationFactorTextSubStr = relatedMitigationParameterTemplateMitigationFactor.Substring(0, relatedMitigationParameterTemplateMitigationFactor.IndexOf('%'));
                                             mitigationFactor = (decimal.Parse(mitigationFactorTextSubStr, provider) / 100);
                                        }
                                        // Excel cell fomat percentage 
                                        else
                                        {
                                            mitigationFactor = (decimal.Parse(relatedMitigationParameterTemplateMitigationFactor, provider));
                                        }

                                        // If error is occured, no need to create object
                                        if (!isErrorOccurred)
                                        {
                                            template.RelatedRiskSubCategoriesTemplate[relatedRiskSubCategoryTemplateIndex]
                                                                                     .RelatedIndicatorsTemplate[relatedIndicatorTemplateIndex]
                                                                                     .RelatedSubIndicatorTemplates[relatedSubIndicatorTemplateIndex]
                                                                                     .RelatedSubIndicatorParameterTemplates[relatedSubIndicatorParameterTemplatesIndex]
                                                                                     .RelatedMitigationTemplates[relatedMitigationTemplatesIndex]
                                                                                     .RelatedMitigationParameterTemplates.Last().MitigationFactor = mitigationFactor;
                                        }
                                    }
                                    catch (InvalidOperationException ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedMitigationParameterTemplateMitigationFactor}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedMitigationParameterInvalidOperationExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                    catch (FormatException ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedMitigationParameterTemplateMitigationFactor}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedMitigationParameterFormatExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                    catch (Exception ex)
                                    {
                                        //logger.LogError(ex, ex.Message);

                                        errorStrBuilder.AppendLine($"Row: {row.RowIndex}.");
                                        errorStrBuilder.AppendLine($"Cell: {cell.CellReference.Value}.");
                                        errorStrBuilder.AppendLine($"Given Text: {relatedMitigationParameterTemplateMitigationFactor}.");
                                        //errorStrBuilder.AppendLine($"Error Message: {stringLocalizer["RelatedMitigationParameterExceptionMgs"]}");
                                        errorStrBuilder.AppendLine();
                                        isErrorOccurred = true;

                                        continue;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (isErrorOccurred)
            {
                throw new Exception(errorStrBuilder.ToString());
            }

            return template;
        }

        private static String GetValue(Cell cell, SharedStringItem[] values)
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
    }
}
