using System;
using System.Runtime.Serialization;

namespace System.Runtime
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	internal class FatalException : SystemException
	{
		// Token: 0x06000099 RID: 153 RVA: 0x000039EC File Offset: 0x00001BEC
		public FatalException()
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000039F4 File Offset: 0x00001BF4
		public FatalException(string message) : base(message)
		{
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000039FD File Offset: 0x00001BFD
		public FatalException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003A07 File Offset: 0x00001C07
		protected FatalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
