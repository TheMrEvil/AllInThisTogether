using System;
using System.Collections.Generic;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Primitives
{
	/// <summary>Describes the contract that a particular <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object satisfies.</summary>
	// Token: 0x02000094 RID: 148
	public class ExportDefinition
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> class.</summary>
		// Token: 0x060003EC RID: 1004 RVA: 0x0000B385 File Offset: 0x00009585
		protected ExportDefinition()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> class with the specified contract name and metadata.</summary>
		/// <param name="contractName">The contract name of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> object.</param>
		/// <param name="metadata">The metadata of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.Primitives.ExportDefinition.Metadata" /> property to an empty, read-only <see cref="T:System.Collections.Generic.IDictionary`2" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contractName" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contractName" /> is an empty string ("").</exception>
		// Token: 0x060003ED RID: 1005 RVA: 0x0000B398 File Offset: 0x00009598
		public ExportDefinition(string contractName, IDictionary<string, object> metadata)
		{
			Requires.NotNullOrEmpty(contractName, "contractName");
			this._contractName = contractName;
			if (metadata != null)
			{
				this._metadata = metadata.AsReadOnly();
			}
		}

		/// <summary>Gets the contract name.</summary>
		/// <returns>The contract name of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> object.</returns>
		/// <exception cref="T:System.NotImplementedException">The property was not overridden by a derived class.</exception>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000B3CC File Offset: 0x000095CC
		public virtual string ContractName
		{
			get
			{
				if (this._contractName != null)
				{
					return this._contractName;
				}
				throw ExceptionBuilder.CreateNotOverriddenByDerived("ContractName");
			}
		}

		/// <summary>Gets the contract metadata.</summary>
		/// <returns>The metadata of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" />. The default is an empty, read-only <see cref="T:System.Collections.Generic.IDictionary`2" /> object.</returns>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000B3E7 File Offset: 0x000095E7
		public virtual IDictionary<string, object> Metadata
		{
			get
			{
				return this._metadata;
			}
		}

		/// <summary>Returns a string representation of the export definition.</summary>
		/// <returns>A string representation of the export definition.</returns>
		// Token: 0x060003F0 RID: 1008 RVA: 0x0000B3EF File Offset: 0x000095EF
		public override string ToString()
		{
			return this.ContractName;
		}

		// Token: 0x04000185 RID: 389
		private readonly IDictionary<string, object> _metadata = MetadataServices.EmptyMetadata;

		// Token: 0x04000186 RID: 390
		private readonly string _contractName;
	}
}
