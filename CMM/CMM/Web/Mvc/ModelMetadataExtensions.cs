namespace CMM.Web.Mvc
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public static class ModelMetadataExtensions
    {
        public static ISelectListDataSource GetDataSource(this ModelMetadata modelMetadata)
        {
            ISelectListDataSource dataSource = null;
            if (modelMetadata is CMMModelMetadata)
            {
                DataSourceAttribute dataSourceAttribute = ((CMMModelMetadata) modelMetadata).DataSourceAttribute;
                if (dataSourceAttribute != null)
                {
                    dataSource = (ISelectListDataSource) Activator.CreateInstance(dataSourceAttribute.DataSourceType);
                }
                if (dataSource == null)
                {
                    dataSource = ((CMMModelMetadata) modelMetadata).DataSource;
                }
            }
            if (dataSource == null)
            {
                return new EmptySelectListDataSource();
            }
            return dataSource;
        }

        public static Type GetDataSourceType(this ModelMetadata modelMetadata)
        {
            Type dataSourceType = null;
            if (modelMetadata is CMMModelMetadata)
            {
                dataSourceType = ((CMMModelMetadata) modelMetadata).DataSourceAttribute.DataSourceType;
            }
            if (dataSourceType == null)
            {
                dataSourceType = typeof(EmptySelectListDataSource);
            }
            return dataSourceType;
        }
    }
}

