using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000075 RID: 117
	public class DocumentationInfo
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00011600 File Offset: 0x0000F800
		public static string version
		{
			get
			{
				return "12.1";
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00011607 File Offset: 0x0000F807
		public static string GetPageLink(string packageName, string pageName)
		{
			return string.Format("https://docs.unity3d.com/Packages/{0}@{1}/manual/{2}.html", packageName, DocumentationInfo.version, pageName);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001161A File Offset: 0x0000F81A
		public DocumentationInfo()
		{
		}

		// Token: 0x04000256 RID: 598
		private const string fallbackVersion = "12.1";

		// Token: 0x04000257 RID: 599
		private const string url = "https://docs.unity3d.com/Packages/{0}@{1}/manual/{2}.html";
	}
}
