using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.AttributedModel;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Internal;

namespace System.ComponentModel.Composition
{
	/// <summary>Contains helper methods for using the MEF attributed programming model with composition.</summary>
	// Token: 0x0200001F RID: 31
	public static class AttributedModelServices
	{
		/// <summary>Gets a metadata view object from a dictionary of loose metadata.</summary>
		/// <param name="metadata">A collection of loose metadata.</param>
		/// <typeparam name="TMetadataView">The type of the metadata view object to get.</typeparam>
		/// <returns>A metadata view containing the specified metadata.</returns>
		// Token: 0x0600010E RID: 270 RVA: 0x00003F37 File Offset: 0x00002137
		public static TMetadataView GetMetadataView<TMetadataView>(IDictionary<string, object> metadata)
		{
			Requires.NotNull<IDictionary<string, object>>(metadata, "metadata");
			return MetadataViewProvider.GetMetadataView<TMetadataView>(metadata);
		}

		/// <summary>Creates a composable part from the specified attributed object.</summary>
		/// <param name="attributedPart">The attributed object.</param>
		/// <returns>The created part.</returns>
		// Token: 0x0600010F RID: 271 RVA: 0x00003F4A File Offset: 0x0000214A
		public static ComposablePart CreatePart(object attributedPart)
		{
			Requires.NotNull<object>(attributedPart, "attributedPart");
			return AttributedModelDiscovery.CreatePart(attributedPart);
		}

		/// <summary>Creates a composable part from the specified attributed object, using the specified reflection context.</summary>
		/// <param name="attributedPart">The attributed object.</param>
		/// <param name="reflectionContext">The reflection context for the part.</param>
		/// <returns>The created part.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reflectionContext" /> is <see langword="null" />.</exception>
		// Token: 0x06000110 RID: 272 RVA: 0x00003F5D File Offset: 0x0000215D
		public static ComposablePart CreatePart(object attributedPart, ReflectionContext reflectionContext)
		{
			Requires.NotNull<object>(attributedPart, "attributedPart");
			Requires.NotNull<ReflectionContext>(reflectionContext, "reflectionContext");
			return AttributedModelDiscovery.CreatePart(attributedPart, reflectionContext);
		}

		/// <summary>Creates a composable part from the specified attributed object, using the specified part definition.</summary>
		/// <param name="partDefinition">The definition of the new part.</param>
		/// <param name="attributedPart">The attributed object.</param>
		/// <returns>The created part.</returns>
		// Token: 0x06000111 RID: 273 RVA: 0x00003F7C File Offset: 0x0000217C
		public static ComposablePart CreatePart(ComposablePartDefinition partDefinition, object attributedPart)
		{
			Requires.NotNull<ComposablePartDefinition>(partDefinition, "partDefinition");
			Requires.NotNull<object>(attributedPart, "attributedPart");
			ReflectionComposablePartDefinition reflectionComposablePartDefinition = partDefinition as ReflectionComposablePartDefinition;
			if (reflectionComposablePartDefinition == null)
			{
				throw ExceptionBuilder.CreateReflectionModelInvalidPartDefinition("partDefinition", partDefinition.GetType());
			}
			return AttributedModelDiscovery.CreatePart(reflectionComposablePartDefinition, attributedPart);
		}

		/// <summary>Creates a part definition with the specified type and origin.</summary>
		/// <param name="type">The type of the definition.</param>
		/// <param name="origin">The origin of the definition.</param>
		/// <returns>The new part definition.</returns>
		// Token: 0x06000112 RID: 274 RVA: 0x00003FB4 File Offset: 0x000021B4
		public static ComposablePartDefinition CreatePartDefinition(Type type, ICompositionElement origin)
		{
			Requires.NotNull<Type>(type, "type");
			return AttributedModelServices.CreatePartDefinition(type, origin, false);
		}

		/// <summary>Creates a part definition with the specified type and origin.</summary>
		/// <param name="type">The type of the definition.</param>
		/// <param name="origin">The origin of the definition.</param>
		/// <param name="ensureIsDiscoverable">A value indicating whether or not the new definition should be discoverable.</param>
		/// <returns>The new part definition.</returns>
		// Token: 0x06000113 RID: 275 RVA: 0x00003FC9 File Offset: 0x000021C9
		public static ComposablePartDefinition CreatePartDefinition(Type type, ICompositionElement origin, bool ensureIsDiscoverable)
		{
			Requires.NotNull<Type>(type, "type");
			if (ensureIsDiscoverable)
			{
				return AttributedModelDiscovery.CreatePartDefinitionIfDiscoverable(type, origin);
			}
			return AttributedModelDiscovery.CreatePartDefinition(type, null, false, origin);
		}

