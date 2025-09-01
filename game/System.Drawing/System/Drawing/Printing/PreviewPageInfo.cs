using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies print preview information for a single page. This class cannot be inherited.</summary>
	// Token: 0x020000BE RID: 190
	public sealed class PreviewPageInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PreviewPageInfo" /> class.</summary>
		/// <param name="image">The image of the printed page.</param>
		/// <param name="physicalSize">The size of the printed page, in hundredths of an inch.</param>
		// Token: 0x06000AA1 RID: 2721 RVA: 0x00018484 File Offset: 0x00016684
		public PreviewPageInfo(Image image, Size physicalSize)
		{
			this._image = image;
			this._physicalSize = physicalSize;
		}

		/// <summary>Gets the image of the printed page.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> representing the printed page.</returns>
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x000184A5 File Offset: 0x000166A5
		public Image Image
		{
			get
			{
				return this._image;
			}
		}

		/// <summary>Gets the size of the printed page, in hundredths of an inch.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size of the printed page, in hundredths of an inch.</returns>
		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x000184AD File Offset: 0x000166AD
		public Size PhysicalSize
		{
			get
			{
				return this._physicalSize;
			}
		}

		// Token: 0x040006F7 RID: 1783
		private Image _image;

		// Token: 0x040006F8 RID: 1784
		private Size _physicalSize = Size.Empty;
	}
}
