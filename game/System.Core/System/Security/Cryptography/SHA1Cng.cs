using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Secure Hash Algorithm (SHA).</summary>
	// Token: 0x02000059 RID: 89
	public sealed class SHA1Cng : SHA1
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA1Cng" /> class. </summary>
		// Token: 0x060001E5 RID: 485 RVA: 0x00005E04 File Offset: 0x00004004
		[SecurityCritical]
		public SHA1Cng()
		{
			this.hash = new SHA1Managed();
		}

		/// <summary>Initializes, or re-initializes, the instance of the hash algorithm. </summary>
		// Token: 0x060001E6 RID: 486 RVA: 0x00005E17 File Offset: 0x00004017
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00005E24 File Offset: 0x00004024
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x00005E37 File Offset: 0x00004037
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA1Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00005E63 File Offset: 0x00004063
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00005E77 File Offset: 0x00004077
		// Note: this type is marked as 'beforefieldinit'.
		static SHA1Cng()
		{
		}

		// Token: 0x0400035E RID: 862
		private static byte[] Empty = new byte[0];

		// Token: 0x0400035F RID: 863
		private SHA1 hash;
	}
}
