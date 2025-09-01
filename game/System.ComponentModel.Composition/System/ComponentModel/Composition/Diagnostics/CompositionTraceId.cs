using System;

namespace System.ComponentModel.Composition.Diagnostics
{
	// Token: 0x020000FC RID: 252
	internal enum CompositionTraceId : ushort
	{
		// Token: 0x040002D8 RID: 728
		Rejection_DefinitionRejected = 1,
		// Token: 0x040002D9 RID: 729
		Rejection_DefinitionResurrected,
		// Token: 0x040002DA RID: 730
		Discovery_AssemblyLoadFailed,
		// Token: 0x040002DB RID: 731
		Discovery_DefinitionMarkedWithPartNotDiscoverableAttribute,
		// Token: 0x040002DC RID: 732
		Discovery_DefinitionMismatchedExportArity,
		// Token: 0x040002DD RID: 733
		Discovery_DefinitionContainsNoExports,
		// Token: 0x040002DE RID: 734
		Discovery_MemberMarkedWithMultipleImportAndImportMany
	}
}
