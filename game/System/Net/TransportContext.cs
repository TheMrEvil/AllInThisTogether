using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	/// <summary>The <see cref="T:System.Net.TransportContext" /> class provides additional context about the underlying transport layer.</summary>
	// Token: 0x02000602 RID: 1538
	public abstract class TransportContext
	{
		/// <summary>Retrieves the requested channel binding.</summary>
		/// <param name="kind">The type of channel binding to retrieve.</param>
		/// <returns>The requested <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBinding" />, or <see langword="null" /> if the channel binding is not supported by the current transport or by the operating system.</returns>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="kind" /> is must be <see cref="F:System.Security.Authentication.ExtendedProtection.ChannelBindingKind.Endpoint" /> for use with the <see cref="T:System.Net.TransportContext" /> retrieved from the <see cref="P:System.Net.HttpListenerRequest.TransportContext" /> property.</exception>
		// Token: 0x060030A7 RID: 12455
		public abstract ChannelBinding GetChannelBinding(ChannelBindingKind kind);

		/// <summary>Gets the transport security layer token bindings.</summary>
		/// <returns>The transport security layer token bindings.</returns>
		// Token: 0x060030A8 RID: 12456 RVA: 0x000044FA File Offset: 0x000026FA
		public virtual IEnumerable<TokenBinding> GetTlsTokenBindings()
		{
			throw new NotSupportedException();
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.TransportContext" /> class</summary>
		// Token: 0x060030A9 RID: 12457 RVA: 0x0000219B File Offset: 0x0000039B
		protected TransportContext()
		{
		}
	}
}
