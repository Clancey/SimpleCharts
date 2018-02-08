using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using SkiaSharp;

namespace SimpleCharts
{
	public abstract class Chart
	{
		#region Properties
		/// <summary>
		/// Used by Xamarin Forms
		/// </summary>
		/// <value>The parent.</value>
		internal object Parent { get; set; }
		/// <summary>
		/// Gets or sets the global margin.
		/// </summary>
		/// <value>The margin.</value>
		public float Margin { get; set; } = 20;

		/// <summary>
		/// Gets or sets the text size of the labels.
		/// </summary>
		/// <value>The size of the label text.</value>
		public float LabelTextSize { get; set; } = 16;

		/// <summary>
		/// Gets or sets the color of the chart background.
		/// </summary>
		/// <value>The color of the background.</value>
		public SKColor BackgroundColor { get; set; } = SKColors.White;

		public string ValueProperty { get; set; }
		public string ValueLabelProperty { get; set; }
		public string LabelProperty { get; set; }
		public SKColor DefaultColor { get; set; } = SKColors.Black;
		public SKColor DefaultTextColor { get; set; } = SKColors.Gray;
		public string ColorProperty { get; set; }
		public string TextColorProperty { get; set; }
		public bool AllowsZoom { get; set; } = true;
		/// <summary>
		/// Gets or sets the data entries.
		/// </summary>
		/// <value>The entries.</value>
		public IEnumerable<object> Entries
		{
			get
			{
				return entries;
			}
			set
			{
				if (entries == value)
					return;
				var oldNotify = entries as INotifyCollectionChanged;
				if (oldNotify != null)
					oldNotify.CollectionChanged -= EntriesCollectionChanged;
				var notifiy = value as INotifyCollectionChanged;
				if (notifiy != null)
					notifiy.CollectionChanged += EntriesCollectionChanged;
				entries = value;
				cachedMaxValue = cachedMinValue = null;
				DrawInvalidated?.Invoke();
			}
		}

		IEnumerable<object> entries;

		/// <summary>
		/// Event fires whenever the data has changed
		/// </summary>
		public Action DrawInvalidated;

		/// <summary>
		/// Gets or sets the minimum value from entries. If not defined, it will be the minimum between zero and the 
		/// minimal entry value.
		/// </summary>
		/// <value>The minimum value.</value>
		public float MinValue
		{
			get
			{
				if (!(this.Entries?.Any() ?? false))
				{
					return 0;
				}
				if (cachedMinValue.HasValue)
					return cachedMinValue.Value;
				if (this.InternalMinValue == null)
				{
					var min = this.Entries.Min(x => GetValue(x));
					if (!AllowsZoom)
						min = Math.Min(0, min);
					return (cachedMinValue = min).Value;
				}

				return (cachedMinValue = Math.Min(this.InternalMinValue.Value, this.Entries.Min(x => GetValue(x)))).Value;
			}

			set => this.InternalMinValue = value;
		}

		/// <summary>
		/// Gets or sets the maximum value from entries. If not defined, it will be the maximum between zero and the 
		/// maximum entry value.
		/// </summary>
		/// <value>The maximum value.</value>
		public float MaxValue
		{
			get
			{
				if (!(this.Entries?.Any() ?? false))
				{
					return 0;
				}
				if (cachedMaxValue.HasValue)
					return cachedMaxValue.Value;
				if (this.InternalMaxValue == null)
				{
					return (cachedMaxValue = Math.Max(0, this.Entries.Max(x => GetValue(x)))).Value;
				}

				return (cachedMaxValue = Math.Max(this.InternalMaxValue.Value, this.Entries.Max(x => GetValue(x)))).Value;
			}

			set => this.InternalMaxValue = value;
		}

		protected float? cachedMaxValue;
		protected float? cachedMinValue;

		/// <summary>
		/// Gets or sets the internal minimum value (that can be null).
		/// </summary>
		/// <value>The internal minimum value.</value>
		protected float? InternalMinValue { get; set; }

