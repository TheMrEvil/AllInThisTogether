using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x02000373 RID: 883
	internal struct MatchResult
	{
		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x000868B8 File Offset: 0x00084AB8
		public bool success
		{
			get
			{
				return this.errorCode == MatchResultErrorCode.None;
			}
		}

		// Token: 0x04000E32 RID: 3634
		public MatchResultErrorCode errorCode;

		// Token: 0x04000E33 RID: 3635
		public string errorValue;
	}
}
