using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Steamworks
{
	// Token: 0x02000192 RID: 402
	[Serializable]
	[StructLayout(LayoutKind.Sequential, Pack = 4, Size = 372)]
	public class gameserveritem_t
	{
		// Token: 0x06000936 RID: 2358 RVA: 0x0000E17D File Offset: 0x0000C37D
		public string GetGameDir()
		{
			return Encoding.UTF8.GetString(this.m_szGameDir, 0, Array.IndexOf<byte>(this.m_szGameDir, 0));
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0000E19C File Offset: 0x0000C39C
		public void SetGameDir(string dir)
		{
			this.m_szGameDir = Encoding.UTF8.GetBytes(dir + "\0");
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0000E1B9 File Offset: 0x0000C3B9
		public string GetMap()
		{
			return Encoding.UTF8.GetString(this.m_szMap, 0, Array.IndexOf<byte>(this.m_szMap, 0));
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0000E1D8 File Offset: 0x0000C3D8
		public void SetMap(string map)
		{
			this.m_szMap = Encoding.UTF8.GetBytes(map + "\0");
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x0000E1F5 File Offset: 0x0000C3F5
		public string GetGameDescription()
		{
			return Encoding.UTF8.GetString(this.m_szGameDescription, 0, Array.IndexOf<byte>(this.m_szGameDescription, 0));
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x0000E214 File Offset: 0x0000C414
		public void SetGameDescription(string desc)
		{
			this.m_szGameDescription = Encoding.UTF8.GetBytes(desc + "\0");
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0000E231 File Offset: 0x0000C431
		public string GetServerName()
		{
			if (this.m_szServerName[0] == 0)
			{
				return this.m_NetAdr.GetConnectionAddressString();
			}
			return Encoding.UTF8.GetString(this.m_szServerName, 0, Array.IndexOf<byte>(this.m_szServerName, 0));
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0000E266 File Offset: 0x0000C466
		public void SetServerName(string name)
		{
			this.m_szServerName = Encoding.UTF8.GetBytes(name + "\0");
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0000E283 File Offset: 0x0000C483
		public string GetGameTags()
		{
			return Encoding.UTF8.GetString(this.m_szGameTags, 0, Array.IndexOf<byte>(this.m_szGameTags, 0));
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0000E2A2 File Offset: 0x0000C4A2
		public void SetGameTags(string tags)
		{
			this.m_szGameTags = Encoding.UTF8.GetBytes(tags + "\0");
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0000E2BF File Offset: 0x0000C4BF
		public gameserveritem_t()
		{
		}

		// Token: 0x04000A80 RID: 2688
		public servernetadr_t m_NetAdr;

		// Token: 0x04000A81 RID: 2689
		public int m_nPing;

		// Token: 0x04000A82 RID: 2690
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bHadSuccessfulResponse;

		// Token: 0x04000A83 RID: 2691
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bDoNotRefresh;

		// Token: 0x04000A84 RID: 2692
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		private byte[] m_szGameDir;

		// Token: 0x04000A85 RID: 2693
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
		private byte[] m_szMap;

		// Token: 0x04000A86 RID: 2694
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_szGameDescription;

		// Token: 0x04000A87 RID: 2695
		public uint m_nAppID;

		// Token: 0x04000A88 RID: 2696
		public int m_nPlayers;

		// Token: 0x04000A89 RID: 2697
		public int m_nMaxPlayers;

		// Token: 0x04000A8A RID: 2698
		public int m_nBotPlayers;

		// Token: 0x04000A8B RID: 2699
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bPassword;

		// Token: 0x04000A8C RID: 2700
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bSecure;

		// Token: 0x04000A8D RID: 2701
		public uint m_ulTimeLastPlayed;

		// Token: 0x04000A8E RID: 2702
		public int m_nServerVersion;

		// Token: 0x04000A8F RID: 2703
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		private byte[] m_szServerName;

		// Token: 0x04000A90 RID: 2704
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		private byte[] m_szGameTags;

		// Token: 0x04000A91 RID: 2705
		public CSteamID m_steamID;
	}
}
