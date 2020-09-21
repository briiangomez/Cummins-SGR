namespace CMM.Web.Mvc
{
    using CMM.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class CMMCompareAttributeAdapter : DataAnnotationsModelValidator<CMMCompareAttribute>
    {
        public CMMCompareAttributeAdapter(ModelMetadata metadata, ControllerContext context, CMMCompareAttribute attribute) : base(metadata, context, attribute)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            ModelClientValidationRule rule = new ModelClientValidationRule {
                ErrorMessage = base.ErrorMessage,
                ValidationType = "compare"
            };
            rule.ValidationParameters["compareTo"] = base.Attribute.CompareTo;
            return new ModelClientValidationRule[] { rule };
        }
    }
}

