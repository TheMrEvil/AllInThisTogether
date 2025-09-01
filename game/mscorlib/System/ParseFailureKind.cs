using System;

namespace System
{
	// Token: 0x0200012A RID: 298
	internal enum ParseFailureKind
	{
		// Token: 0x0400115F RID: 4447
		None,
		// Token: 0x04001160 RID: 4448
		ArgumentNull,
		// Token: 0x04001161 RID: 4449
		Format,
		// Token: 0x04001162 RID: 4450
		FormatWithParameter,
		// Token: 0x04001163 RID: 4451
		FormatWithOriginalDateTime,
		// Token: 0x04001164 RID: 4452
		FormatWithFormatSpecifier,
		// Token: 0x04001165 RID: 4453
		FormatWithOriginalDateTimeAndParameter,
		// Token: 0x04001166 RID: 4454
		FormatBadDateTimeCalendar
	}
}
