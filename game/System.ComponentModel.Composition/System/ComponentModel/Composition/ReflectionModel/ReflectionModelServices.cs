using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.ReflectionModel
{
	/// <summary>Provides extension methods to create and retrieve reflection-based parts.</summary>
	// Token: 0x0200007E RID: 126
	public static class ReflectionModelServices
	{
		/// <summary>Gets the type of a part from a specified part definition.</summary>
		/// <param name="partDefinition">The part definition to examine.</param>
		/// <returns>The type of the defined part.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="partDefinition" /> is <see langword="null" />.</exception>
		// Token: 0x0600034E RID: 846 RVA: 0x0000A0D2 File Offset: 0x000082D2
		public static Lazy<Type> GetPartType(ComposablePartDefinition partDefinition)
		{
			Requires.NotNull<ComposablePartDefinition>(partDefinition, "partDefinition");
			ReflectionComposablePartDefinition reflectionComposablePartDefinition = partDefinition as ReflectionComposablePartDefinition;
			if (reflectionComposablePartDefinition == null)
			{
				throw ExceptionBuilder.CreateReflectionModelInvalidPartDefinition("partDefinition", partDefinition.GetType());
			}
			return reflectionComposablePartDefinition.GetLazyPartType();
		}

		/// <summary>Determines whether the specified part requires disposal.</summary>
		/// <param name="partDefinition">The part to examine.</param>
		/// <returns>
		///   <see langword="true" /> if the part requires disposal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="partDefinition" /> is <see langword="null" />.</exception>
		// Token: 0x0600034F RID: 847 RVA: 0x0000A0FE File Offset: 0x000082FE
		public static bool IsDisposalRequired(ComposablePartDefinition partDefinition)
		{
			Requires.NotNull<ComposablePartDefinition>(partDefinition, "partDefinition");
			ReflectionComposablePartDefinition reflectionComposablePartDefinition = partDefinition as ReflectionComposablePartDefinition;
			if (reflectionComposablePartDefinition == null)
			{
				throw ExceptionBuilder.CreateReflectionModelInvalidPartDefinition("partDefinition", partDefinition.GetType());
			}
			return reflectionComposablePartDefinition.IsDisposalRequired;
		}

		/// <summary>Gets the exporting member from a specified export definition.</summary>
		/// <param name="exportDefinition">The export definition to examine.</param>
		/// <returns>The member specified in the export definition.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="exportDefinition" /> is <see langword="null" />.</exception>
		// Token: 0x06000350 RID: 848 RVA: 0x0000A12A File Offset: 0x0000832A
		public static LazyMemberInfo GetExportingMember(ExportDefinition exportDefinition)
		{
			Requires.NotNull<ExportDefinition>(exportDefinition, "exportDefinition");
			ReflectionMemberExportDefinition reflectionMemberExportDefinition = exportDefinition as ReflectionMemberExportDefinition;
			if (reflectionMemberExportDefinition == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidExportDefinition, exportDefinition.GetType()), "exportDefinition");
			}
			return reflectionMemberExportDefinition.ExportingLazyMember;
		}

		/// <summary>Gets the importing member from a specified import definition.</summary>
		/// <param name="importDefinition">The import definition to examine.</param>
		/// <returns>The member specified in the import definition.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="importDefinition" /> is <see langword="null" />.</exception>
		// Token: 0x06000351 RID: 849 RVA: 0x0000A165 File Offset: 0x00008365
		public static LazyMemberInfo GetImportingMember(ImportDefinition importDefinition)
		{
			Requires.NotNull<ImportDefinition>(importDefinition, "importDefinition");
			ReflectionMemberImportDefinition reflectionMemberImportDefinition = importDefinition as ReflectionMemberImportDefinition;
			if (reflectionMemberImportDefinition == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidMemberImportDefinition, importDefinition.GetType()), "importDefinition");
			}
			return reflectionMemberImportDefinition.ImportingLazyMember;
		}

		/// <summary>Gets the importing parameter from a specified import definition.</summary>
		/// <param name="importDefinition">The import definition to examine.</param>
		/// <returns>The parameter specified in the import definition.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="importDefinition" /> is <see langword="null" />.</exception>
		// Token: 0x06000352 RID: 850 RVA: 0x0000A1A0 File Offset: 0x000083A0
		public static Lazy<ParameterInfo> GetImportingParameter(ImportDefinition importDefinition)
		{
			Requires.NotNull<ImportDefinition>(importDefinition, "importDefinition");
			ReflectionParameterImportDefinition reflectionParameterImportDefinition = importDefinition as ReflectionParameterImportDefinition;
			if (reflectionParameterImportDefinition == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidParameterImportDefinition, importDefinition.GetType()), "importDefinition");
			}
			return reflectionParameterImportDefinition.ImportingLazyParameter;
		}

		/// <summary>Determines whether an import definition represents a member or a parameter.</summary>
		/// <param name="importDefinition">The import definition to examine.</param>
		/// <returns>
		///   <see langword="true" /> if the import definition represents a parameter; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="importDefinition" /> is <see langword="null" />.</exception>
		// Token: 0x06000353 RID: 851 RVA: 0x0000A1DB File Offset: 0x000083DB
		public static bool IsImportingParameter(ImportDefinition importDefinition)
		{
			Requires.NotNull<ImportDefinition>(importDefinition, "importDefinition");
			if (!(importDefinition is ReflectionImportDefinition))
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidImportDefinition, importDefinition.GetType()), "importDefinition");
			}
			return importDefinition is ReflectionParameterImportDefinition;
		}

		/// <summary>Indicates whether a specified import definition represents an export factory (<see cref="T:System.ComponentModel.Composition.ExportFactory`1" /> or <see cref="T:System.ComponentModel.Composition.ExportFactory`2" /> object).</summary>
		/// <param name="importDefinition">The import definition to check.</param>
		/// <returns>
		///   <see langword="true" /> if the specified import definition represents an export factory; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000354 RID: 852 RVA: 0x0000A219 File Offset: 0x00008419
		public static bool IsExportFactoryImportDefinition(ImportDefinition importDefinition)
		{
			Requires.NotNull<ImportDefinition>(importDefinition, "importDefinition");
			return importDefinition is IPartCreatorImportDefinition;
		}

		/// <summary>Returns a representation of an import definition as an export factory product.</summary>
		/// <param name="importDefinition">The import definition to represent.</param>
		/// <returns>The representation of the import definition.</returns>
		// Token: 0x06000355 RID: 853 RVA: 0x0000A22F File Offset: 0x0000842F
		public static ContractBasedImportDefinition GetExportFactoryProductImportDefinition(ImportDefinition importDefinition)
		{
			Requires.NotNull<ImportDefinition>(importDefinition, "importDefinition");
			IPartCreatorImportDefinition partCreatorImportDefinition = importDefinition as IPartCreatorImportDefinition;
			if (partCreatorImportDefinition == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidImportDefinition, importDefinition.GetType()), "importDefinition");
			}
			return partCreatorImportDefinition.ProductImportDefinition;
		}

		/// <summary>Creates a part definition with the specified part type, imports, exports, metadata, and origin.</summary>
		/// <param name="partType">The type of the part.</param>
		/// <param name="isDisposalRequired">
		///   <see langword="true" /> if the part requires disposal; otherwise, <see langword="false" />.</param>
		/// <param name="imports">A collection of the part's imports.</param>
		/// <param name="exports">A collection of the part's exports.</param>
		/// <param name="metadata">The part's metadata.</param>
		/// <param name="origin">The part's origin.</param>
		/// <returns>A part definition created from the specified parameters.</returns>
		// Token: 0x06000356 RID: 854 RVA: 0x0000A26A File Offset: 0x0000846A
		public static ComposablePartDefinition CreatePartDefinition(Lazy<Type> partType, bool isDisposalRequired, Lazy<IEnumerable<ImportDefinition>> imports, Lazy<IEnumerable<ExportDefinition>> exports, Lazy<IDictionary<string, object>> metadata, ICompositionElement origin)
		{
			Requires.NotNull<Lazy<Type>>(partType, "partType");
			return new ReflectionComposablePartDefinition(new ReflectionPartCreationInfo(partType, isDisposalRequired, imports, exports, metadata, origin));
		}

		/// <summary>Creates an export definition from the specified member, with the specified contract name, metadata, and origin.</summary>
		/// <param name="exportingMember">The member to export.</param>
		/// <param name="contractName">The contract name to use for the export.</param>
		/// <param name="metadata">The metadata for the export.</param>
		/// <param name="origin">The object that the export originates from.</param>
		/// <returns>An export definition created from the specified parameters.</returns>
		// Token: 0x06000357 RID: 855 RVA: 0x0000A289 File Offset: 0x00008489
		public static ExportDefinition CreateExportDefinition(LazyMemberInfo exportingMember, string contractName, Lazy<IDictionary<string, object>> metadata, ICompositionElement origin)
		{
			Requires.NotNullOrEmpty(contractName, "contractName");
			Requires.IsInMembertypeSet(exportingMember.MemberType, "exportingMember", MemberTypes.Field | MemberTypes.Method | MemberTypes.Property | MemberTypes.TypeInfo | MemberTypes.NestedType);
			return new ReflectionMemberExportDefinition(exportingMember, new LazyExportDefinition(contractName, metadata), origin);
		}

		/// <summary>Creates an import definition for the specified member by using the specified contract name, type identity, import metadata, cardinality, recomposition policy, and creation policy.</summary>
		/// <param name="importingMember">The member to import into.</param>
		/// <param name="contractName">The contract name to use for the import.</param>
		/// <param name="requiredTypeIdentity">The required type identity for the import.</param>
		/// <param name="requiredMetadata">The required metadata for the import.</param>
		/// <param name="cardinality">The cardinality of the import.</param>
		/// <param name="isRecomposable">
		///   <see langword="true" /> to indicate that the import is recomposable; otherwise, <see langword="false" />.</param>
		/// <param name="requiredCreationPolicy">One of the enumeration values that specifies the import's creation policy.</param>
		/// <param name="origin">The object to import into.</param>
		/// <returns>An import definition created from the specified parameters.</returns>
		// Token: 0x06000358 RID: 856 RVA: 0x0000A2BC File Offset: 0x000084BC
		public static ContractBasedImportDefinition CreateImportDefinition(LazyMemberInfo importingMember, string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, bool isRecomposable, CreationPolicy requiredCreationPolicy, ICompositionElement origin)
		{
			return ReflectionModelServices.CreateImportDefinition(importingMember, contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable, requiredCreationPolicy, MetadataServices.EmptyMetadata, false, origin);
		}

		/// <summary>Creates an import definition for the specified member by using the specified contract name, type identity, import and contract metadata, cardinality, recomposition policy, and creation policy.</summary>
		/// <param name="importingMember">The member to import into.</param>
		/// <param name="contractName">The contract name to use for the import.</param>
		/// <param name="requiredTypeIdentity">The required type identity for the import.</param>
		/// <param name="requiredMetadata">The required metadata for the import.</param>
		/// <param name="cardinality">The cardinality of the import.</param>
		/// <param name="isRecomposable">
		///   <see langword="true" /> to indicate that the import is recomposable; otherwise, <see langword="false" />.</param>
		/// <param name="requiredCreationPolicy">One of the enumeration values that specifies the import's creation policy.</param>
		/// <param name="metadata">The contract metadata.</param>
		/// <param name="isExportFactory">
		///   <see langword="true" /> to indicate that the import represents an <see cref="T:System.ComponentModel.Composition.ExportFactory`1" />; otherwise, <see langword="false" />.</param>
		/// <param name="origin">The object to import into.</param>
		/// <returns>An import definition created from the specified parameters.</returns>
		// Token: 0x06000359 RID: 857 RVA: 0x0000A2E0 File Offset: 0x000084E0
		public static ContractBasedImportDefinition CreateImportDefinition(LazyMemberInfo importingMember, string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, bool isRecomposable, CreationPolicy requiredCreationPolicy, IDictionary<string, object> metadata, bool isExportFactory, ICompositionElement origin)
		{
			return ReflectionModelServices.CreateImportDefinition(importingMember, contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable, false, requiredCreationPolicy, metadata, isExportFactory, origin);
		}

		/// <summary>Creates an import definition for the specified member by using the specified contract name, type identity, import and contract metadata, cardinality, recomposition policy, and creation policy.</summary>
		/// <param name="importingMember">The member to import into.</param>
		/// <param name="contractName">The contract name to use for the import.</param>
		/// <param name="requiredTypeIdentity">The required type identity for the import.</param>
		/// <param name="requiredMetadata">The required metadata for the import.</param>
		/// <param name="cardinality">The cardinality of the import.</param>
		/// <param name="isRecomposable">
		///   <see langword="true" /> to indicate that the import is recomposable; otherwise, <see langword="false" />.</param>
		/// <param name="isPreRequisite">
		///   <see langword="true" /> to indicate that the import is a prerequisite; otherwise, <see langword="false" />.</param>
		/// <param name="requiredCreationPolicy">One of the enumeration values that specifies the import's creation policy.</param>
		/// <param name="metadata">The contract metadata.</param>
		/// <param name="isExportFactory">
		///   <see langword="true" /> to indicate that the import represents an <see cref="T:System.ComponentModel.Composition.ExportFactory`1" />; otherwise, <see langword="false" />.</param>
		/// <param name="origin">The object to import into.</param>
		/// <returns>An import definition created from the specified parameters.</returns>
		// Token: 0x0600035A RID: 858 RVA: 0x0000A304 File Offset: 0x00008504
		public static ContractBasedImportDefinition CreateImportDefinition(LazyMemberInfo importingMember, string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, bool isRecomposable, bool isPreRequisite, CreationPolicy requiredCreationPolicy, IDictionary<string, object> metadata, bool isExportFactory, ICompositionElement origin)
		{
			Requires.NotNullOrEmpty(contractName, "contractName");
			Requires.IsInMembertypeSet(importingMember.MemberType, "importingMember", MemberTypes.Field | MemberTypes.Property);
			if (isExportFactory)
			{
				return new PartCreatorMemberImportDefinition(importingMember, origin, new ContractBasedImportDefinition(contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable, isPreRequisite, CreationPolicy.NonShared, metadata));
			}
			return new ReflectionMemberImportDefinition(importingMember, contractName, requiredTypeIdentity, requiredMetadata, cardinality, isRecomposable, isPreRequisite, requiredCreationPolicy, metadata, origin);
		}

		/// <summary>Creates an import definition for the specified parameter by using the specified contract name, type identity, import metadata, cardinality, and creation policy.</summary>
		/// <param name="parameter">The parameter to import.</param>
		/// <param name="contractName">The contract name to use for the import.</param>
		/// <param name="requiredTypeIdentity">The required type identity for the import.</param>
		/// <param name="requiredMetadata">The required metadata for the import.</param>
		/// <param name="cardinality">The cardinality of the import.</param>
		/// <param name="requiredCreationPolicy">One of the enumeration values that specifies the import's creation policy.</param>
		/// <param name="origin">The object to import into.</param>
		/// <returns>An import definition created from the specified parameters.</returns>
		// Token: 0x0600035B RID: 859 RVA: 0x0000A364 File Offset: 0x00008564
		public static ContractBasedImportDefinition CreateImportDefinition(Lazy<ParameterInfo> parameter, string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, CreationPolicy requiredCreationPolicy, ICompositionElement origin)
		{
			return ReflectionModelServices.CreateImportDefinition(parameter, contractName, requiredTypeIdentity, requiredMetadata, cardinality, requiredCreationPolicy, MetadataServices.EmptyMetadata, false, origin);
		}

		/// <summary>Creates an import definition for the specified parameter by using the specified contract name, type identity, import and contract metadata, cardinality, and creation policy.</summary>
		/// <param name="parameter">The parameter to import.</param>
		/// <param name="contractName">The contract name to use for the import.</param>
		/// <param name="requiredTypeIdentity">The required type identity for the import.</param>
		/// <param name="requiredMetadata">The required metadata for the import.</param>
		/// <param name="cardinality">The cardinality of the import.</param>
		/// <param name="requiredCreationPolicy">One of the enumeration values that specifies the import's creation policy.</param>
		/// <param name="metadata">The contract metadata</param>
		/// <param name="isExportFactory">
		///   <see langword="true" /> to indicate that the import represents an <see cref="T:System.ComponentModel.Composition.ExportFactory`1" />; otherwise, <see langword="false" />.</param>
		/// <param name="origin">The object to import into.</param>
		/// <returns>An import definition created from the specified parameters.</returns>
		// Token: 0x0600035C RID: 860 RVA: 0x0000A388 File Offset: 0x00008588
		public static ContractBasedImportDefinition CreateImportDefinition(Lazy<ParameterInfo> parameter, string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ImportCardinality cardinality, CreationPolicy requiredCreationPolicy, IDictionary<string, object> metadata, bool isExportFactory, ICompositionElement origin)
		{
			Requires.NotNull<Lazy<ParameterInfo>>(parameter, "parameter");
			Requires.NotNullOrEmpty(contractName, "contractName");
			if (isExportFactory)
			{
				return new PartCreatorParameterImportDefinition(parameter, origin, new ContractBasedImportDefinition(contractName, requiredTypeIdentity, requiredMetadata, cardinality, false, true, CreationPolicy.NonShared, metadata));
			}
			return new ReflectionParameterImportDefinition(parameter, contractName, requiredTypeIdentity, requiredMetadata, cardinality, requiredCreationPolicy, metadata, origin);
		}

		/// <summary>Indicates whether a generic part definition can be specialized with the provided parameters.</summary>
		/// <param name="partDefinition">The part definition.</param>
		/// <param name="genericParameters">A collection of types to specify the generic parameters.</param>
		/// <param name="specialization">When this method returns, contains the specialized part definition. This parameter is treated as uninitialized.</param>
		/// <returns>
		///   <see langword="true" /> if the specialization succeeds; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600035D RID: 861 RVA: 0x0000A3D8 File Offset: 0x000085D8
		public static bool TryMakeGenericPartDefinition(ComposablePartDefinition partDefinition, IEnumerable<Type> genericParameters, out ComposablePartDefinition specialization)
		{
			Requires.NotNull<ComposablePartDefinition>(partDefinition, "partDefinition");
			specialization = null;
			ReflectionComposablePartDefinition reflectionComposablePartDefinition = partDefinition as ReflectionComposablePartDefinition;
			if (reflectionComposablePartDefinition == null)
			{
				throw ExceptionBuilder.CreateReflectionModelInvalidPartDefinition("partDefinition", partDefinition.GetType());
			}
			return reflectionComposablePartDefinition.TryMakeGenericPartDefinition(genericParameters.ToArray<Type>(), out specialization);
		}
	}
}
