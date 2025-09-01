using System;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

namespace Photon.Pun
{
	// Token: 0x0200001E RID: 30
	[HelpURL("https://doc.photonengine.com/en-us/pun/v2/getting-started/initial-setup")]
	[Serializable]
	public class ServerSettings : ScriptableObject
	{
		// Token: 0x0600016A RID: 362 RVA: 0x00008FE9 File Offset: 0x000071E9
		public void UseCloud(string cloudAppid, string code = "")
		{
			this.AppSettings.AppIdRealtime = cloudAppid;
			this.AppSettings.Server = null;
			this.AppSettings.FixedRegion = (string.IsNullOrEmpty(code) ? null : code);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000901C File Offset: 0x0000721C
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

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000904C File Offset: 0x0000724C
		public static string BestRegionSummaryInPreferences
		{
			get
			{
				return PhotonNetwork.BestRegionSummaryInPreferences;
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00009053 File Offset: 0x00007253
		public static void ResetBestRegionCodeInPreferences()
		{
			PhotonNetwork.BestRegionSummaryInPreferences = null;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000905B File Offset: 0x0000725B
		public override string ToString()
		{
			return "ServerSettings: " + this.AppSettings.ToStringFull();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00009072 File Offset: 0x00007272
		public ServerSettings()
		{
		}

		// Token: 0x040000BA RID: 186
		[Tooltip("Core Photon Server/Cloud settings.")]
		public AppSettings AppSettings;

		// Token: 0x040000BB RID: 187
		[Tooltip("Developer build override for Best Region.")]
		public string DevRegion;

		// Token: 0x040000BC RID: 188
		[Tooltip("Log output by PUN.")]
		public PunLogLevel PunLogging;

		// Token: 0x040000BD RID: 189
		[Tooltip("Logs additional info for debugging.")]
		public bool EnableSupportLogger;

		// Token: 0x040000BE RID: 190
		[Tooltip("Enables apps to keep the connection without focus.")]
		public bool RunInBackground = true;

		// Token: 0x040000BF RID: 191
		[Tooltip("Simulates an online connection.\nPUN can be used as usual.")]
		public bool StartInOfflineMode;

		// Token: 0x040000C0 RID: 192
		[Tooltip("RPC name list.\nUsed as shortcut when sending calls.")]
		public List<string> RpcList = new List<string>();
	}
}
