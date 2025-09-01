using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies whether smoothing (antialiasing) is applied to lines and curves and the edges of filled areas.</summary>
	// Token: 0x02000151 RID: 337
	public enum SmoothingMode
	{
		/// <summary>Specifies an invalid mode.</summary>
		// Token: 0x04000B64 RID: 2916
		Invalid = -1,
		/// <summary>Specifies no antialiasing.</summary>
		// Token: 0x04000B65 RID: 2917
		Default,
		/// <summary>Specifies no antialiasing.</summary>
		// Token: 0x04000B66 RID: 2918
		HighSpeed,
		/// <summary>Specifies antialiased rendering.</summary>
		// Token: 0x04000B67 RID: 2919
		HighQuality,
		/// <summary>Specifies no antialiasing.</summary>
		// Token: 0x04000B68 RID: 2920
		None,
		/// <summary>Specifies antialiased rendering.</summary>
		// Token: 0x04000B69 RID: 2921
		AntiAlias
	}
}
