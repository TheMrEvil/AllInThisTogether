using System;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x020009B0 RID: 2480
	internal static class Assert
	{
		// Token: 0x0600598D RID: 22925 RVA: 0x00132EB2 File Offset: 0x001310B2
		static Assert()
		{
		}

		// Token: 0x0600598E RID: 22926 RVA: 0x00132EBE File Offset: 0x001310BE
		internal static void Check(bool condition, string conditionString, string message)
		{
			if (!condition)
			{
				Assert.Fail(conditionString, message, null, -2146232797);
			}
		}

		// Token: 0x0600598F RID: 22927 RVA: 0x00132ED0 File Offset: 0x001310D0
		internal static void Check(bool condition, string conditionString, string message, int exitCode)
		{
			if (!condition)
			{
				Assert.Fail(conditionString, message, null, exitCode);
			}
		}

		// Token: 0x06005990 RID: 22928 RVA: 0x00132EDE File Offset: 0x001310DE
		internal static void Fail(string conditionString, string message)
		{
			Assert.Fail(conditionString, message, null, -2146232797);
		}

		// Token: 0x06005991 RID: 22929 RVA: 0x00132EED File Offset: 0x001310ED
		internal static void Fail(string conditionString, string message, string windowTitle, int exitCode)
		{
			Assert.Fail(conditionString, message, windowTitle, exitCode, StackTrace.TraceFormat.Normal, 0);
		}

		// Token: 0x06005992 RID: 22930 RVA: 0x00132EFA File Offset: 0x001310FA
		internal static void Fail(string conditionString, string message, int exitCode, StackTrace.TraceFormat stackTraceFormat)
		{
			Assert.Fail(conditionString, message, null, exitCode, stackTraceFormat, 0);
		}

		// Token: 0x06005993 RID: 22931 RVA: 0x00132F08 File Offset: 0x00131108
		[SecuritySafeCritical]
		internal static void Fail(string conditionString, string message, string windowTitle, int exitCode, StackTrace.TraceFormat stackTraceFormat, int numStackFramesToSkip)
		{
			StackTrace location = new StackTrace(numStackFramesToSkip, true);
			AssertFilters assertFilters = Assert.Filter.AssertFailure(conditionString, message, location, stackTraceFormat, windowTitle);
			if (assertFilters == AssertFilters.FailDebug)
			{
				if (Debugger.IsAttached)
				{
					Debugger.Break();
					return;
				}
				if (!Debugger.Launch())
				{
					throw new InvalidOperationException(Environment.GetResourceString("Debugger unable to launch."));
				}
			}
			else if (assertFilters == AssertFilters.FailTerminate)
			{
				if (Debugger.IsAttached)
				{
					Environment._Exit(exitCode);
					return;
				}
				Environment.FailFast(message, (uint)exitCode);
			}
		}

		// Token: 0x06005994 RID: 22932 RVA: 0x000479F8 File Offset: 0x00045BF8
		internal static int ShowDefaultAssertDialog(string conditionString, string message, string stackTrace, string windowTitle)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400376D RID: 14189
		internal const int COR_E_FAILFAST = -2146232797;

		// Token: 0x0400376E RID: 14190
		private static AssertFilter Filter = new DefaultFilter();
	}
}
