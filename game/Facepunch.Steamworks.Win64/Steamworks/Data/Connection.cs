using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Steamworks.Data
{
	// Token: 0x020001E8 RID: 488
	public struct Connection
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x000194D8 File Offset: 0x000176D8
		// (set) Token: 0x06000F8D RID: 3981 RVA: 0x000194E0 File Offset: 0x000176E0
		public uint Id
		{
			[CompilerGenerated]
			readonly get
			{
				return this.<Id>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Id>k__BackingField = value;
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x000194EC File Offset: 0x000176EC
		public override string ToString()
		{
			return this.Id.ToString();
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00019508 File Offset: 0x00017708
		public static implicit operator Connection(uint value)
		{
			return new Connection
			{
				Id = value
			};
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00019527 File Offset: 0x00017727
		public static implicit operator uint(Connection value)
		{
			return value.Id;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00019530 File Offset: 0x00017730
		public Result Accept()
		{
			return SteamNetworkingSockets.Internal.AcceptConnection(this);
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00019554 File Offset: 0x00017754
		public bool Close(bool linger = false, int reasonCode = 0, string debugString = "Closing Connection")
		{
			return SteamNetworkingSockets.Internal.CloseConnection(this, reasonCode, debugString, linger);
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00019579 File Offset: 0x00017779
		// (set) Token: 0x06000F94 RID: 3988 RVA: 0x0001958B File Offset: 0x0001778B
		public long UserData
		{
			get
			{
				return SteamNetworkingSockets.Internal.GetConnectionUserData(this);
			}
			set
			{
				SteamNetworkingSockets.Internal.SetConnectionUserData(this, value);
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x000195A0 File Offset: 0x000177A0
		// (set) Token: 0x06000F96 RID: 3990 RVA: 0x000195D4 File Offset: 0x000177D4
		public string ConnectionName
		{
			get
			{
				string text;
				bool flag = !SteamNetworkingSockets.Internal.GetConnectionName(this, out text);
				string result;
				if (flag)
				{
					result = "ERROR";
				}
				else
				{
					result = text;
				}
				return result;
			}
			set
			{
				SteamNetworkingSockets.Internal.SetConnectionName(this, value);
			}
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x000195E8 File Offset: 0x000177E8
		public Result SendMessage(IntPtr ptr, int size, SendType sendType = SendType.Reliable)
		{
			long num = 0L;
			return SteamNetworkingSockets.Internal.SendMessageToConnection(this, ptr, (uint)size, (int)sendType, ref num);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00019614 File Offset: 0x00017814
		public unsafe Result SendMessage(byte[] data, SendType sendType = SendType.Reliable)
		{
			byte* value;
			if (data == null || data.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &data[0];
			}
			return this.SendMessage((IntPtr)((void*)value), data.Length, sendType);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x00019650 File Offset: 0x00017850
		public unsafe Result SendMessage(byte[] data, int offset, int length, SendType sendType = SendType.Reliable)
		{
			byte* value;
			if (data == null || data.Length == 0)
			{
				value = null;
			}
			else
			{
				value = &data[0];
			}
			return this.SendMessage((IntPtr)((void*)value) + offset, length, sendType);
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00019690 File Offset: 0x00017890
		public Result SendMessage(string str, SendType sendType = SendType.Reliable)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			return this.SendMessage(bytes, sendType);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x000196B6 File Offset: 0x000178B6
		public Result Flush()
		{
			return SteamNetworkingSockets.Internal.FlushMessagesOnConnection(this);
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x000196C8 File Offset: 0x000178C8
		public string DetailedStatus()
		{
			string text;
			bool flag = SteamNetworkingSockets.Internal.GetDetailedConnectionStatus(this, out text) != 0;
			string result;
			if (flag)
			{
				result = null;
			}
			else
			{
				result = text;
			}
			return result;
		}

		// Token: 0x04000BD7 RID: 3031
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private uint <Id>k__BackingField;
	}
}
