namespace CMM.Web.Mvc
{
    using CMM.ComponentModel;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    public class CMMDataAnnotationsModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            ModelMetadata metadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);
            return new CMMModelMetadata(this, containerType, modelAccessor, modelType, propertyName, attributes.OfType<DisplayColumnAttribute>().FirstOrDefault<DisplayColumnAttribute>(), attributes) { TemplateHint = metadata.TemplateHint, HideSurroundingHtml = metadata.HideSurroundingHtml, DataTypeName = metadata.DataTypeName, IsReadOnly = metadata.IsReadOnly, NullDisplayText = metadata.NullDisplayText, DisplayFormatString = metadata.DisplayFormatString, ConvertEmptyStringToNull = false, EditFormatString = metadata.EditFormatString, ShowForDisplay = metadata.ShowForDisplay, ShowForEdit = metadata.ShowForEdit, DisplayName = metadata.DisplayName };
        }

        protected override ICustomTypeDescriptor GetTypeDescriptor(Type type)
        {
            ICustomTypeDescriptor typeDescriptor = CMM.ComponentModel.TypeDescriptorHelper.Get(type);
            if (typeDescriptor == null)
            {
                typeDescriptor = base.GetTypeDescriptor(type);
            }
            return typeDescriptor;
        }
    }
}

