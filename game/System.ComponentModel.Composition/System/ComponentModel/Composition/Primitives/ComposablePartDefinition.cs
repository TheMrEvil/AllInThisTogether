using System;
using System.Collections.Generic;
using System.Linq;

namespace System.ComponentModel.Composition.Primitives
{
	/// <summary>Defines an abstract base class for composable part definitions, which describe and enable the creation of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> objects.</summary>
	// Token: 0x0200008C RID: 140
	public abstract class ComposablePartDefinition
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> class.</summary>
		// Token: 0x060003B3 RID: 947 RVA: 0x00002BAC File Offset: 0x00000DAC
		protected ComposablePartDefinition()
		{
		}

		/// <summary>Gets a collection of <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects that describe the objects exported by the part defined by this <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> object.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects that describe the exported objects provided by <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> objects created by the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" />.</returns>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003B4 RID: 948
		public abstract IEnumerable<ExportDefinition> ExportDefinitions { get; }

		/// <summary>Gets a collection of <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> objects that describe the imports required by the part defined by this <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> object.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> objects that describe the imports required by <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePart" /> objects created by the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" />.</returns>
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003B5 RID: 949
		public abstract IEnumerable<ImportDefinition> ImportDefinitions { get; }

		/// <summary>Gets a collection of the metadata for this <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> object.</summary>
		/// <returns>A collection that contains the metadata for the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" />. The default is an empty, read-only <see cref="T:System.Collections.Generic.IDictionary`2" /> object.</returns>
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000AB7D File Offset: 0x00008D7D
		public virtual IDictionary<string, object> Metadata
		{
			get
			{
				return MetadataServices.EmptyMetadata;
			}
		}

		/// <summary>Creates a new instance of a part that the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> describes.</summary>
		/// <returns>The created part.</returns>
		// Token: 0x060003B7 RID: 951
		public abstract ComposablePart CreatePart();

		// Token: 0x060003B8 RID: 952 RVA: 0x0000ACE8 File Offset: 0x00008EE8
		internal virtual IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
		{
			List<Tuple<ComposablePartDefinition, ExportDefinition>> list = null;
			foreach (ExportDefinition exportDefinition in this.ExportDefinitions)
			{
				if (definition.IsConstraintSatisfiedBy(exportDefinition))
				{
					if (list == null)
					{
						list = new List<Tuple<ComposablePartDefinition, ExportDefinition>>();
					}
					list.Add(new Tuple<ComposablePartDefinition, ExportDefinition>(this, exportDefinition));
				}
			}
			IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> enumerable = list;
			return enumerable ?? ComposablePartDefinition._EmptyExports;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000AD5C File Offset: 0x00008F5C
		internal virtual ComposablePartDefinition GetGenericPartDefinition()
		{
			return null;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000AD5F File Offset: 0x00008F5F
		// Note: this type is marked as 'beforefieldinit'.
		static ComposablePartDefinition()
		{
		}

		// Token: 0x04000176 RID: 374
		internal static readonly IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> _EmptyExports = Enumerable.Empty<Tuple<ComposablePartDefinition, ExportDefinition>>();
	}
}
