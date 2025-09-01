using System;
using System.Runtime.Serialization;

namespace System.Net
{
	// Token: 0x020005DC RID: 1500
	internal class InternalException : SystemException
	{
		// Token: 0x06003039 RID: 12345 RVA: 0x000A648F File Offset: 0x000A468F
		internal InternalException()
		{
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x0005573F File Offset: 0x0005393F
		internal InternalException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}
	}
}
