using System;

namespace Photon.SocketServer.Security
{
	// Token: 0x0200004A RID: 74
	internal interface ICryptoProvider : IDisposable
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060003C9 RID: 969
		bool IsInitialized { get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060003CA RID: 970
		byte[] PublicKey { get; }

		// Token: 0x060003CB RID: 971
		void DeriveSharedKey(byte[] otherPartyPublicKey);

		// Token: 0x060003CC RID: 972
		byte[] Encrypt(byte[] data);

		// Token: 0x060003CD RID: 973
		byte[] Encrypt(byte[] data, int offset, int count);

		// Token: 0x060003CE RID: 974
		byte[] Decrypt(byte[] data);

		// Token: 0x060003CF RID: 975
		byte[] Decrypt(byte[] data, int offset, int count);
	}
}
