using System;
using System.ComponentModel.Composition.Primitives;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Hosting
{
	/// <summary>Defines static convenience methods for scoping.</summary>
	// Token: 0x020000F8 RID: 248
	public static class ScopingExtensions
	{
		/// <summary>Gets a value that indicates whether the specified part exports the specified contract.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="contractName">The name of the contract.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> exports the specified contract; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000686 RID: 1670 RVA: 0x0001423C File Offset: 0x0001243C
		public static bool Exports(this ComposablePartDefinition part, string contractName)
		{
			Requires.NotNull<ComposablePartDefinition>(part, "part");
			Requires.NotNull<string>(contractName, "contractName");
			foreach (ExportDefinition exportDefinition in part.ExportDefinitions)
			{
				if (StringComparers.ContractName.Equals(contractName, exportDefinition.ContractName))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the specified part imports the specified contract.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="contractName">The name of the contract.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> imports the specified contract; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000687 RID: 1671 RVA: 0x000142B4 File Offset: 0x000124B4
		public static bool Imports(this ComposablePartDefinition part, string contractName)
		{
			Requires.NotNull<ComposablePartDefinition>(part, "part");
			Requires.NotNull<string>(contractName, "contractName");
			foreach (ImportDefinition importDefinition in part.ImportDefinitions)
			{
				if (StringComparers.ContractName.Equals(contractName, importDefinition.ContractName))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Determines whether the specified part imports the specified contract with the specified cardinality.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="contractName">The name of the contract.</param>
		/// <param name="importCardinality">The cardinality of the contract.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> imports a contract that has the specified name and cardinality; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000688 RID: 1672 RVA: 0x0001432C File Offset: 0x0001252C
		public static bool Imports(this ComposablePartDefinition part, string contractName, ImportCardinality importCardinality)
		{
			Requires.NotNull<ComposablePartDefinition>(part, "part");
			Requires.NotNull<string>(contractName, "contractName");
			foreach (ImportDefinition importDefinition in part.ImportDefinitions)
			{
				if (StringComparers.ContractName.Equals(contractName, importDefinition.ContractName) && importDefinition.Cardinality == importCardinality)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Gets a value that indicates whether the specified part contains metadata that has the specified key.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="key">The metadata key.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> contains metadata that has the specified key; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000689 RID: 1673 RVA: 0x000143AC File Offset: 0x000125AC
		public static bool ContainsPartMetadataWithKey(this ComposablePartDefinition part, string key)
		{
			Requires.NotNull<ComposablePartDefinition>(part, "part");
			Requires.NotNull<string>(key, "key");
			return part.Metadata.ContainsKey(key);
		}

		/// <summary>Gets a value that indicates whether the specified part contains metadata that has the specified key and value.</summary>
		/// <param name="part">The part to search.</param>
		/// <param name="key">The metadata key.</param>
		/// <param name="value">The metadata value.</param>
		/// <typeparam name="T">The type of the metadata value.</typeparam>
		/// <returns>
		///   <see langword="true" /> if <paramref name="part" /> contains metadata that has the specified key, value type, and value; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600068A RID: 1674 RVA: 0x000143D0 File Offset: 0x000125D0
		public static bool ContainsPartMetadata<T>(this ComposablePartDefinition part, string key, T value)
		{
			Requires.NotNull<ComposablePartDefinition>(part, "part");
			Requires.NotNull<string>(key, "key");
			object obj = null;
			if (!part.Metadata.TryGetValue(key, out obj))
			{
				return false;
			}
			if (value == null)
			{
				return obj == null;
			}
			return value.Equals(obj);
		}

		/// <summary>Filters the specified catalog with the specified filter function.</summary>
		/// <param name="catalog">The catalog to filter.</param>
		/// <param name="filter">The filter function.</param>
		/// <returns>A new catalog filtered by using the specified filter.</returns>
		// Token: 0x0600068B RID: 1675 RVA: 0x00014422 File Offset: 0x00012622
		public static FilteredCatalog Filter(this ComposablePartCatalog catalog, Func<ComposablePartDefinition, bool> filter)
		{
			Requires.NotNull<ComposablePartCatalog>(catalog, "catalog");
			Requires.NotNull<Func<ComposablePartDefinition, bool>>(filter, "filter");
			return new FilteredCatalog(catalog, filter);
		}
	}
}
