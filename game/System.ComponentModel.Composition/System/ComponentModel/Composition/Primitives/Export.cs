using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Primitives
{
	/// <summary>Represents an export, which is a type that consists of a delay-created exported object and the metadata that describes that object.</summary>
	// Token: 0x02000093 RID: 147
	public class Export
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> class.</summary>
		// Token: 0x060003E3 RID: 995 RVA: 0x0000B284 File Offset: 0x00009484
		protected Export()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> class with the specified contract name and exported value getter.</summary>
		/// <param name="contractName">The contract name of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object.</param>
		/// <param name="exportedValueGetter">A method that is called to create the exported object of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" />. This delays the creation of the object until the <see cref="P:System.ComponentModel.Composition.Primitives.Export.Value" /> method is called.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contractName" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="exportedObjectGetter" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contractName" /> is an empty string ("").</exception>
		// Token: 0x060003E4 RID: 996 RVA: 0x0000B299 File Offset: 0x00009499
		public Export(string contractName, Func<object> exportedValueGetter) : this(new ExportDefinition(contractName, null), exportedValueGetter)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> class with the specified contract name, metadata, and exported value getter.</summary>
		/// <param name="contractName">The contract name of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object.</param>
		/// <param name="metadata">The metadata of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object or <see langword="null" /> to set the <see cref="P:System.ComponentModel.Composition.Primitives.Export.Metadata" /> property to an empty, read-only <see cref="T:System.Collections.Generic.IDictionary`2" /> object.</param>
		/// <param name="exportedValueGetter">A method that is called to create the exported object of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" />. This delays the creation of the object until the <see cref="P:System.ComponentModel.Composition.Primitives.Export.Value" /> method is called.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contractName" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="exportedObjectGetter" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contractName" /> is an empty string ("").</exception>
		// Token: 0x060003E5 RID: 997 RVA: 0x0000B2A9 File Offset: 0x000094A9
		public Export(string contractName, IDictionary<string, object> metadata, Func<object> exportedValueGetter) : this(new ExportDefinition(contractName, metadata), exportedValueGetter)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> class with the specified export definition and exported object getter.</summary>
		/// <param name="definition">An object that describes the contract that the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object satisfies.</param>
		/// <param name="exportedValueGetter">A method that is called to create the exported object of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" />. This delays the creation of the object until the <see cref="P:System.ComponentModel.Composition.Primitives.Export.Value" /> property is called.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="definition" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="exportedObjectGetter" /> is <see langword="null" />.</exception>
		// Token: 0x060003E6 RID: 998 RVA: 0x0000B2B9 File Offset: 0x000094B9
		public Export(ExportDefinition definition, Func<object> exportedValueGetter)
		{
			Requires.NotNull<ExportDefinition>(definition, "definition");
			Requires.NotNull<Func<object>>(exportedValueGetter, "exportedValueGetter");
			this._definition = definition;
			this._exportedValueGetter = exportedValueGetter;
		}

		/// <summary>Gets the definition that describes the contract that the export satisfies.</summary>
		/// <returns>A definition that describes the contract that the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> object satisfies.</returns>
		/// <exception cref="T:System.NotImplementedException">This property was not overridden by a derived class.</exception>
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000B2F2 File Offset: 0x000094F2
		public virtual ExportDefinition Definition
		{
			get
			{
				if (this._definition != null)
				{
					return this._definition;
				}
				throw ExceptionBuilder.CreateNotOverriddenByDerived("Definition");
			}
		}

		/// <summary>Gets the metadata for the export.</summary>
		/// <returns>The metadata of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" />.</returns>
		/// <exception cref="T:System.NotImplementedException">The <see cref="P:System.ComponentModel.Composition.Primitives.Export.Definition" /> property was not overridden by a derived class.</exception>
		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000B30D File Offset: 0x0000950D
		public IDictionary<string, object> Metadata
		{
			get
			{
				return this.Definition.Metadata;
			}
		}

		/// <summary>Provides the object this export represents.</summary>
		/// <returns>The object this export represents.</returns>
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000B31C File Offset: 0x0000951C
		public object Value
		{
			get
			{
				if (this._exportedValue == Export._EmptyValue)
				{
					object exportedValueCore = this.GetExportedValueCore();
					Interlocked.CompareExchange(ref this._exportedValue, exportedValueCore, Export._EmptyValue);
				}
				return this._exportedValue;
			}
		}

		/// <summary>Returns the exported object the export provides.</summary>
		/// <returns>The exported object the export provides.</returns>
		/// <exception cref="T:System.NotImplementedException">The <see cref="M:System.ComponentModel.Composition.Primitives.Export.GetExportedValueCore" /> method was not overridden by a derived class.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.CompositionException">An error occurred during composition. <see cref="P:System.ComponentModel.Composition.CompositionException.Errors" /> will contain a collection of errors that occurred.</exception>
		// Token: 0x060003EA RID: 1002 RVA: 0x0000B359 File Offset: 0x00009559
		protected virtual object GetExportedValueCore()
		{
			if (this._exportedValueGetter != null)
			{
				return this._exportedValueGetter();
			}
			throw ExceptionBuilder.CreateNotOverriddenByDerived("GetExportedValueCore");
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000B379 File Offset: 0x00009579
		// Note: this type is marked as 'beforefieldinit'.
		static Export()
		{
		}

		// Token: 0x04000181 RID: 385
		private readonly ExportDefinition _definition;

		// Token: 0x04000182 RID: 386
		private readonly Func<object> _exportedValueGetter;

		// Token: 0x04000183 RID: 387
		private static readonly object _EmptyValue = new object();

		// Token: 0x04000184 RID: 388
		private volatile object _exportedValue = Export._EmptyValue;
	}
}
