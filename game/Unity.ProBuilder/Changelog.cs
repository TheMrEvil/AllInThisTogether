using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;

namespace UnityEngine.ProBuilder
{
	// Token: 0x0200000C RID: 12
	[Serializable]
	internal class Changelog
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003FD2 File Offset: 0x000021D2
		public ReadOnlyCollection<ChangelogEntry> entries
		{
			get
			{
				return new ReadOnlyCollection<ChangelogEntry>(this.m_Entries);
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003FE0 File Offset: 0x000021E0
		public Changelog(string log)
		{
			string version = string.Empty;
			StringBuilder stringBuilder = null;
			this.m_Entries = new List<ChangelogEntry>();
			ChangelogEntry item;
			foreach (string text in log.Split('\n', StringSplitOptions.None))
			{
				if (Regex.Match(text, "(##\\s\\[[0-9]+\\.[0-9]+\\.[0-9]+(\\-[a-zA-Z]+(\\.[0-9]+)*)*\\])").Success)
				{
					if ((item = this.CreateEntry(version, (stringBuilder != null) ? stringBuilder.ToString() : "")) != null)
					{
						this.m_Entries.Add(item);
					}
					version = text;
					stringBuilder = new StringBuilder();
				}
				else if (stringBuilder != null)
				{
					stringBuilder.AppendLine(text);
				}
			}
			if ((item = this.CreateEntry(version, stringBuilder.ToString())) != null)
			{
				this.m_Entries.Add(item);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00004098 File Offset: 0x00002298
		private ChangelogEntry CreateEntry(string version, string contents)
		{
			Match match = Regex.Match(version, "(?<=##\\s\\[).*(?=\\])");
			Match match2 = Regex.Match(version, "(?<=##\\s\\[.*\\]\\s-\\s)[0-9-]*");
			if (match.Success)
			{
				return new ChangelogEntry(new SemVer(match.Value, match2.Value), contents.Trim());
			}
			return null;
		}

		// Token: 0x0400003B RID: 59
		private const string k_ChangelogEntryPattern = "(##\\s\\[[0-9]+\\.[0-9]+\\.[0-9]+(\\-[a-zA-Z]+(\\.[0-9]+)*)*\\])";

		// Token: 0x0400003C RID: 60
		private const string k_VersionInfoPattern = "(?<=##\\s\\[).*(?=\\])";

		// Token: 0x0400003D RID: 61
		private const string k_VersionDatePattern = "(?<=##\\s\\[.*\\]\\s-\\s)[0-9-]*";

		// Token: 0x0400003E RID: 62
		[SerializeField]
		private List<ChangelogEntry> m_Entries;
	}
}
