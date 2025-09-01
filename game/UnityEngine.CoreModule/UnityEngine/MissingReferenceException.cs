using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x0200021D RID: 541
	[Serializable]
	public class MissingReferenceException : SystemException
	{
		// Token: 0x0600177A RID: 6010 RVA: 0x00025F0D File Offset: 0x0002410D
		public MissingReferenceException() : base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00025F28 File Offset: 0x00024128
		public MissingReferenceException(string message) : base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00025F3F File Offset: 0x0002413F
		public MissingReferenceException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00025F57 File Offset: 0x00024157
		protected MissingReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0400080D RID: 2061
		private const int Result = -2147467261;

		// Token: 0x0400080E RID: 2062
		private string unityStackTrace;
	}
}
