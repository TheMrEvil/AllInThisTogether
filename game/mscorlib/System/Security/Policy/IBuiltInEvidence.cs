using System;

namespace System.Security.Policy
{
	// Token: 0x02000415 RID: 1045
	internal interface IBuiltInEvidence
	{
		// Token: 0x06002ABF RID: 10943
		int GetRequiredSize(bool verbose);

		// Token: 0x06002AC0 RID: 10944
		int InitFromBuffer(char[] buffer, int position);

		// Token: 0x06002AC1 RID: 10945
		int OutputToBuffer(char[] buffer, int position, bool verbose);
	}
}
