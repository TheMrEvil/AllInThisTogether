using System;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000093 RID: 147
	public sealed class Pkcs12KeyBag : Pkcs12SafeBag
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x000149B5 File Offset: 0x00012BB5
		public ReadOnlyMemory<byte> Pkcs8PrivateKey
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x000149EC File Offset: 0x00012BEC
		public Pkcs12KeyBag(ReadOnlyMemory<byte> pkcs8PrivateKey, bool skipCopy = false) : base(null, default(ReadOnlyMemory<byte>), false)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
