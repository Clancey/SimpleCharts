using System;
using System.Collections.Generic;

using Xamarin.Forms;
using SimpleChartsSampleForms.ViewModels;

namespace SimpleChartsSampleForms.Views
{
	public partial class GraphsPage : ContentPage
	{
		public GraphsPage()
		{
			InitializeComponent();
			BindingContext = new GraphsViewModel();
		}
	}
}
