using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000050 RID: 80
	public sealed class KeyBuilder
	{
		// Token: 0x06000319 RID: 793 RVA: 0x000106D1 File Offset: 0x0000E8D1
		private KeyBuilder()
		{
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600031A RID: 794 RVA: 0x000106D9 File Offset: 0x0000E8D9
		private static RandomNumberGenerator Rng
		{
			get
			{
				if (KeyBuilder.rng == null)
				{
					KeyBuilder.rng = RandomNumberGenerator.Create();
				}
				return KeyBuilder.rng;
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000106F4 File Offset: 0x0000E8F4
		public static byte[] Key(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00010714 File Offset: 0x0000E914
		public static byte[] IV(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x040002A4 RID: 676
		private static RandomNumberGenerator rng;
	}
}
