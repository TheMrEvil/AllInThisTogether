using System;

namespace System.Security.Cryptography
{
	// Token: 0x020004D2 RID: 1234
	public sealed class AesCcm : IDisposable
	{
		// Token: 0x06003154 RID: 12628 RVA: 0x000B6C4E File Offset: 0x000B4E4E
		public AesCcm(byte[] key)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000B6C4E File Offset: 0x000B4E4E
		public AesCcm(ReadOnlySpan<byte> key)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06003156 RID: 12630 RVA: 0x0001B98B File Offset: 0x00019B8B
		public static KeySizes NonceByteSizes
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06003157 RID: 12631 RVA: 0x0001B98B File Offset: 0x00019B8B
		public static KeySizes TagByteSizes
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x0001B98B File Offset: 0x00019B8B
		public void Decrypt(byte[] nonce, byte[] ciphertext, byte[] tag, byte[] plaintext, byte[] associatedData = null)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x0001B98B File Offset: 0x00019B8B
		public void Decrypt(ReadOnlySpan<byte> nonce, ReadOnlySpan<byte> ciphertext, ReadOnlySpan<byte> tag, Span<byte> plaintext, ReadOnlySpan<byte> associatedData = default(ReadOnlySpan<byte>))
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public void Dispose()
		{
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x0001B98B File Offset: 0x00019B8B
		public void Encrypt(byte[] nonce, byte[] plaintext, byte[] ciphertext, byte[] tag, byte[] associatedData = null)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x0001B98B File Offset: 0x00019B8B
		public void Encrypt(ReadOnlySpan<byte> nonce, ReadOnlySpan<byte> plaintext, Span<byte> ciphertext, Span<byte> tag, ReadOnlySpan<byte> associatedData = default(ReadOnlySpan<byte>))
		{
			throw new PlatformNotSupportedException();
		}
	}
}
