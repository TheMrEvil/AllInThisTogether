using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;

namespace Photon.Realtime
{
	// Token: 0x02000018 RID: 24
	internal class InRoomCallbacksContainer : List<IInRoomCallbacks>, IInRoomCallbacks
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00006C60 File Offset: 0x00004E60
		public InRoomCallbacksContainer(LoadBalancingClient client)
		{
			this.client = client;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006C70 File Offset: 0x00004E70
		public void OnPlayerEnteredRoom(Player newPlayer)
		{
			this.client.UpdateCallbackTargets();
			foreach (IInRoomCallbacks inRoomCallbacks in this)
			{
				inRoomCallbacks.OnPlayerEnteredRoom(newPlayer);
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00006CC8 File Offset: 0x00004EC8
		public void OnPlayerLeftRoom(Player otherPlayer)
		{
			this.client.UpdateCallbackTargets();
			foreach (IInRoomCallbacks inRoomCallbacks in this)
			{
				inRoomCallbacks.OnPlayerLeftRoom(otherPlayer);
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00006D20 File Offset: 0x00004F20
		public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
		{
			this.client.UpdateCallbackTargets();
			foreach (IInRoomCallbacks inRoomCallbacks in this)
			{
				inRoomCallbacks.OnRoomPropertiesUpdate(propertiesThatChanged);
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00006D78 File Offset: 0x00004F78
		public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProp)
		{
			this.client.UpdateCallbackTargets();
			foreach (IInRoomCallbacks inRoomCallbacks in this)
			{
				inRoomCallbacks.OnPlayerPropertiesUpdate(targetPlayer, changedProp);
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006DD0 File Offset: 0x00004FD0
		public void OnMasterClientSwitched(Player newMasterClient)
		{
			this.client.UpdateCallbackTargets();
			foreach (IInRoomCallbacks inRoomCallbacks in this)
			{
				inRoomCallbacks.OnMasterClientSwitched(newMasterClient);
			}
		}

		// Token: 0x040000AC RID: 172
		private readonly LoadBalancingClient client;
	}
}
