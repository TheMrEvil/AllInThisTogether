using System;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000098 RID: 152
	public sealed class Pkcs12ShroudedKeyBag : Pkcs12SafeBag
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x000149B5 File Offset: 0x00012BB5
		public ReadOnlyMemory<byte> EncryptedPkcs8PrivateKey
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00014A58 File Offset: 0x00012C58
		public Pkcs12ShroudedKeyBag(ReadOnlyMemory<byte> encryptedPkcs8PrivateKey, bool skipCopy = false) : base(null, default(ReadOnlyMemory<byte>), false)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
