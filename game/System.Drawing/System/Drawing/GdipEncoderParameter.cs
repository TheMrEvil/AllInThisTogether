using System;
using System.Drawing.Imaging;

namespace System.Drawing
{
	// Token: 0x020000A1 RID: 161
	internal struct GdipEncoderParameter
	{
		// Token: 0x04000615 RID: 1557
		internal Guid guid;

		// Token: 0x04000616 RID: 1558
		internal uint numberOfValues;

		// Token: 0x04000617 RID: 1559
		internal EncoderParameterValueType type;

		// Token: 0x04000618 RID: 1560
		internal IntPtr value;
	}
}
