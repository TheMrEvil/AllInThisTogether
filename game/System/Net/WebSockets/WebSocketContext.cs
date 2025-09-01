using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Principal;

namespace System.Net.WebSockets
{
	/// <summary>Used for accessing the information in the WebSocket handshake.</summary>
	// Token: 0x020007EF RID: 2031
	public abstract class WebSocketContext
	{
		/// <summary>The URI requested by the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.Uri" />.</returns>
		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x060040B8 RID: 16568
		public abstract Uri RequestUri { get; }

		/// <summary>The HTTP headers that were sent to the server during the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Specialized.NameValueCollection" />.</returns>
		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x060040B9 RID: 16569
		public abstract NameValueCollection Headers { get; }

		/// <summary>The value of the Origin HTTP header included in the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x060040BA RID: 16570
		public abstract string Origin { get; }

		/// <summary>The value of the SecWebSocketKey HTTP header included in the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Collections.Generic.IEnumerable`1" />.</returns>
		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x060040BB RID: 16571
		public abstract IEnumerable<string> SecWebSocketProtocols { get; }

		/// <summary>The list of subprotocols requested by the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x060040BC RID: 16572
		public abstract string SecWebSocketVersion { get; }

		/// <summary>The value of the SecWebSocketKey HTTP header included in the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.String" />.</returns>
		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x060040BD RID: 16573
		public abstract string SecWebSocketKey { get; }

		/// <summary>The cookies that were passed to the server during the opening handshake.</summary>
		/// <returns>Returns <see cref="T:System.Net.CookieCollection" />.</returns>
		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x060040BE RID: 16574
		public abstract CookieCollection CookieCollection { get; }

		/// <summary>An object used to obtain identity, authentication information, and security roles for the WebSocket client.</summary>
		/// <returns>Returns <see cref="T:System.Security.Principal.IPrincipal" />.</returns>
		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x060040BF RID: 16575
		public abstract IPrincipal User { get; }

		/// <summary>Whether the WebSocket client is authenticated.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x060040C0 RID: 16576
		public abstract bool IsAuthenticated { get; }

		/// <summary>Whether the WebSocket client connected from the local machine.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x060040C1 RID: 16577
		public abstract bool IsLocal { get; }

		/// <summary>Whether the WebSocket connection is secured using Secure Sockets Layer (SSL).</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.</returns>
		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x060040C2 RID: 16578
		public abstract bool IsSecureConnection { get; }

		/// <summary>The WebSocket instance used to interact (send/receive/close/etc) with the WebSocket connection.</summary>
		/// <returns>Returns <see cref="T:System.Net.WebSockets.WebSocket" />.</returns>
		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x060040C3 RID: 16579
		public abstract WebSocket WebSocket { get; }

		/// <summary>Creates an instance of the <see cref="T:System.Net.WebSockets.WebSocketContext" /> class.</summary>
		// Token: 0x060040C4 RID: 16580 RVA: 0x0000219B File Offset: 0x0000039B
		protected WebSocketContext()
		{
		}
	}
}
