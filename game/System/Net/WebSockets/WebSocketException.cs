using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.WebSockets
{
	/// <summary>Represents an exception that occurred when performing an operation on a WebSocket connection.</summary>
	// Token: 0x020007F1 RID: 2033
	[Serializable]
	public sealed class WebSocketException : Win32Exception
	{
		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		// Token: 0x060040C5 RID: 16581 RVA: 0x000DEFD6 File Offset: 0x000DD1D6
		public WebSocketException() : this(Marshal.GetLastWin32Error())
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		// Token: 0x060040C6 RID: 16582 RVA: 0x000DEFE3 File Offset: 0x000DD1E3
		public WebSocketException(WebSocketError error) : this(error, WebSocketException.GetErrorMessage(error))
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="message">The description of the error.</param>
		// Token: 0x060040C7 RID: 16583 RVA: 0x000DEFF2 File Offset: 0x000DD1F2
		public WebSocketException(WebSocketError error, string message) : base(message)
		{
			this._webSocketErrorCode = error;
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x060040C8 RID: 16584 RVA: 0x000DF002 File Offset: 0x000DD202
		public WebSocketException(WebSocketError error, Exception innerException) : this(error, WebSocketException.GetErrorMessage(error), innerException)
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="message">The description of the error.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x060040C9 RID: 16585 RVA: 0x000DF012 File Offset: 0x000DD212
		public WebSocketException(WebSocketError error, string message, Exception innerException) : base(message, innerException)
		{
			this._webSocketErrorCode = error;
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="nativeError">The native error code for the exception.</param>
		// Token: 0x060040CA RID: 16586 RVA: 0x000DF023 File Offset: 0x000DD223
		public WebSocketException(int nativeError) : base(nativeError)
		{
			this._webSocketErrorCode = ((!WebSocketException.Succeeded(nativeError)) ? WebSocketError.NativeError : WebSocketError.Success);
			this.SetErrorCodeOnError(nativeError);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="nativeError">The native error code for the exception.</param>
		/// <param name="message">The description of the error.</param>
		// Token: 0x060040CB RID: 16587 RVA: 0x000DF045 File Offset: 0x000DD245
		public WebSocketException(int nativeError, string message) : base(nativeError, message)
		{
			this._webSocketErrorCode = ((!WebSocketException.Succeeded(nativeError)) ? WebSocketError.NativeError : WebSocketError.Success);
			this.SetErrorCodeOnError(nativeError);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="nativeError">The native error code for the exception.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x060040CC RID: 16588 RVA: 0x000DF068 File Offset: 0x000DD268
		public WebSocketException(int nativeError, Exception innerException) : base("An internal WebSocket error occurred. Please see the innerException, if present, for more details.", innerException)
		{
			this._webSocketErrorCode = ((!WebSocketException.Succeeded(nativeError)) ? WebSocketError.NativeError : WebSocketError.Success);
			this.SetErrorCodeOnError(nativeError);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="nativeError">The native error code for the exception.</param>
		// Token: 0x060040CD RID: 16589 RVA: 0x000DF08F File Offset: 0x000DD28F
		public WebSocketException(WebSocketError error, int nativeError) : this(error, nativeError, WebSocketException.GetErrorMessage(error))
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="nativeError">The native error code for the exception.</param>
		/// <param name="message">The description of the error.</param>
		// Token: 0x060040CE RID: 16590 RVA: 0x000DF09F File Offset: 0x000DD29F
		public WebSocketException(WebSocketError error, int nativeError, string message) : base(message)
		{
			this._webSocketErrorCode = error;
			this.SetErrorCodeOnError(nativeError);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="nativeError">The native error code for the exception.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x060040CF RID: 16591 RVA: 0x000DF0B6 File Offset: 0x000DD2B6
		public WebSocketException(WebSocketError error, int nativeError, Exception innerException) : this(error, nativeError, WebSocketException.GetErrorMessage(error), innerException)
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="error">The error from the WebSocketError enumeration.</param>
		/// <param name="nativeError">The native error code for the exception.</param>
		/// <param name="message">The description of the error.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x060040D0 RID: 16592 RVA: 0x000DF0C7 File Offset: 0x000DD2C7
		public WebSocketException(WebSocketError error, int nativeError, string message, Exception innerException) : base(message, innerException)
		{
			this._webSocketErrorCode = error;
			this.SetErrorCodeOnError(nativeError);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="message">The description of the error.</param>
		// Token: 0x060040D1 RID: 16593 RVA: 0x000DF0E0 File Offset: 0x000DD2E0
		public WebSocketException(string message) : base(message)
		{
		}

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketException" /> class.</summary>
		/// <param name="message">The description of the error.</param>
		/// <param name="innerException">Indicates the previous exception that led to the current exception.</param>
		// Token: 0x060040D2 RID: 16594 RVA: 0x000DF0E9 File Offset: 0x000DD2E9
		public WebSocketException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x000A5CA4 File Offset: 0x000A3EA4
		private WebSocketException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Sets the SerializationInfo object with the file name and line number where the exception occurred.</summary>
		/// <param name="info">A SerializationInfo object.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		// Token: 0x060040D4 RID: 16596 RVA: 0x000DF0F3 File Offset: 0x000DD2F3
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("WebSocketErrorCode", this._webSocketErrorCode);
		}

		/// <summary>The native error code for the exception that occurred.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.</returns>
		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x060040D5 RID: 16597 RVA: 0x000A5CAE File Offset: 0x000A3EAE
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}

		/// <summary>Returns a WebSocketError indicating the type of error that occurred.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocketError" />.</returns>
		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x060040D6 RID: 16598 RVA: 0x000DF113 File Offset: 0x000DD313
		public WebSocketError WebSocketErrorCode
		{
			get
			{
				return this._webSocketErrorCode;
			}
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x000DF11C File Offset: 0x000DD31C
		private static string GetErrorMessage(WebSocketError error)
		{
			switch (error)
			{
			case WebSocketError.InvalidMessageType:
				return SR.Format("The received  message type is invalid after calling {0}. {0} should only be used if no more data is expected from the remote endpoint. Use '{1}' instead to keep being able to receive data but close the output channel.", "WebSocket.CloseAsync", "WebSocket.CloseOutputAsync");
			case WebSocketError.Faulted:
				return "An exception caused the WebSocket to enter the Aborted state. Please see the InnerException, if present, for more details.";
			case WebSocketError.NotAWebSocket:
				return "A WebSocket operation was called on a request or response that is not a WebSocket.";
			case WebSocketError.UnsupportedVersion:
				return "Unsupported WebSocket version.";
			case WebSocketError.UnsupportedProtocol:
				return "The WebSocket request or response operation was called with unsupported protocol(s).";
			case WebSocketError.HeaderError:
				return "The WebSocket request or response contained unsupported header(s).";
			case WebSocketError.ConnectionClosedPrematurely:
				return "The remote party closed the WebSocket connection without completing the close handshake.";
			case WebSocketError.InvalidState:
				return "The WebSocket instance cannot be used for communication because it has been transitioned into an invalid state.";
			}
			return "An internal WebSocket error occurred. Please see the innerException, if present, for more details.";
		}

		// Token: 0x060040D8 RID: 16600 RVA: 0x000DF19B File Offset: 0x000DD39B
		private void SetErrorCodeOnError(int nativeError)
		{
			if (!WebSocketException.Succeeded(nativeError))
			{
				base.HResult = nativeError;
			}
		}

		// Token: 0x060040D9 RID: 16601 RVA: 0x000DF1AC File Offset: 0x000DD3AC
		private static bool Succeeded(int hr)
		{
			return hr >= 0;
		}

		// Token: 0x0400274B RID: 10059
		private readonly WebSocketError _webSocketErrorCode;
	}
}
