namespace SGR.Communicator.Models
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    public abstract class ChartBase
    {
        //public const int AxisXStepWidth = 40;
        //private System.Web.UI.DataVisualization.Charting.ChartArea ca1;
        //private Chart chart;
        //public const int MaxAxisXCount = 20;

        //public ChartBase(bool legendVisible = false)
        //{
        //    this.InitChart();
        //    if (legendVisible)
        //    {
        //        this.AddLegend();
        //    }
        //    this.AddChartArea();
        //}

        //private void AddChartArea()
        //{
        //    this.ca1 = new System.Web.UI.DataVisualization.Charting.ChartArea("ca1");
        //    this.ca1.BackColor = Color.FromName("OldLace");
        //    this.ca1.BorderColor = Color.FromArgb(0x40, 0x40, 0x40, 0x40);
        //    this.ca1.BorderDashStyle = ChartDashStyle.Solid;
        //    this.ca1.BackSecondaryColor = Color.FromName("White");
        //    this.ca1.ShadowColor = Color.Transparent;
        //    this.ca1.BackGradientStyle = GradientStyle.TopBottom;
        //    this.ca1.Area3DStyle.Rotation = 0x19;
        //    this.ca1.Area3DStyle.Perspective = 9;
        //    this.ca1.Area3DStyle.LightStyle = LightStyle.Realistic;
        //    this.ca1.Area3DStyle.Inclination = 20;
        //    this.ca1.Area3DStyle.IsRightAngleAxes = false;
        //    this.ca1.Area3DStyle.WallWidth = 3;
        //    this.ca1.Area3DStyle.IsClustered = false;
        //    this.ca1.AxisY.LineColor = Color.FromArgb(0x40, 0x40, 0x40, 0x40);
        //    this.ca1.AxisY.LabelStyle.Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold);
        //    this.ca1.AxisY.MajorGrid.LineColor = Color.FromArgb(0x40, 0x40, 0x40, 0x40);
        //    this.ca1.AxisY.Title = "Percentage (%)";
        //    this.ca1.AxisY.TitleFont = new Font("Trebuchet MS", 8.25f, FontStyle.Bold);
        //    this.ca1.AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
        //    this.ca1.AxisX.LineColor = Color.FromArgb(0x40, 0x40, 0x40, 0x40);
        //    this.ca1.AxisX.LabelStyle.Font = new Font("Trebuchet MS", 8.25f, FontStyle.Bold);
        //    this.ca1.AxisX.MajorGrid.LineColor = Color.FromArgb(0x40, 0x40, 0x40, 0x40);
        //    this.ca1.AxisX.Title = "Month";
        //    this.ca1.AxisX.TitleFont = new Font("Trebuchet MS", 8.25f, FontStyle.Bold);
        //    this.ca1.AxisX.CustomLabels.Clear();
        //    this.chart.ChartAreas.Add(this.ca1);
        //}

        //private void AddLegend()
        //{
        //    Legend item = new Legend {
        //        LegendStyle = LegendStyle.Row,
        //        Name = "Default",
        //        BackColor = Color.Transparent,
        //        Font = new Font("Trebuchet MS", 8.25f, FontStyle.Regular),
        //        Docking = Docking.Bottom,
        //        TableStyle = LegendTableStyle.Wide,
        //        Alignment = StringAlignment.Center
        //    };
        //    this.chart.Legends.Add(item);
        //}

        //public virtual void AddSeries(Series series)
        //{
        //    this.chart.Series.Add(series);
        //}

        //public abstract void AddSeries(string label, Color color, IEnumerable<double> values);
        //private void InitChart()
        //{
        //    this.chart = new Chart();
        //    //this.chart.Palette = ChartColorPalette.SemiTransparent;
        //    //this.chart.BackColor = ColorTranslator.FromHtml("#AADE96");
        //    this.chart.BackColor = ColorTranslator.FromHtml("#ffffff");

        //    this.chart.BackGradientStyle = GradientStyle.TopBottom;
        //    this.chart.BorderlineColor = Color.FromArgb(0, 0xb5, 0x40, 1);
            
            
        //    //this.chart.BorderlineDashStyle = ChartDashStyle.Solid;
        //    //this.chart.BorderWidth = Unit.Pixel(2);
        //    //this.chart.BorderSkin.SkinStyle = BorderSkinStyle.Emboss;
        //    this.chart.BorderSkin.BackGradientStyle = GradientStyle.VerticalCenter;
        //    this.chart.BorderSkin.BackColor = ColorTranslator.FromHtml("#ffffff");
            
        //    this.chart.Width = Unit.Pixel(970);
        //    //this.chart.Height = Unit.Pixel(350);
        //}

        //public byte[] Output()
        //{
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        this.chart.SaveImage(stream, ChartImageFormat.Png);
        //        stream.Seek(0L, SeekOrigin.Begin);
        //        return stream.ToArray();
        //    }
        //}

        //public void SetAxisX(string title, IEnumerable<string> labels)
        //{
        //    int num = labels.Count<string>();
        //    if (num > 20)
        //    {
        //        this.Width += (num - 20) * 40;
        //    }
        //    this.ca1.AxisX.Title = title;
        //    int num2 = 1;
        //    foreach (string str in labels)
        //    {
        //        CustomLabel item = new CustomLabel {
        //            GridTicks = GridTickTypes.All,
        //            FromPosition = num2 * 2,
        //            Text = str
        //        };
        //        this.ca1.AxisX.CustomLabels.Add(item);
        //        num2++;
        //    }
        //}

        //public void SetAxisY(string title)
        //{
        //    this.ca1.AxisY.Title = title;
        //}

        //protected System.Web.UI.DataVisualization.Charting.ChartArea ChartArea
        //{
        //    get
        //    {
        //        return this.ca1;
        //    }
        //}

        //public int Height
        //{
        //    get
        //    {
        //        return (int) this.chart.Height.Value;
        //    }
        //    set
        //    {
        //        this.chart.Height = Unit.Pixel(value);
        //    }
        //}

        //public int Width
        //{
        //    get
        //    {
        //        return (int) this.chart.Width.Value;
        //    }
        //    set
        //    {
        //        this.chart.Width = Unit.Pixel(value);
        //    }
        //}
    }
}

