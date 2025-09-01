using System;

namespace System.Security.Cryptography
{
	// Token: 0x020004D3 RID: 1235
	public sealed class AesGcm : IDisposable
	{
		// Token: 0x0600315D RID: 12637 RVA: 0x000B6C4E File Offset: 0x000B4E4E
		public AesGcm(byte[] key)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x000B6C4E File Offset: 0x000B4E4E
		public AesGcm(ReadOnlySpan<byte> key)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x0001B98B File Offset: 0x00019B8B
		public static KeySizes NonceByteSizes
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x0001B98B File Offset: 0x00019B8B
		public static KeySizes TagByteSizes
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x0001B98B File Offset: 0x00019B8B
		public void Decrypt(byte[] nonce, byte[] ciphertext, byte[] tag, byte[] plaintext, byte[] associatedData = null)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x0001B98B File Offset: 0x00019B8B
		public void Decrypt(ReadOnlySpan<byte> nonce, ReadOnlySpan<byte> ciphertext, ReadOnlySpan<byte> tag, Span<byte> plaintext, ReadOnlySpan<byte> associatedData = default(ReadOnlySpan<byte>))
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Dispose()
		{
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x0001B98B File Offset: 0x00019B8B
		public void Encrypt(byte[] nonce, byte[] plaintext, byte[] ciphertext, byte[] tag, byte[] associatedData = null)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x0001B98B File Offset: 0x00019B8B
		public void Encrypt(ReadOnlySpan<byte> nonce, ReadOnlySpan<byte> plaintext, Span<byte> ciphertext, Span<byte> tag, ReadOnlySpan<byte> associatedData = default(ReadOnlySpan<byte>))
		{
			throw new PlatformNotSupportedException();
		}
	}
}
