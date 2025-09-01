using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net.NetworkInformation
{
	/// <summary>The exception that is thrown when an error occurs while retrieving network information.</summary>
	// Token: 0x020006F4 RID: 1780
	[Serializable]
	public class NetworkInformationException : Win32Exception
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class.</summary>
		// Token: 0x06003943 RID: 14659 RVA: 0x0007B700 File Offset: 0x00079900
		public NetworkInformationException() : base(Marshal.GetLastWin32Error())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class with the specified error code.</summary>
		/// <param name="errorCode">A <see langword="Win32" /> error code.</param>
		// Token: 0x06003944 RID: 14660 RVA: 0x000A5C91 File Offset: 0x000A3E91
		public NetworkInformationException(int errorCode) : base(errorCode)
		{
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x000A5C91 File Offset: 0x000A3E91
		internal NetworkInformationException(SocketError socketError) : base((int)socketError)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.NetworkInformationException" /> class with serialized data.</summary>
		/// <param name="serializationInfo">A SerializationInfo object that contains the serialized exception data.</param>
		/// <param name="streamingContext">A StreamingContext that contains contextual information about the serialized exception.</param>
		// Token: 0x06003946 RID: 14662 RVA: 0x000A5CA4 File Offset: 0x000A3EA4
		protected NetworkInformationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets the <see langword="Win32" /> error code for this exception.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the <see langword="Win32" /> error code.</returns>
		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06003947 RID: 14663 RVA: 0x000A5CAE File Offset: 0x000A3EAE
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}
	}
}