		/// <summary>Gets the unique identifier for the specified type.</summary>
		/// <param name="type">The type to examine.</param>
		/// <returns>The unique identifier for the type.</returns>
		// Token: 0x06000114 RID: 276 RVA: 0x00003FEA File Offset: 0x000021EA
		public static string GetTypeIdentity(Type type)
		{
			Requires.NotNull<Type>(type, "type");
			return ContractNameServices.GetTypeIdentity(type);
		}

		/// <summary>Gets the unique identifier for the specified method.</summary>
		/// <param name="method">The method to examine.</param>
		/// <returns>The unique identifier for the method.</returns>
		// Token: 0x06000115 RID: 277 RVA: 0x00003FFD File Offset: 0x000021FD
		public static string GetTypeIdentity(MethodInfo method)
		{
			Requires.NotNull<MethodInfo>(method, "method");
			return ContractNameServices.GetTypeIdentityFromMethod(method);
		}

		/// <summary>Gets a canonical contract name for the specified type.</summary>
		/// <param name="type">The type to use.</param>
		/// <returns>A contract name created from the specified type.</returns>
		// Token: 0x06000116 RID: 278 RVA: 0x00004010 File Offset: 0x00002210
		public static string GetContractName(Type type)
		{
			Requires.NotNull<Type>(type, "type");
			return AttributedModelServices.GetTypeIdentity(type);
		}

		/// <summary>Creates a part from the specified value and adds it to the specified batch.</summary>
		/// <param name="batch">The batch to add to.</param>
		/// <param name="exportedValue">The value to add.</param>
		/// <typeparam name="T">The type of the new part.</typeparam>
		/// <returns>The new part.</returns>
		// Token: 0x06000117 RID: 279 RVA: 0x00004024 File Offset: 0x00002224
		public static ComposablePart AddExportedValue<T>(this CompositionBatch batch, T exportedValue)
		{
			Requires.NotNull<CompositionBatch>(batch, "batch");
			string contractName = AttributedModelServices.GetContractName(typeof(T));
			return batch.AddExportedValue(contractName, exportedValue);
		}

		/// <summary>Creates a part from the specified value and composes it in the specified composition container.</summary>
		/// <param name="container">The composition container to perform composition in.</param>
		/// <param name="exportedValue">The value to compose.</param>
		/// <typeparam name="T">The type of the new part.</typeparam>
		// Token: 0x06000118 RID: 280 RVA: 0x00004054 File Offset: 0x00002254
		public static void ComposeExportedValue<T>(this CompositionContainer container, T exportedValue)
		{
			Requires.NotNull<CompositionContainer>(container, "container");
			CompositionBatch batch = new CompositionBatch();
			batch.AddExportedValue(exportedValue);
			container.Compose(batch);
		}

		/// <summary>Creates a part from the specified value and adds it to the specified batch with the specified contract name.</summary>
		/// <param name="batch">The batch to add to.</param>
		/// <param name="contractName">The contract name of the export.</param>
		/// <param name="exportedValue">The value to add.</param>
		/// <typeparam name="T">The type of the new part.</typeparam>
		/// <returns>The new part.</returns>
		// Token: 0x06000119 RID: 281 RVA: 0x00004084 File Offset: 0x00002284
		public static ComposablePart AddExportedValue<T>(this CompositionBatch batch, string contractName, T exportedValue)
		{
			Requires.NotNull<CompositionBatch>(batch, "batch");
			string typeIdentity = AttributedModelServices.GetTypeIdentity(typeof(T));
			return batch.AddExport(new Export(contractName, new Dictionary<string, object>
			{
				{
					"ExportTypeIdentity",
					typeIdentity
				}
			}, () => exportedValue));
		}

