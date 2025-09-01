using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the type of fill a <see cref="T:System.Drawing.Pen" /> object uses to fill lines.</summary>
	// Token: 0x0200014C RID: 332
	public enum PenType
	{
		/// <summary>Specifies a solid fill.</summary>
		// Token: 0x04000B51 RID: 2897
		SolidColor,
		/// <summary>Specifies a hatch fill.</summary>
		// Token: 0x04000B52 RID: 2898
		HatchFill,
		/// <summary>Specifies a bitmap texture fill.</summary>
		// Token: 0x04000B53 RID: 2899
		TextureFill,
		/// <summary>Specifies a path gradient fill.</summary>
		// Token: 0x04000B54 RID: 2900
		PathGradient,
		/// <summary>Specifies a linear gradient fill.</summary>
		// Token: 0x04000B55 RID: 2901
		LinearGradient
	}
}
