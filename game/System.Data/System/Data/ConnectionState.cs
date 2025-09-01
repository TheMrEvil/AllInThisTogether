using System;

namespace System.Data
{
	/// <summary>Describes the current state of the connection to a data source.</summary>
	// Token: 0x020000A8 RID: 168
	[Flags]
	public enum ConnectionState
	{
		/// <summary>The connection is closed.</summary>
		// Token: 0x04000777 RID: 1911
		Closed = 0,
		/// <summary>The connection is open.</summary>
		// Token: 0x04000778 RID: 1912
		Open = 1,
		/// <summary>The connection object is connecting to the data source.</summary>
		// Token: 0x04000779 RID: 1913
		Connecting = 2,
		/// <summary>The connection object is executing a command. (This value is reserved for future versions of the product.)</summary>
		// Token: 0x0400077A RID: 1914
		Executing = 4,
		/// <summary>The connection object is retrieving data. (This value is reserved for future versions of the product.)</summary>
		// Token: 0x0400077B RID: 1915
		Fetching = 8,
		/// <summary>The connection to the data source is broken. This can occur only after the connection has been opened. A connection in this state may be closed and then re-opened. (This value is reserved for future versions of the product.)</summary>
		// Token: 0x0400077C RID: 1916
		Broken = 16
	}
}
