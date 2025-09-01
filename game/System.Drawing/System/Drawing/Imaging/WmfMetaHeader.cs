using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	// Token: 0x02000119 RID: 281
	[StructLayout(LayoutKind.Sequential, Pack = 2)]
	internal struct WmfMetaHeader
	{
		// Token: 0x04000A70 RID: 2672
		public short file_type;

		// Token: 0x04000A71 RID: 2673
		public short header_size;

		// Token: 0x04000A72 RID: 2674
		public short version;

		// Token: 0x04000A73 RID: 2675
		public ushort file_size_low;

		// Token: 0x04000A74 RID: 2676
		public ushort file_size_high;

		// Token: 0x04000A75 RID: 2677
		public short num_of_objects;

		// Token: 0x04000A76 RID: 2678
		public int max_record_size;

		// Token: 0x04000A77 RID: 2679
		public short num_of_params;
	}
}
