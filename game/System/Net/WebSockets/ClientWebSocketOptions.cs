using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net.WebSockets
{
	/// <summary>Options to use with a  <see cref="T:System.Net.WebSockets.ClientWebSocket" /> object.</summary>
	// Token: 0x020007E2 RID: 2018
	public sealed class ClientWebSocketOptions
	{
		// Token: 0x06004056 RID: 16470 RVA: 0x000DD1EE File Offset: 0x000DB3EE
		internal ClientWebSocketOptions()
		{
			this._requestedSubProtocols = new List<string>();
			this._requestHeaders = new WebHeaderCollection();
		}

		/// <summary>Creates a HTTP request header and its value.</summary>
		/// <param name="headerName">The name of the HTTP header.</param>
		/// <param name="headerValue">The value of the HTTP header.</param>
		// Token: 0x06004057 RID: 16471 RVA: 0x000DD22D File Offset: 0x000DB42D
		public void SetRequestHeader(string headerName, string headerValue)
		{
			this.ThrowIfReadOnly();
			this._requestHeaders.Set(headerName, headerValue);
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06004058 RID: 16472 RVA: 0x000DD242 File Offset: 0x000DB442
		internal WebHeaderCollection RequestHeaders
		{
			get
			{
				return this._requestHeaders;
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06004059 RID: 16473 RVA: 0x000DD24A File Offset: 0x000DB44A
		internal List<string> RequestedSubProtocols
		{
			get
			{
				return this._requestedSubProtocols;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates if default credentials should be used during WebSocket handshake.</summary>
		/// <returns>
		///   <see langword="true" /> if default credentials should be used during WebSocket handshake; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x0600405A RID: 16474 RVA: 0x000DD252 File Offset: 0x000DB452
		// (set) Token: 0x0600405B RID: 16475 RVA: 0x000DD25A File Offset: 0x000DB45A
		public bool UseDefaultCredentials
		{
			get
			{
				return this._useDefaultCredentials;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._useDefaultCredentials = value;
			}
		}

		/// <summary>Gets or sets the credential information for the client.</summary>
		/// <returns>The credential information for the client.</returns>
		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x0600405C RID: 16476 RVA: 0x000DD269 File Offset: 0x000DB469
		// (set) Token: 0x0600405D RID: 16477 RVA: 0x000DD271 File Offset: 0x000DB471
		public ICredentials Credentials
		{
			get
			{
				return this._credentials;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._credentials = value;
			}
		}

		/// <summary>Gets or sets the proxy for WebSocket requests.</summary>
		/// <returns>The proxy for WebSocket requests.</returns>
		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x0600405E RID: 16478 RVA: 0x000DD280 File Offset: 0x000DB480
		// (set) Token: 0x0600405F RID: 16479 RVA: 0x000DD288 File Offset: 0x000DB488
		public IWebProxy Proxy
		{
			get
			{
				return this._proxy;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._proxy = value;
			}
		}

		/// <summary>Gets or sets a collection of client side certificates.</summary>
		/// <returns>A collection of client side certificates.</returns>
		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06004060 RID: 16480 RVA: 0x000DD297 File Offset: 0x000DB497
		// (set) Token: 0x06004061 RID: 16481 RVA: 0x000DD2B2 File Offset: 0x000DB4B2
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this._clientCertificates == null)
				{
					this._clientCertificates = new X509CertificateCollection();
				}
				return this._clientCertificates;
			}
			set
			{
				this.ThrowIfReadOnly();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._clientCertificates = value;
			}
		}

		// Token: 0x17000E94 RID: 3732
		// (get) Token: 0x06004062 RID: 16482 RVA: 0x000DD2CF File Offset: 0x000DB4CF
		// (set) Token: 0x06004063 RID: 16483 RVA: 0x000DD2D7 File Offset: 0x000DB4D7
		public RemoteCertificateValidationCallback RemoteCertificateValidationCallback
		{
			get
			{
				return this._remoteCertificateValidationCallback;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._remoteCertificateValidationCallback = value;
			}
		}

		/// <summary>Gets or sets the cookies associated with the request.</summary>
		/// <returns>The cookies associated with the request.</returns>
		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x06004064 RID: 16484 RVA: 0x000DD2E6 File Offset: 0x000DB4E6
		// (set) Token: 0x06004065 RID: 16485 RVA: 0x000DD2EE File Offset: 0x000DB4EE
		public CookieContainer Cookies
		{
			get
			{
				return this._cookies;
			}
			set
			{
				this.ThrowIfReadOnly();
				this._cookies = value;
			}
		}

		/// <summary>Adds a sub-protocol to be negotiated during the WebSocket connection handshake.</summary>
		/// <param name="subProtocol">The WebSocket sub-protocol to add.</param>
		// Token: 0x06004066 RID: 16486 RVA: 0x000DD300 File Offset: 0x000DB500
		public void AddSubProtocol(string subProtocol)
		{
			this.ThrowIfReadOnly();
			WebSocketValidate.ValidateSubprotocol(subProtocol);
			using (List<string>.Enumerator enumerator = this._requestedSubProtocols.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (string.Equals(enumerator.Current, subProtocol, StringComparison.OrdinalIgnoreCase))
					{
						throw new ArgumentException(SR.Format("Duplicate protocols are not allowed: '{0}'.", subProtocol), "subProtocol");
					}
				}
			}
			this._requestedSubProtocols.Add(subProtocol);
		}

		/// <summary>Gets or sets the WebSocket protocol keep-alive interval.</summary>
		/// <returns>The WebSocket protocol keep-alive interval.</returns>
		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06004067 RID: 16487 RVA: 0x000DD384 File Offset: 0x000DB584
		// (set) Token: 0x06004068 RID: 16488 RVA: 0x000DD38C File Offset: 0x000DB58C
		public TimeSpan KeepAliveInterval
		{
			get
			{
				return this._keepAliveInterval;
			}
			set
			{
				this.ThrowIfReadOnly();
				if (value != Timeout.InfiniteTimeSpan && value < TimeSpan.Zero)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format("The argument must be a value greater than {0}.", Timeout.InfiniteTimeSpan.ToString()));
				}
				this._keepAliveInterval = value;
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06004069 RID: 16489 RVA: 0x000DD3EB File Offset: 0x000DB5EB
		internal int ReceiveBufferSize
		{
			get
			{
				return this._receiveBufferSize;
			}
		}

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x0600406A RID: 16490 RVA: 0x000DD3F3 File Offset: 0x000DB5F3
		internal int SendBufferSize
		{
			get
			{
				return this._sendBufferSize;
			}
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x0600406B RID: 16491 RVA: 0x000DD3FB File Offset: 0x000DB5FB
		internal ArraySegment<byte>? Buffer
		{
			get
			{
				return this._buffer;
			}
		}

		/// <summary>Sets the client buffer parameters.</summary>
		/// <param name="receiveBufferSize">The size, in bytes, of the client receive buffer.</param>
		/// <param name="sendBufferSize">The size, in bytes, of the client send buffer.</param>
		// Token: 0x0600406C RID: 16492 RVA: 0x000DD404 File Offset: 0x000DB604
		public void SetBuffer(int receiveBufferSize, int sendBufferSize)
		{
			this.ThrowIfReadOnly();
			if (receiveBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			if (sendBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("sendBufferSize", sendBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			this._receiveBufferSize = receiveBufferSize;
			this._sendBufferSize = sendBufferSize;
			this._buffer = null;
		}

		/// <summary>Sets client buffer parameters.</summary>
		/// <param name="receiveBufferSize">The size, in bytes, of the client receive buffer.</param>
		/// <param name="sendBufferSize">The size, in bytes, of the client send buffer.</param>
		/// <param name="buffer">The receive buffer to use.</param>
		// Token: 0x0600406D RID: 16493 RVA: 0x000DD47C File Offset: 0x000DB67C
		public void SetBuffer(int receiveBufferSize, int sendBufferSize, ArraySegment<byte> buffer)
		{
			this.ThrowIfReadOnly();
			if (receiveBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("receiveBufferSize", receiveBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			if (sendBufferSize <= 0)
			{
				throw new ArgumentOutOfRangeException("sendBufferSize", sendBufferSize, SR.Format("The argument must be a value greater than {0}.", 1));
			}
			WebSocketValidate.ValidateArraySegment(buffer, "buffer");
			if (buffer.Count == 0)
			{
				throw new ArgumentOutOfRangeException("buffer");
			}
			this._receiveBufferSize = receiveBufferSize;
			this._sendBufferSize = sendBufferSize;
			this._buffer = new ArraySegment<byte>?(buffer);
		}

		// Token: 0x0600406E RID: 16494 RVA: 0x000DD512 File Offset: 0x000DB712
		internal void SetToReadOnly()
		{
			this._isReadOnly = true;
		}

		// Token: 0x0600406F RID: 16495 RVA: 0x000DD51B File Offset: 0x000DB71B
		private void ThrowIfReadOnly()
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException("The WebSocket has already been started.");
			}
		}

		// Token: 0x040026DD RID: 9949
		private bool _isReadOnly;

		// Token: 0x040026DE RID: 9950
		private readonly List<string> _requestedSubProtocols;

		// Token: 0x040026DF RID: 9951
		private readonly WebHeaderCollection _requestHeaders;

		// Token: 0x040026E0 RID: 9952
		private TimeSpan _keepAliveInterval = WebSocket.DefaultKeepAliveInterval;

		// Token: 0x040026E1 RID: 9953
		private bool _useDefaultCredentials;

		// Token: 0x040026E2 RID: 9954
		private ICredentials _credentials;

		// Token: 0x040026E3 RID: 9955
		private IWebProxy _proxy;

		// Token: 0x040026E4 RID: 9956
		private X509CertificateCollection _clientCertificates;

		// Token: 0x040026E5 RID: 9957
		private CookieContainer _cookies;

		// Token: 0x040026E6 RID: 9958
		private int _receiveBufferSize = 4096;

		// Token: 0x040026E7 RID: 9959
		private int _sendBufferSize = 4096;

		// Token: 0x040026E8 RID: 9960
		private ArraySegment<byte>? _buffer;

		// Token: 0x040026E9 RID: 9961
		private RemoteCertificateValidationCallback _remoteCertificateValidationCallback;
	}
}
