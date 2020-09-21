namespace CMM
{
    using System;

    public class RegexPatterns
    {
        public const string Alphanum = @"[\w\d_]+";
        public const string Domain = @"\s*[\w.-]+\.[a-z]{2,4}\s*";
        public const string EmailAddress = @"\s*[\w.%-+]+@[\w.-]+\.[a-z]{2,4}\s*";
        public const string FileName = "^[^\\\\\\./:\\*\\?\\\"<>\\|]{1}[^\\\\/:\\*\\?\\\"<>\\|]{0,254}$";
        public const string HttpUrl = @"(http|https):\/\/([\w\-_]+(\.[\w\-_]+)+|localhost)([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?";
        public const string IP = @"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";
        public const string SimpleName = @"^[A-Za-z][\w]*$";
        public const string Version = @"^([0-9]+)\.([0-9]+)\.([0-9]+)\.([0-9])+$";
    }
}

