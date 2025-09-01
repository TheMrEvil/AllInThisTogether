using System;

namespace System.Drawing
{
	/// <summary>Provides methods for creating graphics buffers that can be used for double buffering.</summary>
	// Token: 0x02000050 RID: 80
	public sealed class BufferedGraphicsContext : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.BufferedGraphicsContext" /> class.</summary>
		// Token: 0x060003A9 RID: 937 RVA: 0x00009B71 File Offset: 0x00007D71
		public BufferedGraphicsContext()
		{
			this.max_buffer = Size.Empty;
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x060003AA RID: 938 RVA: 0x00009B84 File Offset: 0x00007D84
		~BufferedGraphicsContext()
		{
		}

		/// <summary>Creates a graphics buffer of the specified size using the pixel format of the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="targetGraphics">The <see cref="T:System.Drawing.Graphics" /> to match the pixel format for the new buffer to.</param>
		/// <param name="targetRectangle">A <see cref="T:System.Drawing.Rectangle" /> indicating the size of the buffer to create.</param>
		/// <returns>A <see cref="T:System.Drawing.BufferedGraphics" /> that can be used to draw to a buffer of the specified dimensions.</returns>
		// Token: 0x060003AB RID: 939 RVA: 0x00009BAC File Offset: 0x00007DAC
		public BufferedGraphics Allocate(Graphics targetGraphics, Rectangle targetRectangle)
		{
			return new BufferedGraphics(targetGraphics, targetRectangle);
		}

		/// <summary>Creates a graphics buffer of the specified size using the pixel format of the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="targetDC">An <see cref="T:System.IntPtr" /> to a device context to match the pixel format of the new buffer to.</param>
		/// <param name="targetRectangle">A <see cref="T:System.Drawing.Rectangle" /> indicating the size of the buffer to create.</param>
		/// <returns>A <see cref="T:System.Drawing.BufferedGraphics" /> that can be used to draw to a buffer of the specified dimensions.</returns>
		// Token: 0x060003AC RID: 940 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("The targetDC parameter has no equivalent in libgdiplus.")]
		public BufferedGraphics Allocate(IntPtr targetDC, Rectangle targetRectangle)
		{
			throw new NotImplementedException();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Drawing.BufferedGraphicsContext" />.</summary>
		// Token: 0x060003AD RID: 941 RVA: 0x00009BB5 File Offset: 0x00007DB5
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		/// <summary>Disposes of the current graphics buffer, if a buffer has been allocated and has not yet been disposed.</summary>
		// Token: 0x060003AE RID: 942 RVA: 0x000049FE File Offset: 0x00002BFE
		public void Invalidate()
		{
		}

		/// <summary>Gets or sets the maximum size of the buffer to use.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> indicating the maximum size of the buffer dimensions.</returns>
		/// <exception cref="T:System.ArgumentException">The height or width of the size is less than or equal to zero.</exception>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060003AF RID: 943 RVA: 0x00009BBD File Offset: 0x00007DBD
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x00009BC5 File Offset: 0x00007DC5
		public Size MaximumBuffer
		{
			get
			{
				return this.max_buffer;
			}
			set
			{
				if (value.Width <= 0 || value.Height <= 0)
				{
					throw new ArgumentException("The height or width of the size is less than or equal to zero.");
				}
				this.max_buffer = value;
			}
		}

		// Token: 0x04000432 RID: 1074
		private Size max_buffer;
	}
}
