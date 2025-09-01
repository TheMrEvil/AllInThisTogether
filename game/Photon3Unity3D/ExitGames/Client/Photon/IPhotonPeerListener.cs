using System;

namespace ExitGames.Client.Photon
{
	// Token: 0x0200000B RID: 11
	public interface IPhotonPeerListener
	{
		// Token: 0x06000077 RID: 119
		void DebugReturn(DebugLevel level, string message);

		// Token: 0x06000078 RID: 120
		void OnOperationResponse(OperationResponse operationResponse);

		// Token: 0x06000079 RID: 121
		void OnStatusChanged(StatusCode statusCode);

		// Token: 0x0600007A RID: 122
		void OnEvent(EventData eventData);
	}
}
