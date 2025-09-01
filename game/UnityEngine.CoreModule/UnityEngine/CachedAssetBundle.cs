using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000EF RID: 239
	[UsedByNativeCode]
	public struct CachedAssetBundle
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x00006FAE File Offset: 0x000051AE
		public CachedAssetBundle(string name, Hash128 hash)
		{
			this.m_Name = name;
			this.m_Hash = hash;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00006FC0 File Offset: 0x000051C0
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x00006FD8 File Offset: 0x000051D8
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00006FE4 File Offset: 0x000051E4
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x00006FFC File Offset: 0x000051FC
		public Hash128 hash
		{
			get
			{
				return this.m_Hash;
			}
			set
			{
				this.m_Hash = value;
			}
		}

		// Token: 0x04000327 RID: 807
		private string m_Name;

		// Token: 0x04000328 RID: 808
		private Hash128 m_Hash;
	}
}
