using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x0200021A RID: 538
	[FriendAccessAllowed]
	internal class CLRConfig
	{
		// Token: 0x0600181A RID: 6170 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[FriendAccessAllowed]
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		internal static bool CheckLegacyManagedDeflateStream()
		{
			return false;
		}

		// Token: 0x0600181B RID: 6171
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool CheckThrowUnobservedTaskExceptions();

		// Token: 0x0600181C RID: 6172 RVA: 0x0000259F File Offset: 0x0000079F
		public CLRConfig()
		{
		}
	}
}
