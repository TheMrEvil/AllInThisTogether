using System;
using System.ComponentModel;
using UnityEngine.Networking.PlayerConnection;

namespace UnityEngine.Diagnostics
{
	// Token: 0x02000450 RID: 1104
	public static class PlayerConnection
	{
		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x060026F6 RID: 9974 RVA: 0x00041138 File Offset: 0x0003F338
		[Obsolete("Use UnityEngine.Networking.PlayerConnection.PlayerConnection.instance.isConnected instead.")]
		public static bool connected
		{
			get
			{
				return PlayerConnection.instance.isConnected;
			}
		}

		// Token: 0x060026F7 RID: 9975 RVA: 0x00004563 File Offset: 0x00002763
		[Obsolete("PlayerConnection.SendFile is no longer supported.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static void SendFile(string remoteFilePath, byte[] data)
		{
		}
	}
}
