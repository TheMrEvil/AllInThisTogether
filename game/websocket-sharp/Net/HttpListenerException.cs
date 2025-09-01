using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace WebSocketSharp.Net
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	public class HttpListenerException : Win32Exception
	{
		// Token: 0x06000275 RID: 629 RVA: 0x00010092 File Offset: 0x0000E292
		protected HttpListenerException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0001009E File Offset: 0x0000E29E
		public HttpListenerException()
		{
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000100A8 File Offset: 0x0000E2A8
		public HttpListenerException(int errorCode) : base(errorCode)
		{
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000100B3 File Offset: 0x0000E2B3
		public HttpListenerException(int errorCode, string message) : base(errorCode, message)
		{
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000279 RID: 633 RVA: 0x000100C0 File Offset: 0x0000E2C0
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}
	}
}
