namespace CMM.Web
{
    using System;
    using System.Net.Mime;
    using System.Text;

    public static class ContentDispositionUtil
    {
        private const string _hexDigits = "0123456789ABCDEF";

        private static void AddByteToStringBuilder(byte b, StringBuilder builder)
        {
            builder.Append('%');
            int num = b;
            AddHexDigitToStringBuilder(num >> 4, builder);
            AddHexDigitToStringBuilder(num % 0x10, builder);
        }

        private static void AddHexDigitToStringBuilder(int digit, StringBuilder builder)
        {
            builder.Append("0123456789ABCDEF"[digit]);
        }

        private static string CreateRfc2231HeaderValue(string filename)
        {
            StringBuilder builder = new StringBuilder("attachment; filename*=UTF-8''");
            foreach (byte num in Encoding.UTF8.GetBytes(filename))
            {
                if (IsByteValidHeaderValueCharacter(num))
                {
                    builder.Append((char) num);
                }
                else
                {
                    AddByteToStringBuilder(num, builder);
                }
            }
            return builder.ToString();
        }

        public static string GetHeaderValue(string fileName)
        {
            try
            {
                ContentDisposition disposition2 = new ContentDisposition {
                    FileName = fileName
                };
                ContentDisposition disposition = disposition2;
                return disposition.ToString();
            }
            catch (FormatException)
            {
                return CreateRfc2231HeaderValue(fileName);
            }
        }

        private static bool IsByteValidHeaderValueCharacter(byte b)
        {
            if ((0x30 <= b) && (b <= 0x39))
            {
                return true;
            }
            if ((0x61 <= b) && (b <= 0x7a))
            {
                return true;
            }
            if ((0x41 <= b) && (b <= 90))
            {
                return true;
            }
            switch (b)
            {
                case 0x3a:
                case 0x5f:
                case 0x7e:
                case 0x24:
                case 0x26:
                case 0x21:
                case 0x2b:
                case 0x2d:
                case 0x2e:
                    return true;
            }
            return false;
        }
    }
}

