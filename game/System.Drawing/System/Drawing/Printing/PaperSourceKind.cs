using System;

namespace System.Drawing.Printing
{
	/// <summary>Standard paper sources.</summary>
	// Token: 0x020000BD RID: 189
	public enum PaperSourceKind
	{
		/// <summary>The upper bin of a printer (or the default bin, if the printer only has one bin).</summary>
		// Token: 0x040006E9 RID: 1769
		Upper = 1,
		/// <summary>The lower bin of a printer.</summary>
		// Token: 0x040006EA RID: 1770
		Lower,
		/// <summary>The middle bin of a printer.</summary>
		// Token: 0x040006EB RID: 1771
		Middle,
		/// <summary>Manually fed paper.</summary>
		// Token: 0x040006EC RID: 1772
		Manual,
		/// <summary>An envelope.</summary>
		// Token: 0x040006ED RID: 1773
		Envelope,
		/// <summary>Manually fed envelope.</summary>
		// Token: 0x040006EE RID: 1774
		ManualFeed,
		/// <summary>Automatically fed paper.</summary>
		// Token: 0x040006EF RID: 1775
		AutomaticFeed,
		/// <summary>A tractor feed.</summary>
		// Token: 0x040006F0 RID: 1776
		TractorFeed,
		/// <summary>Small-format paper.</summary>
		// Token: 0x040006F1 RID: 1777
		SmallFormat,
		/// <summary>Large-format paper.</summary>
		// Token: 0x040006F2 RID: 1778
		LargeFormat,
		/// <summary>The printer's large-capacity bin.</summary>
		// Token: 0x040006F3 RID: 1779
		LargeCapacity,
		/// <summary>A paper cassette.</summary>
		// Token: 0x040006F4 RID: 1780
		Cassette = 14,
		/// <summary>The printer's default input bin.</summary>
		// Token: 0x040006F5 RID: 1781
		FormSource,
		/// <summary>A printer-specific paper source.</summary>
		// Token: 0x040006F6 RID: 1782
		Custom = 257
	}
}
