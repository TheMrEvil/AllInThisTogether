using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Internal;
using Microsoft.Internal.Collections;

namespace System.ComponentModel.Composition.Hosting
{
	/// <summary>Represents a catalog after a filter function is applied to it.</summary>
	// Token: 0x020000E1 RID: 225
	public class FilteredCatalog : ComposablePartCatalog, INotifyComposablePartCatalogChanged
	{
		/// <summary>Gets a new <see cref="T:System.ComponentModel.Composition.Hosting.FilteredCatalog" /> object that contains all the parts from this catalog and all their dependencies.</summary>
		/// <returns>The new catalog.</returns>
		// Token: 0x060005F9 RID: 1529 RVA: 0x000120CD File Offset: 0x000102CD
		public FilteredCatalog IncludeDependencies()
		{
			return this.IncludeDependencies((ImportDefinition i) => i.Cardinality == ImportCardinality.ExactlyOne);
		}

		/// <summary>Gets a new <see cref="T:System.ComponentModel.Composition.Hosting.FilteredCatalog" /> object that contains all the parts from this catalog and all dependencies that can be reached through imports that match the specified filter.</summary>
		/// <param name="importFilter">The filter for imports.</param>
		/// <returns>The new catalog.</returns>
		// Token: 0x060005FA RID: 1530 RVA: 0x000120F4 File Offset: 0x000102F4
		public FilteredCatalog IncludeDependencies(Func<ImportDefinition, bool> importFilter)
		{
			Requires.NotNull<Func<ImportDefinition, bool>>(importFilter, "importFilter");
			this.ThrowIfDisposed();
			return this.Traverse(new FilteredCatalog.DependenciesTraversal(this, importFilter));
		}

		/// <summary>Gets a new <see cref="T:System.ComponentModel.Composition.Hosting.FilteredCatalog" /> object that contains all the parts from this catalog and all their dependents.</summary>
		/// <returns>The new catalog.</returns>
		// Token: 0x060005FB RID: 1531 RVA: 0x00012114 File Offset: 0x00010314
		public FilteredCatalog IncludeDependents()
		{
			return this.IncludeDependents((ImportDefinition i) => i.Cardinality == ImportCardinality.ExactlyOne);
		}

