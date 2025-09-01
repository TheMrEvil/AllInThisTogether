using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the type of graphic shape to use on both ends of each dash in a dashed line.</summary>
	// Token: 0x02000132 RID: 306
	public enum DashCap
	{
		/// <summary>Specifies a square cap that squares off both ends of each dash.</summary>
		// Token: 0x04000AAD RID: 2733
		Flat,
		/// <summary>Specifies a circular cap that rounds off both ends of each dash.</summary>
		// Token: 0x04000AAE RID: 2734
		Round = 2,
		/// <summary>Specifies a triangular cap that points both ends of each dash.</summary>
		// Token: 0x04000AAF RID: 2735
		Triangle
	}
}
