using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Secure Hash Algorithm (SHA) for 512-bit hash values.</summary>
	// Token: 0x0200005E RID: 94
	public sealed class SHA512Cng : SHA512
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA512Cng" /> class. </summary>
		// Token: 0x06000203 RID: 515 RVA: 0x00006084 File Offset: 0x00004284
		[SecurityCritical]
		public SHA512Cng()
		{
			this.hash = new SHA512Managed();
		}

		/// <summary>Initializes, or re-initializes, the instance of the hash algorithm. </summary>
		// Token: 0x06000204 RID: 516 RVA: 0x00006097 File Offset: 0x00004297
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000060A4 File Offset: 0x000042A4
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000060B7 File Offset: 0x000042B7
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA512Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000060E3 File Offset: 0x000042E3
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000060F7 File Offset: 0x000042F7
		// Note: this type is marked as 'beforefieldinit'.
		static SHA512Cng()
		{
		}

		// Token: 0x04000368 RID: 872
		private static byte[] Empty = new byte[0];

		// Token: 0x04000369 RID: 873
		private SHA512 hash;
	}
}
