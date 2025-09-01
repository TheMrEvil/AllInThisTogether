using System;

namespace System.Configuration
{
	// Token: 0x020001B0 RID: 432
	internal interface IConfigXmlNode
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000B7B RID: 2939
		string Filename { get; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000B7C RID: 2940
		int LineNumber { get; }
	}
}
