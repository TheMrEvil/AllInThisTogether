using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Hosting
{
	/// <summary>Represents a node in a tree of scoped catalogs, reflecting an underlying catalog and its child scopes.</summary>
	// Token: 0x020000D2 RID: 210
	[DebuggerTypeProxy(typeof(CompositionScopeDefinitionDebuggerProxy))]
	public class CompositionScopeDefinition : ComposablePartCatalog, INotifyComposablePartCatalogChanged
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionScopeDefinition" /> class.</summary>
		// Token: 0x06000557 RID: 1367 RVA: 0x0001002E File Offset: 0x0000E22E
		protected CompositionScopeDefinition()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionScopeDefinition" /> class with the specified underlying catalog and children.</summary>
		/// <param name="catalog">The underlying catalog for this catalog.</param>
		/// <param name="children">A collection of the child scopes of this catalog.</param>
		// Token: 0x06000558 RID: 1368 RVA: 0x00010041 File Offset: 0x0000E241
		public CompositionScopeDefinition(ComposablePartCatalog catalog, IEnumerable<CompositionScopeDefinition> children)
		{
			Requires.NotNull<ComposablePartCatalog>(catalog, "catalog");
			Requires.NullOrNotNullElements<CompositionScopeDefinition>(children, "children");
			this.InitializeCompositionScopeDefinition(catalog, children, null);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionScopeDefinition" /> class with the specified underlying catalog, children, and public surface.</summary>
		/// <param name="catalog">The underlying catalog for this catalog.</param>
		/// <param name="children">A collection of the child scopes of this catalog.</param>
		/// <param name="publicSurface">The public surface for this catalog.</param>
		// Token: 0x06000559 RID: 1369 RVA: 0x00010073 File Offset: 0x0000E273
		public CompositionScopeDefinition(ComposablePartCatalog catalog, IEnumerable<CompositionScopeDefinition> children, IEnumerable<ExportDefinition> publicSurface)
		{
			Requires.NotNull<ComposablePartCatalog>(catalog, "catalog");
			Requires.NullOrNotNullElements<CompositionScopeDefinition>(children, "children");
			Requires.NullOrNotNullElements<ExportDefinition>(publicSurface, "publicSurface");
			this.InitializeCompositionScopeDefinition(catalog, children, publicSurface);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000100B0 File Offset: 0x0000E2B0
		private void InitializeCompositionScopeDefinition(ComposablePartCatalog catalog, IEnumerable<CompositionScopeDefinition> children, IEnumerable<ExportDefinition> publicSurface)
		{
			this._catalog = catalog;
			if (children != null)
			{
				this._children = children.ToArray<CompositionScopeDefinition>();
			}
			if (publicSurface != null)
			{
				this._publicSurface = publicSurface;
			}
			INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged = this._catalog as INotifyComposablePartCatalogChanged;
			if (notifyComposablePartCatalogChanged != null)
			{
				notifyComposablePartCatalogChanged.Changed += this.OnChangedInternal;
				notifyComposablePartCatalogChanged.Changing += this.OnChangingInternal;
			}
		}

		/// <summary>Called by the <see langword="Dispose()" /> and <see langword="Finalize()" /> methods to release the managed and unmanaged resources used by the current instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionScopeDefinition" /> class.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x0600055B RID: 1371 RVA: 0x00010110 File Offset: 0x0000E310
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && Interlocked.CompareExchange(ref this._isDisposed, 1, 0) == 0)
				{
					INotifyComposablePartCatalogChanged notifyComposablePartCatalogChanged = this._catalog as INotifyComposablePartCatalogChanged;
					if (notifyComposablePartCatalogChanged != null)
					{
						notifyComposablePartCatalogChanged.Changed -= this.OnChangedInternal;
						notifyComposablePartCatalogChanged.Changing -= this.OnChangingInternal;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Gets the child scopes of this catalog.</summary>
		/// <returns>A collection of the child scopes of this catalog.</returns>
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0001017C File Offset: 0x0000E37C
		public virtual IEnumerable<CompositionScopeDefinition> Children
		{
			get
			{
				this.ThrowIfDisposed();
				return this._children;
			}
		}

		/// <summary>Gets a collection of parts visible to the parent scope of this catalog.</summary>
		/// <returns>A collection of parts visible to the parent scope of this catalog.</returns>
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0001018A File Offset: 0x0000E38A
		public virtual IEnumerable<ExportDefinition> PublicSurface
		{
			get
			{
				this.ThrowIfDisposed();
				if (this._publicSurface == null)
				{
					return this.SelectMany((ComposablePartDefinition p) => p.ExportDefinitions);
				}
				return this._publicSurface;
			}
		}

		/// <summary>Returns an enumerator that iterates through the catalog.</summary>
		/// <returns>An enumerator that can be used to iterate through the catalog.</returns>
		// Token: 0x0600055E RID: 1374 RVA: 0x000101C6 File Offset: 0x0000E3C6
		public override IEnumerator<ComposablePartDefinition> GetEnumerator()
		{
			return this._catalog.GetEnumerator();
		}

		/// <summary>Gets a collection of exports that match the conditions specified by the import definition.</summary>
		/// <param name="definition">Conditions that specify which exports to match.</param>
		/// <returns>A collection of exports that match the specified conditions.</returns>
		// Token: 0x0600055F RID: 1375 RVA: 0x000101D3 File Offset: 0x0000E3D3
		public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
		{
			this.ThrowIfDisposed();
			return this._catalog.GetExports(definition);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000101E8 File Offset: 0x0000E3E8
		internal IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExportsFromPublicSurface(ImportDefinition definition)
		{
			Assumes.NotNull<ImportDefinition, string>(definition, "definition");
			List<Tuple<ComposablePartDefinition, ExportDefinition>> list = new List<Tuple<ComposablePartDefinition, ExportDefinition>>();
			foreach (ExportDefinition exportDefinition in this.PublicSurface)
			{
				if (definition.IsConstraintSatisfiedBy(exportDefinition))
				{
					foreach (Tuple<ComposablePartDefinition, ExportDefinition> tuple in this.GetExports(definition))
					{
						if (tuple.Item2 == exportDefinition)
						{
							list.Add(tuple);
							break;
						}
					}
				}
			}
			return list;
		}

		/// <summary>Occurs when the underlying catalog has changed, if that catalog supports notifications.</summary>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000561 RID: 1377 RVA: 0x00010294 File Offset: 0x0000E494
		// (remove) Token: 0x06000562 RID: 1378 RVA: 0x000102CC File Offset: 0x0000E4CC
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

		/// <summary>Occurs when the underlying catalog is changing, if that catalog supports notifications.</summary>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000563 RID: 1379 RVA: 0x00010304 File Offset: 0x0000E504
		// (remove) Token: 0x06000564 RID: 1380 RVA: 0x0001033C File Offset: 0x0000E53C
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

		/// <summary>Raises the <see cref="E:System.ComponentModel.Composition.Hosting.CompositionScopeDefinition.Changed" /> event.</summary>
		/// <param name="e">Contains data for the <see cref="E:System.ComponentModel.Composition.Hosting.CompositionScopeDefinition.Changed" /> event.</param>
		// Token: 0x06000565 RID: 1381 RVA: 0x00010374 File Offset: 0x0000E574
		protected virtual void OnChanged(ComposablePartCatalogChangeEventArgs e)
		{
			EventHandler<ComposablePartCatalogChangeEventArgs> changed = this.Changed;
			if (changed != null)
			{
				changed(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Composition.Hosting.CompositionScopeDefinition.Changing" /> event.</summary>
		/// <param name="e">Contains data for the <see cref="E:System.ComponentModel.Composition.Hosting.CompositionScopeDefinition.Changing" /> event.</param>
		// Token: 0x06000566 RID: 1382 RVA: 0x00010394 File Offset: 0x0000E594
		protected virtual void OnChanging(ComposablePartCatalogChangeEventArgs e)
		{
			EventHandler<ComposablePartCatalogChangeEventArgs> changing = this.Changing;
			if (changing != null)
			{
				changing(this, e);
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000103B3 File Offset: 0x0000E5B3
		private void OnChangedInternal(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			this.OnChanged(e);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x000103BC File Offset: 0x0000E5BC
		private void OnChangingInternal(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			this.OnChanging(e);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000103C5 File Offset: 0x0000E5C5
		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (this._isDisposed == 1)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}

		// Token: 0x04000253 RID: 595
		private ComposablePartCatalog _catalog;

		// Token: 0x04000254 RID: 596
		private IEnumerable<ExportDefinition> _publicSurface;

		// Token: 0x04000255 RID: 597
		private IEnumerable<CompositionScopeDefinition> _children = Enumerable.Empty<CompositionScopeDefinition>();

		// Token: 0x04000256 RID: 598
		private volatile int _isDisposed;

		// Token: 0x04000257 RID: 599
		[CompilerGenerated]
		private EventHandler<ComposablePartCatalogChangeEventArgs> Changed;

		// Token: 0x04000258 RID: 600
		[CompilerGenerated]
		private EventHandler<ComposablePartCatalogChangeEventArgs> Changing;

		// Token: 0x020000D3 RID: 211
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x0600056A RID: 1386 RVA: 0x000103D9 File Offset: 0x0000E5D9
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600056B RID: 1387 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c()
			{
			}

			// Token: 0x0600056C RID: 1388 RVA: 0x000103E5 File Offset: 0x0000E5E5
			internal IEnumerable<ExportDefinition> <get_PublicSurface>b__12_0(ComposablePartDefinition p)
			{
				return p.ExportDefinitions;
			}

			// Token: 0x04000259 RID: 601
			public static readonly CompositionScopeDefinition.<>c <>9 = new CompositionScopeDefinition.<>c();

			// Token: 0x0400025A RID: 602
			public static Func<ComposablePartDefinition, IEnumerable<ExportDefinition>> <>9__12_0;
		}
	}
}
