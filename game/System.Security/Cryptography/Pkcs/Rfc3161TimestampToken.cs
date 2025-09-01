using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x0200009C RID: 156
	public sealed class Rfc3161TimestampToken
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Rfc3161TimestampTokenInfo TokenInfo
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000149DF File Offset: 0x00012BDF
		internal Rfc3161TimestampToken()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000149B5 File Offset: 0x00012BB5
		public SignedCms AsSignedCms()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000149B5 File Offset: 0x00012BB5
		public static bool TryDecode(ReadOnlyMemory<byte> encodedBytes, out Rfc3161TimestampToken token, out int bytesConsumed)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool VerifySignatureForData(ReadOnlySpan<byte> data, out X509Certificate2 signerCertificate, X509Certificate2Collection extraCandidates = null)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool VerifySignatureForHash(ReadOnlySpan<byte> hash, HashAlgorithmName hashAlgorithm, out X509Certificate2 signerCertificate, X509Certificate2Collection extraCandidates = null)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool VerifySignatureForHash(ReadOnlySpan<byte> hash, Oid hashAlgorithmId, out X509Certificate2 signerCertificate, X509Certificate2Collection extraCandidates = null)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool VerifySignatureForSignerInfo(SignerInfo signerInfo, out X509Certificate2 signerCertificate, X509Certificate2Collection extraCandidates = null)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
