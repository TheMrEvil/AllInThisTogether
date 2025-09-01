using System;
using System.Runtime.Serialization;

namespace System.Globalization
{
	// Token: 0x02000989 RID: 2441
	[Serializable]
	internal class EraInfo
	{
		// Token: 0x060056A0 RID: 22176 RVA: 0x00124E94 File Offset: 0x00123094
		internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear)
		{
			this.era = era;
			this.yearOffset = yearOffset;
			this.minEraYear = minEraYear;
			this.maxEraYear = maxEraYear;
			this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
		}

		// Token: 0x060056A1 RID: 22177 RVA: 0x00124EE0 File Offset: 0x001230E0
		internal EraInfo(int era, int startYear, int startMonth, int startDay, int yearOffset, int minEraYear, int maxEraYear, string eraName, string abbrevEraName, string englishEraName)
		{
			this.era = era;
			this.yearOffset = yearOffset;
			this.minEraYear = minEraYear;
			this.maxEraYear = maxEraYear;
			this.ticks = new DateTime(startYear, startMonth, startDay).Ticks;
			this.eraName = eraName;
			this.abbrevEraName = abbrevEraName;
			this.englishEraName = englishEraName;
		}

		// Token: 0x040035EC RID: 13804
		internal int era;

		// Token: 0x040035ED RID: 13805
		internal long ticks;

		// Token: 0x040035EE RID: 13806
		internal int yearOffset;

		// Token: 0x040035EF RID: 13807
		internal int minEraYear;

		// Token: 0x040035F0 RID: 13808
		internal int maxEraYear;

		// Token: 0x040035F1 RID: 13809
		[OptionalField(VersionAdded = 4)]
		internal string eraName;

		// Token: 0x040035F2 RID: 13810
		[OptionalField(VersionAdded = 4)]
		internal string abbrevEraName;

		// Token: 0x040035F3 RID: 13811
		[OptionalField(VersionAdded = 4)]
		internal string englishEraName;
	}
}
