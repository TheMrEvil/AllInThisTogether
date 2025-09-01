using System;
using ExitGames.Client.Photon;

namespace Photon.Realtime
{
	// Token: 0x02000002 RID: 2
	[Serializable]
	public class AppSettings
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public bool IsMasterServerAddress
		{
			get
			{
				return !this.UseNameServer;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000205B File Offset: 0x0000025B
		public bool IsBestRegion
		{
			get
			{
				return this.UseNameServer && string.IsNullOrEmpty(this.FixedRegion);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002072 File Offset: 0x00000272
		public bool IsDefaultNameServer
		{
			get
			{
				return this.UseNameServer && string.IsNullOrEmpty(this.Server);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002089 File Offset: 0x00000289
		public bool IsDefaultPort
		{
			get
			{
				return this.Port <= 0;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002098 File Offset: 0x00000298
		public string ToStringFull()
		{
			return string.Format("appId {0}{1}{2}{3}use ns: {4}, reg: {5}, {9}, {6}{7}{8}auth: {10}", new object[]
			{
				string.IsNullOrEmpty(this.AppIdRealtime) ? string.Empty : ("Realtime/PUN: " + this.HideAppId(this.AppIdRealtime) + ", "),
				string.IsNullOrEmpty(this.AppIdFusion) ? string.Empty : ("Fusion: " + this.HideAppId(this.AppIdFusion) + ", "),
				string.IsNullOrEmpty(this.AppIdChat) ? string.Empty : ("Chat: " + this.HideAppId(this.AppIdChat) + ", "),
				string.IsNullOrEmpty(this.AppIdVoice) ? string.Empty : ("Voice: " + this.HideAppId(this.AppIdVoice) + ", "),
				string.IsNullOrEmpty(this.AppVersion) ? string.Empty : ("AppVersion: " + this.AppVersion + ", "),
				"UseNameServer: " + this.UseNameServer.ToString() + ", ",
				"Fixed Region: " + this.FixedRegion + ", ",
				string.IsNullOrEmpty(this.Server) ? string.Empty : ("Server: " + this.Server + ", "),
				this.IsDefaultPort ? string.Empty : ("Port: " + this.Port.ToString() + ", "),
				string.IsNullOrEmpty(this.ProxyServer) ? string.Empty : ("Proxy: " + this.ProxyServer + ", "),
				this.Protocol,
				this.AuthMode
			});
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002284 File Offset: 0x00000484
		public static bool IsAppId(string val)
		{
			try
			{
				new Guid(val);
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022B4 File Offset: 0x000004B4
		private string HideAppId(string appId)
		{
			if (!string.IsNullOrEmpty(appId) && appId.Length >= 8)
			{
				return appId.Substring(0, 8) + "***";
			}
			return appId;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000022DC File Offset: 0x000004DC
		public AppSettings CopyTo(AppSettings d)
		{
			d.AppIdRealtime = this.AppIdRealtime;
			d.AppIdFusion = this.AppIdFusion;
			d.AppIdChat = this.AppIdChat;
			d.AppIdVoice = this.AppIdVoice;
			d.AppVersion = this.AppVersion;
			d.UseNameServer = this.UseNameServer;
			d.FixedRegion = this.FixedRegion;
			d.BestRegionSummaryFromStorage = this.BestRegionSummaryFromStorage;
			d.Server = this.Server;
			d.Port = this.Port;
			d.ProxyServer = this.ProxyServer;
			d.Protocol = this.Protocol;
			d.AuthMode = this.AuthMode;
			d.EnableLobbyStatistics = this.EnableLobbyStatistics;
			d.NetworkLogging = this.NetworkLogging;
			d.EnableProtocolFallback = this.EnableProtocolFallback;
			return d;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000023AA File Offset: 0x000005AA
		public AppSettings()
		{
		}

		// Token: 0x04000001 RID: 1
		public string AppIdRealtime;

		// Token: 0x04000002 RID: 2
		public string AppIdFusion;

		// Token: 0x04000003 RID: 3
		public string AppIdChat;

		// Token: 0x04000004 RID: 4
		public string AppIdVoice;

		// Token: 0x04000005 RID: 5
		public string AppVersion;

		// Token: 0x04000006 RID: 6
		public bool UseNameServer = true;

		// Token: 0x04000007 RID: 7
		public string FixedRegion;

		// Token: 0x04000008 RID: 8
		[NonSerialized]
		public string BestRegionSummaryFromStorage;

		// Token: 0x04000009 RID: 9
		public string Server;

		// Token: 0x0400000A RID: 10
		public int Port;

		// Token: 0x0400000B RID: 11
		public string ProxyServer;

		// Token: 0x0400000C RID: 12
		public ConnectionProtocol Protocol;

		// Token: 0x0400000D RID: 13
		public bool EnableProtocolFallback = true;

		// Token: 0x0400000E RID: 14
		public AuthModeOption AuthMode;

		// Token: 0x0400000F RID: 15
		public bool EnableLobbyStatistics;

		// Token: 0x04000010 RID: 16
		public DebugLevel NetworkLogging = DebugLevel.ERROR;
	}
}
