using System;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000097 RID: 151
	public sealed class Pkcs12SecretBag : Pkcs12SafeBag
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x000149B5 File Offset: 0x00012BB5
		public ReadOnlyMemory<byte> SecretValue
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00014A34 File Offset: 0x00012C34
		internal Pkcs12SecretBag() : base(null, default(ReadOnlyMemory<byte>), false)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Oid GetSecretType()
		{
			throw new PlatformNotSupportedException();
		}
	}
}
