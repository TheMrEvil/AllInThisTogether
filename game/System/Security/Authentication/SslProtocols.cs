using System;

namespace System.Security.Authentication
{
	/// <summary>Defines the possible versions of <see cref="T:System.Security.Authentication.SslProtocols" />.</summary>
	// Token: 0x020002A0 RID: 672
	[Flags]
	public enum SslProtocols
	{
		/// <summary>Allows the operating system to choose the best protocol to use, and to block protocols that are not secure. Unless your app has a specific reason not to, you should use this field.</summary>
		// Token: 0x04000BE8 RID: 3048
		None = 0,
		/// <summary>Specifies the SSL 2.0 protocol. SSL 2.0 has been superseded by the TLS protocol and is provided for backward compatibility only.</summary>
		// Token: 0x04000BE9 RID: 3049
		Ssl2 = 12,
		/// <summary>Specifies the SSL 3.0 protocol. SSL 3.0 has been superseded by the TLS protocol and is provided for backward compatibility only.</summary>
		// Token: 0x04000BEA RID: 3050
		Ssl3 = 48,
		/// <summary>Specifies the TLS 1.0 security protocol. The TLS protocol is defined in IETF RFC 2246.</summary>
		// Token: 0x04000BEB RID: 3051
		Tls = 192,
		/// <summary>Specifies the TLS 1.1 security protocol. The TLS protocol is defined in IETF RFC 4346.</summary>
		// Token: 0x04000BEC RID: 3052
		[MonoTODO("unsupported")]
		Tls11 = 768,
		/// <summary>Specifies the TLS 1.2 security protocol. The TLS protocol is defined in IETF RFC 5246.</summary>
		// Token: 0x04000BED RID: 3053
		[MonoTODO("unsupported")]
		Tls12 = 3072,
		/// <summary>Specifies the TLS 1.3 security protocol. The TLS protocol is defined in IETF RFC 8446.</summary>
		// Token: 0x04000BEE RID: 3054
		Tls13 = 12288,
		/// <summary>Use None instead of Default. Default permits only the Secure Sockets Layer (SSL) 3.0 or Transport Layer Security (TLS) 1.0 protocols to be negotiated, and those options are now considered obsolete. Consequently, Default is not allowed in many organizations. Despite the name of this field, <see cref="T:System.Net.Security.SslStream" /> does not use it as a default except under special circumstances.</summary>
		// Token: 0x04000BEF RID: 3055
		Default = 240
	}
}
