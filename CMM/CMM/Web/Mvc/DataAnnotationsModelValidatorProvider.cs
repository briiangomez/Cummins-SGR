namespace CMM.Web.Mvc
{
    using CMM.ComponentModel.DataAnnotations;
    using System;
    using System.Web.Mvc;

    public class DataAnnotationsModelValidatorProvider
    {
        public static void RegisterCMMValidators()
        {
            System.Web.Mvc.DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(CMMRequiredAttribute), typeof(CMMRequiredAttributeAdapter));
            System.Web.Mvc.DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(CMMRegularExpressionAttribute), typeof(CMMRegularExpressionAttributeAdapter));
            System.Web.Mvc.DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(CMMCompareAttribute), typeof(CMMCompareAttributeAdapter));
        }
    }
}

