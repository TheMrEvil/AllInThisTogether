using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Steamworks.Data;

namespace Steamworks
{
	// Token: 0x0200008E RID: 142
	public class ConnectionManager
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000710 RID: 1808 RVA: 0x0000A930 File Offset: 0x00008B30
		// (set) Token: 0x06000711 RID: 1809 RVA: 0x0000A938 File Offset: 0x00008B38
		public IConnectionManager Interface
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000712 RID: 1810 RVA: 0x0000A941 File Offset: 0x00008B41
		// (set) Token: 0x06000713 RID: 1811 RVA: 0x0000A949 File Offset: 0x00008B49
		public ConnectionInfo ConnectionInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<ConnectionInfo>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<ConnectionInfo>k__BackingField = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000714 RID: 1812 RVA: 0x0000A952 File Offset: 0x00008B52
		// (set) Token: 0x06000715 RID: 1813 RVA: 0x0000A95F File Offset: 0x00008B5F
		public string ConnectionName
		{
			get
			{
				return this.Connection.ConnectionName;
			}
			set
			{
				this.Connection.ConnectionName = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0000A96E File Offset: 0x00008B6E
		// (set) Token: 0x06000717 RID: 1815 RVA: 0x0000A97B File Offset: 0x00008B7B
		public long UserData
		{
			get
			{
				return this.Connection.UserData;
			}
			set
			{
				this.Connection.UserData = value;
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0000A98A File Offset: 0x00008B8A
		public void Close()
		{
			this.Connection.Close(false, 0, "Closing Connection");
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0000A99F File Offset: 0x00008B9F
		public override string ToString()
		{
			return this.Connection.ToString();
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0000A9B4 File Offset: 0x00008BB4
		public virtual void OnConnectionChanged(ConnectionInfo info)
		{
			this.ConnectionInfo = info;
			switch (info.State)
			{
			case ConnectionState.None:
			case ConnectionState.ClosedByPeer:
			case ConnectionState.ProblemDetectedLocally:
				this.OnDisconnected(info);
				break;
			case ConnectionState.Connecting:
				this.OnConnecting(info);
				break;
			case ConnectionState.Connected:
				this.OnConnected(info);
				break;
			}
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0000AA12 File Offset: 0x00008C12
		public virtual void OnConnecting(ConnectionInfo info)
		{
			IConnectionManager @interface = this.Interface;
			if (@interface != null)
			{
				@interface.OnConnecting(info);
			}
			this.Connecting = true;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0000AA2F File Offset: 0x00008C2F
		public virtual void OnConnected(ConnectionInfo info)
		{
			IConnectionManager @interface = this.Interface;
			if (@interface != null)
			{
				@interface.OnConnected(info);
			}
			this.Connected = true;
			this.Connecting = false;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0000AA53 File Offset: 0x00008C53
		public virtual void OnDisconnected(ConnectionInfo info)
		{
			IConnectionManager @interface = this.Interface;
			if (@interface != null)
			{
				@interface.OnDisconnected(info);
			}
			this.Connected = false;
			this.Connecting = false;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0000AA78 File Offset: 0x00008C78
		public void Receive(int bufferSize = 32)
		{
			int num = 0;
			IntPtr intPtr = Marshal.AllocHGlobal(IntPtr.Size * bufferSize);
			try
			{
				num = SteamNetworkingSockets.Internal.ReceiveMessagesOnConnection(this.Connection, intPtr, bufferSize);
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

		// Token: 0x0600071F RID: 1823 RVA: 0x0000AB00 File Offset: 0x00008D00
		internal unsafe void ReceiveMessage(IntPtr msgPtr)
		{
			NetMsg netMsg = Marshal.PtrToStructure<NetMsg>(msgPtr);
			try
			{
				this.OnMessage(netMsg.DataPtr, netMsg.DataSize, netMsg.RecvTime, netMsg.MessageNumber, netMsg.Channel);
			}
			finally
			{
				NetMsg.InternalRelease((NetMsg*)((void*)msgPtr));
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0000AB60 File Offset: 0x00008D60
		public virtual void OnMessage(IntPtr data, int size, long messageNum, long recvTime, int channel)
		{
			IConnectionManager @interface = this.Interface;
			if (@interface != null)
			{
				@interface.OnMessage(data, size, messageNum, recvTime, channel);
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0000AB7C File Offset: 0x00008D7C
		public ConnectionManager()
		{
		}

		// Token: 0x040006D3 RID: 1747
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private IConnectionManager <Interface>k__BackingField;

		// Token: 0x040006D4 RID: 1748
		public Connection Connection;

		// Token: 0x040006D5 RID: 1749
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ConnectionInfo <ConnectionInfo>k__BackingField;

		// Token: 0x040006D6 RID: 1750
		public bool Connected = false;

		// Token: 0x040006D7 RID: 1751
		public bool Connecting = true;
	}
}
