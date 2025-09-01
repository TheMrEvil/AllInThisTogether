using System;

namespace System.Security.Cryptography
{
	/// <summary>Provides a Cryptography Next Generation (CNG) implementation of the Secure Hash Algorithm (SHA) for 256-bit hash values.</summary>
	// Token: 0x0200005A RID: 90
	public sealed class SHA256Cng : SHA256
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA256Cng" /> class. </summary>
		// Token: 0x060001EB RID: 491 RVA: 0x00005E84 File Offset: 0x00004084
		[SecurityCritical]
		public SHA256Cng()
		{
			this.hash = new SHA256Managed();
		}

		/// <summary>Initializes, or re-initializes, the instance of the hash algorithm. </summary>
		// Token: 0x060001EC RID: 492 RVA: 0x00005E97 File Offset: 0x00004097
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00005EA4 File Offset: 0x000040A4
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00005EB7 File Offset: 0x000040B7
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA256Cng.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00005EE3 File Offset: 0x000040E3
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00005EF7 File Offset: 0x000040F7
		// Note: this type is marked as 'beforefieldinit'.
		static SHA256Cng()
		{
		}

		// Token: 0x04000360 RID: 864
		private static byte[] Empty = new byte[0];

		// Token: 0x04000361 RID: 865
		private SHA256 hash;
	}
}
