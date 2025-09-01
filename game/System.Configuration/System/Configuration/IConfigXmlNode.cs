using System;

namespace System.Configuration
{
	// Token: 0x02000043 RID: 67
	internal interface IConfigXmlNode
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000243 RID: 579
		string Filename { get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000244 RID: 580
		int LineNumber { get; }
	}
}
