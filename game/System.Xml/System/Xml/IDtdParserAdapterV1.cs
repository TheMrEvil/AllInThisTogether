using System;

namespace System.Xml
{
	// Token: 0x0200003E RID: 62
	internal interface IDtdParserAdapterV1 : IDtdParserAdapterWithValidation, IDtdParserAdapter
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000205 RID: 517
		bool V1CompatibilityMode { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000206 RID: 518
		bool Normalization { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000207 RID: 519
		bool Namespaces { get; }
	}
}
