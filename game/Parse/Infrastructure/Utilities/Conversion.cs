using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Parse.Infrastructure.Utilities
{
	// Token: 0x02000050 RID: 80
	public static class Conversion
	{
		// Token: 0x06000402 RID: 1026 RVA: 0x0000CAF2 File Offset: 0x0000ACF2
		public static T As<T>(object value) where T : class
		{
			return Conversion.ConvertTo<T>(value) as T;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000CB04 File Offset: 0x0000AD04
		public static T To<T>(object value)
		{
			return (T)((object)Conversion.ConvertTo<T>(value));
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000CB14 File Offset: 0x0000AD14
		internal static object ConvertTo<T>(object value)
		{
			if (value is T || value == null)
			{
				return value;
			}
			if (typeof(T).IsPrimitive)
			{
				return (T)((object)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture));
			}
			if (typeof(T).IsConstructedGenericType)
			{
				if (typeof(T).CheckWrappedWithNullable())
				{
					Type type = typeof(T).GenericTypeArguments[0];
					if (type != null && type.IsPrimitive)
					{
						return (T)((object)Convert.ChangeType(value, type, CultureInfo.InvariantCulture));
					}
				}
				Type interfaceType = Conversion.GetInterfaceType(value.GetType(), typeof(IList<>));
				if (interfaceType != null && typeof(T).GetGenericTypeDefinition() == typeof(IList<>))
				{
					return Activator.CreateInstance(typeof(FlexibleListWrapper<, >).MakeGenericType(new Type[]
					{
						typeof(T).GenericTypeArguments[0],
						interfaceType.GenericTypeArguments[0]
					}), new object[]
					{
						value
					});
				}
				Type interfaceType2 = Conversion.GetInterfaceType(value.GetType(), typeof(IDictionary<, >));
				if (interfaceType2 != null && typeof(T).GetGenericTypeDefinition() == typeof(IDictionary<, >))
				{
					return Activator.CreateInstance(typeof(FlexibleDictionaryWrapper<, >).MakeGenericType(new Type[]
					{
						typeof(T).GenericTypeArguments[1],
						interfaceType2.GenericTypeArguments[1]
					}), new object[]
					{
						value
					});
				}
			}
			return value;
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000CCB3 File Offset: 0x0000AEB3
		private static Dictionary<Tuple<Type, Type>, Type> InterfaceLookupCache
		{
			[CompilerGenerated]
			get
			{
				return Conversion.<InterfaceLookupCache>k__BackingField;
			}
		} = new Dictionary<Tuple<Type, Type>, Type>();

		// Token: 0x06000406 RID: 1030 RVA: 0x0000CCBC File Offset: 0x0000AEBC
		private static Type GetInterfaceType(Type objType, Type genericInterfaceType)
		{
			Tuple<Type, Type> key = new Tuple<Type, Type>(objType, genericInterfaceType);
			if (Conversion.InterfaceLookupCache.ContainsKey(key))
			{
				return Conversion.InterfaceLookupCache[key];
			}
			foreach (Type type in objType.GetInterfaces())
			{
				if (type.IsConstructedGenericType && type.GetGenericTypeDefinition() == genericInterfaceType)
				{
					return Conversion.InterfaceLookupCache[key] = type;
				}
			}
			return null;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000CD2C File Offset: 0x0000AF2C
		// Note: this type is marked as 'beforefieldinit'.
		static Conversion()
		{
		}

		// Token: 0x040000C7 RID: 199
		[CompilerGenerated]
		private static readonly Dictionary<Tuple<Type, Type>, Type> <InterfaceLookupCache>k__BackingField;
	}
}
