using System;

namespace System.Net
{
	/// <summary>Defines status codes for the <see cref="T:System.Net.WebException" /> class.</summary>
	// Token: 0x02000607 RID: 1543
	public enum WebExceptionStatus
	{
		/// <summary>No error was encountered.</summary>
		// Token: 0x04001C49 RID: 7241
		Success,
		/// <summary>The name resolver service could not resolve the host name.</summary>
		// Token: 0x04001C4A RID: 7242
		NameResolutionFailure,
		/// <summary>The remote service point could not be contacted at the transport level.</summary>
		// Token: 0x04001C4B RID: 7243
		ConnectFailure,
		/// <summary>A complete response was not received from the remote server.</summary>
		// Token: 0x04001C4C RID: 7244
		ReceiveFailure,
		/// <summary>A complete request could not be sent to the remote server.</summary>
		// Token: 0x04001C4D RID: 7245
		SendFailure,
		/// <summary>The request was a pipelined request and the connection was closed before the response was received.</summary>
		// Token: 0x04001C4E RID: 7246
		PipelineFailure,
		/// <summary>The request was canceled, the <see cref="M:System.Net.WebRequest.Abort" /> method was called, or an unclassifiable error occurred. This is the default value for <see cref="P:System.Net.WebException.Status" />.</summary>
		// Token: 0x04001C4F RID: 7247
		RequestCanceled,
		/// <summary>The response received from the server was complete but indicated a protocol-level error. For example, an HTTP protocol error such as 401 Access Denied would use this status.</summary>
		// Token: 0x04001C50 RID: 7248
		ProtocolError,
		/// <summary>The connection was prematurely closed.</summary>
		// Token: 0x04001C51 RID: 7249
		ConnectionClosed,
		/// <summary>A server certificate could not be validated.</summary>
		// Token: 0x04001C52 RID: 7250
		TrustFailure,
		/// <summary>An error occurred while establishing a connection using SSL.</summary>
		// Token: 0x04001C53 RID: 7251
		SecureChannelFailure,
		/// <summary>The server response was not a valid HTTP response.</summary>
		// Token: 0x04001C54 RID: 7252
		ServerProtocolViolation,
		/// <summary>The connection for a request that specifies the Keep-alive header was closed unexpectedly.</summary>
		// Token: 0x04001C55 RID: 7253
		KeepAliveFailure,
		/// <summary>An internal asynchronous request is pending.</summary>
		// Token: 0x04001C56 RID: 7254
		Pending,
		/// <summary>No response was received during the time-out period for a request.</summary>
		// Token: 0x04001C57 RID: 7255
		Timeout,
		/// <summary>The name resolver service could not resolve the proxy host name.</summary>
		// Token: 0x04001C58 RID: 7256
		ProxyNameResolutionFailure,
		/// <summary>An exception of unknown type has occurred.</summary>
		// Token: 0x04001C59 RID: 7257
		UnknownError,
		/// <summary>A message was received that exceeded the specified limit when sending a request or receiving a response from the server.</summary>
		// Token: 0x04001C5A RID: 7258
		MessageLengthLimitExceeded,
		/// <summary>The specified cache entry was not found.</summary>
		// Token: 0x04001C5B RID: 7259
		CacheEntryNotFound,
		/// <summary>The request was not permitted by the cache policy. In general, this occurs when a request is not cacheable and the effective policy prohibits sending the request to the server. You might receive this status if a request method implies the presence of a request body, a request method requires direct interaction with the server, or a request contains a conditional header.</summary>
		// Token: 0x04001C5C RID: 7260
		RequestProhibitedByCachePolicy,
		/// <summary>This request was not permitted by the proxy.</summary>
		// Token: 0x04001C5D RID: 7261
		RequestProhibitedByProxy
	}
}
