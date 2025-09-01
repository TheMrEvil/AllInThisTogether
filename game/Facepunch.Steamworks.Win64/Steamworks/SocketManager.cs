using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x02000091 RID: 145
	public class SocketManager
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600072A RID: 1834 RVA: 0x0000AB93 File Offset: 0x00008D93
		// (set) Token: 0x0600072B RID: 1835 RVA: 0x0000AB9B File Offset: 0x00008D9B
		public ISocketManager Interface
		{
			[CompilerGenerated]
			get
			{
				return this.<Interface>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Interface>k__BackingField = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x0000ABA4 File Offset: 0x00008DA4
		// (set) Token: 0x0600072D RID: 1837 RVA: 0x0000ABAC File Offset: 0x00008DAC
		public Socket Socket
		{
			[CompilerGenerated]
			get
			{
				return this.<Socket>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Socket>k__BackingField = value;
			}
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0000ABB8 File Offset: 0x00008DB8
		public override string ToString()
		{
			return this.Socket.ToString();
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0000ABD9 File Offset: 0x00008DD9
		internal void Initialize()
		{
			this.pollGroup = SteamNetworkingSockets.Internal.CreatePollGroup();
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0000ABEC File Offset: 0x00008DEC
		public bool Close()
		{
			bool isValid = SteamNetworkingSockets.Internal.IsValid;
			if (isValid)
			{
				SteamNetworkingSockets.Internal.DestroyPollGroup(this.pollGroup);
				this.Socket.Close();
			}
			this.pollGroup = 0U;
			this.Socket = 0U;
			return true;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0000AC48 File Offset: 0x00008E48
		public virtual void OnConnectionChanged(Connection connection, ConnectionInfo info)
		{
			switch (info.State)
			{
			case ConnectionState.None:
			case ConnectionState.ClosedByPeer:
			case ConnectionState.ProblemDetectedLocally:
			{
				bool flag = this.Connecting.Contains(connection) || this.Connected.Contains(connection);
				if (flag)
				{
					this.OnDisconnected(connection, info);
				}
				break;
			}
			case ConnectionState.Connecting:
			{
				bool flag2 = !this.Connecting.Contains(connection);
				if (flag2)
				{
					this.Connecting.Add(connection);
					this.OnConnecting(connection, info);
				}
				break;
			}
			case ConnectionState.Connected:
			{
				bool flag3 = !this.Connected.Contains(connection);
				if (flag3)
				{
					this.Connecting.Remove(connection);
					this.Connected.Add(connection);
					this.OnConnected(connection, info);
				}
				break;
			}
			}
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0000AD1C File Offset: 0x00008F1C
		public virtual void OnConnecting(Connection connection, ConnectionInfo info)
		{
			bool flag = this.Interface != null;
			if (flag)
			{
				this.Interface.OnConnecting(connection, info);
			}
			else
			{
				connection.Accept();
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0000AD52 File Offset: 0x00008F52
		public virtual void OnConnected(Connection connection, ConnectionInfo info)
		{
			SteamNetworkingSockets.Internal.SetConnectionPollGroup(connection, this.pollGroup);
			ISocketManager @interface = this.Interface;
			if (@interface != null)
			{
				@interface.OnConnected(connection, info);
			}
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0000AD7C File Offset: 0x00008F7C
		public virtual void OnDisconnected(Connection connection, ConnectionInfo info)
		{
			SteamNetworkingSockets.Internal.SetConnectionPollGroup(connection, 0U);
			connection.Close(false, 0, "Closing Connection");
			this.Connecting.Remove(connection);
			this.Connected.Remove(connection);
			ISocketManager @interface = this.Interface;
			if (@interface != null)
			{
				@interface.OnDisconnected(connection, info);
			}
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0000ADDC File Offset: 0x00008FDC
		public void Receive(int bufferSize = 32)
		{
			int num = 0;
			IntPtr intPtr = Marshal.AllocHGlobal(IntPtr.Size * bufferSize);
			try
			{
				num = SteamNetworkingSockets.Internal.ReceiveMessagesOnPollGroup(this.pollGroup, intPtr, bufferSize);
				for (int i = 0; i < num; i++)
				{
					this.ReceiveMessage(Marshal.ReadIntPtr(intPtr, i * IntPtr.Size));
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			bool flag = num == bufferSize;
			if (flag)
			{
				this.Receive(bufferSize);
			}
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0000AE64 File Offset: 0x00009064
		internal unsafe void ReceiveMessage(IntPtr msgPtr)
		{
			NetMsg netMsg = Marshal.PtrToStructure<NetMsg>(msgPtr);
			try
			{
				this.OnMessage(netMsg.Connection, netMsg.Identity, netMsg.DataPtr, netMsg.DataSize, netMsg.RecvTime, netMsg.MessageNumber, netMsg.Channel);
			}
			finally
			{
				NetMsg.InternalRelease((NetMsg*)((void*)msgPtr));
			}
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0000AED0 File Offset: 0x000090D0
		public virtual void OnMessage(Connection connection, NetIdentity identity, IntPtr data, int size, long messageNum, long recvTime, int channel)
		{
			ISocketManager @interface = this.Interface;
			if (@interface != null)
			{
				@interface.OnMessage(connection, identity, data, size, messageNum, recvTime, channel);
			}
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0000AEF0 File Offset: 0x000090F0
		public SocketManager()
		{
		}

		// Token: 0x040006D8 RID: 1752
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ISocketManager <Interface>k__BackingField;

		// Token: 0x040006D9 RID: 1753
		public List<Connection> Connecting = new List<Connection>();

		// Token: 0x040006DA RID: 1754
		public List<Connection> Connected = new List<Connection>();

		// Token: 0x040006DB RID: 1755
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Socket <Socket>k__BackingField;

		// Token: 0x040006DC RID: 1756
		internal HSteamNetPollGroup pollGroup;
	}
}
