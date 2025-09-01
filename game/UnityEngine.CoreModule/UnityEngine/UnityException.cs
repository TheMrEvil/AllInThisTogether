using System;
using System.Runtime.Serialization;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200021A RID: 538
	[RequiredByNativeCode]
	[Serializable]
	public class UnityException : SystemException
	{
		// Token: 0x0600176E RID: 5998 RVA: 0x00025F0D File Offset: 0x0002410D
		public UnityException() : base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00025F28 File Offset: 0x00024128
		public UnityException(string message) : base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00025F3F File Offset: 0x0002413F
		public UnityException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00025F57 File Offset: 0x00024157
		protected UnityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000807 RID: 2055
		private const int Result = -2147467261;

		// Token: 0x04000808 RID: 2056
		private string unityStackTrace;
	}
}
