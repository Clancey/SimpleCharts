using System;
namespace SimpleCharts
{
	internal static class ReflectionHelper
	{
		public static float GetValue(this object entry, string property)
		{
			if (entry == null)
				return 0;
			var prop = entry.GetType().GetProperty(property);
			return (float) prop.GetValue(entry);
		}
		public static string GetStringValue(this object entry, string property)
		{
			if (entry == null)
				return null;
			var prop = entry.GetType().GetProperty(property);
			return prop.GetValue(entry).ToString();
		}
		public static T GetValue<T>(this object entry, string property)
		{
			if (entry == null)
				return default(T);
			var prop = entry.GetType().GetProperty(property);
			if (prop.PropertyType != typeof(T))
				throw new InvalidCastException($"{entry.GetType()} {prop.PropertyType} cannot be casted to {typeof(T)} ");
			return (T)prop.GetValue(entry);
		}
	}
}
