using System;

namespace System.Security.Cryptography
{
	/// <summary>Specifies whether to create an asymmetric signature key or an asymmetric exchange key.</summary>
	// Token: 0x02000468 RID: 1128
	public enum KeyNumber
	{
		/// <summary>An exchange key pair used to encrypt session keys so that they can be safely stored and exchanged with other users.</summary>
		// Token: 0x040020CF RID: 8399
		Exchange = 1,
		/// <summary>A signature key pair used for authenticating digitally signed messages or files.</summary>
		// Token: 0x040020D0 RID: 8400
		Signature
	}
}
