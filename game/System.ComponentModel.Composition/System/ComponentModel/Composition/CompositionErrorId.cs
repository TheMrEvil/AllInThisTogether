using System;

namespace System.ComponentModel.Composition
{
	// Token: 0x02000027 RID: 39
	internal enum CompositionErrorId
	{
		// Token: 0x04000072 RID: 114
		Unknown,
		// Token: 0x04000073 RID: 115
		InvalidExportMetadata,
		// Token: 0x04000074 RID: 116
		ImportNotSetOnPart,
		// Token: 0x04000075 RID: 117
		ImportEngine_ComposeTookTooManyIterations,
		// Token: 0x04000076 RID: 118
		ImportEngine_ImportCardinalityMismatch,
		// Token: 0x04000077 RID: 119
		ImportEngine_PartCycle,
		// Token: 0x04000078 RID: 120
		ImportEngine_PartCannotSetImport,
		// Token: 0x04000079 RID: 121
		ImportEngine_PartCannotGetExportedValue,
		// Token: 0x0400007A RID: 122
		ImportEngine_PartCannotActivate,
		// Token: 0x0400007B RID: 123
		ImportEngine_PreventedByExistingImport,
		// Token: 0x0400007C RID: 124
		ImportEngine_InvalidStateForRecomposition,
		// Token: 0x0400007D RID: 125
		ReflectionModel_ImportThrewException,
		// Token: 0x0400007E RID: 126
		ReflectionModel_ImportNotAssignableFromExport,
		// Token: 0x0400007F RID: 127
		ReflectionModel_ImportCollectionNull,
		// Token: 0x04000080 RID: 128
		ReflectionModel_ImportCollectionNotWritable,
		// Token: 0x04000081 RID: 129
		ReflectionModel_ImportCollectionConstructionThrewException,
		// Token: 0x04000082 RID: 130
		ReflectionModel_ImportCollectionGetThrewException,
		// Token: 0x04000083 RID: 131
		ReflectionModel_ImportCollectionIsReadOnlyThrewException,
		// Token: 0x04000084 RID: 132
		ReflectionModel_ImportCollectionClearThrewException,
		// Token: 0x04000085 RID: 133
		ReflectionModel_ImportCollectionAddThrewException,
		// Token: 0x04000086 RID: 134
		ReflectionModel_ImportManyOnParameterCanOnlyBeAssigned
	}
}
