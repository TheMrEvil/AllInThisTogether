using System;
using System.Net;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Mono.Security.Interface
{
	// Token: 0x02000042 RID: 66
	public interface IMonoSslStream : IDisposable
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600026B RID: 619
		SslStream SslStream { get; }

		// Token: 0x0600026C RID: 620
		Task AuthenticateAsClientAsync(string targetHost, X509CertificateCollection clientCertificates, SslProtocols enabledSslProtocols, bool checkCertificateRevocation);

		// Token: 0x0600026D RID: 621
		Task AuthenticateAsServerAsync(X509Certificate serverCertificate, bool clientCertificateRequired, SslProtocols enabledSslProtocols, bool checkCertificateRevocation);

		// Token: 0x0600026E RID: 622
		Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken);

		// Token: 0x0600026F RID: 623
		Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken);

		// Token: 0x06000270 RID: 624
		Task ShutdownAsync();

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000271 RID: 625
		TransportContext TransportContext { get; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000272 RID: 626
		bool IsAuthenticated { get; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000273 RID: 627
		bool IsMutuallyAuthenticated { get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000274 RID: 628
		bool IsEncrypted { get; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000275 RID: 629
		bool IsSigned { get; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000276 RID: 630
		bool IsServer { get; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000277 RID: 631
		CipherAlgorithmType CipherAlgorithm { get; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000278 RID: 632
		int CipherStrength { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000279 RID: 633
		HashAlgorithmType HashAlgorithm { get; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600027A RID: 634
		int HashStrength { get; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600027B RID: 635
		ExchangeAlgorithmType KeyExchangeAlgorithm { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600027C RID: 636
		int KeyExchangeStrength { get; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600027D RID: 637
		bool CanRead { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600027E RID: 638
		bool CanTimeout { get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600027F RID: 639
		bool CanWrite { get; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000280 RID: 640
		long Length { get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000281 RID: 641
		long Position { get; }

		// Token: 0x06000282 RID: 642
		void SetLength(long value);

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000283 RID: 643
		AuthenticatedStream AuthenticatedStream { get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000284 RID: 644
		// (set) Token: 0x06000285 RID: 645
		int ReadTimeout { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000286 RID: 646
		// (set) Token: 0x06000287 RID: 647
		int WriteTimeout { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000288 RID: 648
		bool CheckCertRevocationStatus { get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000289 RID: 649
		X509Certificate InternalLocalCertificate { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600028A RID: 650
		X509Certificate LocalCertificate { get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600028B RID: 651
		X509Certificate RemoteCertificate { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600028C RID: 652
		SslProtocols SslProtocol { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600028D RID: 653
		MonoTlsProvider Provider { get; }

		// Token: 0x0600028E RID: 654
		MonoTlsConnectionInfo GetConnectionInfo();

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600028F RID: 655
		bool CanRenegotiate { get; }

		// Token: 0x06000290 RID: 656
		Task RenegotiateAsync(CancellationToken cancellationToken);
	}
}
