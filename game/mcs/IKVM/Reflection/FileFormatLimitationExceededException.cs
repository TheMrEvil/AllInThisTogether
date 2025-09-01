using System;
using System.Runtime.Serialization;

namespace IKVM.Reflection
{
	// Token: 0x02000072 RID: 114
	[Serializable]
	public sealed class FileFormatLimitationExceededException : InvalidOperationException
	{
		// Token: 0x0600067D RID: 1661 RVA: 0x00013A0F File Offset: 0x00011C0F
		public FileFormatLimitationExceededException(string message, int hresult) : base(message)
		{
			base.HResult = hresult;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00009B40 File Offset: 0x00007D40
		private FileFormatLimitationExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x00013A1F File Offset: 0x00011C1F
		public int ErrorCode
		{
			get
			{
				return base.HResult;
			}
		}

		// Token: 0x04000274 RID: 628
		public const int META_E_STRINGSPACE_FULL = -2146233960;
	}
}
