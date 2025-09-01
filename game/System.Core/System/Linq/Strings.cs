using System;

namespace System.Linq
{
	// Token: 0x02000096 RID: 150
	internal static class Strings
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x0000F794 File Offset: 0x0000D994
		internal static string ArgumentNotIEnumerableGeneric(string message)
		{
			return SR.Format("{0} is not IEnumerable<>", message);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000F7A1 File Offset: 0x0000D9A1
		internal static string ArgumentNotValid(string message)
		{
			return SR.Format("Argument {0} is not valid", message);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000F7AE File Offset: 0x0000D9AE
		internal static string NoMethodOnType(string name, object type)
		{
			return SR.Format("There is no method '{0}' on type '{1}'", name, type);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000F7BC File Offset: 0x0000D9BC
		internal static string NoMethodOnTypeMatchingArguments(string name, object type)
		{
			return SR.Format("There is no method '{0}' on type '{1}' that matches the specified arguments", name, type);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000F7CA File Offset: 0x0000D9CA
		internal static string EnumeratingNullEnumerableExpression()
		{
			return "Cannot enumerate a query created from a null IEnumerable<>";
		}
	}
}
