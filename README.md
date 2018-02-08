# SimpleCharts

This is a HardForm from [Microcharts](https://github.com/aloisdeniel/Microcharts).

I have plans on making this super easy to use in Xamarin and Xamarin.Forms which will require a lot of major changes.

**SimpleCharts** is an extremely simple charting library for a wide range of platforms (see *Compatibility* section below), with shared code and rendering for all of them!

## Gallery
 
![gallery](Documentation/images/Gallery.png)

## Install

Available on NuGet

**NET Standard 1.4, Xamarin.iOS, Xamarin.Android, UWP**

[![NuGet](https://img.shields.io/nuget/v/SimpleCharts.svg?label=NuGet)](https://www.nuget.org/packages/SimpleCharts/)


**Xamarin.Forms (.NET Standard 1.4)**

[![NuGet](https://img.shields.io/nuget/v/SimpleCharts.Forms.svg?label=NuGet)](https://www.nuget.org/packages/SimpleCharts.Forms/)

## Quickstart

### 1°) Your chart will need a set of data entries.

```csharp
var entries = new[]
{
    new Entry(200)
    {
        Label = "January",
        ValueLabel = "200",
	FillColor = SKColor.Parse("#266489")
    },
    new Entry(400)
    {
	Label = "February",
	ValueLabel = "400",
	FillColor = SKColor.Parse("#68B9C0")
    },
    new Entry(-100)
    {
	Label = "March",
	ValueLabel = "-100",
	FillColor = SKColor.Parse("#90D585")
    }
};
```

### 2°) Instanciate a chart from those entries

```csharp
var chart = new BarChart() { Entries = entries };
// or: var chart = new PointChart() { Entries = entries };
// or: var chart = new LineChart() { Entries = entries };
// or: var chart = new DonutChart() { Entries = entries };
// or: var chart = new RadialGaugeChart() { Entries = entries };
// or: var chart = new RadarChart() { Entries = entries };
```

### 2°) Add it to your UI!

**Xamarin.iOS**

![ios-screen](Documentation/images/iOS-Screenshot.png)

```csharp
public override void ViewDidLoad()
{
    base.ViewDidLoad();

    var entries = // ... see 1°) above
    var chart = // ... see 2°) above

    var chartView = new ChartView
    {
        Frame = new CGRect(0, 32, this.View.Bounds.Width, 140),
        AutoresizingMask = UIViewAutoresizing.FlexibleWidth,
        Chart = chart
    };

	this.View.AddSubview(chartView);
}
```

**Xamarin.Android**

![android-screen](Documentation/images/Android-Screenshot.png)

```xml
<?xml version="1.0" encoding="utf-8"?>
<ScrollView xmlns:android="http://schemas.android.com/apk/res/android"
    android:layout_width="match_parent"
    android:layout_height="match_parent">
    <LinearLayout 
        android:orientation="vertical"
        android:layout_width="match_parent"
        android:layout_height="wrap_content">
        <SimpleCharts.droid.ChartView
            android:id="@+id/chartView"
            android:layout_width="match_parent"
            android:layout_height="160dp" />
    </LinearLayout>
</ScrollView>
```

```csharp
protected override void OnCreate(Bundle savedInstanceState)
{
    base.OnCreate(savedInstanceState);

    SetContentView(Resource.Layout.Main);

    var entries = // ... see 1°) above
    var chart = // ... see 2°) above

    var chartView = FindViewById<ChartView>(Resource.Id.chartView);
    chartView.Chart = chart;
}
```

**UWP (Windows 10)**

![uwp-screen](Documentation/images/UWP-Screenshot.PNG)

```xml
<Page
    x:Class="SimpleCharts.Samples.Uwp.MainPage"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:local="using:SimpleCharts.Samples.Uwp"
    xmlns:SimpleCharts="using:SimpleCharts.Uwp"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    mc:Ignorable="d">

    <SimpleCharts:ChartView x:Name="chartView" />

</Page>
```

```csharp
public MainPage()
{
    this.InitializeComponent();

    var entries = // ... see 1°) above
    var chart = // ... see 2°) above

    this.chartView.Chart = chart;
}
```

**Xamarin.Forms**

```xml
<ContentPage 
    xmlns="http://xamarin.com/schemas/2014/forms" 
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" 
    xmlns:SimpleCharts="clr-namespace:SimpleCharts.Forms;assembly=SimpleCharts.Forms" 
    xmlns:local="clr-namespace:SimpleCharts.Samples.Forms" 
    x:Class="SimpleCharts.Samples.Forms.MainPage">

    <SimpleCharts:ChartView x:Name="chartView" />

</ContentPage>
```

```csharp
protected override void OnAppearing()
{
    base.OnAppearing();

    var entries = // ... see 1°) above
    var chart = // ... see 2°) above

    this.chartView.Chart = chart;
}
```

**Xamarin.macOS**

```csharp
public override void ViewDidLoad()
{
	base.ViewDidLoad();

	var entries = // ... see 1°) above
	var chart = // ... see 2°) above

	var chartView = new ChartView
	{
       Frame = this.View.Bounds,
       AutoresizingMask = NSViewResizingMask.WidthSizable | NSViewResizingMask.HeightSizable,
		Chart = chart,
	};

	this.View.AddSubview(chartView);
}
```

## Tutorials

* [Video: Charts for Xamarin Forms](https://www.youtube.com/watch?v=tmymWdmf1y4) by [@HoussemDellai](https://github.com/HoussemDellai)

## Usage

Available charts are `BarChart`, `PointChart`, `LineChart`, `DonutChart`, `RadialGaugeChart`, `RadarChart`. They all have several properties to tweak their rendering.

Those charts have a `Draw` method for platforms that haven't built in views.

## Compatibility

Built in views are provided for **UWP**, **Xamarin.Forms**, **Xamarin.iOS** and **Xamarin.Android**, **Xamarin.macOS**, but any other **.NET Standard 1.4** [SkiaSharp](https://github.com/mono/SkiaSharp) supported platform is also compatible (see one of the included `ChartView` implementations for more details).

## About

This project is just simple drawing on top of the awesome [SkiaSharp](https://github.com/mono/SkiaSharp) library. The purpose is not to have an heavily customizable charting library. If you want so, simply fork the code, since all of this is fairly simple. Their is no interaction, nor animation at the moment.

## Contributions

Contributions are welcome! If you find a bug please report it and if you want a feature please report it.

If you want to contribute code please file an issue and create a branch off of the current dev branch and file a pull request.

## License

MIT © [Aloïs Deniel](http://aloisdeniel.github.io)
