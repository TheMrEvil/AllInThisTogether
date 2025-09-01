using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000019 RID: 25
	[DebuggerDisplay("Script = {tag},  Language Count = {languages.Count}")]
	[Serializable]
	internal struct OpenTypeLayoutScript
	{
		// Token: 0x040000B2 RID: 178
		public string tag;

		// Token: 0x040000B3 RID: 179
		public List<OpenTypeLayoutLanguage> languages;
	}
}
