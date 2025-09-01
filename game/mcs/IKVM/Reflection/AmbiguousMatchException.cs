using System;
using System.Runtime.Serialization;

namespace IKVM.Reflection
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	public sealed class AmbiguousMatchException : Exception
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public AmbiguousMatchException()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public AmbiguousMatchException(string message) : base(message)
		{
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		public AmbiguousMatchException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000206B File Offset: 0x0000026B
		private AmbiguousMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
