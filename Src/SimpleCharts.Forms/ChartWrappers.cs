using System;
using MChart = SimpleCharts.Chart;
using MLineChart = SimpleCharts.LineChart;
using MBarChart = SimpleCharts.BarChart;
using MDonutChart = SimpleCharts.DonutChart;
using MPointChart = SimpleCharts.PointChart;
using MRadarChart = SimpleCharts.RadarChart;
using MRadialGaugeChart = SimpleCharts.RadialGaugeChart;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkiaSharp.Views.Forms;
using Xamarin.Forms.Internals;
namespace SimpleCharts.Forms
{
	public class BaseChart : BindableObject
	{
		public static readonly BindableProperty EntriesProperty = BindableProperty.Create(nameof(Entries), typeof(IEnumerable<object>), typeof(BaseChart), null);

		public static readonly BindableProperty BackgroundColorProperty = BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(BaseChart), Color.White);

		public static readonly BindableProperty ValuePropertyProperty = BindableProperty.Create(nameof(ValueProperty), typeof(string), typeof(BaseChart), null);

		public static readonly BindableProperty ValueLabelPropertyProperty = BindableProperty.Create(nameof(ValueLabelProperty), typeof(string), typeof(BaseChart), null);

		public static readonly BindableProperty LabelPropertyProperty = BindableProperty.Create(nameof(LabelProperty), typeof(string), typeof(BaseChart), null);

		public static readonly BindableProperty DefaultColorProperty = BindableProperty.Create(nameof(DefaultColor), typeof(Color), typeof(BaseChart), Color.DeepSkyBlue);

		public static readonly BindableProperty DefaultTextColorProperty = BindableProperty.Create(nameof(DefaultTextColor), typeof(Color), typeof(BaseChart), Color.DeepSkyBlue);

		public static readonly BindableProperty ColorPropertyProperty = BindableProperty.Create(nameof(ColorProperty), typeof(string), typeof(BaseChart), null);

		public static readonly BindableProperty TextColorPropertyProperty = BindableProperty.Create(nameof(TextColorProperty), typeof(string), typeof(BaseChart), null);

		public IEnumerable<object> Entries
		{
			get => (IEnumerable<object>)GetValue(EntriesProperty);
			set => SetValue(EntriesProperty, value);
		}

		public Color BackgroundColor
		{
			get => (Color)GetValue(BackgroundColorProperty);
			set => SetValue(BackgroundColorProperty, value);
		}

		public string ValueProperty {
			get => (string)GetValue(ValuePropertyProperty);
			set => SetValue(ValuePropertyProperty, value);
		}
		public string ValueLabelProperty {
			get => (string)GetValue(ValueLabelPropertyProperty);
			set => SetValue(ValueLabelPropertyProperty, value);
		}
		public string LabelProperty
		{
			get => (string)GetValue(LabelPropertyProperty);
			set => SetValue(LabelPropertyProperty, value);
		}

		public Color DefaultColor
		{
			get => (Color)GetValue(DefaultColorProperty);
			set => SetValue(DefaultColorProperty, value);
		}
		public Color DefaultTextColor
		{
			get => (Color)GetValue(DefaultTextColorProperty);
			set => SetValue(DefaultTextColorProperty, value);
		}
		public string ColorProperty
		{
			get => (string)GetValue(ColorPropertyProperty);
			set => SetValue(ColorPropertyProperty, value);
		}

		public string TextColorProperty
		{
			get => (string)GetValue(TextColorPropertyProperty);
			set => SetValue(TextColorPropertyProperty, value);
		}

	}
	public class BaseChart<T> : BaseChart where T : MChart, new()
	{
		public BaseChart()
		{
			Chart.Parent = this;
		}
		public T Chart { get; set; } = new T();

		protected override void OnPropertyChanging(string propertyName = null)
		{
			base.OnPropertyChanging(propertyName);

			if (Chart == null)
				return;

			if (propertyName == nameof(Entries))
				Chart.Entries = Entries;
			else if (propertyName == nameof(BackgroundColor))
				Chart.BackgroundColor = BackgroundColor.ToSKColor();
			else if (propertyName == nameof(ValueProperty))
				Chart.ValueProperty = ValueProperty;
			else if (propertyName == nameof(ValueLabelProperty))
				Chart.ValueLabelProperty = ValueLabelProperty;
			else if (propertyName == nameof(LabelProperty))
				Chart.LabelProperty = LabelProperty;
			else if (propertyName == nameof(DefaultColor))
				Chart.DefaultColor = DefaultColor.ToSKColor();
			else if (propertyName == nameof(DefaultTextColor))
				Chart.DefaultTextColor = DefaultTextColor.ToSKColor();
			else if (propertyName == nameof(ColorProperty))
				Chart.ColorProperty = ColorProperty;
			else if (propertyName == nameof(TextColorProperty))
				Chart.TextColorProperty = TextColorProperty;
		}
		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			Chart.BackgroundColor = BackgroundColor.ToSKColor();
			Chart.ValueProperty = ValueProperty;
			Chart.ValueLabelProperty = ValueLabelProperty;
			Chart.LabelProperty = LabelProperty;
			Chart.DefaultColor = DefaultColor.ToSKColor();
			Chart.DefaultTextColor = DefaultTextColor.ToSKColor();
			Chart.ColorProperty = ColorProperty;
			Chart.TextColorProperty = TextColorProperty;
			Chart.Entries = Entries;
		}

	}
	[Preserve (AllMembers = true)] 
	public class LineChart : BaseChart<MLineChart>
	{
		public static implicit operator MLineChart(LineChart chart)
		{
			return chart.Chart;
		}
	}
	[Preserve (AllMembers = true)] 
	public class BarChart : BaseChart<MBarChart>
	{
		public static implicit operator MBarChart(BarChart chart)
		{
			return chart.Chart;
		}
	}
	[Preserve (AllMembers = true)] 
	public class DonutChart : BaseChart<MDonutChart>
	{
		public static implicit operator MDonutChart(DonutChart chart)
		{
			return chart.Chart;
		}
	}
	[Preserve (AllMembers = true)] 
	public class PointChart : BaseChart<MPointChart>
	{
		public static implicit operator MPointChart(PointChart chart)
		{
			return chart.Chart;
		}
	}
	[Preserve (AllMembers = true)] 
	public class RadarChart : BaseChart<MRadarChart>
	{
		public static implicit operator MRadarChart(RadarChart chart)
		{
			return chart.Chart;
		}
	}
	[Preserve (AllMembers = true)] 
	public class RadialGaugeChart : BaseChart<MRadialGaugeChart>
	{
		public static implicit operator MRadialGaugeChart(RadialGaugeChart chart)
		{
			return chart.Chart;
		}
	}
}
