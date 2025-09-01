using System;

namespace System.Xml
{
	// Token: 0x02000039 RID: 57
	internal interface IDtdDefaultAttributeInfo : IDtdAttributeInfo
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001CF RID: 463
		string DefaultValueExpanded { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001D0 RID: 464
		object DefaultValueTyped { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001D1 RID: 465
		int ValueLineNumber { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001D2 RID: 466
		int ValueLinePosition { get; }
	}
}
