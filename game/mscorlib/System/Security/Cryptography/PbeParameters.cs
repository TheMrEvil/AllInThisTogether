using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography
{
	// Token: 0x020004D6 RID: 1238
	public sealed class PbeParameters
	{
		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06003168 RID: 12648 RVA: 0x000B6C74 File Offset: 0x000B4E74
		public PbeEncryptionAlgorithm EncryptionAlgorithm
		{
			[CompilerGenerated]
			get
			{
				return this.<EncryptionAlgorithm>k__BackingField;
			}
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06003169 RID: 12649 RVA: 0x000B6C7C File Offset: 0x000B4E7C
		public HashAlgorithmName HashAlgorithm
		{
			[CompilerGenerated]
			get
			{
				return this.<HashAlgorithm>k__BackingField;
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x000B6C84 File Offset: 0x000B4E84
		public int IterationCount
		{
			[CompilerGenerated]
			get
			{
				return this.<IterationCount>k__BackingField;
			}
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x000B6C8C File Offset: 0x000B4E8C
		public PbeParameters(PbeEncryptionAlgorithm encryptionAlgorithm, HashAlgorithmName hashAlgorithm, int iterationCount)
		{
			if (iterationCount < 1)
			{
				throw new ArgumentOutOfRangeException("iterationCount", iterationCount, "Positive number required.");
			}
			this.EncryptionAlgorithm = encryptionAlgorithm;
			this.HashAlgorithm = hashAlgorithm;
			this.IterationCount = iterationCount;
		}

		// Token: 0x04002284 RID: 8836
		[CompilerGenerated]
		private readonly PbeEncryptionAlgorithm <EncryptionAlgorithm>k__BackingField;

		// Token: 0x04002285 RID: 8837
		[CompilerGenerated]
		private readonly HashAlgorithmName <HashAlgorithm>k__BackingField;

		// Token: 0x04002286 RID: 8838
		[CompilerGenerated]
		private readonly int <IterationCount>k__BackingField;
	}
}
