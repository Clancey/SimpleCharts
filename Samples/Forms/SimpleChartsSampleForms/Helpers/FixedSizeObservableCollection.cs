using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
namespace System.Collections.Generic
{
	public class FixedSizeObservableCollection<T> : ObservableCollection<T>
	{
		private readonly object privateLockObject = new object();

		public Action<T> OnDequeue { get; set; }

		public int Size { get; private set; }

		public FixedSizeObservableCollection(int size)
		{
			Size = size;
		}

		bool isBulkUpdating;
		protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			if(!isBulkUpdating)
				base.OnCollectionChanged(e);
		}

		public void Replace(IEnumerable<T> items)
		{
			if (!items?.Any() ?? false)
			{
				this.Clear();
				return;
			}
			isBulkUpdating = true;
			this.Clear();

			foreach (var item in items)
				this.Add(item);
			isBulkUpdating = false;
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}
		public void AddRange(IEnumerable<T> items)
		{
			if (!items?.Any() ?? false)
			{
				return;
			}

			isBulkUpdating = true;
			var startingIndex = items.Count();
			foreach (var item in items)
				this.Add(item);
			isBulkUpdating = false;
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, items.ToList()));
		}
		protected override void InsertItem(int index, T item)
		{
			lock (privateLockObject)
			{
				base.InsertItem(index, item);
				while (this.Count > Size)
				{
					T outObj = this[0];
					this.Remove(outObj);
				}

			}
		}
		protected override void RemoveItem(int index)
		{
			var item = this[index];
			base.RemoveItem(index);
			OnDequeue?.Invoke(item);
		}
	}
}
