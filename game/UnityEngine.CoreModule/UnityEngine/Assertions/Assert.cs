using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine.Assertions.Comparers;

namespace UnityEngine.Assertions
{
	// Token: 0x02000486 RID: 1158
	[DebuggerStepThrough]
	public static class Assert
	{
		// Token: 0x060028D4 RID: 10452 RVA: 0x00043634 File Offset: 0x00041834
		private static void Fail(string message, string userMessage)
		{
			bool flag = !Assert.raiseExceptions;
			if (flag)
			{
				bool flag2 = message == null;
				if (flag2)
				{
					message = "Assertion has failed\n";
				}
				bool flag3 = userMessage != null;
				if (flag3)
				{
					message = userMessage + "\n" + message;
				}
				Debug.LogAssertion(message);
				return;
			}
			throw new AssertionException(message, userMessage);
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x00043685 File Offset: 0x00041885
		[Obsolete("Assert.Equals should not be used for Assertions", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new static bool Equals(object obj1, object obj2)
		{
			throw new InvalidOperationException("Assert.Equals should not be used for Assertions");
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x00043692 File Offset: 0x00041892
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Assert.ReferenceEquals should not be used for Assertions", true)]
		public new static bool ReferenceEquals(object obj1, object obj2)
		{
			throw new InvalidOperationException("Assert.ReferenceEquals should not be used for Assertions");
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000436A0 File Offset: 0x000418A0
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsTrue(bool condition)
		{
			bool flag = !condition;
			if (flag)
			{
				Assert.IsTrue(condition, null);
			}
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000436C0 File Offset: 0x000418C0
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsTrue(bool condition, string message)
		{
			bool flag = !condition;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.BooleanFailureMessage(true), message);
			}
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000436E4 File Offset: 0x000418E4
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsFalse(bool condition)
		{
			if (condition)
			{
				Assert.IsFalse(condition, null);
			}
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x00043700 File Offset: 0x00041900
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsFalse(bool condition, string message)
		{
			if (condition)
			{
				Assert.Fail(AssertionMessageUtil.BooleanFailureMessage(false), message);
			}
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x00043720 File Offset: 0x00041920
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual)
		{
			Assert.AreEqual<float>(expected, actual, null, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x00043731 File Offset: 0x00041931
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual, string message)
		{
			Assert.AreEqual<float>(expected, actual, message, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x00043742 File Offset: 0x00041942
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual, float tolerance)
		{
			Assert.AreApproximatelyEqual(expected, actual, tolerance, null);
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x0004374F File Offset: 0x0004194F
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreApproximatelyEqual(float expected, float actual, float tolerance, string message)
		{
			Assert.AreEqual<float>(expected, actual, message, new FloatComparer(tolerance));
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x00043761 File Offset: 0x00041961
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual)
		{
			Assert.AreNotEqual<float>(expected, actual, null, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x00043772 File Offset: 0x00041972
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual, string message)
		{
			Assert.AreNotEqual<float>(expected, actual, message, FloatComparer.s_ComparerWithDefaultTolerance);
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x00043783 File Offset: 0x00041983
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual, float tolerance)
		{
			Assert.AreNotApproximatelyEqual(expected, actual, tolerance, null);
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x00043790 File Offset: 0x00041990
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotApproximatelyEqual(float expected, float actual, float tolerance, string message)
		{
			Assert.AreNotEqual<float>(expected, actual, message, new FloatComparer(tolerance));
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x000437A2 File Offset: 0x000419A2
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual)
		{
			Assert.AreEqual<T>(expected, actual, null);
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x000437AE File Offset: 0x000419AE
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual, string message)
		{
			Assert.AreEqual<T>(expected, actual, message, EqualityComparer<T>.Default);
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x000437C0 File Offset: 0x000419C0
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual, string message, IEqualityComparer<T> comparer)
		{
			bool flag = typeof(Object).IsAssignableFrom(typeof(T));
			if (flag)
			{
				Assert.AreEqual(expected as Object, actual as Object, message);
			}
			else
			{
				bool flag2 = !comparer.Equals(actual, expected);
				if (flag2)
				{
					Assert.Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, true), message);
				}
			}
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x00043834 File Offset: 0x00041A34
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(Object expected, Object actual, string message)
		{
			bool flag = actual != expected;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, true), message);
			}
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0004385C File Offset: 0x00041A5C
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual<T>(T expected, T actual)
		{
			Assert.AreNotEqual<T>(expected, actual, null);
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x00043868 File Offset: 0x00041A68
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual<T>(T expected, T actual, string message)
		{
			Assert.AreNotEqual<T>(expected, actual, message, EqualityComparer<T>.Default);
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x0004387C File Offset: 0x00041A7C
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual<T>(T expected, T actual, string message, IEqualityComparer<T> comparer)
		{
			bool flag = typeof(Object).IsAssignableFrom(typeof(T));
			if (flag)
			{
				Assert.AreNotEqual(expected as Object, actual as Object, message);
			}
			else
			{
				bool flag2 = comparer.Equals(actual, expected);
				if (flag2)
				{
					Assert.Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, false), message);
				}
			}
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x000438EC File Offset: 0x00041AEC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(Object expected, Object actual, string message)
		{
			bool flag = actual == expected;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, false), message);
			}
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x00043914 File Offset: 0x00041B14
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNull<T>(T value) where T : class
		{
			Assert.IsNull<T>(value, null);
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x00043920 File Offset: 0x00041B20
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNull<T>(T value, string message) where T : class
		{
			bool flag = typeof(Object).IsAssignableFrom(typeof(T));
			if (flag)
			{
				Assert.IsNull(value as Object, message);
			}
			else
			{
				bool flag2 = value != null;
				if (flag2)
				{
					Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, true), message);
				}
			}
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x00043984 File Offset: 0x00041B84
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNull(Object value, string message)
		{
			bool flag = value != null;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, true), message);
			}
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x000439AB File Offset: 0x00041BAB
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNotNull<T>(T value) where T : class
		{
			Assert.IsNotNull<T>(value, null);
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x000439B8 File Offset: 0x00041BB8
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNotNull<T>(T value, string message) where T : class
		{
			bool flag = typeof(Object).IsAssignableFrom(typeof(T));
			if (flag)
			{
				Assert.IsNotNull(value as Object, message);
			}
			else
			{
				bool flag2 = value == null;
				if (flag2)
				{
					Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, false), message);
				}
			}
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x00043A1C File Offset: 0x00041C1C
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNotNull(Object value, string message)
		{
			bool flag = value == null;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, false), message);
			}
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x00043A44 File Offset: 0x00041C44
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(sbyte expected, sbyte actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<sbyte>(expected, actual, null);
			}
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x00043A68 File Offset: 0x00041C68
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(sbyte expected, sbyte actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<sbyte>(expected, actual, message);
			}
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x00043A8C File Offset: 0x00041C8C
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(sbyte expected, sbyte actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<sbyte>(expected, actual, null);
			}
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x00043AAC File Offset: 0x00041CAC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(sbyte expected, sbyte actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<sbyte>(expected, actual, message);
			}
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x00043ACC File Offset: 0x00041CCC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(byte expected, byte actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<byte>(expected, actual, null);
			}
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x00043AF0 File Offset: 0x00041CF0
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(byte expected, byte actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<byte>(expected, actual, message);
			}
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x00043B14 File Offset: 0x00041D14
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(byte expected, byte actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<byte>(expected, actual, null);
			}
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x00043B34 File Offset: 0x00041D34
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(byte expected, byte actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<byte>(expected, actual, message);
			}
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x00043B54 File Offset: 0x00041D54
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(char expected, char actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<char>(expected, actual, null);
			}
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x00043B78 File Offset: 0x00041D78
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(char expected, char actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<char>(expected, actual, message);
			}
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x00043B9C File Offset: 0x00041D9C
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(char expected, char actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<char>(expected, actual, null);
			}
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x00043BBC File Offset: 0x00041DBC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(char expected, char actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<char>(expected, actual, message);
			}
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x00043BDC File Offset: 0x00041DDC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(short expected, short actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<short>(expected, actual, null);
			}
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x00043C00 File Offset: 0x00041E00
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(short expected, short actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<short>(expected, actual, message);
			}
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x00043C24 File Offset: 0x00041E24
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(short expected, short actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<short>(expected, actual, null);
			}
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x00043C44 File Offset: 0x00041E44
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(short expected, short actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<short>(expected, actual, message);
			}
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x00043C64 File Offset: 0x00041E64
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(ushort expected, ushort actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<ushort>(expected, actual, null);
			}
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x00043C88 File Offset: 0x00041E88
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(ushort expected, ushort actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<ushort>(expected, actual, message);
			}
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x00043CAC File Offset: 0x00041EAC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(ushort expected, ushort actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<ushort>(expected, actual, null);
			}
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x00043CCC File Offset: 0x00041ECC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(ushort expected, ushort actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<ushort>(expected, actual, message);
			}
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x00043CEC File Offset: 0x00041EEC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(int expected, int actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<int>(expected, actual, null);
			}
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x00043D10 File Offset: 0x00041F10
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(int expected, int actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<int>(expected, actual, message);
			}
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x00043D34 File Offset: 0x00041F34
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(int expected, int actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<int>(expected, actual, null);
			}
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x00043D54 File Offset: 0x00041F54
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(int expected, int actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<int>(expected, actual, message);
			}
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x00043D74 File Offset: 0x00041F74
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(uint expected, uint actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<uint>(expected, actual, null);
			}
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x00043D98 File Offset: 0x00041F98
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(uint expected, uint actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<uint>(expected, actual, message);
			}
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x00043DBC File Offset: 0x00041FBC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(uint expected, uint actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<uint>(expected, actual, null);
			}
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x00043DDC File Offset: 0x00041FDC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(uint expected, uint actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<uint>(expected, actual, message);
			}
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x00043DFC File Offset: 0x00041FFC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(long expected, long actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<long>(expected, actual, null);
			}
		}

		// Token: 0x0600290E RID: 10510 RVA: 0x00043E20 File Offset: 0x00042020
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(long expected, long actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<long>(expected, actual, message);
			}
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x00043E44 File Offset: 0x00042044
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(long expected, long actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<long>(expected, actual, null);
			}
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x00043E64 File Offset: 0x00042064
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(long expected, long actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<long>(expected, actual, message);
			}
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x00043E84 File Offset: 0x00042084
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(ulong expected, ulong actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<ulong>(expected, actual, null);
			}
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x00043EA8 File Offset: 0x000420A8
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(ulong expected, ulong actual, string message)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<ulong>(expected, actual, message);
			}
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x00043ECC File Offset: 0x000420CC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(ulong expected, ulong actual)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<ulong>(expected, actual, null);
			}
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x00043EEC File Offset: 0x000420EC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreNotEqual(ulong expected, ulong actual, string message)
		{
			bool flag = expected == actual;
			if (flag)
			{
				Assert.AreNotEqual<ulong>(expected, actual, message);
			}
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x00043F0B File Offset: 0x0004210B
		// Note: this type is marked as 'beforefieldinit'.
		static Assert()
		{
		}

		// Token: 0x04000F9B RID: 3995
		internal const string UNITY_ASSERTIONS = "UNITY_ASSERTIONS";

		// Token: 0x04000F9C RID: 3996
		[Obsolete("Future versions of Unity are expected to always throw exceptions and not have this field.")]
		public static bool raiseExceptions = true;
	}
}
