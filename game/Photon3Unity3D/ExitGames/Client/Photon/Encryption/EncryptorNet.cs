using System;
using System.Security.Cryptography;

namespace ExitGames.Client.Photon.Encryption
{
	// Token: 0x02000048 RID: 72
	public class EncryptorNet : IPhotonEncryptor
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x0001BD7C File Offset: 0x00019F7C
		public void Init(byte[] encryptionSecret, byte[] hmacSecret, byte[] ivBytes = null, bool chainingModeGCM = false, int mtu = 1200)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0001BD84 File Offset: 0x00019F84
		public void Encrypt2(byte[] data, int len, byte[] header, byte[] output, int outOffset, ref int outSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001BD8C File Offset: 0x00019F8C
		public byte[] Decrypt2(byte[] data, int offset, int len, byte[] header, out int outLen)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001BD94 File Offset: 0x00019F94
		public int CalculateEncryptedSize(int unencryptedSize)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001BD9C File Offset: 0x00019F9C
		public int CalculateFragmentLength()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001BDA4 File Offset: 0x00019FA4
		public EncryptorNet()
		{
		}

		// Token: 0x040001FA RID: 506
		protected Aes encryptorIn;

		// Token: 0x040001FB RID: 507
		protected Aes encryptorOut;

		// Token: 0x040001FC RID: 508
		protected HMACSHA256 hmacsha256In;

		// Token: 0x040001FD RID: 509
		protected HMACSHA256 hmacsha256Out;
	}
}
