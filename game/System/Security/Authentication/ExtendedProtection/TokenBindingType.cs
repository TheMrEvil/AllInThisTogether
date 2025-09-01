using System;

namespace System.Security.Authentication.ExtendedProtection
{
	/// <summary>Represents types of token binding.</summary>
	// Token: 0x020002A2 RID: 674
	public enum TokenBindingType
	{
		/// <summary>Used to establish a token binding when connecting to a server.</summary>
		// Token: 0x04000BF1 RID: 3057
		Provided,
		/// <summary>Used when requesting tokens to be presented to a different server.</summary>
		// Token: 0x04000BF2 RID: 3058
		Referred
	}
}
