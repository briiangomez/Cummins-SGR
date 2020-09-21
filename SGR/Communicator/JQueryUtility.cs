namespace SGR.Communicator
{
    using System;
    using System.Text.RegularExpressions;

    public static class JQueryUtility
    {
        public static string DateFormat()
        {
            return String.Empty;//Regex.Replace(Regex.Replace(CommunicatorContext.Current.Portal.MessageContext.LocalizationInfo.CultureInfo.DateTimeFormat.ShortDatePattern, @"\by+\b", "yy"), @"\bM+\b", "mm");
        }
    }
}

