using System;

namespace UnityEngine
{
	// Token: 0x02000022 RID: 34
	public sealed class GUILayoutOption
	{
		// Token: 0x0600024B RID: 587 RVA: 0x000095BB File Offset: 0x000077BB
		internal GUILayoutOption(GUILayoutOption.Type type, object value)
		{
			this.type = type;
			this.value = value;
		}

		// Token: 0x04000082 RID: 130
		internal GUILayoutOption.Type type;

		// Token: 0x04000083 RID: 131
		internal object value;

		// Token: 0x02000023 RID: 35
		internal enum Type
		{
			// Token: 0x04000085 RID: 133
			fixedWidth,
			// Token: 0x04000086 RID: 134
			fixedHeight,
			// Token: 0x04000087 RID: 135
			minWidth,
			// Token: 0x04000088 RID: 136
			maxWidth,
			// Token: 0x04000089 RID: 137
			minHeight,
			// Token: 0x0400008A RID: 138
			maxHeight,
			// Token: 0x0400008B RID: 139
			stretchWidth,
			// Token: 0x0400008C RID: 140
			stretchHeight,
			// Token: 0x0400008D RID: 141
			alignStart,
			// Token: 0x0400008E RID: 142
			alignMiddle,
			// Token: 0x0400008F RID: 143
			alignEnd,
			// Token: 0x04000090 RID: 144
			alignJustify,
			// Token: 0x04000091 RID: 145
			equalSize,
			// Token: 0x04000092 RID: 146
			spacing
		}
	}
}
