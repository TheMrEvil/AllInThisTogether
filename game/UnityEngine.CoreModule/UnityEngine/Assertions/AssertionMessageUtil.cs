using System;

namespace UnityEngine.Assertions
{
	// Token: 0x02000488 RID: 1160
	internal class AssertionMessageUtil
	{
		// Token: 0x06002918 RID: 10520 RVA: 0x00043F64 File Offset: 0x00042164
		public static string GetMessage(string failureMessage)
		{
			return UnityString.Format("{0} {1}", new object[]
			{
				"Assertion failure.",
				failureMessage
			});
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x00043F94 File Offset: 0x00042194
		public static string GetMessage(string failureMessage, string expected)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("{0}{1}{2} {3}", new object[]
			{
				failureMessage,
				Environment.NewLine,
				"Expected:",
				expected
			}));
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x00043FD4 File Offset: 0x000421D4
		public static string GetEqualityMessage(object actual, object expected, bool expectEqual)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("Values are {0}equal.", new object[]
			{
				expectEqual ? "not " : ""
			}), UnityString.Format("{0} {2} {1}", new object[]
			{
				actual,
				expected,
				expectEqual ? "==" : "!="
			}));
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x00044038 File Offset: 0x00042238
		public static string NullFailureMessage(object value, bool expectNull)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("Value was {0}Null", new object[]
			{
				expectNull ? "not " : ""
			}), UnityString.Format("Value was {0}Null", new object[]
			{
				expectNull ? "" : "not "
			}));
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x00044094 File Offset: 0x00042294
		public static string BooleanFailureMessage(bool expected)
		{
			return AssertionMessageUtil.GetMessage("Value was " + (!expected).ToString(), expected.ToString());
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x00002072 File Offset: 0x00000272
		public AssertionMessageUtil()
		{
		}

		// Token: 0x04000F9E RID: 3998
		private const string k_Expected = "Expected:";

		// Token: 0x04000F9F RID: 3999
		private const string k_AssertionFailed = "Assertion failure.";
	}
}
