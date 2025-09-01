using System;
using System.Security.Cryptography.Pkcs;

namespace Internal.Cryptography
{
	// Token: 0x02000112 RID: 274
	internal abstract class RecipientInfoPal
	{
		// Token: 0x060006FB RID: 1787 RVA: 0x00002145 File Offset: 0x00000345
		internal RecipientInfoPal()
		{
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060006FC RID: 1788
		public abstract byte[] EncryptedKey { get; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060006FD RID: 1789
		public abstract AlgorithmIdentifier KeyEncryptionAlgorithm { get; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060006FE RID: 1790
		public abstract SubjectIdentifier RecipientIdentifier { get; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060006FF RID: 1791
		public abstract int Version { get; }
	}
}
