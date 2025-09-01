using System;
using System.Runtime.CompilerServices;

namespace System.Security.Cryptography.Asn1
{
	// Token: 0x020000E1 RID: 225
	[AttributeUsage(AttributeTargets.Field)]
	internal sealed class ExpectedTagAttribute : Attribute
	{
		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x000172C9 File Offset: 0x000154C9
		public TagClass TagClass
		{
			[CompilerGenerated]
			get
			{
				return this.<TagClass>k__BackingField;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x000172D1 File Offset: 0x000154D1
		public int TagValue
		{
			[CompilerGenerated]
			get
			{
				return this.<TagValue>k__BackingField;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x000172D9 File Offset: 0x000154D9
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x000172E1 File Offset: 0x000154E1
		public bool ExplicitTag
		{
			[CompilerGenerated]
			get
			{
				return this.<ExplicitTag>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<ExplicitTag>k__BackingField = value;
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000172EA File Offset: 0x000154EA
		public ExpectedTagAttribute(int tagValue) : this(TagClass.ContextSpecific, tagValue)
		{
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000172F8 File Offset: 0x000154F8
		public ExpectedTagAttribute(TagClass tagClass, int tagValue)
		{
			this.TagClass = tagClass;
			this.TagValue = tagValue;
		}

		// Token: 0x040003B8 RID: 952
		[CompilerGenerated]
		private readonly TagClass <TagClass>k__BackingField;

		// Token: 0x040003B9 RID: 953
		[CompilerGenerated]
		private readonly int <TagValue>k__BackingField;

		// Token: 0x040003BA RID: 954
		[CompilerGenerated]
		private bool <ExplicitTag>k__BackingField;
	}
}
