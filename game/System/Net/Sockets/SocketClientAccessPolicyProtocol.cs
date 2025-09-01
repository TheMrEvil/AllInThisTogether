using System;
using System.ComponentModel;

namespace System.Net.Sockets
{
	/// <summary>Specifies the method to download a client access policy file.</summary>
	// Token: 0x020007B1 RID: 1969
	[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public enum SocketClientAccessPolicyProtocol
	{
		/// <summary>The socket policy file is downloaded using a custom TCP protocol running on TCP port 943.</summary>
		// Token: 0x04002531 RID: 9521
		Tcp,
		/// <summary>The socket policy file is downloaded using the HTTP protocol running on TCP port 943.</summary>
		// Token: 0x04002532 RID: 9522
		Http
	}
}
