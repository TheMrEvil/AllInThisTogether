using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Networking.PlayerConnection
{
	// Token: 0x02000388 RID: 904
	[MovedFrom("UnityEngine.Experimental.Networking.PlayerConnection")]
	public interface IConnectionState : IDisposable
	{
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001EC9 RID: 7881
		ConnectionTarget connectedToTarget { get; }

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001ECA RID: 7882
		string connectionName { get; }
	}
}
