using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Reflectrify
{
	// Token: 0x02000010 RID: 16
	public static class PropertyExtensions
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002E42 File Offset: 0x00001042
		public static PropertyCache Cache
		{
			[CompilerGenerated]
			get
			{
				return PropertyExtensions.<Cache>k__BackingField;
			}
		} = new PropertyCache();

		// Token: 0x06000039 RID: 57 RVA: 0x00002E4C File Offset: 0x0000104C
		public static T GetProperty<T>(this object classInstance, string name, BindingFlags? flags = null, TimeSpan? timeout = null)
		{
			flags = new BindingFlags?(flags ?? DefaultBindingFlags.Instance);
			timeout = new TimeSpan?(timeout ?? Timeout.InfiniteTimeSpan);
			object orAdd = PropertyExtensions.locks.GetOrAdd(classInstance, new object());
			if (Monitor.TryEnter(orAdd, timeout.Value))
			{
				try
				{
					PropertyInfo property = PropertyExtensions.Cache.GetProperty(classInstance.GetType(), name, flags.Value);
					if (property != null && property.CanRead)
					{
						return (T)((object)property.GetValue(classInstance));
					}
				}
				finally
				{
					Monitor.Exit(orAdd);
				}
			}
			return default(T);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002F1C File Offset: 0x0000111C
		public static void SetProperty<T>(this object classInstance, string name, T value, BindingFlags? flags = null, TimeSpan? timeout = null)
		{
			flags = new BindingFlags?(flags ?? DefaultBindingFlags.Instance);
			timeout = new TimeSpan?(timeout ?? Timeout.InfiniteTimeSpan);
			object orAdd = PropertyExtensions.locks.GetOrAdd(classInstance, new object());
			if (Monitor.TryEnter(orAdd, timeout.Value))
			{
				try
				{
					PropertyInfo property = PropertyExtensions.Cache.GetProperty(classInstance.GetType(), name, flags.Value);
					if (property != null && property.CanWrite)
					{
						property.SetValue(classInstance, value);
					}
				}
				finally
				{
					Monitor.Exit(orAdd);
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002FDC File Offset: 0x000011DC
		public static T GetStaticProperty<T>(this Type type, string name, BindingFlags? flags = null, TimeSpan? timeout = null)
		{
			flags = new BindingFlags?(flags ?? DefaultBindingFlags.Static);
			timeout = new TimeSpan?(timeout ?? Timeout.InfiniteTimeSpan);
			object orAdd = PropertyExtensions.locks.GetOrAdd(type, new object());
			if (Monitor.TryEnter(orAdd, timeout.Value))
			{
				try
				{
					PropertyInfo property = PropertyExtensions.Cache.GetProperty(type, name, flags.Value);
					if (property != null && property.CanRead)
					{
						return (T)((object)property.GetValue(null));
					}
				}
				finally
				{
					Monitor.Exit(orAdd);
				}
			}
			return default(T);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000030A4 File Offset: 0x000012A4
		public static void SetStaticProperty<T>(this Type type, string name, T value, BindingFlags? flags = null, TimeSpan? timeout = null)
		{
			flags = new BindingFlags?(flags ?? DefaultBindingFlags.Static);
			timeout = new TimeSpan?(timeout ?? Timeout.InfiniteTimeSpan);
			object orAdd = PropertyExtensions.locks.GetOrAdd(type, new object());
			if (Monitor.TryEnter(orAdd, timeout.Value))
			{
				try
				{
					PropertyInfo property = PropertyExtensions.Cache.GetProperty(type, name, flags.Value);
					if (property != null && property.CanWrite)
					{
						property.SetValue(null, value);
					}
				}
				finally
				{
					Monitor.Exit(orAdd);
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003160 File Offset: 0x00001360
		// Note: this type is marked as 'beforefieldinit'.
		static PropertyExtensions()
		{
		}

		// Token: 0x0400000F RID: 15
		[CompilerGenerated]
		private static readonly PropertyCache <Cache>k__BackingField;

		// Token: 0x04000010 RID: 16
		private static readonly ConcurrentDictionary<object, object> locks = new ConcurrentDictionary<object, object>();
	}
}
