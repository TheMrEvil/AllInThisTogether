using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Runtime.CompilerServices;
using Microsoft.Internal;
using Microsoft.Internal.Collections;

namespace System.ComponentModel.Composition.Hosting
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Composition.Hosting.INotifyComposablePartCatalogChanged.Changed" /> event.</summary>
	// Token: 0x020000BE RID: 190
	public class ComposablePartCatalogChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartCatalogChangeEventArgs" /> class with the specified changes.</summary>
		/// <param name="addedDefinitions">The part definitions that were added to the catalog.</param>
		/// <param name="removedDefinitions">The part definitions that were removed from the catalog.</param>
		/// <param name="atomicComposition">The composition transaction to use, or <see langword="null" /> to disable transactional composition.</param>
		// Token: 0x060004DB RID: 1243 RVA: 0x0000E372 File Offset: 0x0000C572
		public ComposablePartCatalogChangeEventArgs(IEnumerable<ComposablePartDefinition> addedDefinitions, IEnumerable<ComposablePartDefinition> removedDefinitions, AtomicComposition atomicComposition)
		{
			Requires.NotNull<IEnumerable<ComposablePartDefinition>>(addedDefinitions, "addedDefinitions");
			Requires.NotNull<IEnumerable<ComposablePartDefinition>>(removedDefinitions, "removedDefinitions");
			this._addedDefinitions = addedDefinitions.AsArray<ComposablePartDefinition>();
			this._removedDefinitions = removedDefinitions.AsArray<ComposablePartDefinition>();
			this.AtomicComposition = atomicComposition;
		}

		/// <summary>Gets a collection of definitions added to the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> in this change.</summary>
		/// <returns>A collection of definitions added to the catalog.</returns>
		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x0000E3AF File Offset: 0x0000C5AF
		public IEnumerable<ComposablePartDefinition> AddedDefinitions
		{
			get
			{
				return this._addedDefinitions;
			}
		}

		/// <summary>Gets a collection of definitions removed from the <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> in this change.</summary>
		/// <returns>A collection of definitions removed from the catalog in this change.</returns>
		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0000E3B7 File Offset: 0x0000C5B7
		public IEnumerable<ComposablePartDefinition> RemovedDefinitions
		{
			get
			{
				return this._removedDefinitions;
			}
		}

		/// <summary>Gets the composition transaction for this change.</summary>
		/// <returns>The composition transaction for this change.</returns>
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x0000E3BF File Offset: 0x0000C5BF
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x0000E3C7 File Offset: 0x0000C5C7
		public AtomicComposition AtomicComposition
		{
			[CompilerGenerated]
			get
			{
				return this.<AtomicComposition>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<AtomicComposition>k__BackingField = value;
			}
		}

		// Token: 0x040001FF RID: 511
		private readonly IEnumerable<ComposablePartDefinition> _addedDefinitions;

		// Token: 0x04000200 RID: 512
		private readonly IEnumerable<ComposablePartDefinition> _removedDefinitions;

		// Token: 0x04000201 RID: 513
		[CompilerGenerated]
		private AtomicComposition <AtomicComposition>k__BackingField;
	}
}
