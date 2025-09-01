using System;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	// Token: 0x020000A2 RID: 162
	internal struct GdipPropertyItem
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x00017484 File Offset: 0x00015684
		internal static void MarshalTo(GdipPropertyItem gdipProp, PropertyItem prop)
		{
			prop.Id = gdipProp.id;
			prop.Len = gdipProp.len;
			prop.Type = gdipProp.type;
			prop.Value = new byte[gdipProp.len];
			Marshal.Copy(gdipProp.value, prop.Value, 0, gdipProp.len);
		}

		// Token: 0x04000619 RID: 1561
		internal int id;

		// Token: 0x0400061A RID: 1562
		internal int len;

		// Token: 0x0400061B RID: 1563
		internal short type;

		// Token: 0x0400061C RID: 1564
		internal IntPtr value;
	}
}
