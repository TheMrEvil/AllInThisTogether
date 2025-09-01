using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x0200008F RID: 143
	public sealed class Pkcs12CertBag : Pkcs12SafeBag
	{
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x000149B5 File Offset: 0x00012BB5
		public ReadOnlyMemory<byte> EncodedCertificate
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool IsX509Certificate
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x000149BC File Offset: 0x00012BBC
		public Pkcs12CertBag(Oid certificateType, ReadOnlyMemory<byte> encodedCertificate) : base(null, default(ReadOnlyMemory<byte>), false)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000149B5 File Offset: 0x00012BB5
		public X509Certificate2 GetCertificate()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Oid GetCertificateType()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
