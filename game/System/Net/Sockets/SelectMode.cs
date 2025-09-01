using System;

namespace System.Net.Sockets
{
	/// <summary>Defines the polling modes for the <see cref="M:System.Net.Sockets.Socket.Poll(System.Int32,System.Net.Sockets.SelectMode)" /> method.</summary>
	// Token: 0x020007AE RID: 1966
	public enum SelectMode
	{
		/// <summary>Read status mode.</summary>
		// Token: 0x0400251D RID: 9501
		SelectRead,
		/// <summary>Write status mode.</summary>
		// Token: 0x0400251E RID: 9502
		SelectWrite,
		/// <summary>Error status mode.</summary>
		// Token: 0x0400251F RID: 9503
		SelectError
	}
}
