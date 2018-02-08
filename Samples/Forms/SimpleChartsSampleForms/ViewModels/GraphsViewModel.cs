using System;
using SimpleCharts;
using SkiaSharp;
using SimpleChartsSampleForms.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace SimpleChartsSampleForms.ViewModels
{
	public class GraphsViewModel : BaseViewModel
	{
		public Entry[] GenericEntries { get; set; } = new[]
		{
			new Entry(200)	{
				Label = "January",
				ValueLabel = "200",
				Color = SKColor.Parse("#266489"),
			},
			new Entry(400)
			{
				Label = "February",
				ValueLabel = "400",
				Color = SKColor.Parse("#68B9C0"),
									},
			new Entry(-100)
			{
				Label = "March",
				ValueLabel = "-100",
				Color = SKColor.Parse("#90D585"),
			},
		};

		public FixedSizeObservableCollection<StockPrice> Stocks { get; } = new FixedSizeObservableCollection<StockPrice>(10);

		public GraphsViewModel()
		{
			UpdateStockPrices();
		}
		Random rand = new Random();
		DateTime start = DateTime.Now;
		async Task UpdateStockPrices()
		{
			while (true)
			{
				Stocks.Add(new StockPrice
				{
					Price = rand.NextDouble(),
					Symbol = "FAKE",
					Time = (DateTime.Now - start).TotalSeconds
				});
				await Task.Delay(rand.Next(500,1500));
			}
		}
	}
}
