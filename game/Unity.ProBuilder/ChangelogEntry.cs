using System;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	internal class ChangelogEntry
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003F8F File Offset: 0x0000218F
		public SemVer versionInfo
		{
			get
			{
				return this.m_VersionInfo;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003F97 File Offset: 0x00002197
		public string releaseNotes
		{
			get
			{
				return this.m_ReleaseNotes;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003F9F File Offset: 0x0000219F
		public ChangelogEntry(SemVer version, string releaseNotes)
		{
			this.m_VersionInfo = version;
			this.m_ReleaseNotes = releaseNotes;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003FB5 File Offset: 0x000021B5
		public override string ToString()
		{
			return this.m_VersionInfo.ToString() + "\n\n" + this.m_ReleaseNotes;
		}

		// Token: 0x04000039 RID: 57
		[SerializeField]
		private SemVer m_VersionInfo;

		// Token: 0x0400003A RID: 58
		[SerializeField]
		private string m_ReleaseNotes;
	}
}
