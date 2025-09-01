using System;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x020002B0 RID: 688
	internal static class ParameterProviderExtensions
	{
		// Token: 0x0600146E RID: 5230 RVA: 0x0003F144 File Offset: 0x0003D344
		public static int IndexOf(this IParameterProvider provider, ParameterExpression parameter)
		{
			int i = 0;
			int parameterCount = provider.ParameterCount;
			while (i < parameterCount)
			{
				if (provider.GetParameter(i) == parameter)
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x0003F171 File Offset: 0x0003D371
		public static bool Contains(this IParameterProvider provider, ParameterExpression parameter)
		{
			return provider.IndexOf(parameter) >= 0;
		}
	}
}
