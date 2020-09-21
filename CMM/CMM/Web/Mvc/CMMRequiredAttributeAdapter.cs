namespace CMM.Web.Mvc
{
    using CMM.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class CMMRequiredAttributeAdapter : DataAnnotationsModelValidator<CMMRequiredAttribute>
    {
        public CMMRequiredAttributeAdapter(ModelMetadata metadata, ControllerContext context, CMMRequiredAttribute attribute) : base(metadata, context, attribute)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new ModelClientValidationRule[] { new ModelClientValidationRequiredRule(base.ErrorMessage) };
        }
    }
}

