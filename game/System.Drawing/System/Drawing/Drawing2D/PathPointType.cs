using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the type of point in a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object.</summary>
	// Token: 0x0200014A RID: 330
	public enum PathPointType
	{
		/// <summary>The starting point of a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object.</summary>
		// Token: 0x04000B42 RID: 2882
		Start,
		/// <summary>A line segment.</summary>
		// Token: 0x04000B43 RID: 2883
		Line,
		/// <summary>A default Bézier curve.</summary>
		// Token: 0x04000B44 RID: 2884
		Bezier = 3,
		/// <summary>A mask point.</summary>
		// Token: 0x04000B45 RID: 2885
		PathTypeMask = 7,
		/// <summary>The corresponding segment is dashed.</summary>
		// Token: 0x04000B46 RID: 2886
		DashMode = 16,
		/// <summary>A path marker.</summary>
		// Token: 0x04000B47 RID: 2887
		PathMarker = 32,
		/// <summary>The endpoint of a subpath.</summary>
		// Token: 0x04000B48 RID: 2888
		CloseSubpath = 128,
		/// <summary>A cubic Bézier curve.</summary>
		// Token: 0x04000B49 RID: 2889
		Bezier3 = 3
	}
}
