using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Unity;

namespace System.Drawing
{
	/// <summary>Encapsulates a GDI+ drawing surface. This class cannot be inherited.</summary>
	// Token: 0x02000071 RID: 113
	public sealed class Graphics : MarshalByRefObject, IDisposable, IDeviceContext
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x0000C54C File Offset: 0x0000A74C
		internal Graphics(IntPtr nativeGraphics)
		{
			this.nativeObject = IntPtr.Zero;
			base..ctor();
			this.nativeObject = nativeGraphics;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000C568 File Offset: 0x0000A768
		internal Graphics(IntPtr nativeGraphics, Image image) : this(nativeGraphics)
		{
			Metafile metafile = image as Metafile;
			if (metafile != null)
			{
				this._metafileHolder = metafile.AddMetafileHolder();
			}
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x060004A7 RID: 1191 RVA: 0x0000C594 File Offset: 0x0000A794
		~Graphics()
		{
			this.Dispose();
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		internal static float systemDpiX
		{
			get
			{
				if (Graphics.defDpiX == 0f)
				{
					Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));
					Graphics.defDpiX = graphics.DpiX;
					Graphics.defDpiY = graphics.DpiY;
				}
				return Graphics.defDpiX;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000C5F4 File Offset: 0x0000A7F4
		internal static float systemDpiY
		{
			get
			{
				if (Graphics.defDpiY == 0f)
				{
					Graphics graphics = Graphics.FromImage(new Bitmap(1, 1));
					Graphics.defDpiX = graphics.DpiX;
					Graphics.defDpiY = graphics.DpiY;
				}
				return Graphics.defDpiY;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000C628 File Offset: 0x0000A828
		internal IntPtr NativeGraphics
		{
			get
			{
				return this.nativeObject;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000C628 File Offset: 0x0000A828
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x0000C630 File Offset: 0x0000A830
		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeObject;
			}
			set
			{
				this.nativeObject = value;
			}
		}

		/// <summary>Adds a comment to the current <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="data">Array of bytes that contains the comment.</param>
		// Token: 0x060004AD RID: 1197 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles, both WMF and EMF formats, aren't supported.")]
		public void AddMetafileComment(byte[] data)
		{
			throw new NotImplementedException();
		}

		/// <summary>Saves a graphics container with the current state of this <see cref="T:System.Drawing.Graphics" /> and opens and uses a new graphics container.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.Drawing2D.GraphicsContainer" /> that represents the state of this <see cref="T:System.Drawing.Graphics" /> at the time of the method call.</returns>
		// Token: 0x060004AE RID: 1198 RVA: 0x0000C63C File Offset: 0x0000A83C
		public GraphicsContainer BeginContainer()
		{
			uint state;
			GDIPlus.CheckStatus(GDIPlus.GdipBeginContainer2(this.nativeObject, out state));
			return new GraphicsContainer(state);
		}

		/// <summary>Saves a graphics container with the current state of this <see cref="T:System.Drawing.Graphics" /> and opens and uses a new graphics container with the specified scale transformation.</summary>
		/// <param name="dstrect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that, together with the <paramref name="srcrect" /> parameter, specifies a scale transformation for the container.</param>
		/// <param name="srcrect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that, together with the <paramref name="dstrect" /> parameter, specifies a scale transformation for the container.</param>
		/// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure for the container.</param>
		/// <returns>This method returns a <see cref="T:System.Drawing.Drawing2D.GraphicsContainer" /> that represents the state of this <see cref="T:System.Drawing.Graphics" /> at the time of the method call.</returns>
		// Token: 0x060004AF RID: 1199 RVA: 0x0000C664 File Offset: 0x0000A864
		[MonoTODO("The rectangles and unit parameters aren't supported in libgdiplus")]
		public GraphicsContainer BeginContainer(Rectangle dstrect, Rectangle srcrect, GraphicsUnit unit)
		{
			uint state;
			GDIPlus.CheckStatus(GDIPlus.GdipBeginContainerI(this.nativeObject, ref dstrect, ref srcrect, unit, out state));
			return new GraphicsContainer(state);
		}

		/// <summary>Saves a graphics container with the current state of this <see cref="T:System.Drawing.Graphics" /> and opens and uses a new graphics container with the specified scale transformation.</summary>
		/// <param name="dstrect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that, together with the <paramref name="srcrect" /> parameter, specifies a scale transformation for the new graphics container.</param>
		/// <param name="srcrect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that, together with the <paramref name="dstrect" /> parameter, specifies a scale transformation for the new graphics container.</param>
		/// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure for the container.</param>
		/// <returns>This method returns a <see cref="T:System.Drawing.Drawing2D.GraphicsContainer" /> that represents the state of this <see cref="T:System.Drawing.Graphics" /> at the time of the method call.</returns>
		// Token: 0x060004B0 RID: 1200 RVA: 0x0000C690 File Offset: 0x0000A890
		[MonoTODO("The rectangles and unit parameters aren't supported in libgdiplus")]
		public GraphicsContainer BeginContainer(RectangleF dstrect, RectangleF srcrect, GraphicsUnit unit)
		{
			uint state;
			GDIPlus.CheckStatus(GDIPlus.GdipBeginContainer(this.nativeObject, ref dstrect, ref srcrect, unit, out state));
			return new GraphicsContainer(state);
		}

		/// <summary>Clears the entire drawing surface and fills it with the specified background color.</summary>
		/// <param name="color">
		///   <see cref="T:System.Drawing.Color" /> structure that represents the background color of the drawing surface.</param>
		// Token: 0x060004B1 RID: 1201 RVA: 0x0000C6BA File Offset: 0x0000A8BA
		public void Clear(Color color)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipGraphicsClear(this.nativeObject, color.ToArgb()));
		}

		/// <summary>Performs a bit-block transfer of color data, corresponding to a rectangle of pixels, from the screen to the drawing surface of the <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="upperLeftSource">The point at the upper-left corner of the source rectangle.</param>
		/// <param name="upperLeftDestination">The point at the upper-left corner of the destination rectangle.</param>
		/// <param name="blockRegionSize">The size of the area to be transferred.</param>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
		// Token: 0x060004B2 RID: 1202 RVA: 0x0000C6D3 File Offset: 0x0000A8D3
		[MonoLimitation("Works on Win32 and on X11 (but not on Cocoa and Quartz)")]
		public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize)
		{
			this.CopyFromScreen(upperLeftSource.X, upperLeftSource.Y, upperLeftDestination.X, upperLeftDestination.Y, blockRegionSize, CopyPixelOperation.SourceCopy);
		}

		/// <summary>Performs a bit-block transfer of color data, corresponding to a rectangle of pixels, from the screen to the drawing surface of the <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="upperLeftSource">The point at the upper-left corner of the source rectangle.</param>
		/// <param name="upperLeftDestination">The point at the upper-left corner of the destination rectangle.</param>
		/// <param name="blockRegionSize">The size of the area to be transferred.</param>
		/// <param name="copyPixelOperation">One of the <see cref="T:System.Drawing.CopyPixelOperation" /> values.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="copyPixelOperation" /> is not a member of <see cref="T:System.Drawing.CopyPixelOperation" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
		// Token: 0x060004B3 RID: 1203 RVA: 0x0000C6FD File Offset: 0x0000A8FD
		[MonoLimitation("Works on Win32 and (for CopyPixelOperation.SourceCopy only) on X11 but not on Cocoa and Quartz")]
		public void CopyFromScreen(Point upperLeftSource, Point upperLeftDestination, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
		{
			this.CopyFromScreen(upperLeftSource.X, upperLeftSource.Y, upperLeftDestination.X, upperLeftDestination.Y, blockRegionSize, copyPixelOperation);
		}

		/// <summary>Performs a bit-block transfer of the color data, corresponding to a rectangle of pixels, from the screen to the drawing surface of the <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="sourceX">The x-coordinate of the point at the upper-left corner of the source rectangle.</param>
		/// <param name="sourceY">The y-coordinate of the point at the upper-left corner of the source rectangle.</param>
		/// <param name="destinationX">The x-coordinate of the point at the upper-left corner of the destination rectangle.</param>
		/// <param name="destinationY">The y-coordinate of the point at the upper-left corner of the destination rectangle.</param>
		/// <param name="blockRegionSize">The size of the area to be transferred.</param>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
		// Token: 0x060004B4 RID: 1204 RVA: 0x0000C724 File Offset: 0x0000A924
		[MonoLimitation("Works on Win32 and on X11 (but not on Cocoa and Quartz)")]
		public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize)
		{
			this.CopyFromScreen(sourceX, sourceY, destinationX, destinationY, blockRegionSize, CopyPixelOperation.SourceCopy);
		}

		/// <summary>Performs a bit-block transfer of the color data, corresponding to a rectangle of pixels, from the screen to the drawing surface of the <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="sourceX">The x-coordinate of the point at the upper-left corner of the source rectangle.</param>
		/// <param name="sourceY">The y-coordinate of the point at the upper-left corner of the source rectangle</param>
		/// <param name="destinationX">The x-coordinate of the point at the upper-left corner of the destination rectangle.</param>
		/// <param name="destinationY">The y-coordinate of the point at the upper-left corner of the destination rectangle.</param>
		/// <param name="blockRegionSize">The size of the area to be transferred.</param>
		/// <param name="copyPixelOperation">One of the <see cref="T:System.Drawing.CopyPixelOperation" /> values.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="copyPixelOperation" /> is not a member of <see cref="T:System.Drawing.CopyPixelOperation" />.</exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operation failed.</exception>
		// Token: 0x060004B5 RID: 1205 RVA: 0x0000C738 File Offset: 0x0000A938
		[MonoLimitation("Works on Win32 and (for CopyPixelOperation.SourceCopy only) on X11 but not on Cocoa and Quartz")]
		public void CopyFromScreen(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
		{
			if (!Enum.IsDefined(typeof(CopyPixelOperation), copyPixelOperation))
			{
				throw new InvalidEnumArgumentException(Locale.GetText("Enum argument value '{0}' is not valid for CopyPixelOperation", new object[]
				{
					copyPixelOperation
				}));
			}
			if (GDIPlus.UseX11Drawable)
			{
				this.CopyFromScreenX11(sourceX, sourceY, destinationX, destinationY, blockRegionSize, copyPixelOperation);
				return;
			}
			if (GDIPlus.UseCarbonDrawable)
			{
				this.CopyFromScreenMac(sourceX, sourceY, destinationX, destinationY, blockRegionSize, copyPixelOperation);
				return;
			}
			if (GDIPlus.UseCocoaDrawable)
			{
				this.CopyFromScreenMac(sourceX, sourceY, destinationX, destinationY, blockRegionSize, copyPixelOperation);
				return;
			}
			this.CopyFromScreenWin32(sourceX, sourceY, destinationX, destinationY, blockRegionSize, copyPixelOperation);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0000C7D4 File Offset: 0x0000A9D4
		private void CopyFromScreenWin32(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
		{
			IntPtr dc = GDIPlus.GetDC(GDIPlus.GetDesktopWindow());
			IntPtr hdc = this.GetHdc();
			GDIPlus.BitBlt(hdc, destinationX, destinationY, blockRegionSize.Width, blockRegionSize.Height, dc, sourceX, sourceY, (int)copyPixelOperation);
			GDIPlus.ReleaseDC(IntPtr.Zero, dc);
			this.ReleaseHdc(hdc);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x00009B6A File Offset: 0x00007D6A
		private void CopyFromScreenMac(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000C824 File Offset: 0x0000AA24
		private void CopyFromScreenX11(int sourceX, int sourceY, int destinationX, int destinationY, Size blockRegionSize, CopyPixelOperation copyPixelOperation)
		{
			int pane = -1;
			int num = 0;
			if (copyPixelOperation != CopyPixelOperation.SourceCopy)
			{
				throw new NotImplementedException("Operation not implemented under X11");
			}
			if (GDIPlus.Display == IntPtr.Zero)
			{
				GDIPlus.Display = GDIPlus.XOpenDisplay(IntPtr.Zero);
			}
			IntPtr drawable = GDIPlus.XRootWindow(GDIPlus.Display, 0);
			IntPtr visual = GDIPlus.XDefaultVisual(GDIPlus.Display, 0);
			XVisualInfo xvisualInfo = default(XVisualInfo);
			xvisualInfo.visualid = GDIPlus.XVisualIDFromVisual(visual);
			IntPtr intPtr = GDIPlus.XGetVisualInfo(GDIPlus.Display, 1, ref xvisualInfo, ref num);
			xvisualInfo = (XVisualInfo)Marshal.PtrToStructure(intPtr, typeof(XVisualInfo));
			IntPtr intPtr2 = GDIPlus.XGetImage(GDIPlus.Display, drawable, sourceX, sourceY, blockRegionSize.Width, blockRegionSize.Height, pane, 2);
			if (intPtr2 == IntPtr.Zero)
			{
				throw new InvalidOperationException(string.Format("XGetImage returned NULL when asked to for a {0}x{1} region block", blockRegionSize.Width, blockRegionSize.Height));
			}
			Bitmap bitmap = new Bitmap(blockRegionSize.Width, blockRegionSize.Height);
			int num2 = (int)xvisualInfo.red_mask;
			int num3 = (int)xvisualInfo.blue_mask;
			int num4 = (int)xvisualInfo.green_mask;
			for (int i = 0; i < blockRegionSize.Height; i++)
			{
				for (int j = 0; j < blockRegionSize.Width; j++)
				{
					int num5 = GDIPlus.XGetPixel(intPtr2, j, i);
					uint depth = xvisualInfo.depth;
					int red;
					int green;
					int blue;
					if (depth != 16U)
					{
						if (depth != 24U && depth != 32U)
						{
							throw new NotImplementedException(Locale.GetText("{0}bbp depth not supported.", new object[]
							{
								xvisualInfo.depth
							}));
						}
						red = ((num5 & num2) >> 16 & 255);
						green = ((num5 & num4) >> 8 & 255);
						blue = (num5 & num3 & 255);
					}
					else
					{
						red = ((num5 & num2) >> 8 & 255);
						green = ((num5 & num4) >> 3 & 255);
						blue = ((num5 & num3) << 3 & 255);
					}
					bitmap.SetPixel(j, i, Color.FromArgb(255, red, green, blue));
				}
			}
			this.DrawImage(bitmap, destinationX, destinationY);
			bitmap.Dispose();
			GDIPlus.XDestroyImage(intPtr2);
			GDIPlus.XFree(intPtr);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Graphics" />.</summary>
		// Token: 0x060004B9 RID: 1209 RVA: 0x0000CA70 File Offset: 0x0000AC70
		public void Dispose()
		{
			if (!this.disposed)
			{
				if (this.deviceContextHdc != IntPtr.Zero)
				{
					this.ReleaseHdc();
				}
				if (GDIPlus.UseCarbonDrawable || GDIPlus.UseCocoaDrawable)
				{
					this.Flush();
					if (this.maccontext != null)
					{
						this.maccontext.Release();
					}
				}
				Status status = GDIPlus.GdipDeleteGraphics(this.nativeObject);
				this.nativeObject = IntPtr.Zero;
				GDIPlus.CheckStatus(status);
				if (this._metafileHolder != null)
				{
					Metafile.MetafileHolder metafileHolder = this._metafileHolder;
					this._metafileHolder = null;
					metafileHolder.GraphicsDisposed();
				}
				this.disposed = true;
			}
			GC.SuppressFinalize(this);
		}

		/// <summary>Draws an arc representing a portion of an ellipse specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the arc.</param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that defines the boundaries of the ellipse.</param>
		/// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc.</param>
		/// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to ending point of the arc.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x060004BA RID: 1210 RVA: 0x0000CB06 File Offset: 0x0000AD06
		public void DrawArc(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
		{
			this.DrawArc(pen, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height, startAngle, sweepAngle);
		}

		/// <summary>Draws an arc representing a portion of an ellipse specified by a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the arc.</param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that defines the boundaries of the ellipse.</param>
		/// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc.</param>
		/// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to ending point of the arc.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" /></exception>
		// Token: 0x060004BB RID: 1211 RVA: 0x0000CB32 File Offset: 0x0000AD32
		public void DrawArc(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
		{
			this.DrawArc(pen, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
		}

		/// <summary>Draws an arc representing a portion of an ellipse specified by a pair of coordinates, a width, and a height.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the arc.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle that defines the ellipse.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle that defines the ellipse.</param>
		/// <param name="width">Width of the rectangle that defines the ellipse.</param>
		/// <param name="height">Height of the rectangle that defines the ellipse.</param>
		/// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc.</param>
		/// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to ending point of the arc.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x060004BC RID: 1212 RVA: 0x0000CB5A File Offset: 0x0000AD5A
		public void DrawArc(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawArc(this.nativeObject, pen.NativePen, x, y, width, height, startAngle, sweepAngle));
		}

		/// <summary>Draws an arc representing a portion of an ellipse specified by a pair of coordinates, a width, and a height.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the arc.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle that defines the ellipse.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle that defines the ellipse.</param>
		/// <param name="width">Width of the rectangle that defines the ellipse.</param>
		/// <param name="height">Height of the rectangle that defines the ellipse.</param>
		/// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the starting point of the arc.</param>
		/// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to ending point of the arc.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x060004BD RID: 1213 RVA: 0x0000CB8A File Offset: 0x0000AD8A
		public void DrawArc(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawArcI(this.nativeObject, pen.NativePen, x, y, width, height, (float)startAngle, (float)sweepAngle));
		}

		/// <summary>Draws a Bézier spline defined by four <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
		/// <param name="pt1">
		///   <see cref="T:System.Drawing.PointF" /> structure that represents the starting point of the curve.</param>
		/// <param name="pt2">
		///   <see cref="T:System.Drawing.PointF" /> structure that represents the first control point for the curve.</param>
		/// <param name="pt3">
		///   <see cref="T:System.Drawing.PointF" /> structure that represents the second control point for the curve.</param>
		/// <param name="pt4">
		///   <see cref="T:System.Drawing.PointF" /> structure that represents the ending point of the curve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x060004BE RID: 1214 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		public void DrawBezier(Pen pen, PointF pt1, PointF pt2, PointF pt3, PointF pt4)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawBezier(this.nativeObject, pen.NativePen, pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y));
		}

		/// <summary>Draws a Bézier spline defined by four <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> structure that determines the color, width, and style of the curve.</param>
		/// <param name="pt1">
		///   <see cref="T:System.Drawing.Point" /> structure that represents the starting point of the curve.</param>
		/// <param name="pt2">
		///   <see cref="T:System.Drawing.Point" /> structure that represents the first control point for the curve.</param>
		/// <param name="pt3">
		///   <see cref="T:System.Drawing.Point" /> structure that represents the second control point for the curve.</param>
		/// <param name="pt4">
		///   <see cref="T:System.Drawing.Point" /> structure that represents the ending point of the curve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x060004BF RID: 1215 RVA: 0x0000CC28 File Offset: 0x0000AE28
		public void DrawBezier(Pen pen, Point pt1, Point pt2, Point pt3, Point pt4)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawBezierI(this.nativeObject, pen.NativePen, pt1.X, pt1.Y, pt2.X, pt2.Y, pt3.X, pt3.Y, pt4.X, pt4.Y));
		}

		/// <summary>Draws a Bézier spline defined by four ordered pairs of coordinates that represent points.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
		/// <param name="x1">The x-coordinate of the starting point of the curve.</param>
		/// <param name="y1">The y-coordinate of the starting point of the curve.</param>
		/// <param name="x2">The x-coordinate of the first control point of the curve.</param>
		/// <param name="y2">The y-coordinate of the first control point of the curve.</param>
		/// <param name="x3">The x-coordinate of the second control point of the curve.</param>
		/// <param name="y3">The y-coordinate of the second control point of the curve.</param>
		/// <param name="x4">The x-coordinate of the ending point of the curve.</param>
		/// <param name="y4">The y-coordinate of the ending point of the curve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x060004C0 RID: 1216 RVA: 0x0000CC94 File Offset: 0x0000AE94
		public void DrawBezier(Pen pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawBezier(this.nativeObject, pen.NativePen, x1, y1, x2, y2, x3, y3, x4, y4));
		}

		/// <summary>Draws a series of Bézier splines from an array of <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the points that determine the curve. The number of points in the array should be a multiple of 3 plus 1, such as 4, 7, or 10.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x060004C1 RID: 1217 RVA: 0x0000CCD4 File Offset: 0x0000AED4
		public void DrawBeziers(Pen pen, Point[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			int num = points.Length;
			if (num < 4)
			{
				return;
			}
			for (int i = 0; i < num - 1; i += 3)
			{
				Point point = points[i];
				Point point2 = points[i + 1];
				Point point3 = points[i + 2];
				Point point4 = points[i + 3];
				GDIPlus.CheckStatus(GDIPlus.GdipDrawBezier(this.nativeObject, pen.NativePen, (float)point.X, (float)point.Y, (float)point2.X, (float)point2.Y, (float)point3.X, (float)point3.Y, (float)point4.X, (float)point4.Y));
			}
		}

		/// <summary>Draws a series of Bézier splines from an array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the points that determine the curve. The number of points in the array should be a multiple of 3 plus 1, such as 4, 7, or 10.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x060004C2 RID: 1218 RVA: 0x0000CD98 File Offset: 0x0000AF98
		public void DrawBeziers(Pen pen, PointF[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			int num = points.Length;
			if (num < 4)
			{
				return;
			}
			for (int i = 0; i < num - 1; i += 3)
			{
				PointF pointF = points[i];
				PointF pointF2 = points[i + 1];
				PointF pointF3 = points[i + 2];
				PointF pointF4 = points[i + 3];
				GDIPlus.CheckStatus(GDIPlus.GdipDrawBezier(this.nativeObject, pen.NativePen, pointF.X, pointF.Y, pointF2.X, pointF2.Y, pointF3.X, pointF3.Y, pointF4.X, pointF4.Y));
			}
		}

		/// <summary>Draws a closed cardinal spline defined by an array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and height of the curve.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x060004C3 RID: 1219 RVA: 0x0000CE4E File Offset: 0x0000B04E
		public void DrawClosedCurve(Pen pen, PointF[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawClosedCurve(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a closed cardinal spline defined by an array of <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and height of the curve.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x060004C4 RID: 1220 RVA: 0x0000CE86 File Offset: 0x0000B086
		public void DrawClosedCurve(Pen pen, Point[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawClosedCurveI(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a closed cardinal spline defined by an array of <see cref="T:System.Drawing.Point" /> structures using a specified tension.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and height of the curve.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline.</param>
		/// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
		/// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled. This parameter is required but ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x060004C5 RID: 1221 RVA: 0x0000CEBE File Offset: 0x0000B0BE
		public void DrawClosedCurve(Pen pen, Point[] points, float tension, FillMode fillmode)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawClosedCurve2I(this.nativeObject, pen.NativePen, points, points.Length, tension));
		}

		/// <summary>Draws a closed cardinal spline defined by an array of <see cref="T:System.Drawing.PointF" /> structures using a specified tension.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and height of the curve.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline.</param>
		/// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
		/// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled. This parameter is required but is ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x060004C6 RID: 1222 RVA: 0x0000CEF7 File Offset: 0x0000B0F7
		public void DrawClosedCurve(Pen pen, PointF[] points, float tension, FillMode fillmode)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawClosedCurve2(this.nativeObject, pen.NativePen, points, points.Length, tension));
		}

		/// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and height of the curve.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x060004C7 RID: 1223 RVA: 0x0000CF30 File Offset: 0x0000B130
		public void DrawCurve(Pen pen, Point[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawCurveI(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x060004C8 RID: 1224 RVA: 0x0000CF68 File Offset: 0x0000B168
		public void DrawCurve(Pen pen, PointF[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawCurve(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.PointF" /> structures using a specified tension.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the points that define the curve.</param>
		/// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x060004C9 RID: 1225 RVA: 0x0000CFA0 File Offset: 0x0000B1A0
		public void DrawCurve(Pen pen, PointF[] points, float tension)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawCurve2(this.nativeObject, pen.NativePen, points, points.Length, tension));
		}

		/// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.Point" /> structures using a specified tension.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline.</param>
		/// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x060004CA RID: 1226 RVA: 0x0000CFD9 File Offset: 0x0000B1D9
		public void DrawCurve(Pen pen, Point[] points, float tension)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawCurve2I(this.nativeObject, pen.NativePen, points, points.Length, tension));
		}

		/// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.PointF" /> structures. The drawing begins offset from the beginning of the array.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline.</param>
		/// <param name="offset">Offset from the first element in the array of the <paramref name="points" /> parameter to the starting point in the curve.</param>
		/// <param name="numberOfSegments">Number of segments after the starting point to include in the curve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x060004CB RID: 1227 RVA: 0x0000D012 File Offset: 0x0000B212
		public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawCurve3(this.nativeObject, pen.NativePen, points, points.Length, offset, numberOfSegments, 0.5f));
		}

		/// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.Point" /> structures using a specified tension.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline.</param>
		/// <param name="offset">Offset from the first element in the array of the <paramref name="points" /> parameter to the starting point in the curve.</param>
		/// <param name="numberOfSegments">Number of segments after the starting point to include in the curve.</param>
		/// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x060004CC RID: 1228 RVA: 0x0000D052 File Offset: 0x0000B252
		public void DrawCurve(Pen pen, Point[] points, int offset, int numberOfSegments, float tension)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawCurve3I(this.nativeObject, pen.NativePen, points, points.Length, offset, numberOfSegments, tension));
		}

		/// <summary>Draws a cardinal spline through a specified array of <see cref="T:System.Drawing.PointF" /> structures using a specified tension. The drawing begins offset from the beginning of the array.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the curve.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline.</param>
		/// <param name="offset">Offset from the first element in the array of the <paramref name="points" /> parameter to the starting point in the curve.</param>
		/// <param name="numberOfSegments">Number of segments after the starting point to include in the curve.</param>
		/// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x060004CD RID: 1229 RVA: 0x0000D08F File Offset: 0x0000B28F
		public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments, float tension)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawCurve3(this.nativeObject, pen.NativePen, points, points.Length, offset, numberOfSegments, tension));
		}

		/// <summary>Draws an ellipse specified by a bounding <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the ellipse.</param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that defines the boundaries of the ellipse.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x060004CE RID: 1230 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		public void DrawEllipse(Pen pen, Rectangle rect)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			this.DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);
		}

		/// <summary>Draws an ellipse defined by a bounding <see cref="T:System.Drawing.RectangleF" />.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the ellipse.</param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that defines the boundaries of the ellipse.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x060004CF RID: 1231 RVA: 0x0000D0FF File Offset: 0x0000B2FF
		public void DrawEllipse(Pen pen, RectangleF rect)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			this.DrawEllipse(pen, rect.X, rect.Y, rect.Width, rect.Height);
		}

		/// <summary>Draws an ellipse defined by a bounding rectangle specified by coordinates for the upper-left corner of the rectangle, a height, and a width.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the ellipse.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse.</param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x060004D0 RID: 1232 RVA: 0x0000D132 File Offset: 0x0000B332
		public void DrawEllipse(Pen pen, int x, int y, int width, int height)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawEllipseI(this.nativeObject, pen.NativePen, x, y, width, height));
		}

