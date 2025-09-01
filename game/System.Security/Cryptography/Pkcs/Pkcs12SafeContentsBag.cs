using System;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000096 RID: 150
	public sealed class Pkcs12SafeContentsBag : Pkcs12SafeBag
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Pkcs12SafeContents SafeContents
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00014A10 File Offset: 0x00012C10
		internal Pkcs12SafeContentsBag() : base(null, default(ReadOnlyMemory<byte>), false)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
