using System;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;

namespace Internal.Cryptography
{
	// Token: 0x0200010D RID: 269
	internal abstract class KeyAgreeRecipientInfoPal : RecipientInfoPal
	{
		// Token: 0x060006E3 RID: 1763 RVA: 0x0001C939 File Offset: 0x0001AB39
		internal KeyAgreeRecipientInfoPal()
		{
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060006E4 RID: 1764
		public abstract DateTime Date { get; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060006E5 RID: 1765
		public abstract SubjectIdentifierOrKey OriginatorIdentifierOrKey { get; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060006E6 RID: 1766
		public abstract CryptographicAttributeObject OtherKeyAttribute { get; }
	}
}
