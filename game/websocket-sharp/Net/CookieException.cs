using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace WebSocketSharp.Net
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public class CookieException : FormatException, ISerializable
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x0000D412 File Offset: 0x0000B612
		internal CookieException(string message) : base(message)
		{
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000D41D File Offset: 0x0000B61D
		internal CookieException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000D429 File Offset: 0x0000B629
		protected CookieException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000D435 File Offset: 0x0000B635
		public CookieException()
		{
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000D43F File Offset: 0x0000B63F
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000D43F File Offset: 0x0000B63F
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}
	}
}
