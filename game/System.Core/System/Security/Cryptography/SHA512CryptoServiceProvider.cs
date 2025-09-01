using System;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) implementation of the <see cref="T:System.Security.Cryptography.SHA512" /> algorithm. </summary>
	// Token: 0x0200005F RID: 95
	public sealed class SHA512CryptoServiceProvider : SHA512
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA512CryptoServiceProvider" /> class. </summary>
		// Token: 0x06000209 RID: 521 RVA: 0x00006104 File Offset: 0x00004304
		[SecurityCritical]
		public SHA512CryptoServiceProvider()
		{
			this.hash = new SHA512Managed();
		}

		/// <summary>Initializes, or reinitializes, an instance of a hash algorithm.</summary>
		// Token: 0x0600020A RID: 522 RVA: 0x00006117 File Offset: 0x00004317
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00006124 File Offset: 0x00004324
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006137 File Offset: 0x00004337
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA512CryptoServiceProvider.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00006163 File Offset: 0x00004363
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00006177 File Offset: 0x00004377
		// Note: this type is marked as 'beforefieldinit'.
		static SHA512CryptoServiceProvider()
		{
		}

		// Token: 0x0400036A RID: 874
		private static byte[] Empty = new byte[0];

		// Token: 0x0400036B RID: 875
		private SHA512 hash;
	}
}