		/// <summary>Draws an ellipse defined by a bounding rectangle specified by a pair of coordinates, a height, and a width.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the ellipse.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse.</param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x060004D1 RID: 1233 RVA: 0x0000D15E File Offset: 0x0000B35E
		public void DrawEllipse(Pen pen, float x, float y, float width, float height)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawEllipse(this.nativeObject, pen.NativePen, x, y, width, height));
		}

		/// <summary>Draws the image represented by the specified <see cref="T:System.Drawing.Icon" /> within the area specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="icon">
		///   <see cref="T:System.Drawing.Icon" /> to draw.</param>
		/// <param name="targetRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the resulting image on the display surface. The image contained in the <paramref name="icon" /> parameter is scaled to the dimensions of this rectangular area.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="icon" /> is <see langword="null" />.</exception>
		// Token: 0x060004D2 RID: 1234 RVA: 0x0000D18A File Offset: 0x0000B38A
		public void DrawIcon(Icon icon, Rectangle targetRect)
		{
			if (icon == null)
			{
				throw new ArgumentNullException("icon");
			}
			this.DrawImage(icon.GetInternalBitmap(), targetRect);
		}

		/// <summary>Draws the image represented by the specified <see cref="T:System.Drawing.Icon" /> at the specified coordinates.</summary>
		/// <param name="icon">
		///   <see cref="T:System.Drawing.Icon" /> to draw.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="icon" /> is <see langword="null" />.</exception>
		// Token: 0x060004D3 RID: 1235 RVA: 0x0000D1A7 File Offset: 0x0000B3A7
		public void DrawIcon(Icon icon, int x, int y)
		{
			if (icon == null)
			{
				throw new ArgumentNullException("icon");
			}
			this.DrawImage(icon.GetInternalBitmap(), x, y);
		}

		/// <summary>Draws the image represented by the specified <see cref="T:System.Drawing.Icon" /> without scaling the image.</summary>
		/// <param name="icon">
		///   <see cref="T:System.Drawing.Icon" /> to draw.</param>
		/// <param name="targetRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the resulting image. The image is not scaled to fit this rectangle, but retains its original size. If the image is larger than the rectangle, it is clipped to fit inside it.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="icon" /> is <see langword="null" />.</exception>
		// Token: 0x060004D4 RID: 1236 RVA: 0x0000D1C5 File Offset: 0x0000B3C5
		public void DrawIconUnstretched(Icon icon, Rectangle targetRect)
		{
			if (icon == null)
			{
				throw new ArgumentNullException("icon");
			}
			this.DrawImageUnscaled(icon.GetInternalBitmap(), targetRect);
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004D5 RID: 1237 RVA: 0x0000D1E4 File Offset: 0x0000B3E4
		public void DrawImage(Image image, RectangleF rect)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRect(this.nativeObject, image.NativeObject, rect.X, rect.Y, rect.Width, rect.Height));
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" />, using its original physical size, at the specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="point">
		///   <see cref="T:System.Drawing.PointF" /> structure that represents the upper-left corner of the drawn image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004D6 RID: 1238 RVA: 0x0000D231 File Offset: 0x0000B431
		public void DrawImage(Image image, PointF point)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImage(this.nativeObject, image.NativeObject, point.X, point.Y));
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified shape and size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004D7 RID: 1239 RVA: 0x0000D265 File Offset: 0x0000B465
		public void DrawImage(Image image, Point[] destPoints)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsI(this.nativeObject, image.NativeObject, destPoints, destPoints.Length));
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" />, using its original physical size, at the specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="point">
		///   <see cref="T:System.Drawing.Point" /> structure that represents the location of the upper-left corner of the drawn image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004D8 RID: 1240 RVA: 0x0000D29D File Offset: 0x0000B49D
		public void DrawImage(Image image, Point point)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			this.DrawImage(image, point.X, point.Y);
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004D9 RID: 1241 RVA: 0x0000D2C2 File Offset: 0x0000B4C2
		public void DrawImage(Image image, Rectangle rect)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			this.DrawImage(image, rect.X, rect.Y, rect.Width, rect.Height);
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified shape and size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004DA RID: 1242 RVA: 0x0000D2F5 File Offset: 0x0000B4F5
		public void DrawImage(Image image, PointF[] destPoints)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePoints(this.nativeObject, image.NativeObject, destPoints, destPoints.Length));
		}

		/// <summary>Draws the specified image, using its original physical size, at the location specified by a coordinate pair.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004DB RID: 1243 RVA: 0x0000D32D File Offset: 0x0000B52D
		public void DrawImage(Image image, int x, int y)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageI(this.nativeObject, image.NativeObject, x, y));
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" />, using its original physical size, at the specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004DC RID: 1244 RVA: 0x0000D355 File Offset: 0x0000B555
		public void DrawImage(Image image, float x, float y)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImage(this.nativeObject, image.NativeObject, x, y));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004DD RID: 1245 RVA: 0x0000D380 File Offset: 0x0000B580
		public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRectI(this.nativeObject, image.NativeObject, destRect.X, destRect.Y, destRect.Width, destRect.Height, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004DE RID: 1246 RVA: 0x0000D3F8 File Offset: 0x0000B5F8
		public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRect(this.nativeObject, image.NativeObject, destRect.X, destRect.Y, destRect.Width, destRect.Height, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004DF RID: 1247 RVA: 0x0000D470 File Offset: 0x0000B670
		public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsRectI(this.nativeObject, image.NativeObject, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004E0 RID: 1248 RVA: 0x0000D4DC File Offset: 0x0000B6DC
		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsRect(this.nativeObject, image.NativeObject, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004E1 RID: 1249 RVA: 0x0000D548 File Offset: 0x0000B748
		public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsRectI(this.nativeObject, image.NativeObject, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, (imageAttr != null) ? imageAttr.nativeImageAttributes : IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="width">Width of the drawn image.</param>
		/// <param name="height">Height of the drawn image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004E2 RID: 1250 RVA: 0x0000D5C1 File Offset: 0x0000B7C1
		public void DrawImage(Image image, float x, float y, float width, float height)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRect(this.nativeObject, image.NativeObject, x, y, width, height));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004E3 RID: 1251 RVA: 0x0000D5F0 File Offset: 0x0000B7F0
		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsRect(this.nativeObject, image.NativeObject, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, (imageAttr != null) ? imageAttr.nativeImageAttributes : IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws a portion of an image at a specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004E4 RID: 1252 RVA: 0x0000D66C File Offset: 0x0000B86C
		public void DrawImage(Image image, int x, int y, Rectangle srcRect, GraphicsUnit srcUnit)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointRectI(this.nativeObject, image.NativeObject, x, y, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit));
		}

		/// <summary>Draws the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="width">Width of the drawn image.</param>
		/// <param name="height">Height of the drawn image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004E5 RID: 1253 RVA: 0x0000D6BD File Offset: 0x0000B8BD
		public void DrawImage(Image image, int x, int y, int width, int height)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectI(this.nativeObject, image.nativeObject, x, y, width, height));
		}

		/// <summary>Draws a portion of an image at a specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004E6 RID: 1254 RVA: 0x0000D6EC File Offset: 0x0000B8EC
		public void DrawImage(Image image, float x, float y, RectangleF srcRect, GraphicsUnit srcUnit)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointRect(this.nativeObject, image.nativeObject, x, y, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)" /> method according to application-determined criteria.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004E7 RID: 1255 RVA: 0x0000D740 File Offset: 0x0000B940
		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsRect(this.nativeObject, image.NativeObject, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, (imageAttr != null) ? imageAttr.nativeImageAttributes : IntPtr.Zero, callback, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)" /> method according to application-determined criteria.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004E8 RID: 1256 RVA: 0x0000D7BC File Offset: 0x0000B9BC
		public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsRectI(this.nativeObject, image.NativeObject, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, (imageAttr != null) ? imageAttr.nativeImageAttributes : IntPtr.Zero, callback, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)" /> method according to application-determined criteria.</param>
		/// <param name="callbackData">Value specifying additional data for the <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate to use when checking whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Point[],System.Drawing.Rectangle,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)" /> method.</param>
		// Token: 0x060004E9 RID: 1257 RVA: 0x0000D838 File Offset: 0x0000BA38
		public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback, int callbackData)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (destPoints == null)
			{
				throw new ArgumentNullException("destPoints");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsRectI(this.nativeObject, image.NativeObject, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, (imageAttr != null) ? imageAttr.nativeImageAttributes : IntPtr.Zero, callback, (IntPtr)callbackData));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
		/// <param name="srcWidth">Width of the portion of the source image to draw.</param>
		/// <param name="srcHeight">Height of the portion of the source image to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004EA RID: 1258 RVA: 0x0000D8B4 File Offset: 0x0000BAB4
		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRect(this.nativeObject, image.NativeObject, (float)destRect.X, (float)destRect.Y, (float)destRect.Width, (float)destRect.Height, srcX, srcY, srcWidth, srcHeight, srcUnit, IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the <paramref name="image" /> object to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used by the <paramref name="srcRect" /> parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)" /> method according to application-determined criteria.</param>
		/// <param name="callbackData">Value specifying additional data for the <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate to use when checking whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.PointF[],System.Drawing.RectangleF,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.Int32)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004EB RID: 1259 RVA: 0x0000D91C File Offset: 0x0000BB1C
		public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback, int callbackData)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImagePointsRect(this.nativeObject, image.NativeObject, destPoints, destPoints.Length, srcRect.X, srcRect.Y, srcRect.Width, srcRect.Height, srcUnit, (imageAttr != null) ? imageAttr.nativeImageAttributes : IntPtr.Zero, callback, (IntPtr)callbackData));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
		/// <param name="srcWidth">Width of the portion of the source image to draw.</param>
		/// <param name="srcHeight">Height of the portion of the source image to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004EC RID: 1260 RVA: 0x0000D97C File Offset: 0x0000BB7C
		public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRectI(this.nativeObject, image.NativeObject, destRect.X, destRect.Y, destRect.Width, destRect.Height, srcX, srcY, srcWidth, srcHeight, srcUnit, IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
		/// <param name="srcWidth">Width of the portion of the source image to draw.</param>
		/// <param name="srcHeight">Height of the portion of the source image to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle.</param>
		/// <param name="imageAttrs">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004ED RID: 1261 RVA: 0x0000D9E0 File Offset: 0x0000BBE0
		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRect(this.nativeObject, image.NativeObject, (float)destRect.X, (float)destRect.Y, (float)destRect.Width, (float)destRect.Height, srcX, srcY, srcWidth, srcHeight, srcUnit, (imageAttrs != null) ? imageAttrs.nativeImageAttributes : IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
		/// <param name="srcWidth">Width of the portion of the source image to draw.</param>
		/// <param name="srcHeight">Height of the portion of the source image to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004EE RID: 1262 RVA: 0x0000DA54 File Offset: 0x0000BC54
		public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRectI(this.nativeObject, image.NativeObject, destRect.X, destRect.Y, destRect.Width, destRect.Height, srcX, srcY, srcWidth, srcHeight, srcUnit, (imageAttr != null) ? imageAttr.nativeImageAttributes : IntPtr.Zero, null, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
		/// <param name="srcWidth">Width of the portion of the source image to draw.</param>
		/// <param name="srcHeight">Height of the portion of the source image to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for <paramref name="image" />.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Int32,System.Int32,System.Int32,System.Int32,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)" /> method according to application-determined criteria.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004EF RID: 1263 RVA: 0x0000DAC4 File Offset: 0x0000BCC4
		public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr, Graphics.DrawImageAbort callback)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRectI(this.nativeObject, image.NativeObject, destRect.X, destRect.Y, destRect.Width, destRect.Height, srcX, srcY, srcWidth, srcHeight, srcUnit, (imageAttr != null) ? imageAttr.nativeImageAttributes : IntPtr.Zero, callback, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
		/// <param name="srcWidth">Width of the portion of the source image to draw.</param>
		/// <param name="srcHeight">Height of the portion of the source image to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle.</param>
		/// <param name="imageAttrs">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Single,System.Single,System.Single,System.Single,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort)" /> method according to application-determined criteria.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004F0 RID: 1264 RVA: 0x0000DB34 File Offset: 0x0000BD34
		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs, Graphics.DrawImageAbort callback)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRect(this.nativeObject, image.NativeObject, (float)destRect.X, (float)destRect.Y, (float)destRect.Width, (float)destRect.Height, srcX, srcY, srcWidth, srcHeight, srcUnit, (imageAttrs != null) ? imageAttrs.nativeImageAttributes : IntPtr.Zero, callback, IntPtr.Zero));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
		/// <param name="srcWidth">Width of the portion of the source image to draw.</param>
		/// <param name="srcHeight">Height of the portion of the source image to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle.</param>
		/// <param name="imageAttrs">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Single,System.Single,System.Single,System.Single,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.IntPtr)" /> method according to application-determined criteria.</param>
		/// <param name="callbackData">Value specifying additional data for the <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate to use when checking whether to stop execution of the <see langword="DrawImage" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004F1 RID: 1265 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs, Graphics.DrawImageAbort callback, IntPtr callbackData)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRect(this.nativeObject, image.NativeObject, (float)destRect.X, (float)destRect.Y, (float)destRect.Width, (float)destRect.Height, srcX, srcY, srcWidth, srcHeight, srcUnit, (imageAttrs != null) ? imageAttrs.nativeImageAttributes : IntPtr.Zero, callback, callbackData));
		}

		/// <summary>Draws the specified portion of the specified <see cref="T:System.Drawing.Image" /> at the specified location and with the specified size.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn image. The image is scaled to fit the rectangle.</param>
		/// <param name="srcX">The x-coordinate of the upper-left corner of the portion of the source image to draw.</param>
		/// <param name="srcY">The y-coordinate of the upper-left corner of the portion of the source image to draw.</param>
		/// <param name="srcWidth">Width of the portion of the source image to draw.</param>
		/// <param name="srcHeight">Height of the portion of the source image to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the units of measure used to determine the source rectangle.</param>
		/// <param name="imageAttrs">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies recoloring and gamma information for the <paramref name="image" /> object.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate that specifies a method to call during the drawing of the image. This method is called frequently to check whether to stop execution of the <see cref="M:System.Drawing.Graphics.DrawImage(System.Drawing.Image,System.Drawing.Rectangle,System.Int32,System.Int32,System.Int32,System.Int32,System.Drawing.GraphicsUnit,System.Drawing.Imaging.ImageAttributes,System.Drawing.Graphics.DrawImageAbort,System.IntPtr)" /> method according to application-determined criteria.</param>
		/// <param name="callbackData">Value specifying additional data for the <see cref="T:System.Drawing.Graphics.DrawImageAbort" /> delegate to use when checking whether to stop execution of the <see langword="DrawImage" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004F2 RID: 1266 RVA: 0x0000DC18 File Offset: 0x0000BE18
		public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs, Graphics.DrawImageAbort callback, IntPtr callbackData)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRect(this.nativeObject, image.NativeObject, (float)destRect.X, (float)destRect.Y, (float)destRect.Width, (float)destRect.Height, (float)srcX, (float)srcY, (float)srcWidth, (float)srcHeight, srcUnit, (imageAttrs != null) ? imageAttrs.nativeImageAttributes : IntPtr.Zero, callback, callbackData));
		}

		/// <summary>Draws a specified image using its original physical size at a specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="point">
		///   <see cref="T:System.Drawing.Point" /> structure that specifies the upper-left corner of the drawn image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004F3 RID: 1267 RVA: 0x0000DC8C File Offset: 0x0000BE8C
		public void DrawImageUnscaled(Image image, Point point)
		{
			this.DrawImageUnscaled(image, point.X, point.Y);
		}

		/// <summary>Draws a specified image using its original physical size at a specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> that specifies the upper-left corner of the drawn image. The X and Y properties of the rectangle specify the upper-left corner. The Width and Height properties are ignored.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004F4 RID: 1268 RVA: 0x0000DCA3 File Offset: 0x0000BEA3
		public void DrawImageUnscaled(Image image, Rectangle rect)
		{
			this.DrawImageUnscaled(image, rect.X, rect.Y, rect.Width, rect.Height);
		}

		/// <summary>Draws the specified image using its original physical size at the location specified by a coordinate pair.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004F5 RID: 1269 RVA: 0x0000DCC8 File Offset: 0x0000BEC8
		public void DrawImageUnscaled(Image image, int x, int y)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			this.DrawImage(image, x, y, image.Width, image.Height);
		}

		/// <summary>Draws a specified image using its original physical size at a specified location.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn image.</param>
		/// <param name="width">Not used.</param>
		/// <param name="height">Not used.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004F6 RID: 1270 RVA: 0x0000DCF0 File Offset: 0x0000BEF0
		public void DrawImageUnscaled(Image image, int x, int y, int width, int height)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if (width <= 0 || height <= 0)
			{
				return;
			}
			using (Image image2 = new Bitmap(width, height))
			{
				using (Graphics graphics = Graphics.FromImage(image2))
				{
					graphics.DrawImage(image, 0, 0, image.Width, image.Height);
					this.DrawImage(image2, x, y, width, height);
				}
			}
		}

		/// <summary>Draws the specified image without scaling and clips it, if necessary, to fit in the specified rectangle.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to draw.</param>
		/// <param name="rect">The <see cref="T:System.Drawing.Rectangle" /> in which to draw the image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x060004F7 RID: 1271 RVA: 0x0000DD7C File Offset: 0x0000BF7C
		public void DrawImageUnscaledAndClipped(Image image, Rectangle rect)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			int width = (image.Width > rect.Width) ? rect.Width : image.Width;
			int height = (image.Height > rect.Height) ? rect.Height : image.Height;
			this.DrawImageUnscaled(image, rect.X, rect.Y, width, height);
		}

		/// <summary>Draws a line connecting two <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line.</param>
		/// <param name="pt1">
		///   <see cref="T:System.Drawing.PointF" /> structure that represents the first point to connect.</param>
		/// <param name="pt2">
		///   <see cref="T:System.Drawing.PointF" /> structure that represents the second point to connect.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x060004F8 RID: 1272 RVA: 0x0000DDEC File Offset: 0x0000BFEC
		public void DrawLine(Pen pen, PointF pt1, PointF pt2)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawLine(this.nativeObject, pen.NativePen, pt1.X, pt1.Y, pt2.X, pt2.Y));
		}

		/// <summary>Draws a line connecting two <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line.</param>
		/// <param name="pt1">
		///   <see cref="T:System.Drawing.Point" /> structure that represents the first point to connect.</param>
		/// <param name="pt2">
		///   <see cref="T:System.Drawing.Point" /> structure that represents the second point to connect.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x060004F9 RID: 1273 RVA: 0x0000DE3C File Offset: 0x0000C03C
		public void DrawLine(Pen pen, Point pt1, Point pt2)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawLineI(this.nativeObject, pen.NativePen, pt1.X, pt1.Y, pt2.X, pt2.Y));
		}

		/// <summary>Draws a line connecting the two points specified by the coordinate pairs.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line.</param>
		/// <param name="x1">The x-coordinate of the first point.</param>
		/// <param name="y1">The y-coordinate of the first point.</param>
		/// <param name="x2">The x-coordinate of the second point.</param>
		/// <param name="y2">The y-coordinate of the second point.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x060004FA RID: 1274 RVA: 0x0000DE89 File Offset: 0x0000C089
		public void DrawLine(Pen pen, int x1, int y1, int x2, int y2)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawLineI(this.nativeObject, pen.NativePen, x1, y1, x2, y2));
		}

		/// <summary>Draws a line connecting the two points specified by the coordinate pairs.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line.</param>
		/// <param name="x1">The x-coordinate of the first point.</param>
		/// <param name="y1">The y-coordinate of the first point.</param>
		/// <param name="x2">The x-coordinate of the second point.</param>
		/// <param name="y2">The y-coordinate of the second point.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x060004FB RID: 1275 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		public void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (!float.IsNaN(x1) && !float.IsNaN(y1) && !float.IsNaN(x2) && !float.IsNaN(y2))
			{
				GDIPlus.CheckStatus(GDIPlus.GdipDrawLine(this.nativeObject, pen.NativePen, x1, y1, x2, y2));
			}
		}

		/// <summary>Draws a series of line segments that connect an array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line segments.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the points to connect.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x060004FC RID: 1276 RVA: 0x0000DF11 File Offset: 0x0000C111
		public void DrawLines(Pen pen, PointF[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawLines(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a series of line segments that connect an array of <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the line segments.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the points to connect.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x060004FD RID: 1277 RVA: 0x0000DF49 File Offset: 0x0000C149
		public void DrawLines(Pen pen, Point[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawLinesI(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the path.</param>
		/// <param name="path">
		///   <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to draw.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="path" /> is <see langword="null" />.</exception>
		// Token: 0x060004FE RID: 1278 RVA: 0x0000DF81 File Offset: 0x0000C181
		public void DrawPath(Pen pen, GraphicsPath path)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawPath(this.nativeObject, pen.NativePen, path.nativePath));
		}

		/// <summary>Draws a pie shape defined by an ellipse specified by a <see cref="T:System.Drawing.Rectangle" /> structure and two radial lines.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the pie shape.</param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that represents the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
		/// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape.</param>
		/// <param name="sweepAngle">Angle measured in degrees clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie shape.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x060004FF RID: 1279 RVA: 0x0000DFBB File Offset: 0x0000C1BB
		public void DrawPie(Pen pen, Rectangle rect, float startAngle, float sweepAngle)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			this.DrawPie(pen, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height, startAngle, sweepAngle);
		}

		/// <summary>Draws a pie shape defined by an ellipse specified by a <see cref="T:System.Drawing.RectangleF" /> structure and two radial lines.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the pie shape.</param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that represents the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
		/// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape.</param>
		/// <param name="sweepAngle">Angle measured in degrees clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie shape.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x06000500 RID: 1280 RVA: 0x0000DFF5 File Offset: 0x0000C1F5
		public void DrawPie(Pen pen, RectangleF rect, float startAngle, float sweepAngle)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			this.DrawPie(pen, rect.X, rect.Y, rect.Width, rect.Height, startAngle, sweepAngle);
		}

		/// <summary>Draws a pie shape defined by an ellipse specified by a coordinate pair, a width, a height, and two radial lines.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the pie shape.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
		/// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape.</param>
		/// <param name="sweepAngle">Angle measured in degrees clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie shape.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x06000501 RID: 1281 RVA: 0x0000E02B File Offset: 0x0000C22B
		public void DrawPie(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawPie(this.nativeObject, pen.NativePen, x, y, width, height, startAngle, sweepAngle));
		}

		/// <summary>Draws a pie shape defined by an ellipse specified by a coordinate pair, a width, a height, and two radial lines.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the pie shape.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie shape comes.</param>
		/// <param name="startAngle">Angle measured in degrees clockwise from the x-axis to the first side of the pie shape.</param>
		/// <param name="sweepAngle">Angle measured in degrees clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie shape.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x06000502 RID: 1282 RVA: 0x0000E05B File Offset: 0x0000C25B
		public void DrawPie(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawPieI(this.nativeObject, pen.NativePen, x, y, width, height, (float)startAngle, (float)sweepAngle));
		}

		/// <summary>Draws a polygon defined by an array of <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the polygon.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the vertices of the polygon.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x06000503 RID: 1283 RVA: 0x0000E08D File Offset: 0x0000C28D
		public void DrawPolygon(Pen pen, Point[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawPolygonI(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a polygon defined by an array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the polygon.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the vertices of the polygon.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x06000504 RID: 1284 RVA: 0x0000E0C5 File Offset: 0x0000C2C5
		public void DrawPolygon(Pen pen, PointF[] points)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawPolygon(this.nativeObject, pen.NativePen, points, points.Length));
		}

		/// <summary>Draws a rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the rectangle.</param>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle to draw.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x06000505 RID: 1285 RVA: 0x0000E0FD File Offset: 0x0000C2FD
		public void DrawRectangle(Pen pen, Rectangle rect)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			this.DrawRectangle(pen, rect.Left, rect.Top, rect.Width, rect.Height);
		}

		/// <summary>Draws a rectangle specified by a coordinate pair, a width, and a height.</summary>
		/// <param name="pen">A <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the rectangle.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw.</param>
		/// <param name="width">The width of the rectangle to draw.</param>
		/// <param name="height">The height of the rectangle to draw.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x06000506 RID: 1286 RVA: 0x0000E130 File Offset: 0x0000C330
		public void DrawRectangle(Pen pen, float x, float y, float width, float height)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawRectangle(this.nativeObject, pen.NativePen, x, y, width, height));
		}

		/// <summary>Draws a rectangle specified by a coordinate pair, a width, and a height.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the rectangle.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to draw.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to draw.</param>
		/// <param name="width">Width of the rectangle to draw.</param>
		/// <param name="height">Height of the rectangle to draw.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.</exception>
		// Token: 0x06000507 RID: 1287 RVA: 0x0000E15C File Offset: 0x0000C35C
		public void DrawRectangle(Pen pen, int x, int y, int width, int height)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("pen");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawRectangleI(this.nativeObject, pen.NativePen, x, y, width, height));
		}

		/// <summary>Draws a series of rectangles specified by <see cref="T:System.Drawing.RectangleF" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the outlines of the rectangles.</param>
		/// <param name="rects">Array of <see cref="T:System.Drawing.RectangleF" /> structures that represent the rectangles to draw.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="rects" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rects" /> is a zero-length array.</exception>
		// Token: 0x06000508 RID: 1288 RVA: 0x0000E188 File Offset: 0x0000C388
		public void DrawRectangles(Pen pen, RectangleF[] rects)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("image");
			}
			if (rects == null)
			{
				throw new ArgumentNullException("rects");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawRectangles(this.nativeObject, pen.NativePen, rects, rects.Length));
		}

		/// <summary>Draws a series of rectangles specified by <see cref="T:System.Drawing.Rectangle" /> structures.</summary>
		/// <param name="pen">
		///   <see cref="T:System.Drawing.Pen" /> that determines the color, width, and style of the outlines of the rectangles.</param>
		/// <param name="rects">Array of <see cref="T:System.Drawing.Rectangle" /> structures that represent the rectangles to draw.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pen" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="rects" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rects" /> is a zero-length array.</exception>
		// Token: 0x06000509 RID: 1289 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		public void DrawRectangles(Pen pen, Rectangle[] rects)
		{
			if (pen == null)
			{
				throw new ArgumentNullException("image");
			}
			if (rects == null)
			{
				throw new ArgumentNullException("rects");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawRectanglesI(this.nativeObject, pen.NativePen, rects, rects.Length));
		}

		/// <summary>Draws the specified text string in the specified rectangle with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects.</summary>
		/// <param name="s">String to draw.</param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text.</param>
		/// <param name="layoutRectangle">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location of the drawn text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="s" /> is <see langword="null" />.</exception>
		// Token: 0x0600050A RID: 1290 RVA: 0x0000E1F8 File Offset: 0x0000C3F8
		public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle)
		{
			this.DrawString(s, font, brush, layoutRectangle, null);
		}

		/// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects.</summary>
		/// <param name="s">String to draw.</param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text.</param>
		/// <param name="point">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the upper-left corner of the drawn text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="s" /> is <see langword="null" />.</exception>
		// Token: 0x0600050B RID: 1291 RVA: 0x0000E206 File Offset: 0x0000C406
		public void DrawString(string s, Font font, Brush brush, PointF point)
		{
			this.DrawString(s, font, brush, new RectangleF(point.X, point.Y, 0f, 0f), null);
		}

		/// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects using the formatting attributes of the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <param name="s">String to draw.</param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text.</param>
		/// <param name="point">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the upper-left corner of the drawn text.</param>
		/// <param name="format">
		///   <see cref="T:System.Drawing.StringFormat" /> that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="s" /> is <see langword="null" />.</exception>
		// Token: 0x0600050C RID: 1292 RVA: 0x0000E22F File Offset: 0x0000C42F
		public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format)
		{
			this.DrawString(s, font, brush, new RectangleF(point.X, point.Y, 0f, 0f), format);
		}

		/// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects.</summary>
		/// <param name="s">String to draw.</param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn text.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="s" /> is <see langword="null" />.</exception>
		// Token: 0x0600050D RID: 1293 RVA: 0x0000E259 File Offset: 0x0000C459
		public void DrawString(string s, Font font, Brush brush, float x, float y)
		{
			this.DrawString(s, font, brush, new RectangleF(x, y, 0f, 0f), null);
		}

		/// <summary>Draws the specified text string at the specified location with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects using the formatting attributes of the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <param name="s">String to draw.</param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the drawn text.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the drawn text.</param>
		/// <param name="format">
		///   <see cref="T:System.Drawing.StringFormat" /> that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="s" /> is <see langword="null" />.</exception>
		// Token: 0x0600050E RID: 1294 RVA: 0x0000E278 File Offset: 0x0000C478
		public void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format)
		{
			this.DrawString(s, font, brush, new RectangleF(x, y, 0f, 0f), format);
		}

		/// <summary>Draws the specified text string in the specified rectangle with the specified <see cref="T:System.Drawing.Brush" /> and <see cref="T:System.Drawing.Font" /> objects using the formatting attributes of the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <param name="s">String to draw.</param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the color and texture of the drawn text.</param>
		/// <param name="layoutRectangle">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location of the drawn text.</param>
		/// <param name="format">
		///   <see cref="T:System.Drawing.StringFormat" /> that specifies formatting attributes, such as line spacing and alignment, that are applied to the drawn text.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="s" /> is <see langword="null" />.</exception>
		// Token: 0x0600050F RID: 1295 RVA: 0x0000E298 File Offset: 0x0000C498
		public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
		{
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (s == null || s.Length == 0)
			{
				return;
			}
			GDIPlus.CheckStatus(GDIPlus.GdipDrawString(this.nativeObject, s, s.Length, font.NativeObject, ref layoutRectangle, (format != null) ? format.NativeObject : IntPtr.Zero, brush.NativeBrush));
		}

		/// <summary>Closes the current graphics container and restores the state of this <see cref="T:System.Drawing.Graphics" /> to the state saved by a call to the <see cref="M:System.Drawing.Graphics.BeginContainer" /> method.</summary>
		/// <param name="container">
		///   <see cref="T:System.Drawing.Drawing2D.GraphicsContainer" /> that represents the container this method restores.</param>
		// Token: 0x06000510 RID: 1296 RVA: 0x0000E304 File Offset: 0x0000C504
		public void EndContainer(GraphicsContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipEndContainer(this.nativeObject, container.NativeObject));
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		// Token: 0x06000511 RID: 1297 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		// Token: 0x06000512 RID: 1298 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		// Token: 0x06000513 RID: 1299 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		// Token: 0x06000514 RID: 1300 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		// Token: 0x06000515 RID: 1301 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		// Token: 0x06000516 RID: 1302 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		// Token: 0x06000517 RID: 1303 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		// Token: 0x06000518 RID: 1304 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		// Token: 0x06000519 RID: 1305 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		// Token: 0x0600051A RID: 1306 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		// Token: 0x0600051B RID: 1307 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		// Token: 0x0600051C RID: 1308 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		// Token: 0x0600051D RID: 1309 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		// Token: 0x0600051E RID: 1310 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structures that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		// Token: 0x0600051F RID: 1311 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		// Token: 0x06000520 RID: 1312 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		// Token: 0x06000521 RID: 1313 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		// Token: 0x06000522 RID: 1314 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
		// Token: 0x06000523 RID: 1315 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
		// Token: 0x06000524 RID: 1316 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
		// Token: 0x06000525 RID: 1317 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
		// Token: 0x06000526 RID: 1318 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
		// Token: 0x06000527 RID: 1319 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of the specified <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
		// Token: 0x06000528 RID: 1320 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		// Token: 0x06000529 RID: 1321 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		// Token: 0x0600052A RID: 1322 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		// Token: 0x0600052B RID: 1323 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		// Token: 0x0600052C RID: 1324 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		// Token: 0x0600052D RID: 1325 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="srcUnit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		// Token: 0x0600052E RID: 1326 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.Point" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
		// Token: 0x0600052F RID: 1327 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point[] destPoints, Rectangle srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the location and size of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
		// Token: 0x06000530 RID: 1328 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Rectangle destRect, Rectangle srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.Point" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
		// Token: 0x06000531 RID: 1329 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, Point destPoint, Rectangle srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records of a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified rectangle using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the location and size of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
		// Token: 0x06000532 RID: 1330 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, RectangleF destRect, RectangleF srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display in a specified parallelogram using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoints">Array of three <see cref="T:System.Drawing.PointF" /> structures that define a parallelogram that determines the size and location of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
		// Token: 0x06000533 RID: 1331 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF[] destPoints, RectangleF srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sends the records in a selected rectangle from a <see cref="T:System.Drawing.Imaging.Metafile" />, one at a time, to a callback method for display at a specified point using specified image attributes.</summary>
		/// <param name="metafile">
		///   <see cref="T:System.Drawing.Imaging.Metafile" /> to enumerate.</param>
		/// <param name="destPoint">
		///   <see cref="T:System.Drawing.PointF" /> structure that specifies the location of the upper-left corner of the drawn metafile.</param>
		/// <param name="srcRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the portion of the metafile, relative to its upper-left corner, to draw.</param>
		/// <param name="unit">Member of the <see cref="T:System.Drawing.GraphicsUnit" /> enumeration that specifies the unit of measure used to determine the portion of the metafile that the rectangle specified by the <paramref name="srcRect" /> parameter contains.</param>
		/// <param name="callback">
		///   <see cref="T:System.Drawing.Graphics.EnumerateMetafileProc" /> delegate that specifies the method to which the metafile records are sent.</param>
		/// <param name="callbackData">Internal pointer that is required, but ignored. You can pass <see cref="F:System.IntPtr.Zero" /> for this parameter.</param>
		/// <param name="imageAttr">
		///   <see cref="T:System.Drawing.Imaging.ImageAttributes" /> that specifies image attribute information for the drawn image.</param>
		// Token: 0x06000534 RID: 1332 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("Metafiles enumeration, for both WMF and EMF formats, isn't supported.")]
		public void EnumerateMetafile(Metafile metafile, PointF destPoint, RectangleF srcRect, GraphicsUnit unit, Graphics.EnumerateMetafileProc callback, IntPtr callbackData, ImageAttributes imageAttr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Updates the clip region of this <see cref="T:System.Drawing.Graphics" /> to exclude the area specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that specifies the rectangle to exclude from the clip region.</param>
		// Token: 0x06000535 RID: 1333 RVA: 0x0000E32A File Offset: 0x0000C52A
		public void ExcludeClip(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRectI(this.nativeObject, rect.X, rect.Y, rect.Width, rect.Height, CombineMode.Exclude));
		}

		/// <summary>Updates the clip region of this <see cref="T:System.Drawing.Graphics" /> to exclude the area specified by a <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">
		///   <see cref="T:System.Drawing.Region" /> that specifies the region to exclude from the clip region.</param>
		// Token: 0x06000536 RID: 1334 RVA: 0x0000E359 File Offset: 0x0000C559
		public void ExcludeClip(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRegion(this.nativeObject, region.NativeObject, CombineMode.Exclude));
		}

		/// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x06000537 RID: 1335 RVA: 0x0000E380 File Offset: 0x0000C580
		public void FillClosedCurve(Brush brush, PointF[] points)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillClosedCurve(this.nativeObject, brush.NativeBrush, points, points.Length));
		}

		/// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x06000538 RID: 1336 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
		public void FillClosedCurve(Brush brush, Point[] points)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillClosedCurveI(this.nativeObject, brush.NativeBrush, points, points.Length));
		}

		/// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.PointF" /> structures using the specified fill mode.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline.</param>
		/// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x06000539 RID: 1337 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			this.FillClosedCurve(brush, points, fillmode, 0.5f);
		}

		/// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.Point" /> structures using the specified fill mode.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline.</param>
		/// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x0600053A RID: 1338 RVA: 0x0000E41C File Offset: 0x0000C61C
		public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			this.FillClosedCurve(brush, points, fillmode, 0.5f);
		}

		/// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.PointF" /> structures using the specified fill mode and tension.</summary>
		/// <param name="brush">A <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that define the spline.</param>
		/// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled.</param>
		/// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x0600053B RID: 1339 RVA: 0x0000E448 File Offset: 0x0000C648
		public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode, float tension)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillClosedCurve2(this.nativeObject, brush.NativeBrush, points, points.Length, tension, fillmode));
		}

		/// <summary>Fills the interior of a closed cardinal spline curve defined by an array of <see cref="T:System.Drawing.Point" /> structures using the specified fill mode and tension.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that define the spline.</param>
		/// <param name="fillmode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines how the curve is filled.</param>
		/// <param name="tension">Value greater than or equal to 0.0F that specifies the tension of the curve.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x0600053C RID: 1340 RVA: 0x0000E483 File Offset: 0x0000C683
		public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode, float tension)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillClosedCurve2I(this.nativeObject, brush.NativeBrush, points, points.Length, tension, fillmode));
		}

		/// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that represents the bounding rectangle that defines the ellipse.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.</exception>
		// Token: 0x0600053D RID: 1341 RVA: 0x0000E4BE File Offset: 0x0000C6BE
		public void FillEllipse(Brush brush, Rectangle rect)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			this.FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
		}

		/// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that represents the bounding rectangle that defines the ellipse.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.</exception>
		// Token: 0x0600053E RID: 1342 RVA: 0x0000E4F1 File Offset: 0x0000C6F1
		public void FillEllipse(Brush brush, RectangleF rect)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			this.FillEllipse(brush, rect.X, rect.Y, rect.Width, rect.Height);
		}

		/// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a pair of coordinates, a width, and a height.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse.</param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.</exception>
		// Token: 0x0600053F RID: 1343 RVA: 0x0000E524 File Offset: 0x0000C724
		public void FillEllipse(Brush brush, float x, float y, float width, float height)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillEllipse(this.nativeObject, brush.NativeBrush, x, y, width, height));
		}

		/// <summary>Fills the interior of an ellipse defined by a bounding rectangle specified by a pair of coordinates, a width, and a height.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse.</param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse.</param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.</exception>
		// Token: 0x06000540 RID: 1344 RVA: 0x0000E550 File Offset: 0x0000C750
		public void FillEllipse(Brush brush, int x, int y, int width, int height)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillEllipseI(this.nativeObject, brush.NativeBrush, x, y, width, height));
		}

		/// <summary>Fills the interior of a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="path">
		///   <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that represents the path to fill.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="path" /> is <see langword="null" />.</exception>
		// Token: 0x06000541 RID: 1345 RVA: 0x0000E57C File Offset: 0x0000C77C
		public void FillPath(Brush brush, GraphicsPath path)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPath(this.nativeObject, brush.NativeBrush, path.nativePath));
		}

		/// <summary>Fills the interior of a pie section defined by an ellipse specified by a <see cref="T:System.Drawing.RectangleF" /> structure and two radial lines.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that represents the bounding rectangle that defines the ellipse from which the pie section comes.</param>
		/// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section.</param>
		/// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie section.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.</exception>
		// Token: 0x06000542 RID: 1346 RVA: 0x0000E5B8 File Offset: 0x0000C7B8
		public void FillPie(Brush brush, Rectangle rect, float startAngle, float sweepAngle)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPie(this.nativeObject, brush.NativeBrush, (float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height, startAngle, sweepAngle));
		}

		/// <summary>Fills the interior of a pie section defined by an ellipse specified by a pair of coordinates, a width, a height, and two radial lines.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
		/// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section.</param>
		/// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie section.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.</exception>
		// Token: 0x06000543 RID: 1347 RVA: 0x0000E60C File Offset: 0x0000C80C
		public void FillPie(Brush brush, int x, int y, int width, int height, int startAngle, int sweepAngle)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPieI(this.nativeObject, brush.NativeBrush, x, y, width, height, (float)startAngle, (float)sweepAngle));
		}

		/// <summary>Fills the interior of a pie section defined by an ellipse specified by a pair of coordinates, a width, a height, and two radial lines.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
		/// <param name="width">Width of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
		/// <param name="height">Height of the bounding rectangle that defines the ellipse from which the pie section comes.</param>
		/// <param name="startAngle">Angle in degrees measured clockwise from the x-axis to the first side of the pie section.</param>
		/// <param name="sweepAngle">Angle in degrees measured clockwise from the <paramref name="startAngle" /> parameter to the second side of the pie section.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.</exception>
		// Token: 0x06000544 RID: 1348 RVA: 0x0000E63E File Offset: 0x0000C83E
		public void FillPie(Brush brush, float x, float y, float width, float height, float startAngle, float sweepAngle)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPie(this.nativeObject, brush.NativeBrush, x, y, width, height, startAngle, sweepAngle));
		}

		/// <summary>Fills the interior of a polygon defined by an array of points specified by <see cref="T:System.Drawing.PointF" /> structures.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the vertices of the polygon to fill.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x06000545 RID: 1349 RVA: 0x0000E66E File Offset: 0x0000C86E
		public void FillPolygon(Brush brush, PointF[] points)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPolygon2(this.nativeObject, brush.NativeBrush, points, points.Length));
		}

		/// <summary>Fills the interior of a polygon defined by an array of points specified by <see cref="T:System.Drawing.Point" /> structures.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the vertices of the polygon to fill.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x06000546 RID: 1350 RVA: 0x0000E6A6 File Offset: 0x0000C8A6
		public void FillPolygon(Brush brush, Point[] points)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPolygon2I(this.nativeObject, brush.NativeBrush, points, points.Length));
		}

		/// <summary>Fills the interior of a polygon defined by an array of points specified by <see cref="T:System.Drawing.Point" /> structures using the specified fill mode.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.Point" /> structures that represent the vertices of the polygon to fill.</param>
		/// <param name="fillMode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines the style of the fill.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x06000547 RID: 1351 RVA: 0x0000E6DE File Offset: 0x0000C8DE
		public void FillPolygon(Brush brush, Point[] points, FillMode fillMode)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPolygonI(this.nativeObject, brush.NativeBrush, points, points.Length, fillMode));
		}

		/// <summary>Fills the interior of a polygon defined by an array of points specified by <see cref="T:System.Drawing.PointF" /> structures using the specified fill mode.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="points">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the vertices of the polygon to fill.</param>
		/// <param name="fillMode">Member of the <see cref="T:System.Drawing.Drawing2D.FillMode" /> enumeration that determines the style of the fill.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="points" /> is <see langword="null" />.</exception>
		// Token: 0x06000548 RID: 1352 RVA: 0x0000E717 File Offset: 0x0000C917
		public void FillPolygon(Brush brush, PointF[] points, FillMode fillMode)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (points == null)
			{
				throw new ArgumentNullException("points");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillPolygon(this.nativeObject, brush.NativeBrush, points, points.Length, fillMode));
		}

		/// <summary>Fills the interior of a rectangle specified by a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle to fill.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.</exception>
		// Token: 0x06000549 RID: 1353 RVA: 0x0000E750 File Offset: 0x0000C950
		public void FillRectangle(Brush brush, RectangleF rect)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			this.FillRectangle(brush, rect.Left, rect.Top, rect.Width, rect.Height);
		}

		/// <summary>Fills the interior of a rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle to fill.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.</exception>
		// Token: 0x0600054A RID: 1354 RVA: 0x0000E783 File Offset: 0x0000C983
		public void FillRectangle(Brush brush, Rectangle rect)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			this.FillRectangle(brush, rect.Left, rect.Top, rect.Width, rect.Height);
		}

		/// <summary>Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill.</param>
		/// <param name="width">Width of the rectangle to fill.</param>
		/// <param name="height">Height of the rectangle to fill.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.</exception>
		// Token: 0x0600054B RID: 1355 RVA: 0x0000E7B6 File Offset: 0x0000C9B6
		public void FillRectangle(Brush brush, int x, int y, int width, int height)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillRectangleI(this.nativeObject, brush.NativeBrush, x, y, width, height));
		}

		/// <summary>Fills the interior of a rectangle specified by a pair of coordinates, a width, and a height.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to fill.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to fill.</param>
		/// <param name="width">Width of the rectangle to fill.</param>
		/// <param name="height">Height of the rectangle to fill.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.</exception>
		// Token: 0x0600054C RID: 1356 RVA: 0x0000E7E2 File Offset: 0x0000C9E2
		public void FillRectangle(Brush brush, float x, float y, float width, float height)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillRectangle(this.nativeObject, brush.NativeBrush, x, y, width, height));
		}

		/// <summary>Fills the interiors of a series of rectangles specified by <see cref="T:System.Drawing.Rectangle" /> structures.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="rects">Array of <see cref="T:System.Drawing.Rectangle" /> structures that represent the rectangles to fill.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="rects" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="rects" /> is a zero-length array.</exception>
		// Token: 0x0600054D RID: 1357 RVA: 0x0000E80E File Offset: 0x0000CA0E
		public void FillRectangles(Brush brush, Rectangle[] rects)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (rects == null)
			{
				throw new ArgumentNullException("rects");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillRectanglesI(this.nativeObject, brush.NativeBrush, rects, rects.Length));
		}

		/// <summary>Fills the interiors of a series of rectangles specified by <see cref="T:System.Drawing.RectangleF" /> structures.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="rects">Array of <see cref="T:System.Drawing.RectangleF" /> structures that represent the rectangles to fill.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="rects" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Rects" /> is a zero-length array.</exception>
		// Token: 0x0600054E RID: 1358 RVA: 0x0000E846 File Offset: 0x0000CA46
		public void FillRectangles(Brush brush, RectangleF[] rects)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (rects == null)
			{
				throw new ArgumentNullException("rects");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillRectangles(this.nativeObject, brush.NativeBrush, rects, rects.Length));
		}

		/// <summary>Fills the interior of a <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="brush">
		///   <see cref="T:System.Drawing.Brush" /> that determines the characteristics of the fill.</param>
		/// <param name="region">
		///   <see cref="T:System.Drawing.Region" /> that represents the area to fill.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="brush" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="region" /> is <see langword="null" />.</exception>
		// Token: 0x0600054F RID: 1359 RVA: 0x0000E87E File Offset: 0x0000CA7E
		public void FillRegion(Brush brush, Region region)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFillRegion(this.nativeObject, brush.NativeBrush, region.NativeObject));
		}

		/// <summary>Forces execution of all pending graphics operations and returns immediately without waiting for the operations to finish.</summary>
		// Token: 0x06000550 RID: 1360 RVA: 0x0000E8B8 File Offset: 0x0000CAB8
		public void Flush()
		{
			this.Flush(FlushIntention.Flush);
		}

		/// <summary>Forces execution of all pending graphics operations with the method waiting or not waiting, as specified, to return before the operations finish.</summary>
		/// <param name="intention">Member of the <see cref="T:System.Drawing.Drawing2D.FlushIntention" /> enumeration that specifies whether the method returns immediately or waits for any existing operations to finish.</param>
		// Token: 0x06000551 RID: 1361 RVA: 0x0000E8C1 File Offset: 0x0000CAC1
		public void Flush(FlushIntention intention)
		{
			if (this.nativeObject == IntPtr.Zero)
			{
				return;
			}
			GDIPlus.CheckStatus(GDIPlus.GdipFlush(this.nativeObject, intention));
			if (this.maccontext != null)
			{
				this.maccontext.Synchronize();
			}
		}

		/// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> from the specified handle to a device context.</summary>
		/// <param name="hdc">Handle to a device context.</param>
		/// <returns>This method returns a new <see cref="T:System.Drawing.Graphics" /> for the specified device context.</returns>
		// Token: 0x06000552 RID: 1362 RVA: 0x0000E8FC File Offset: 0x0000CAFC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Graphics FromHdc(IntPtr hdc)
		{
			IntPtr nativeGraphics;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFromHDC(hdc, out nativeGraphics));
			return new Graphics(nativeGraphics);
		}

		/// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> from the specified handle to a device context and handle to a device.</summary>
		/// <param name="hdc">Handle to a device context.</param>
		/// <param name="hdevice">Handle to a device.</param>
		/// <returns>This method returns a new <see cref="T:System.Drawing.Graphics" /> for the specified device context and device.</returns>
		// Token: 0x06000553 RID: 1363 RVA: 0x00009B6A File Offset: 0x00007D6A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[MonoTODO]
		public static Graphics FromHdc(IntPtr hdc, IntPtr hdevice)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a <see cref="T:System.Drawing.Graphics" /> for the specified device context.</summary>
		/// <param name="hdc">Handle to a device context.</param>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> for the specified device context.</returns>
		// Token: 0x06000554 RID: 1364 RVA: 0x0000E91C File Offset: 0x0000CB1C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Graphics FromHdcInternal(IntPtr hdc)
		{
			GDIPlus.Display = hdc;
			return null;
		}

		/// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> from the specified handle to a window.</summary>
		/// <param name="hwnd">Handle to a window.</param>
		/// <returns>This method returns a new <see cref="T:System.Drawing.Graphics" /> for the specified window handle.</returns>
		// Token: 0x06000555 RID: 1365 RVA: 0x0000E928 File Offset: 0x0000CB28
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Graphics FromHwnd(IntPtr hwnd)
		{
			if (GDIPlus.UseCocoaDrawable)
			{
				if (hwnd == IntPtr.Zero)
				{
					throw new NotSupportedException("Opening display graphics is not supported");
				}
				CocoaContext cgcontextForNSView = MacSupport.GetCGContextForNSView(hwnd);
				IntPtr nativeGraphics;
				GDIPlus.GdipCreateFromContext_macosx(cgcontextForNSView.ctx, cgcontextForNSView.width, cgcontextForNSView.height, out nativeGraphics);
				return new Graphics(nativeGraphics)
				{
					maccontext = cgcontextForNSView
				};
			}
			else
			{
				IntPtr nativeGraphics;
				if (GDIPlus.UseCarbonDrawable)
				{
					CarbonContext cgcontextForView = MacSupport.GetCGContextForView(hwnd);
					GDIPlus.GdipCreateFromContext_macosx(cgcontextForView.ctx, cgcontextForView.width, cgcontextForView.height, out nativeGraphics);
					return new Graphics(nativeGraphics)
					{
						maccontext = cgcontextForView
					};
				}
				if (GDIPlus.UseX11Drawable)
				{
					if (GDIPlus.Display == IntPtr.Zero)
					{
						GDIPlus.Display = GDIPlus.XOpenDisplay(IntPtr.Zero);
						if (GDIPlus.Display == IntPtr.Zero)
						{
							throw new NotSupportedException("Could not open display (X-Server required. Check your DISPLAY environment variable)");
						}
					}
					if (hwnd == IntPtr.Zero)
					{
						hwnd = GDIPlus.XRootWindow(GDIPlus.Display, GDIPlus.XDefaultScreen(GDIPlus.Display));
					}
					return Graphics.FromXDrawable(hwnd, GDIPlus.Display);
				}
				GDIPlus.CheckStatus(GDIPlus.GdipCreateFromHWND(hwnd, out nativeGraphics));
				return new Graphics(nativeGraphics);
			}
		}

		/// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> for the specified windows handle.</summary>
		/// <param name="hwnd">Handle to a window.</param>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> for the specified window handle.</returns>
		// Token: 0x06000556 RID: 1366 RVA: 0x0000EA43 File Offset: 0x0000CC43
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Graphics FromHwndInternal(IntPtr hwnd)
		{
			return Graphics.FromHwnd(hwnd);
		}

		/// <summary>Creates a new <see cref="T:System.Drawing.Graphics" /> from the specified <see cref="T:System.Drawing.Image" />.</summary>
		/// <param name="image">
		///   <see cref="T:System.Drawing.Image" /> from which to create the new <see cref="T:System.Drawing.Graphics" />.</param>
		/// <returns>This method returns a new <see cref="T:System.Drawing.Graphics" /> for the specified <see cref="T:System.Drawing.Image" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Exception">
		///   <paramref name="image" /> has an indexed pixel format or its format is undefined.</exception>
		// Token: 0x06000557 RID: 1367 RVA: 0x0000EA4C File Offset: 0x0000CC4C
		public static Graphics FromImage(Image image)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			if ((image.PixelFormat & PixelFormat.Indexed) != PixelFormat.Undefined)
			{
				throw new Exception(Locale.GetText("Cannot create Graphics from an indexed bitmap."));
			}
			IntPtr nativeGraphics;
			GDIPlus.CheckStatus(GDIPlus.GdipGetImageGraphicsContext(image.nativeObject, out nativeGraphics));
			Graphics graphics = new Graphics(nativeGraphics, image);
			if (GDIPlus.RunningOnUnix())
			{
				Rectangle rectangle = new Rectangle(0, 0, image.Width, image.Height);
				GDIPlus.GdipSetVisibleClip_linux(graphics.NativeObject, ref rectangle);
			}
			return graphics;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0000EACC File Offset: 0x0000CCCC
		internal static Graphics FromXDrawable(IntPtr drawable, IntPtr display)
		{
			IntPtr nativeGraphics;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFromXDrawable_linux(drawable, display, out nativeGraphics));
			return new Graphics(nativeGraphics);
		}

		/// <summary>Gets a handle to the current Windows halftone palette.</summary>
		/// <returns>Internal pointer that specifies the handle to the palette.</returns>
		// Token: 0x06000559 RID: 1369 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO]
		public static IntPtr GetHalftonePalette()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the handle to the device context associated with this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>Handle to the device context associated with this <see cref="T:System.Drawing.Graphics" />.</returns>
		// Token: 0x0600055A RID: 1370 RVA: 0x0000EAED File Offset: 0x0000CCED
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public IntPtr GetHdc()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipGetDC(this.nativeObject, out this.deviceContextHdc));
			return this.deviceContextHdc;
		}

		/// <summary>Gets the nearest color to the specified <see cref="T:System.Drawing.Color" /> structure.</summary>
		/// <param name="color">
		///   <see cref="T:System.Drawing.Color" /> structure for which to find a match.</param>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the nearest color to the one specified with the <paramref name="color" /> parameter.</returns>
		// Token: 0x0600055B RID: 1371 RVA: 0x0000EB0C File Offset: 0x0000CD0C
		public Color GetNearestColor(Color color)
		{
			int argb;
			GDIPlus.CheckStatus(GDIPlus.GdipGetNearestColor(this.nativeObject, out argb));
			return Color.FromArgb(argb);
		}

		/// <summary>Updates the clip region of this <see cref="T:System.Drawing.Graphics" /> to the intersection of the current clip region and the specified <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">
		///   <see cref="T:System.Drawing.Region" /> to intersect with the current region.</param>
		// Token: 0x0600055C RID: 1372 RVA: 0x0000EB31 File Offset: 0x0000CD31
		public void IntersectClip(Region region)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRegion(this.nativeObject, region.NativeObject, CombineMode.Intersect));
		}

		/// <summary>Updates the clip region of this <see cref="T:System.Drawing.Graphics" /> to the intersection of the current clip region and the specified <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure to intersect with the current clip region.</param>
		// Token: 0x0600055D RID: 1373 RVA: 0x0000EB58 File Offset: 0x0000CD58
		public void IntersectClip(RectangleF rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRect(this.nativeObject, rect.X, rect.Y, rect.Width, rect.Height, CombineMode.Intersect));
		}

		/// <summary>Updates the clip region of this <see cref="T:System.Drawing.Graphics" /> to the intersection of the current clip region and the specified <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure to intersect with the current clip region.</param>
		// Token: 0x0600055E RID: 1374 RVA: 0x0000EB87 File Offset: 0x0000CD87
		public void IntersectClip(Rectangle rect)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRectI(this.nativeObject, rect.X, rect.Y, rect.Width, rect.Height, CombineMode.Intersect));
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Drawing.Point" /> structure is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="point">
		///   <see cref="T:System.Drawing.Point" /> structure to test for visibility.</param>
		/// <returns>
		///   <see langword="true" /> if the point specified by the <paramref name="point" /> parameter is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600055F RID: 1375 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
		public bool IsVisible(Point point)
		{
			bool result = false;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisiblePointI(this.nativeObject, point.X, point.Y, out result));
			return result;
		}

		/// <summary>Indicates whether the rectangle specified by a <see cref="T:System.Drawing.RectangleF" /> structure is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure to test for visibility.</param>
		/// <returns>
		///   <see langword="true" /> if the rectangle specified by the <paramref name="rect" /> parameter is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000560 RID: 1376 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
		public bool IsVisible(RectangleF rect)
		{
			bool result = false;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRect(this.nativeObject, rect.X, rect.Y, rect.Width, rect.Height, out result));
			return result;
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Drawing.PointF" /> structure is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="point">
		///   <see cref="T:System.Drawing.PointF" /> structure to test for visibility.</param>
		/// <returns>
		///   <see langword="true" /> if the point specified by the <paramref name="point" /> parameter is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000561 RID: 1377 RVA: 0x0000EC28 File Offset: 0x0000CE28
		public bool IsVisible(PointF point)
		{
			bool result = false;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisiblePoint(this.nativeObject, point.X, point.Y, out result));
			return result;
		}

		/// <summary>Indicates whether the rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure to test for visibility.</param>
		/// <returns>
		///   <see langword="true" /> if the rectangle specified by the <paramref name="rect" /> parameter is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000562 RID: 1378 RVA: 0x0000EC58 File Offset: 0x0000CE58
		public bool IsVisible(Rectangle rect)
		{
			bool result = false;
			GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleRectI(this.nativeObject, rect.X, rect.Y, rect.Width, rect.Height, out result));
			return result;
		}

		/// <summary>Indicates whether the point specified by a pair of coordinates is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="x">The x-coordinate of the point to test for visibility.</param>
		/// <param name="y">The y-coordinate of the point to test for visibility.</param>
		/// <returns>
		///   <see langword="true" /> if the point defined by the <paramref name="x" /> and <paramref name="y" /> parameters is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000563 RID: 1379 RVA: 0x0000EC96 File Offset: 0x0000CE96
		public bool IsVisible(float x, float y)
		{
			return this.IsVisible(new PointF(x, y));
		}

		/// <summary>Indicates whether the point specified by a pair of coordinates is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="x">The x-coordinate of the point to test for visibility.</param>
		/// <param name="y">The y-coordinate of the point to test for visibility.</param>
		/// <returns>
		///   <see langword="true" /> if the point defined by the <paramref name="x" /> and <paramref name="y" /> parameters is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000564 RID: 1380 RVA: 0x0000ECA5 File Offset: 0x0000CEA5
		public bool IsVisible(int x, int y)
		{
			return this.IsVisible(new Point(x, y));
		}

		/// <summary>Indicates whether the rectangle specified by a pair of coordinates, a width, and a height is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test for visibility.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test for visibility.</param>
		/// <param name="width">Width of the rectangle to test for visibility.</param>
		/// <param name="height">Height of the rectangle to test for visibility.</param>
		/// <returns>
		///   <see langword="true" /> if the rectangle defined by the <paramref name="x" />, <paramref name="y" />, <paramref name="width" />, and <paramref name="height" /> parameters is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000565 RID: 1381 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		public bool IsVisible(float x, float y, float width, float height)
		{
			return this.IsVisible(new RectangleF(x, y, width, height));
		}

		/// <summary>Indicates whether the rectangle specified by a pair of coordinates, a width, and a height is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="x">The x-coordinate of the upper-left corner of the rectangle to test for visibility.</param>
		/// <param name="y">The y-coordinate of the upper-left corner of the rectangle to test for visibility.</param>
		/// <param name="width">Width of the rectangle to test for visibility.</param>
		/// <param name="height">Height of the rectangle to test for visibility.</param>
		/// <returns>
		///   <see langword="true" /> if the rectangle defined by the <paramref name="x" />, <paramref name="y" />, <paramref name="width" />, and <paramref name="height" /> parameters is contained within the visible clip region of this <see cref="T:System.Drawing.Graphics" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000566 RID: 1382 RVA: 0x0000ECC6 File Offset: 0x0000CEC6
		public bool IsVisible(int x, int y, int width, int height)
		{
			return this.IsVisible(new Rectangle(x, y, width, height));
		}

		/// <summary>Gets an array of <see cref="T:System.Drawing.Region" /> objects, each of which bounds a range of character positions within the specified string.</summary>
		/// <param name="text">String to measure.</param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
		/// <param name="layoutRect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that specifies the layout rectangle for the string.</param>
		/// <param name="stringFormat">
		///   <see cref="T:System.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the string.</param>
		/// <returns>This method returns an array of <see cref="T:System.Drawing.Region" /> objects, each of which bounds a range of character positions within the specified string.</returns>
		// Token: 0x06000567 RID: 1383 RVA: 0x0000ECD8 File Offset: 0x0000CED8
		public Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat)
		{
			if (text == null || text.Length == 0)
			{
				return new Region[0];
			}
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			if (stringFormat == null)
			{
				throw new ArgumentException("stringFormat");
			}
			int measurableCharacterRangeCount = stringFormat.GetMeasurableCharacterRangeCount();
			if (measurableCharacterRangeCount == 0)
			{
				return new Region[0];
			}
			IntPtr[] array = new IntPtr[measurableCharacterRangeCount];
			Region[] array2 = new Region[measurableCharacterRangeCount];
			for (int i = 0; i < measurableCharacterRangeCount; i++)
			{
				array2[i] = new Region();
				array[i] = array2[i].NativeObject;
			}
			GDIPlus.CheckStatus(GDIPlus.GdipMeasureCharacterRanges(this.nativeObject, text, text.Length, font.NativeObject, ref layoutRect, stringFormat.NativeObject, measurableCharacterRangeCount, out array[0]));
			return array2;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0000ED84 File Offset: 0x0000CF84
		private SizeF GdipMeasureString(IntPtr graphics, string text, Font font, ref RectangleF layoutRect, IntPtr stringFormat)
		{
			if (text == null || text.Length == 0)
			{
				return SizeF.Empty;
			}
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			RectangleF rectangleF = default(RectangleF);
			GDIPlus.CheckStatus(GDIPlus.GdipMeasureString(this.nativeObject, text, text.Length, font.NativeObject, ref layoutRect, stringFormat, out rectangleF, null, null));
			return new SizeF(rectangleF.Width, rectangleF.Height);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" />.</summary>
		/// <param name="text">String to measure.</param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified by the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is <see langword="null" />.</exception>
		// Token: 0x06000569 RID: 1385 RVA: 0x0000EDF2 File Offset: 0x0000CFF2
		public SizeF MeasureString(string text, Font font)
		{
			return this.MeasureString(text, font, SizeF.Empty);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> within the specified layout area.</summary>
		/// <param name="text">String to measure.</param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> defines the text format of the string.</param>
		/// <param name="layoutArea">
		///   <see cref="T:System.Drawing.SizeF" /> structure that specifies the maximum layout area for the text.</param>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified by the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is <see langword="null" />.</exception>
		// Token: 0x0600056A RID: 1386 RVA: 0x0000EE04 File Offset: 0x0000D004
		public SizeF MeasureString(string text, Font font, SizeF layoutArea)
		{
			RectangleF rectangleF = new RectangleF(0f, 0f, layoutArea.Width, layoutArea.Height);
			return this.GdipMeasureString(this.nativeObject, text, font, ref rectangleF, IntPtr.Zero);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" />.</summary>
		/// <param name="text">String to measure.</param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the format of the string.</param>
		/// <param name="width">Maximum width of the string in pixels.</param>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified in the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is <see langword="null" />.</exception>
		// Token: 0x0600056B RID: 1387 RVA: 0x0000EE48 File Offset: 0x0000D048
		public SizeF MeasureString(string text, Font font, int width)
		{
			RectangleF rectangleF = new RectangleF(0f, 0f, (float)width, 2.1474836E+09f);
			return this.GdipMeasureString(this.nativeObject, text, font, ref rectangleF, IntPtr.Zero);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> and formatted with the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <param name="text">String to measure.</param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> defines the text format of the string.</param>
		/// <param name="layoutArea">
		///   <see cref="T:System.Drawing.SizeF" /> structure that specifies the maximum layout area for the text.</param>
		/// <param name="stringFormat">
		///   <see cref="T:System.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the string.</param>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified in the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the <paramref name="stringFormat" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is <see langword="null" />.</exception>
		// Token: 0x0600056C RID: 1388 RVA: 0x0000EE84 File Offset: 0x0000D084
		public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat)
		{
			RectangleF rectangleF = new RectangleF(0f, 0f, layoutArea.Width, layoutArea.Height);
			IntPtr stringFormat2 = (stringFormat == null) ? IntPtr.Zero : stringFormat.NativeObject;
			return this.GdipMeasureString(this.nativeObject, text, font, ref rectangleF, stringFormat2);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> and formatted with the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <param name="text">String to measure.</param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
		/// <param name="width">Maximum width of the string.</param>
		/// <param name="format">
		///   <see cref="T:System.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the string.</param>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified in the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the <paramref name="stringFormat" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is <see langword="null" />.</exception>
		// Token: 0x0600056D RID: 1389 RVA: 0x0000EED4 File Offset: 0x0000D0D4
		public SizeF MeasureString(string text, Font font, int width, StringFormat format)
		{
			RectangleF rectangleF = new RectangleF(0f, 0f, (float)width, 2.1474836E+09f);
			IntPtr stringFormat = (format == null) ? IntPtr.Zero : format.NativeObject;
			return this.GdipMeasureString(this.nativeObject, text, font, ref rectangleF, stringFormat);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> and formatted with the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <param name="text">String to measure.</param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> defines the text format of the string.</param>
		/// <param name="origin">
		///   <see cref="T:System.Drawing.PointF" /> structure that represents the upper-left corner of the string.</param>
		/// <param name="stringFormat">
		///   <see cref="T:System.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the string.</param>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the string specified by the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the <paramref name="stringFormat" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is <see langword="null" />.</exception>
		// Token: 0x0600056E RID: 1390 RVA: 0x0000EF20 File Offset: 0x0000D120
		public SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat)
		{
			RectangleF rectangleF = new RectangleF(origin.X, origin.Y, 0f, 0f);
			IntPtr stringFormat2 = (stringFormat == null) ? IntPtr.Zero : stringFormat.NativeObject;
			return this.GdipMeasureString(this.nativeObject, text, font, ref rectangleF, stringFormat2);
		}

		/// <summary>Measures the specified string when drawn with the specified <see cref="T:System.Drawing.Font" /> and formatted with the specified <see cref="T:System.Drawing.StringFormat" />.</summary>
		/// <param name="text">String to measure.</param>
		/// <param name="font">
		///   <see cref="T:System.Drawing.Font" /> that defines the text format of the string.</param>
		/// <param name="layoutArea">
		///   <see cref="T:System.Drawing.SizeF" /> structure that specifies the maximum layout area for the text.</param>
		/// <param name="stringFormat">
		///   <see cref="T:System.Drawing.StringFormat" /> that represents formatting information, such as line spacing, for the string.</param>
		/// <param name="charactersFitted">Number of characters in the string.</param>
		/// <param name="linesFilled">Number of text lines in the string.</param>
		/// <returns>This method returns a <see cref="T:System.Drawing.SizeF" /> structure that represents the size of the string, in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property, of the <paramref name="text" /> parameter as drawn with the <paramref name="font" /> parameter and the <paramref name="stringFormat" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="font" /> is <see langword="null" />.</exception>
		// Token: 0x0600056F RID: 1391 RVA: 0x0000EF70 File Offset: 0x0000D170
		public unsafe SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat, out int charactersFitted, out int linesFilled)
		{
			charactersFitted = 0;
			linesFilled = 0;
			if (text == null || text.Length == 0)
			{
				return SizeF.Empty;
			}
			if (font == null)
			{
				throw new ArgumentNullException("font");
			}
			RectangleF rectangleF = default(RectangleF);
			RectangleF rectangleF2 = new RectangleF(0f, 0f, layoutArea.Width, layoutArea.Height);
			IntPtr stringFormat2 = (stringFormat == null) ? IntPtr.Zero : stringFormat.NativeObject;
			fixed (int* ptr = &charactersFitted)
			{
				int* codepointsFitted = ptr;
				fixed (int* ptr2 = &linesFilled)
				{
					int* linesFilled2 = ptr2;
					GDIPlus.CheckStatus(GDIPlus.GdipMeasureString(this.nativeObject, text, text.Length, font.NativeObject, ref rectangleF2, stringFormat2, out rectangleF, codepointsFitted, linesFilled2));
					ptr = null;
				}
				return new SizeF(rectangleF.Width, rectangleF.Height);
			}
		}

		/// <summary>Multiplies the world transformation of this <see cref="T:System.Drawing.Graphics" /> and specified the <see cref="T:System.Drawing.Drawing2D.Matrix" />.</summary>
		/// <param name="matrix">4x4 <see cref="T:System.Drawing.Drawing2D.Matrix" /> that multiplies the world transformation.</param>
		// Token: 0x06000570 RID: 1392 RVA: 0x0000F02F File Offset: 0x0000D22F
		public void MultiplyTransform(Matrix matrix)
		{
			this.MultiplyTransform(matrix, MatrixOrder.Prepend);
		}

		/// <summary>Multiplies the world transformation of this <see cref="T:System.Drawing.Graphics" /> and specified the <see cref="T:System.Drawing.Drawing2D.Matrix" /> in the specified order.</summary>
		/// <param name="matrix">4x4 <see cref="T:System.Drawing.Drawing2D.Matrix" /> that multiplies the world transformation.</param>
		/// <param name="order">Member of the <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> enumeration that determines the order of the multiplication.</param>
		// Token: 0x06000571 RID: 1393 RVA: 0x0000F039 File Offset: 0x0000D239
		public void MultiplyTransform(Matrix matrix, MatrixOrder order)
		{
			if (matrix == null)
			{
				throw new ArgumentNullException("matrix");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipMultiplyWorldTransform(this.nativeObject, matrix.nativeMatrix, order));
		}

		/// <summary>Releases a device context handle obtained by a previous call to the <see cref="M:System.Drawing.Graphics.GetHdc" /> method of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="hdc">Handle to a device context obtained by a previous call to the <see cref="M:System.Drawing.Graphics.GetHdc" /> method of this <see cref="T:System.Drawing.Graphics" />.</param>
		// Token: 0x06000572 RID: 1394 RVA: 0x0000F060 File Offset: 0x0000D260
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void ReleaseHdc(IntPtr hdc)
		{
			this.ReleaseHdcInternal(hdc);
		}

		/// <summary>Releases a device context handle obtained by a previous call to the <see cref="M:System.Drawing.Graphics.GetHdc" /> method of this <see cref="T:System.Drawing.Graphics" />.</summary>
		// Token: 0x06000573 RID: 1395 RVA: 0x0000F069 File Offset: 0x0000D269
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public void ReleaseHdc()
		{
			this.ReleaseHdcInternal(this.deviceContextHdc);
		}

		/// <summary>Releases a handle to a device context.</summary>
		/// <param name="hdc">Handle to a device context.</param>
		// Token: 0x06000574 RID: 1396 RVA: 0x0000F078 File Offset: 0x0000D278
		[MonoLimitation("Can only be used when hdc was provided by Graphics.GetHdc() method")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void ReleaseHdcInternal(IntPtr hdc)
		{
			Status status = Status.InvalidParameter;
			if (hdc == this.deviceContextHdc)
			{
				status = GDIPlus.GdipReleaseDC(this.nativeObject, this.deviceContextHdc);
				this.deviceContextHdc = IntPtr.Zero;
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Resets the clip region of this <see cref="T:System.Drawing.Graphics" /> to an infinite region.</summary>
		// Token: 0x06000575 RID: 1397 RVA: 0x0000F0B8 File Offset: 0x0000D2B8
		public void ResetClip()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipResetClip(this.nativeObject));
		}

		/// <summary>Resets the world transformation matrix of this <see cref="T:System.Drawing.Graphics" /> to the identity matrix.</summary>
		// Token: 0x06000576 RID: 1398 RVA: 0x0000F0CA File Offset: 0x0000D2CA
		public void ResetTransform()
		{
			GDIPlus.CheckStatus(GDIPlus.GdipResetWorldTransform(this.nativeObject));
		}

		/// <summary>Restores the state of this <see cref="T:System.Drawing.Graphics" /> to the state represented by a <see cref="T:System.Drawing.Drawing2D.GraphicsState" />.</summary>
		/// <param name="gstate">
		///   <see cref="T:System.Drawing.Drawing2D.GraphicsState" /> that represents the state to which to restore this <see cref="T:System.Drawing.Graphics" />.</param>
		// Token: 0x06000577 RID: 1399 RVA: 0x0000F0DC File Offset: 0x0000D2DC
		public void Restore(GraphicsState gstate)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRestoreGraphics(this.nativeObject, (uint)gstate.nativeState));
		}

		/// <summary>Applies the specified rotation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="angle">Angle of rotation in degrees.</param>
		// Token: 0x06000578 RID: 1400 RVA: 0x0000F0F4 File Offset: 0x0000D2F4
		public void RotateTransform(float angle)
		{
			this.RotateTransform(angle, MatrixOrder.Prepend);
		}

		/// <summary>Applies the specified rotation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" /> in the specified order.</summary>
		/// <param name="angle">Angle of rotation in degrees.</param>
		/// <param name="order">Member of the <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies whether the rotation is appended or prepended to the matrix transformation.</param>
		// Token: 0x06000579 RID: 1401 RVA: 0x0000F0FE File Offset: 0x0000D2FE
		public void RotateTransform(float angle, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRotateWorldTransform(this.nativeObject, angle, order));
		}

		/// <summary>Saves the current state of this <see cref="T:System.Drawing.Graphics" /> and identifies the saved state with a <see cref="T:System.Drawing.Drawing2D.GraphicsState" />.</summary>
		/// <returns>This method returns a <see cref="T:System.Drawing.Drawing2D.GraphicsState" /> that represents the saved state of this <see cref="T:System.Drawing.Graphics" />.</returns>
		// Token: 0x0600057A RID: 1402 RVA: 0x0000F114 File Offset: 0x0000D314
		public GraphicsState Save()
		{
			uint nativeState;
			GDIPlus.CheckStatus(GDIPlus.GdipSaveGraphics(this.nativeObject, out nativeState));
			return new GraphicsState((int)nativeState);
		}

		/// <summary>Applies the specified scaling operation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" /> by prepending it to the object's transformation matrix.</summary>
		/// <param name="sx">Scale factor in the x direction.</param>
		/// <param name="sy">Scale factor in the y direction.</param>
		// Token: 0x0600057B RID: 1403 RVA: 0x0000F139 File Offset: 0x0000D339
		public void ScaleTransform(float sx, float sy)
		{
			this.ScaleTransform(sx, sy, MatrixOrder.Prepend);
		}

		/// <summary>Applies the specified scaling operation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" /> in the specified order.</summary>
		/// <param name="sx">Scale factor in the x direction.</param>
		/// <param name="sy">Scale factor in the y direction.</param>
		/// <param name="order">Member of the <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies whether the scaling operation is prepended or appended to the transformation matrix.</param>
		// Token: 0x0600057C RID: 1404 RVA: 0x0000F144 File Offset: 0x0000D344
		public void ScaleTransform(float sx, float sy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipScaleWorldTransform(this.nativeObject, sx, sy, order));
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the rectangle specified by a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure that represents the new clip region.</param>
		// Token: 0x0600057D RID: 1405 RVA: 0x0000F159 File Offset: 0x0000D359
		public void SetClip(RectangleF rect)
		{
			this.SetClip(rect, CombineMode.Replace);
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="path">
		///   <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> that represents the new clip region.</param>
		// Token: 0x0600057E RID: 1406 RVA: 0x0000F163 File Offset: 0x0000D363
		public void SetClip(GraphicsPath path)
		{
			this.SetClip(path, CombineMode.Replace);
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure that represents the new clip region.</param>
		// Token: 0x0600057F RID: 1407 RVA: 0x0000F16D File Offset: 0x0000D36D
		public void SetClip(Rectangle rect)
		{
			this.SetClip(rect, CombineMode.Replace);
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the <see langword="Clip" /> property of the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="g">
		///   <see cref="T:System.Drawing.Graphics" /> from which to take the new clip region.</param>
		// Token: 0x06000580 RID: 1408 RVA: 0x0000F177 File Offset: 0x0000D377
		public void SetClip(Graphics g)
		{
			this.SetClip(g, CombineMode.Replace);
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the result of the specified combining operation of the current clip region and the <see cref="P:System.Drawing.Graphics.Clip" /> property of the specified <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="g">
		///   <see cref="T:System.Drawing.Graphics" /> that specifies the clip region to combine.</param>
		/// <param name="combineMode">Member of the <see cref="T:System.Drawing.Drawing2D.CombineMode" /> enumeration that specifies the combining operation to use.</param>
		// Token: 0x06000581 RID: 1409 RVA: 0x0000F181 File Offset: 0x0000D381
		public void SetClip(Graphics g, CombineMode combineMode)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipGraphics(this.nativeObject, g.NativeObject, combineMode));
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the result of the specified operation combining the current clip region and the rectangle specified by a <see cref="T:System.Drawing.Rectangle" /> structure.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.Rectangle" /> structure to combine.</param>
		/// <param name="combineMode">Member of the <see cref="T:System.Drawing.Drawing2D.CombineMode" /> enumeration that specifies the combining operation to use.</param>
		// Token: 0x06000582 RID: 1410 RVA: 0x0000F1A8 File Offset: 0x0000D3A8
		public void SetClip(Rectangle rect, CombineMode combineMode)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRectI(this.nativeObject, rect.X, rect.Y, rect.Width, rect.Height, combineMode));
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the result of the specified operation combining the current clip region and the rectangle specified by a <see cref="T:System.Drawing.RectangleF" /> structure.</summary>
		/// <param name="rect">
		///   <see cref="T:System.Drawing.RectangleF" /> structure to combine.</param>
		/// <param name="combineMode">Member of the <see cref="T:System.Drawing.Drawing2D.CombineMode" /> enumeration that specifies the combining operation to use.</param>
		// Token: 0x06000583 RID: 1411 RVA: 0x0000F1D7 File Offset: 0x0000D3D7
		public void SetClip(RectangleF rect, CombineMode combineMode)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRect(this.nativeObject, rect.X, rect.Y, rect.Width, rect.Height, combineMode));
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the result of the specified operation combining the current clip region and the specified <see cref="T:System.Drawing.Region" />.</summary>
		/// <param name="region">
		///   <see cref="T:System.Drawing.Region" /> to combine.</param>
		/// <param name="combineMode">Member from the <see cref="T:System.Drawing.Drawing2D.CombineMode" /> enumeration that specifies the combining operation to use.</param>
		// Token: 0x06000584 RID: 1412 RVA: 0x0000F206 File Offset: 0x0000D406
		public void SetClip(Region region, CombineMode combineMode)
		{
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipRegion(this.nativeObject, region.NativeObject, combineMode));
		}

		/// <summary>Sets the clipping region of this <see cref="T:System.Drawing.Graphics" /> to the result of the specified operation combining the current clip region and the specified <see cref="T:System.Drawing.Drawing2D.GraphicsPath" />.</summary>
		/// <param name="path">
		///   <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> to combine.</param>
		/// <param name="combineMode">Member of the <see cref="T:System.Drawing.Drawing2D.CombineMode" /> enumeration that specifies the combining operation to use.</param>
		// Token: 0x06000585 RID: 1413 RVA: 0x0000F22D File Offset: 0x0000D42D
		public void SetClip(GraphicsPath path, CombineMode combineMode)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipSetClipPath(this.nativeObject, path.nativePath, combineMode));
		}

		/// <summary>Transforms an array of points from one coordinate space to another using the current world and page transformations of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="destSpace">Member of the <see cref="T:System.Drawing.Drawing2D.CoordinateSpace" /> enumeration that specifies the destination coordinate space.</param>
		/// <param name="srcSpace">Member of the <see cref="T:System.Drawing.Drawing2D.CoordinateSpace" /> enumeration that specifies the source coordinate space.</param>
		/// <param name="pts">Array of <see cref="T:System.Drawing.PointF" /> structures that represent the points to transform.</param>
		// Token: 0x06000586 RID: 1414 RVA: 0x0000F254 File Offset: 0x0000D454
		public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, PointF[] pts)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			IntPtr intPtr = GDIPlus.FromPointToUnManagedMemory(pts);
			GDIPlus.CheckStatus(GDIPlus.GdipTransformPoints(this.nativeObject, destSpace, srcSpace, intPtr, pts.Length));
			GDIPlus.FromUnManagedMemoryToPoint(intPtr, pts);
		}

		/// <summary>Transforms an array of points from one coordinate space to another using the current world and page transformations of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="destSpace">Member of the <see cref="T:System.Drawing.Drawing2D.CoordinateSpace" /> enumeration that specifies the destination coordinate space.</param>
		/// <param name="srcSpace">Member of the <see cref="T:System.Drawing.Drawing2D.CoordinateSpace" /> enumeration that specifies the source coordinate space.</param>
		/// <param name="pts">Array of <see cref="T:System.Drawing.Point" /> structures that represents the points to transformation.</param>
		// Token: 0x06000587 RID: 1415 RVA: 0x0000F294 File Offset: 0x0000D494
		public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, Point[] pts)
		{
			if (pts == null)
			{
				throw new ArgumentNullException("pts");
			}
			IntPtr intPtr = GDIPlus.FromPointToUnManagedMemoryI(pts);
			GDIPlus.CheckStatus(GDIPlus.GdipTransformPointsI(this.nativeObject, destSpace, srcSpace, intPtr, pts.Length));
			GDIPlus.FromUnManagedMemoryToPointI(intPtr, pts);
		}

		/// <summary>Translates the clipping region of this <see cref="T:System.Drawing.Graphics" /> by specified amounts in the horizontal and vertical directions.</summary>
		/// <param name="dx">The x-coordinate of the translation.</param>
		/// <param name="dy">The y-coordinate of the translation.</param>
		// Token: 0x06000588 RID: 1416 RVA: 0x0000F2D3 File Offset: 0x0000D4D3
		public void TranslateClip(int dx, int dy)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslateClipI(this.nativeObject, dx, dy));
		}

		/// <summary>Translates the clipping region of this <see cref="T:System.Drawing.Graphics" /> by specified amounts in the horizontal and vertical directions.</summary>
		/// <param name="dx">The x-coordinate of the translation.</param>
		/// <param name="dy">The y-coordinate of the translation.</param>
		// Token: 0x06000589 RID: 1417 RVA: 0x0000F2E7 File Offset: 0x0000D4E7
		public void TranslateClip(float dx, float dy)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslateClip(this.nativeObject, dx, dy));
		}

		/// <summary>Changes the origin of the coordinate system by prepending the specified translation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <param name="dx">The x-coordinate of the translation.</param>
		/// <param name="dy">The y-coordinate of the translation.</param>
		// Token: 0x0600058A RID: 1418 RVA: 0x0000F2FB File Offset: 0x0000D4FB
		public void TranslateTransform(float dx, float dy)
		{
			this.TranslateTransform(dx, dy, MatrixOrder.Prepend);
		}

		/// <summary>Changes the origin of the coordinate system by applying the specified translation to the transformation matrix of this <see cref="T:System.Drawing.Graphics" /> in the specified order.</summary>
		/// <param name="dx">The x-coordinate of the translation.</param>
		/// <param name="dy">The y-coordinate of the translation.</param>
		/// <param name="order">Member of the <see cref="T:System.Drawing.Drawing2D.MatrixOrder" /> enumeration that specifies whether the translation is prepended or appended to the transformation matrix.</param>
		// Token: 0x0600058B RID: 1419 RVA: 0x0000F306 File Offset: 0x0000D506
		public void TranslateTransform(float dx, float dy, MatrixOrder order)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipTranslateWorldTransform(this.nativeObject, dx, dy, order));
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.Region" /> that limits the drawing region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Region" /> that limits the portion of this <see cref="T:System.Drawing.Graphics" /> that is currently available for drawing.</returns>
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0000F31C File Offset: 0x0000D51C
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x0000F346 File Offset: 0x0000D546
		public Region Clip
		{
			get
			{
				Region region = new Region();
				GDIPlus.CheckStatus(GDIPlus.GdipGetClip(this.nativeObject, region.NativeObject));
				return region;
			}
			set
			{
				this.SetClip(value, CombineMode.Replace);
			}
		}

		/// <summary>Gets a <see cref="T:System.Drawing.RectangleF" /> structure that bounds the clipping region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> structure that represents a bounding rectangle for the clipping region of this <see cref="T:System.Drawing.Graphics" />.</returns>
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0000F350 File Offset: 0x0000D550
		public RectangleF ClipBounds
		{
			get
			{
				RectangleF result = default(RectangleF);
				GDIPlus.CheckStatus(GDIPlus.GdipGetClipBounds(this.nativeObject, out result));
				return result;
			}
		}

		/// <summary>Gets a value that specifies how composited images are drawn to this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>This property specifies a member of the <see cref="T:System.Drawing.Drawing2D.CompositingMode" /> enumeration. The default is <see cref="F:System.Drawing.Drawing2D.CompositingMode.SourceOver" />.</returns>
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0000F378 File Offset: 0x0000D578
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x0000F398 File Offset: 0x0000D598
		public CompositingMode CompositingMode
		{
			get
			{
				CompositingMode result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetCompositingMode(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetCompositingMode(this.nativeObject, value));
			}
		}

		/// <summary>Gets or sets the rendering quality of composited images drawn to this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>This property specifies a member of the <see cref="T:System.Drawing.Drawing2D.CompositingQuality" /> enumeration. The default is <see cref="F:System.Drawing.Drawing2D.CompositingQuality.Default" />.</returns>
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0000F3AC File Offset: 0x0000D5AC
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x0000F3CC File Offset: 0x0000D5CC
		public CompositingQuality CompositingQuality
		{
			get
			{
				CompositingQuality result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetCompositingQuality(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetCompositingQuality(this.nativeObject, value));
			}
		}

		/// <summary>Gets the horizontal resolution of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>The value, in dots per inch, for the horizontal resolution supported by this <see cref="T:System.Drawing.Graphics" />.</returns>
		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
		public float DpiX
		{
			get
			{
				float result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetDpiX(this.nativeObject, out result));
				return result;
			}
		}

		/// <summary>Gets the vertical resolution of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>The value, in dots per inch, for the vertical resolution supported by this <see cref="T:System.Drawing.Graphics" />.</returns>
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000F400 File Offset: 0x0000D600
		public float DpiY
		{
			get
			{
				float result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetDpiY(this.nativeObject, out result));
				return result;
			}
		}

		/// <summary>Gets or sets the interpolation mode associated with this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Drawing2D.InterpolationMode" /> values.</returns>
		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0000F420 File Offset: 0x0000D620
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x0000F442 File Offset: 0x0000D642
		public InterpolationMode InterpolationMode
		{
			get
			{
				InterpolationMode result = InterpolationMode.Invalid;
				GDIPlus.CheckStatus(GDIPlus.GdipGetInterpolationMode(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetInterpolationMode(this.nativeObject, value));
			}
		}

		/// <summary>Gets a value indicating whether the clipping region of this <see cref="T:System.Drawing.Graphics" /> is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if the clipping region of this <see cref="T:System.Drawing.Graphics" /> is empty; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000F458 File Offset: 0x0000D658
		public bool IsClipEmpty
		{
			get
			{
				bool result = false;
				GDIPlus.CheckStatus(GDIPlus.GdipIsClipEmpty(this.nativeObject, out result));
				return result;
			}
		}

		/// <summary>Gets a value indicating whether the visible clipping region of this <see cref="T:System.Drawing.Graphics" /> is empty.</summary>
		/// <returns>
		///   <see langword="true" /> if the visible portion of the clipping region of this <see cref="T:System.Drawing.Graphics" /> is empty; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0000F47C File Offset: 0x0000D67C
		public bool IsVisibleClipEmpty
		{
			get
			{
				bool result = false;
				GDIPlus.CheckStatus(GDIPlus.GdipIsVisibleClipEmpty(this.nativeObject, out result));
				return result;
			}
		}

		/// <summary>Gets or sets the scaling between world units and page units for this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>This property specifies a value for the scaling between world units and page units for this <see cref="T:System.Drawing.Graphics" />.</returns>
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x0000F4C0 File Offset: 0x0000D6C0
		public float PageScale
		{
			get
			{
				float result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPageScale(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetPageScale(this.nativeObject, value));
			}
		}

		/// <summary>Gets or sets the unit of measure used for page coordinates in this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.GraphicsUnit" /> values other than <see cref="F:System.Drawing.GraphicsUnit.World" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <see cref="P:System.Drawing.Graphics.PageUnit" /> is set to <see cref="F:System.Drawing.GraphicsUnit.World" />, which is not a physical unit.</exception>
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000F4D4 File Offset: 0x0000D6D4
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
		public GraphicsUnit PageUnit
		{
			get
			{
				GraphicsUnit result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPageUnit(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetPageUnit(this.nativeObject, value));
			}
		}

		/// <summary>Gets or sets a value specifying how pixels are offset during rendering of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>This property specifies a member of the <see cref="T:System.Drawing.Drawing2D.PixelOffsetMode" /> enumeration</returns>
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0000F508 File Offset: 0x0000D708
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x0000F52A File Offset: 0x0000D72A
		[MonoTODO("This property does not do anything when used with libgdiplus.")]
		public PixelOffsetMode PixelOffsetMode
		{
			get
			{
				PixelOffsetMode result = PixelOffsetMode.Invalid;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPixelOffsetMode(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetPixelOffsetMode(this.nativeObject, value));
			}
		}

		/// <summary>Gets or sets the rendering origin of this <see cref="T:System.Drawing.Graphics" /> for dithering and for hatch brushes.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> structure that represents the dither origin for 8-bits-per-pixel and 16-bits-per-pixel dithering and is also used to set the origin for hatch brushes.</returns>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0000F540 File Offset: 0x0000D740
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0000F568 File Offset: 0x0000D768
		public Point RenderingOrigin
		{
			get
			{
				int x;
				int y;
				GDIPlus.CheckStatus(GDIPlus.GdipGetRenderingOrigin(this.nativeObject, out x, out y));
				return new Point(x, y);
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetRenderingOrigin(this.nativeObject, value.X, value.Y));
			}
		}

		/// <summary>Gets or sets the rendering quality for this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Drawing2D.SmoothingMode" /> values.</returns>
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0000F588 File Offset: 0x0000D788
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x0000F5AA File Offset: 0x0000D7AA
		public SmoothingMode SmoothingMode
		{
			get
			{
				SmoothingMode result = SmoothingMode.Invalid;
				GDIPlus.CheckStatus(GDIPlus.GdipGetSmoothingMode(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetSmoothingMode(this.nativeObject, value));
			}
		}

		/// <summary>Gets or sets the gamma correction value for rendering text.</summary>
		/// <returns>The gamma correction value used for rendering antialiased and ClearType text.</returns>
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x0000F5E0 File Offset: 0x0000D7E0
		[MonoTODO("This property does not do anything when used with libgdiplus.")]
		public int TextContrast
		{
			get
			{
				int result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetTextContrast(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetTextContrast(this.nativeObject, value));
			}
		}

		/// <summary>Gets or sets the rendering mode for text associated with this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.Text.TextRenderingHint" /> values.</returns>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0000F5F4 File Offset: 0x0000D7F4
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x0000F614 File Offset: 0x0000D814
		public TextRenderingHint TextRenderingHint
		{
			get
			{
				TextRenderingHint result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetTextRenderingHint(this.nativeObject, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetTextRenderingHint(this.nativeObject, value));
			}
		}

		/// <summary>Gets or sets a copy of the geometric world transformation for this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>A copy of the <see cref="T:System.Drawing.Drawing2D.Matrix" /> that represents the geometric world transformation for this <see cref="T:System.Drawing.Graphics" />.</returns>
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000F628 File Offset: 0x0000D828
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x0000F652 File Offset: 0x0000D852
		public Matrix Transform
		{
			get
			{
				Matrix matrix = new Matrix();
				GDIPlus.CheckStatus(GDIPlus.GdipGetWorldTransform(this.nativeObject, matrix.nativeMatrix));
				return matrix;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetWorldTransform(this.nativeObject, value.nativeMatrix));
			}
		}

		/// <summary>Gets the bounding rectangle of the visible clipping region of this <see cref="T:System.Drawing.Graphics" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.RectangleF" /> structure that represents a bounding rectangle for the visible clipping region of this <see cref="T:System.Drawing.Graphics" />.</returns>
		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0000F678 File Offset: 0x0000D878
		public RectangleF VisibleClipBounds
		{
			get
			{
				RectangleF result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetVisibleClipBounds(this.nativeObject, out result));
				return result;
			}
		}

		/// <summary>Gets the cumulative graphics context.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the cumulative graphics context.</returns>
		// Token: 0x060005AA RID: 1450 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public object GetContextInfo()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00005B7D File Offset: 0x00003D7D
		internal Graphics()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000494 RID: 1172
		internal IntPtr nativeObject;

		// Token: 0x04000495 RID: 1173
		internal IMacContext maccontext;

		// Token: 0x04000496 RID: 1174
		private bool disposed;

		// Token: 0x04000497 RID: 1175
		private static float defDpiX;

		// Token: 0x04000498 RID: 1176
		private static float defDpiY;

		// Token: 0x04000499 RID: 1177
		private IntPtr deviceContextHdc;

		// Token: 0x0400049A RID: 1178
		private Metafile.MetafileHolder _metafileHolder;

		// Token: 0x0400049B RID: 1179
		private const string MetafileEnumeration = "Metafiles enumeration, for both WMF and EMF formats, isn't supported.";

		/// <summary>Provides a callback method for the <see cref="Overload:System.Drawing.Graphics.EnumerateMetafile" /> method.</summary>
		/// <param name="recordType">Member of the <see cref="T:System.Drawing.Imaging.EmfPlusRecordType" /> enumeration that specifies the type of metafile record.</param>
		/// <param name="flags">Set of flags that specify attributes of the record.</param>
		/// <param name="dataSize">Number of bytes in the record data.</param>
		/// <param name="data">Pointer to a buffer that contains the record data.</param>
		/// <param name="callbackData">Not used.</param>
		/// <returns>Return <see langword="true" /> if you want to continue enumerating records; otherwise, <see langword="false" />.</returns>
		// Token: 0x02000072 RID: 114
		// (Invoke) Token: 0x060005AD RID: 1453
		public delegate bool EnumerateMetafileProc(EmfPlusRecordType recordType, int flags, int dataSize, IntPtr data, PlayRecordCallback callbackData);

		/// <summary>Provides a callback method for deciding when the <see cref="Overload:System.Drawing.Graphics.DrawImage" /> method should prematurely cancel execution and stop drawing an image.</summary>
		/// <param name="callbackdata">Internal pointer that specifies data for the callback method. This parameter is not passed by all <see cref="Overload:System.Drawing.Graphics.DrawImage" /> overloads. You can test for its absence by checking for the value <see cref="F:System.IntPtr.Zero" />.</param>
		/// <returns>This method returns <see langword="true" /> if it decides that the <see cref="Overload:System.Drawing.Graphics.DrawImage" /> method should prematurely stop execution. Otherwise it returns <see langword="false" /> to indicate that the <see cref="Overload:System.Drawing.Graphics.DrawImage" /> method should continue execution.</returns>
		// Token: 0x02000073 RID: 115
		// (Invoke) Token: 0x060005B1 RID: 1457
		public delegate bool DrawImageAbort(IntPtr callbackdata);
	}
}
