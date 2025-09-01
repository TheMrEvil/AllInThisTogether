using System;

namespace System.Security.Permissions
{
	/// <summary>Specifies categories of functionality potentially harmful to the host if invoked by a method or class.</summary>
	// Token: 0x0200042A RID: 1066
	[Flags]
	public enum HostProtectionResource
	{
		/// <summary>Exposes all host resources.</summary>
		// Token: 0x04001FD3 RID: 8147
		All = 511,
		/// <summary>Might create or destroy other processes.</summary>
		// Token: 0x04001FD4 RID: 8148
		ExternalProcessMgmt = 4,
		/// <summary>Creates or manipulates threads other than its own, which might be harmful to the host.</summary>
		// Token: 0x04001FD5 RID: 8149
		ExternalThreading = 16,
		/// <summary>Might cause a resource leak on termination, if not protected by a safe handle or some other means of ensuring the release of resources.</summary>
		// Token: 0x04001FD6 RID: 8150
		MayLeakOnAbort = 256,
		/// <summary>Exposes no host resources.</summary>
		// Token: 0x04001FD7 RID: 8151
		None = 0,
		/// <summary>Exposes the security infrastructure.</summary>
		// Token: 0x04001FD8 RID: 8152
		SecurityInfrastructure = 64,
		/// <summary>Might exit the current process, terminating the server.</summary>
		// Token: 0x04001FD9 RID: 8153
		SelfAffectingProcessMgmt = 8,
		/// <summary>Manipulates threads in a way that only affects user code.</summary>
		// Token: 0x04001FDA RID: 8154
		SelfAffectingThreading = 32,
		/// <summary>Exposes state that might be shared between threads.</summary>
		// Token: 0x04001FDB RID: 8155
		SharedState = 2,
		/// <summary>Exposes synchronization.</summary>
		// Token: 0x04001FDC RID: 8156
		Synchronization = 1,
		/// <summary>Exposes the user interface.</summary>
		// Token: 0x04001FDD RID: 8157
		UI = 128
	}
}
