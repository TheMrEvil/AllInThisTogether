using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x0200021B RID: 539
	[Serializable]
	public class MissingComponentException : SystemException
	{
		// Token: 0x06001772 RID: 6002 RVA: 0x00025F0D File Offset: 0x0002410D
		public MissingComponentException() : base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x00025F28 File Offset: 0x00024128
		public MissingComponentException(string message) : base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x00025F3F File Offset: 0x0002413F
		public MissingComponentException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00025F57 File Offset: 0x00024157
		protected MissingComponentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000809 RID: 2057
		private const int Result = -2147467261;

		// Token: 0x0400080A RID: 2058
		private string unityStackTrace;
	}
}
