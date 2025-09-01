using System;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) implementation of the <see cref="T:System.Security.Cryptography.SHA256" /> algorithm. </summary>
	// Token: 0x0200005B RID: 91
	public sealed class SHA256CryptoServiceProvider : SHA256
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA256CryptoServiceProvider" /> class. </summary>
		// Token: 0x060001F1 RID: 497 RVA: 0x00005F04 File Offset: 0x00004104
		[SecurityCritical]
		public SHA256CryptoServiceProvider()
		{
			this.hash = new SHA256Managed();
		}

		/// <summary>Initializes, or reinitializes, an instance of a hash algorithm.</summary>
		// Token: 0x060001F2 RID: 498 RVA: 0x00005F17 File Offset: 0x00004117
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00005F24 File Offset: 0x00004124
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00005F37 File Offset: 0x00004137
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA256CryptoServiceProvider.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00005F63 File Offset: 0x00004163
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00005F77 File Offset: 0x00004177
		// Note: this type is marked as 'beforefieldinit'.
		static SHA256CryptoServiceProvider()
		{
		}

		// Token: 0x04000362 RID: 866
		private static byte[] Empty = new byte[0];

		// Token: 0x04000363 RID: 867
		private SHA256 hash;
	}
}
