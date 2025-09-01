using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>The exception that is thrown when an error occurs processing an HTTP request.</summary>
	// Token: 0x020005CE RID: 1486
	[Serializable]
	public class HttpListenerException : Win32Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class.</summary>
		// Token: 0x0600300E RID: 12302 RVA: 0x0007B700 File Offset: 0x00079900
		public HttpListenerException() : base(Marshal.GetLastWin32Error())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class using the specified error code.</summary>
		/// <param name="errorCode">A <see cref="T:System.Int32" /> value that identifies the error that occurred.</param>
		// Token: 0x0600300F RID: 12303 RVA: 0x000A5C91 File Offset: 0x000A3E91
		public HttpListenerException(int errorCode) : base(errorCode)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class using the specified error code and message.</summary>
		/// <param name="errorCode">A <see cref="T:System.Int32" /> value that identifies the error that occurred.</param>
		/// <param name="message">A <see cref="T:System.String" /> that describes the error that occurred.</param>
		// Token: 0x06003010 RID: 12304 RVA: 0x000A5C9A File Offset: 0x000A3E9A
		public HttpListenerException(int errorCode, string message) : base(errorCode, message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains the information required to deserialize the new <see cref="T:System.Net.HttpListenerException" /> object.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> object.</param>
		// Token: 0x06003011 RID: 12305 RVA: 0x000A5CA4 File Offset: 0x000A3EA4
		protected HttpListenerException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets a value that identifies the error that occurred.</summary>
		/// <returns>A <see cref="T:System.Int32" /> value.</returns>
		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06003012 RID: 12306 RVA: 0x000A5CAE File Offset: 0x000A3EAE
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}
	}
}
