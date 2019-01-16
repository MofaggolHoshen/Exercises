using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ReadingExcelFile
{
    // ToDo: This Template stuff must be linked to 
    // BusinessActivity and ClientCategorization with m:n table

    public class RiskCategoryTemplate 
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 100, ErrorMessage = "{0} must be less than {1} characters.")]
        public string Name { get; set; }
        [ForeignKey("Tenant")]

        //[ForeignKey("CaseStepSetup")]
        //public int CaseStepSetupId { get; set; }

        //public CaseStepSetup CaseStepSetup { get; set; }
               
        // We should not Include the real RelatedRiskCategories, it might be to heavy to query. It's not necessary
        //public List<RelatedRiskCategory> RelatedRiskCategories { get; set; }

        public List<RelatedRiskSubCategoryTemplate> RelatedRiskSubCategoriesTemplate { get; set; }
              
        public DateTime DateCreatedUTC { get; set; }
        public DateTime DateModifiedUTC { get; set; }
        public bool IsActive { get; set; }

        public RiskCategoryTemplate() {
            RelatedRiskSubCategoriesTemplate = new List<RelatedRiskSubCategoryTemplate>();
        }
        public RiskCategoryTemplate(int id, string name, Guid tenantId)
        {
            Id = id;
            Name = name;
            //CaseStepSetupId = caseStepSetupId;
            DateCreatedUTC = DateTime.Now;
            DateCreatedUTC = DateTime.Now;
            IsActive = true;
        }
    }

    public class RelatedRiskSubCategoryTemplate
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 100, ErrorMessage = "{0} must be less than {1} characters.")]
        public string Name { get; set; }
        public int OrderId { get; set; }

        [ForeignKey("RelatedRiskSubCategoryType")]
        public int RelatedRiskSubCategoryTypeId { get; set; }
        public RelatedRiskSubCategoryType RelatedRiskSubCategoryType { get; set; }

        [ForeignKey("RiskCategoryTemplate")]
        public int RiskCategoryTemplateId { get; set; }
        public RiskCategoryTemplate RiskCategoryTemplate { get; set; }

        public List<RelatedIndicatorTemplate> RelatedIndicatorsTemplate { get; set; }

        public RelatedRiskSubCategoryTemplate() {
            RelatedIndicatorsTemplate = new List<RelatedIndicatorTemplate>();
        }
        public RelatedRiskSubCategoryTemplate(int id, string name, int orderId, int riskCategoryTemplateId, int relatedRistSubCategoryTypeId)
        {
            Id = id;
            Name = name;
            OrderId = orderId;
            RiskCategoryTemplateId = riskCategoryTemplateId;
            RelatedRiskSubCategoryTypeId = relatedRistSubCategoryTypeId;
        }
    }

    public class RelatedIndicatorTemplate
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 100, ErrorMessage = "{0} must be less than {1} characters.")]
        public string Name { get; set; }
        public int OrderId { get; set; }
        [ForeignKey("RelatedRiskSubCategoryTemplate")]
        public int RelatedRiskSubCategoryTemplateId { get; set; }
        public RelatedRiskSubCategoryTemplate RelatedRiskSubCategoryTemplate { get; set; }
        public List<RelatedSubIndicatorTemplate> RelatedSubIndicatorTemplates { get; set; }

        public RelatedIndicatorTemplate() {
            RelatedSubIndicatorTemplates = new List<RelatedSubIndicatorTemplate>();
        }
        public RelatedIndicatorTemplate(int id, string name, int orderId, int relatedRiskSubCategoryTemplateId)
        {
            Id = id;
            Name = name;
            OrderId = orderId;
            RelatedRiskSubCategoryTemplateId = relatedRiskSubCategoryTemplateId;
        }
    }

    public class RelatedSubIndicatorTemplate
    {
        // Question
        public int Id { get; set; }
        [StringLength(maximumLength: 100, ErrorMessage = "{0} must be less than {1} characters.")]
        public string Name { get; set; }
        public int OrderId { get; set; }
        public string ImpactText { get; set; }

        [ForeignKey("RelatedIndicatorTemplate")]
        public int RelatedIndicatorTemplateId { get; set; }
        public RelatedIndicatorTemplate RelatedIndicatorTemplate { get; set; }

        public List<RelatedRiskParameterTemplate> RelatedRiskParametersTemplate { get; set; }

        public RelatedSubIndicatorTemplate() {
            RelatedRiskParametersTemplate = new List<RelatedRiskParameterTemplate>();
        }
        public RelatedSubIndicatorTemplate(int id, string name, int orderId, int relatedIndicatorTemplateId, string impactText)
        {
            Id = id;
            Name = name;
            OrderId = orderId;
            RelatedIndicatorTemplateId = relatedIndicatorTemplateId;
            ImpactText = impactText;
        }
    }

    public class RelatedRiskParameterTemplate
    {
        // Answer
        public int Id { get; set; }
        [StringLength(maximumLength: 100, ErrorMessage = "{0} must be less than {1} characters.")]
        public string Name { get; set; }
        public int OrderId { get; set; }

        [ForeignKey("RelatedSubIndicatorTemplate")]
        public int RelatedSubIndicatorTemplateId { get; set; }
        public RelatedSubIndicatorTemplate RelatedSubIndicatorTemplate { get; set; }
        public int Weightage { get; set; }

        public bool IsEligibilityCheckRelevant { get; set; }
        public RelatedRiskParameterTemplate() { }
        public RelatedRiskParameterTemplate(int id, string name, int orderId, int relatedSubIndicatorTemplateId, int weightage, bool isEligibilityCheckRelevant)
        {
            Id = id;
            Name = name;
            OrderId = orderId;
            RelatedSubIndicatorTemplateId = relatedSubIndicatorTemplateId;
            Weightage = weightage;
            IsEligibilityCheckRelevant = isEligibilityCheckRelevant;
        }
    }

    public class RiskCategoryType
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 100, ErrorMessage = "{0} must be less than {1} characters.")]
        public string Name { get; set; }
        public int OrderId { get; set; }

        public RiskCategoryType() { }
        public RiskCategoryType(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public enum RiskCategoryTypeEnum
    {
        GeneralRisk = 1,
        SpecificRisk = 2
    }

    public class RelatedRiskSubCategoryType
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 50, ErrorMessage = "Last Name must be less than {1} characters.")]
        public string Name { get; set; }

        public List<RelatedRiskSubCategoryTemplate> RelatedRiskSubCategoryTemplates { get; set; }

        public RelatedRiskSubCategoryType(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public enum RelatedRiskSubCategoryTypeEnum
    {
        ClientRisk = 1,
        BusinessRisk = 2,
        MarketRisk = 3
    }
}
