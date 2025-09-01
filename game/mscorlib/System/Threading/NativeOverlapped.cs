using System;

namespace System.Threading
{
	/// <summary>Provides an explicit layout that is visible from unmanaged code and that will have the same layout as the Win32 OVERLAPPED structure with additional reserved fields at the end.</summary>
	// Token: 0x020002A3 RID: 675
	public struct NativeOverlapped
	{
		/// <summary>Specifies a system-dependent status. Reserved for operating system use.</summary>
		// Token: 0x04001A68 RID: 6760
		public IntPtr InternalLow;

		/// <summary>Specifies the length of the data transferred. Reserved for operating system use.</summary>
		// Token: 0x04001A69 RID: 6761
		public IntPtr InternalHigh;

		/// <summary>Specifies a file position at which to start the transfer.</summary>
		// Token: 0x04001A6A RID: 6762
		public int OffsetLow;

		/// <summary>Specifies the high word of the byte offset at which to start the transfer.</summary>
		// Token: 0x04001A6B RID: 6763
		public int OffsetHigh;

		/// <summary>Specifies the handle to an event set to the signaled state when the operation is complete. The calling process must set this member either to zero or to a valid event handle before calling any overlapped functions.</summary>
		// Token: 0x04001A6C RID: 6764
		public IntPtr EventHandle;
	}
}
