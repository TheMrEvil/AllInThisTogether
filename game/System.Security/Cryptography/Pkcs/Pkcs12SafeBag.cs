using System;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000094 RID: 148
	public abstract class Pkcs12SafeBag
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000149B5 File Offset: 0x00012BB5
		public CryptographicAttributeObjectCollection Attributes
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x000149B5 File Offset: 0x00012BB5
		public ReadOnlyMemory<byte> EncodedBagValue
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000149DF File Offset: 0x00012BDF
		protected Pkcs12SafeBag(string bagIdValue, ReadOnlyMemory<byte> encodedBagValue, bool skipCopy = false)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000149B5 File Offset: 0x00012BB5
		public byte[] Encode()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Oid GetBagId()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool TryEncode(Span<byte> destination, out int bytesWritten)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
