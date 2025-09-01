using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002F1 RID: 753
	public class UxmlValueMatches : UxmlTypeRestriction
	{
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x00065D36 File Offset: 0x00063F36
		// (set) Token: 0x060018F2 RID: 6386 RVA: 0x00065D3E File Offset: 0x00063F3E
		public string regex
		{
			[CompilerGenerated]
			get
			{
				return this.<regex>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<regex>k__BackingField = value;
			}
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x00065D48 File Offset: 0x00063F48
		public override bool Equals(UxmlTypeRestriction other)
		{
			UxmlValueMatches uxmlValueMatches = other as UxmlValueMatches;
			bool flag = uxmlValueMatches == null;
			return !flag && this.regex == uxmlValueMatches.regex;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x00065D7F File Offset: 0x00063F7F
		public UxmlValueMatches()
		{
		}

		// Token: 0x04000AB3 RID: 2739
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <regex>k__BackingField;
	}
}
