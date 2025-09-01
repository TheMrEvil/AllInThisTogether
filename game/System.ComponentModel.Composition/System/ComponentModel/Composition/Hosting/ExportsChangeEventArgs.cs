using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Internal;
using Microsoft.Internal.Collections;

namespace System.ComponentModel.Composition.Hosting
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.Composition.Hosting.ExportProvider.ExportsChanging" /> and <see cref="E:System.ComponentModel.Composition.Hosting.ExportProvider.ExportsChanged" /> event.</summary>
	// Token: 0x020000DF RID: 223
	public class ExportsChangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ExportsChangeEventArgs" /> class.</summary>
		/// <param name="addedExports">The events that were added.</param>
		/// <param name="removedExports">The events that were removed.</param>
		/// <param name="atomicComposition">The composition transaction that contains the change.</param>
		// Token: 0x060005F0 RID: 1520 RVA: 0x00011FF8 File Offset: 0x000101F8
		public ExportsChangeEventArgs(IEnumerable<ExportDefinition> addedExports, IEnumerable<ExportDefinition> removedExports, AtomicComposition atomicComposition)
		{
			Requires.NotNull<IEnumerable<ExportDefinition>>(addedExports, "addedExports");
			Requires.NotNull<IEnumerable<ExportDefinition>>(removedExports, "removedExports");
			this._addedExports = addedExports.AsArray<ExportDefinition>();
			this._removedExports = removedExports.AsArray<ExportDefinition>();
			this.AtomicComposition = atomicComposition;
		}

		/// <summary>Gets the exports that were added in this change.</summary>
		/// <returns>A collection of the exports that were added.</returns>
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00012035 File Offset: 0x00010235
		public IEnumerable<ExportDefinition> AddedExports
		{
			get
			{
				return this._addedExports;
			}
		}

		/// <summary>Gets the exports that were removed in the change.</summary>
		/// <returns>A collection of the removed exports.</returns>
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x0001203D File Offset: 0x0001023D
		public IEnumerable<ExportDefinition> RemovedExports
		{
			get
			{
				return this._removedExports;
			}
		}

		/// <summary>Gets the contract names that were altered in the change.</summary>
		/// <returns>A collection of the altered contract names.</returns>
		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x00012048 File Offset: 0x00010248
		public IEnumerable<string> ChangedContractNames
		{
			get
			{
				if (this._changedContractNames == null)
				{
					this._changedContractNames = (from export in this.AddedExports.Concat(this.RemovedExports)
					select export.ContractName).Distinct<string>().ToArray<string>();
				}
				return this._changedContractNames;
			}
		}

		/// <summary>Gets the composition transaction of the change, if any.</summary>
		/// <returns>A reference to the composition transaction associated with the change, or <see langword="null" /> if no transaction is being used.</returns>
		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x000120A8 File Offset: 0x000102A8
		// (set) Token: 0x060005F5 RID: 1525 RVA: 0x000120B0 File Offset: 0x000102B0
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

		// Token: 0x04000283 RID: 643
		private readonly IEnumerable<ExportDefinition> _addedExports;

		// Token: 0x04000284 RID: 644
		private readonly IEnumerable<ExportDefinition> _removedExports;

		// Token: 0x04000285 RID: 645
		private IEnumerable<string> _changedContractNames;

		// Token: 0x04000286 RID: 646
		[CompilerGenerated]
		private AtomicComposition <AtomicComposition>k__BackingField;

		// Token: 0x020000E0 RID: 224
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060005F6 RID: 1526 RVA: 0x000120B9 File Offset: 0x000102B9
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060005F7 RID: 1527 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c()
			{
			}

			// Token: 0x060005F8 RID: 1528 RVA: 0x000120C5 File Offset: 0x000102C5
			internal string <get_ChangedContractNames>b__9_0(ExportDefinition export)
			{
				return export.ContractName;
			}

			// Token: 0x04000287 RID: 647
			public static readonly ExportsChangeEventArgs.<>c <>9 = new ExportsChangeEventArgs.<>c();

			// Token: 0x04000288 RID: 648
			public static Func<ExportDefinition, string> <>9__9_0;
		}
	}
}
