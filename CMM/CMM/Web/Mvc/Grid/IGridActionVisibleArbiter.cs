namespace CMM.Web.Mvc.Grid
{
    using System;
    using System.Web.Mvc;

    public interface IGridActionVisibleArbiter
    {
        bool IsVisible(object dataItem, ViewContext viewContext);
    }
}

