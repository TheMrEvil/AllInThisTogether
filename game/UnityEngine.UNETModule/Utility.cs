using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking
{
	// Token: 0x0200000D RID: 13
	public class Utility
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003CB4 File Offset: 0x00001EB4
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00003C81 File Offset: 0x00001E81
		[Obsolete("This property is unused and should not be referenced in code.", true)]
		public static bool useRandomSourceID
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00002371 File Offset: 0x00000571
		private Utility()
		{
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003CC8 File Offset: 0x00001EC8
		public static SourceID GetSourceID()
		{
			return (SourceID)((long)SystemInfo.deviceUniqueIdentifier.GetHashCode());
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003C81 File Offset: 0x00001E81
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This function is unused and should not be referenced in code. Please sign in and setup your project in the editor instead.", true)]
		public static void SetAppID(AppID newAppID)
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003CE8 File Offset: 0x00001EE8
		[Obsolete("This function is unused and should not be referenced in code. Please sign in and setup your project in the editor instead.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public static AppID GetAppID()
		{
			return AppID.Invalid;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003CFC File Offset: 0x00001EFC
		public static void SetAccessTokenForNetwork(NetworkID netId, NetworkAccessToken accessToken)
		{
			bool flag = Utility.s_dictTokens.ContainsKey(netId);
			if (flag)
			{
				Utility.s_dictTokens.Remove(netId);
			}
			Utility.s_dictTokens.Add(netId, accessToken);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003D34 File Offset: 0x00001F34
		public static NetworkAccessToken GetAccessTokenForNetwork(NetworkID netId)
		{
			NetworkAccessToken result;
			bool flag = !Utility.s_dictTokens.TryGetValue(netId, out result);
			if (flag)
			{
				result = new NetworkAccessToken();
			}
			return result;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00003D63 File Offset: 0x00001F63
		// Note: this type is marked as 'beforefieldinit'.
		static Utility()
		{
		}

		// Token: 0x04000064 RID: 100
		private static Dictionary<NetworkID, NetworkAccessToken> s_dictTokens = new Dictionary<NetworkID, NetworkAccessToken>();
	}
}
