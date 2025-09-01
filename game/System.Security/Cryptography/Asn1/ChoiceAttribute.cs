using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000F4 RID: 244
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
	internal sealed class ChoiceAttribute : Attribute
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0001737D File Offset: 0x0001557D
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x00017385 File Offset: 0x00015585
		public bool AllowNull
		{
			[CompilerGenerated]
			get
			{
				return this.<AllowNull>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AllowNull>k__BackingField = value;
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0001730E File Offset: 0x0001550E
		public ChoiceAttribute()
		{
		}

		// Token: 0x040003BF RID: 959
		[CompilerGenerated]
		private bool <AllowNull>k__BackingField;
	}
}
