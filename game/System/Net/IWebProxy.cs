using System;

namespace System.Net
{
	/// <summary>Provides the base interface for implementation of proxy access for the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x0200065B RID: 1627
	public interface IWebProxy
	{
		/// <summary>Returns the URI of a proxy.</summary>
		/// <param name="destination">A <see cref="T:System.Uri" /> that specifies the requested Internet resource.</param>
		/// <returns>A <see cref="T:System.Uri" /> instance that contains the URI of the proxy used to contact <paramref name="destination" />.</returns>
		// Token: 0x0600335D RID: 13149
		Uri GetProxy(Uri destination);

		/// <summary>Indicates that the proxy should not be used for the specified host.</summary>
		/// <param name="host">The <see cref="T:System.Uri" /> of the host to check for proxy use.</param>
		/// <returns>
		///   <see langword="true" /> if the proxy server should not be used for <paramref name="host" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600335E RID: 13150
		bool IsBypassed(Uri host);

		/// <summary>The credentials to submit to the proxy server for authentication.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> instance that contains the credentials that are needed to authenticate a request to the proxy server.</returns>
		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x0600335F RID: 13151
		// (set) Token: 0x06003360 RID: 13152
		ICredentials Credentials { get; set; }
	}
}
