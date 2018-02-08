using System;
using MChart = SimpleCharts.Chart;
using MLineChart = SimpleCharts.LineChart;
using MBarChart = SimpleCharts.BarChart;
using MDonutChart = SimpleCharts.DonutChart;
using MLineSeriesChart = SimpleCharts.LineSeriesChart;
using MPointChart = SimpleCharts.PointChart;
using MRadarChart = SimpleCharts.RadarChart;
using MRadialGaugeChart = SimpleCharts.RadialGaugeChart;
using Entry = SimpleCharts.Entry;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace SimpleCharts.Forms
{
	public class BaseChart<T> : BindableObject where T : MChart, new ()
	{
		public static readonly BindableProperty EntriesProperty = BindableProperty.Create(nameof(Entries), typeof(IEnumerable<Entry>), typeof(LineChart), null, propertyChanged: OnEntriesChanged);

		public BaseChart()
		{
			Chart.Parent = this;
		}
		public T Chart { get; set; } = new T();
		public IEnumerable<Entry> Entries
		{
			get => Chart.Entries;
			set => Chart.Entries = value;
		}

		public static implicit operator MChart(BaseChart<T> chart)
		{
			return chart.Chart;
		}
		protected static void OnEntriesChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((BaseChart<T>)bindable).Chart.Entries = newValue as IEnumerable<Entry>;
		}
		protected override void OnPropertyChanging(string propertyName = null)
		{
			base.OnPropertyChanging(propertyName);
			if (propertyName == nameof(Entries))
				Chart.Entries = (IEnumerable<Entry>)GetValue(EntriesProperty);
		}

	}
	public class LineChart : BaseChart<MLineChart>
	{
		public static implicit operator MLineChart(LineChart chart)
		{
			return chart.Chart;
		}


	}
}
