using Xamarin.Forms;
using SkiaSharp.Views.Forms;
using System;

namespace SimpleCharts.Forms
{

	public class ChartView : SKCanvasView
	{
		public ChartView()
		{
			this.BackgroundColor = Color.Transparent;
			this.PaintSurface += OnPaintCanvas;
		}

		public static readonly BindableProperty ChartProperty = BindableProperty.Create(nameof(Chart), typeof(Chart), typeof(ChartView), null, propertyChanged: OnChartChanged);

		public Chart Chart
		{
			get { return (Chart)GetValue(ChartProperty); }
			set
			{
				var oldChart = (Chart)GetValue(ChartProperty);
				if (oldChart != null)
					oldChart.DrawInvalidated = null;
				SetValue(ChartProperty, value);
				if (value != null)
					value.DrawInvalidated = () => Device.BeginInvokeOnMainThread(InvalidateSurface);
				var wraper = value.Parent as BindableObject;
				if (wraper != null)
					wraper.BindingContext = BindingContext;
			}
		}

		private static void OnChartChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((ChartView)bindable).InvalidateSurface();
		}

		private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
		{
			if (this.Chart != null)
			{
				this.Chart.Draw(e.Surface.Canvas, e.Info.Width, e.Info.Height);
			}
		}
		protected override void OnPropertyChanged(string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			if (propertyName == nameof(Chart))
			{
				var chart = Chart;
				if (chart != null)
					chart.DrawInvalidated = () => Device.BeginInvokeOnMainThread(InvalidateSurface);
			}
		}
		protected override void OnPropertyChanging(string propertyName = null)
		{
			if (propertyName == nameof(Chart))
			{
				var oldChart = Chart;
				if (oldChart != null)
					oldChart.DrawInvalidated = null;
			}
			base.OnPropertyChanging(propertyName);
		}
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			var wraper = Chart?.Parent as BindableObject;
			if (wraper != null)
				wraper.BindingContext = BindingContext;
		}
	}
}
