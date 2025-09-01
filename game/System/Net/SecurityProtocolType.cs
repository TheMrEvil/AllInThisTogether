using System;

namespace System.Net
{
	/// <summary>Specifies the security protocols that are supported by the Schannel security package.</summary>
	// Token: 0x02000596 RID: 1430
	[Flags]
	public enum SecurityProtocolType
	{
		/// <summary>Allows the operating system to choose the best protocol to use, and to block protocols that are not secure. Unless your app has a specific reason not to, you should use this value.</summary>
		// Token: 0x040019A7 RID: 6567
		SystemDefault = 0,
		/// <summary>Specifies the Secure Socket Layer (SSL) 3.0 security protocol. SSL 3.0 has been superseded by the Transport Layer Security (TLS) protocol and is provided for backward compatibility only.</summary>
		// Token: 0x040019A8 RID: 6568
		Ssl3 = 48,
		/// <summary>Specifies the Transport Layer Security (TLS) 1.0 security protocol. The TLS 1.0 protocol is defined in IETF RFC 2246.</summary>
		// Token: 0x040019A9 RID: 6569
		Tls = 192,
		/// <summary>Specifies the Transport Layer Security (TLS) 1.1 security protocol. The TLS 1.1 protocol is defined in IETF RFC 4346. On Windows systems, this value is supported starting with Windows 7.</summary>
		// Token: 0x040019AA RID: 6570
		Tls11 = 768,
		/// <summary>Specifies the Transport Layer Security (TLS) 1.2 security protocol. The TLS 1.2 protocol is defined in IETF RFC 5246. On Windows systems, this value is supported starting with Windows 7.</summary>
		// Token: 0x040019AB RID: 6571
		Tls12 = 3072,
		/// <summary>Specifies the TLS 1.3 security protocol. The TLS protocol is defined in IETF RFC 8446.</summary>
		// Token: 0x040019AC RID: 6572
		Tls13 = 12288
	}
}
