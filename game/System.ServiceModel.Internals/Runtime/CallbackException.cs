using System;
using System.Runtime.Serialization;

namespace System.Runtime
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	internal class CallbackException : FatalException
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00003272 File Offset: 0x00001472
		public CallbackException()
		{
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000327A File Offset: 0x0000147A
		public CallbackException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003284 File Offset: 0x00001484
		protected CallbackException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
