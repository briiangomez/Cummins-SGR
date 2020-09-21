namespace CMM.Web.Mvc
{
    using CMM.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class CMMRangeAttributeAdapter : DataAnnotationsModelValidator<CMMRangeAttribute>
    {
        public CMMRangeAttributeAdapter(ModelMetadata metadata, ControllerContext context, CMMRangeAttribute attribute) : base(metadata, context, attribute)
        {
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new ModelClientValidationRule[] { new ModelClientValidationRangeRule(base.ErrorMessage, base.Attribute.Minimum, base.Attribute.Maximum) };
        }
    }
}

