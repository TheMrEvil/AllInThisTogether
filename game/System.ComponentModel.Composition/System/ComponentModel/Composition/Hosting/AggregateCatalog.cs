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
	/// <summary>A catalog that combines the elements of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> objects.</summary>
	// Token: 0x0200009D RID: 157
	public class AggregateCatalog : ComposablePartCatalog, INotifyComposablePartCatalogChanged
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> class.</summary>
		// Token: 0x06000417 RID: 1047 RVA: 0x0000B942 File Offset: 0x00009B42
		public AggregateCatalog() : this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> class with the specified catalogs.</summary>
		/// <param name="catalogs">A array of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="catalogs" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="catalogs" /> contains an element that is <see langword="null" />.</exception>
		// Token: 0x06000418 RID: 1048 RVA: 0x0000B94B File Offset: 0x00009B4B
		public AggregateCatalog(params ComposablePartCatalog[] catalogs) : this(catalogs)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> class with the specified catalogs.</summary>
		/// <param name="catalogs">A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> objects to add to the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> or <see langword="null" /> to create an empty <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="catalogs" /> contains an element that is <see langword="null" />.</exception>
		// Token: 0x06000419 RID: 1049 RVA: 0x0000B954 File Offset: 0x00009B54
		public AggregateCatalog(IEnumerable<ComposablePartCatalog> catalogs)
		{
			Requires.NullOrNotNullElements<ComposablePartCatalog>(catalogs, "catalogs");
			this._catalogs = new ComposablePartCatalogCollection(catalogs, new Action<ComposablePartCatalogChangeEventArgs>(this.OnChanged), new Action<ComposablePartCatalogChangeEventArgs>(this.OnChanging));
		}

		/// <summary>Occurs when the contents of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> object have changed.</summary>
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600041A RID: 1050 RVA: 0x0000B98D File Offset: 0x00009B8D
		// (remove) Token: 0x0600041B RID: 1051 RVA: 0x0000B99B File Offset: 0x00009B9B
		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changed
		{
			add
			{
				this._catalogs.Changed += value;
			}
			remove
			{
				this._catalogs.Changed -= value;
			}
		}

		/// <summary>Occurs when the contents of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> object are changing.</summary>
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600041C RID: 1052 RVA: 0x0000B9A9 File Offset: 0x00009BA9
		// (remove) Token: 0x0600041D RID: 1053 RVA: 0x0000B9B7 File Offset: 0x00009BB7
		public event EventHandler<ComposablePartCatalogChangeEventArgs> Changing
		{
			add
			{
				this._catalogs.Changing += value;
			}
			remove
			{
				this._catalogs.Changing -= value;
			}
		}

		/// <summary>Gets the export definitions that match the constraint expressed by the specified definition.</summary>
		/// <param name="definition">The conditions of the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects to be returned.</param>
		/// <returns>A collection of <see cref="T:System.Tuple`2" /> containing the <see cref="T:System.ComponentModel.Composition.Primitives.ExportDefinition" /> objects and their associated <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartDefinition" /> objects for objects that match the constraint specified by <paramref name="definition" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> object has been disposed of.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="definition" /> is <see langword="null" />.</exception>
		// Token: 0x0600041E RID: 1054 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
		{
			this.ThrowIfDisposed();
			Requires.NotNull<ImportDefinition>(definition, "definition");
			List<Tuple<ComposablePartDefinition, ExportDefinition>> list = new List<Tuple<ComposablePartDefinition, ExportDefinition>>();
			foreach (ComposablePartCatalog composablePartCatalog in this._catalogs)
			{
				foreach (Tuple<ComposablePartDefinition, ExportDefinition> item in composablePartCatalog.GetExports(definition))
				{
					list.Add(item);
				}
			}
			return list;
		}

		/// <summary>Gets the underlying catalogs of the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> object.</summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.Composition.Primitives.ComposablePartCatalog" /> objects that underlie the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> object has been disposed of.</exception>
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000BA64 File Offset: 0x00009C64
		public ICollection<ComposablePartCatalog> Catalogs
		{
			get
			{
				this.ThrowIfDisposed();
				return this._catalogs;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Composition.Hosting.AggregateCatalog" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000420 RID: 1056 RVA: 0x0000BA74 File Offset: 0x00009C74
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && Interlocked.CompareExchange(ref this._isDisposed, 1, 0) == 0)
				{
					this._catalogs.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Returns an enumerator that iterates through the catalog.</summary>
		/// <returns>An enumerator that can be used to iterate through the catalog.</returns>
		// Token: 0x06000421 RID: 1057 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		public override IEnumerator<ComposablePartDefinition> GetEnumerator()
		{
			return this._catalogs.SelectMany((ComposablePartCatalog catalog) => catalog).GetEnumerator();
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Composition.Hosting.AggregateCatalog.Changed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartCatalogChangeEventArgs" /> object that contains the event data.</param>
		// Token: 0x06000422 RID: 1058 RVA: 0x0000BAE9 File Offset: 0x00009CE9
		protected virtual void OnChanged(ComposablePartCatalogChangeEventArgs e)
		{
			this._catalogs.OnChanged(this, e);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.Composition.Hosting.AggregateCatalog.Changing" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.Composition.Hosting.ComposablePartCatalogChangeEventArgs" /> object that contains the event data.</param>
		// Token: 0x06000423 RID: 1059 RVA: 0x0000BAF8 File Offset: 0x00009CF8
		protected virtual void OnChanging(ComposablePartCatalogChangeEventArgs e)
		{
			this._catalogs.OnChanging(this, e);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000BB07 File Offset: 0x00009D07
		[DebuggerStepThrough]
		private void ThrowIfDisposed()
		{
			if (this._isDisposed == 1)
			{
				throw ExceptionBuilder.CreateObjectDisposed(this);
			}
		}

		// Token: 0x0400019F RID: 415
		private ComposablePartCatalogCollection _catalogs;

		// Token: 0x040001A0 RID: 416
		private volatile int _isDisposed;

		// Token: 0x0200009E RID: 158
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000425 RID: 1061 RVA: 0x0000BB1B File Offset: 0x00009D1B
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06000426 RID: 1062 RVA: 0x00002BAC File Offset: 0x00000DAC
			public <>c()
			{
			}

			// Token: 0x06000427 RID: 1063 RVA: 0x0000AB72 File Offset: 0x00008D72
			internal IEnumerable<ComposablePartDefinition> <GetEnumerator>b__15_0(ComposablePartCatalog catalog)
			{
				return catalog;
			}

			// Token: 0x040001A1 RID: 417
			public static readonly AggregateCatalog.<>c <>9 = new AggregateCatalog.<>c();

			// Token: 0x040001A2 RID: 418
			public static Func<ComposablePartCatalog, IEnumerable<ComposablePartDefinition>> <>9__15_0;
		}
	}
}
