using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;

namespace Internal.Cryptography
{
	// Token: 0x02000108 RID: 264
	internal abstract class DecryptorPal : IDisposable
	{
		// Token: 0x060006C3 RID: 1731 RVA: 0x0001C207 File Offset: 0x0001A407
		internal DecryptorPal(RecipientInfoCollection recipientInfos)
		{
			this.RecipientInfos = recipientInfos;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060006C4 RID: 1732 RVA: 0x0001C216 File Offset: 0x0001A416
		public RecipientInfoCollection RecipientInfos
		{
			[CompilerGenerated]
			get
			{
				return this.<RecipientInfos>k__BackingField;
			}
		}

		// Token: 0x060006C5 RID: 1733
		public abstract ContentInfo TryDecrypt(RecipientInfo recipientInfo, X509Certificate2 cert, X509Certificate2Collection originatorCerts, X509Certificate2Collection extraStore, out Exception exception);

		// Token: 0x060006C6 RID: 1734
		public abstract void Dispose();

		// Token: 0x0400041C RID: 1052
		[CompilerGenerated]
		private readonly RecipientInfoCollection <RecipientInfos>k__BackingField;
	}
}
