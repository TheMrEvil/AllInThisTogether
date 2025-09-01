using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x0200009D RID: 157
	public sealed class Rfc3161TimestampTokenInfo
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x000149B5 File Offset: 0x00012BB5
		public long? AccuracyInMicroseconds
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool HasExtensions
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Oid HashAlgorithmId
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool IsOrdering
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Oid PolicyId
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x000149B5 File Offset: 0x00012BB5
		public DateTimeOffset Timestamp
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x000149B5 File Offset: 0x00012BB5
		public int Version
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000149DF File Offset: 0x00012BDF
		public Rfc3161TimestampTokenInfo(Oid policyId, Oid hashAlgorithmId, ReadOnlyMemory<byte> messageHash, ReadOnlyMemory<byte> serialNumber, DateTimeOffset timestamp, long? accuracyInMicroseconds = null, bool isOrdering = false, ReadOnlyMemory<byte>? nonce = null, ReadOnlyMemory<byte>? timestampAuthorityName = null, X509ExtensionCollection extensions = null)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000149B5 File Offset: 0x00012BB5
		public byte[] Encode()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000149B5 File Offset: 0x00012BB5
		public X509ExtensionCollection GetExtensions()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x000149B5 File Offset: 0x00012BB5
		public ReadOnlyMemory<byte> GetMessageHash()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000149B5 File Offset: 0x00012BB5
		public ReadOnlyMemory<byte>? GetNonce()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000149B5 File Offset: 0x00012BB5
		public ReadOnlyMemory<byte> GetSerialNumber()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000149B5 File Offset: 0x00012BB5
		public ReadOnlyMemory<byte>? GetTimestampAuthorityName()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000149B5 File Offset: 0x00012BB5
		public static bool TryDecode(ReadOnlyMemory<byte> encodedBytes, out Rfc3161TimestampTokenInfo timestampTokenInfo, out int bytesConsumed)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool TryEncode(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
