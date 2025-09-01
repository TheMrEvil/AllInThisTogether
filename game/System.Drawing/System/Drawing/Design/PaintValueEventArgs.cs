using System;
using System.ComponentModel;

namespace System.Drawing.Design
{
	/// <summary>Provides data for the <see cref="M:System.Drawing.Design.UITypeEditor.PaintValue(System.Object,System.Drawing.Graphics,System.Drawing.Rectangle)" /> method.</summary>
	// Token: 0x02000125 RID: 293
	public class PaintValueEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.PaintValueEventArgs" /> class using the specified values.</summary>
		/// <param name="context">The context in which the value appears.</param>
		/// <param name="value">The value to paint.</param>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> object with which drawing is to be done.</param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> in which drawing is to be done.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is <see langword="null" />.</exception>
		// Token: 0x06000D99 RID: 3481 RVA: 0x0001EFDA File Offset: 0x0001D1DA
		public PaintValueEventArgs(ITypeDescriptorContext context, object value, Graphics graphics, Rectangle bounds)
		{
			this.context = context;
			this.valueToPaint = value;
			this.graphics = graphics;
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			this.bounds = bounds;
		}

		/// <summary>Gets the rectangle that indicates the area in which the painting should be done.</summary>
		/// <returns>The rectangle that indicates the area in which the painting should be done.</returns>
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0001F00D File Offset: 0x0001D20D
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> interface to be used to gain additional information about the context this value appears in.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that indicates the context of the event.</returns>
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0001F015 File Offset: 0x0001D215
		public ITypeDescriptorContext Context
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> object with which painting should be done.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> object to use for painting.</returns>
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0001F01D File Offset: 0x0001D21D
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the value to paint.</summary>
		/// <returns>An object indicating what to paint.</returns>
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x0001F025 File Offset: 0x0001D225
		public object Value
		{
			get
			{
				return this.valueToPaint;
			}
		}

		// Token: 0x04000A9B RID: 2715
		private readonly ITypeDescriptorContext context;

		// Token: 0x04000A9C RID: 2716
		private readonly object valueToPaint;

		// Token: 0x04000A9D RID: 2717
		private readonly Graphics graphics;

		// Token: 0x04000A9E RID: 2718
		private readonly Rectangle bounds;
	}
}
