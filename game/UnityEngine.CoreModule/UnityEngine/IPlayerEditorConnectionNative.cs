using System;

namespace UnityEngine
{
	// Token: 0x020001CF RID: 463
	internal interface IPlayerEditorConnectionNative
	{
		// Token: 0x060015A5 RID: 5541
		void Initialize();

		// Token: 0x060015A6 RID: 5542
		void DisconnectAll();

		// Token: 0x060015A7 RID: 5543
		void SendMessage(Guid messageId, byte[] data, int playerId);

		// Token: 0x060015A8 RID: 5544
		bool TrySendMessage(Guid messageId, byte[] data, int playerId);

		// Token: 0x060015A9 RID: 5545
		void Poll();

		// Token: 0x060015AA RID: 5546
		void RegisterInternal(Guid messageId);

		// Token: 0x060015AB RID: 5547
		void UnregisterInternal(Guid messageId);

		// Token: 0x060015AC RID: 5548
		bool IsConnected();
	}
}
