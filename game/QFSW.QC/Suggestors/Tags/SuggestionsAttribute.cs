using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace QFSW.QC.Suggestors.Tags
{
	// Token: 0x0200005B RID: 91
	public sealed class SuggestionsAttribute : SuggestorTagAttribute
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00009B9C File Offset: 0x00007D9C
		public SuggestionsAttribute(params object[] suggestions)
		{
			InlineSuggestionsTag inlineSuggestionsTag = new InlineSuggestionsTag(from o in suggestions
			select o.ToString());
			this._tags = new IQcSuggestorTag[]
			{
				inlineSuggestionsTag
			};
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00009BF0 File Offset: 0x00007DF0
		public override IQcSuggestorTag[] GetSuggestorTags()
		{
			return this._tags;
		}

		// Token: 0x0400013C RID: 316
		private readonly IQcSuggestorTag[] _tags;

		// Token: 0x020000B3 RID: 179
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600036F RID: 879 RVA: 0x0000C85B File Offset: 0x0000AA5B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000370 RID: 880 RVA: 0x0000C867 File Offset: 0x0000AA67
			public <>c()
			{
			}

			// Token: 0x06000371 RID: 881 RVA: 0x0000C86F File Offset: 0x0000AA6F
			internal string <.ctor>b__1_0(object o)
			{
				return o.ToString();
			}

			// Token: 0x0400023A RID: 570
			public static readonly SuggestionsAttribute.<>c <>9 = new SuggestionsAttribute.<>c();

			// Token: 0x0400023B RID: 571
			public static Func<object, string> <>9__1_0;
		}
	}
}
