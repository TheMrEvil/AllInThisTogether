using System;

namespace QFSW.QC.Suggestors.Tags
{
	// Token: 0x02000059 RID: 89
	public sealed class CommandNameAttribute : SuggestorTagAttribute
	{
		// Token: 0x060001EC RID: 492 RVA: 0x00009B57 File Offset: 0x00007D57
		public override IQcSuggestorTag[] GetSuggestorTags()
		{
			return this._tags;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00009B60 File Offset: 0x00007D60
		public CommandNameAttribute()
		{
		}

		// Token: 0x0400013A RID: 314
		private readonly IQcSuggestorTag[] _tags = new IQcSuggestorTag[]
		{
			default(CommandNameTag)
		};
	}
}
