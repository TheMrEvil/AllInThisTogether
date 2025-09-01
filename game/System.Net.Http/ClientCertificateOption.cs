using System;

namespace System.Net.Http
{
	/// <summary>Specifies how client certificates are provided.</summary>
	// Token: 0x02000013 RID: 19
	public enum ClientCertificateOption
	{
		/// <summary>The application manually provides the client certificates to the <see cref="T:System.Net.Http.WebRequestHandler" />. This value is the default.</summary>
		// Token: 0x0400004F RID: 79
		Manual,
		/// <summary>The <see cref="T:System.Net.Http.HttpClientHandler" /> will attempt to provide  all available client certificates  automatically.</summary>
		// Token: 0x04000050 RID: 80
		Automatic
	}
}
