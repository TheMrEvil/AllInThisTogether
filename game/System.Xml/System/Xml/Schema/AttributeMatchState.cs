using System;

namespace System.Xml.Schema
{
	// Token: 0x02000575 RID: 1397
	internal enum AttributeMatchState
	{
		// Token: 0x040028C9 RID: 10441
		AttributeFound,
		// Token: 0x040028CA RID: 10442
		AnyIdAttributeFound,
		// Token: 0x040028CB RID: 10443
		UndeclaredElementAndAttribute,
		// Token: 0x040028CC RID: 10444
		UndeclaredAttribute,
		// Token: 0x040028CD RID: 10445
		AnyAttributeLax,
		// Token: 0x040028CE RID: 10446
		AnyAttributeSkip,
		// Token: 0x040028CF RID: 10447
		ProhibitedAnyAttribute,
		// Token: 0x040028D0 RID: 10448
		ProhibitedAttribute,
		// Token: 0x040028D1 RID: 10449
		AttributeNameMismatch,
		// Token: 0x040028D2 RID: 10450
		ValidateAttributeInvalidCall
	}
}
