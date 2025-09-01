using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies how different clipping regions can be combined.</summary>
	// Token: 0x02000137 RID: 311
	public enum CombineMode
	{
		/// <summary>One clipping region is replaced by another.</summary>
		// Token: 0x04000ABB RID: 2747
		Replace,
		/// <summary>Two clipping regions are combined by taking their intersection.</summary>
		// Token: 0x04000ABC RID: 2748
		Intersect,
		/// <summary>Two clipping regions are combined by taking the union of both.</summary>
		// Token: 0x04000ABD RID: 2749
		Union,
		/// <summary>Two clipping regions are combined by taking only the areas enclosed by one or the other region, but not both.</summary>
		// Token: 0x04000ABE RID: 2750
		Xor,
		/// <summary>Specifies that the existing region is replaced by the result of the new region being removed from the existing region. Said differently, the new region is excluded from the existing region.</summary>
		// Token: 0x04000ABF RID: 2751
		Exclude,
		/// <summary>Specifies that the existing region is replaced by the result of the existing region being removed from the new region. Said differently, the existing region is excluded from the new region.</summary>
		// Token: 0x04000AC0 RID: 2752
		Complement
	}
}
