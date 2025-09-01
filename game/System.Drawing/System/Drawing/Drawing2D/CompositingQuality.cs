using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the quality level to use during compositing.</summary>
	// Token: 0x02000139 RID: 313
	public enum CompositingQuality
	{
		/// <summary>Invalid quality.</summary>
		// Token: 0x04000AC5 RID: 2757
		Invalid = -1,
		/// <summary>Default quality.</summary>
		// Token: 0x04000AC6 RID: 2758
		Default,
		/// <summary>High speed, low quality.</summary>
		// Token: 0x04000AC7 RID: 2759
		HighSpeed,
		/// <summary>High quality, low speed compositing.</summary>
		// Token: 0x04000AC8 RID: 2760
		HighQuality,
		/// <summary>Gamma correction is used.</summary>
		// Token: 0x04000AC9 RID: 2761
		GammaCorrected,
		/// <summary>Assume linear values.</summary>
		// Token: 0x04000ACA RID: 2762
		AssumeLinear
	}
}
