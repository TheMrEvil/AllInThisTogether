using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200055A RID: 1370
	internal class SecurityPackageInfoClass
	{
		// Token: 0x06002C80 RID: 11392 RVA: 0x000978B8 File Offset: 0x00095AB8
		internal unsafe SecurityPackageInfoClass(SafeHandle safeHandle, int index)
		{
			if (safeHandle.IsInvalid)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("Invalid handle: {0}", new object[]
					{
						safeHandle
					}), ".ctor");
				}
				return;
			}
			IntPtr intPtr = safeHandle.DangerousGetHandle() + sizeof(SecurityPackageInfo) * index;
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("unmanagedAddress: {0}", new object[]
				{
					intPtr
				}), ".ctor");
			}
			SecurityPackageInfo* ptr = (SecurityPackageInfo*)((void*)intPtr);
			this.Capabilities = ptr->Capabilities;
			this.Version = ptr->Version;
			this.RPCID = ptr->RPCID;
			this.MaxToken = ptr->MaxToken;
			IntPtr intPtr2 = ptr->Name;
			if (intPtr2 != IntPtr.Zero)
			{
				this.Name = Marshal.PtrToStringUni(intPtr2);
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("Name: {0}", new object[]
					{
						this.Name
					}), ".ctor");
				}
			}
			intPtr2 = ptr->Comment;
			if (intPtr2 != IntPtr.Zero)
			{
				this.Comment = Marshal.PtrToStringUni(intPtr2);
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("Comment: {0}", new object[]
					{
						this.Comment
					}), ".ctor");
				}
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, this.ToString(), ".ctor");
			}
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x00097A20 File Offset: 0x00095C20
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Capabilities:",
				string.Format(CultureInfo.InvariantCulture, "0x{0:x}", this.Capabilities),
				" Version:",
				this.Version.ToString(NumberFormatInfo.InvariantInfo),
				" RPCID:",
				this.RPCID.ToString(NumberFormatInfo.InvariantInfo),
				" MaxToken:",
				this.MaxToken.ToString(NumberFormatInfo.InvariantInfo),
				" Name:",
				(this.Name == null) ? "(null)" : this.Name,
				" Comment:",
				(this.Comment == null) ? "(null)" : this.Comment
			});
		}

		// Token: 0x040017E4 RID: 6116
		internal int Capabilities;

		// Token: 0x040017E5 RID: 6117
		internal short Version;

		// Token: 0x040017E6 RID: 6118
		internal short RPCID;

		// Token: 0x040017E7 RID: 6119
		internal int MaxToken;

		// Token: 0x040017E8 RID: 6120
		internal string Name;

		// Token: 0x040017E9 RID: 6121
		internal string Comment;
	}
}
