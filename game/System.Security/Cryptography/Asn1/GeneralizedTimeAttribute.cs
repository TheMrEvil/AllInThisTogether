using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000F1 RID: 241
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class GeneralizedTimeAttribute : AsnTypeAttribute
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x00017340 File Offset: 0x00015540
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x00017348 File Offset: 0x00015548
		public bool DisallowFractions
		{
			[CompilerGenerated]
			get
			{
				return this.<DisallowFractions>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<DisallowFractions>k__BackingField = value;
			}
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00017316 File Offset: 0x00015516
		public GeneralizedTimeAttribute()
		{
		}

		// Token: 0x040003BD RID: 957
		[CompilerGenerated]
		private bool <DisallowFractions>k__BackingField;
	}
}
