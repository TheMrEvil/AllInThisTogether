using System;

namespace Mono.CSharp
{
	// Token: 0x02000263 RID: 611
	public static class TypeExtensions
	{
		// Token: 0x06001E20 RID: 7712 RVA: 0x000936E0 File Offset: 0x000918E0
		public static string GetNamespace(this Type t)
		{
			string result;
			try
			{
				result = t.Namespace;
			}
			catch
			{
				result = null;
			}
			return result;
		}
	}
}
