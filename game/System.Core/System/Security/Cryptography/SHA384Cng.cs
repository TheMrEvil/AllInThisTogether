using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Secure Hash Algorithm (SHA) for 384-bit hash values.</summary>
	// Token: 0x0200005C RID: 92
	public sealed class SHA384Cng : SHA384
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA384Cng" /> class. </summary>
		// Token: 0x060001F7 RID: 503 RVA: 0x00005F84 File Offset: 0x00004184
		[SecurityCritical]
		public SHA384Cng()
		{
			this.hash = new SHA384Managed();
		}

		/// <summary>Initializes, or re-initializes, the instance of the hash algorithm. </summary>
		// Token: 0x060001F8 RID: 504 RVA: 0x00005F97 File Offset: 0x00004197
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00005FA4 File Offset: 0x000041A4
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00005FB7 File Offset: 0x000041B7
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA384Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00005FE3 File Offset: 0x000041E3
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00005FF7 File Offset: 0x000041F7
		// Note: this type is marked as 'beforefieldinit'.
		static SHA384Cng()
		{
		}

		// Token: 0x04000364 RID: 868
		private static byte[] Empty = new byte[0];

		// Token: 0x04000365 RID: 869
		private SHA384 hash;
	}
}
