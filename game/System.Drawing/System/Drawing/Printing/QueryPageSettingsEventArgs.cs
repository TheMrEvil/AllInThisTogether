using System;

namespace System.Drawing.Printing
{
	/// <summary>Provides data for the <see cref="E:System.Drawing.Printing.PrintDocument.QueryPageSettings" /> event.</summary>
	// Token: 0x020000C7 RID: 199
	public class QueryPageSettingsEventArgs : PrintEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.QueryPageSettingsEventArgs" /> class.</summary>
		/// <param name="pageSettings">The page settings for the page to be printed.</param>
		// Token: 0x06000ABD RID: 2749 RVA: 0x00018734 File Offset: 0x00016934
		public QueryPageSettingsEventArgs(PageSettings pageSettings)
		{
			this._pageSettings = pageSettings;
		}

		/// <summary>Gets or sets the page settings for the page to be printed.</summary>
		/// <returns>The page settings for the page to be printed.</returns>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00018743 File Offset: 0x00016943
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x00018752 File Offset: 0x00016952
		public PageSettings PageSettings
		{
			get
			{
				this.PageSettingsChanged = true;
				return this._pageSettings;
			}
			set
			{
				if (value == null)
				{
					value = new PageSettings();
				}
				this._pageSettings = value;
				this.PageSettingsChanged = true;
			}
		}

		// Token: 0x04000710 RID: 1808
		private PageSettings _pageSettings;

		// Token: 0x04000711 RID: 1809
		internal bool PageSettingsChanged;
	}
}
