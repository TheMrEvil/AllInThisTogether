using System;
using System.ComponentModel.Composition.Primitives;
using Microsoft.Internal;

namespace System.ComponentModel.Composition
{
	// Token: 0x02000033 RID: 51
	internal static class ErrorBuilder
	{
		// Token: 0x06000198 RID: 408 RVA: 0x000055A8 File Offset: 0x000037A8
		public static CompositionError PreventedByExistingImport(ComposablePart part, ImportDefinition import)
		{
			return CompositionError.Create(CompositionErrorId.ImportEngine_PreventedByExistingImport, Strings.ImportEngine_PreventedByExistingImport, new object[]
			{
				import.ToElement().DisplayName,
				part.ToElement().DisplayName
			});
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000055D8 File Offset: 0x000037D8
		public static CompositionError InvalidStateForRecompposition(ComposablePart part)
		{
			return CompositionError.Create(CompositionErrorId.ImportEngine_InvalidStateForRecomposition, Strings.ImportEngine_InvalidStateForRecomposition, new object[]
			{
				part.ToElement().DisplayName
			});
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000055FA File Offset: 0x000037FA
		public static CompositionError ComposeTookTooManyIterations(int maximumNumberOfCompositionIterations)
		{
			return CompositionError.Create(CompositionErrorId.ImportEngine_ComposeTookTooManyIterations, Strings.ImportEngine_ComposeTookTooManyIterations, new object[]
			{
				maximumNumberOfCompositionIterations
			});
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00005616 File Offset: 0x00003816
		public static CompositionError CreateImportCardinalityMismatch(ImportCardinalityMismatchException exception, ImportDefinition definition)
		{
			Assumes.NotNull<ImportCardinalityMismatchException, ImportDefinition>(exception, definition);
			CompositionErrorId id = CompositionErrorId.ImportEngine_ImportCardinalityMismatch;
			string message = exception.Message;
			object[] array = new object[2];
			array[0] = definition.ToElement();
			return CompositionError.Create(id, message, array);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000563C File Offset: 0x0000383C
		public static CompositionError CreatePartCannotActivate(ComposablePart part, Exception innerException)
		{
			Assumes.NotNull<ComposablePart, Exception>(part, innerException);
			ICompositionElement compositionElement = part.ToElement();
			return CompositionError.Create(CompositionErrorId.ImportEngine_PartCannotActivate, compositionElement, innerException, Strings.ImportEngine_PartCannotActivate, new object[]
			{
				compositionElement.DisplayName
			});
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005674 File Offset: 0x00003874
		public static CompositionError CreatePartCannotSetImport(ComposablePart part, ImportDefinition definition, Exception innerException)
		{
			Assumes.NotNull<ComposablePart, ImportDefinition, Exception>(part, definition, innerException);
			ICompositionElement compositionElement = definition.ToElement();
			return CompositionError.Create(CompositionErrorId.ImportEngine_PartCannotSetImport, compositionElement, innerException, Strings.ImportEngine_PartCannotSetImport, new object[]
			{
				compositionElement.DisplayName,
				part.ToElement().DisplayName
			});
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000056BC File Offset: 0x000038BC
		public static CompositionError CreateCannotGetExportedValue(ComposablePart part, ExportDefinition definition, Exception innerException)
		{
			Assumes.NotNull<ComposablePart, ExportDefinition, Exception>(part, definition, innerException);
			ICompositionElement compositionElement = definition.ToElement();
			return CompositionError.Create(CompositionErrorId.ImportEngine_PartCannotGetExportedValue, compositionElement, innerException, Strings.ImportEngine_PartCannotGetExportedValue, new object[]
			{
				compositionElement.DisplayName,
				part.ToElement().DisplayName
			});
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005704 File Offset: 0x00003904
		public static CompositionError CreatePartCycle(ComposablePart part)
		{
			Assumes.NotNull<ComposablePart>(part);
			ICompositionElement compositionElement = part.ToElement();
			return CompositionError.Create(CompositionErrorId.ImportEngine_PartCycle, compositionElement, Strings.ImportEngine_PartCycle, new object[]
			{
				compositionElement.DisplayName
			});
		}
	}
}
