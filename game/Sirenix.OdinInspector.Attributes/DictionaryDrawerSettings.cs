using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200000F RID: 15
	[Conditional("UNITY_EDITOR")]
	public sealed class DictionaryDrawerSettings : Attribute
	{
		// Token: 0x06000044 RID: 68 RVA: 0x0000252F File Offset: 0x0000072F
		public DictionaryDrawerSettings()
		{
		}

		// Token: 0x0400003B RID: 59
		public string KeyLabel = "Key";

		// Token: 0x0400003C RID: 60
		public string ValueLabel = "Value";

		// Token: 0x0400003D RID: 61
		public DictionaryDisplayOptions DisplayMode;

		// Token: 0x0400003E RID: 62
		public bool IsReadOnly;

		// Token: 0x0400003F RID: 63
		public float KeyColumnWidth = 130f;
	}
}
