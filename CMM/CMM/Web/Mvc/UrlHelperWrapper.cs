namespace CMM.Web.Mvc
{
    using CMM;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;
    using System.Web.Routing;

    internal class UrlHelperWrapper
    {
        private static readonly ParameterExpression _unusedParameterExpr = Expression.Parameter(typeof(object), "_unused");

        public UrlHelperWrapper(System.Web.Mvc.UrlHelper helper)
        {
            this.UrlHelper = helper;
        }

        internal UrlHelperWrapper(RequestContext requestContext)
        {
            this.UrlHelper = new System.Web.Mvc.UrlHelper(requestContext);
        }

        public string Action<TController>(Expression<Func<TController, ActionResult>> action) where TController: IController
        {
            string actionName = string.Empty;
            string controllerName = string.Empty;
            Expression body = action.Body;
            if (body.NodeType == ExpressionType.Lambda)
            {
                body = ((LambdaExpression) action.Body).Body;
            }
            if (body.NodeType != ExpressionType.Call)
            {
                throw new NodeTypeNotSupportException(body.NodeType);
            }
            MethodCallExpression expression = (MethodCallExpression) body;
            actionName = expression.Method.Name;
            RouteValueDictionary routeValues = new RouteValueDictionary();
            ParameterInfo[] parameters = expression.Method.GetParameters();
            if (parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    Expression expression3 = expression.Arguments[i];
                    object obj2 = null;
                    ConstantExpression expression4 = expression3 as ConstantExpression;
                    if (expression4 != null)
                    {
                        obj2 = expression4.Value;
                    }
                    else if (string.Equals(expression3.ToString(), "UrlParameter`1.Empty", StringComparison.InvariantCulture))
                    {
                        obj2 = null;
                    }
                    else
                    {
                        Expression<Func<object, object>> expression5 = Expression.Lambda<Func<object, object>>(Expression.Convert(expression3, typeof(object)), new ParameterExpression[] { _unusedParameterExpr });
                        obj2 = expression5.Compile()(null);
                    }
                    if (obj2 != null)
                    {
                        routeValues.Add(parameters[i].Name, obj2);
                    }
                }
            }
            Type type = typeof(TController);
            if (type.Name.EndsWith("Controller"))
            {
                controllerName = type.Name.Substring(0, type.Name.Length - 10);
            }
            else
            {
                controllerName = type.Name;
            }
            return this.UrlHelper.Action(actionName, controllerName, routeValues);
        }

        private System.Web.Mvc.UrlHelper UrlHelper { get; set; }
    }
}

