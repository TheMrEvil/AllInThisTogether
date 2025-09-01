using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a CNG (Cryptography Next Generation) implementation of the MD5 (Message Digest 5) 128-bit hashing algorithm.</summary>
	// Token: 0x02000058 RID: 88
	public sealed class MD5Cng : MD5
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.MD5Cng" /> class. </summary>
		/// <exception cref="T:System.InvalidOperationException">This implementation is not part of the Windows Platform FIPS-validated cryptographic algorithms.</exception>
		// Token: 0x060001DF RID: 479 RVA: 0x00005D84 File Offset: 0x00003F84
		[SecurityCritical]
		public MD5Cng()
		{
			this.hash = new MD5CryptoServiceProvider();
		}

		/// <summary>Initializes, or re-initializes, the instance of the hash algorithm. </summary>
		// Token: 0x060001E0 RID: 480 RVA: 0x00005D97 File Offset: 0x00003F97
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00005DA4 File Offset: 0x00003FA4
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00005DB7 File Offset: 0x00003FB7
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(MD5Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00005DE3 File Offset: 0x00003FE3
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00005DF7 File Offset: 0x00003FF7
		// Note: this type is marked as 'beforefieldinit'.
		static MD5Cng()
		{
		}

		// Token: 0x0400035C RID: 860
		private static byte[] Empty = new byte[0];

		// Token: 0x0400035D RID: 861
		private MD5 hash;
	}
}
