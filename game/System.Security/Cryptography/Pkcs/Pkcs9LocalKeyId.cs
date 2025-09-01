using System;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x0200009A RID: 154
	public sealed class Pkcs9LocalKeyId : Pkcs9AttributeObject
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x000149B5 File Offset: 0x00012BB5
		public ReadOnlyMemory<byte> KeyId
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00014A7B File Offset: 0x00012C7B
		public Pkcs9LocalKeyId()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00014A7B File Offset: 0x00012C7B
		public Pkcs9LocalKeyId(byte[] keyId)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x00014A7B File Offset: 0x00012C7B
		public Pkcs9LocalKeyId(ReadOnlySpan<byte> keyId)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
