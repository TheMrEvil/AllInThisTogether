using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Internal
{
	// Token: 0x02000005 RID: 5
	internal static class AttributeServices
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000020E6 File Offset: 0x000002E6
		public static T[] GetAttributes<T>(this ICustomAttributeProvider attributeProvider) where T : class
		{
			return (T[])attributeProvider.GetCustomAttributes(typeof(T), false);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020FE File Offset: 0x000002FE
		public static T[] GetAttributes<T>(this ICustomAttributeProvider attributeProvider, bool inherit) where T : class
		{
			return (T[])attributeProvider.GetCustomAttributes(typeof(T), inherit);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002116 File Offset: 0x00000316
		public static T GetFirstAttribute<T>(this ICustomAttributeProvider attributeProvider) where T : class
		{
			return attributeProvider.GetAttributes<T>().FirstOrDefault<T>();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002123 File Offset: 0x00000323
		public static T GetFirstAttribute<T>(this ICustomAttributeProvider attributeProvider, bool inherit) where T : class
		{
			return attributeProvider.GetAttributes(inherit).FirstOrDefault<T>();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002131 File Offset: 0x00000331
		public static bool IsAttributeDefined<T>(this ICustomAttributeProvider attributeProvider) where T : class
		{
			return attributeProvider.IsDefined(typeof(T), false);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002144 File Offset: 0x00000344
		public static bool IsAttributeDefined<T>(this ICustomAttributeProvider attributeProvider, bool inherit) where T : class
		{
			return attributeProvider.IsDefined(typeof(T), inherit);
		}
	}
}
