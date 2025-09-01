using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Drawing2D
{
	/// <summary>Encapsulates a custom user-defined line cap.</summary>
	// Token: 0x0200013B RID: 315
	public class CustomLineCap : MarshalByRefObject, ICloneable, IDisposable
	{
		// Token: 0x06000E18 RID: 3608 RVA: 0x0001FDFD File Offset: 0x0001DFFD
		internal static CustomLineCap CreateCustomLineCapObject(IntPtr cap)
		{
			return new CustomLineCap(cap);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x00003A04 File Offset: 0x00001C04
		internal CustomLineCap()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> class with the specified outline and fill.</summary>
		/// <param name="fillPath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the fill for the custom cap.</param>
		/// <param name="strokePath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the outline of the custom cap.</param>
		// Token: 0x06000E1A RID: 3610 RVA: 0x0001FE05 File Offset: 0x0001E005
		public CustomLineCap(GraphicsPath fillPath, GraphicsPath strokePath) : this(fillPath, strokePath, LineCap.Flat)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> class from the specified existing <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration with the specified outline and fill.</summary>
		/// <param name="fillPath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the fill for the custom cap.</param>
		/// <param name="strokePath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the outline of the custom cap.</param>
		/// <param name="baseCap">The line cap from which to create the custom cap.</param>
		// Token: 0x06000E1B RID: 3611 RVA: 0x0001FE10 File Offset: 0x0001E010
		public CustomLineCap(GraphicsPath fillPath, GraphicsPath strokePath, LineCap baseCap) : this(fillPath, strokePath, baseCap, 0f)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> class from the specified existing <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration with the specified outline, fill, and inset.</summary>
		/// <param name="fillPath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the fill for the custom cap.</param>
		/// <param name="strokePath">A <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object that defines the outline of the custom cap.</param>
		/// <param name="baseCap">The line cap from which to create the custom cap.</param>
		/// <param name="baseInset">The distance between the cap and the line.</param>
		// Token: 0x06000E1C RID: 3612 RVA: 0x0001FE20 File Offset: 0x0001E020
		public CustomLineCap(GraphicsPath fillPath, GraphicsPath strokePath, LineCap baseCap, float baseInset)
		{
			IntPtr nativeLineCap;
			int num = GDIPlus.GdipCreateCustomLineCap(new HandleRef(fillPath, (fillPath == null) ? IntPtr.Zero : fillPath.nativePath), new HandleRef(strokePath, (strokePath == null) ? IntPtr.Zero : strokePath.nativePath), baseCap, baseInset, out nativeLineCap);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativeLineCap(nativeLineCap);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0001FE7B File Offset: 0x0001E07B
		internal CustomLineCap(IntPtr nativeLineCap)
		{
			this.SetNativeLineCap(nativeLineCap);
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0001FE8A File Offset: 0x0001E08A
		internal void SetNativeLineCap(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			this.nativeCap = new SafeCustomLineCapHandle(handle);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> object.</summary>
		// Token: 0x06000E1F RID: 3615 RVA: 0x0001FEB0 File Offset: 0x0001E0B0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x06000E20 RID: 3616 RVA: 0x0001FEBF File Offset: 0x0001E0BF
		protected virtual void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			if (disposing && this.nativeCap != null)
			{
				this.nativeCap.Dispose();
			}
			this._disposed = true;
		}

		/// <summary>Allows an <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> to attempt to free resources and perform other cleanup operations before the <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> is reclaimed by garbage collection.</summary>
		// Token: 0x06000E21 RID: 3617 RVA: 0x0001FEE8 File Offset: 0x0001E0E8
		~CustomLineCap()
		{
			this.Dispose(false);
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> this method creates, cast as an object.</returns>
		// Token: 0x06000E22 RID: 3618 RVA: 0x0001FF18 File Offset: 0x0001E118
		public object Clone()
		{
			return this.CoreClone();
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0001FF20 File Offset: 0x0001E120
		internal virtual object CoreClone()
		{
			IntPtr cap;
			int num = GDIPlus.GdipCloneCustomLineCap(new HandleRef(this, this.nativeCap), out cap);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return CustomLineCap.CreateCustomLineCapObject(cap);
		}

		/// <summary>Sets the caps used to start and end lines that make up this custom cap.</summary>
		/// <param name="startCap">The <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration used at the beginning of a line within this cap.</param>
		/// <param name="endCap">The <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration used at the end of a line within this cap.</param>
		// Token: 0x06000E24 RID: 3620 RVA: 0x0001FF58 File Offset: 0x0001E158
		public void SetStrokeCaps(LineCap startCap, LineCap endCap)
		{
			int num = GDIPlus.GdipSetCustomLineCapStrokeCaps(new HandleRef(this, this.nativeCap), startCap, endCap);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Gets the caps used to start and end lines that make up this custom cap.</summary>
		/// <param name="startCap">The <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration used at the beginning of a line within this cap.</param>
		/// <param name="endCap">The <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration used at the end of a line within this cap.</param>
		// Token: 0x06000E25 RID: 3621 RVA: 0x0001FF88 File Offset: 0x0001E188
		public void GetStrokeCaps(out LineCap startCap, out LineCap endCap)
		{
			int num = GDIPlus.GdipGetCustomLineCapStrokeCaps(new HandleRef(this, this.nativeCap), out startCap, out endCap);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.Drawing2D.LineJoin" /> enumeration that determines how lines that compose this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> object are joined.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.LineJoin" /> enumeration this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> object uses to join lines.</returns>
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0001FFB8 File Offset: 0x0001E1B8
		// (set) Token: 0x06000E27 RID: 3623 RVA: 0x0001FFEC File Offset: 0x0001E1EC
		public LineJoin StrokeJoin
		{
			get
			{
				LineJoin result;
				int num = GDIPlus.GdipGetCustomLineCapStrokeJoin(new HandleRef(this, this.nativeCap), out result);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return result;
			}
			set
			{
				int num = GDIPlus.GdipSetCustomLineCapStrokeJoin(new HandleRef(this, this.nativeCap), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration on which this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> is based.</summary>
		/// <returns>The <see cref="T:System.Drawing.Drawing2D.LineCap" /> enumeration on which this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> is based.</returns>
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0002001C File Offset: 0x0001E21C
		// (set) Token: 0x06000E29 RID: 3625 RVA: 0x00020050 File Offset: 0x0001E250
		public LineCap BaseCap
		{
			get
			{
				LineCap result;
				int num = GDIPlus.GdipGetCustomLineCapBaseCap(new HandleRef(this, this.nativeCap), out result);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return result;
			}
			set
			{
				int num = GDIPlus.GdipSetCustomLineCapBaseCap(new HandleRef(this, this.nativeCap), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		/// <summary>Gets or sets the distance between the cap and the line.</summary>
		/// <returns>The distance between the beginning of the cap and the end of the line.</returns>
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x00020080 File Offset: 0x0001E280
		// (set) Token: 0x06000E2B RID: 3627 RVA: 0x000200B4 File Offset: 0x0001E2B4
		public float BaseInset
		{
			get
			{
				float result;
				int num = GDIPlus.GdipGetCustomLineCapBaseInset(new HandleRef(this, this.nativeCap), out result);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return result;
			}
			set
			{
				int num = GDIPlus.GdipSetCustomLineCapBaseInset(new HandleRef(this, this.nativeCap), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		/// <summary>Gets or sets the amount by which to scale this <see cref="T:System.Drawing.Drawing2D.CustomLineCap" /> Class object with respect to the width of the <see cref="T:System.Drawing.Pen" /> object.</summary>
		/// <returns>The amount by which to scale the cap.</returns>
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x000200E4 File Offset: 0x0001E2E4
		// (set) Token: 0x06000E2D RID: 3629 RVA: 0x00020118 File Offset: 0x0001E318
		public float WidthScale
		{
			get
			{
				float result;
				int num = GDIPlus.GdipGetCustomLineCapWidthScale(new HandleRef(this, this.nativeCap), out result);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				return result;
			}
			set
			{
				int num = GDIPlus.GdipSetCustomLineCapWidthScale(new HandleRef(this, this.nativeCap), value);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
			}
		}

		// Token: 0x04000ACF RID: 2767
		internal SafeCustomLineCapHandle nativeCap;

		// Token: 0x04000AD0 RID: 2768
		private bool _disposed;
	}
}
