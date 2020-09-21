namespace CMM.Web.Mvc
{
    using CMM.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    //using System.Web.Mvc;

    public class CMMRegularExpressionAttributeAdapter : DataAnnotationsModelValidator<CMMRegularExpressionAttribute>
    {
        public CMMRegularExpressionAttributeAdapter(ModelMetadata metadata, ControllerContext context, CMMRegularExpressionAttribute attribute) : base(metadata, context, attribute)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new ModelClientValidationRule[] { new ModelClientValidationRegexRule(base.ErrorMessage, base.Attribute.Pattern) };
        }
    }
}

