using System;

namespace QFSW.QC.Suggestors.Tags
{
	// Token: 0x0200005D RID: 93
	public sealed class SceneNameAttribute : SuggestorTagAttribute
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00009BF8 File Offset: 0x00007DF8
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x00009C05 File Offset: 0x00007E05
		public bool LoadedOnly
		{
			get
			{
				return this._tag.LoadedOnly;
			}
			set
			{
				this._tag.LoadedOnly = value;
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00009C13 File Offset: 0x00007E13
		public override IQcSuggestorTag[] GetSuggestorTags()
		{
			return new IQcSuggestorTag[]
			{
				this._tag
			};
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00009C29 File Offset: 0x00007E29
		public SceneNameAttribute()
		{
		}

		// Token: 0x0400013E RID: 318
		private SceneNameTag _tag;
	}
}
