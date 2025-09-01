using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace System.Dynamic.Utils
{
	// Token: 0x02000326 RID: 806
	internal static class ContractUtils
	{
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001857 RID: 6231 RVA: 0x000522A0 File Offset: 0x000504A0
		[ExcludeFromCodeCoverage]
		public static Exception Unreachable
		{
			get
			{
				return new InvalidOperationException("Code supposed to be unreachable");
			}
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x000522AC File Offset: 0x000504AC
		public static void Requires(bool precondition, string paramName)
		{
			if (!precondition)
			{
				throw Error.InvalidArgumentValue(paramName);
			}
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x000522B8 File Offset: 0x000504B8
		public static void RequiresNotNull(object value, string paramName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(paramName);
			}
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x000522C4 File Offset: 0x000504C4
		public static void RequiresNotNull(object value, string paramName, int index)
		{
			if (value == null)
			{
				throw new ArgumentNullException(ContractUtils.GetParamName(paramName, index));
			}
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x000522D6 File Offset: 0x000504D6
		public static void RequiresNotEmpty<T>(ICollection<T> collection, string paramName)
		{
			ContractUtils.RequiresNotNull(collection, paramName);
			if (collection.Count == 0)
			{
				throw Error.NonEmptyCollectionRequired(paramName);
			}
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x000522F0 File Offset: 0x000504F0
		public static void RequiresNotNullItems<T>(IList<T> array, string arrayName)
		{
			ContractUtils.RequiresNotNull(array, arrayName);
			int i = 0;
			int count = array.Count;
			while (i < count)
			{
				if (array[i] == null)
				{
					throw new ArgumentNullException(ContractUtils.GetParamName(arrayName, i));
				}
				i++;
			}
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x00003A59 File Offset: 0x00001C59
		[Conditional("DEBUG")]
		public static void AssertLockHeld(object lockObject)
		{
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x00052332 File Offset: 0x00050532
		private static string GetParamName(string paramName, int index)
		{
			if (index < 0)
			{
				return paramName;
			}
			return string.Format("{0}[{1}]", paramName, index);
		}

		// Token: 0x0600185F RID: 6239 RVA: 0x0005234B File Offset: 0x0005054B
		public static void RequiresArrayRange<T>(IList<T> array, int offset, int count, string offsetName, string countName)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException(countName);
			}
			if (offset < 0 || array.Count - offset < count)
			{
				throw new ArgumentOutOfRangeException(offsetName);
			}
		}
	}
}
