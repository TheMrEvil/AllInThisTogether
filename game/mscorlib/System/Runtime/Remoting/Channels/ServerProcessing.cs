using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Indicates the status of the server message processing.</summary>
	// Token: 0x020005C8 RID: 1480
	[ComVisible(true)]
	[Serializable]
	public enum ServerProcessing
	{
		/// <summary>The server synchronously processed the message.</summary>
		// Token: 0x040025EB RID: 9707
		Complete,
		/// <summary>The message was dispatched and no response can be sent.</summary>
		// Token: 0x040025EC RID: 9708
		OneWay,
		/// <summary>The call was dispatched asynchronously, which indicates that the sink must store response data on the stack for later processing.</summary>
		// Token: 0x040025ED RID: 9709
		Async
	}
}
