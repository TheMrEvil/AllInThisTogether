using System;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000022 RID: 34
	internal interface IResponse
	{
		// Token: 0x060001A1 RID: 417
		void SetSuccess();

		// Token: 0x060001A2 RID: 418
		void SetFailure(string info);
	}
}
