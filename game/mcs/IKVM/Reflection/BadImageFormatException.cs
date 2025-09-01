using System;
using System.Runtime.Serialization;

namespace IKVM.Reflection
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public sealed class BadImageFormatException : Exception
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00002050 File Offset: 0x00000250
		public BadImageFormatException()
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002058 File Offset: 0x00000258
		public BadImageFormatException(string message) : base(message)
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002061 File Offset: 0x00000261
		public BadImageFormatException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000206B File Offset: 0x0000026B
		private BadImageFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
