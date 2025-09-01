using System;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Diagnostics
{
	// Token: 0x020000FB RID: 251
	internal static class CompositionTrace
	{
		// Token: 0x060006A2 RID: 1698 RVA: 0x00014A63 File Offset: 0x00012C63
		internal static void PartDefinitionResurrected(ComposablePartDefinition definition)
		{
			Assumes.NotNull<ComposablePartDefinition>(definition);
			if (CompositionTraceSource.CanWriteInformation)
			{
				CompositionTraceSource.WriteInformation(CompositionTraceId.Rejection_DefinitionResurrected, Strings.CompositionTrace_Rejection_DefinitionResurrected, new object[]
				{
					definition.GetDisplayName()
				});
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00014A8C File Offset: 0x00012C8C
		internal static void PartDefinitionRejected(ComposablePartDefinition definition, ChangeRejectedException exception)
		{
			Assumes.NotNull<ComposablePartDefinition, ChangeRejectedException>(definition, exception);
			if (CompositionTraceSource.CanWriteWarning)
			{
				CompositionTraceSource.WriteWarning(CompositionTraceId.Rejection_DefinitionRejected, Strings.CompositionTrace_Rejection_DefinitionRejected, new object[]
				{
					definition.GetDisplayName(),
					exception.Message
				});
			}
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00014ABF File Offset: 0x00012CBF
		internal static void AssemblyLoadFailed(DirectoryCatalog catalog, string fileName, Exception exception)
		{
			Assumes.NotNull<DirectoryCatalog, Exception>(catalog, exception);
			Assumes.NotNullOrEmpty(fileName);
			if (CompositionTraceSource.CanWriteWarning)
			{
				CompositionTraceSource.WriteWarning(CompositionTraceId.Discovery_AssemblyLoadFailed, Strings.CompositionTrace_Discovery_AssemblyLoadFailed, new object[]
				{
					catalog.GetDisplayName(),
					fileName,
					exception.Message
				});
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00014AFC File Offset: 0x00012CFC
		internal static void DefinitionMarkedWithPartNotDiscoverableAttribute(Type type)
		{
			Assumes.NotNull<Type>(type);
			if (CompositionTraceSource.CanWriteInformation)
			{
				CompositionTraceSource.WriteInformation(CompositionTraceId.Discovery_DefinitionMarkedWithPartNotDiscoverableAttribute, Strings.CompositionTrace_Discovery_DefinitionMarkedWithPartNotDiscoverableAttribute, new object[]
				{
					type.GetDisplayName()
				});
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00014B25 File Offset: 0x00012D25
		internal static void DefinitionMismatchedExportArity(Type type, MemberInfo member)
		{
			Assumes.NotNull<Type>(type);
			Assumes.NotNull<MemberInfo>(member);
			if (CompositionTraceSource.CanWriteInformation)
			{
				CompositionTraceSource.WriteInformation(CompositionTraceId.Discovery_DefinitionMismatchedExportArity, Strings.CompositionTrace_Discovery_DefinitionMismatchedExportArity, new object[]
				{
					type.GetDisplayName(),
					member.GetDisplayName()
				});
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00014B5D File Offset: 0x00012D5D
		internal static void DefinitionContainsNoExports(Type type)
		{
			Assumes.NotNull<Type>(type);
			if (CompositionTraceSource.CanWriteInformation)
			{
				CompositionTraceSource.WriteInformation(CompositionTraceId.Discovery_DefinitionContainsNoExports, Strings.CompositionTrace_Discovery_DefinitionContainsNoExports, new object[]
				{
					type.GetDisplayName()
				});
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00014B86 File Offset: 0x00012D86
		internal static void MemberMarkedWithMultipleImportAndImportMany(ReflectionItem item)
		{
			Assumes.NotNull<ReflectionItem>(item);
			if (CompositionTraceSource.CanWriteError)
			{
				CompositionTraceSource.WriteError(CompositionTraceId.Discovery_MemberMarkedWithMultipleImportAndImportMany, Strings.CompositionTrace_Discovery_MemberMarkedWithMultipleImportAndImportMany, new object[]
				{
					item.GetDisplayName()
				});
			}
		}
	}
}
