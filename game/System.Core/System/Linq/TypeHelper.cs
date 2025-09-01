using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Linq
{
	// Token: 0x02000097 RID: 151
	internal static class TypeHelper
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x0000F7D4 File Offset: 0x0000D9D4
		internal static Type FindGenericType(Type definition, Type type)
		{
			bool? flag = null;
			while (type != null && type != typeof(object))
			{
				if (type.IsGenericType && type.GetGenericTypeDefinition() == definition)
				{
					return type;
				}
				if (flag == null)
				{
					flag = new bool?(definition.IsInterface);
				}
				if (flag.GetValueOrDefault())
				{
					foreach (Type type2 in type.GetInterfaces())
					{
						Type type3 = TypeHelper.FindGenericType(definition, type2);
						if (type3 != null)
						{
							return type3;
						}
					}
				}
				type = type.BaseType;
			}
			return null;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000F878 File Offset: 0x0000DA78
		internal static IEnumerable<MethodInfo> GetStaticMethods(this Type type)
		{
			return from m in type.GetRuntimeMethods()
			where m.IsStatic
			select m;
		}

		// Token: 0x02000098 RID: 152
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060004D4 RID: 1236 RVA: 0x0000F8A4 File Offset: 0x0000DAA4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060004D5 RID: 1237 RVA: 0x00002162 File Offset: 0x00000362
			public <>c()
			{
			}

			// Token: 0x060004D6 RID: 1238 RVA: 0x0000F8B0 File Offset: 0x0000DAB0
			internal bool <GetStaticMethods>b__1_0(MethodInfo m)
			{
				return m.IsStatic;
			}

			// Token: 0x04000458 RID: 1112
			public static readonly TypeHelper.<>c <>9 = new TypeHelper.<>c();

			// Token: 0x04000459 RID: 1113
			public static Func<MethodInfo, bool> <>9__1_0;
		}
	}
}
