using System;

namespace WebSocketSharp
{
	// Token: 0x0200000F RID: 15
	public class WebSocketException : Exception
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00008E88 File Offset: 0x00007088
		internal WebSocketException() : this(CloseStatusCode.Abnormal, null, null)
		{
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00008E99 File Offset: 0x00007099
		internal WebSocketException(Exception innerException) : this(CloseStatusCode.Abnormal, null, innerException)
		{
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00008EAA File Offset: 0x000070AA
		internal WebSocketException(string message) : this(CloseStatusCode.Abnormal, message, null)
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00008EBB File Offset: 0x000070BB
		internal WebSocketException(CloseStatusCode code) : this(code, null, null)
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00008EC8 File Offset: 0x000070C8
		internal WebSocketException(string message, Exception innerException) : this(CloseStatusCode.Abnormal, message, innerException)
		{
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00008ED9 File Offset: 0x000070D9
		internal WebSocketException(CloseStatusCode code, Exception innerException) : this(code, null, innerException)
		{
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00008EE6 File Offset: 0x000070E6
		internal WebSocketException(CloseStatusCode code, string message) : this(code, message, null)
		{
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00008EF3 File Offset: 0x000070F3
		internal WebSocketException(CloseStatusCode code, string message, Exception innerException) : base(message ?? code.GetMessage(), innerException)
		{
			this._code = code;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00008F10 File Offset: 0x00007110
		public CloseStatusCode Code
		{
			get
			{
				return this._code;
			}
		}

		// Token: 0x0400006B RID: 107
		private CloseStatusCode _code;
	}
}
