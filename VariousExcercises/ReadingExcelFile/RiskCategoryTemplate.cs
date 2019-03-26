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
        public Guid TenantId { get; set; }

        //[ForeignKey("CaseStepSetup")]
        //public int CaseStepSetupId { get; set; }

        //public CaseStepSetup CaseStepSetup { get; set; }

        // We should not Include the real RelatedRiskCategories, it might be to heavy to query. It's not necessary
        //public List<RelatedRiskCategory> RelatedRiskCategories { get; set; }

        public List<RelatedRiskSubCategoryTemplate> RelatedRiskSubCategoriesTemplate { get; set; }

        public DateTime DateCreatedUTC { get; set; }
        public DateTime DateModifiedUTC { get; set; }
        public bool IsActive { get; set; }
        public decimal Weight { get; set; }

        public RiskCategoryTemplate()
        {
            RelatedRiskSubCategoriesTemplate = new List<RelatedRiskSubCategoryTemplate>();
        }
        public RiskCategoryTemplate(int id, string name, Guid tenantId, decimal weight)
        {
            Id = id;
            Name = name;
            TenantId = tenantId;
            //CaseStepSetupId = caseStepSetupId;
            DateCreatedUTC = DateTime.UtcNow;
            DateCreatedUTC = DateTime.UtcNow;
            IsActive = true;
            Weight = weight;
        }
    }

    public class RelatedRiskSubCategoryTemplate
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 100, ErrorMessage = "{0} must be less than {1} characters.")]
        public string Name { get; set; }
        public int OrderId { get; set; }
        public decimal Weight { get; set; }

        [ForeignKey("RelatedRiskSubCategoryType")]
        public int? RelatedRiskSubCategoryTypeId { get; set; }
        public RelatedRiskSubCategoryType RelatedRiskSubCategoryType { get; set; }

        [ForeignKey("RiskCategoryTemplate")]
        public int RiskCategoryTemplateId { get; set; }
        public RiskCategoryTemplate RiskCategoryTemplate { get; set; }

        public List<RelatedIndicatorTemplate> RelatedIndicatorsTemplate { get; set; }

        public RelatedRiskSubCategoryTemplate()
        {
            RelatedIndicatorsTemplate = new List<RelatedIndicatorTemplate>();
        }
        public RelatedRiskSubCategoryTemplate(int id, string name, int orderId, int riskCategoryTemplateId, int relatedRistSubCategoryTypeId, decimal weight)
        {
            Id = id;
            Name = name;
            OrderId = orderId;
            RiskCategoryTemplateId = riskCategoryTemplateId;
            RelatedRiskSubCategoryTypeId = relatedRistSubCategoryTypeId;
            Weight = weight;
        }
    }

    public class RelatedIndicatorTemplate
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 100, ErrorMessage = "{0} must be less than {1} characters.")]
        public string Name { get; set; }
        public int OrderId { get; set; }
        public decimal Weight { get; set; }

        [ForeignKey("RelatedRiskSubCategoryTemplate")]
        public int RelatedRiskSubCategoryTemplateId { get; set; }
        public RelatedRiskSubCategoryTemplate RelatedRiskSubCategoryTemplate { get; set; }
        public List<RelatedSubIndicatorTemplate> RelatedSubIndicatorTemplates { get; set; }

        public RelatedIndicatorTemplate()
        {
            RelatedSubIndicatorTemplates = new List<RelatedSubIndicatorTemplate>();
        }
        public RelatedIndicatorTemplate(int id, string name, int orderId, int relatedRiskSubCategoryTemplateId, decimal weight)
        {
            Id = id;
            Name = name;
            OrderId = orderId;
            RelatedRiskSubCategoryTemplateId = relatedRiskSubCategoryTemplateId;
            Weight = weight;
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
        public decimal Weight { get; set; }

        [ForeignKey("RelatedIndicatorTemplate")]
        public int RelatedIndicatorTemplateId { get; set; }
        public RelatedIndicatorTemplate RelatedIndicatorTemplate { get; set; }

        public List<RelatedSubIndicatorParameterTemplate> RelatedSubIndicatorParameterTemplates { get; set; }

        public RelatedSubIndicatorTemplate()
        {
            RelatedSubIndicatorParameterTemplates = new List<RelatedSubIndicatorParameterTemplate>();
        }
        public RelatedSubIndicatorTemplate(int id, string name, int orderId, int relatedIndicatorTemplateId, string impactText, decimal weight)
        {
            Id = id;
            Name = name;
            OrderId = orderId;
            RelatedIndicatorTemplateId = relatedIndicatorTemplateId;
            ImpactText = impactText;
            Weight = weight;
        }
    }

    public class RelatedSubIndicatorParameterTemplate
    {
        // Answer
        public int Id { get; set; }
        [StringLength(maximumLength: 100, ErrorMessage = "{0} must be less than {1} characters.")]
        public string Name { get; set; }
        public int OrderId { get; set; }

        [ForeignKey("RelatedSubIndicatorTemplate")]
        public int RelatedSubIndicatorTemplateId { get; set; }
        public RelatedSubIndicatorTemplate RelatedSubIndicatorTemplate { get; set; }
        public decimal RiskFactor { get; set; }

        public bool IsEligibilityCheckRelevant { get; set; }

        public bool IsMitigatable { get; set; }

        public List<RelatedMitigationTemplate> RelatedMitigationTemplates { get; set; }

        public RelatedSubIndicatorParameterTemplate()
        {
            RelatedMitigationTemplates = new List<RelatedMitigationTemplate>();
        }
        public RelatedSubIndicatorParameterTemplate(int id, string name, int orderId, int relatedSubIndicatorTemplateId, int weightage, bool isEligibilityCheckRelevant, bool isMitigatable, decimal riskFactor)
        {
            Id = id;
            Name = name;
            OrderId = orderId;
            RelatedSubIndicatorTemplateId = relatedSubIndicatorTemplateId;
            RiskFactor = (decimal)weightage;
            IsEligibilityCheckRelevant = isEligibilityCheckRelevant;
            IsMitigatable = isMitigatable;
            RiskFactor = riskFactor;
        }
    }


    public class RelatedMitigationTemplate
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("RelatedSubIndicatorParameter")]
        public int RelatedSubIndicatorParameterTemplateId { get; set; }

        public RelatedSubIndicatorParameterTemplate RelatedSubIndicatorParameterTemplate { get; set; }

        public List<RelatedMitigationParameterTemplate> RelatedMitigationParameterTemplates { get; set; }
        

        public RelatedMitigationTemplate()
        {
            RelatedMitigationParameterTemplates = new List<RelatedMitigationParameterTemplate>();
        }

        public RelatedMitigationTemplate(int id, string name, int orderId, int relatedSubIndicatorParameterTemplateId)
        {
            Id = id;
            Name = name;
            OrderId = orderId;
            RelatedSubIndicatorParameterTemplateId = relatedSubIndicatorParameterTemplateId;


        }

    }

    public class RelatedMitigationParameterTemplate
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int OrderId { get; set; }
        /// <summary>
        /// Mitigatigation Factor is in percentage. for example, MitigationFactor = 20%
        /// </summary>
        public decimal MitigationFactor { get; set; }

        [ForeignKey("RelatedMitigationTemplate")]
        public int RelatedMitigationTemplateId { get; set; }

        public RelatedMitigationTemplate RelatedMitigationTemplate { get; set; }

        public RelatedMitigationParameterTemplate()
        { }

        public RelatedMitigationParameterTemplate(int id, string name, int orderId, int relatedMitigationTemplateId, decimal mitigationFactor)
        {
            Id = id;
            Name = name;
            OrderId = orderId;
            RelatedMitigationTemplateId = relatedMitigationTemplateId;
            MitigationFactor = mitigationFactor;
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
