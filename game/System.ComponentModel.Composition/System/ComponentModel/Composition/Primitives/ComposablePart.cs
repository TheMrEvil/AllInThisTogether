using System;
using System.Collections.Generic;

namespace System.ComponentModel.Composition.Primitives
{
	/// <summary>Defines the abstract base class for composable parts, which import objects and produce exported objects.</summary>
	// Token: 0x02000089 RID: 137
	public abstract class ComposablePart
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> class.</summary>
		// Token: 0x060003A0 RID: 928 RVA: 0x00002BAC File Offset: 0x00000DAC
		protected ComposablePart()
		{
		}

		/// <summary>Gets a collection of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects that describe the exported objects provided by the part.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects that describe the exported objects provided by the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object has been disposed of.</exception>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003A1 RID: 929
		public abstract IEnumerable<ExportDefinition> ExportDefinitions { get; }

		/// <summary>Gets a collection of the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> objects that describe the imported objects required by the part.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> objects that describe the imported objects required by the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object has been disposed of.</exception>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003A2 RID: 930
		public abstract IEnumerable<ImportDefinition> ImportDefinitions { get; }

		/// <summary>Gets the metadata of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object.</summary>
		/// <returns>The metadata of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object. The default is an empty, read-only <see cref="T:System.Collections.Generic.IDictionary`2" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object has been disposed of.</exception>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000AB7D File Offset: 0x00008D7D
		public virtual IDictionary<string, object> Metadata
		{
			get
			{
				return MetadataServices.EmptyMetadata;
			}
		}

		/// <summary>Called when all the imports of the part have been set, and exports can be retrieved.</summary>
		// Token: 0x060003A4 RID: 932 RVA: 0x000028FF File Offset: 0x00000AFF
		public virtual void Activate()
		{
		}

		/// <summary>Gets the exported object described by the specified <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> object.</summary>
		/// <param name="definition">One of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects from the <see cref="P:System.ComponentModel.Composition.Primitives.ComposablePart.ExportDefinitions" /> property that describes the exported object to return.</param>
		/// <returns>The exported object described by <paramref name="definition" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object has been disposed of.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="definition" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException">An error occurred getting the exported object described by the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="definition" /> did not originate from the <see cref="P:System.ComponentModel.Composition.Primitives.ComposablePart.ExportDefinitions" /> property on the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">One or more prerequisite imports, indicated by <see cref="P:System.ComponentModel.Composition.Primitives.ImportDefinition.IsPrerequisite" />, have not been set.</exception>
		// Token: 0x060003A5 RID: 933
		public abstract object GetExportedValue(ExportDefinition definition);

		/// <summary>Sets the import described by the specified <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> object to be satisfied by the specified exports.</summary>
		/// <param name="definition">One of the objects from the <see cref="P:System.ComponentModel.Composition.Primitives.ComposablePart.ImportDefinitions" /> property that specifies the import to be set.</param>
		/// <param name="exports">A collection of <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects of which to set the import described by <paramref name="definition" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> object has been disposed of.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="definition" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="exports" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ComponentModel.Composition.Primitives.ComposablePartException">An error occurred setting the import described by the <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> object.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="definition" /> did not originate from the <see cref="P:System.ComponentModel.Composition.Primitives.ComposablePart.ImportDefinitions" /> property on the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" />.  
		/// -or-  
		/// <paramref name="exports" /> contains an element that is <see langword="null" />.  
		/// -or-  
		/// <paramref name="exports" /> is empty and <see cref="P:System.ComponentModel.Composition.Primitives.ImportDefinition.Cardinality" /> is <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ExactlyOne" />.  
		/// -or-  
		/// <paramref name="exports" /> contains more than one element and <see cref="P:System.ComponentModel.Composition.Primitives.ImportDefinition.Cardinality" /> is <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ZeroOrOne" /> or <see cref="F:System.ComponentModel.Composition.Primitives.ImportCardinality.ExactlyOne" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.ComponentModel.Composition.Primitives.ComposablePart.SetImport(System.ComponentModel.Composition.Primitives.ImportDefinition,System.Collections.Generic.IEnumerable{System.ComponentModel.Composition.Primitives.Export})" /> has been previously called and <see cref="P:System.ComponentModel.Composition.Primitives.ImportDefinition.IsRecomposable" /> is <see langword="false" />.</exception>
		// Token: 0x060003A6 RID: 934
		public abstract void SetImport(ImportDefinition definition, IEnumerable<Export> exports);
	}
}
