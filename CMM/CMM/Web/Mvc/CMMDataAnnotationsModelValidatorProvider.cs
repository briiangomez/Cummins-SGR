namespace CMM.Web.Mvc
{
    using CMM.ComponentModel;
    using CMM.Globalization;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    public class CMMDataAnnotationsModelValidatorProvider : System.Web.Mvc.DataAnnotationsModelValidatorProvider
    {
        protected override ICustomTypeDescriptor GetTypeDescriptor(Type type)
        {
            ICustomTypeDescriptor typeDescriptor = CMM.ComponentModel.TypeDescriptorHelper.Get(type);
            if (typeDescriptor == null)
            {
                typeDescriptor = base.GetTypeDescriptor(type);
            }
            return typeDescriptor;
        }

        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            foreach (ValidationAttribute attribute in attributes.OfType<ValidationAttribute>())
            {
                if (!string.IsNullOrWhiteSpace(attribute.ErrorMessage))
                {
                    attribute.ErrorMessage = attribute.ErrorMessage.Localize("");
                }
                else if (attribute is RequiredAttribute)
                {
                    attribute.ErrorMessage = "Required".Localize("");
                }
            }
            return base.GetValidators(metadata, context, attributes);
        }
    }
}

