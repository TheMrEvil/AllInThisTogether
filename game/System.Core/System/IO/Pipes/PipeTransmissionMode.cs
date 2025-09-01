using System;

namespace System.IO.Pipes
{
	/// <summary>Specifies the transmission mode of the pipe.</summary>
	// Token: 0x02000351 RID: 849
	public enum PipeTransmissionMode
	{
		/// <summary>Indicates that data in the pipe is transmitted and read as a stream of bytes.</summary>
		// Token: 0x04000C68 RID: 3176
		Byte,
		/// <summary>Indicates that data in the pipe is transmitted and read as a stream of messages.</summary>
		// Token: 0x04000C69 RID: 3177
		Message
	}
}