		/// <summary>Gets a new <see cref="T:System.ComponentModel.Composition.Hosting.FilteredCatalog" /> object that contains all the parts from this catalog and all dependents that can be reached through imports that match the specified filter.</summary>
		/// <param name="importFilter">The filter for imports.</param>
		/// <returns>The new catalog.</returns>
		// Token: 0x060005FC RID: 1532 RVA: 0x0001213B File Offset: 0x0001033B
		public FilteredCatalog IncludeDependents(Func<ImportDefinition, bool> importFilter)
		{
			Requires.NotNull<Func<ImportDefinition, bool>>(importFilter, "importFilter");
			this.ThrowIfDisposed();
			return this.Traverse(new FilteredCatalog.DependentsTraversal(this, importFilter));
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001215C File Offset: 0x0001035C
		private FilteredCatalog Traverse(FilteredCatalog.IComposablePartCatalogTraversal traversal)
		{
			Assumes.NotNull<FilteredCatalog.IComposablePartCatalogTraversal>(traversal);
			this.FreezeInnerCatalog();
			FilteredCatalog result;
			try
			{
				traversal.Initialize();
				HashSet<ComposablePartDefinition> traversalClosure = FilteredCatalog.GetTraversalClosure(this._innerCatalog.Where(this._filter), traversal);
				result = new FilteredCatalog(this._innerCatalog, (ComposablePartDefinition p) => traversalClosure.Contains(p));
			}
			finally
			{
				this.UnfreezeInnerCatalog();
			}
			return result;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000121D0 File Offset: 0x000103D0
		private static HashSet<ComposablePartDefinition> GetTraversalClosure(IEnumerable<ComposablePartDefinition> parts, FilteredCatalog.IComposablePartCatalogTraversal traversal)
		{
			Assumes.NotNull<FilteredCatalog.IComposablePartCatalogTraversal>(traversal);
			HashSet<ComposablePartDefinition> hashSet = new HashSet<ComposablePartDefinition>();
			FilteredCatalog.GetTraversalClosure(parts, hashSet, traversal);
			return hashSet;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x000121F4 File Offset: 0x000103F4
		private static void GetTraversalClosure(IEnumerable<ComposablePartDefinition> parts, HashSet<ComposablePartDefinition> traversedParts, FilteredCatalog.IComposablePartCatalogTraversal traversal)
		{
			foreach (ComposablePartDefinition composablePartDefinition in parts)
			{
				if (traversedParts.Add(composablePartDefinition))
				{
					IEnumerable<ComposablePartDefinition> parts2 = null;
					if (traversal.TryTraverse(composablePartDefinition, out parts2))
					{
						FilteredCatalog.GetTraversalClosure(parts2, traversedParts, traversal);
					}
				}
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00012254 File Offset: 0x00010454
		private void FreezeInnerCatalog()
		{
			INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged = this._innerCatalog as INotifyComposablePartCatalogChanged;
			if (notifyComposablePartCatalogChanged != null)
			{
				notifyComposablePartCatalogChanged.Changing += FilteredCatalog.ThrowOnRecomposition;
			}
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00012284 File Offset: 0x00010484
		private void UnfreezeInnerCatalog()
		{
			INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged = this._innerCatalog as INotifyComposablePartCatalogChanged;
			if (notifyComposablePartCatalogChanged != null)
			{
				notifyComposablePartCatalogChanged.Changing -= FilteredCatalog.ThrowOnRecomposition;
			}
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000122B2 File Offset: 0x000104B2
		private static void ThrowOnRecomposition(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			throw new ChangeRejectedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.FilteredCatalog" /> class with the specified underlying catalog and filter.</summary>
		/// <param name="catalog">The underlying catalog.</param>
		/// <param name="filter">The function to filter parts.</param>
		// Token: 0x06000603 RID: 1539 RVA: 0x000122B9 File Offset: 0x000104B9
		public FilteredCatalog(ComposablePartCatalog catalog, Func<ComposablePartDefinition, bool> filter) : this(catalog, filter, null)
		{
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000122C4 File Offset: 0x000104C4
		internal FilteredCatalog(ComposablePartCatalog catalog, Func<ComposablePartDefinition, bool> filter, FilteredCatalog complement)
		{
			Requires.NotNull<ComposablePartCatalog>(catalog, "catalog");
			Requires.NotNull<Func<ComposablePartDefinition, bool>>(filter, "filter");
			this._innerCatalog = catalog;
			this._filter = ((ComposablePartDefinition p) => filter(p.GetGenericPartDefinition() ?? p));
			this._complement = complement;
			INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged = this._innerCatalog as INotifyComposablePartCatalogChanged;
			if (notifyComposablePartCatalogChanged != null)
			{
				notifyComposablePartCatalogChanged.Changed += this.OnChangedInternal;
				notifyComposablePartCatalogChanged.Changing += this.OnChangingInternal;
			}
		}

		/// <summary>Called by the <see langword="Dispose()" /> and <see langword="Finalize()" /> methods to release the managed and unmanaged resources used by the current instance of the <see cref="T:System.ComponentModel.Composition.Hosting.FilteredCatalog" /> class.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000605 RID: 1541 RVA: 0x00012360 File Offset: 0x00010560
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && !this._isDisposed)
				{
					INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged = null;
					try
					{
						object @lock = this._lock;
						lock (@lock)
						{
							if (!this._isDisposed)
							{
								this._isDisposed = true;
								notifyComposablePartCatalogChanged = (this._innerCatalog as INotifyComposablePartCatalogChanged);
								this._innerCatalog = null;
							}
						}
					}
					finally
					{
						if (notifyComposablePartCatalogChanged != null)
						{
							notifyComposablePartCatalogChanged.Changed -= this.OnChangedInternal;
							notifyComposablePartCatalogChanged.Changing -= this.OnChangingInternal;
						}
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Returns an enumerator that iterates through the catalog.</summary>
		/// <returns>An enumerator that can be used to iterate through the catalog.</returns>
		// Token: 0x06000606 RID: 1542 RVA: 0x0001241C File Offset: 0x0001061C
		public override IEnumerator<ComposablePartDefinition> GetEnumerator()
		{
			return this._innerCatalog.Where(this._filter).GetEnumerator();
		}

		/// <summary>Gets a catalog that contains parts that are present in the underlying catalog but that were filtered out by the filter function.</summary>
		/// <returns>A catalog that contains the complement of this catalog.</returns>
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x00012434 File Offset: 0x00010634
		public FilteredCatalog Complement
		{
			get
			{
				this.ThrowIfDisposed();
				if (this._complement == null)
				{
					FilteredCatalog filteredCatalog = new FilteredCatalog(this._innerCatalog, (ComposablePartDefinition p) => !this._filter(p), this);
					object @lock = this._lock;
					lock (@lock)
					{
						if (this._complement == null)
						{
							Thread.MemoryBarrier();
							this._complement = filteredCatalog;
							filteredCatalog = null;
						}
					}
					if (filteredCatalog != null)
					{
						filteredCatalog.Dispose();
					}
				}
				return this._complement;
			}
		}

		/// <summary>Gets the exported parts from this catalog that match the specified import.</summary>
		/// <param name="definition">The import to match.</param>
		/// <returns>A collection of matching parts.</returns>
		// Token: 0x06000608 RID: 1544 RVA: 0x000124BC File Offset: 0x000106BC
		public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
		{
			this.ThrowIfDisposed();
			Requires.NotNull<ImportDefinition>(definition, "definition");
			List<Tuple<ComposablePartDefinition, ExportDefinition>> list = new List<Tuple<ComposablePartDefinition, ExportDefinition>>();
			foreach (Tuple<ComposablePartDefinition, ExportDefinition> tuple in this._innerCatalog.GetExports(definition))
			{
				if (this._filter(tuple.Item1))
				{
					list.Add(tuple);
				}
			}
			return list;
		}

		/// <summary>Occurs when the underlying catalog has changed.</summary>
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000609 RID: 1545 RVA: 0x0001253C File Offset: 0x0001073C
		// (remove) Token: 0x0600060A RID: 1546 RVA: 0x00012574 File Offset: 0x00010774
		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changed
		{
			[CompilerGenerated]
			add
			{
				EventHandler<ComposablePartCatalogChangeEventArgs> eventHandler = this.Changed;
				EventHandler<ComposablePartCatalogChangeEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ComposablePartCatalogChangeEventArgs> value2 = (EventHandler<ComposablePartCatalogChangeEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ComposablePartCatalogChangeEventArgs>>(ref this.Changed, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<ComposablePartCatalogChangeEventArgs> eventHandler = this.Changed;
				EventHandler<ComposablePartCatalogChangeEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ComposablePartCatalogChangeEventArgs> value2 = (EventHandler<ComposablePartCatalogChangeEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ComposablePartCatalogChangeEventArgs>>(ref this.Changed, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		/// <summary>Occurs when the underlying catalog is changing.</summary>
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600060B RID: 1547 RVA: 0x000125AC File Offset: 0x000107AC
		// (remove) Token: 0x0600060C RID: 1548 RVA: 0x000125E4 File Offset: 0x000107E4
		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changing
		{
			[CompilerGenerated]
			add
			{
				EventHandler<ComposablePartCatalogChangeEventArgs> eventHandler = this.Changing;
				EventHandler<ComposablePartCatalogChangeEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ComposablePartCatalogChangeEventArgs> value2 = (EventHandler<ComposablePartCatalogChangeEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ComposablePartCatalogChangeEventArgs>>(ref this.Changing, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<ComposablePartCatalogChangeEventArgs> eventHandler = this.Changing;
				EventHandler<ComposablePartCatalogChangeEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<ComposablePartCatalogChangeEventArgs> value2 = (EventHandler<ComposablePartCatalogChangeEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<ComposablePartCatalogChangeEventArgs>>(ref this.Changing, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Composition.Hosting.FilteredCatalog.Changed" /> event.</summary>
		/// <param name="e">Provides data for the event.</param>
		// Token: 0x0600060D RID: 1549 RVA: 0x0001261C File Offset: 0x0001081C
		protected virtual void OnChanged(ComposablePartCatalogChangeEventArgs e)
		{
			EventHandler<ComposablePartCatalogChangeEventArgs> changed = this.Changed;
			if (changed != null)
			{
				changed(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Composition.Hosting.FilteredCatalog.Changing" /> event.</summary>
		/// <param name="e">Provides data for the event.</param>
		// Token: 0x0600060E RID: 1550 RVA: 0x0001263C File Offset: 0x0001083C
		protected virtual void OnChanging(ComposablePartCatalogChangeEventArgs e)
		{
			EventHandler<ComposablePartCatalogChangeEventArgs> changing = this.Changing;
			if (changing != null)
			{
				changing(this, e);
			}
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001265C File Offset: 0x0001085C
		private void OnChangedInternal(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			ComposablePartCatalogChangeEventArgs composablePartCatalogChangeEventArgs = this.ProcessEventArgs(e);
			if (composablePartCatalogChangeEventArgs != null)
			{
				this.OnChanged(this.ProcessEventArgs(composablePartCatalogChangeEventArgs));
			}
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00012684 File Offset: 0x00010884
		private void OnChangingInternal(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			ComposablePartCatalogChangeEventArgs composablePartCatalogChangeEventArgs = this.ProcessEventArgs(e);
			if (composablePartCatalogChangeEventArgs != null)
			{
				this.OnChanging(this.ProcessEventArgs(composablePartCatalogChangeEventArgs));
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x000126AC File Offset: 0x000108AC
		private ComposablePartCatalogChangeEventArgs ProcessEventArgs(ComposablePartCatalogChangeEventArgs e)
		{
			ComposablePartCatalogChangeEventArgs composablePartCatalogChangeEventArgs = new ComposablePartCatalogChangeEventArgs(e.AddedDefinitions.Where(this._filter), e.RemovedDefinitions.Where(this._filter), e.AtomicComposition);
			if (composablePartCatalogChangeEventArgs.AddedDefinitions.FastAny<ComposablePartDefinition>() || composablePartCatalogChangeEventArgs.RemovedDefinitions.FastAny<ComposablePartDefinition>())
			{
				return composablePartCatalogChangeEventArgs;
			}
			return null;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00012704 File Offset: 0x00010904
		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00012717 File Offset: 0x00010917
		[CompilerGenerated]
		private bool <get_Complement>b__23_0(ComposablePartDefinition p)
		{
			return !this._filter(p);
		}

		// Token: 0x04000289 RID: 649
		private Func<ComposablePartDefinition, bool> _filter;

		// Token: 0x0400028A RID: 650
		private ComposablePartCatalog _innerCatalog;

		// Token: 0x0400028B RID: 651
		private FilteredCatalog _complement;

		// Token: 0x0400028C RID: 652
		private object _lock = new object();

		// Token: 0x0400028D RID: 653
		private volatile bool _isDisposed;

		// Token: 0x0400028E RID: 654
		[CompilerGenerated]
		private EventHandler<ComposablePartCatalogChangeEventArgs> Changed;

		// Token: 0x0400028F RID: 655
		[CompilerGenerated]
		private EventHandler<ComposablePartCatalogChangeEventArgs> Changing;

		// Token: 0x020000E2 RID: 226
		internal class DependenciesTraversal : FilteredCatalog.IComposablePartCatalogTraversal
		{
			// Token: 0x06000614 RID: 1556 RVA: 0x00012728 File Offset: 0x00010928
			public DependenciesTraversal(FilteredCatalog catalog, Func<ImportDefinition, bool> importFilter)
			{
				Assumes.NotNull<FilteredCatalog>(catalog);
				Assumes.NotNull<Func<ImportDefinition, bool>>(importFilter);
				this._parts = catalog._innerCatalog;
				this._importFilter = importFilter;
			}

			// Token: 0x06000615 RID: 1557 RVA: 0x0001274F File Offset: 0x0001094F
			public void Initialize()
			{
				this.BuildExportersIndex();
			}

			// Token: 0x06000616 RID: 1558 RVA: 0x00012758 File Offset: 0x00010958
			private void BuildExportersIndex()
			{
				this._exportersIndex = new Dictionary<string, List<ComposablePartDefinition>>();
				foreach (ComposablePartDefinition composablePartDefinition in this._parts)
				{
					foreach (ExportDefinition exportDefinition in composablePartDefinition.ExportDefinitions)
					{
						this.AddToExportersIndex(exportDefinition.ContractName, composablePartDefinition);
					}
				}
			}

			// Token: 0x06000617 RID: 1559 RVA: 0x000127EC File Offset: 0x000109EC
			private void AddToExportersIndex(string contractName, ComposablePartDefinition part)
			{
				List<ComposablePartDefinition> list = null;
				if (!this._exportersIndex.TryGetValue(contractName, out list))
				{
					list = new List<ComposablePartDefinition>();
					this._exportersIndex.Add(contractName, list);
				}
				list.Add(part);
			}

			// Token: 0x06000618 RID: 1560 RVA: 0x00012828 File Offset: 0x00010A28
			public bool TryTraverse(ComposablePartDefinition part, out IEnumerable<ComposablePartDefinition> reachableParts)
			{
				reachableParts = null;
				List<ComposablePartDefinition> list = null;
				foreach (ImportDefinition import in part.ImportDefinitions.Where(this._importFilter))
				{
					List<ComposablePartDefinition> list2 = null;
					foreach (string key in import.GetCandidateContractNames(part))
					{
						if (this._exportersIndex.TryGetValue(key, out list2))
						{
							foreach (ComposablePartDefinition composablePartDefinition in list2)
							{
								foreach (ExportDefinition export in composablePartDefinition.ExportDefinitions)
								{
									if (import.IsImportDependentOnPart(composablePartDefinition, export, part.IsGeneric() != composablePartDefinition.IsGeneric()))
									{
										if (list == null)
										{
											list = new List<ComposablePartDefinition>();
										}
										list.Add(composablePartDefinition);
									}
								}
							}
						}
					}
				}
				reachableParts = list;
				return reachableParts != null;
			}

			// Token: 0x04000290 RID: 656
			private IEnumerable<ComposablePartDefinition> _parts;

			// Token: 0x04000291 RID: 657
			private Func<ImportDefinition, bool> _importFilter;

			// Token: 0x04000292 RID: 658
			private Dictionary<string, List<ComposablePartDefinition>> _exportersIndex;
		}

		// Token: 0x020000E3 RID: 227
		internal class DependentsTraversal : FilteredCatalog.IComposablePartCatalogTraversal
		{
			// Token: 0x06000619 RID: 1561 RVA: 0x00012988 File Offset: 0x00010B88
			public DependentsTraversal(FilteredCatalog catalog, Func<ImportDefinition, bool> importFilter)
			{
				Assumes.NotNull<FilteredCatalog>(catalog);
				Assumes.NotNull<Func<ImportDefinition, bool>>(importFilter);
				this._parts = catalog._innerCatalog;
				this._importFilter = importFilter;
			}

			// Token: 0x0600061A RID: 1562 RVA: 0x000129AF File Offset: 0x00010BAF
			public void Initialize()
			{
				this.BuildImportersIndex();
			}

			// Token: 0x0600061B RID: 1563 RVA: 0x000129B8 File Offset: 0x00010BB8
			private void BuildImportersIndex()
			{
				this._importersIndex = new Dictionary<string, List<ComposablePartDefinition>>();
				foreach (ComposablePartDefinition composablePartDefinition in this._parts)
				{
					foreach (ImportDefinition import in composablePartDefinition.ImportDefinitions)
					{
						foreach (string contractName in import.GetCandidateContractNames(composablePartDefinition))
						{
							this.AddToImportersIndex(contractName, composablePartDefinition);
						}
					}
				}
			}

			// Token: 0x0600061C RID: 1564 RVA: 0x00012A7C File Offset: 0x00010C7C
			private void AddToImportersIndex(string contractName, ComposablePartDefinition part)
			{
				List<ComposablePartDefinition> list = null;
				if (!this._importersIndex.TryGetValue(contractName, out list))
				{
					list = new List<ComposablePartDefinition>();
					this._importersIndex.Add(contractName, list);
				}
				list.Add(part);
			}

			// Token: 0x0600061D RID: 1565 RVA: 0x00012AB8 File Offset: 0x00010CB8
			public bool TryTraverse(ComposablePartDefinition part, out IEnumerable<ComposablePartDefinition> reachableParts)
			{
				reachableParts = null;
				List<ComposablePartDefinition> list = null;
				foreach (ExportDefinition exportDefinition in part.ExportDefinitions)
				{
					List<ComposablePartDefinition> list2 = null;
					if (this._importersIndex.TryGetValue(exportDefinition.ContractName, out list2))
					{
						foreach (ComposablePartDefinition composablePartDefinition in list2)
						{
							using (IEnumerator<ImportDefinition> enumerator3 = composablePartDefinition.ImportDefinitions.Where(this._importFilter).GetEnumerator())
							{
								while (enumerator3.MoveNext())
								{
									if (enumerator3.Current.IsImportDependentOnPart(part, exportDefinition, part.IsGeneric() != composablePartDefinition.IsGeneric()))
									{
										if (list == null)
										{
											list = new List<ComposablePartDefinition>();
										}
										list.Add(composablePartDefinition);
									}
								}
							}
						}
					}
				}
				reachableParts = list;
				return reachableParts != null;
			}

			// Token: 0x04000293 RID: 659
			private IEnumerable<ComposablePartDefinition> _parts;

			// Token: 0x04000294 RID: 660
			private Func<ImportDefinition, bool> _importFilter;

			// Token: 0x04000295 RID: 661
			private Dictionary<string, List<ComposablePartDefinition>> _importersIndex;
		}

		// Token: 0x020000E4 RID: 228
		internal interface IComposablePartCatalogTraversal
		{
			// Token: 0x0600061E RID: 1566
			void Initialize();

			// Token: 0x0600061F RID: 1567
			bool TryTraverse(ComposablePartDefinition part, out IEnumerable<ComposablePartDefinition> reachableParts);
		}

		// Token: 0x020000E5 RID: 229
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000620 RID: 1568 RVA: 0x00012BD4 File Offset: 0x00010DD4
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000621 RID: 1569 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c()
			{
			}

			// Token: 0x06000622 RID: 1570 RVA: 0x00012BE0 File Offset: 0x00010DE0
			internal bool <IncludeDependencies>b__3_0(ImportDefinition i)
			{
				return i.Cardinality == ImportCardinality.ExactlyOne;
			}

			// Token: 0x06000623 RID: 1571 RVA: 0x00012BE0 File Offset: 0x00010DE0
			internal bool <IncludeDependents>b__5_0(ImportDefinition i)
			{
				return i.Cardinality == ImportCardinality.ExactlyOne;
			}

			// Token: 0x04000296 RID: 662
			public static readonly FilteredCatalog.<>c <>9 = new FilteredCatalog.<>c();

			// Token: 0x04000297 RID: 663
			public static Func<ImportDefinition, bool> <>9__3_0;

			// Token: 0x04000298 RID: 664
			public static Func<ImportDefinition, bool> <>9__5_0;
		}

		// Token: 0x020000E6 RID: 230
		[CompilerGenerated]
		private sealed class <>c__DisplayClass7_0
		{
			// Token: 0x06000624 RID: 1572 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass7_0()
			{
			}

			// Token: 0x06000625 RID: 1573 RVA: 0x00012BEB File Offset: 0x00010DEB
			internal bool <Traverse>b__0(ComposablePartDefinition p)
			{
				return this.traversalClosure.Contains(p);
			}

			// Token: 0x04000299 RID: 665
			public HashSet<ComposablePartDefinition> traversalClosure;
		}

		// Token: 0x020000E7 RID: 231
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_0
		{
			// Token: 0x06000626 RID: 1574 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass19_0()
			{
			}

			// Token: 0x06000627 RID: 1575 RVA: 0x00012BF9 File Offset: 0x00010DF9
			internal bool <.ctor>b__0(ComposablePartDefinition p)
			{
				return this.filter(p.GetGenericPartDefinition() ?? p);
			}

			// Token: 0x0400029A RID: 666
			public Func<ComposablePartDefinition, bool> filter;
		}
	}
}
