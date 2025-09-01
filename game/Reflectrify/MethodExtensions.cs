using System;
using System.Collections.Concurrent;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Reflectrify
{
	// Token: 0x0200000F RID: 15
	public static class MethodExtensions
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000029A6 File Offset: 0x00000BA6
		public static MethodCache Cache
		{
			[CompilerGenerated]
			get
			{
				return MethodExtensions.<Cache>k__BackingField;
			}
		} = new MethodCache();

		// Token: 0x0600002F RID: 47 RVA: 0x000029B0 File Offset: 0x00000BB0
		public static T InvokeMethod<T>(this object classInstance, string name, BindingFlags? flags = null, params object[] parameters)
		{
			flags = new BindingFlags?(flags ?? DefaultBindingFlags.Instance);
			MethodInfo method = MethodExtensions.Cache.GetMethod(classInstance.GetType(), name, flags.Value, null);
			if (method != null)
			{
				return (T)((object)method.Invoke(classInstance, parameters));
			}
			return default(T);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A18 File Offset: 0x00000C18
		public static T InvokeMethodThreadSafe<T>(this object classInstance, string name, TimeSpan timeout, BindingFlags? flags = null, params object[] parameters)
		{
			flags = new BindingFlags?(flags ?? DefaultBindingFlags.Instance);
			bool flag = false;
			try
			{
				Monitor.TryEnter(classInstance, timeout, ref flag);
				if (flag)
				{
					MethodInfo method = MethodExtensions.Cache.GetMethod(classInstance.GetType(), name, flags.Value, null);
					if (method != null)
					{
						return (T)((object)method.Invoke(classInstance, parameters));
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(classInstance);
				}
			}
			return default(T);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public static T InvokeStaticMethod<T>(this Type type, string name, BindingFlags? flags = null, params object[] parameters)
		{
			flags = new BindingFlags?(flags ?? DefaultBindingFlags.Static);
			MethodInfo method = MethodExtensions.Cache.GetMethod(type, name, flags.Value, null);
			if (method != null)
			{
				return (T)((object)method.Invoke(null, parameters));
			}
			return default(T);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002B14 File Offset: 0x00000D14
		public static T InvokeStaticMethodThreadSafe<T>(this Type type, string name, TimeSpan timeout, BindingFlags? flags = null, params object[] parameters)
		{
			flags = new BindingFlags?(flags ?? DefaultBindingFlags.Static);
			object orAdd = MethodExtensions.typeLocks.GetOrAdd(type, (Type _) => new object());
			bool flag = false;
			try
			{
				Monitor.TryEnter(orAdd, timeout, ref flag);
				if (flag)
				{
					MethodInfo method = MethodExtensions.Cache.GetMethod(type, name, flags.Value, null);
					if (method != null)
					{
						return (T)((object)method.Invoke(null, parameters));
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(orAdd);
				}
			}
			return default(T);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public static T InvokeGenericMethod<T>(this object classInstance, string name, Type[] genericTypes, BindingFlags? flags = null, params object[] parameters)
		{
			flags = new BindingFlags?(flags ?? DefaultBindingFlags.Instance);
			MethodInfo method = MethodExtensions.Cache.GetMethod(classInstance.GetType(), name, flags.Value, genericTypes);
			if (method != null)
			{
				return (T)((object)method.Invoke(classInstance, parameters));
			}
			return default(T);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002C3C File Offset: 0x00000E3C
		public static T InvokeGenericMethodThreadSafe<T>(this object classInstance, string name, Type[] genericTypes, TimeSpan timeout, BindingFlags? flags = null, params object[] parameters)
		{
			flags = new BindingFlags?(flags ?? DefaultBindingFlags.Instance);
			object orAdd = MethodExtensions.typeLocks.GetOrAdd(classInstance.GetType(), (Type _) => new object());
			bool flag = false;
			try
			{
				Monitor.TryEnter(orAdd, timeout, ref flag);
				if (flag)
				{
					MethodInfo method = MethodExtensions.Cache.GetMethod(classInstance.GetType(), name, flags.Value, genericTypes);
					if (method != null)
					{
						return (T)((object)method.Invoke(classInstance, parameters));
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(orAdd);
				}
			}
			return default(T);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002D08 File Offset: 0x00000F08
		public static T InvokeGenericStaticMethod<T>(this Type type, string name, Type[] genericTypes, BindingFlags? flags = null, params object[] parameters)
		{
			flags = new BindingFlags?(flags ?? DefaultBindingFlags.Static);
			MethodInfo method = MethodExtensions.Cache.GetMethod(type, name, flags.Value, genericTypes);
			if (method != null)
			{
				return (T)((object)method.Invoke(null, parameters));
			}
			return default(T);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002D6C File Offset: 0x00000F6C
		public static T InvokeGenericStaticMethodThreadSafe<T>(this Type type, string name, Type[] genericTypes, TimeSpan timeout, BindingFlags? flags = null, params object[] parameters)
		{
			flags = new BindingFlags?(flags ?? DefaultBindingFlags.Static);
			object orAdd = MethodExtensions.typeLocks.GetOrAdd(type, (Type _) => new object());
			bool flag = false;
			try
			{
				Monitor.TryEnter(orAdd, timeout, ref flag);
				if (flag)
				{
					MethodInfo method = MethodExtensions.Cache.GetMethod(type, name, flags.Value, genericTypes);
					if (method != null)
					{
						return (T)((object)method.Invoke(null, parameters));
					}
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(orAdd);
				}
			}
			return default(T);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002E2C File Offset: 0x0000102C
		// Note: this type is marked as 'beforefieldinit'.
		static MethodExtensions()
		{
		}

		// Token: 0x0400000D RID: 13
		[CompilerGenerated]
		private static readonly MethodCache <Cache>k__BackingField;

		// Token: 0x0400000E RID: 14
		private static ConcurrentDictionary<Type, object> typeLocks = new ConcurrentDictionary<Type, object>();

		// Token: 0x02000017 RID: 23
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__7<T>
		{
			// Token: 0x06000059 RID: 89 RVA: 0x0000378F File Offset: 0x0000198F
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__7()
			{
			}

			// Token: 0x0600005A RID: 90 RVA: 0x0000379B File Offset: 0x0000199B
			public <>c__7()
			{
			}

			// Token: 0x0600005B RID: 91 RVA: 0x000037A3 File Offset: 0x000019A3
			internal object <InvokeStaticMethodThreadSafe>b__7_0(Type _)
			{
				return new object();
			}

			// Token: 0x04000026 RID: 38
			public static readonly MethodExtensions.<>c__7<T> <>9 = new MethodExtensions.<>c__7<T>();

			// Token: 0x04000027 RID: 39
			public static Func<Type, object> <>9__7_0;
		}

		// Token: 0x02000018 RID: 24
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__9<T>
		{
			// Token: 0x0600005C RID: 92 RVA: 0x000037AA File Offset: 0x000019AA
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__9()
			{
			}

			// Token: 0x0600005D RID: 93 RVA: 0x000037B6 File Offset: 0x000019B6
			public <>c__9()
			{
			}

			// Token: 0x0600005E RID: 94 RVA: 0x000037BE File Offset: 0x000019BE
			internal object <InvokeGenericMethodThreadSafe>b__9_0(Type _)
			{
				return new object();
			}

			// Token: 0x04000028 RID: 40
			public static readonly MethodExtensions.<>c__9<T> <>9 = new MethodExtensions.<>c__9<T>();

			// Token: 0x04000029 RID: 41
			public static Func<Type, object> <>9__9_0;
		}

		// Token: 0x02000019 RID: 25
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__11<T>
		{
			// Token: 0x0600005F RID: 95 RVA: 0x000037C5 File Offset: 0x000019C5
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__11()
			{
			}

			// Token: 0x06000060 RID: 96 RVA: 0x000037D1 File Offset: 0x000019D1
			public <>c__11()
			{
			}

			// Token: 0x06000061 RID: 97 RVA: 0x000037D9 File Offset: 0x000019D9
			internal object <InvokeGenericStaticMethodThreadSafe>b__11_0(Type _)
			{
				return new object();
			}

			// Token: 0x0400002A RID: 42
			public static readonly MethodExtensions.<>c__11<T> <>9 = new MethodExtensions.<>c__11<T>();

			// Token: 0x0400002B RID: 43
			public static Func<Type, object> <>9__11_0;
		}
	}
}
