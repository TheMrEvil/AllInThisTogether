using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies how pixels are offset during rendering.</summary>
	// Token: 0x0200014D RID: 333
	public enum PixelOffsetMode
	{
		/// <summary>Specifies an invalid mode.</summary>
		// Token: 0x04000B57 RID: 2903
		Invalid = -1,
		/// <summary>Specifies the default mode.</summary>
		// Token: 0x04000B58 RID: 2904
		Default,
		/// <summary>Specifies high speed, low quality rendering.</summary>
		// Token: 0x04000B59 RID: 2905
		HighSpeed,
		/// <summary>Specifies high quality, low speed rendering.</summary>
		// Token: 0x04000B5A RID: 2906
		HighQuality,
		/// <summary>Specifies no pixel offset.</summary>
		// Token: 0x04000B5B RID: 2907
		None,
		/// <summary>Specifies that pixels are offset by -.5 units, both horizontally and vertically, for high speed antialiasing.</summary>
		// Token: 0x04000B5C RID: 2908
		Half
	}
}
