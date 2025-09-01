using System;

namespace System.Drawing.Design
{
	/// <summary>Provides information about a property displayed in the Properties window, including the associated event handler, pop-up information string, and the icon to display for the property.</summary>
	// Token: 0x02000127 RID: 295
	public class PropertyValueUIItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.PropertyValueUIItem" /> class.</summary>
		/// <param name="uiItemImage">The icon to display. The image must be 8 x 8 pixels.</param>
		/// <param name="handler">The handler to invoke when the image is double-clicked.</param>
		/// <param name="tooltip">The <see cref="P:System.Drawing.Design.PropertyValueUIItem.ToolTip" /> to display for the property that this <see cref="T:System.Drawing.Design.PropertyValueUIItem" /> is associated with.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uiItemImage" /> or <paramref name="handler" /> is <see langword="null" />.</exception>
		// Token: 0x06000DA2 RID: 3490 RVA: 0x0001F02D File Offset: 0x0001D22D
		public PropertyValueUIItem(Image uiItemImage, PropertyValueUIItemInvokeHandler handler, string tooltip)
		{
			this.itemImage = uiItemImage;
			this.handler = handler;
			if (this.itemImage == null)
			{
				throw new ArgumentNullException("uiItemImage");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.tooltip = tooltip;
		}

		/// <summary>Gets the 8 x 8 pixel image that will be drawn in the Properties window.</summary>
		/// <returns>The image to use for the property icon.</returns>
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000DA3 RID: 3491 RVA: 0x0001F06B File Offset: 0x0001D26B
		public virtual Image Image
		{
			get
			{
				return this.itemImage;
			}
		}

		/// <summary>Gets the handler that is raised when a user double-clicks this item.</summary>
		/// <returns>A <see cref="T:System.Drawing.Design.PropertyValueUIItemInvokeHandler" /> indicating the event handler for this user interface (UI) item.</returns>
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0001F073 File Offset: 0x0001D273
		public virtual PropertyValueUIItemInvokeHandler InvokeHandler
		{
			get
			{
				return this.handler;
			}
		}

		/// <summary>Gets or sets the information string to display for this item.</summary>
		/// <returns>A string containing the information string to display for this item.</returns>
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x0001F07B File Offset: 0x0001D27B
		public virtual string ToolTip
		{
			get
			{
				return this.tooltip;
			}
		}

		/// <summary>Resets the user interface (UI) item.</summary>
		// Token: 0x06000DA6 RID: 3494 RVA: 0x000049FE File Offset: 0x00002BFE
		public virtual void Reset()
		{
		}

		// Token: 0x04000A9F RID: 2719
		private Image itemImage;

		// Token: 0x04000AA0 RID: 2720
		private PropertyValueUIItemInvokeHandler handler;

		// Token: 0x04000AA1 RID: 2721
		private string tooltip;
	}
}
