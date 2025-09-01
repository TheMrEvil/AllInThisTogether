using System;

namespace ExitGames.Client.Photon.Encryption
{
	// Token: 0x02000047 RID: 71
	public interface IPhotonEncryptor
	{
		// Token: 0x060003AD RID: 941
		void Init(byte[] encryptionSecret, byte[] hmacSecret, byte[] ivBytes = null, bool chainingModeGCM = false, int mtu = 1200);

		// Token: 0x060003AE RID: 942
		void Encrypt2(byte[] data, int len, byte[] header, byte[] output, int outOffset, ref int outSize);

		// Token: 0x060003AF RID: 943
		byte[] Decrypt2(byte[] data, int offset, int len, byte[] header, out int outLen);

		// Token: 0x060003B0 RID: 944
		int CalculateEncryptedSize(int unencryptedSize);

		// Token: 0x060003B1 RID: 945
		int CalculateFragmentLength();
	}
}
