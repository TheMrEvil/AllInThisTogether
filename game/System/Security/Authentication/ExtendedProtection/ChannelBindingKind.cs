using System;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.ChannelBindingKind" /> enumeration represents the kinds of channel bindings that can be queried from secure channels.</summary>
	// Token: 0x020002A5 RID: 677
	public enum ChannelBindingKind
	{
		/// <summary>An unknown channel binding type.</summary>
		// Token: 0x04000BF6 RID: 3062
		Unknown,
		/// <summary>A channel binding completely unique to a given channel (a TLS session key, for example).</summary>
		// Token: 0x04000BF7 RID: 3063
		Unique = 25,
		/// <summary>A channel binding unique to a given endpoint (a TLS server certificate, for example).</summary>
		// Token: 0x04000BF8 RID: 3064
		Endpoint
	}
}
