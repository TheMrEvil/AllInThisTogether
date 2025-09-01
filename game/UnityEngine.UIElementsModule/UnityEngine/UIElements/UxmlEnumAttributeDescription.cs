using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002E2 RID: 738
	public class UxmlEnumAttributeDescription<T> : TypedUxmlAttributeDescription<T> where T : struct, IConvertible
	{
		// Token: 0x0600189A RID: 6298 RVA: 0x000651C0 File Offset: 0x000633C0
		public UxmlEnumAttributeDescription()
		{
			bool flag = !typeof(T).IsEnum;
			if (flag)
			{
				throw new ArgumentException("T must be an enumerated type");
			}
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			base.defaultValue = Activator.CreateInstance<T>();
			UxmlEnumeration uxmlEnumeration = new UxmlEnumeration();
			List<string> list = new List<string>();
			foreach (object obj in Enum.GetValues(typeof(T)))
			{
				T t = (T)((object)obj);
				list.Add(t.ToString(CultureInfo.InvariantCulture));
			}
			uxmlEnumeration.values = list;
			base.restriction = uxmlEnumeration;
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x000652A8 File Offset: 0x000634A8
		public override string defaultValueAsString
		{
			get
			{
				T defaultValue = base.defaultValue;
				return defaultValue.ToString(CultureInfo.InvariantCulture.NumberFormat);
			}
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x000652D8 File Offset: 0x000634D8
		public override T GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			return base.GetValueFromBag<T>(bag, cc, (string s, T convertible) => UxmlEnumAttributeDescription<T>.ConvertValueToEnum<T>(s, convertible), base.defaultValue);
		}

		// Token: 0x0600189D RID: 6301 RVA: 0x00065318 File Offset: 0x00063518
		public bool TryGetValueFromBag(IUxmlAttributes bag, CreationContext cc, ref T value)
		{
			return base.TryGetValueFromBag<T>(bag, cc, (string s, T convertible) => UxmlEnumAttributeDescription<T>.ConvertValueToEnum<T>(s, convertible), base.defaultValue, ref value);
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x00065358 File Offset: 0x00063558
		private static U ConvertValueToEnum<U>(string v, U defaultValue)
		{
			bool flag = v == null || !Enum.IsDefined(typeof(U), v);
			U result;
			if (flag)
			{
				result = defaultValue;
			}
			else
			{
				U u = (U)((object)Enum.Parse(typeof(U), v));
				result = u;
			}
			return result;
		}

		// Token: 0x020002E3 RID: 739
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600189F RID: 6303 RVA: 0x000653A2 File Offset: 0x000635A2
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060018A0 RID: 6304 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x060018A1 RID: 6305 RVA: 0x000653AE File Offset: 0x000635AE
			internal T <GetValueFromBag>b__3_0(string s, T convertible)
			{
				return UxmlEnumAttributeDescription<T>.ConvertValueToEnum<T>(s, convertible);
			}

			// Token: 0x060018A2 RID: 6306 RVA: 0x000653AE File Offset: 0x000635AE
			internal T <TryGetValueFromBag>b__4_0(string s, T convertible)
			{
				return UxmlEnumAttributeDescription<T>.ConvertValueToEnum<T>(s, convertible);
			}

			// Token: 0x04000A92 RID: 2706
			public static readonly UxmlEnumAttributeDescription<T>.<>c <>9 = new UxmlEnumAttributeDescription<T>.<>c();

			// Token: 0x04000A93 RID: 2707
			public static Func<string, T, T> <>9__3_0;

			// Token: 0x04000A94 RID: 2708
			public static Func<string, T, T> <>9__4_0;
		}
	}
}
