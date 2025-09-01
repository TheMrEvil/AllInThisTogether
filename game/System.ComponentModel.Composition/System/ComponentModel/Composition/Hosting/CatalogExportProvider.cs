using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Diagnostics;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.ReflectionModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Internal;
using Microsoft.Internal.Collections;

namespace System.ComponentModel.Composition.Hosting
{
	/// <summary>Retrieves exports from a catalog.</summary>
	// Token: 0x020000A5 RID: 165
	public class CatalogExportProvider : ExportProvider, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> class with the specified catalog.</summary>
		/// <param name="catalog">The catalog that the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> uses to produce <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="catalog" /> is <see langword="null" />.</exception>
		// Token: 0x0600046C RID: 1132 RVA: 0x0000C954 File Offset: 0x0000AB54
		public CatalogExportProvider(ComposablePartCatalog catalog) : this(catalog, CompositionOptions.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> class with the specified catalog and optional thread-safe mode.</summary>
		/// <param name="catalog">The catalog that the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> uses to produce <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.</param>
		/// <param name="isThreadSafe">
		///   <see langword="true" /> if this object must be thread-safe; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="catalog" /> is <see langword="null" />.</exception>
		// Token: 0x0600046D RID: 1133 RVA: 0x0000C95E File Offset: 0x0000AB5E
		public CatalogExportProvider(ComposablePartCatalog catalog, bool isThreadSafe) : this(catalog, isThreadSafe ? CompositionOptions.IsThreadSafe : CompositionOptions.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> class with the specified catalog and composition options.</summary>
		/// <param name="catalog">The catalog that the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> uses to produce <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.</param>
		/// <param name="compositionOptions">Options that determine the behavior of this provider.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="catalog" /> is <see langword="null" />.</exception>
		// Token: 0x0600046E RID: 1134 RVA: 0x0000C970 File Offset: 0x0000AB70
		public CatalogExportProvider(ComposablePartCatalog catalog, CompositionOptions compositionOptions)
		{
			Requires.NotNull<ComposablePartCatalog>(catalog, "catalog");
			if (compositionOptions > (CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe | CompositionOptions.ExportCompositionService))
			{
				throw new ArgumentOutOfRangeException("compositionOptions");
			}
			this._catalog = catalog;
			this._compositionOptions = compositionOptions;
			INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged = this._catalog as INotifyComposablePartCatalogChanged;
			if (notifyComposablePartCatalogChanged != null)
			{
				notifyComposablePartCatalogChanged.Changing += this.OnCatalogChanging;
			}
			CompositionScopeDefinition compositionScopeDefinition = this._catalog as CompositionScopeDefinition;
			if (compositionScopeDefinition != null)
			{
				this._innerExportProvider = new AggregateExportProvider(new ExportProvider[]
				{
					new CatalogExportProvider.ScopeManager(this, compositionScopeDefinition),
					new CatalogExportProvider.InnerCatalogExportProvider(new Func<ImportDefinition, AtomicComposition, IEnumerable<Export>>(this.InternalGetExportsCore))
				});
			}
			else
			{
				this._innerExportProvider = new CatalogExportProvider.InnerCatalogExportProvider(new Func<ImportDefinition, AtomicComposition, IEnumerable<Export>>(this.InternalGetExportsCore));
			}
			this._lock = new CompositionLock(compositionOptions.HasFlag(CompositionOptions.IsThreadSafe));
		}

		/// <summary>Gets the catalog that is used to provide exports.</summary>
		/// <returns>The catalog that the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> uses to produce <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> has been disposed of.</exception>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000CA60 File Offset: 0x0000AC60
		public ComposablePartCatalog Catalog
		{
			get
			{
				this.ThrowIfDisposed();
				return this._catalog;
			}
		}

		/// <summary>Gets or sets the export provider that provides access to additional exports.</summary>
		/// <returns>The export provider that provides the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> access to additional <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects. The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> has been disposed of.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This property has already been set.  
		///  -or-  
		///  The methods on the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> object have already been accessed.</exception>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0000CA70 File Offset: 0x0000AC70
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x0000CAB4 File Offset: 0x0000ACB4
		public ExportProvider SourceProvider
		{
			get
			{
				this.ThrowIfDisposed();
				ExportProvider sourceProvider;
				using (this._lock.LockStateForRead())
				{
					sourceProvider = this._sourceProvider;
				}
				return sourceProvider;
			}
			set
			{
				this.ThrowIfDisposed();
				Requires.NotNull<ExportProvider>(value, "value");
				ImportEngine importEngine = null;
				AggregateExportProvider aggregateExportProvider = null;
				bool flag = true;
				try
				{
					importEngine = new ImportEngine(value, this._compositionOptions);
					value.ExportsChanging += this.OnExportsChangingInternal;
					using (this._lock.LockStateForWrite())
					{
						this.EnsureCanSet<ExportProvider>(this._sourceProvider);
						this._sourceProvider = value;
						this._importEngine = importEngine;
						flag = false;
					}
				}
				finally
				{
					if (flag)
					{
						value.ExportsChanging -= this.OnExportsChangingInternal;
						importEngine.Dispose();
						if (aggregateExportProvider != null)
						{
							aggregateExportProvider.Dispose();
						}
					}
				}
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> class.</summary>
		// Token: 0x06000472 RID: 1138 RVA: 0x0000CB74 File Offset: 0x0000AD74
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.CatalogExportProvider" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000473 RID: 1139 RVA: 0x0000CB84 File Offset: 0x0000AD84
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				bool flag = false;
				INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged = null;
				HashSet<IDisposable> hashSet = null;
				ImportEngine importEngine = null;
				ExportProvider exportProvider = null;
				AggregateExportProvider aggregateExportProvider = null;
				try
				{
					using (this._lock.LockStateForWrite())
					{
						if (!this._isDisposed)
						{
							notifyComposablePartCatalogChanged = (this._catalog as INotifyComposablePartCatalogChanged);
							this._catalog = null;
							aggregateExportProvider = (this._innerExportProvider as AggregateExportProvider);
							this._innerExportProvider = null;
							exportProvider = this._sourceProvider;
							this._sourceProvider = null;
							importEngine = this._importEngine;
							this._importEngine = null;
							hashSet = this._partsToDispose;
							this._gcRoots = null;
							flag = true;
							this._isDisposed = true;
						}
					}
				}
				finally
				{
					if (notifyComposablePartCatalogChanged != null)
					{
						notifyComposablePartCatalogChanged.Changing -= this.OnCatalogChanging;
					}
					if (aggregateExportProvider != null)
					{
						aggregateExportProvider.Dispose();
					}
					if (exportProvider != null)
					{
						exportProvider.ExportsChanging -= this.OnExportsChangingInternal;
					}
					if (importEngine != null)
					{
						importEngine.Dispose();
					}
					if (hashSet != null)
					{
						foreach (IDisposable disposable2 in hashSet)
						{
							disposable2.Dispose();
						}
					}
					if (flag)
					{
						this._lock.Dispose();
					}
				}
			}
		}

		/// <summary>Returns all exports that match the conditions of the specified import.</summary>
		/// <param name="definition">The conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects to be returned.</param>
		/// <param name="atomicComposition">The composition transaction to use, or <see langword="null" /> to disable transactional composition.</param>
		/// <returns>A collection that contains all the exports that match the specified condition.</returns>
		// Token: 0x06000474 RID: 1140 RVA: 0x0000CCE0 File Offset: 0x0000AEE0
		protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
		{
			this.ThrowIfDisposed();
			this.EnsureRunning();
			Assumes.NotNull<ExportProvider>(this._innerExportProvider);
			IEnumerable<Export> result;
			this._innerExportProvider.TryGetExports(definition, atomicComposition, out result);
			return result;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0000CD18 File Offset: 0x0000AF18
		private IEnumerable<Export> InternalGetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
		{
			this.ThrowIfDisposed();
			this.EnsureRunning();
			ComposablePartCatalog valueAllowNull = atomicComposition.GetValueAllowNull(this._catalog);
			IPartCreatorImportDefinition partCreatorImportDefinition = definition as IPartCreatorImportDefinition;
			bool isExportFactory = false;
			if (partCreatorImportDefinition != null)
			{
				definition = partCreatorImportDefinition.ProductImportDefinition;
				isExportFactory = true;
			}
			CreationPolicy requiredCreationPolicy = definition.GetRequiredCreationPolicy();
			List<Export> list = new List<Export>();
			foreach (Tuple<ComposablePartDefinition, ExportDefinition> tuple in valueAllowNull.GetExports(definition))
			{
				if (!this.IsRejected(tuple.Item1, atomicComposition))
				{
					list.Add(this.CreateExport(tuple.Item1, tuple.Item2, isExportFactory, requiredCreationPolicy));
				}
			}
			return list;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0000CDCC File Offset: 0x0000AFCC
		private Export CreateExport(ComposablePartDefinition partDefinition, ExportDefinition exportDefinition, bool isExportFactory, CreationPolicy importPolicy)
		{
			if (isExportFactory)
			{
				return new CatalogExportProvider.PartCreatorExport(this, partDefinition, exportDefinition);
			}
			return CatalogExportProvider.CatalogExport.CreateExport(this, partDefinition, exportDefinition, importPolicy);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0000CDE4 File Offset: 0x0000AFE4
		private void OnExportsChangingInternal(object sender, ExportsChangeEventArgs e)
		{
			this.UpdateRejections(e.AddedExports.Concat(e.RemovedExports), e.AtomicComposition);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000CE04 File Offset: 0x0000B004
		private static ExportDefinition[] GetExportsFromPartDefinitions(IEnumerable<ComposablePartDefinition> partDefinitions)
		{
			List<ExportDefinition> list = new List<ExportDefinition>();
			foreach (ComposablePartDefinition composablePartDefinition in partDefinitions)
			{
				foreach (ExportDefinition exportDefinition in composablePartDefinition.ExportDefinitions)
				{
					list.Add(exportDefinition);
					list.Add(new PartCreatorExportDefinition(exportDefinition));
				}
			}
			return list.ToArray();
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000CE98 File Offset: 0x0000B098
		private void OnCatalogChanging(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			using (AtomicComposition atomicComposition = new AtomicComposition(e.AtomicComposition))
			{
				atomicComposition.SetValue(this._catalog, new CatalogExportProvider.CatalogChangeProxy(this._catalog, e.AddedDefinitions, e.RemovedDefinitions));
				IEnumerable<ExportDefinition> addedExports = CatalogExportProvider.GetExportsFromPartDefinitions(e.AddedDefinitions);
				IEnumerable<ExportDefinition> removedExports = CatalogExportProvider.GetExportsFromPartDefinitions(e.RemovedDefinitions);
				foreach (ComposablePartDefinition composablePartDefinition in e.RemovedDefinitions)
				{
					CatalogExportProvider.CatalogPart catalogPart = null;
					bool flag = false;
					using (this._lock.LockStateForRead())
					{
						flag = this._activatedParts.TryGetValue(composablePartDefinition, out catalogPart);
					}
					if (flag)
					{
						ComposablePartDefinition capturedDefinition = composablePartDefinition;
						this.ReleasePart(null, catalogPart, atomicComposition);
						atomicComposition.AddCompleteActionAllowNull(delegate
						{
							using (this._lock.LockStateForWrite())
							{
								this._activatedParts.Remove(capturedDefinition);
							}
						});
					}
				}
				this.UpdateRejections(addedExports.ConcatAllowingNull(removedExports), atomicComposition);
				this.OnExportsChanging(new ExportsChangeEventArgs(addedExports, removedExports, atomicComposition));
				atomicComposition.AddCompleteAction(delegate
				{
					this.OnExportsChanged(new ExportsChangeEventArgs(addedExports, removedExports, null));
				});
				atomicComposition.Complete();
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000D038 File Offset: 0x0000B238
		private CatalogExportProvider.CatalogPart GetComposablePart(ComposablePartDefinition partDefinition, bool isSharedPart)
		{
			this.ThrowIfDisposed();
			this.EnsureRunning();
			CatalogExportProvider.CatalogPart result = null;
			if (isSharedPart)
			{
				result = this.GetSharedPart(partDefinition);
			}
			else
			{
				ComposablePart composablePart = partDefinition.CreatePart();
				result = new CatalogExportProvider.CatalogPart(composablePart);
				IDisposable disposable = composablePart as IDisposable;
				if (disposable != null)
				{
					using (this._lock.LockStateForWrite())
					{
						this._partsToDispose.Add(disposable);
					}
				}
			}
			return result;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000D0AC File Offset: 0x0000B2AC
		private CatalogExportProvider.CatalogPart GetSharedPart(ComposablePartDefinition partDefinition)
		{
			CatalogExportProvider.CatalogPart catalogPart = null;
			using (this._lock.LockStateForRead())
			{
				if (this._activatedParts.TryGetValue(partDefinition, out catalogPart))
				{
					return catalogPart;
				}
			}
			ComposablePart composablePart = partDefinition.CreatePart();
			IDisposable disposable2 = composablePart as IDisposable;
			using (this._lock.LockStateForWrite())
			{
				if (!this._activatedParts.TryGetValue(partDefinition, out catalogPart))
				{
					catalogPart = new CatalogExportProvider.CatalogPart(composablePart);
					this._activatedParts.Add(partDefinition, catalogPart);
					if (disposable2 != null)
					{
						this._partsToDispose.Add(disposable2);
					}
					disposable2 = null;
				}
			}
			if (disposable2 != null)
			{
				disposable2.Dispose();
			}
			return catalogPart;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0000D170 File Offset: 0x0000B370
		private object GetExportedValue(CatalogExportProvider.CatalogPart part, ExportDefinition export, bool isSharedPart)
		{
			this.ThrowIfDisposed();
			this.EnsureRunning();
			Assumes.NotNull<CatalogExportProvider.CatalogPart, ExportDefinition>(part, export);
			bool importsSatisfied = part.ImportsSatisfied;
			object exportedValueFromComposedPart = CompositionServices.GetExportedValueFromComposedPart(importsSatisfied ? null : this._importEngine, part.Part, export);
			if (!importsSatisfied)
			{
				part.ImportsSatisfied = true;
			}
			if (exportedValueFromComposedPart != null && !isSharedPart && part.Part.IsRecomposable())
			{
				this.PreventPartCollection(exportedValueFromComposedPart, part.Part);
			}
			return exportedValueFromComposedPart;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0000D1DC File Offset: 0x0000B3DC
		private void ReleasePart(object exportedValue, CatalogExportProvider.CatalogPart catalogPart, AtomicComposition atomicComposition)
		{
			this.ThrowIfDisposed();
			this.EnsureRunning();
			Assumes.NotNull<CatalogExportProvider.CatalogPart>(catalogPart);
			this._importEngine.ReleaseImports(catalogPart.Part, atomicComposition);
			if (exportedValue != null)
			{
				atomicComposition.AddCompleteActionAllowNull(delegate
				{
					this.AllowPartCollection(exportedValue);
				});
			}
			IDisposable diposablePart = catalogPart.Part as IDisposable;
			if (diposablePart != null)
			{
				atomicComposition.AddCompleteActionAllowNull(delegate
				{
					bool flag = false;
					using (this._lock.LockStateForWrite())
					{
						flag = this._partsToDispose.Remove(diposablePart);
					}
					if (flag)
					{
						diposablePart.Dispose();
					}
				});
			}
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0000D268 File Offset: 0x0000B468
		private void PreventPartCollection(object exportedValue, ComposablePart part)
		{
			Assumes.NotNull<object, ComposablePart>(exportedValue, part);
			using (this._lock.LockStateForWrite())
			{
				ConditionalWeakTable<object, List<ComposablePart>> conditionalWeakTable = this._gcRoots;
				if (conditionalWeakTable == null)
				{
					conditionalWeakTable = new ConditionalWeakTable<object, List<ComposablePart>>();
				}
				List<ComposablePart> list;
				if (!conditionalWeakTable.TryGetValue(exportedValue, out list))
				{
					list = new List<ComposablePart>();
					conditionalWeakTable.Add(exportedValue, list);
				}
				list.Add(part);
				if (this._gcRoots == null)
				{
					Thread.MemoryBarrier();
					this._gcRoots = conditionalWeakTable;
				}
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000D2E8 File Offset: 0x0000B4E8
		private void AllowPartCollection(object gcRoot)
		{
			if (this._gcRoots != null)
			{
				using (this._lock.LockStateForWrite())
				{
					this._gcRoots.Remove(gcRoot);
				}
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0000D334 File Offset: 0x0000B534
		private bool IsRejected(ComposablePartDefinition definition, AtomicComposition atomicComposition)
		{
			bool flag = false;
			if (atomicComposition != null)
			{
				CatalogExportProvider.AtomicCompositionQueryState atomicCompositionQueryState = this.GetAtomicCompositionQuery(atomicComposition)(definition);
				switch (atomicCompositionQueryState)
				{
				case CatalogExportProvider.AtomicCompositionQueryState.TreatAsRejected:
					return true;
				case CatalogExportProvider.AtomicCompositionQueryState.TreatAsValidated:
					return false;
				case CatalogExportProvider.AtomicCompositionQueryState.NeedsTesting:
					flag = true;
					break;
				default:
					Assumes.IsTrue(atomicCompositionQueryState == CatalogExportProvider.AtomicCompositionQueryState.Unknown);
					break;
				}
			}
			if (!flag)
			{
				using (this._lock.LockStateForRead())
				{
					if (this._activatedParts.ContainsKey(definition))
					{
						return false;
					}
					if (this._rejectedParts.Contains(definition))
					{
						return true;
					}
				}
			}
			return this.DetermineRejection(definition, atomicComposition);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000D3D4 File Offset: 0x0000B5D4
		private bool DetermineRejection(ComposablePartDefinition definition, AtomicComposition parentAtomicComposition)
		{
			ChangeRejectedException exception = null;
			using (AtomicComposition atomicComposition = new AtomicComposition(parentAtomicComposition))
			{
				this.UpdateAtomicCompositionQuery(atomicComposition, (ComposablePartDefinition def) => definition.Equals(def), CatalogExportProvider.AtomicCompositionQueryState.TreatAsValidated);
				ComposablePart newPart = definition.CreatePart();
				try
				{
					this._importEngine.PreviewImports(newPart, atomicComposition);
					atomicComposition.AddCompleteActionAllowNull(delegate
					{
						using (this._lock.LockStateForWrite())
						{
							if (!this._activatedParts.ContainsKey(definition))
							{
								this._activatedParts.Add(definition, new CatalogExportProvider.CatalogPart(newPart));
								IDisposable disposable2 = newPart as IDisposable;
								if (disposable2 != null)
								{
									this._partsToDispose.Add(disposable2);
								}
							}
						}
					});
					atomicComposition.Complete();
					return false;
				}
				catch (ChangeRejectedException exception)
				{
					ChangeRejectedException exception2;
					exception = exception2;
				}
			}
			parentAtomicComposition.AddCompleteActionAllowNull(delegate
			{
				using (this._lock.LockStateForWrite())
				{
					this._rejectedParts.Add(definition);
				}
				CompositionTrace.PartDefinitionRejected(definition, exception);
			});
			if (parentAtomicComposition != null)
			{
				this.UpdateAtomicCompositionQuery(parentAtomicComposition, (ComposablePartDefinition def) => definition.Equals(def), CatalogExportProvider.AtomicCompositionQueryState.TreatAsRejected);
			}
			return true;
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000D4D0 File Offset: 0x0000B6D0
		private void UpdateRejections(IEnumerable<ExportDefinition> changedExports, AtomicComposition atomicComposition)
		{
			using (AtomicComposition atomicComposition2 = new AtomicComposition(atomicComposition))
			{
				HashSet<ComposablePartDefinition> affectedRejections = new HashSet<ComposablePartDefinition>();
				Func<ComposablePartDefinition, CatalogExportProvider.AtomicCompositionQueryState> atomicCompositionQuery = this.GetAtomicCompositionQuery(atomicComposition2);
				ComposablePartDefinition[] array;
				using (this._lock.LockStateForRead())
				{
					array = this._rejectedParts.ToArray<ComposablePartDefinition>();
				}
				foreach (ComposablePartDefinition composablePartDefinition in array)
				{
					if (atomicCompositionQuery(composablePartDefinition) != CatalogExportProvider.AtomicCompositionQueryState.TreatAsValidated)
					{
						using (IEnumerator<ImportDefinition> enumerator = composablePartDefinition.ImportDefinitions.Where(new Func<ImportDefinition, bool>(ImportEngine.IsRequiredImportForPreview)).GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								ImportDefinition import = enumerator.Current;
								if (changedExports.Any((ExportDefinition export) => import.IsConstraintSatisfiedBy(export)))
								{
									affectedRejections.Add(composablePartDefinition);
									break;
								}
							}
						}
					}
				}
				this.UpdateAtomicCompositionQuery(atomicComposition2, (ComposablePartDefinition def) => affectedRejections.Contains(def), CatalogExportProvider.AtomicCompositionQueryState.NeedsTesting);
				List<ExportDefinition> resurrectedExports = new List<ExportDefinition>();
				foreach (ComposablePartDefinition composablePartDefinition2 in affectedRejections)
				{
					if (!this.IsRejected(composablePartDefinition2, atomicComposition2))
					{
						resurrectedExports.AddRange(composablePartDefinition2.ExportDefinitions);
						ComposablePartDefinition capturedPartDefinition = composablePartDefinition2;
						atomicComposition2.AddCompleteAction(delegate
						{
							using (this._lock.LockStateForWrite())
							{
								this._rejectedParts.Remove(capturedPartDefinition);
							}
							CompositionTrace.PartDefinitionResurrected(capturedPartDefinition);
						});
					}
				}
				if (resurrectedExports.Any<ExportDefinition>())
				{
					this.OnExportsChanging(new ExportsChangeEventArgs(resurrectedExports, new ExportDefinition[0], atomicComposition2));
					atomicComposition2.AddCompleteAction(delegate
					{
						this.OnExportsChanged(new ExportsChangeEventArgs(resurrectedExports, new ExportDefinition[0], null));
					});
				}
				atomicComposition2.Complete();
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000D718 File Offset: 0x0000B918
		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000D72B File Offset: 0x0000B92B
		[DebuggerStepThrough]
		private void EnsureCanRun()
		{
			if (this._sourceProvider == null || this._importEngine == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.ObjectMustBeInitialized, "SourceProvider"));
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000D758 File Offset: 0x0000B958
		[DebuggerStepThrough]
		private void EnsureRunning()
		{
			if (!this._isRunning)
			{
				using (this._lock.LockStateForWrite())
				{
					if (!this._isRunning)
					{
						this.EnsureCanRun();
						this._isRunning = true;
					}
				}
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000D7B0 File Offset: 0x0000B9B0
		[DebuggerStepThrough]
		private void EnsureCanSet<T>(T currentValue) where T : class
		{
			if (this._isRunning || currentValue != null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.ObjectAlreadyInitialized, Array.Empty<object>()));
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000D7E0 File Offset: 0x0000B9E0
		private Func<ComposablePartDefinition, CatalogExportProvider.AtomicCompositionQueryState> GetAtomicCompositionQuery(AtomicComposition atomicComposition)
		{
			Func<ComposablePartDefinition, CatalogExportProvider.AtomicCompositionQueryState> func;
			atomicComposition.TryGetValue<Func<ComposablePartDefinition, CatalogExportProvider.AtomicCompositionQueryState>>(this, out func);
			if (func == null)
			{
				return (ComposablePartDefinition definition) => CatalogExportProvider.AtomicCompositionQueryState.Unknown;
			}
			return func;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0000D81C File Offset: 0x0000BA1C
		private void UpdateAtomicCompositionQuery(AtomicComposition atomicComposition, Func<ComposablePartDefinition, bool> query, CatalogExportProvider.AtomicCompositionQueryState state)
		{
			Func<ComposablePartDefinition, CatalogExportProvider.AtomicCompositionQueryState> parentQuery = this.GetAtomicCompositionQuery(atomicComposition);
			Func<ComposablePartDefinition, CatalogExportProvider.AtomicCompositionQueryState> value = delegate(ComposablePartDefinition definition)
			{
				if (query(definition))
				{
					return state;
				}
				return parentQuery(definition);
			};
			atomicComposition.SetValue(this, value);
		}

		// Token: 0x040001BA RID: 442
		private readonly CompositionLock _lock;

		// Token: 0x040001BB RID: 443
		private Dictionary<ComposablePartDefinition, CatalogExportProvider.CatalogPart> _activatedParts = new Dictionary<ComposablePartDefinition, CatalogExportProvider.CatalogPart>();

		// Token: 0x040001BC RID: 444
		private HashSet<ComposablePartDefinition> _rejectedParts = new HashSet<ComposablePartDefinition>();

		// Token: 0x040001BD RID: 445
		private ConditionalWeakTable<object, List<ComposablePart>> _gcRoots;

		// Token: 0x040001BE RID: 446
		private HashSet<IDisposable> _partsToDispose = new HashSet<IDisposable>();

		// Token: 0x040001BF RID: 447
		private ComposablePartCatalog _catalog;

		// Token: 0x040001C0 RID: 448
		private volatile bool _isDisposed;

		// Token: 0x040001C1 RID: 449
		private volatile bool _isRunning;

		// Token: 0x040001C2 RID: 450
		private ExportProvider _sourceProvider;

		// Token: 0x040001C3 RID: 451
		private ImportEngine _importEngine;

		// Token: 0x040001C4 RID: 452
		private CompositionOptions _compositionOptions;

		// Token: 0x040001C5 RID: 453
		private ExportProvider _innerExportProvider;

		// Token: 0x020000A6 RID: 166
		private class CatalogChangeProxy : ComposablePartCatalog
		{
			// Token: 0x06000489 RID: 1161 RVA: 0x0000D85D File Offset: 0x0000BA5D
			public CatalogChangeProxy(ComposablePartCatalog originalCatalog, IEnumerable<ComposablePartDefinition> addedParts, IEnumerable<ComposablePartDefinition> removedParts)
			{
				this._originalCatalog = originalCatalog;
				this._addedParts = new List<ComposablePartDefinition>(addedParts);
				this._removedParts = new HashSet<ComposablePartDefinition>(removedParts);
			}

			// Token: 0x0600048A RID: 1162 RVA: 0x0000D884 File Offset: 0x0000BA84
			public override IEnumerator<ComposablePartDefinition> GetEnumerator()
			{
				return this._originalCatalog.Concat(this._addedParts).Except(this._removedParts).GetEnumerator();
			}

			// Token: 0x0600048B RID: 1163 RVA: 0x0000D8A8 File Offset: 0x0000BAA8
			public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
			{
				Requires.NotNull<ImportDefinition>(definition, "definition");
				IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> first = from partAndExport in this._originalCatalog.GetExports(definition)
				where !this._removedParts.Contains(partAndExport.Item1)
				select partAndExport;
				List<Tuple<ComposablePartDefinition, ExportDefinition>> list = new List<Tuple<ComposablePartDefinition, ExportDefinition>>();
				foreach (ComposablePartDefinition composablePartDefinition in this._addedParts)
				{
					foreach (ExportDefinition exportDefinition in composablePartDefinition.ExportDefinitions)
					{
						if (definition.IsConstraintSatisfiedBy(exportDefinition))
						{
							list.Add(new Tuple<ComposablePartDefinition, ExportDefinition>(composablePartDefinition, exportDefinition));
						}
					}
				}
				return first.Concat(list);
			}

			// Token: 0x0600048C RID: 1164 RVA: 0x0000D980 File Offset: 0x0000BB80
			[CompilerGenerated]
			private bool <GetExports>b__5_0(Tuple<ComposablePartDefinition, ExportDefinition> partAndExport)
			{
				return !this._removedParts.Contains(partAndExport.Item1);
			}

			// Token: 0x040001C6 RID: 454
			private ComposablePartCatalog _originalCatalog;

			// Token: 0x040001C7 RID: 455
			private List<ComposablePartDefinition> _addedParts;

			// Token: 0x040001C8 RID: 456
			private HashSet<ComposablePartDefinition> _removedParts;
		}

		// Token: 0x020000A7 RID: 167
		private class CatalogExport : Export
		{
			// Token: 0x0600048D RID: 1165 RVA: 0x0000D996 File Offset: 0x0000BB96
			public CatalogExport(CatalogExportProvider catalogExportProvider, ComposablePartDefinition partDefinition, ExportDefinition definition)
			{
				this._catalogExportProvider = catalogExportProvider;
				this._partDefinition = partDefinition;
				this._definition = definition;
			}

			// Token: 0x17000144 RID: 324
			// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000D9B3 File Offset: 0x0000BBB3
			public override ExportDefinition Definition
			{
				get
				{
					return this._definition;
				}
			}

			// Token: 0x17000145 RID: 325
			// (get) Token: 0x0600048F RID: 1167 RVA: 0x00005907 File Offset: 0x00003B07
			protected virtual bool IsSharedPart
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06000490 RID: 1168 RVA: 0x0000D9BB File Offset: 0x0000BBBB
			protected CatalogExportProvider.CatalogPart GetPartCore()
			{
				return this._catalogExportProvider.GetComposablePart(this._partDefinition, this.IsSharedPart);
			}

			// Token: 0x06000491 RID: 1169 RVA: 0x0000D9D4 File Offset: 0x0000BBD4
			protected void ReleasePartCore(CatalogExportProvider.CatalogPart part, object value)
			{
				this._catalogExportProvider.ReleasePart(value, part, null);
			}

			// Token: 0x06000492 RID: 1170 RVA: 0x0000D9E4 File Offset: 0x0000BBE4
			protected virtual CatalogExportProvider.CatalogPart GetPart()
			{
				return this.GetPartCore();
			}

			// Token: 0x06000493 RID: 1171 RVA: 0x0000D9EC File Offset: 0x0000BBEC
			protected override object GetExportedValueCore()
			{
				return this._catalogExportProvider.GetExportedValue(this.GetPart(), this._definition, this.IsSharedPart);
			}

			// Token: 0x06000494 RID: 1172 RVA: 0x0000DA0B File Offset: 0x0000BC0B
			public static CatalogExportProvider.CatalogExport CreateExport(CatalogExportProvider catalogExportProvider, ComposablePartDefinition partDefinition, ExportDefinition definition, CreationPolicy importCreationPolicy)
			{
				if (CatalogExportProvider.CatalogExport.ShouldUseSharedPart(partDefinition.Metadata.GetValue("System.ComponentModel.Composition.CreationPolicy"), importCreationPolicy))
				{
					return new CatalogExportProvider.CatalogExport(catalogExportProvider, partDefinition, definition);
				}
				return new CatalogExportProvider.NonSharedCatalogExport(catalogExportProvider, partDefinition, definition);
			}

			// Token: 0x06000495 RID: 1173 RVA: 0x0000DA38 File Offset: 0x0000BC38
			private static bool ShouldUseSharedPart(CreationPolicy partPolicy, CreationPolicy importPolicy)
			{
				if (partPolicy == CreationPolicy.Any)
				{
					return importPolicy == CreationPolicy.Any || importPolicy == CreationPolicy.NewScope || importPolicy == CreationPolicy.Shared;
				}
				if (partPolicy != CreationPolicy.NonShared)
				{
					Assumes.IsTrue(partPolicy == CreationPolicy.Shared);
					Assumes.IsTrue(importPolicy != CreationPolicy.NonShared && importPolicy != CreationPolicy.NewScope);
					return true;
				}
				Assumes.IsTrue(importPolicy != CreationPolicy.Shared);
				return false;
			}

			// Token: 0x040001C9 RID: 457
			protected readonly CatalogExportProvider _catalogExportProvider;

			// Token: 0x040001CA RID: 458
			protected readonly ComposablePartDefinition _partDefinition;

			// Token: 0x040001CB RID: 459
			protected readonly ExportDefinition _definition;
		}

		// Token: 0x020000A8 RID: 168
		private sealed class NonSharedCatalogExport : CatalogExportProvider.CatalogExport, IDisposable
		{
			// Token: 0x06000496 RID: 1174 RVA: 0x0000DA88 File Offset: 0x0000BC88
			public NonSharedCatalogExport(CatalogExportProvider catalogExportProvider, ComposablePartDefinition partDefinition, ExportDefinition definition) : base(catalogExportProvider, partDefinition, definition)
			{
			}

			// Token: 0x06000497 RID: 1175 RVA: 0x0000DAA0 File Offset: 0x0000BCA0
			protected override CatalogExportProvider.CatalogPart GetPart()
			{
				if (this._part == null)
				{
					CatalogExportProvider.CatalogPart catalogPart = base.GetPartCore();
					object @lock = this._lock;
					lock (@lock)
					{
						if (this._part == null)
						{
							Thread.MemoryBarrier();
							this._part = catalogPart;
							catalogPart = null;
						}
					}
					if (catalogPart != null)
					{
						base.ReleasePartCore(catalogPart, null);
					}
				}
				return this._part;
			}

			// Token: 0x17000146 RID: 326
			// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000A969 File Offset: 0x00008B69
			protected override bool IsSharedPart
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000499 RID: 1177 RVA: 0x0000DB10 File Offset: 0x0000BD10
			void IDisposable.Dispose()
			{
				if (this._part != null)
				{
					base.ReleasePartCore(this._part, base.Value);
					this._part = null;
				}
			}

			// Token: 0x040001CC RID: 460
			private CatalogExportProvider.CatalogPart _part;

			// Token: 0x040001CD RID: 461
			private readonly object _lock = new object();
		}

		// Token: 0x020000A9 RID: 169
		internal abstract class FactoryExport : Export
		{
			// Token: 0x0600049A RID: 1178 RVA: 0x0000DB33 File Offset: 0x0000BD33
			public FactoryExport(ComposablePartDefinition partDefinition, ExportDefinition exportDefinition)
			{
				this._partDefinition = partDefinition;
				this._exportDefinition = exportDefinition;
				this._factoryExportDefinition = new PartCreatorExportDefinition(this._exportDefinition);
			}

			// Token: 0x17000147 RID: 327
			// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000DB5A File Offset: 0x0000BD5A
			public override ExportDefinition Definition
			{
				get
				{
					return this._factoryExportDefinition;
				}
			}

			// Token: 0x0600049C RID: 1180 RVA: 0x0000DB62 File Offset: 0x0000BD62
			protected override object GetExportedValueCore()
			{
				if (this._factoryExportPartDefinition == null)
				{
					this._factoryExportPartDefinition = new CatalogExportProvider.FactoryExport.FactoryExportPartDefinition(this);
				}
				return this._factoryExportPartDefinition;
			}

			// Token: 0x17000148 RID: 328
			// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000DB7E File Offset: 0x0000BD7E
			protected ComposablePartDefinition UnderlyingPartDefinition
			{
				get
				{
					return this._partDefinition;
				}
			}

			// Token: 0x17000149 RID: 329
			// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000DB86 File Offset: 0x0000BD86
			protected ExportDefinition UnderlyingExportDefinition
			{
				get
				{
					return this._exportDefinition;
				}
			}

			// Token: 0x0600049F RID: 1183
			public abstract Export CreateExportProduct();

			// Token: 0x040001CE RID: 462
			private readonly ComposablePartDefinition _partDefinition;

			// Token: 0x040001CF RID: 463
			private readonly ExportDefinition _exportDefinition;

			// Token: 0x040001D0 RID: 464
			private ExportDefinition _factoryExportDefinition;

			// Token: 0x040001D1 RID: 465
			private CatalogExportProvider.FactoryExport.FactoryExportPartDefinition _factoryExportPartDefinition;

			// Token: 0x020000AA RID: 170
			private class FactoryExportPartDefinition : ComposablePartDefinition
			{
				// Token: 0x060004A0 RID: 1184 RVA: 0x0000DB8E File Offset: 0x0000BD8E
				public FactoryExportPartDefinition(CatalogExportProvider.FactoryExport FactoryExport)
				{
					this._FactoryExport = FactoryExport;
				}

				// Token: 0x1700014A RID: 330
				// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000DB9D File Offset: 0x0000BD9D
				public override IEnumerable<ExportDefinition> ExportDefinitions
				{
					get
					{
						return new ExportDefinition[]
						{
							this._FactoryExport.Definition
						};
					}
				}

				// Token: 0x1700014B RID: 331
				// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000DBB3 File Offset: 0x0000BDB3
				public override IEnumerable<ImportDefinition> ImportDefinitions
				{
					get
					{
						return Enumerable.Empty<ImportDefinition>();
					}
				}

				// Token: 0x1700014C RID: 332
				// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000DBBA File Offset: 0x0000BDBA
				public ExportDefinition FactoryExportDefinition
				{
					get
					{
						return this._FactoryExport.Definition;
					}
				}

				// Token: 0x060004A4 RID: 1188 RVA: 0x0000DBC7 File Offset: 0x0000BDC7
				public Export CreateProductExport()
				{
					return this._FactoryExport.CreateExportProduct();
				}

				// Token: 0x060004A5 RID: 1189 RVA: 0x0000DBD4 File Offset: 0x0000BDD4
				public override ComposablePart CreatePart()
				{
					return new CatalogExportProvider.FactoryExport.FactoryExportPart(this);
				}

				// Token: 0x040001D2 RID: 466
				private readonly CatalogExportProvider.FactoryExport _FactoryExport;
			}

			// Token: 0x020000AB RID: 171
			private sealed class FactoryExportPart : ComposablePart, IDisposable
			{
				// Token: 0x060004A6 RID: 1190 RVA: 0x0000DBDC File Offset: 0x0000BDDC
				public FactoryExportPart(CatalogExportProvider.FactoryExport.FactoryExportPartDefinition definition)
				{
					this._definition = definition;
					this._export = definition.CreateProductExport();
				}

				// Token: 0x1700014D RID: 333
				// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000DBF7 File Offset: 0x0000BDF7
				public override IEnumerable<ExportDefinition> ExportDefinitions
				{
					get
					{
						return this._definition.ExportDefinitions;
					}
				}

				// Token: 0x1700014E RID: 334
				// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000DC04 File Offset: 0x0000BE04
				public override IEnumerable<ImportDefinition> ImportDefinitions
				{
					get
					{
						return this._definition.ImportDefinitions;
					}
				}

				// Token: 0x060004A9 RID: 1193 RVA: 0x0000DC11 File Offset: 0x0000BE11
				public override object GetExportedValue(ExportDefinition definition)
				{
					if (definition != this._definition.FactoryExportDefinition)
					{
						throw ExceptionBuilder.CreateExportDefinitionNotOnThisComposablePart("definition");
					}
					return this._export.Value;
				}

				// Token: 0x060004AA RID: 1194 RVA: 0x0000DC37 File Offset: 0x0000BE37
				public override void SetImport(ImportDefinition definition, IEnumerable<Export> exports)
				{
					throw ExceptionBuilder.CreateImportDefinitionNotOnThisComposablePart("definition");
				}

				// Token: 0x060004AB RID: 1195 RVA: 0x0000DC44 File Offset: 0x0000BE44
				public void Dispose()
				{
					IDisposable disposable = this._export as IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}

				// Token: 0x040001D3 RID: 467
				private readonly CatalogExportProvider.FactoryExport.FactoryExportPartDefinition _definition;

				// Token: 0x040001D4 RID: 468
				private readonly Export _export;
			}
		}

		// Token: 0x020000AC RID: 172
		internal class PartCreatorExport : CatalogExportProvider.FactoryExport
		{
			// Token: 0x060004AC RID: 1196 RVA: 0x0000DC66 File Offset: 0x0000BE66
			public PartCreatorExport(CatalogExportProvider catalogExportProvider, ComposablePartDefinition partDefinition, ExportDefinition exportDefinition) : base(partDefinition, exportDefinition)
			{
				this._catalogExportProvider = catalogExportProvider;
			}

			// Token: 0x060004AD RID: 1197 RVA: 0x0000DC77 File Offset: 0x0000BE77
			public override Export CreateExportProduct()
			{
				return new CatalogExportProvider.NonSharedCatalogExport(this._catalogExportProvider, base.UnderlyingPartDefinition, base.UnderlyingExportDefinition);
			}

			// Token: 0x040001D5 RID: 469
			private readonly CatalogExportProvider _catalogExportProvider;
		}

		// Token: 0x020000AD RID: 173
		internal class ScopeFactoryExport : CatalogExportProvider.FactoryExport
		{
			// Token: 0x060004AE RID: 1198 RVA: 0x0000DC90 File Offset: 0x0000BE90
			internal ScopeFactoryExport(CatalogExportProvider.ScopeManager scopeManager, CompositionScopeDefinition catalog, ComposablePartDefinition partDefinition, ExportDefinition exportDefinition) : base(partDefinition, exportDefinition)
			{
				this._scopeManager = scopeManager;
				this._catalog = catalog;
			}

			// Token: 0x060004AF RID: 1199 RVA: 0x0000DCA9 File Offset: 0x0000BEA9
			public virtual Export CreateExportProduct(Func<ComposablePartDefinition, bool> filter)
			{
				return new CatalogExportProvider.ScopeFactoryExport.ScopeCatalogExport(this, filter);
			}

			// Token: 0x060004B0 RID: 1200 RVA: 0x0000DCB2 File Offset: 0x0000BEB2
			public override Export CreateExportProduct()
			{
				return new CatalogExportProvider.ScopeFactoryExport.ScopeCatalogExport(this, null);
			}

			// Token: 0x040001D6 RID: 470
			private readonly CatalogExportProvider.ScopeManager _scopeManager;

			// Token: 0x040001D7 RID: 471
			private readonly CompositionScopeDefinition _catalog;

			// Token: 0x020000AE RID: 174
			private sealed class ScopeCatalogExport : Export, IDisposable
			{
				// Token: 0x060004B1 RID: 1201 RVA: 0x0000DCBB File Offset: 0x0000BEBB
				public ScopeCatalogExport(CatalogExportProvider.ScopeFactoryExport scopeFactoryExport, Func<ComposablePartDefinition, bool> catalogFilter)
				{
					this._scopeFactoryExport = scopeFactoryExport;
					this._catalogFilter = catalogFilter;
				}

				// Token: 0x1700014F RID: 335
				// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000DCDC File Offset: 0x0000BEDC
				public override ExportDefinition Definition
				{
					get
					{
						return this._scopeFactoryExport.UnderlyingExportDefinition;
					}
				}

				// Token: 0x060004B3 RID: 1203 RVA: 0x0000DCEC File Offset: 0x0000BEEC
				protected override object GetExportedValueCore()
				{
					if (this._export == null)
					{
						CompositionScopeDefinition childCatalog = new CompositionScopeDefinition(new FilteredCatalog(this._scopeFactoryExport._catalog, this._catalogFilter), this._scopeFactoryExport._catalog.Children);
						CompositionContainer compositionContainer = this._scopeFactoryExport._scopeManager.CreateChildContainer(childCatalog);
						Export export = compositionContainer.CatalogExportProvider.CreateExport(this._scopeFactoryExport.UnderlyingPartDefinition, this._scopeFactoryExport.UnderlyingExportDefinition, false, CreationPolicy.Any);
						object @lock = this._lock;
						lock (@lock)
						{
							if (this._export == null)
							{
								this._childContainer = compositionContainer;
								Thread.MemoryBarrier();
								this._export = export;
								compositionContainer = null;
							}
						}
						if (compositionContainer != null)
						{
							compositionContainer.Dispose();
						}
					}
					return this._export.Value;
				}

				// Token: 0x060004B4 RID: 1204 RVA: 0x0000DDC8 File Offset: 0x0000BFC8
				public void Dispose()
				{
					CompositionContainer compositionContainer = null;
					if (this._export != null)
					{
						object @lock = this._lock;
						lock (@lock)
						{
							Export export = this._export;
							compositionContainer = this._childContainer;
							this._childContainer = null;
							Thread.MemoryBarrier();
							this._export = null;
						}
					}
					if (compositionContainer != null)
					{
						compositionContainer.Dispose();
					}
				}

				// Token: 0x040001D8 RID: 472
				private readonly CatalogExportProvider.ScopeFactoryExport _scopeFactoryExport;

				// Token: 0x040001D9 RID: 473
				private Func<ComposablePartDefinition, bool> _catalogFilter;

				// Token: 0x040001DA RID: 474
				private CompositionContainer _childContainer;

				// Token: 0x040001DB RID: 475
				private Export _export;

				// Token: 0x040001DC RID: 476
				private readonly object _lock = new object();
			}
		}

		// Token: 0x020000AF RID: 175
		internal class ScopeManager : ExportProvider
		{
			// Token: 0x060004B5 RID: 1205 RVA: 0x0000DE38 File Offset: 0x0000C038
			public ScopeManager(CatalogExportProvider catalogExportProvider, CompositionScopeDefinition scopeDefinition)
			{
				Assumes.NotNull<CatalogExportProvider>(catalogExportProvider);
				Assumes.NotNull<CompositionScopeDefinition>(scopeDefinition);
				this._scopeDefinition = scopeDefinition;
				this._catalogExportProvider = catalogExportProvider;
			}

			// Token: 0x060004B6 RID: 1206 RVA: 0x0000DE5C File Offset: 0x0000C05C
			protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
			{
				List<Export> list = new List<Export>();
				ImportDefinition importDefinition = CatalogExportProvider.ScopeManager.TranslateImport(definition);
				if (importDefinition == null)
				{
					return list;
				}
				foreach (CompositionScopeDefinition compositionScopeDefinition in this._scopeDefinition.Children)
				{
					foreach (Tuple<ComposablePartDefinition, ExportDefinition> tuple in compositionScopeDefinition.GetExportsFromPublicSurface(importDefinition))
					{
						using (CompositionContainer compositionContainer = this.CreateChildContainer(compositionScopeDefinition))
						{
							using (AtomicComposition atomicComposition2 = new AtomicComposition(atomicComposition))
							{
								if (!compositionContainer.CatalogExportProvider.DetermineRejection(tuple.Item1, atomicComposition2))
								{
									list.Add(this.CreateScopeExport(compositionScopeDefinition, tuple.Item1, tuple.Item2));
								}
							}
						}
					}
				}
				return list;
			}

			// Token: 0x060004B7 RID: 1207 RVA: 0x0000DF74 File Offset: 0x0000C174
			private Export CreateScopeExport(CompositionScopeDefinition childCatalog, ComposablePartDefinition partDefinition, ExportDefinition exportDefinition)
			{
				return new CatalogExportProvider.ScopeFactoryExport(this, childCatalog, partDefinition, exportDefinition);
			}

			// Token: 0x060004B8 RID: 1208 RVA: 0x0000DF7F File Offset: 0x0000C17F
			internal CompositionContainer CreateChildContainer(ComposablePartCatalog childCatalog)
			{
				return new CompositionContainer(childCatalog, this._catalogExportProvider._compositionOptions, new ExportProvider[]
				{
					this._catalogExportProvider._sourceProvider
				});
			}

			// Token: 0x060004B9 RID: 1209 RVA: 0x0000DFA8 File Offset: 0x0000C1A8
			private static ImportDefinition TranslateImport(ImportDefinition definition)
			{
				IPartCreatorImportDefinition partCreatorImportDefinition = definition as IPartCreatorImportDefinition;
				if (partCreatorImportDefinition == null)
				{
					return null;
				}
				ContractBasedImportDefinition productImportDefinition = partCreatorImportDefinition.ProductImportDefinition;
				ImportDefinition result = null;
				CreationPolicy requiredCreationPolicy = productImportDefinition.RequiredCreationPolicy;
				if (requiredCreationPolicy != CreationPolicy.Any)
				{
					if (requiredCreationPolicy - CreationPolicy.NonShared <= 1)
					{
						result = new ContractBasedImportDefinition(productImportDefinition.ContractName, productImportDefinition.RequiredTypeIdentity, productImportDefinition.RequiredMetadata, productImportDefinition.Cardinality, productImportDefinition.IsRecomposable, productImportDefinition.IsPrerequisite, CreationPolicy.Any, productImportDefinition.Metadata);
					}
				}
				else
				{
					result = productImportDefinition;
				}
				return result;
			}

			// Token: 0x040001DD RID: 477
			private CompositionScopeDefinition _scopeDefinition;

			// Token: 0x040001DE RID: 478
			private CatalogExportProvider _catalogExportProvider;
		}

		// Token: 0x020000B0 RID: 176
		private class InnerCatalogExportProvider : ExportProvider
		{
			// Token: 0x060004BA RID: 1210 RVA: 0x0000E010 File Offset: 0x0000C210
			public InnerCatalogExportProvider(Func<ImportDefinition, AtomicComposition, IEnumerable<Export>> getExportsCore)
			{
				this._getExportsCore = getExportsCore;
			}

			// Token: 0x060004BB RID: 1211 RVA: 0x0000E01F File Offset: 0x0000C21F
			protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
			{
				Assumes.NotNull<Func<ImportDefinition, AtomicComposition, IEnumerable<Export>>>(this._getExportsCore);
				return this._getExportsCore(definition, atomicComposition);
			}

			// Token: 0x040001DF RID: 479
			private Func<ImportDefinition, AtomicComposition, IEnumerable<Export>> _getExportsCore;
		}

		// Token: 0x020000B1 RID: 177
		private enum AtomicCompositionQueryState
		{
			// Token: 0x040001E1 RID: 481
			Unknown,
			// Token: 0x040001E2 RID: 482
			TreatAsRejected,
			// Token: 0x040001E3 RID: 483
			TreatAsValidated,
			// Token: 0x040001E4 RID: 484
			NeedsTesting
		}

		// Token: 0x020000B2 RID: 178
		private class CatalogPart
		{
			// Token: 0x060004BC RID: 1212 RVA: 0x0000E039 File Offset: 0x0000C239
			public CatalogPart(ComposablePart part)
			{
				this.Part = part;
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000E048 File Offset: 0x0000C248
			// (set) Token: 0x060004BE RID: 1214 RVA: 0x0000E050 File Offset: 0x0000C250
			public ComposablePart Part
			{
				[CompilerGenerated]
				get
				{
					return this.<Part>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Part>k__BackingField = value;
				}
			}

			// Token: 0x17000151 RID: 337
			// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000E059 File Offset: 0x0000C259
			// (set) Token: 0x060004C0 RID: 1216 RVA: 0x0000E063 File Offset: 0x0000C263
			public bool ImportsSatisfied
			{
				get
				{
					return this._importsSatisfied;
				}
				set
				{
					this._importsSatisfied = value;
				}
			}

			// Token: 0x040001E5 RID: 485
			private volatile bool _importsSatisfied;

			// Token: 0x040001E6 RID: 486
			[CompilerGenerated]
			private ComposablePart <Part>k__BackingField;
		}

		// Token: 0x020000B3 RID: 179
		[CompilerGenerated]
		private sealed class <>c__DisplayClass35_0
		{
			// Token: 0x060004C1 RID: 1217 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass35_0()
			{
			}

			// Token: 0x060004C2 RID: 1218 RVA: 0x0000E06E File Offset: 0x0000C26E
			internal void <OnCatalogChanging>b__0()
			{
				this.<>4__this.OnExportsChanged(new ExportsChangeEventArgs(this.addedExports, this.removedExports, null));
			}

			// Token: 0x040001E7 RID: 487
			public CatalogExportProvider <>4__this;

			// Token: 0x040001E8 RID: 488
			public IEnumerable<ExportDefinition> addedExports;

			// Token: 0x040001E9 RID: 489
			public IEnumerable<ExportDefinition> removedExports;
		}

		// Token: 0x020000B4 RID: 180
		[CompilerGenerated]
		private sealed class <>c__DisplayClass35_1
		{
			// Token: 0x060004C3 RID: 1219 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass35_1()
			{
			}

			// Token: 0x060004C4 RID: 1220 RVA: 0x0000E090 File Offset: 0x0000C290
			internal void <OnCatalogChanging>b__1()
			{
				using (this.CS$<>8__locals1.<>4__this._lock.LockStateForWrite())
				{
					this.CS$<>8__locals1.<>4__this._activatedParts.Remove(this.capturedDefinition);
				}
			}

			// Token: 0x040001EA RID: 490
			public ComposablePartDefinition capturedDefinition;

			// Token: 0x040001EB RID: 491
			public CatalogExportProvider.<>c__DisplayClass35_0 CS$<>8__locals1;
		}

		// Token: 0x020000B5 RID: 181
		[CompilerGenerated]
		private sealed class <>c__DisplayClass39_0
		{
			// Token: 0x060004C5 RID: 1221 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass39_0()
			{
			}

			// Token: 0x060004C6 RID: 1222 RVA: 0x0000E0EC File Offset: 0x0000C2EC
			internal void <ReleasePart>b__0()
			{
				this.<>4__this.AllowPartCollection(this.exportedValue);
			}

			// Token: 0x060004C7 RID: 1223 RVA: 0x0000E100 File Offset: 0x0000C300
			internal void <ReleasePart>b__1()
			{
				bool flag = false;
				using (this.<>4__this._lock.LockStateForWrite())
				{
					flag = this.<>4__this._partsToDispose.Remove(this.diposablePart);
				}
				if (flag)
				{
					this.diposablePart.Dispose();
				}
			}

			// Token: 0x040001EC RID: 492
			public CatalogExportProvider <>4__this;

			// Token: 0x040001ED RID: 493
			public object exportedValue;

			// Token: 0x040001EE RID: 494
			public IDisposable diposablePart;
		}

		// Token: 0x020000B6 RID: 182
		[CompilerGenerated]
		private sealed class <>c__DisplayClass43_0
		{
			// Token: 0x060004C8 RID: 1224 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass43_0()
			{
			}

			// Token: 0x060004C9 RID: 1225 RVA: 0x0000E164 File Offset: 0x0000C364
			internal bool <DetermineRejection>b__2(ComposablePartDefinition def)
			{
				return this.definition.Equals(def);
			}

			// Token: 0x060004CA RID: 1226 RVA: 0x0000E174 File Offset: 0x0000C374
			internal void <DetermineRejection>b__0()
			{
				using (this.<>4__this._lock.LockStateForWrite())
				{
					this.<>4__this._rejectedParts.Add(this.definition);
				}
				CompositionTrace.PartDefinitionRejected(this.definition, this.exception);
			}

			// Token: 0x060004CB RID: 1227 RVA: 0x0000E164 File Offset: 0x0000C364
			internal bool <DetermineRejection>b__1(ComposablePartDefinition def)
			{
				return this.definition.Equals(def);
			}

			// Token: 0x040001EF RID: 495
			public ComposablePartDefinition definition;

			// Token: 0x040001F0 RID: 496
			public CatalogExportProvider <>4__this;

			// Token: 0x040001F1 RID: 497
			public ChangeRejectedException exception;
		}

		// Token: 0x020000B7 RID: 183
		[CompilerGenerated]
		private sealed class <>c__DisplayClass43_1
		{
			// Token: 0x060004CC RID: 1228 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass43_1()
			{
			}

			// Token: 0x060004CD RID: 1229 RVA: 0x0000E1D8 File Offset: 0x0000C3D8
			internal void <DetermineRejection>b__3()
			{
				using (this.CS$<>8__locals1.<>4__this._lock.LockStateForWrite())
				{
					if (!this.CS$<>8__locals1.<>4__this._activatedParts.ContainsKey(this.CS$<>8__locals1.definition))
					{
						this.CS$<>8__locals1.<>4__this._activatedParts.Add(this.CS$<>8__locals1.definition, new CatalogExportProvider.CatalogPart(this.newPart));
						IDisposable disposable2 = this.newPart as IDisposable;
						if (disposable2 != null)
						{
							this.CS$<>8__locals1.<>4__this._partsToDispose.Add(disposable2);
						}
					}
				}
			}

			// Token: 0x040001F2 RID: 498
			public ComposablePart newPart;

			// Token: 0x040001F3 RID: 499
			public CatalogExportProvider.<>c__DisplayClass43_0 CS$<>8__locals1;
		}

		// Token: 0x020000B8 RID: 184
		[CompilerGenerated]
		private sealed class <>c__DisplayClass44_0
		{
			// Token: 0x060004CE RID: 1230 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass44_0()
			{
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x0000E28C File Offset: 0x0000C48C
			internal bool <UpdateRejections>b__0(ComposablePartDefinition def)
			{
				return this.affectedRejections.Contains(def);
			}

			// Token: 0x060004D0 RID: 1232 RVA: 0x0000E29A File Offset: 0x0000C49A
			internal void <UpdateRejections>b__1()
			{
				this.<>4__this.OnExportsChanged(new ExportsChangeEventArgs(this.resurrectedExports, new ExportDefinition[0], null));
			}

			// Token: 0x040001F4 RID: 500
			public CatalogExportProvider <>4__this;

			// Token: 0x040001F5 RID: 501
			public HashSet<ComposablePartDefinition> affectedRejections;

			// Token: 0x040001F6 RID: 502
			public List<ExportDefinition> resurrectedExports;
		}

		// Token: 0x020000B9 RID: 185
		[CompilerGenerated]
		private sealed class <>c__DisplayClass44_1
		{
			// Token: 0x060004D1 RID: 1233 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass44_1()
			{
			}

			// Token: 0x060004D2 RID: 1234 RVA: 0x0000E2B9 File Offset: 0x0000C4B9
			internal bool <UpdateRejections>b__2(ExportDefinition export)
			{
				return this.import.IsConstraintSatisfiedBy(export);
			}

			// Token: 0x040001F7 RID: 503
			public ImportDefinition import;
		}

		// Token: 0x020000BA RID: 186
		[CompilerGenerated]
		private sealed class <>c__DisplayClass44_2
		{
			// Token: 0x060004D3 RID: 1235 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass44_2()
			{
			}

			// Token: 0x060004D4 RID: 1236 RVA: 0x0000E2C8 File Offset: 0x0000C4C8
			internal void <UpdateRejections>b__3()
			{
				using (this.CS$<>8__locals1.<>4__this._lock.LockStateForWrite())
				{
					this.CS$<>8__locals1.<>4__this._rejectedParts.Remove(this.capturedPartDefinition);
				}
				CompositionTrace.PartDefinitionResurrected(this.capturedPartDefinition);
			}

			// Token: 0x040001F8 RID: 504
			public ComposablePartDefinition capturedPartDefinition;

			// Token: 0x040001F9 RID: 505
			public CatalogExportProvider.<>c__DisplayClass44_0 CS$<>8__locals1;
		}

		// Token: 0x020000BB RID: 187
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060004D5 RID: 1237 RVA: 0x0000E330 File Offset: 0x0000C530
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060004D6 RID: 1238 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c()
			{
			}

			// Token: 0x060004D7 RID: 1239 RVA: 0x0000A969 File Offset: 0x00008B69
			internal CatalogExportProvider.AtomicCompositionQueryState <GetAtomicCompositionQuery>b__49_0(ComposablePartDefinition definition)
			{
				return CatalogExportProvider.AtomicCompositionQueryState.Unknown;
			}

			// Token: 0x040001FA RID: 506
			public static readonly CatalogExportProvider.<>c <>9 = new CatalogExportProvider.<>c();

			// Token: 0x040001FB RID: 507
			public static Func<ComposablePartDefinition, CatalogExportProvider.AtomicCompositionQueryState> <>9__49_0;
		}

		// Token: 0x020000BC RID: 188
		[CompilerGenerated]
		private sealed class <>c__DisplayClass50_0
		{
			// Token: 0x060004D8 RID: 1240 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass50_0()
			{
			}

			// Token: 0x060004D9 RID: 1241 RVA: 0x0000E33C File Offset: 0x0000C53C
			internal CatalogExportProvider.AtomicCompositionQueryState <UpdateAtomicCompositionQuery>b__0(ComposablePartDefinition definition)
			{
				if (this.query(definition))
				{
					return this.state;
				}
				return this.parentQuery(definition);
			}

			// Token: 0x040001FC RID: 508
			public Func<ComposablePartDefinition, bool> query;

			// Token: 0x040001FD RID: 509
			public CatalogExportProvider.AtomicCompositionQueryState state;

			// Token: 0x040001FE RID: 510
			public Func<ComposablePartDefinition, CatalogExportProvider.AtomicCompositionQueryState> parentQuery;
		}
	}
}
