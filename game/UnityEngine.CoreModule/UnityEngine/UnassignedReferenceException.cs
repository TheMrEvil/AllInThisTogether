using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x0200021C RID: 540
	[Serializable]
	public class UnassignedReferenceException : SystemException
	{
		// Token: 0x06001776 RID: 6006 RVA: 0x00025F0D File Offset: 0x0002410D
		public UnassignedReferenceException() : base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00025F28 File Offset: 0x00024128
		public UnassignedReferenceException(string message) : base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00025F3F File Offset: 0x0002413F
		public UnassignedReferenceException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00025F57 File Offset: 0x00024157
		protected UnassignedReferenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0400080B RID: 2059
		private const int Result = -2147467261;

		// Token: 0x0400080C RID: 2060
		private string unityStackTrace;
	}
}
