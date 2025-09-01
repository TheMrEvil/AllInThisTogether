using System;

namespace System
{
	// Token: 0x02000206 RID: 518
	internal enum TypeNameFormatFlags
	{
		// Token: 0x04001590 RID: 5520
		FormatBasic,
		// Token: 0x04001591 RID: 5521
		FormatNamespace,
		// Token: 0x04001592 RID: 5522
		FormatFullInst,
		// Token: 0x04001593 RID: 5523
		FormatAssembly = 4,
		// Token: 0x04001594 RID: 5524
		FormatSignature = 8,
		// Token: 0x04001595 RID: 5525
		FormatNoVersion = 16,
		// Token: 0x04001596 RID: 5526
		FormatAngleBrackets = 64,
		// Token: 0x04001597 RID: 5527
		FormatStubInfo = 128,
		// Token: 0x04001598 RID: 5528
		FormatGenericParam = 256,
		// Token: 0x04001599 RID: 5529
		FormatSerialization = 259
	}
}
