using System;
using System.ComponentModel.Composition.Primitives;

namespace System.ComponentModel.Composition.Hosting
{
	/// <summary>Contains static metadata keys used by the composition system.</summary>
	// Token: 0x020000CB RID: 203
	public static class CompositionConstants
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x0000F8F1 File Offset: 0x0000DAF1
		// Note: this type is marked as 'beforefieldinit'.
		static CompositionConstants()
		{
		}

		// Token: 0x0400022C RID: 556
		private const string CompositionNamespace = "System.ComponentModel.Composition";

		/// <summary>Specifies the metadata key created by the composition system to mark a part with a creation policy.</summary>
		// Token: 0x0400022D RID: 557
		public const string PartCreationPolicyMetadataName = "System.ComponentModel.Composition.CreationPolicy";

		/// <summary>Specifies the metadata key created by the composition system to mark an import source.</summary>
		// Token: 0x0400022E RID: 558
		public const string ImportSourceMetadataName = "System.ComponentModel.Composition.ImportSource";

		/// <summary>Specifies the metadata key created by the composition system to mark an <see langword="IsGenericPart" /> method.</summary>
		// Token: 0x0400022F RID: 559
		public const string IsGenericPartMetadataName = "System.ComponentModel.Composition.IsGenericPart";

		/// <summary>Specifies the metadata key created by the composition system to mark a generic contract.</summary>
		// Token: 0x04000230 RID: 560
		public const string GenericContractMetadataName = "System.ComponentModel.Composition.GenericContractName";

		/// <summary>Specifies the metadata key created by the composition system to mark generic parameters.</summary>
		// Token: 0x04000231 RID: 561
		public const string GenericParametersMetadataName = "System.ComponentModel.Composition.GenericParameters";

		/// <summary>Specifies the metadata key created by the composition system to mark a part with a unique identifier.</summary>
		// Token: 0x04000232 RID: 562
		public const string ExportTypeIdentityMetadataName = "ExportTypeIdentity";

		// Token: 0x04000233 RID: 563
		internal const string GenericImportParametersOrderMetadataName = "System.ComponentModel.Composition.GenericImportParametersOrderMetadataName";

		// Token: 0x04000234 RID: 564
		internal const string GenericExportParametersOrderMetadataName = "System.ComponentModel.Composition.GenericExportParametersOrderMetadataName";

		// Token: 0x04000235 RID: 565
		internal const string GenericPartArityMetadataName = "System.ComponentModel.Composition.GenericPartArity";

		// Token: 0x04000236 RID: 566
		internal const string GenericParameterConstraintsMetadataName = "System.ComponentModel.Composition.GenericParameterConstraints";

		// Token: 0x04000237 RID: 567
		internal const string GenericParameterAttributesMetadataName = "System.ComponentModel.Composition.GenericParameterAttributes";

		// Token: 0x04000238 RID: 568
		internal const string ProductDefinitionMetadataName = "ProductDefinition";

		// Token: 0x04000239 RID: 569
		internal const string PartCreatorContractName = "System.ComponentModel.Composition.Contracts.ExportFactory";

		// Token: 0x0400023A RID: 570
		internal static readonly string PartCreatorTypeIdentity = AttributedModelServices.GetTypeIdentity(typeof(ComposablePartDefinition));
	}
}
