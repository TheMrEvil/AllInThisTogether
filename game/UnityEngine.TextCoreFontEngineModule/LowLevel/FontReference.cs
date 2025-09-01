using System;
using System.Diagnostics;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x0200000C RID: 12
	[DebuggerDisplay("{familyName} - {styleName}")]
	[UsedByNativeCode]
	internal struct FontReference
	{
		// Token: 0x0400006A RID: 106
		public string familyName;

		// Token: 0x0400006B RID: 107
		public string styleName;

		// Token: 0x0400006C RID: 108
		public int faceIndex;

		// Token: 0x0400006D RID: 109
		public string filePath;
	}
}
