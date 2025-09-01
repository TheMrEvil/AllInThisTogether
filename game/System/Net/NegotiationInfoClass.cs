using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200054F RID: 1359
	internal class NegotiationInfoClass
	{
		// Token: 0x06002C22 RID: 11298 RVA: 0x0009632C File Offset: 0x0009452C
		internal unsafe NegotiationInfoClass(SafeHandle safeHandle, int negotiationState)
		{
			if (safeHandle.IsInvalid)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("Invalid handle:{0}", new object[]
					{
						safeHandle
					}), ".ctor");
				}
				return;
			}
			IntPtr intPtr = safeHandle.DangerousGetHandle();
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("packageInfo:{0} negotiationState:{1:x}", new object[]
				{
					intPtr,
					negotiationState
				}), ".ctor");
			}
			if (negotiationState == 0 || negotiationState == 1)
			{
				string text = null;
				IntPtr name = ((SecurityPackageInfo*)((void*)intPtr))->Name;
				if (name != IntPtr.Zero)
				{
					text = Marshal.PtrToStringUni(name);
				}
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("packageInfo:{0} negotiationState:{1:x} name:{2}", new object[]
					{
						intPtr,
						negotiationState,
						text
					}), ".ctor");
				}
				if (string.Compare(text, "Kerberos", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.AuthenticationPackage = "Kerberos";
					return;
				}
				if (string.Compare(text, "NTLM", StringComparison.OrdinalIgnoreCase) == 0)
				{
					this.AuthenticationPackage = "NTLM";
					return;
				}
				this.AuthenticationPackage = text;
			}
		}

		// Token: 0x040017C3 RID: 6083
		internal string AuthenticationPackage;

		// Token: 0x040017C4 RID: 6084
		internal const string NTLM = "NTLM";

		// Token: 0x040017C5 RID: 6085
		internal const string Kerberos = "Kerberos";

		// Token: 0x040017C6 RID: 6086
		internal const string Negotiate = "Negotiate";

		// Token: 0x040017C7 RID: 6087
		internal const string Basic = "Basic";
	}
}
