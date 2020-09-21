namespace SGR.Communicator
{
    using CMM.Globalization;
    using System;

    public static class ViewUtility
    {
        public static string FormatDateTime(DateTime dateTime)
        {
            return (dateTime.ToShortDateString() + dateTime.ToString(" HH:mm"));
        }

        public static string Int2Excel(int intInput)
        {
            string str = string.Empty;
            int num = intInput % 0x1a;
            intInput /= 0x1a;
            while ((intInput != 0) || (num != 0))
            {
                if (num == 0)
                {
                    intInput--;
                    num = 0x1a;
                }
                str = Convert.ToChar((int) (num + 0x60)) + str;
                num = intInput % 0x1a;
                intInput /= 0x1a;
            }
            return str.ToUpper();
        }

        public static string MailTag(int index)
        {
            char ch = (char) (0x41 + index);
            return ch.ToString();
        }

        public static string Percent(int a, int b)
        {
            if (b == 0)
            {
                return "0.00";
            }
            double num = (((double) a) / ((double) b)) * 100.0;
            return num.ToString("f2");
        }

        public static double PercentOf(int a, int b)
        {
            if (b == 0)
            {
                return 0.0;
            }
            return Math.Round(((((double) a) / ((double) b)) * 100.0));
        }

        public static string EllipsisText
        {
            get
            {
                return "…";
            }
        }

        public static string FinishButtonText
        {
            get
            {
                return "Finish \x00bb".Localize("");
            }
        }

        public static string LeftNavigateText
        {
            get
            {
                return "\x00ab";
            }
        }

        public static string NextButtonText
        {
            get
            {
                //return ("Next".Localize("") + " \x00bb");
                return ("Next".Localize("") );
            }
        }

        public static string PreviousButtonText
        {
            get
            {
                //return ("\x00ab " + "Previous".Localize(""));
                return ("Previous".Localize(""));
            }
        }

        public static string RightNavigateText
        {
            get
            {
                return "\x00bb";
            }
        }

        public static string SaveAndReturnButtonText
        {
            get
            {
                return "Save and Return".Localize("");
            }
        }
    }
}

