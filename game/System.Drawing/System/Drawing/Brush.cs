using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Defines objects used to fill the interiors of graphical shapes such as rectangles, ellipses, pies, polygons, and paths.</summary>
	// Token: 0x02000018 RID: 24
	public abstract class Brush : MarshalByRefObject, ICloneable, IDisposable
	{
		/// <summary>When overridden in a derived class, creates an exact copy of this <see cref="T:System.Drawing.Brush" />.</summary>
		/// <returns>The new <see cref="T:System.Drawing.Brush" /> that this method creates.</returns>
		// Token: 0x06000047 RID: 71
		public abstract object Clone();

		/// <summary>In a derived class, sets a reference to a GDI+ brush object.</summary>
		/// <param name="brush">A pointer to the GDI+ brush object.</param>
		// Token: 0x06000048 RID: 72 RVA: 0x00003931 File Offset: 0x00001B31
		protected internal void SetNativeBrush(IntPtr brush)
		{
			this.SetNativeBrushInternal(brush);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000393A File Offset: 0x00001B3A
		internal void SetNativeBrushInternal(IntPtr brush)
		{
			this._nativeBrush = brush;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00003943 File Offset: 0x00001B43
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal IntPtr NativeBrush
		{
			get
			{
				return this._nativeBrush;
			}
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Brush" /> object.</summary>
		// Token: 0x0600004B RID: 75 RVA: 0x0000394B File Offset: 0x00001B4B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Drawing.Brush" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x0600004C RID: 76 RVA: 0x0000395C File Offset: 0x00001B5C
		protected virtual void Dispose(bool disposing)
		{
			if (this._nativeBrush != IntPtr.Zero)
			{
				try
				{
					GDIPlus.GdipDeleteBrush(new HandleRef(this, this._nativeBrush));
				}
				catch (Exception ex) when (!ClientUtils.IsSecurityOrCriticalException(ex))
				{
				}
				finally
				{
					this._nativeBrush = IntPtr.Zero;
				}
			}
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x0600004D RID: 77 RVA: 0x000039D4 File Offset: 0x00001BD4
		~Brush()
		{
			this.Dispose(false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Brush" /> class.</summary>
		// Token: 0x0600004E RID: 78 RVA: 0x00003A04 File Offset: 0x00001C04
		protected Brush()
		{
		}

		// Token: 0x04000150 RID: 336
		private IntPtr _nativeBrush;
	}
}
