using System;
using System.ComponentModel.Composition.Primitives;
using Microsoft.Internal;
using Unity;

namespace System.ComponentModel.Composition.Hosting
{
	/// <summary>Provides methods to satisfy imports on an existing part instance.</summary>
	// Token: 0x020000D5 RID: 213
	public class CompositionService : ICompositionService, IDisposable
	{
		// Token: 0x06000571 RID: 1393 RVA: 0x00010440 File Offset: 0x0000E640
		internal CompositionService(ComposablePartCatalog composablePartCatalog)
		{
			Assumes.NotNull<ComposablePartCatalog>(composablePartCatalog);
			this._notifyCatalog = (composablePartCatalog as INotifyComposablePartCatalogChanged);
			try
			{
				if (this._notifyCatalog != null)
				{
					this._notifyCatalog.Changing += this.OnCatalogChanging;
				}
				CompositionOptions compositionOptions = CompositionOptions.DisableSilentRejection | CompositionOptions.IsThreadSafe | CompositionOptions.ExportCompositionService;
				CompositionContainer compositionContainer = new CompositionContainer(composablePartCatalog, compositionOptions, Array.Empty<ExportProvider>());
				this._compositionContainer = compositionContainer;
			}
			catch
			{
				if (this._notifyCatalog != null)
				{
					this._notifyCatalog.Changing -= this.OnCatalogChanging;
				}
				throw;
			}
		}

		/// <summary>Composes the specified part, with recomposition and validation disabled.</summary>
		/// <param name="part">The part to compose.</param>
		// Token: 0x06000572 RID: 1394 RVA: 0x000104D0 File Offset: 0x0000E6D0
		public void SatisfyImportsOnce(ComposablePart part)
		{
			Requires.NotNull<ComposablePart>(part, "part");
			Assumes.NotNull<CompositionContainer>(this._compositionContainer);
			this._compositionContainer.SatisfyImportsOnce(part);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer" /> class.</summary>
		// Token: 0x06000573 RID: 1395 RVA: 0x000104F4 File Offset: 0x0000E6F4
		public void Dispose()
		{
			Assumes.NotNull<CompositionContainer>(this._compositionContainer);
			if (this._notifyCatalog != null)
			{
				this._notifyCatalog.Changing -= this.OnCatalogChanging;
			}
			this._compositionContainer.Dispose();
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0001052B File Offset: 0x0000E72B
		private void OnCatalogChanging(object sender, ComposablePartCatalogChangeEventArgs e)
		{
			throw new ChangeRejectedException(Strings.NotSupportedCatalogChanges);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00010537 File Offset: 0x0000E737
		internal CompositionService()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400025C RID: 604
		private CompositionContainer _compositionContainer;

		// Token: 0x0400025D RID: 605
		private INotifyComposablePartCatalogChanged _notifyCatalog;
	}
}
