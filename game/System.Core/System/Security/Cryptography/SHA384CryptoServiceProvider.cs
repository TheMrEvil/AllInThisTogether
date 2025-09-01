using System;

namespace System.Security.Cryptography
{
	/// <summary>Defines a wrapper object to access the cryptographic service provider (CSP) implementation of the <see cref="T:System.Security.Cryptography.SHA384" /> algorithm. </summary>
	// Token: 0x0200005D RID: 93
	public sealed class SHA384CryptoServiceProvider : SHA384
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.SHA384CryptoServiceProvider" /> class. </summary>
		// Token: 0x060001FD RID: 509 RVA: 0x00006004 File Offset: 0x00004204
		[SecurityCritical]
		public SHA384CryptoServiceProvider()
		{
			this.hash = new SHA384Managed();
		}

		/// <summary>Initializes, or reinitializes, an instance of a hash algorithm.</summary>
		// Token: 0x060001FE RID: 510 RVA: 0x00006017 File Offset: 0x00004217
		[SecurityCritical]
		public override void Initialize()
		{
			this.hash.Initialize();
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00006024 File Offset: 0x00004224
		[SecurityCritical]
		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			this.hash.TransformBlock(array, ibStart, cbSize, null, 0);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00006037 File Offset: 0x00004237
		[SecurityCritical]
		protected override byte[] HashFinal()
		{
			this.hash.TransformFinalBlock(SHA384CryptoServiceProvider.Empty, 0, 0);
			this.HashValue = this.hash.Hash;
			return this.HashValue;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006063 File Offset: 0x00004263
		[SecurityCritical]
		protected override void Dispose(bool disposing)
		{
			((IDisposable)this.hash).Dispose();
			base.Dispose(disposing);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00006077 File Offset: 0x00004277
		// Note: this type is marked as 'beforefieldinit'.
		static SHA384CryptoServiceProvider()
		{
		}

		// Token: 0x04000366 RID: 870
		private static byte[] Empty = new byte[0];

		// Token: 0x04000367 RID: 871
		private SHA384 hash;
	}
}
