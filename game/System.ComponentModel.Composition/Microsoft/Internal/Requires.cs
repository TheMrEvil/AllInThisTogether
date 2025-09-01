using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.Internal
{
	// Token: 0x02000011 RID: 17
	internal static class Requires
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00002DBB File Offset: 0x00000FBB
		[DebuggerStepThrough]
		public static void NotNull<T>(T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002DCC File Offset: 0x00000FCC
		[DebuggerStepThrough]
		public static void NotNullOrEmpty(string value, string parameterName)
		{
			Requires.NotNull<string>(value, parameterName);
			if (value.Length == 0)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ArgumentException_EmptyString, parameterName), parameterName);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002DF4 File Offset: 0x00000FF4
		[DebuggerStepThrough]
		public static void NotNullOrNullElements<T>(IEnumerable<T> values, string parameterName) where T : class
		{
			Requires.NotNull<IEnumerable<T>>(values, parameterName);
			Requires.NotNullElements<T>(values, parameterName);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002E04 File Offset: 0x00001004
		[DebuggerStepThrough]
		public static void NullOrNotNullElements<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> values, string parameterName) where TKey : class where TValue : class
		{
			Requires.NotNullElements<TKey, TValue>(values, parameterName);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002E0D File Offset: 0x0000100D
		[DebuggerStepThrough]
		public static void NullOrNotNullElements<T>(IEnumerable<T> values, string parameterName) where T : class
		{
			Requires.NotNullElements<T>(values, parameterName);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002E16 File Offset: 0x00001016
		private static void NotNullElements<T>(IEnumerable<T> values, string parameterName) where T : class
		{
			if (values != null)
			{
				if (!Contract.ForAll<T>(values, (T value) => value != null))
				{
					throw ExceptionBuilder.CreateContainsNullElement(parameterName);
				}
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002E49 File Offset: 0x00001049
		private static void NotNullElements<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> values, string parameterName) where TKey : class where TValue : class
		{
			if (values != null)
			{
				if (!Contract.ForAll<KeyValuePair<TKey, TValue>>(values, (KeyValuePair<TKey, TValue> keyValue) => keyValue.Key != null && keyValue.Value != null))
				{
					throw ExceptionBuilder.CreateContainsNullElement(parameterName);
				}
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E7C File Offset: 0x0000107C
		[DebuggerStepThrough]
		public static void IsInMembertypeSet(MemberTypes value, string parameterName, MemberTypes enumFlagSet)
		{
			if ((value & enumFlagSet) != value || (value & value - 1) != (MemberTypes)0)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ArgumentOutOfRange_InvalidEnumInSet, parameterName, value, enumFlagSet.ToString()), parameterName);
			}
		}

		// Token: 0x02000012 RID: 18
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__5<T> where T : class
		{
			// Token: 0x0600005C RID: 92 RVA: 0x00002EB5 File Offset: 0x000010B5
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__5()
			{
			}

			// Token: 0x0600005D RID: 93 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__5()
			{
			}

			// Token: 0x0600005E RID: 94 RVA: 0x00002EC1 File Offset: 0x000010C1
			internal bool <NotNullElements>b__5_0(T value)
			{
				return value != null;
			}

			// Token: 0x04000054 RID: 84
			public static readonly Requires.<>c__5<T> <>9 = new Requires.<>c__5<T>();

			// Token: 0x04000055 RID: 85
			public static Predicate<T> <>9__5_0;
		}

		// Token: 0x02000013 RID: 19
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c__6<TKey, TValue> where TKey : class where TValue : class
		{
			// Token: 0x0600005F RID: 95 RVA: 0x00002ECC File Offset: 0x000010CC
			// Note: this type is marked as 'beforefieldinit'.
			static <>c__6()
			{
			}

			// Token: 0x06000060 RID: 96 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__6()
			{
			}

			// Token: 0x06000061 RID: 97 RVA: 0x00002ED8 File Offset: 0x000010D8
			internal bool <NotNullElements>b__6_0(KeyValuePair<TKey, TValue> keyValue)
			{
				return keyValue.Key != null && keyValue.Value != null;
			}

			// Token: 0x04000056 RID: 86
			public static readonly Requires.<>c__6<TKey, TValue> <>9 = new Requires.<>c__6<TKey, TValue>();

			// Token: 0x04000057 RID: 87
			public static Predicate<KeyValuePair<TKey, TValue>> <>9__6_0;
		}
	}
}
