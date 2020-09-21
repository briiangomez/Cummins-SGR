namespace CMM.Web
{
    using System;
    using System.Security;
    using System.Web;

    public class TrustLevelUtility
    {
        private static AspNetHostingPermissionLevel? _trustLevel;
        private static object lockObj = new object();

        private static AspNetHostingPermissionLevel GetCurrentTrustLevel()
        {
            foreach (AspNetHostingPermissionLevel level in new AspNetHostingPermissionLevel[] { AspNetHostingPermissionLevel.Unrestricted, AspNetHostingPermissionLevel.High, AspNetHostingPermissionLevel.Medium, AspNetHostingPermissionLevel.Low, AspNetHostingPermissionLevel.Minimal })
            {
                try
                {
                    new AspNetHostingPermission(level).Demand();
                }
                catch (SecurityException)
                {
                    goto Label_0057;
                }
                return level;
            Label_0057:;
            }
            return AspNetHostingPermissionLevel.None;
        }

        public static AspNetHostingPermissionLevel CurrentTrustLevel
        {
            get
            {
                if (!_trustLevel.HasValue)
                {
                    lock (lockObj)
                    {
                        if (!_trustLevel.HasValue)
                        {
                            _trustLevel = new AspNetHostingPermissionLevel?(GetCurrentTrustLevel());
                        }
                    }
                }
                return _trustLevel.Value;
            }
        }
    }
}

