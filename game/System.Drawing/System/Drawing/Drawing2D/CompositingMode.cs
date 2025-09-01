using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies how the source colors are combined with the background colors.</summary>
	// Token: 0x02000138 RID: 312
	public enum CompositingMode
	{
		/// <summary>Specifies that when a color is rendered, it is blended with the background color. The blend is determined by the alpha component of the color being rendered.</summary>
		// Token: 0x04000AC2 RID: 2754
		SourceOver,
		/// <summary>Specifies that when a color is rendered, it overwrites the background color.</summary>
		// Token: 0x04000AC3 RID: 2755
		SourceCopy
	}
}