		/// <summary>Creates a part from the specified object under the specified contract name and composes it in the specified composition container.</summary>
		/// <param name="container">The composition container to perform composition in.</param>
		/// <param name="contractName">The contract name to export the part under.</param>
		/// <param name="exportedValue">The value to compose.</param>
		/// <typeparam name="T">The type of the new part.</typeparam>
		// Token: 0x0600011A RID: 282 RVA: 0x000040E4 File Offset: 0x000022E4
		public static void ComposeExportedValue<T>(this CompositionContainer container, string contractName, T exportedValue)
		{
			Requires.NotNull<CompositionContainer>(container, "container");
			CompositionBatch batch = new CompositionBatch();
			batch.AddExportedValue(contractName, exportedValue);
			container.Compose(batch);
		}

		/// <summary>Creates a composable part from the specified attributed object, and adds it to the specified composition batch.</summary>
		/// <param name="batch">The batch to add to.</param>
		/// <param name="attributedPart">The object to add.</param>
		/// <returns>The new part.</returns>
		// Token: 0x0600011B RID: 283 RVA: 0x00004114 File Offset: 0x00002314
		public static ComposablePart AddPart(this CompositionBatch batch, object attributedPart)
		{
			Requires.NotNull<CompositionBatch>(batch, "batch");
			Requires.NotNull<object>(attributedPart, "attributedPart");
			ComposablePart composablePart = AttributedModelServices.CreatePart(attributedPart);
			batch.AddPart(composablePart);
			return composablePart;
		}

		/// <summary>Creates composable parts from an array of attributed objects and composes them in the specified composition container.</summary>
		/// <param name="container">The composition container to perform composition in.</param>
		/// <param name="attributedParts">An array of attributed objects to compose.</param>
		// Token: 0x0600011C RID: 284 RVA: 0x00004148 File Offset: 0x00002348
		public static void ComposeParts(this CompositionContainer container, params object[] attributedParts)
		{
			Requires.NotNull<CompositionContainer>(container, "container");
			Requires.NotNullOrNullElements<object>(attributedParts, "attributedParts");
			CompositionBatch batch = new CompositionBatch((from attributedPart in attributedParts
			select AttributedModelServices.CreatePart(attributedPart)).ToArray<ComposablePart>(), Enumerable.Empty<ComposablePart>());
			container.Compose(batch);
		}

		/// <summary>Composes the specified part by using the specified composition service, with recomposition disabled.</summary>
		/// <param name="compositionService">The composition service to use.</param>
		/// <param name="attributedPart">The part to compose.</param>
		/// <returns>The composed part.</returns>
		// Token: 0x0600011D RID: 285 RVA: 0x000041A8 File Offset: 0x000023A8
		public static ComposablePart SatisfyImportsOnce(this ICompositionService compositionService, object attributedPart)
		{
			Requires.NotNull<ICompositionService>(compositionService, "compositionService");
			Requires.NotNull<object>(attributedPart, "attributedPart");
			ComposablePart composablePart = AttributedModelServices.CreatePart(attributedPart);
			compositionService.SatisfyImportsOnce(composablePart);
			return composablePart;
		}

		/// <summary>Composes the specified part by using the specified composition service, with recomposition disabled and using the specified reflection context.</summary>
		/// <param name="compositionService">The composition service to use.</param>
		/// <param name="attributedPart">The part to compose.</param>
		/// <param name="reflectionContext">The reflection context for the part.</param>
		/// <returns>The composed part.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reflectionContext" /> is <see langword="null" />.</exception>
		// Token: 0x0600011E RID: 286 RVA: 0x000041DC File Offset: 0x000023DC
		public static ComposablePart SatisfyImportsOnce(this ICompositionService compositionService, object attributedPart, ReflectionContext reflectionContext)
		{
			Requires.NotNull<ICompositionService>(compositionService, "compositionService");
			Requires.NotNull<object>(attributedPart, "attributedPart");
			Requires.NotNull<ReflectionContext>(reflectionContext, "reflectionContext");
			ComposablePart composablePart = AttributedModelServices.CreatePart(attributedPart, reflectionContext);
			compositionService.SatisfyImportsOnce(composablePart);
			return composablePart;
		}

		/// <summary>Returns a value that indicates whether the specified part contains an export that matches the specified contract type.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="contractType">The contract type.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> contains an export definition that matches <paramref name="contractType" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600011F RID: 287 RVA: 0x0000421A File Offset: 0x0000241A
		public static bool Exports(this ComposablePartDefinition part, Type contractType)
		{
			Requires.NotNull<ComposablePartDefinition>(part, "part");
			Requires.NotNull<Type>(contractType, "contractType");
			return part.Exports(AttributedModelServices.GetContractName(contractType));
		}

