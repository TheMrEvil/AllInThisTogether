using System;
using System.Diagnostics;

namespace System.Runtime.Serialization
{
	// Token: 0x02000159 RID: 345
	internal static class DiagnosticUtility
	{
		// Token: 0x06001255 RID: 4693 RVA: 0x000470FD File Offset: 0x000452FD
		// Note: this type is marked as 'beforefieldinit'.
		static DiagnosticUtility()
		{
		}

		// Token: 0x04000745 RID: 1861
		internal static bool ShouldTraceError = true;

		// Token: 0x04000746 RID: 1862
		internal static readonly bool ShouldTraceWarning = false;

		// Token: 0x04000747 RID: 1863
		internal static readonly bool ShouldTraceInformation = false;

		// Token: 0x04000748 RID: 1864
		internal static bool ShouldTraceVerbose = true;

		// Token: 0x0200015A RID: 346
		internal static class DiagnosticTrace
		{
			// Token: 0x06001256 RID: 4694 RVA: 0x0000A8EE File Offset: 0x00008AEE
			internal static void TraceEvent(params object[] args)
			{
			}
		}

		// Token: 0x0200015B RID: 347
		internal static class ExceptionUtility
		{
			// Token: 0x06001257 RID: 4695 RVA: 0x00047117 File Offset: 0x00045317
			internal static Exception ThrowHelperError(Exception e)
			{
				return DiagnosticUtility.ExceptionUtility.ThrowHelper(e, TraceEventType.Error);
			}

			// Token: 0x06001258 RID: 4696 RVA: 0x00047120 File Offset: 0x00045320
			internal static Exception ThrowHelperCallback(string msg, Exception e)
			{
				return new CallbackException(msg, e);
			}

			// Token: 0x06001259 RID: 4697 RVA: 0x00047129 File Offset: 0x00045329
			internal static Exception ThrowHelperCallback(Exception e)
			{
				return new CallbackException("Callback exception", e);
			}

			// Token: 0x0600125A RID: 4698 RVA: 0x00002068 File Offset: 0x00000268
			internal static Exception ThrowHelper(Exception e, TraceEventType type)
			{
				return e;
			}

			// Token: 0x0600125B RID: 4699 RVA: 0x00047136 File Offset: 0x00045336
			internal static Exception ThrowHelperArgument(string arg)
			{
				return new ArgumentException(arg);
			}

			// Token: 0x0600125C RID: 4700 RVA: 0x0004713E File Offset: 0x0004533E
			internal static Exception ThrowHelperArgument(string arg, string message)
			{
				return new ArgumentException(message, arg);
			}

			// Token: 0x0600125D RID: 4701 RVA: 0x00047147 File Offset: 0x00045347
			internal static Exception ThrowHelperArgumentNull(string arg)
			{
				return new ArgumentNullException(arg);
			}

			// Token: 0x0600125E RID: 4702 RVA: 0x0004714F File Offset: 0x0004534F
			internal static Exception ThrowHelperFatal(string msg, Exception e)
			{
				return new FatalException(msg, e);
			}
		}
	}
}
