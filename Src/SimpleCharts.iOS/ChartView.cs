#if __IOS__

using UIKit;
using SkiaSharp.Views.iOS;
#else
using SkiaSharp.Views.Mac;
#endif

namespace
#if __IOS__
Microcharts.iOS
#else
Microcharts.MacOS
#endif

{

public class ChartView : SKCanvasView
	{
		public ChartView()
		{
#if __IOS__
            this.BackgroundColor = UIColor.Clear;
#endif
			this.PaintSurface += OnPaintCanvas;
		}

		private Chart chart;

		public Chart Chart
		{
			get => this.chart;
			set
			{
				if (this.chart != null)
					this.chart.DrawInvalidated = null;
				if (this.chart != value)
				{
					this.chart = value;
					this.SetNeedsDisplayInRect(this.Bounds);
					if (this.chart != null)
						this.chart.DrawInvalidated = () => this.BeginInvokeOnMainThread(() => this.SetNeedsDisplayInRect(this.Bounds));
				}
			}
		}

		private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
		{
			if (this.chart != null)
			{
				this.chart.Draw(e.Surface.Canvas, e.Info.Width, e.Info.Height);
			}
		}
	}
}
