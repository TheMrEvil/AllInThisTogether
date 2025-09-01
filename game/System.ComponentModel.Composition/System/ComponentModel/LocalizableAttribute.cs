using System;
using System.Diagnostics;

namespace System.ComponentModel
{
	// Token: 0x0200001E RID: 30
	[Conditional("NOT_FEATURE_LEGACYCOMPONENTMODEL")]
	internal sealed class LocalizableAttribute : Attribute
	{
		// Token: 0x0600010D RID: 269 RVA: 0x00003F2F File Offset: 0x0000212F
		public LocalizableAttribute(bool isLocalizable)
		{
		}
	}
}
