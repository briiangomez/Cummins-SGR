namespace CMM.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public class CMMModelMetadata : DataAnnotationsModelMetadata
    {
        private string _description;

        public CMMModelMetadata(DataAnnotationsModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName, DisplayColumnAttribute displayColumnAttribute, IEnumerable<Attribute> attributes) : base(provider, containerType, modelAccessor, modelType, propertyName, displayColumnAttribute)
        {
            DescriptionAttribute attribute = attributes.OfType<DescriptionAttribute>().SingleOrDefault<DescriptionAttribute>();
            this._description = (attribute != null) ? attribute.Description : "";
            this.DataSourceAttribute = attributes.OfType<CMM.Web.Mvc.DataSourceAttribute>().SingleOrDefault<CMM.Web.Mvc.DataSourceAttribute>();
            EnumDataTypeAttribute attribute2 = attributes.OfType<EnumDataTypeAttribute>().SingleOrDefault<EnumDataTypeAttribute>();
            if (attribute2 != null)
            {
                this.DataSource = new EnumTypeSelectListDataSource(attribute2.EnumType);
            }
            this.Attributes = attributes;
        }

        public IEnumerable<Attribute> Attributes { get; private set; }

        public ISelectListDataSource DataSource { get; private set; }

        public CMM.Web.Mvc.DataSourceAttribute DataSourceAttribute { get; private set; }

        public override string Description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
            }
        }
    }
}

