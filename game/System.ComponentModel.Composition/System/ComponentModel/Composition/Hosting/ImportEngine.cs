using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Internal;
using Microsoft.Internal.Collections;

namespace System.ComponentModel.Composition.Hosting
{
	/// <summary>Performs composition for containers.</summary>
	// Token: 0x020000E9 RID: 233
	public class ImportEngine : ICompositionService, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ImportEngine" /> class.</summary>
		/// <param name="sourceProvider">The <see cref="T:System.ComponentModel.Composition.Hosting.ExportProvider" /> that provides the <see cref="T:System.ComponentModel.Composition.Hosting.ImportEngine" /> access to <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.</param>
		// Token: 0x0600062C RID: 1580 RVA: 0x00012C11 File Offset: 0x00010E11
		public ImportEngine(ExportProvider sourceProvider) : this(sourceProvider, CompositionOptions.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ImportEngine" /> class, optionally in thread-safe mode.</summary>
		/// <param name="sourceProvider">The <see cref="T:System.ComponentModel.Composition.Hosting.ExportProvider" /> that provides the <see cref="T:System.ComponentModel.Composition.Hosting.ImportEngine" /> access to <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.</param>
		/// <param name="isThreadSafe">
		///   <see langword="true" /> if thread safety is required; otherwise, <see langword="false" />.</param>
		// Token: 0x0600062D RID: 1581 RVA: 0x00012C1B File Offset: 0x00010E1B
		public ImportEngine(ExportProvider sourceProvider, bool isThreadSafe) : this(sourceProvider, isThreadSafe ? CompositionOptions.IsThreadSafe : CompositionOptions.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ImportEngine" /> class with the specified options.</summary>
		/// <param name="sourceProvider">The <see cref="T:System.ComponentModel.Composition.Hosting.ExportProvider" /> that provides the <see cref="T:System.ComponentModel.Composition.Hosting.ImportEngine" /> access to <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.</param>
		/// <param name="compositionOptions">An object that specifies options that affect the behavior of the engine.</param>
		// Token: 0x0600062E RID: 1582 RVA: 0x00012C2C File Offset: 0x00010E2C
		public ImportEngine(ExportProvider sourceProvider, CompositionOptions compositionOptions)
		{
			Requires.NotNull<ExportProvider>(sourceProvider, "sourceProvider");
			this._compositionOptions = compositionOptions;
			this._sourceProvider = sourceProvider;
			this._sourceProvider.ExportsChanging += this.OnExportsChanging;
			this._lock = new CompositionLock(compositionOptions.HasFlag(CompositionOptions.IsThreadSafe));
		}

		/// <summary>Previews all the required imports for the specified part to make sure that they can be satisfied, without actually setting them.</summary>
		/// <param name="part">The part to preview the imports of.</param>
		/// <param name="atomicComposition">The composition transaction to use, or <see langword="null" /> for no composition transaction.</param>
		// Token: 0x0600062F RID: 1583 RVA: 0x00012CAC File Offset: 0x00010EAC
		public void PreviewImports(ComposablePart part, AtomicComposition atomicComposition)
		{
			this.ThrowIfDisposed();
			Requires.NotNull<ComposablePart>(part, "part");
			if (this._compositionOptions.HasFlag(CompositionOptions.DisableSilentRejection))
			{
				return;
			}
			IDisposable compositionLockHolder = this._lock.IsThreadSafe ? this._lock.LockComposition() : null;
			bool flag = compositionLockHolder != null;
			try
			{
				if (flag && atomicComposition != null)
				{
					atomicComposition.AddRevertAction(delegate
					{
						compositionLockHolder.Dispose();
					});
				}
				ImportEngine.PartManager partManager = this.GetPartManager(part, true);
				this.TryPreviewImportsStateMachine(partManager, part, atomicComposition).ThrowOnErrors(atomicComposition);
				this.StartSatisfyingImports(partManager, atomicComposition);
				if (flag && atomicComposition != null)
				{
					atomicComposition.AddCompleteAction(delegate
					{
						compositionLockHolder.Dispose();
					});
				}
			}
			finally
			{
				if (flag && atomicComposition == null)
				{
					compositionLockHolder.Dispose();
				}
			}
		}

		/// <summary>Satisfies the imports of the specified part.</summary>
		/// <param name="part">The part to satisfy the imports of.</param>
		// Token: 0x06000630 RID: 1584 RVA: 0x00012D8C File Offset: 0x00010F8C
		public void SatisfyImports(ComposablePart part)
		{
			this.ThrowIfDisposed();
			Requires.NotNull<ComposablePart>(part, "part");
			ImportEngine.PartManager partManager = this.GetPartManager(part, true);
			if (partManager.State == ImportEngine.ImportState.Composed)
			{
				return;
			}
			using (this._lock.LockComposition())
			{
				this.TrySatisfyImports(partManager, part, true).ThrowOnErrors();
			}
		}

		/// <summary>Satisfies the imports of the specified part without registering them for recomposition.</summary>
		/// <param name="part">The part to satisfy the imports of.</param>
		// Token: 0x06000631 RID: 1585 RVA: 0x00012DF8 File Offset: 0x00010FF8
		public void SatisfyImportsOnce(ComposablePart part)
		{
			this.ThrowIfDisposed();
			Requires.NotNull<ComposablePart>(part, "part");
			ImportEngine.PartManager partManager = this.GetPartManager(part, true);
			if (partManager.State == ImportEngine.ImportState.Composed)
			{
				return;
			}
			using (this._lock.LockComposition())
			{
				this.TrySatisfyImports(partManager, part, false).ThrowOnErrors();
			}
		}

		/// <summary>Releases all the exports used to satisfy the imports of the specified part.</summary>
		/// <param name="part">The part to release the imports of.</param>
		/// <param name="atomicComposition">The composition transaction to use, or <see langword="null" /> for no composition transaction.</param>
		// Token: 0x06000632 RID: 1586 RVA: 0x00012E64 File Offset: 0x00011064
		public void ReleaseImports(ComposablePart part, AtomicComposition atomicComposition)
		{
			this.ThrowIfDisposed();
			Requires.NotNull<ComposablePart>(part, "part");
			using (this._lock.LockComposition())
			{
				ImportEngine.PartManager partManager = this.GetPartManager(part, false);
				if (partManager != null)
				{
					this.StopSatisfyingImports(partManager, atomicComposition);
				}
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ImportEngine" /> class.</summary>
		// Token: 0x06000633 RID: 1587 RVA: 0x00012EC0 File Offset: 0x000110C0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.ImportEngine" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000634 RID: 1588 RVA: 0x00012ED0 File Offset: 0x000110D0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				bool flag = false;
				ExportProvider exportProvider = null;
				using (this._lock.LockStateForWrite())
				{
					if (!this._isDisposed)
					{
						exportProvider = this._sourceProvider;
						this._sourceProvider = null;
						this._recompositionManager = null;
						this._partManagers = null;
						this._isDisposed = true;
						flag = true;
					}
				}
				if (exportProvider != null)
				{
					exportProvider.ExportsChanging -= this.OnExportsChanging;
				}
				if (flag)
				{
					this._lock.Dispose();
				}
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00012F6C File Offset: 0x0001116C
		private CompositionResult TryPreviewImportsStateMachine(ImportEngine.PartManager partManager, ComposablePart part, AtomicComposition atomicComposition)
		{
			CompositionResult result = CompositionResult.SucceededResult;
			if (partManager.State == ImportEngine.ImportState.ImportsPreviewing)
			{
				return new CompositionResult(new CompositionError[]
				{
					ErrorBuilder.CreatePartCycle(part)
				});
			}
			if (partManager.State == ImportEngine.ImportState.NoImportsSatisfied)
			{
				partManager.State = ImportEngine.ImportState.ImportsPreviewing;
				IEnumerable<ImportDefinition> imports = part.ImportDefinitions.Where(new Func<ImportDefinition, bool>(ImportEngine.IsRequiredImportForPreview));
				atomicComposition.AddRevertActionAllowNull(delegate
				{
					partManager.State = ImportEngine.ImportState.NoImportsSatisfied;
				});
				result = result.MergeResult(this.TrySatisfyImportSubset(partManager, imports, atomicComposition));
				if (!result.Succeeded)
				{
					partManager.State = ImportEngine.ImportState.NoImportsSatisfied;
					return result;
				}
				partManager.State = ImportEngine.ImportState.ImportsPreviewed;
			}
			return result;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001302C File Offset: 0x0001122C
		private CompositionResult TrySatisfyImportsStateMachine(ImportEngine.PartManager partManager, ComposablePart part)
		{
			CompositionResult result = CompositionResult.SucceededResult;
			while (partManager.State < ImportEngine.ImportState.Composed)
			{
				ImportEngine.ImportState state = partManager.State;
				switch (partManager.State)
				{
				case ImportEngine.ImportState.NoImportsSatisfied:
				case ImportEngine.ImportState.ImportsPreviewed:
				{
					partManager.State = ImportEngine.ImportState.PreExportImportsSatisfying;
					IEnumerable<ImportDefinition> imports = from import in part.ImportDefinitions
					where import.IsPrerequisite
					select import;
					result = result.MergeResult(this.TrySatisfyImportSubset(partManager, imports, null));
					partManager.State = ImportEngine.ImportState.PreExportImportsSatisfied;
					break;
				}
				case ImportEngine.ImportState.ImportsPreviewing:
					return new CompositionResult(new CompositionError[]
					{
						ErrorBuilder.CreatePartCycle(part)
					});
				case ImportEngine.ImportState.PreExportImportsSatisfying:
				case ImportEngine.ImportState.PostExportImportsSatisfying:
					if (this.InPrerequisiteLoop())
					{
						return result.MergeError(ErrorBuilder.CreatePartCycle(part));
					}
					return result;
				case ImportEngine.ImportState.PreExportImportsSatisfied:
				{
					partManager.State = ImportEngine.ImportState.PostExportImportsSatisfying;
					IEnumerable<ImportDefinition> imports2 = from import in part.ImportDefinitions
					where !import.IsPrerequisite
					select import;
					result = result.MergeResult(this.TrySatisfyImportSubset(partManager, imports2, null));
					partManager.State = ImportEngine.ImportState.PostExportImportsSatisfied;
					break;
				}
				case ImportEngine.ImportState.PostExportImportsSatisfied:
					partManager.State = ImportEngine.ImportState.ComposedNotifying;
					partManager.ClearSavedImports();
					result = result.MergeResult(partManager.TryOnComposed());
					partManager.State = ImportEngine.ImportState.Composed;
					break;
				case ImportEngine.ImportState.ComposedNotifying:
					return result;
				}
				if (!result.Succeeded)
				{
					partManager.State = state;
					return result;
				}
			}
			return result;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001318C File Offset: 0x0001138C
		private CompositionResult TrySatisfyImports(ImportEngine.PartManager partManager, ComposablePart part, bool shouldTrackImports)
		{
			Assumes.NotNull<ComposablePart>(part);
			CompositionResult result = CompositionResult.SucceededResult;
			if (partManager.State == ImportEngine.ImportState.Composed)
			{
				return result;
			}
			if (this._recursionStateStack.Count >= 100)
			{
				return result.MergeError(ErrorBuilder.ComposeTookTooManyIterations(100));
			}
			this._recursionStateStack.Push(partManager);
			try
			{
				result = result.MergeResult(this.TrySatisfyImportsStateMachine(partManager, part));
			}
			finally
			{
				this._recursionStateStack.Pop();
			}
			if (shouldTrackImports)
			{
				this.StartSatisfyingImports(partManager, null);
			}
			return result;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00013218 File Offset: 0x00011418
		private CompositionResult TrySatisfyImportSubset(ImportEngine.PartManager partManager, IEnumerable<ImportDefinition> imports, AtomicComposition atomicComposition)
		{
			CompositionResult result = CompositionResult.SucceededResult;
			ComposablePart part = partManager.Part;
			foreach (ImportDefinition importDefinition in imports)
			{
				Export[] array = partManager.GetSavedImport(importDefinition);
				if (array == null)
				{
					CompositionResult<IEnumerable<Export>> compositionResult = ImportEngine.TryGetExports(this._sourceProvider, part, importDefinition, atomicComposition);
					if (!compositionResult.Succeeded)
					{
						result = result.MergeResult(compositionResult.ToResult());
						continue;
					}
					array = compositionResult.Value.AsArray<Export>();
				}
				if (atomicComposition == null)
				{
					result = result.MergeResult(partManager.TrySetImport(importDefinition, array));
				}
				else
				{
					partManager.SetSavedImport(importDefinition, array, atomicComposition);
				}
			}
			return result;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x000132CC File Offset: 0x000114CC
		private void OnExportsChanging(object sender, ExportsChangeEventArgs e)
		{
			CompositionResult compositionResult = CompositionResult.SucceededResult;
			AtomicComposition atomicComposition = e.AtomicComposition;
			IEnumerable<ImportEngine.PartManager> enumerable = this._recompositionManager.GetAffectedParts(e.ChangedContractNames);
			ImportEngine.EngineContext engineContext;
			if (atomicComposition != null && atomicComposition.TryGetValue<ImportEngine.EngineContext>(this, out engineContext))
			{
				enumerable = enumerable.ConcatAllowingNull(engineContext.GetAddedPartManagers()).Except(engineContext.GetRemovedPartManagers());
			}
			IEnumerable<ExportDefinition> changedExports = e.AddedExports.ConcatAllowingNull(e.RemovedExports);
			foreach (ImportEngine.PartManager partManager in enumerable)
			{
				compositionResult = compositionResult.MergeResult(this.TryRecomposeImports(partManager, changedExports, atomicComposition));
			}
			compositionResult.ThrowOnErrors(atomicComposition);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00013388 File Offset: 0x00011588
		private CompositionResult TryRecomposeImports(ImportEngine.PartManager partManager, IEnumerable<ExportDefinition> changedExports, AtomicComposition atomicComposition)
		{
			CompositionResult result = CompositionResult.SucceededResult;
			ImportEngine.ImportState state = partManager.State;
			if (state != ImportEngine.ImportState.ImportsPreviewed && state != ImportEngine.ImportState.Composed)
			{
				return new CompositionResult(new CompositionError[]
				{
					ErrorBuilder.InvalidStateForRecompposition(partManager.Part)
				});
			}
			IEnumerable<ImportDefinition> affectedImports = ImportEngine.RecompositionManager.GetAffectedImports(partManager.Part, changedExports);
			bool flag = partManager.State == ImportEngine.ImportState.Composed;
			bool flag2 = false;
			foreach (ImportDefinition import in affectedImports)
			{
				result = result.MergeResult(this.TryRecomposeImport(partManager, flag, import, atomicComposition));
				flag2 = true;
			}
			if (result.Succeeded && flag2 && flag)
			{
				if (atomicComposition == null)
				{
					result = result.MergeResult(partManager.TryOnComposed());
				}
				else
				{
					atomicComposition.AddCompleteAction(delegate
					{
						partManager.TryOnComposed().ThrowOnErrors();
					});
				}
			}
			return result;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001348C File Offset: 0x0001168C
		private CompositionResult TryRecomposeImport(ImportEngine.PartManager partManager, bool partComposed, ImportDefinition import, AtomicComposition atomicComposition)
		{
			if (partComposed && !import.IsRecomposable)
			{
				return new CompositionResult(new CompositionError[]
				{
					ErrorBuilder.PreventedByExistingImport(partManager.Part, import)
				});
			}
			CompositionResult<IEnumerable<Export>> compositionResult = ImportEngine.TryGetExports(this._sourceProvider, partManager.Part, import, atomicComposition);
			if (!compositionResult.Succeeded)
			{
				return compositionResult.ToResult();
			}
			Export[] exports = compositionResult.Value.AsArray<Export>();
			if (partComposed)
			{
				if (atomicComposition == null)
				{
					return partManager.TrySetImport(import, exports);
				}
				atomicComposition.AddCompleteAction(delegate
				{
					partManager.TrySetImport(import, exports).ThrowOnErrors();
				});
			}
			else
			{
				partManager.SetSavedImport(import, exports, atomicComposition);
			}
			return CompositionResult.SucceededResult;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00013576 File Offset: 0x00011776
		private void StartSatisfyingImports(ImportEngine.PartManager partManager, AtomicComposition atomicComposition)
		{
			if (atomicComposition == null)
			{
				if (!partManager.TrackingImports)
				{
					partManager.TrackingImports = true;
					this._recompositionManager.AddPartToIndex(partManager);
					return;
				}
			}
			else
			{
				this.GetEngineContext(atomicComposition).AddPartManager(partManager);
			}
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x000135A4 File Offset: 0x000117A4
		private void StopSatisfyingImports(ImportEngine.PartManager partManager, AtomicComposition atomicComposition)
		{
			if (atomicComposition == null)
			{
				this._partManagers.Remove(partManager.Part);
				partManager.DisposeAllDependencies();
				if (partManager.TrackingImports)
				{
					partManager.TrackingImports = false;
					this._recompositionManager.AddPartToUnindex(partManager);
					return;
				}
			}
			else
			{
				this.GetEngineContext(atomicComposition).RemovePartManager(partManager);
			}
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x000135F8 File Offset: 0x000117F8
		private ImportEngine.PartManager GetPartManager(ComposablePart part, bool createIfNotpresent)
		{
			ImportEngine.PartManager partManager = null;
			using (this._lock.LockStateForRead())
			{
				if (this._partManagers.TryGetValue(part, out partManager))
				{
					return partManager;
				}
			}
			if (createIfNotpresent)
			{
				using (this._lock.LockStateForWrite())
				{
					if (!this._partManagers.TryGetValue(part, out partManager))
					{
						partManager = new ImportEngine.PartManager(this, part);
						this._partManagers.Add(part, partManager);
					}
				}
			}
			return partManager;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00013694 File Offset: 0x00011894
		private ImportEngine.EngineContext GetEngineContext(AtomicComposition atomicComposition)
		{
			Assumes.NotNull<AtomicComposition>(atomicComposition);
			ImportEngine.EngineContext engineContext;
			if (!atomicComposition.TryGetValue<ImportEngine.EngineContext>(this, true, out engineContext))
			{
				ImportEngine.EngineContext parentEngineContext;
				atomicComposition.TryGetValue<ImportEngine.EngineContext>(this, false, out parentEngineContext);
				engineContext = new ImportEngine.EngineContext(this, parentEngineContext);
				atomicComposition.SetValue(this, engineContext);
				atomicComposition.AddCompleteAction(new Action(engineContext.Complete));
			}
			return engineContext;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000136E4 File Offset: 0x000118E4
		private bool InPrerequisiteLoop()
		{
			ImportEngine.PartManager partManager = this._recursionStateStack.First<ImportEngine.PartManager>();
			ImportEngine.PartManager partManager2 = null;
			foreach (ImportEngine.PartManager partManager3 in this._recursionStateStack.Skip(1))
			{
				if (partManager3.State == ImportEngine.ImportState.PreExportImportsSatisfying)
				{
					return true;
				}
				if (partManager3 == partManager)
				{
					partManager2 = partManager3;
					break;
				}
			}
			Assumes.IsTrue(partManager2 == partManager);
			return false;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00013764 File Offset: 0x00011964
		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00013778 File Offset: 0x00011978
		private static CompositionResult<IEnumerable<Export>> TryGetExports(ExportProvider provider, ComposablePart part, ImportDefinition definition, AtomicComposition atomicComposition)
		{
			CompositionResult<IEnumerable<Export>> result;
			try
			{
				result = new CompositionResult<IEnumerable<Export>>(provider.GetExports(definition, atomicComposition).AsArray<Export>());
			}
			catch (ImportCardinalityMismatchException exception)
			{
				CompositionException innerException = new CompositionException(ErrorBuilder.CreateImportCardinalityMismatch(exception, definition));
				result = new CompositionResult<IEnumerable<Export>>(new CompositionError[]
				{
					ErrorBuilder.CreatePartCannotSetImport(part, definition, innerException)
				});
			}
			return result;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x000137D0 File Offset: 0x000119D0
		internal static bool IsRequiredImportForPreview(ImportDefinition import)
		{
			return import.Cardinality == ImportCardinality.ExactlyOne;
		}

		// Token: 0x0400029B RID: 667
		private const int MaximumNumberOfCompositionIterations = 100;

		// Token: 0x0400029C RID: 668
		private volatile bool _isDisposed;

		// Token: 0x0400029D RID: 669
		private ExportProvider _sourceProvider;

		// Token: 0x0400029E RID: 670
		private Stack<ImportEngine.PartManager> _recursionStateStack = new Stack<ImportEngine.PartManager>();

		// Token: 0x0400029F RID: 671
		private ConditionalWeakTable<ComposablePart, ImportEngine.PartManager> _partManagers = new ConditionalWeakTable<ComposablePart, ImportEngine.PartManager>();

		// Token: 0x040002A0 RID: 672
		private ImportEngine.RecompositionManager _recompositionManager = new ImportEngine.RecompositionManager();

		// Token: 0x040002A1 RID: 673
		private readonly CompositionLock _lock;

		// Token: 0x040002A2 RID: 674
		private readonly CompositionOptions _compositionOptions;

		// Token: 0x020000EA RID: 234
		private class EngineContext
		{
			// Token: 0x06000644 RID: 1604 RVA: 0x000137DB File Offset: 0x000119DB
			public EngineContext(ImportEngine importEngine, ImportEngine.EngineContext parentEngineContext)
			{
				this._importEngine = importEngine;
				this._parentEngineContext = parentEngineContext;
			}

			// Token: 0x06000645 RID: 1605 RVA: 0x00013807 File Offset: 0x00011A07
			public void AddPartManager(ImportEngine.PartManager part)
			{
				Assumes.NotNull<ImportEngine.PartManager>(part);
				if (!this._removedPartManagers.Remove(part))
				{
					this._addedPartManagers.Add(part);
				}
			}

			// Token: 0x06000646 RID: 1606 RVA: 0x00013829 File Offset: 0x00011A29
			public void RemovePartManager(ImportEngine.PartManager part)
			{
				Assumes.NotNull<ImportEngine.PartManager>(part);
				if (!this._addedPartManagers.Remove(part))
				{
					this._removedPartManagers.Add(part);
				}
			}

			// Token: 0x06000647 RID: 1607 RVA: 0x0001384B File Offset: 0x00011A4B
			public IEnumerable<ImportEngine.PartManager> GetAddedPartManagers()
			{
				if (this._parentEngineContext != null)
				{
					return this._addedPartManagers.ConcatAllowingNull(this._parentEngineContext.GetAddedPartManagers());
				}
				return this._addedPartManagers;
			}

			// Token: 0x06000648 RID: 1608 RVA: 0x00013872 File Offset: 0x00011A72
			public IEnumerable<ImportEngine.PartManager> GetRemovedPartManagers()
			{
				if (this._parentEngineContext != null)
				{
					return this._removedPartManagers.ConcatAllowingNull(this._parentEngineContext.GetRemovedPartManagers());
				}
				return this._removedPartManagers;
			}

			// Token: 0x06000649 RID: 1609 RVA: 0x0001389C File Offset: 0x00011A9C
			public void Complete()
			{
				foreach (ImportEngine.PartManager partManager in this._addedPartManagers)
				{
					this._importEngine.StartSatisfyingImports(partManager, null);
				}
				foreach (ImportEngine.PartManager partManager2 in this._removedPartManagers)
				{
					this._importEngine.StopSatisfyingImports(partManager2, null);
				}
			}

			// Token: 0x040002A3 RID: 675
			private ImportEngine _importEngine;

			// Token: 0x040002A4 RID: 676
			private List<ImportEngine.PartManager> _addedPartManagers = new List<ImportEngine.PartManager>();

			// Token: 0x040002A5 RID: 677
			private List<ImportEngine.PartManager> _removedPartManagers = new List<ImportEngine.PartManager>();

			// Token: 0x040002A6 RID: 678
			private ImportEngine.EngineContext _parentEngineContext;
		}

		// Token: 0x020000EB RID: 235
		private class PartManager
		{
			// Token: 0x0600064A RID: 1610 RVA: 0x00013940 File Offset: 0x00011B40
			public PartManager(ImportEngine importEngine, ComposablePart part)
			{
				this._importEngine = importEngine;
				this._part = part;
			}

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x0600064B RID: 1611 RVA: 0x00013956 File Offset: 0x00011B56
			public ComposablePart Part
			{
				get
				{
					return this._part;
				}
			}

			// Token: 0x1700017C RID: 380
			// (get) Token: 0x0600064C RID: 1612 RVA: 0x00013960 File Offset: 0x00011B60
			// (set) Token: 0x0600064D RID: 1613 RVA: 0x000139A4 File Offset: 0x00011BA4
			public ImportEngine.ImportState State
			{
				get
				{
					ImportEngine.ImportState state;
					using (this._importEngine._lock.LockStateForRead())
					{
						state = this._state;
					}
					return state;
				}
				set
				{
					using (this._importEngine._lock.LockStateForWrite())
					{
						this._state = value;
					}
				}
			}

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x0600064E RID: 1614 RVA: 0x000139E8 File Offset: 0x00011BE8
			// (set) Token: 0x0600064F RID: 1615 RVA: 0x000139F0 File Offset: 0x00011BF0
			public bool TrackingImports
			{
				[CompilerGenerated]
				get
				{
					return this.<TrackingImports>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<TrackingImports>k__BackingField = value;
				}
			}

			// Token: 0x06000650 RID: 1616 RVA: 0x000139FC File Offset: 0x00011BFC
			public IEnumerable<string> GetImportedContractNames()
			{
				if (this.Part == null)
				{
					return Enumerable.Empty<string>();
				}
				if (this._importedContractNames == null)
				{
					this._importedContractNames = (from import in this.Part.ImportDefinitions
					select import.ContractName ?? ImportDefinition.EmptyContractName).Distinct<string>().ToArray<string>();
				}
				return this._importedContractNames;
			}

			// Token: 0x06000651 RID: 1617 RVA: 0x00013A64 File Offset: 0x00011C64
			public CompositionResult TrySetImport(ImportDefinition import, IEnumerable<Export> exports)
			{
				CompositionResult result;
				try
				{
					this.Part.SetImport(import, exports);
					this.UpdateDisposableDependencies(import, exports);
					result = CompositionResult.SucceededResult;
				}
				catch (CompositionException innerException)
				{
					result = new CompositionResult(new CompositionError[]
					{
						ErrorBuilder.CreatePartCannotSetImport(this.Part, import, innerException)
					});
				}
				catch (ComposablePartException innerException2)
				{
					result = new CompositionResult(new CompositionError[]
					{
						ErrorBuilder.CreatePartCannotSetImport(this.Part, import, innerException2)
					});
				}
				return result;
			}

			// Token: 0x06000652 RID: 1618 RVA: 0x00013AEC File Offset: 0x00011CEC
			public void SetSavedImport(ImportDefinition import, Export[] exports, AtomicComposition atomicComposition)
			{
				if (atomicComposition != null)
				{
					Export[] savedExports = this.GetSavedImport(import);
					atomicComposition.AddRevertAction(delegate
					{
						this.SetSavedImport(import, savedExports, null);
					});
				}
				if (this._importCache == null)
				{
					this._importCache = new Dictionary<ImportDefinition, Export[]>();
				}
				this._importCache[import] = exports;
			}

			// Token: 0x06000653 RID: 1619 RVA: 0x00013B5C File Offset: 0x00011D5C
			public Export[] GetSavedImport(ImportDefinition import)
			{
				Export[] result = null;
				if (this._importCache != null)
				{
					this._importCache.TryGetValue(import, out result);
				}
				return result;
			}

			// Token: 0x06000654 RID: 1620 RVA: 0x00013B83 File Offset: 0x00011D83
			public void ClearSavedImports()
			{
				this._importCache = null;
			}

			// Token: 0x06000655 RID: 1621 RVA: 0x00013B8C File Offset: 0x00011D8C
			public CompositionResult TryOnComposed()
			{
				CompositionResult result;
				try
				{
					this.Part.Activate();
					result = CompositionResult.SucceededResult;
				}
				catch (ComposablePartException innerException)
				{
					result = new CompositionResult(new CompositionError[]
					{
						ErrorBuilder.CreatePartCannotActivate(this.Part, innerException)
					});
				}
				return result;
			}

			// Token: 0x06000656 RID: 1622 RVA: 0x00013BDC File Offset: 0x00011DDC
			public void UpdateDisposableDependencies(ImportDefinition import, IEnumerable<Export> exports)
			{
				List<IDisposable> list = null;
				foreach (IDisposable item in exports.OfType<IDisposable>())
				{
					if (list == null)
					{
						list = new List<IDisposable>();
					}
					list.Add(item);
				}
				List<IDisposable> list2 = null;
				if (this._importedDisposableExports != null && this._importedDisposableExports.TryGetValue(import, out list2))
				{
					list2.ForEach(delegate(IDisposable disposable)
					{
						disposable.Dispose();
					});
					if (list == null)
					{
						this._importedDisposableExports.Remove(import);
						if (!this._importedDisposableExports.FastAny<KeyValuePair<ImportDefinition, List<IDisposable>>>())
						{
							this._importedDisposableExports = null;
						}
						return;
					}
				}
				if (list != null)
				{
					if (this._importedDisposableExports == null)
					{
						this._importedDisposableExports = new Dictionary<ImportDefinition, List<IDisposable>>();
					}
					this._importedDisposableExports[import] = list;
				}
			}

			// Token: 0x06000657 RID: 1623 RVA: 0x00013CBC File Offset: 0x00011EBC
			public void DisposeAllDependencies()
			{
				if (this._importedDisposableExports != null)
				{
					IEnumerable<IDisposable> source = this._importedDisposableExports.Values.SelectMany((List<IDisposable> exports) => exports);
					this._importedDisposableExports = null;
					source.ForEach(delegate(IDisposable disposableExport)
					{
						disposableExport.Dispose();
					});
				}
			}

			// Token: 0x040002A7 RID: 679
			private Dictionary<ImportDefinition, List<IDisposable>> _importedDisposableExports;

			// Token: 0x040002A8 RID: 680
			private Dictionary<ImportDefinition, Export[]> _importCache;

			// Token: 0x040002A9 RID: 681
			private string[] _importedContractNames;

			// Token: 0x040002AA RID: 682
			private ComposablePart _part;

			// Token: 0x040002AB RID: 683
			private ImportEngine.ImportState _state;

			// Token: 0x040002AC RID: 684
			private readonly ImportEngine _importEngine;

			// Token: 0x040002AD RID: 685
			[CompilerGenerated]
			private bool <TrackingImports>k__BackingField;

			// Token: 0x020000EC RID: 236
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x06000658 RID: 1624 RVA: 0x00013D2B File Offset: 0x00011F2B
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x06000659 RID: 1625 RVA: 0x00002BAC File Offset: 0x00000DAC
				public <>c()
				{
				}

				// Token: 0x0600065A RID: 1626 RVA: 0x00013D37 File Offset: 0x00011F37
				internal string <GetImportedContractNames>b__16_0(ImportDefinition import)
				{
					return import.ContractName ?? ImportDefinition.EmptyContractName;
				}

				// Token: 0x0600065B RID: 1627 RVA: 0x00013D48 File Offset: 0x00011F48
				internal void <UpdateDisposableDependencies>b__22_0(IDisposable disposable)
				{
					disposable.Dispose();
				}

				// Token: 0x0600065C RID: 1628 RVA: 0x0000AB72 File Offset: 0x00008D72
				internal IEnumerable<IDisposable> <DisposeAllDependencies>b__23_0(List<IDisposable> exports)
				{
					return exports;
				}

				// Token: 0x0600065D RID: 1629 RVA: 0x00013D48 File Offset: 0x00011F48
				internal void <DisposeAllDependencies>b__23_1(IDisposable disposableExport)
				{
					disposableExport.Dispose();
				}

				// Token: 0x040002AE RID: 686
				public static readonly ImportEngine.PartManager.<>c <>9 = new ImportEngine.PartManager.<>c();

				// Token: 0x040002AF RID: 687
				public static Func<ImportDefinition, string> <>9__16_0;

				// Token: 0x040002B0 RID: 688
				public static Action<IDisposable> <>9__22_0;

				// Token: 0x040002B1 RID: 689
				public static Func<List<IDisposable>, IEnumerable<IDisposable>> <>9__23_0;

				// Token: 0x040002B2 RID: 690
				public static Action<IDisposable> <>9__23_1;
			}

			// Token: 0x020000ED RID: 237
			[CompilerGenerated]
			private sealed class <>c__DisplayClass18_0
			{
				// Token: 0x0600065E RID: 1630 RVA: 0x00002BAC File Offset: 0x00000DAC
				public <>c__DisplayClass18_0()
				{
				}

				// Token: 0x0600065F RID: 1631 RVA: 0x00013D50 File Offset: 0x00011F50
				internal void <SetSavedImport>b__0()
				{
					this.<>4__this.SetSavedImport(this.import, this.savedExports, null);
				}

				// Token: 0x040002B3 RID: 691
				public ImportEngine.PartManager <>4__this;

				// Token: 0x040002B4 RID: 692
				public ImportDefinition import;

				// Token: 0x040002B5 RID: 693
				public Export[] savedExports;
			}
		}

		// Token: 0x020000EE RID: 238
		private class RecompositionManager
		{
			// Token: 0x06000660 RID: 1632 RVA: 0x00013D6A File Offset: 0x00011F6A
			public void AddPartToIndex(ImportEngine.PartManager partManager)
			{
				this._partsToIndex.Add(partManager);
			}

			// Token: 0x06000661 RID: 1633 RVA: 0x00013D78 File Offset: 0x00011F78
			public void AddPartToUnindex(ImportEngine.PartManager partManager)
			{
				this._partsToUnindex.Add(partManager);
			}

			// Token: 0x06000662 RID: 1634 RVA: 0x00013D88 File Offset: 0x00011F88
			public IEnumerable<ImportEngine.PartManager> GetAffectedParts(IEnumerable<string> changedContractNames)
			{
				this.UpdateImportIndex();
				List<ImportEngine.PartManager> list = new List<ImportEngine.PartManager>();
				list.AddRange(this.GetPartsImporting(ImportDefinition.EmptyContractName));
				foreach (string contractName in changedContractNames)
				{
					list.AddRange(this.GetPartsImporting(contractName));
				}
				return list;
			}

			// Token: 0x06000663 RID: 1635 RVA: 0x00013DF4 File Offset: 0x00011FF4
			public static IEnumerable<ImportDefinition> GetAffectedImports(ComposablePart part, IEnumerable<ExportDefinition> changedExports)
			{
				return from import in part.ImportDefinitions
				where ImportEngine.RecompositionManager.IsAffectedImport(import, changedExports)
				select import;
			}

			// Token: 0x06000664 RID: 1636 RVA: 0x00013E28 File Offset: 0x00012028
			private static bool IsAffectedImport(ImportDefinition import, IEnumerable<ExportDefinition> changedExports)
			{
				foreach (ExportDefinition exportDefinition in changedExports)
				{
					if (import.IsConstraintSatisfiedBy(exportDefinition))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000665 RID: 1637 RVA: 0x00013E7C File Offset: 0x0001207C
			public IEnumerable<ImportEngine.PartManager> GetPartsImporting(string contractName)
			{
				WeakReferenceCollection<ImportEngine.PartManager> weakReferenceCollection;
				if (!this._partManagerIndex.TryGetValue(contractName, out weakReferenceCollection))
				{
					return Enumerable.Empty<ImportEngine.PartManager>();
				}
				return weakReferenceCollection.AliveItemsToList();
			}

			// Token: 0x06000666 RID: 1638 RVA: 0x00013EA8 File Offset: 0x000120A8
			private void AddIndexEntries(ImportEngine.PartManager partManager)
			{
				foreach (string key in partManager.GetImportedContractNames())
				{
					WeakReferenceCollection<ImportEngine.PartManager> weakReferenceCollection;
					if (!this._partManagerIndex.TryGetValue(key, out weakReferenceCollection))
					{
						weakReferenceCollection = new WeakReferenceCollection<ImportEngine.PartManager>();
						this._partManagerIndex.Add(key, weakReferenceCollection);
					}
					if (!weakReferenceCollection.Contains(partManager))
					{
						weakReferenceCollection.Add(partManager);
					}
				}
			}

			// Token: 0x06000667 RID: 1639 RVA: 0x00013F24 File Offset: 0x00012124
			private void RemoveIndexEntries(ImportEngine.PartManager partManager)
			{
				foreach (string key in partManager.GetImportedContractNames())
				{
					WeakReferenceCollection<ImportEngine.PartManager> weakReferenceCollection;
					if (this._partManagerIndex.TryGetValue(key, out weakReferenceCollection))
					{
						weakReferenceCollection.Remove(partManager);
						if (weakReferenceCollection.AliveItemsToList().Count == 0)
						{
							this._partManagerIndex.Remove(key);
						}
					}
				}
			}

			// Token: 0x06000668 RID: 1640 RVA: 0x00013F9C File Offset: 0x0001219C
			private void UpdateImportIndex()
			{
				List<ImportEngine.PartManager> list = this._partsToIndex.AliveItemsToList();
				this._partsToIndex.Clear();
				List<ImportEngine.PartManager> list2 = this._partsToUnindex.AliveItemsToList();
				this._partsToUnindex.Clear();
				if (list.Count == 0 && list2.Count == 0)
				{
					return;
				}
				foreach (ImportEngine.PartManager partManager in list)
				{
					int num = list2.IndexOf(partManager);
					if (num >= 0)
					{
						list2[num] = null;
					}
					else
					{
						this.AddIndexEntries(partManager);
					}
				}
				foreach (ImportEngine.PartManager partManager2 in list2)
				{
					if (partManager2 != null)
					{
						this.RemoveIndexEntries(partManager2);
					}
				}
			}

			// Token: 0x06000669 RID: 1641 RVA: 0x00014088 File Offset: 0x00012288
			public RecompositionManager()
			{
			}

			// Token: 0x040002B6 RID: 694
			private WeakReferenceCollection<ImportEngine.PartManager> _partsToIndex = new WeakReferenceCollection<ImportEngine.PartManager>();

			// Token: 0x040002B7 RID: 695
			private WeakReferenceCollection<ImportEngine.PartManager> _partsToUnindex = new WeakReferenceCollection<ImportEngine.PartManager>();

			// Token: 0x040002B8 RID: 696
			private Dictionary<string, WeakReferenceCollection<ImportEngine.PartManager>> _partManagerIndex = new Dictionary<string, WeakReferenceCollection<ImportEngine.PartManager>>();

			// Token: 0x020000EF RID: 239
			[CompilerGenerated]
			private sealed class <>c__DisplayClass6_0
			{
				// Token: 0x0600066A RID: 1642 RVA: 0x00002BAC File Offset: 0x00000DAC
				public <>c__DisplayClass6_0()
				{
				}

				// Token: 0x0600066B RID: 1643 RVA: 0x000140B1 File Offset: 0x000122B1
				internal bool <GetAffectedImports>b__0(ImportDefinition import)
				{
					return ImportEngine.RecompositionManager.IsAffectedImport(import, this.changedExports);
				}

				// Token: 0x040002B9 RID: 697
				public IEnumerable<ExportDefinition> changedExports;
			}
		}

		// Token: 0x020000F0 RID: 240
		private enum ImportState
		{
			// Token: 0x040002BB RID: 699
			NoImportsSatisfied,
			// Token: 0x040002BC RID: 700
			ImportsPreviewing,
			// Token: 0x040002BD RID: 701
			ImportsPreviewed,
			// Token: 0x040002BE RID: 702
			PreExportImportsSatisfying,
			// Token: 0x040002BF RID: 703
			PreExportImportsSatisfied,
			// Token: 0x040002C0 RID: 704
			PostExportImportsSatisfying,
			// Token: 0x040002C1 RID: 705
			PostExportImportsSatisfied,
			// Token: 0x040002C2 RID: 706
			ComposedNotifying,
			// Token: 0x040002C3 RID: 707
			Composed
		}

		// Token: 0x020000F1 RID: 241
		[CompilerGenerated]
		private sealed class <>c__DisplayClass14_0
		{
			// Token: 0x0600066C RID: 1644 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass14_0()
			{
			}

			// Token: 0x0600066D RID: 1645 RVA: 0x000140BF File Offset: 0x000122BF
			internal void <PreviewImports>b__0()
			{
				this.compositionLockHolder.Dispose();
			}

			// Token: 0x0600066E RID: 1646 RVA: 0x000140BF File Offset: 0x000122BF
			internal void <PreviewImports>b__1()
			{
				this.compositionLockHolder.Dispose();
			}

			// Token: 0x040002C4 RID: 708
			public IDisposable compositionLockHolder;
		}

		// Token: 0x020000F2 RID: 242
		[CompilerGenerated]
		private sealed class <>c__DisplayClass20_0
		{
			// Token: 0x0600066F RID: 1647 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass20_0()
			{
			}

			// Token: 0x06000670 RID: 1648 RVA: 0x000140CC File Offset: 0x000122CC
			internal void <TryPreviewImportsStateMachine>b__0()
			{
				this.partManager.State = ImportEngine.ImportState.NoImportsSatisfied;
			}

			// Token: 0x040002C5 RID: 709
			public ImportEngine.PartManager partManager;
		}

		// Token: 0x020000F3 RID: 243
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000671 RID: 1649 RVA: 0x000140DA File Offset: 0x000122DA
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000672 RID: 1650 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c()
			{
			}

			// Token: 0x06000673 RID: 1651 RVA: 0x000096D5 File Offset: 0x000078D5
			internal bool <TrySatisfyImportsStateMachine>b__21_0(ImportDefinition import)
			{
				return import.IsPrerequisite;
			}

			// Token: 0x06000674 RID: 1652 RVA: 0x000096DD File Offset: 0x000078DD
			internal bool <TrySatisfyImportsStateMachine>b__21_1(ImportDefinition import)
			{
				return !import.IsPrerequisite;
			}

			// Token: 0x040002C6 RID: 710
			public static readonly ImportEngine.<>c <>9 = new ImportEngine.<>c();

			// Token: 0x040002C7 RID: 711
			public static Func<ImportDefinition, bool> <>9__21_0;

			// Token: 0x040002C8 RID: 712
			public static Func<ImportDefinition, bool> <>9__21_1;
		}

		// Token: 0x020000F4 RID: 244
		[CompilerGenerated]
		private sealed class <>c__DisplayClass25_0
		{
			// Token: 0x06000675 RID: 1653 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass25_0()
			{
			}

			// Token: 0x06000676 RID: 1654 RVA: 0x000140E8 File Offset: 0x000122E8
			internal void <TryRecomposeImports>b__0()
			{
				this.partManager.TryOnComposed().ThrowOnErrors();
			}

			// Token: 0x040002C9 RID: 713
			public ImportEngine.PartManager partManager;
		}

		// Token: 0x020000F5 RID: 245
		[CompilerGenerated]
		private sealed class <>c__DisplayClass26_0
		{
			// Token: 0x06000677 RID: 1655 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass26_0()
			{
			}

			// Token: 0x06000678 RID: 1656 RVA: 0x00014108 File Offset: 0x00012308
			internal void <TryRecomposeImport>b__0()
			{
				this.partManager.TrySetImport(this.import, this.exports).ThrowOnErrors();
			}

			// Token: 0x040002CA RID: 714
			public ImportEngine.PartManager partManager;

			// Token: 0x040002CB RID: 715
			public ImportDefinition import;

			// Token: 0x040002CC RID: 716
			public Export[] exports;
		}
	}
}
