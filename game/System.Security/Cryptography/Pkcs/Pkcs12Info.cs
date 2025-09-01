using System;
using System.Collections.ObjectModel;

namespace System.Security.Cryptography.Pkcs
{
	// Token: 0x02000091 RID: 145
	public sealed class Pkcs12Info
	{
		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x000149B5 File Offset: 0x00012BB5
		public ReadOnlyCollection<Pkcs12SafeContents> AuthenticatedSafe
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x000149B5 File Offset: 0x00012BB5
		public Pkcs12IntegrityMode IntegrityMode
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000149DF File Offset: 0x00012BDF
		internal Pkcs12Info()
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000149B5 File Offset: 0x00012BB5
		public static Pkcs12Info Decode(ReadOnlyMemory<byte> encodedBytes, out int bytesConsumed, bool skipCopy = false)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool VerifyMac(ReadOnlySpan<char> password)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x000149B5 File Offset: 0x00012BB5
		public bool VerifyMac(string password)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
