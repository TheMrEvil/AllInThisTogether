using System;

namespace System.Xml.Serialization
{
	// Token: 0x0200028B RID: 651
	internal class EnumMapping : PrimitiveMapping
	{
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x0008EA83 File Offset: 0x0008CC83
		// (set) Token: 0x06001884 RID: 6276 RVA: 0x0008EA8B File Offset: 0x0008CC8B
		internal bool IsFlags
		{
			get
			{
				return this.isFlags;
			}
			set
			{
				this.isFlags = value;
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x0008EA94 File Offset: 0x0008CC94
		// (set) Token: 0x06001886 RID: 6278 RVA: 0x0008EA9C File Offset: 0x0008CC9C
		internal ConstantMapping[] Constants
		{
			get
			{
				return this.constants;
			}
			set
			{
				this.constants = value;
			}
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x0008EAA5 File Offset: 0x0008CCA5
		public EnumMapping()
		{
		}

		// Token: 0x040018CF RID: 6351
		private ConstantMapping[] constants;

		// Token: 0x040018D0 RID: 6352
		private bool isFlags;
	}
}
