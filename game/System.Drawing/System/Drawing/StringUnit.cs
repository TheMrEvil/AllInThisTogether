using System;

namespace System.Drawing
{
	/// <summary>Specifies the units of measure for a text string.</summary>
	// Token: 0x0200003F RID: 63
	public enum StringUnit
	{
		/// <summary>Specifies world units as the unit of measure.</summary>
		// Token: 0x04000372 RID: 882
		World,
		/// <summary>Specifies the device unit as the unit of measure.</summary>
		// Token: 0x04000373 RID: 883
		Display,
		/// <summary>Specifies a pixel as the unit of measure.</summary>
		// Token: 0x04000374 RID: 884
		Pixel,
		/// <summary>Specifies a printer's point (1/72 inch) as the unit of measure.</summary>
		// Token: 0x04000375 RID: 885
		Point,
		/// <summary>Specifies an inch as the unit of measure.</summary>
		// Token: 0x04000376 RID: 886
		Inch,
		/// <summary>Specifies 1/300 of an inch as the unit of measure.</summary>
		// Token: 0x04000377 RID: 887
		Document,
		/// <summary>Specifies a millimeter as the unit of measure</summary>
		// Token: 0x04000378 RID: 888
		Millimeter,
		/// <summary>Specifies a printer's em size of 32 as the unit of measure.</summary>
		// Token: 0x04000379 RID: 889
		Em = 32
	}
}
