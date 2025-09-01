using System;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000099 RID: 153
	public sealed class Pkcs8PrivateKeyInfo
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Oid AlgorithmId
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x000149B5 File Offset: 0x00012BB5
		public ReadOnlyMemory<byte>? AlgorithmParameters
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x000149B5 File Offset: 0x00012BB5
		public CryptographicAttributeObjectCollection Attributes
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x000149B5 File Offset: 0x00012BB5
		public ReadOnlyMemory<byte> PrivateKeyBytes
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000149DF File Offset: 0x00012BDF
		public Pkcs8PrivateKeyInfo(Oid algorithmId, ReadOnlyMemory<byte>? algorithmParameters, ReadOnlyMemory<byte> privateKey, bool skipCopies = false)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000149B5 File Offset: 0x00012BB5
		public static Pkcs8PrivateKeyInfo Create(AsymmetricAlgorithm privateKey)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x000149B5 File Offset: 0x00012BB5
		public static Pkcs8PrivateKeyInfo Decode(ReadOnlyMemory<byte> source, out int bytesRead, bool skipCopy = false)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x000149B5 File Offset: 0x00012BB5
		public static Pkcs8PrivateKeyInfo DecryptAndDecode(ReadOnlySpan<byte> passwordBytes, ReadOnlyMemory<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x000149B5 File Offset: 0x00012BB5
		public static Pkcs8PrivateKeyInfo DecryptAndDecode(ReadOnlySpan<char> password, ReadOnlyMemory<byte> source, out int bytesRead)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x000149B5 File Offset: 0x00012BB5
		public byte[] Encode()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000149B5 File Offset: 0x00012BB5
		public byte[] Encrypt(ReadOnlySpan<byte> passwordBytes, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x000149B5 File Offset: 0x00012BB5
		public byte[] Encrypt(ReadOnlySpan<char> password, PbeParameters pbeParameters)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool TryEncode(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool TryEncrypt(ReadOnlySpan<byte> passwordBytes, PbeParameters pbeParameters, Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool TryEncrypt(ReadOnlySpan<char> password, PbeParameters pbeParameters, Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
