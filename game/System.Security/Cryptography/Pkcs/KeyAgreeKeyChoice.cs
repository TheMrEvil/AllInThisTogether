using System;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.KeyAgreeKeyChoice" /> enumeration defines the type of key used in a key agreement protocol.</summary>
	// Token: 0x0200008D RID: 141
	public enum KeyAgreeKeyChoice
	{
		/// <summary>The key agreement key type is unknown.</summary>
		// Token: 0x040002D0 RID: 720
		Unknown,
		/// <summary>The key agreement key is ephemeral, existing only for the duration of the key agreement protocol.</summary>
		// Token: 0x040002D1 RID: 721
		EphemeralKey,
		/// <summary>The key agreement key is static, existing for an extended period of time.</summary>
		// Token: 0x040002D2 RID: 722
		StaticKey
	}
}
