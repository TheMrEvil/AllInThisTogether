using System;

namespace System.Linq
{
	// Token: 0x02000094 RID: 148
	internal static class Error
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x0000CBBF File Offset: 0x0000ADBF
		internal static Exception ArgumentNotIEnumerableGeneric(string message)
		{
			return new ArgumentException(Strings.ArgumentNotIEnumerableGeneric(message));
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000CBCC File Offset: 0x0000ADCC
		internal static Exception ArgumentNotValid(string message)
		{
			return new ArgumentException(Strings.ArgumentNotValid(message));
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000CBD9 File Offset: 0x0000ADD9
		internal static Exception NoMethodOnType(string name, object type)
		{
			return new InvalidOperationException(Strings.NoMethodOnType(name, type));
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000CBE7 File Offset: 0x0000ADE7
		internal static Exception NoMethodOnTypeMatchingArguments(string name, object type)
		{
			return new InvalidOperationException(Strings.NoMethodOnTypeMatchingArguments(name, type));
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000CBF5 File Offset: 0x0000ADF5
		internal static Exception EnumeratingNullEnumerableExpression()
		{
			return new InvalidOperationException(Strings.EnumeratingNullEnumerableExpression());
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000CC01 File Offset: 0x0000AE01
		internal static Exception ArgumentNull(string s)
		{
			return new ArgumentNullException(s);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000CC09 File Offset: 0x0000AE09
		internal static Exception ArgumentOutOfRange(string s)
		{
			return new ArgumentOutOfRangeException(s);
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000CC11 File Offset: 0x0000AE11
		internal static Exception MoreThanOneElement()
		{
			return new InvalidOperationException("Sequence contains more than one element");
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000CC1D File Offset: 0x0000AE1D
		internal static Exception MoreThanOneMatch()
		{
			return new InvalidOperationException("Sequence contains more than one matching element");
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000CC29 File Offset: 0x0000AE29
		internal static Exception NoElements()
		{
			return new InvalidOperationException("Sequence contains no elements");
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000CC35 File Offset: 0x0000AE35
		internal static Exception NoMatch()
		{
			return new InvalidOperationException("Sequence contains no matching element");
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000CC41 File Offset: 0x0000AE41
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}
	}
}
