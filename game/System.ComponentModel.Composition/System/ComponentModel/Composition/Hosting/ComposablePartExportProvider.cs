using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Hosting
{
	/// <summary>Retrieves exports from a part.</summary>
	// Token: 0x020000C4 RID: 196
	public class ComposablePartExportProvider : ExportProvider, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> class.</summary>
		// Token: 0x06000506 RID: 1286 RVA: 0x0000ED62 File Offset: 0x0000CF62
		public ComposablePartExportProvider() : this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> class, optionally in thread-safe mode.</summary>
		/// <param name="isThreadSafe">
		///   <see langword="true" /> if the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> object must be thread-safe; otherwise, <see langword="false" />.</param>
		// Token: 0x06000507 RID: 1287 RVA: 0x0000ED6B File Offset: 0x0000CF6B
		public ComposablePartExportProvider(bool isThreadSafe) : this(isThreadSafe ? CompositionOptions.IsThreadSafe : CompositionOptions.Default)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> class with the specified composition options.</summary>
		/// <param name="compositionOptions">Options that specify the behavior of this provider.</param>
		// Token: 0x06000508 RID: 1288 RVA: 0x0000ED7C File Offset: 0x0000CF7C
		public ComposablePartExportProvider(CompositionOptions compositionOptions)
		{
			if (compositionOptions > (CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe | CompositionOptions.ExportCompositionService))
			{
				throw new ArgumentOutOfRangeException("compositionOptions");
			}
			this._compositionOptions = compositionOptions;
			this._lock = new CompositionLock(compositionOptions.HasFlag(CompositionOptions.IsThreadSafe));
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> class.</summary>
		// Token: 0x06000509 RID: 1289 RVA: 0x0000EDCC File Offset: 0x0000CFCC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x0600050A RID: 1290 RVA: 0x0000EDDC File Offset: 0x0000CFDC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this._isDisposed)
			{
				bool flag = false;
				ImportEngine importEngine = null;
				try
				{
					using (this._lock.LockStateForWrite())
					{
						if (!this._isDisposed)
						{
							importEngine = this._importEngine;
							this._importEngine = null;
							this._sourceProvider = null;
							this._isDisposed = true;
							flag = true;
						}
					}
				}
				finally
				{
					if (importEngine != null)
					{
						importEngine.Dispose();
					}
					if (flag)
					{
						this._lock.Dispose();
					}
				}
			}
		}

		/// <summary>Gets or sets the export provider that provides access to additional <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.</summary>
		/// <returns>A provider that provides the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> access to <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> objects.  
		///  The default is <see langword="null" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> has been disposed of.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">This property has already been set.  
		///  -or-  
		///  The methods on the <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartExportProvider" /> have already been accessed.</exception>
		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0000EE70 File Offset: 0x0000D070
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x0000EE80 File Offset: 0x0000D080
		public ExportProvider SourceProvider
		{
			get
			{
				this.ThrowIfDisposed();
				return this._sourceProvider;
			}
			set
			{
				this.ThrowIfDisposed();
				Requires.NotNull<ExportProvider>(value, "value");
				using (this._lock.LockStateForWrite())
				{
					this.EnsureCanSet<ExportProvider>(this._sourceProvider);
					this._sourceProvider = value;
				}
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000EEDC File Offset: 0x0000D0DC
		private ImportEngine ImportEngine
		{
			get
			{
				if (this._importEngine == null)
				{
					Assumes.NotNull<ExportProvider>(this._sourceProvider);
					ImportEngine importEngine = new ImportEngine(this._sourceProvider, this._compositionOptions);
					using (this._lock.LockStateForWrite())
					{
						if (this._importEngine == null)
						{
							Thread.MemoryBarrier();
							this._importEngine = importEngine;
							importEngine = null;
						}
					}
					if (importEngine != null)
					{
						importEngine.Dispose();
					}
				}
				return this._importEngine;
			}
		}

		/// <summary>Gets a collection of all exports in this provider that match the conditions of the specified import.</summary>
		/// <param name="definition">The <see cref="T:System.ComponentModel.Composition.Primitives.ImportDefinition" /> that defines the conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.Export" /> to get.</param>
		/// <param name="atomicComposition">The composition transaction to use, or <see langword="null" /> to disable transactional composition.</param>
		/// <returns>A collection of all exports in this provider that match the specified conditions.</returns>
		// Token: 0x0600050E RID: 1294 RVA: 0x0000EF5C File Offset: 0x0000D15C
		protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition)
		{
			this.ThrowIfDisposed();
			this.EnsureRunning();
			List<ComposablePart> list = null;
			using (this._lock.LockStateForRead())
			{
				list = atomicComposition.GetValueAllowNull(this, this._parts);
			}
			if (list.Count == 0)
			{
				return null;
			}
			List<Export> list2 = new List<Export>();
			foreach (ComposablePart composablePart in list)
			{
				foreach (ExportDefinition exportDefinition in composablePart.ExportDefinitions)
				{
					if (definition.IsConstraintSatisfiedBy(exportDefinition))
					{
						list2.Add(this.CreateExport(composablePart, exportDefinition));
					}
				}
			}
			return list2;
		}

		/// <summary>Executes composition on the specified batch.</summary>
		/// <param name="batch">The batch to execute composition on.</param>
		/// <exception cref="T:System.InvalidOperationException">The container is already in the process of composing.</exception>
		// Token: 0x0600050F RID: 1295 RVA: 0x0000F04C File Offset: 0x0000D24C
		public void Compose(CompositionBatch batch)
		{
			this.ThrowIfDisposed();
			this.EnsureRunning();
			Requires.NotNull<CompositionBatch>(batch, "batch");
			if (batch.PartsToAdd.Count == 0 && batch.PartsToRemove.Count == 0)
			{
				return;
			}
			CompositionResult compositionResult = CompositionResult.SucceededResult;
			List<ComposablePart> updatedPartsList = this.GetUpdatedPartsList(ref batch);
			using (AtomicComposition atomicComposition = new AtomicComposition())
			{
				if (this._currentlyComposing)
				{
					throw new InvalidOperationException(Strings.ReentrantCompose);
				}
				this._currentlyComposing = true;
				try
				{
					atomicComposition.SetValue(this, updatedPartsList);
					this.Recompose(batch, atomicComposition);
					foreach (ComposablePart part2 in batch.PartsToAdd)
					{
						try
						{
							this.ImportEngine.PreviewImports(part2, atomicComposition);
						}
						catch (ChangeRejectedException ex)
						{
							compositionResult = compositionResult.MergeResult(new CompositionResult(ex.Errors));
						}
					}
					compositionResult.ThrowOnErrors(atomicComposition);
					using (this._lock.LockStateForWrite())
					{
						this._parts = updatedPartsList;
					}
					atomicComposition.Complete();
				}
				finally
				{
					this._currentlyComposing = false;
				}
			}
			using (IEnumerator<ComposablePart> enumerator = batch.PartsToAdd.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ComposablePart part = enumerator.Current;
					compositionResult = compositionResult.MergeResult(CompositionServices.TryInvoke(delegate
					{
						this.ImportEngine.SatisfyImports(part);
					}));
				}
			}
			compositionResult.ThrowOnErrors();
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000F214 File Offset: 0x0000D414
		private List<ComposablePart> GetUpdatedPartsList(ref CompositionBatch batch)
		{
			Assumes.NotNull<CompositionBatch>(batch);
			List<ComposablePart> list = null;
			using (this._lock.LockStateForRead())
			{
				list = this._parts.ToList<ComposablePart>();
			}
			foreach (ComposablePart item in batch.PartsToAdd)
			{
				list.Add(item);
			}
			List<ComposablePart> list2 = null;
			foreach (ComposablePart item2 in batch.PartsToRemove)
			{
				if (list.Remove(item2))
				{
					if (list2 == null)
					{
						list2 = new List<ComposablePart>();
					}
					list2.Add(item2);
				}
			}
			batch = new CompositionBatch(batch.PartsToAdd, list2);
			return list;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000F304 File Offset: 0x0000D504
		private void Recompose(CompositionBatch batch, AtomicComposition atomicComposition)
		{
			ComposablePartExportProvider.<>c__DisplayClass21_0 CS$<>8__locals1 = new ComposablePartExportProvider.<>c__DisplayClass21_0();
			CS$<>8__locals1.<>4__this = this;
			Assumes.NotNull<CompositionBatch>(batch);
			foreach (ComposablePart part2 in batch.PartsToRemove)
			{
				this.ImportEngine.ReleaseImports(part2, atomicComposition);
			}
			ComposablePartExportProvider.<>c__DisplayClass21_0 CS$<>8__locals2 = CS$<>8__locals1;
			IEnumerable<ExportDefinition> addedExports;
			if (batch.PartsToAdd.Count == 0)
			{
				addedExports = new ExportDefinition[0];
			}
			else
			{
				addedExports = batch.PartsToAdd.SelectMany((ComposablePart part) => part.ExportDefinitions).ToArray<ExportDefinition>();
			}
			CS$<>8__locals2.addedExports = addedExports;
			ComposablePartExportProvider.<>c__DisplayClass21_0 CS$<>8__locals3 = CS$<>8__locals1;
			IEnumerable<ExportDefinition> removedExports;
			if (batch.PartsToRemove.Count == 0)
			{
				removedExports = new ExportDefinition[0];
			}
			else
			{
				removedExports = batch.PartsToRemove.SelectMany((ComposablePart part) => part.ExportDefinitions).ToArray<ExportDefinition>();
			}
			CS$<>8__locals3.removedExports = removedExports;
			this.OnExportsChanging(new ExportsChangeEventArgs(CS$<>8__locals1.addedExports, CS$<>8__locals1.removedExports, atomicComposition));
			atomicComposition.AddCompleteAction(delegate
			{
				CS$<>8__locals1.<>4__this.OnExportsChanged(new ExportsChangeEventArgs(CS$<>8__locals1.addedExports, CS$<>8__locals1.removedExports, null));
			});
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000F428 File Offset: 0x0000D628
		private Export CreateExport(ComposablePart part, ExportDefinition export)
		{
			return new Export(export, () => this.GetExportedValue(part, export));
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000F467 File Offset: 0x0000D667
		private object GetExportedValue(ComposablePart part, ExportDefinition export)
		{
			this.ThrowIfDisposed();
			this.EnsureRunning();
			return CompositionServices.GetExportedValueFromComposedPart(this.ImportEngine, part, export);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000F482 File Offset: 0x0000D682
		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000F49F File Offset: 0x0000D69F
		[DebuggerStepThrough]
		private void EnsureCanRun()
		{
			if (this._sourceProvider == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.ObjectMustBeInitialized, "SourceProvider"));
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000F4C4 File Offset: 0x0000D6C4
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

		// Token: 0x06000517 RID: 1303 RVA: 0x0000F51C File Offset: 0x0000D71C
		[DebuggerStepThrough]
		private void EnsureCanSet<T>(T currentValue) where T : class
		{
			if (this._isRunning || currentValue != null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.ObjectAlreadyInitialized, Array.Empty<object>()));
			}
		}

		// Token: 0x04000211 RID: 529
		private List<ComposablePart> _parts = new List<ComposablePart>();

		// Token: 0x04000212 RID: 530
		private volatile bool _isDisposed;

		// Token: 0x04000213 RID: 531
		private volatile bool _isRunning;

		// Token: 0x04000214 RID: 532
		private CompositionLock _lock;

		// Token: 0x04000215 RID: 533
		private ExportProvider _sourceProvider;

		// Token: 0x04000216 RID: 534
		private ImportEngine _importEngine;

		// Token: 0x04000217 RID: 535
		private volatile bool _currentlyComposing;

		// Token: 0x04000218 RID: 536
		private CompositionOptions _compositionOptions;

		// Token: 0x020000C5 RID: 197
		[CompilerGenerated]
		private sealed class <>c__DisplayClass19_0
		{
			// Token: 0x06000518 RID: 1304 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass19_0()
			{
			}

			// Token: 0x06000519 RID: 1305 RVA: 0x0000F54A File Offset: 0x0000D74A
			internal void <Compose>b__0()
			{
				this.<>4__this.ImportEngine.SatisfyImports(this.part);
			}

			// Token: 0x04000219 RID: 537
			public ComposablePart part;

			// Token: 0x0400021A RID: 538
			public ComposablePartExportProvider <>4__this;
		}

		// Token: 0x020000C6 RID: 198
		[CompilerGenerated]
		private sealed class <>c__DisplayClass21_0
		{
			// Token: 0x0600051A RID: 1306 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass21_0()
			{
			}

			// Token: 0x0600051B RID: 1307 RVA: 0x0000F562 File Offset: 0x0000D762
			internal void <Recompose>b__2()
			{
				this.<>4__this.OnExportsChanged(new ExportsChangeEventArgs(this.addedExports, this.removedExports, null));
			}

			// Token: 0x0400021B RID: 539
			public ComposablePartExportProvider <>4__this;

			// Token: 0x0400021C RID: 540
			public IEnumerable<ExportDefinition> addedExports;

			// Token: 0x0400021D RID: 541
			public IEnumerable<ExportDefinition> removedExports;
		}

		// Token: 0x020000C7 RID: 199
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600051C RID: 1308 RVA: 0x0000F581 File Offset: 0x0000D781
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600051D RID: 1309 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c()
			{
			}

			// Token: 0x0600051E RID: 1310 RVA: 0x0000F58D File Offset: 0x0000D78D
			internal IEnumerable<ExportDefinition> <Recompose>b__21_0(ComposablePart part)
			{
				return part.ExportDefinitions;
			}

			// Token: 0x0600051F RID: 1311 RVA: 0x0000F58D File Offset: 0x0000D78D
			internal IEnumerable<ExportDefinition> <Recompose>b__21_1(ComposablePart part)
			{
				return part.ExportDefinitions;
			}

			// Token: 0x0400021E RID: 542
			public static readonly ComposablePartExportProvider.<>c <>9 = new ComposablePartExportProvider.<>c();

			// Token: 0x0400021F RID: 543
			public static Func<ComposablePart, IEnumerable<ExportDefinition>> <>9__21_0;

			// Token: 0x04000220 RID: 544
			public static Func<ComposablePart, IEnumerable<ExportDefinition>> <>9__21_1;
		}

		// Token: 0x020000C8 RID: 200
		[CompilerGenerated]
		private sealed class <>c__DisplayClass22_0
		{
			// Token: 0x06000520 RID: 1312 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c__DisplayClass22_0()
			{
			}

			// Token: 0x06000521 RID: 1313 RVA: 0x0000F595 File Offset: 0x0000D795
			internal object <CreateExport>b__0()
			{
				return this.<>4__this.GetExportedValue(this.part, this.export);
			}

			// Token: 0x04000221 RID: 545
			public ComposablePartExportProvider <>4__this;

			// Token: 0x04000222 RID: 546
			public ComposablePart part;

			// Token: 0x04000223 RID: 547
			public ExportDefinition export;
		}
	}
}
