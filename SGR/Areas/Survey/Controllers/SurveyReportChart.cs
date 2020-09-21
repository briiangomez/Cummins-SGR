using SGI.Communicator;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace SGR.Controllers
{
    public class SurveyReportChart
    {
        private SurveyReportData ChartData;
        private Chart ChartObj;
        private SeriesChartType ChartType;

        public SurveyReportChart(SeriesChartType chartType, SurveyReportData chartData)
        {
            this.ChartType = chartType;
            this.ChartData = chartData;
            this.Initialize();
        }

        private void AddArea()
        {
            ChartArea item = new ChartArea();
            this.ChartObj.ChartAreas.Add(item);
            item.BorderColor = Color.FromArgb(0x40, 0x40, 0x40, 0x40);
            item.BackSecondaryColor = Color.White;
            item.BackColor = Color.OldLace;
            item.ShadowColor = Color.Transparent;
            item.BackGradientStyle = GradientStyle.TopBottom;
            item.Area3DStyle.Rotation = 10;
            item.Area3DStyle.Perspective = 10;
            item.Area3DStyle.Inclination = 15;
            item.Area3DStyle.WallWidth = 0;
            item.Area3DStyle.IsRightAngleAxes = false;
            item.Area3DStyle.IsClustered = false;
            item.AxisY.LineColor = Color.FromArgb(0x40, 0x40, 0x40, 0x40);
            item.AxisY.LabelAutoFitMaxFontSize = 8;
            item.AxisY.LabelStyle.Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold);
            item.AxisY.LineColor = Color.FromArgb(0x40, 0x40, 0x40, 0x40);
            item.AxisX.LineColor = Color.FromArgb(0x40, 0x40, 0x40, 0x40);
            item.AxisX.LabelAutoFitMaxFontSize = 8;
            item.AxisX.LabelStyle.Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold);
            item.AxisX.LabelStyle.IsEndLabelVisible = false;
            item.AxisX.LineColor = Color.FromArgb(0x40, 0x40, 0x40, 0x40);
            if (this.ChartType == SeriesChartType.Column)
            {
                item.AxisY.Title = this.ChartData.TitleY;
                item.AxisX.Title = this.ChartData.TitleX;
                int num = this.ChartData.Options.Sum<SurveyReportOption>((Func<SurveyReportOption, int>) (o => o.Subtotal));
                for (int i = 0; i < this.ChartData.Options.Count; i++)
                {
                    SurveyReportOption option = this.ChartData.Options[i];
                    CustomLabel label = new CustomLabel {
                        Text = string.Concat(new object[] { ViewUtility.Int2Excel(i + 1), " (", option.Subtotal, ")" }),
                        GridTicks = GridTickTypes.All,
                        FromPosition = (i + 1) * 2
                    };
                    item.AxisX.CustomLabels.Add(label);
                }
            }
        }

        private void AddLegend()
        {
            if (this.ChartType == SeriesChartType.Pie)
            {
                Legend item = new Legend();
                this.ChartObj.Legends.Add(item);
                item.TitleFont = new Font("Microsoft Sans Serif", 8f, FontStyle.Bold);
                item.BackColor = Color.Transparent;
                item.IsEquallySpacedItems = true;
                item.Font = new Font("Trebuchet MS", 8f, FontStyle.Bold);
                item.IsTextAutoFit = false;
            }
        }

        private void AddSeries()
        {
            Series item = new Series();
            this.ChartObj.Series.Add(item);
            item.ChartType = this.ChartType;
            item.BorderColor = Color.FromArgb(180, 0x1a, 0x3b, 0x69);
            item.Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold);
            item.IsValueShownAsLabel = true;
            if (this.ChartType == SeriesChartType.Column)
            {
                item["PointWidth"] = "0.6";
                item["DrawingStyle"] = "Cylinder";
            }
            else if (this.ChartType == SeriesChartType.Pie)
            {
                item["DoughnutRadius"] = "25";
                item["PieDrawingStyle"] = "Concave";
                item["CollectedLabel"] = "Other";
                item["MinimumRelativePieSize"] = "20";
                item.MarkerStyle = MarkerStyle.Circle;
                item.BorderColor = Color.FromArgb(0x40, 0x40, 0x40, 0x40);
                item.Color = Color.FromArgb(180, 0x41, 140, 240);
            }
            if (this.ChartData.Options.Sum<SurveyReportOption>(((Func<SurveyReportOption, int>) (o => o.Subtotal))) != 0)
            {
                item.Label = "#PERCENT{P2}";
            }
            int num = this.ChartData.Options.Sum<SurveyReportOption>((Func<SurveyReportOption, int>) (o => o.Subtotal));
            for (int i = 0; i < this.ChartData.Options.Count; i++)
            {
                SurveyReportOption option = this.ChartData.Options[i];
                DataPoint point = new DataPoint();
                item.Points.Add(point);
                point.LegendText = string.Concat(new object[] { ViewUtility.Int2Excel(i + 1), " (", option.Subtotal, ")" });
                point.AxisLabel = ViewUtility.Int2Excel(i + 1);
                point.YValues = new double[] { (double) option.Subtotal };
            }
        }

        private void ChartSize()
        {
            int num = 500;
            int num2 = 400;
            int num3 = this.ChartData.Options.Count * 100;
            if (this.ChartType == SeriesChartType.Column)
            {
                if (num3 > num)
                {
                    num = num3;
                    num2 = 500;
                }
            }
            else if ((this.ChartType == SeriesChartType.Pie) && (num3 > num))
            {
                num = 600;
                num2 = 500;
            }
            this.ChartObj.Width = new Unit(num);
            this.ChartObj.Height = new Unit(num2);
        }

        private void ChartTitle()
        {
            Title item = new Title();
            this.ChartObj.Titles.Add(item);
            item.Text = this.ChartData.Title;
            item.Font = new Font("Trebuchet MS", 14.25f, FontStyle.Bold);
            item.ForeColor = Color.FromArgb(0xff, 0x1a, 0x3b, 0x69);
            item.ShadowColor = Color.FromArgb(0x20, 0, 0, 0);
            item.ShadowOffset = 3;
        }

        private void Initialize()
        {
            this.ChartObj = new Chart();
            this.ChartObj.Palette = ChartColorPalette.BrightPastel;
            this.ChartObj.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
            this.ChartObj.BackColor = ColorTranslator.FromHtml("#F3DFC1");
            this.ChartObj.BorderlineDashStyle = ChartDashStyle.Solid;
            this.ChartObj.BackGradientStyle = GradientStyle.TopBottom;
            this.ChartObj.BorderWidth = Unit.Pixel(2);
            this.ChartObj.BorderColor = Color.FromArgb(0xff, 0xb5, 0x40, 1);
            this.ChartTitle();
            this.ChartSize();
            this.AddLegend();
            this.AddArea();
            this.AddSeries();
        }

        public byte[] Output()
        {
            this.ChartObj.ImageType = ChartImageType.Png;
            using (MemoryStream stream = new MemoryStream())
            {
                this.ChartObj.SaveImage(stream, ChartImageFormat.Png);
                stream.Seek(0L, SeekOrigin.Begin);
                return stream.ToArray();
            }
        }
    }
}

