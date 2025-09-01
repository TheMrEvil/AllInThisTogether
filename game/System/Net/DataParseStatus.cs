using System;

namespace System.Net
{
	// Token: 0x02000621 RID: 1569
	internal enum DataParseStatus
	{
		// Token: 0x04001CDF RID: 7391
		NeedMoreData,
		// Token: 0x04001CE0 RID: 7392
		ContinueParsing,
		// Token: 0x04001CE1 RID: 7393
		Done,
		// Token: 0x04001CE2 RID: 7394
		Invalid,
		// Token: 0x04001CE3 RID: 7395
		DataTooBig
	}
}
