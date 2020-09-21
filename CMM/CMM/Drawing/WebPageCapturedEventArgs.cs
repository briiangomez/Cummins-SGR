namespace CMM.Drawing
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public class WebPageCapturedEventArgs : EventArgs
    {
        public System.Drawing.Image Image { get; set; }
    }
}

