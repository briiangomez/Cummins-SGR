namespace CMM
{
    using System;

    public class Settings
    {
        public static string BaseDirectory
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            }
        }

        public static string BinDirectory
        {
            get
            {
                if (IsWebApplication)
                {
                    return AppDomain.CurrentDomain.SetupInformation.PrivateBinPath;
                }
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            }
        }

        public static string ComponentsDirectory
        {
            get
            {
                return (AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Components");
            }
        }

        public static bool IsWebApplication
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ConfigurationFile.EndsWith(@"\web.config");
            }
        }
    }
}

