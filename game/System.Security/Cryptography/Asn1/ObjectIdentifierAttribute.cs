using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000E7 RID: 231
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class ObjectIdentifierAttribute : AsnTypeAttribute
	{
		// Token: 0x060005A5 RID: 1445 RVA: 0x00017316 File Offset: 0x00015516
		public ObjectIdentifierAttribute()
		{
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001731E File Offset: 0x0001551E
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x00017326 File Offset: 0x00015526
		public bool PopulateFriendlyName
		{
			[CompilerGenerated]
			get
			{
				return this.<PopulateFriendlyName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<PopulateFriendlyName>k__BackingField = value;
			}
		}

		// Token: 0x040003BB RID: 955
		[CompilerGenerated]
		private bool <PopulateFriendlyName>k__BackingField;
	}
}
