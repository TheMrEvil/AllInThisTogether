using System;

namespace IKVM.Reflection
{
	// Token: 0x02000067 RID: 103
	public static class IntrospectionExtensions
	{
		// Token: 0x060005D4 RID: 1492 RVA: 0x000117B6 File Offset: 0x0000F9B6
		public static TypeInfo GetTypeInfo(Type type)
		{
			return type.GetTypeInfo();
		}
	}
}
