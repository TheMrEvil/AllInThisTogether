using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x0200009B RID: 155
	public sealed class Rfc3161TimestampRequest
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool HasExtensions
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Oid HashAlgorithmId
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Oid RequestedPolicyId
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool RequestSignerCertificate
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x000149B5 File Offset: 0x00012BB5
		public int Version
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000149DF File Offset: 0x00012BDF
		internal Rfc3161TimestampRequest()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000149B5 File Offset: 0x00012BB5
		public static Rfc3161TimestampRequest CreateFromData(ReadOnlySpan<byte> data, HashAlgorithmName hashAlgorithm, Oid requestedPolicyId = null, ReadOnlyMemory<byte>? nonce = null, bool requestSignerCertificates = false, X509ExtensionCollection extensions = null)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000149B5 File Offset: 0x00012BB5
		public static Rfc3161TimestampRequest CreateFromHash(ReadOnlyMemory<byte> hash, HashAlgorithmName hashAlgorithm, Oid requestedPolicyId = null, ReadOnlyMemory<byte>? nonce = null, bool requestSignerCertificates = false, X509ExtensionCollection extensions = null)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000149B5 File Offset: 0x00012BB5
		public static Rfc3161TimestampRequest CreateFromHash(ReadOnlyMemory<byte> hash, Oid hashAlgorithmId, Oid requestedPolicyId = null, ReadOnlyMemory<byte>? nonce = null, bool requestSignerCertificates = false, X509ExtensionCollection extensions = null)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000149B5 File Offset: 0x00012BB5
		public static Rfc3161TimestampRequest CreateFromSignerInfo(SignerInfo signerInfo, HashAlgorithmName hashAlgorithm, Oid requestedPolicyId = null, ReadOnlyMemory<byte>? nonce = null, bool requestSignerCertificates = false, X509ExtensionCollection extensions = null)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000149B5 File Offset: 0x00012BB5
		public byte[] Encode()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000149B5 File Offset: 0x00012BB5
		public X509ExtensionCollection GetExtensions()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000149B5 File Offset: 0x00012BB5
		public ReadOnlyMemory<byte> GetMessageHash()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000149B5 File Offset: 0x00012BB5
		public ReadOnlyMemory<byte>? GetNonce()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Rfc3161TimestampToken ProcessResponse(ReadOnlyMemory<byte> responseBytes, out int bytesConsumed)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000149B5 File Offset: 0x00012BB5
		public static bool TryDecode(ReadOnlyMemory<byte> encodedBytes, out Rfc3161TimestampRequest request, out int bytesConsumed)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool TryEncode(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
