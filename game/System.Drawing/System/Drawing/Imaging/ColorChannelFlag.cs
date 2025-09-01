using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies individual channels in the CMYK (cyan, magenta, yellow, black) color space. This enumeration is used by the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.SetOutputChannel" /> methods.</summary>
	// Token: 0x020000F7 RID: 247
	public enum ColorChannelFlag
	{
		/// <summary>The cyan color channel.</summary>
		// Token: 0x04000843 RID: 2115
		ColorChannelC,
		/// <summary>The magenta color channel.</summary>
		// Token: 0x04000844 RID: 2116
		ColorChannelM,
		/// <summary>The yellow color channel.</summary>
		// Token: 0x04000845 RID: 2117
		ColorChannelY,
		/// <summary>The black color channel.</summary>
		// Token: 0x04000846 RID: 2118
		ColorChannelK,
		/// <summary>The last selected channel should be used.</summary>
		// Token: 0x04000847 RID: 2119
		ColorChannelLast
	}
}
