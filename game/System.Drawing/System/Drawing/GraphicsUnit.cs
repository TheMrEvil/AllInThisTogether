using System;

namespace System.Drawing
{
	/// <summary>Specifies the unit of measure for the given data.</summary>
	// Token: 0x02000074 RID: 116
	public enum GraphicsUnit
	{
		/// <summary>Specifies the world coordinate system unit as the unit of measure.</summary>
		// Token: 0x0400049D RID: 1181
		World,
		/// <summary>Specifies the unit of measure of the display device. Typically pixels for video displays, and 1/100 inch for printers.</summary>
		// Token: 0x0400049E RID: 1182
		Display,
		/// <summary>Specifies a device pixel as the unit of measure.</summary>
		// Token: 0x0400049F RID: 1183
		Pixel,
		/// <summary>Specifies a printer's point (1/72 inch) as the unit of measure.</summary>
		// Token: 0x040004A0 RID: 1184
		Point,
		/// <summary>Specifies the inch as the unit of measure.</summary>
		// Token: 0x040004A1 RID: 1185
		Inch,
		/// <summary>Specifies the document unit (1/300 inch) as the unit of measure.</summary>
		// Token: 0x040004A2 RID: 1186
		Document,
		/// <summary>Specifies the millimeter as the unit of measure.</summary>
		// Token: 0x040004A3 RID: 1187
		Millimeter
	}
}
