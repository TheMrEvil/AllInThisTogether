using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Reflectrify
{
	// Token: 0x0200000E RID: 14
	public static class FieldExtensions
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002692 File Offset: 0x00000892
		public static FieldCache Cache
		{
			[CompilerGenerated]
			get
			{
				return FieldExtensions.<Cache>k__BackingField;
			}
		} = new FieldCache();

		// Token: 0x06000029 RID: 41 RVA: 0x0000269C File Offset: 0x0000089C
		public static T GetField<T>(this object classInstance, string name, BindingFlags? flags = null, TimeSpan? timeout = null)
		{
			flags = new BindingFlags?(flags ?? DefaultBindingFlags.Instance);
			timeout = new TimeSpan?(timeout ?? Timeout.InfiniteTimeSpan);
			object orAdd = FieldExtensions.locks.GetOrAdd(classInstance, new object());
			if (Monitor.TryEnter(orAdd, timeout.Value))
			{
				try
				{
					FieldInfo field = FieldExtensions.Cache.GetField(classInstance.GetType(), name, flags.Value);
					if (field != null)
					{
						return (T)((object)field.GetValue(classInstance));
					}
				}
				finally
				{
					Monitor.Exit(orAdd);
				}
			}
			return default(T);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002764 File Offset: 0x00000964
		public static void SetField<T>(this object classInstance, string name, T value, BindingFlags? flags = null, TimeSpan? timeout = null)
		{
			flags = new BindingFlags?(flags ?? DefaultBindingFlags.Instance);
			timeout = new TimeSpan?(timeout ?? Timeout.InfiniteTimeSpan);
			object orAdd = FieldExtensions.locks.GetOrAdd(classInstance, new object());
			if (Monitor.TryEnter(orAdd, timeout.Value))
			{
				try
				{
					FieldInfo field = FieldExtensions.Cache.GetField(classInstance.GetType(), name, flags.Value);
					if (field != null)
					{
						field.SetValue(classInstance, value);
					}
				}
				finally
				{
					Monitor.Exit(orAdd);
				}
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000281C File Offset: 0x00000A1C
		public static T GetStaticField<T>(this Type type, string name, BindingFlags? flags = null, TimeSpan? timeout = null)
		{
			flags = new BindingFlags?(flags ?? DefaultBindingFlags.Static);
			timeout = new TimeSpan?(timeout ?? Timeout.InfiniteTimeSpan);
			object orAdd = FieldExtensions.locks.GetOrAdd(type, new object());
			if (Monitor.TryEnter(orAdd, timeout.Value))
			{
				try
				{
					FieldInfo field = FieldExtensions.Cache.GetField(type, name, flags.Value);
					if (field != null)
					{
						return (T)((object)field.GetValue(null));
					}
				}
				finally
				{
					Monitor.Exit(orAdd);
				}
			}
			return default(T);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028DC File Offset: 0x00000ADC
		public static void SetStaticField<T>(this Type type, string name, T value, BindingFlags? flags = null, TimeSpan? timeout = null)
		{
			flags = new BindingFlags?(flags ?? DefaultBindingFlags.Static);
			timeout = new TimeSpan?(timeout ?? Timeout.InfiniteTimeSpan);
			object orAdd = FieldExtensions.locks.GetOrAdd(type, new object());
			if (Monitor.TryEnter(orAdd, timeout.Value))
			{
				try
				{
					FieldInfo field = FieldExtensions.Cache.GetField(type, name, flags.Value);
					if (field != null)
					{
						field.SetValue(null, value);
					}
				}
				finally
				{
					Monitor.Exit(orAdd);
				}
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002990 File Offset: 0x00000B90
		// Note: this type is marked as 'beforefieldinit'.
		static FieldExtensions()
		{
		}

		// Token: 0x0400000B RID: 11
		[CompilerGenerated]
		private static readonly FieldCache <Cache>k__BackingField;

		// Token: 0x0400000C RID: 12
		private static readonly ConcurrentDictionary<object, object> locks = new ConcurrentDictionary<object, object>();
	}
}