		/// <summary>
		/// Gets or sets the internal max value (that can be null).
		/// </summary>
		/// <value>The internal max value.</value>
		protected float? InternalMaxValue { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Draw the  graph onto the specified canvas.
		/// </summary>
		/// <param name="canvas">The canvas.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		public void Draw(SKCanvas canvas, int width, int height)
		{
			canvas.Clear(this.BackgroundColor);

			this.DrawContent(canvas, width, height);
		}

		/// <summary>
		/// Draws the chart content.
		/// </summary>
		/// <param name="canvas">The canvas.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		public abstract void DrawContent(SKCanvas canvas, int width, int height);

		/// <summary>
		/// Draws caption elements on the right or left side of the chart.
		/// </summary>
		/// <param name="canvas">The canvas.</param>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		/// <param name="entries">The entries.</param>
		/// <param name="isLeft">If set to <c>true</c> is left.</param>
		protected void DrawCaptionElements(SKCanvas canvas, int width, int height, List<object> entries, bool isLeft)
		{
			var margin = 2 * this.Margin;
			var availableHeight = height - (2 * margin);
			var x = isLeft ? this.Margin : (width - this.Margin - this.LabelTextSize);
			var ySpace = (availableHeight - this.LabelTextSize) / ((entries.Count <= 1) ? 1 : entries.Count - 1);

			for (int i = 0; i < entries.Count; i++)
			{
				var entry = entries.ElementAt(i);
				var y = margin + (i * ySpace);
				if (entries.Count <= 1)
				{
					y += (availableHeight - this.LabelTextSize) / 2;
				}

				var label = GetLabel(entry);
				var valueLabel = GetValueLabel(entry);
				var hasLabel = !string.IsNullOrEmpty(label);
				var hasValueLabel = !string.IsNullOrEmpty(valueLabel);

				if (hasLabel || hasValueLabel)
				{
					var hasOffset = hasLabel && hasValueLabel;
					var captionMargin = this.LabelTextSize * 0.60f;
					var space = hasOffset ? captionMargin : 0;
					var captionX = isLeft ? this.Margin : width - this.Margin - this.LabelTextSize;

					var color = GetColor(entry);
					using (var paint = new SKPaint
					{
						Style = SKPaintStyle.Fill,
						Color = color,
					})
					{
						var rect = SKRect.Create(captionX, y, this.LabelTextSize, this.LabelTextSize);
						canvas.DrawRect(rect, paint);
					}

					if (isLeft)
					{
						captionX += this.LabelTextSize + captionMargin;
					}
					else
					{
						captionX -= captionMargin;
					}

					canvas.DrawCaptionLabels(label, GetTextColor(entry), valueLabel, color, this.LabelTextSize, new SKPoint(captionX, y + (this.LabelTextSize / 2)), isLeft ? SKTextAlign.Left : SKTextAlign.Right);
				}
			}
		}

		internal virtual float GetValue(object item)
		{
			var entry = item as Entry;
			if (entry != null)
				return entry.Value;
			if (string.IsNullOrWhiteSpace(ValueProperty))
				throw new NotSupportedException("You must set the ValueProperty");
			return item.GetValue(ValueProperty);
		}
		internal virtual string GetLabel(object item)
		{
			var entry = item as Entry;
			if (entry != null)
				return entry.Label;
			if (string.IsNullOrWhiteSpace(LabelProperty))
				return null;
			return item.GetStringValue(LabelProperty);

		}
		internal virtual string GetValueLabel(object item)
		{
			var entry = item as Entry;
			if (entry != null)
				return entry.ValueLabel;
			if (string.IsNullOrWhiteSpace(ValueLabelProperty))
				return null;
			return item.GetStringValue(ValueLabelProperty);
		}

		internal virtual SKColor GetColor(object item)
		{
			var entry = item as Entry;
			if (entry != null)
				return entry.Color;
			if (!string.IsNullOrWhiteSpace(ColorProperty))
				return item.GetValue<SKColor>(ColorProperty);
			return DefaultColor;
		}
		internal virtual SKColor GetTextColor(object item)
		{
			var entry = item as Entry;
			if (entry != null)
				return entry.TextColor;
			if (!string.IsNullOrWhiteSpace(TextColorProperty))
				return item.GetValue<SKColor>(TextColorProperty);
			return DefaultTextColor;
		}

		protected virtual void EntriesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			cachedMaxValue = cachedMinValue = null;
			DrawInvalidated?.Invoke();
		}

		#endregion
	}
}