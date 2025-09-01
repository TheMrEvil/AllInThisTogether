using System;
using UnityEngine.Events;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x0200038A RID: 906
	public interface IEditorPlayerConnection
	{
		// Token: 0x06001ECC RID: 7884
		void Register(Guid messageId, UnityAction<MessageEventArgs> callback);

		// Token: 0x06001ECD RID: 7885
		void Unregister(Guid messageId, UnityAction<MessageEventArgs> callback);

		// Token: 0x06001ECE RID: 7886
		void DisconnectAll();

		// Token: 0x06001ECF RID: 7887
		void RegisterConnection(UnityAction<int> callback);

		// Token: 0x06001ED0 RID: 7888
		void RegisterDisconnection(UnityAction<int> callback);

		// Token: 0x06001ED1 RID: 7889
		void UnregisterConnection(UnityAction<int> callback);

		// Token: 0x06001ED2 RID: 7890
		void UnregisterDisconnection(UnityAction<int> callback);

		// Token: 0x06001ED3 RID: 7891
		void Send(Guid messageId, byte[] data);

		// Token: 0x06001ED4 RID: 7892
		bool TrySend(Guid messageId, byte[] data);
	}
}