		/// <summary>Returns a value that indicates whether the specified part contains an export that matches the specified contract type.</summary>
		/// <param name="part">The part to search.</param>
		/// <typeparam name="T">The contract type.</typeparam>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> contains an export definition of type <paramref name="T" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000120 RID: 288 RVA: 0x0000423E File Offset: 0x0000243E
		public static bool Exports<T>(this ComposablePartDefinition part)
		{
			Requires.NotNull<ComposablePartDefinition>(part, "part");
			return part.Exports(typeof(T));
		}

		/// <summary>Returns a value that indicates whether the specified part contains an import that matches the specified contract type.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="contractType">The contract type.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> contains an import definition that matches <paramref name="contractType" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000121 RID: 289 RVA: 0x0000425B File Offset: 0x0000245B
		public static bool Imports(this ComposablePartDefinition part, Type contractType)
		{
			Requires.NotNull<ComposablePartDefinition>(part, "part");
			Requires.NotNull<Type>(contractType, "contractType");
			return part.Imports(AttributedModelServices.GetContractName(contractType));
		}

		/// <summary>Returns a value that indicates whether the specified part contains an import that matches the specified contract type.</summary>
		/// <param name="part">The part to search.</param>
		/// <typeparam name="T">The contract type.</typeparam>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> contains an import definition of type <paramref name="T" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000122 RID: 290 RVA: 0x0000427F File Offset: 0x0000247F
		public static bool Imports<T>(this ComposablePartDefinition part)
		{
			Requires.NotNull<ComposablePartDefinition>(part, "part");
			return part.Imports(typeof(T));
		}

		/// <summary>Returns a value that indicates whether the specified part contains an import that matches the specified contract type and import cardinality.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="contractType">The contract type.</param>
		/// <param name="importCardinality">The import cardinality.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> contains an import definition that matches <paramref name="contractType" /> and <paramref name="importCardinality" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000123 RID: 291 RVA: 0x0000429C File Offset: 0x0000249C
		public static bool Imports(this ComposablePartDefinition part, Type contractType, ImportCardinality importCardinality)
		{
			Requires.NotNull<ComposablePartDefinition>(part, "part");
			Requires.NotNull<Type>(contractType, "contractType");
			return part.Imports(AttributedModelServices.GetContractName(contractType), importCardinality);
		}

		/// <summary>Returns a value that indicates whether the specified part contains an import that matches the specified contract type and import cardinality.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="importCardinality">The import cardinality.</param>
		/// <typeparam name="T">The contract type.</typeparam>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> contains an import definition of type <paramref name="T" /> that has the specified import cardinality; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000124 RID: 292 RVA: 0x000042C1 File Offset: 0x000024C1
		public static bool Imports<T>(this ComposablePartDefinition part, ImportCardinality importCardinality)
		{
			Requires.NotNull<ComposablePartDefinition>(part, "part");
			return part.Imports(typeof(T), importCardinality);
		}

		// Token: 0x02000020 RID: 32
		[CompilerGenerated]
		private sealed class <>c__DisplayClass11_0<T>
		{
			// Token: 0x06000125 RID: 293 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass11_0()
			{
			}

			// Token: 0x06000126 RID: 294 RVA: 0x000042DF File Offset: 0x000024DF
			internal object <AddExportedValue>b__0()
			{
				return this.exportedValue;
			}

			// Token: 0x04000068 RID: 104
			public T exportedValue;
		}

		// Token: 0x02000021 RID: 33
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000127 RID: 295 RVA: 0x000042EC File Offset: 0x000024EC
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000128 RID: 296 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c()
			{
			}

			// Token: 0x06000129 RID: 297 RVA: 0x000042F8 File Offset: 0x000024F8
			internal ComposablePart <ComposeParts>b__14_0(object attributedPart)
			{
				return AttributedModelServices.CreatePart(attributedPart);
			}

			// Token: 0x04000069 RID: 105
			public static readonly AttributedModelServices.<>c <>9 = new AttributedModelServices.<>c();

			// Token: 0x0400006A RID: 106
			public static Func<object, ComposablePart> <>9__14_0;
		}
	}
}
