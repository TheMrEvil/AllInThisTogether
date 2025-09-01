using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Reflectrify
{
	// Token: 0x02000011 RID: 17
	public static class TypeExtensions
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003176 File Offset: 0x00001376
		public static TypeCache Cache
		{
			[CompilerGenerated]
			get
			{
				return TypeExtensions.<Cache>k__BackingField;
			}
		} = new TypeCache();

		// Token: 0x0600003F RID: 63 RVA: 0x00003180 File Offset: 0x00001380
		public static IEnumerable<MethodInfo> GetMethodInfo(this Type type, BindingFlags? flags = null, bool inherited = true)
		{
			flags = new BindingFlags?(flags ?? (BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
			return TypeExtensions.Cache.GetOrAddMethods(type, flags.Value, inherited);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000031BD File Offset: 0x000013BD
		public static bool Implements(this Type type, Type interfaceType)
		{
			return type.GetInterfaces().Contains(interfaceType);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000031CC File Offset: 0x000013CC
		public static IEnumerable<ValueTuple<EventInfo, T>> GetEventsWithAttribute<T>(this Type type, BindingFlags? flags = null, bool inherit = true) where T : Attribute
		{
			HashSet<ValueTuple<EventInfo, T>> hashSet = new HashSet<ValueTuple<EventInfo, T>>();
			foreach (EventInfo eventInfo in type.GetEvents(flags ?? DefaultBindingFlags.Instance))
			{
				object[] customAttributes = eventInfo.GetCustomAttributes(typeof(T), inherit);
				for (int j = 0; j < customAttributes.Length; j++)
				{
					T item = (T)((object)customAttributes[j]);
					hashSet.Add(new ValueTuple<EventInfo, T>(eventInfo, item));
				}
			}
			return hashSet;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003258 File Offset: 0x00001458
		public static IEnumerable<ValueTuple<MethodInfo, T>> GetMethodsWithAttribute<T>(this Type type, BindingFlags? flags = null, bool inherit = true) where T : Attribute
		{
			HashSet<ValueTuple<MethodInfo, T>> hashSet = new HashSet<ValueTuple<MethodInfo, T>>();
			foreach (MethodInfo methodInfo in type.GetMethods(flags ?? DefaultBindingFlags.Instance))
			{
				object[] customAttributes = methodInfo.GetCustomAttributes(typeof(T), inherit);
				for (int j = 0; j < customAttributes.Length; j++)
				{
					T item = (T)((object)customAttributes[j]);
					hashSet.Add(new ValueTuple<MethodInfo, T>(methodInfo, item));
				}
			}
			return hashSet;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000032E4 File Offset: 0x000014E4
		public static IEnumerable<ValueTuple<PropertyInfo, T>> GetPropertiesWithAttribute<T>(this Type type, BindingFlags? flags = null, bool inherit = true) where T : Attribute
		{
			HashSet<ValueTuple<PropertyInfo, T>> hashSet = new HashSet<ValueTuple<PropertyInfo, T>>();
			foreach (PropertyInfo propertyInfo in type.GetProperties(flags ?? DefaultBindingFlags.Instance))
			{
				object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(T), inherit);
				for (int j = 0; j < customAttributes.Length; j++)
				{
					T item = (T)((object)customAttributes[j]);
					hashSet.Add(new ValueTuple<PropertyInfo, T>(propertyInfo, item));
				}
			}
			return hashSet;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003370 File Offset: 0x00001570
		public static IEnumerable<ValueTuple<FieldInfo, T>> GetFieldsWithAttribute<T>(this Type type, BindingFlags? flags = null, bool inherit = true) where T : Attribute
		{
			HashSet<ValueTuple<FieldInfo, T>> hashSet = new HashSet<ValueTuple<FieldInfo, T>>();
			foreach (FieldInfo fieldInfo in type.GetFields(flags ?? DefaultBindingFlags.Instance))
			{
				object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(T), inherit);
				for (int j = 0; j < customAttributes.Length; j++)
				{
					T item = (T)((object)customAttributes[j]);
					hashSet.Add(new ValueTuple<FieldInfo, T>(fieldInfo, item));
				}
			}
			return hashSet;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000033FA File Offset: 0x000015FA
		// Note: this type is marked as 'beforefieldinit'.
		static TypeExtensions()
		{
		}

		// Token: 0x04000011 RID: 17
		[CompilerGenerated]
		private static readonly TypeCache <Cache>k__BackingField;
	}
}
