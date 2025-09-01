using System;

namespace System.Reflection.Emit
{
	/// <summary>Specifies one of two factors that determine the memory alignment of fields when a type is marshaled.</summary>
	// Token: 0x02000909 RID: 2313
	public enum PackingSize
	{
		/// <summary>The packing size is not specified.</summary>
		// Token: 0x0400309E RID: 12446
		Unspecified,
		/// <summary>The packing size is 1 byte.</summary>
		// Token: 0x0400309F RID: 12447
		Size1,
		/// <summary>The packing size is 2 bytes.</summary>
		// Token: 0x040030A0 RID: 12448
		Size2,
		/// <summary>The packing size is 4 bytes.</summary>
		// Token: 0x040030A1 RID: 12449
		Size4 = 4,
		/// <summary>The packing size is 8 bytes.</summary>
		// Token: 0x040030A2 RID: 12450
		Size8 = 8,
		/// <summary>The packing size is 16 bytes.</summary>
		// Token: 0x040030A3 RID: 12451
		Size16 = 16,
		/// <summary>The packing size is 32 bytes.</summary>
		// Token: 0x040030A4 RID: 12452
		Size32 = 32,
		/// <summary>The packing size is 64 bytes.</summary>
		// Token: 0x040030A5 RID: 12453
		Size64 = 64,
		/// <summary>The packing size is 128 bytes.</summary>
		// Token: 0x040030A6 RID: 12454
		Size128 = 128
	}
}
