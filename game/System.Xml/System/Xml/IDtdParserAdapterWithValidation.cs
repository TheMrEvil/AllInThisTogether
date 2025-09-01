using System;

namespace System.Xml
{
	// Token: 0x0200003D RID: 61
	internal interface IDtdParserAdapterWithValidation : IDtdParserAdapter
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000203 RID: 515
		bool DtdValidation { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000204 RID: 516
		IValidationEventHandling ValidationEventHandling { get; }
	}
}
