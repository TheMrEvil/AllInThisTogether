using System;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000087 RID: 135
	internal sealed class KeyBuilder
	{
		// Token: 0x060002D3 RID: 723 RVA: 0x0000259F File Offset: 0x0000079F
		private KeyBuilder()
		{
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000EFFD File Offset: 0x0000D1FD
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

		// Token: 0x060002D5 RID: 725 RVA: 0x0000F018 File Offset: 0x0000D218
		public static byte[] Key(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000F038 File Offset: 0x0000D238
		public static byte[] IV(int size)
		{
			byte[] array = new byte[size];
			KeyBuilder.Rng.GetBytes(array);
			return array;
		}

		// Token: 0x04000ED6 RID: 3798
		private static RandomNumberGenerator rng;
	}
}
