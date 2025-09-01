using System;
using ExitGames.Client.Photon;

namespace Photon.Realtime
{
	// Token: 0x02000012 RID: 18
	public interface IInRoomCallbacks
	{
		// Token: 0x060000CF RID: 207
		void OnPlayerEnteredRoom(Player newPlayer);

		// Token: 0x060000D0 RID: 208
		void OnPlayerLeftRoom(Player otherPlayer);

		// Token: 0x060000D1 RID: 209
		void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged);

		// Token: 0x060000D2 RID: 210
		void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps);

		// Token: 0x060000D3 RID: 211
		void OnMasterClientSwitched(Player newMasterClient);
	}
}
