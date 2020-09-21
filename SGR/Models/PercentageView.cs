namespace SGR.Communicator.Models
{
    using CMM.Globalization;
    using System;
    using System.Runtime.CompilerServices;

    public class PercentageView
    {
        public static PercentageView Create(int count, int totalCount)
        {
            return new PercentageView { ProcessedCount = count, TotalCount = totalCount, StyleString = ViewUtility.PercentOf(count, totalCount), PercentageString = (count == 0) ? "Waiting in queue".Localize("") : string.Format("{0}%({1}/{2})", ViewUtility.Percent(count, totalCount), count, totalCount) };
        }

        public string PercentageString { get; set; }

        public int ProcessedCount { get; set; }

        public double StyleString { get; set; }

        public int TotalCount { get; set; }
    }
}

