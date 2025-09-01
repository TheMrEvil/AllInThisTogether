using System;

namespace ExitGames.Client.Photon
{
	// Token: 0x02000012 RID: 18
	public interface ITrafficRecorder
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B7 RID: 183
		// (set) Token: 0x060000B8 RID: 184
		bool Enabled { get; set; }

		// Token: 0x060000B9 RID: 185
		void Record(byte[] inBuffer, int length, bool incoming, short peerId, IPhotonSocket connection);
	}
}
