using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net.Sockets
{
	/// <summary>The exception that is thrown when a socket error occurs.</summary>
	// Token: 0x020007A4 RID: 1956
	[Serializable]
	public class SocketException : Win32Exception
	{
		// Token: 0x06003E94 RID: 16020
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int WSAGetLastError_icall();

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class with the last operating system error code.</summary>
		// Token: 0x06003E95 RID: 16021 RVA: 0x000D6669 File Offset: 0x000D4869
		public SocketException() : base(SocketException.WSAGetLastError_icall())
		{
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x000A5C9A File Offset: 0x000A3E9A
		internal SocketException(int error, string message) : base(error, message)
		{
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x000D6676 File Offset: 0x000D4876
		internal SocketException(EndPoint endPoint) : base(Marshal.GetLastWin32Error())
		{
			this.m_EndPoint = endPoint;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class with the specified error code.</summary>
		/// <param name="errorCode">The error code that indicates the error that occurred.</param>
		// Token: 0x06003E98 RID: 16024 RVA: 0x000A5C91 File Offset: 0x000A3E91
		public SocketException(int errorCode) : base(errorCode)
		{
		}

		// Token: 0x06003E99 RID: 16025 RVA: 0x000D668A File Offset: 0x000D488A
		internal SocketException(int errorCode, EndPoint endPoint) : base(errorCode)
		{
			this.m_EndPoint = endPoint;
		}

		// Token: 0x06003E9A RID: 16026 RVA: 0x000A5C91 File Offset: 0x000A3E91
		internal SocketException(SocketError socketError) : base((int)socketError)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information that is required to serialize the new <see cref="T:System.Net.Sockets.SocketException" /> instance.</param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.Sockets.SocketException" /> instance.</param>
		// Token: 0x06003E9B RID: 16027 RVA: 0x000A5CA4 File Offset: 0x000A3EA4
		protected SocketException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets the error code that is associated with this exception.</summary>
		/// <returns>An integer error code that is associated with this exception.</returns>
		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06003E9C RID: 16028 RVA: 0x000A5CAE File Offset: 0x000A3EAE
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}

		/// <summary>Gets the error message that is associated with this exception.</summary>
		/// <returns>A string that contains the error message.</returns>
		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06003E9D RID: 16029 RVA: 0x000D669A File Offset: 0x000D489A
		public override string Message
		{
			get
			{
				if (this.m_EndPoint == null)
				{
					return base.Message;
				}
				return base.Message + " " + this.m_EndPoint.ToString();
			}
		}

		/// <summary>Gets the error code that is associated with this exception.</summary>
		/// <returns>An integer error code that is associated with this exception.</returns>
		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06003E9E RID: 16030 RVA: 0x000A5CAE File Offset: 0x000A3EAE
		public SocketError SocketErrorCode
		{
			get
			{
				return (SocketError)base.NativeErrorCode;
			}
		}

		// Token: 0x04002490 RID: 9360
		[NonSerialized]
		private EndPoint m_EndPoint;
	}
}
