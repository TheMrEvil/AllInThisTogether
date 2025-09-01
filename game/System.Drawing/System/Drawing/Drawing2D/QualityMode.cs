using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the overall quality when rendering GDI+ objects.</summary>
	// Token: 0x0200014E RID: 334
	public enum QualityMode
	{
		/// <summary>Specifies an invalid mode.</summary>
		// Token: 0x04000B5E RID: 2910
		Invalid = -1,
		/// <summary>Specifies the default mode.</summary>
		// Token: 0x04000B5F RID: 2911
		Default,
		/// <summary>Specifies low quality, high speed rendering.</summary>
		// Token: 0x04000B60 RID: 2912
		Low,
		/// <summary>Specifies high quality, low speed rendering.</summary>
		// Token: 0x04000B61 RID: 2913
		High
	}
}
