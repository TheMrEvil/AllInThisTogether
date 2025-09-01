using System;

namespace System.Drawing
{
	/// <summary>Provides a graphics buffer for double buffering.</summary>
	// Token: 0x0200004F RID: 79
	public sealed class BufferedGraphics : IDisposable
	{
		// Token: 0x060003A0 RID: 928 RVA: 0x00002050 File Offset: 0x00000250
		private BufferedGraphics()
		{
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00009A60 File Offset: 0x00007C60
		internal BufferedGraphics(Graphics targetGraphics, Rectangle targetRectangle)
		{
			this.size = targetRectangle;
			this.target = targetGraphics;
			this.membmp = new Bitmap(this.size.Width, this.size.Height);
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x060003A2 RID: 930 RVA: 0x00009A98 File Offset: 0x00007C98
		~BufferedGraphics()
		{
			this.Dispose(false);
		}

		/// <summary>Gets a <see cref="T:System.Drawing.Graphics" /> object that outputs to the graphics buffer.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> object that outputs to the graphics buffer.</returns>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00009AC8 File Offset: 0x00007CC8
		public Graphics Graphics
		{
			get
			{
				if (this.source == null)
				{
					this.source = Graphics.FromImage(this.membmp);
				}
				return this.source;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Drawing.BufferedGraphics" /> object.</summary>
		// Token: 0x060003A4 RID: 932 RVA: 0x00009AE9 File Offset: 0x00007CE9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00009AF8 File Offset: 0x00007CF8
		private void Dispose(bool disposing)
		{
			if (!disposing)
			{
				return;
			}
			if (this.membmp != null)
			{
				this.membmp.Dispose();
				this.membmp = null;
			}
			if (this.source != null)
			{
				this.source.Dispose();
				this.source = null;
			}
			this.target = null;
		}

		/// <summary>Writes the contents of the graphics buffer to the default device.</summary>
		// Token: 0x060003A6 RID: 934 RVA: 0x00009B44 File Offset: 0x00007D44
		public void Render()
		{
			this.Render(this.target);
		}

		/// <summary>Writes the contents of the graphics buffer to the specified <see cref="T:System.Drawing.Graphics" /> object.</summary>
		/// <param name="target">A <see cref="T:System.Drawing.Graphics" /> object to which to write the contents of the graphics buffer.</param>
		// Token: 0x060003A7 RID: 935 RVA: 0x00009B52 File Offset: 0x00007D52
		public void Render(Graphics target)
		{
			if (target == null)
			{
				return;
			}
			target.DrawImage(this.membmp, this.size);
		}

		/// <summary>Writes the contents of the graphics buffer to the device context associated with the specified <see cref="T:System.IntPtr" /> handle.</summary>
		/// <param name="targetDC">An <see cref="T:System.IntPtr" /> that points to the device context to which to write the contents of the graphics buffer.</param>
		// Token: 0x060003A8 RID: 936 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("The targetDC parameter has no equivalent in libgdiplus.")]
		public void Render(IntPtr targetDC)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400042E RID: 1070
		private Rectangle size;

		// Token: 0x0400042F RID: 1071
		private Bitmap membmp;

		// Token: 0x04000430 RID: 1072
		private Graphics target;

		// Token: 0x04000431 RID: 1073
		private Graphics source;
	}
}
