using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Specifies characteristics of the X.500 distinguished name.</summary>
	// Token: 0x020002C1 RID: 705
	[Flags]
	public enum X500DistinguishedNameFlags
	{
		/// <summary>The distinguished name has no special characteristics.</summary>
		// Token: 0x04000C6D RID: 3181
		None = 0,
		/// <summary>The distinguished name is reversed.</summary>
		// Token: 0x04000C6E RID: 3182
		Reversed = 1,
		/// <summary>The distinguished name uses semicolons.</summary>
		// Token: 0x04000C6F RID: 3183
		UseSemicolons = 16,
		/// <summary>The distinguished name does not use the plus sign.</summary>
		// Token: 0x04000C70 RID: 3184
		DoNotUsePlusSign = 32,
		/// <summary>The distinguished name does not use quotation marks.</summary>
		// Token: 0x04000C71 RID: 3185
		DoNotUseQuotes = 64,
		/// <summary>The distinguished name uses commas.</summary>
		// Token: 0x04000C72 RID: 3186
		UseCommas = 128,
		/// <summary>The distinguished name uses the new line character.</summary>
		// Token: 0x04000C73 RID: 3187
		UseNewLines = 256,
		/// <summary>The distinguished name uses UTF8 encoding instead of Unicode character encoding.</summary>
		// Token: 0x04000C74 RID: 3188
		UseUTF8Encoding = 4096,
		/// <summary>The distinguished name uses T61 encoding.</summary>
		// Token: 0x04000C75 RID: 3189
		UseT61Encoding = 8192,
		/// <summary>Forces the distinguished name to encode specific X.500 keys as UTF-8 strings rather than printable Unicode strings. For more information and the list of X.500 keys affected, see the X500NameFlags enumeration.</summary>
		// Token: 0x04000C76 RID: 3190
		ForceUTF8Encoding = 16384
	}
}
