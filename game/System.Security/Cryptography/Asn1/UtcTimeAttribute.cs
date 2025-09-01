using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000F0 RID: 240
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class UtcTimeAttribute : AsnTypeAttribute
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0001732F File Offset: 0x0001552F
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x00017337 File Offset: 0x00015537
		public int TwoDigitYearMax
		{
			[CompilerGenerated]
			get
			{
				return this.<TwoDigitYearMax>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TwoDigitYearMax>k__BackingField = value;
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00017316 File Offset: 0x00015516
		public UtcTimeAttribute()
		{
		}

		// Token: 0x040003BC RID: 956
		[CompilerGenerated]
		private int <TwoDigitYearMax>k__BackingField;
	}
}
