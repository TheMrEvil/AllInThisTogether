using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000052 RID: 82
	[Flags]
	internal enum VersionChangeType
	{
		// Token: 0x040000E2 RID: 226
		Bindings = 1,
		// Token: 0x040000E3 RID: 227
		ViewData = 2,
		// Token: 0x040000E4 RID: 228
		Hierarchy = 4,
		// Token: 0x040000E5 RID: 229
		Layout = 8,
		// Token: 0x040000E6 RID: 230
		StyleSheet = 16,
		// Token: 0x040000E7 RID: 231
		Styles = 32,
		// Token: 0x040000E8 RID: 232
		Overflow = 64,
		// Token: 0x040000E9 RID: 233
		BorderRadius = 128,
		// Token: 0x040000EA RID: 234
		BorderWidth = 256,
		// Token: 0x040000EB RID: 235
		Transform = 512,
		// Token: 0x040000EC RID: 236
		Size = 1024,
		// Token: 0x040000ED RID: 237
		Repaint = 2048,
		// Token: 0x040000EE RID: 238
		Opacity = 4096,
		// Token: 0x040000EF RID: 239
		Color = 8192,
		// Token: 0x040000F0 RID: 240
		RenderHints = 16384,
		// Token: 0x040000F1 RID: 241
		TransitionProperty = 32768,
		// Token: 0x040000F2 RID: 242
		Picking = 1048576
	}
}
