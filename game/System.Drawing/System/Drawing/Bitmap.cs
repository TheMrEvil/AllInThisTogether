using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Drawing
{
	/// <summary>Encapsulates a GDI+ bitmap, which consists of the pixel data for a graphics image and its attributes. A <see cref="T:System.Drawing.Bitmap" /> is an object used to work with images defined by pixel data.</summary>
	// Token: 0x0200004D RID: 77
	[ComVisible(true)]
	[Editor("System.Drawing.Design.BitmapEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Serializable]
	public sealed class Bitmap : Image
	{
		// Token: 0x060002F3 RID: 755 RVA: 0x000085AF File Offset: 0x000067AF
		private Bitmap()
		{
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000085B7 File Offset: 0x000067B7
		internal Bitmap(IntPtr ptr)
		{
			this.nativeObject = ptr;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000085C6 File Offset: 0x000067C6
		internal Bitmap(IntPtr ptr, Stream stream)
		{
			if (GDIPlus.RunningOnWindows())
			{
				this.stream = stream;
			}
			this.nativeObject = ptr;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class with the specified size.</summary>
		/// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x060002F6 RID: 758 RVA: 0x000085E3 File Offset: 0x000067E3
		public Bitmap(int width, int height) : this(width, height, PixelFormat.Format32bppArgb)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class with the specified size and with the resolution of the specified <see cref="T:System.Drawing.Graphics" /> object.</summary>
		/// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object that specifies the resolution for the new <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> is <see langword="null" />.</exception>
		// Token: 0x060002F7 RID: 759 RVA: 0x000085F4 File Offset: 0x000067F4
		public Bitmap(int width, int height, Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			IntPtr nativeObject;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromGraphics(width, height, g.nativeObject, out nativeObject));
			this.nativeObject = nativeObject;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class with the specified size and format.</summary>
		/// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="format">The pixel format for the new <see cref="T:System.Drawing.Bitmap" />. This must specify a value that begins with Format.</param>
		/// <exception cref="T:System.ArgumentException">A <see cref="T:System.Drawing.Imaging.PixelFormat" /> value is specified whose name does not start with Format. For example, specifying <see cref="F:System.Drawing.Imaging.PixelFormat.Gdi" /> will cause an <see cref="T:System.ArgumentException" />, but <see cref="F:System.Drawing.Imaging.PixelFormat.Format48bppRgb" /> will not.</exception>
		// Token: 0x060002F8 RID: 760 RVA: 0x00008630 File Offset: 0x00006830
		public Bitmap(int width, int height, PixelFormat format)
		{
			IntPtr nativeObject;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromScan0(width, height, 0, format, IntPtr.Zero, out nativeObject));
			this.nativeObject = nativeObject;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified existing image.</summary>
		/// <param name="original">The <see cref="T:System.Drawing.Image" /> from which to create the new <see cref="T:System.Drawing.Bitmap" />.</param>
		// Token: 0x060002F9 RID: 761 RVA: 0x0000865F File Offset: 0x0000685F
		public Bitmap(Image original) : this(original, original.Width, original.Height)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified data stream.</summary>
		/// <param name="stream">The data stream used to load the image.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not contain image data or is <see langword="null" />.  
		/// -or-  
		/// <paramref name="stream" /> contains a PNG image file with a single dimension greater than 65,535 pixels.</exception>
		// Token: 0x060002FA RID: 762 RVA: 0x00008674 File Offset: 0x00006874
		public Bitmap(Stream stream) : this(stream, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified file.</summary>
		/// <param name="filename">The bitmap file name and path.</param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file is not found.</exception>
		// Token: 0x060002FB RID: 763 RVA: 0x0000867E File Offset: 0x0000687E
		public Bitmap(string filename) : this(filename, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified existing image, scaled to the specified size.</summary>
		/// <param name="original">The <see cref="T:System.Drawing.Image" /> from which to create the new <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="newSize">The <see cref="T:System.Drawing.Size" /> structure that represent the size of the new <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x060002FC RID: 764 RVA: 0x00008688 File Offset: 0x00006888
		public Bitmap(Image original, Size newSize) : this(original, newSize.Width, newSize.Height)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified data stream.</summary>
		/// <param name="stream">The data stream used to load the image.</param>
		/// <param name="useIcm">
		///   <see langword="true" /> to use color correction for this <see cref="T:System.Drawing.Bitmap" />; otherwise, <see langword="false" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not contain image data or is <see langword="null" />.  
		/// -or-  
		/// <paramref name="stream" /> contains a PNG image file with a single dimension greater than 65,535 pixels.</exception>
		// Token: 0x060002FD RID: 765 RVA: 0x0000869F File Offset: 0x0000689F
		public Bitmap(Stream stream, bool useIcm)
		{
			this.nativeObject = Image.InitFromStream(stream);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified file.</summary>
		/// <param name="filename">The name of the bitmap file.</param>
		/// <param name="useIcm">
		///   <see langword="true" /> to use color correction for this <see cref="T:System.Drawing.Bitmap" />; otherwise, <see langword="false" />.</param>
		// Token: 0x060002FE RID: 766 RVA: 0x000086B4 File Offset: 0x000068B4
		public Bitmap(string filename, bool useIcm)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			IntPtr nativeObject;
			Status status;
			if (useIcm)
			{
				status = GDIPlus.GdipCreateBitmapFromFileICM(filename, out nativeObject);
			}
			else
			{
				status = GDIPlus.GdipCreateBitmapFromFile(filename, out nativeObject);
			}
			GDIPlus.CheckStatus(status);
			this.nativeObject = nativeObject;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from a specified resource.</summary>
		/// <param name="type">The class used to extract the resource.</param>
		/// <param name="resource">The name of the resource.</param>
		// Token: 0x060002FF RID: 767 RVA: 0x000086FC File Offset: 0x000068FC
		public Bitmap(Type type, string resource)
		{
			if (resource == null)
			{
				throw new ArgumentException("resource");
			}
			if (type == null)
			{
				throw new NullReferenceException();
			}
			Stream manifestResourceStream = type.GetTypeInfo().Assembly.GetManifestResourceStream(type, resource);
			if (manifestResourceStream == null)
			{
				throw new FileNotFoundException(Locale.GetText("Resource '{0}' was not found.", new object[]
				{
					resource
				}));
			}
			this.nativeObject = Image.InitFromStream(manifestResourceStream);
			if (GDIPlus.RunningOnWindows())
			{
				this.stream = manifestResourceStream;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified existing image, scaled to the specified size.</summary>
		/// <param name="original">The <see cref="T:System.Drawing.Image" /> from which to create the new <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x06000300 RID: 768 RVA: 0x00008776 File Offset: 0x00006976
		public Bitmap(Image original, int width, int height) : this(width, height, PixelFormat.Format32bppArgb)
		{
			Graphics graphics = Graphics.FromImage(this);
			graphics.DrawImage(original, 0, 0, width, height);
			graphics.Dispose();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class with the specified size, pixel format, and pixel data.</summary>
		/// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="stride">Integer that specifies the byte offset between the beginning of one scan line and the next. This is usually (but not necessarily) the number of bytes in the pixel format (for example, 2 for 16 bits per pixel) multiplied by the width of the bitmap. The value passed to this parameter must be a multiple of four.</param>
		/// <param name="format">The pixel format for the new <see cref="T:System.Drawing.Bitmap" />. This must specify a value that begins with Format.</param>
		/// <param name="scan0">Pointer to an array of bytes that contains the pixel data.</param>
		/// <exception cref="T:System.ArgumentException">A <see cref="T:System.Drawing.Imaging.PixelFormat" /> value is specified whose name does not start with Format. For example, specifying <see cref="F:System.Drawing.Imaging.PixelFormat.Gdi" /> will cause an <see cref="T:System.ArgumentException" />, but <see cref="F:System.Drawing.Imaging.PixelFormat.Format48bppRgb" /> will not.</exception>
		// Token: 0x06000301 RID: 769 RVA: 0x0000879C File Offset: 0x0000699C
		public Bitmap(int width, int height, int stride, PixelFormat format, IntPtr scan0)
		{
			IntPtr nativeObject;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromScan0(width, height, stride, format, scan0, out nativeObject));
			this.nativeObject = nativeObject;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000087C9 File Offset: 0x000069C9
		private Bitmap(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		/// <summary>Gets the color of the specified pixel in this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <param name="x">The x-coordinate of the pixel to retrieve.</param>
		/// <param name="y">The y-coordinate of the pixel to retrieve.</param>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the color of the specified pixel.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="x" /> is less than 0, or greater than or equal to <see cref="P:System.Drawing.Image.Width" />.  
		/// -or-  
		/// <paramref name="y" /> is less than 0, or greater than or equal to <see cref="P:System.Drawing.Image.Height" />.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x06000303 RID: 771 RVA: 0x000087D4 File Offset: 0x000069D4
		public Color GetPixel(int x, int y)
		{
			int argb;
			GDIPlus.CheckStatus(GDIPlus.GdipBitmapGetPixel(this.nativeObject, x, y, out argb));
			return Color.FromArgb(argb);
		}

		/// <summary>Sets the color of the specified pixel in this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <param name="x">The x-coordinate of the pixel to set.</param>
		/// <param name="y">The y-coordinate of the pixel to set.</param>
		/// <param name="color">A <see cref="T:System.Drawing.Color" /> structure that represents the color to assign to the specified pixel.</param>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x06000304 RID: 772 RVA: 0x000087FB File Offset: 0x000069FB
		public void SetPixel(int x, int y, Color color)
		{
			Status status = GDIPlus.GdipBitmapSetPixel(this.nativeObject, x, y, color.ToArgb());
			if (status == Status.InvalidParameter && (base.PixelFormat & PixelFormat.Indexed) != PixelFormat.Undefined)
			{
				throw new InvalidOperationException(Locale.GetText("SetPixel cannot be called on indexed bitmaps."));
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Creates a copy of the section of this <see cref="T:System.Drawing.Bitmap" /> defined by <see cref="T:System.Drawing.Rectangle" /> structure and with a specified <see cref="T:System.Drawing.Imaging.PixelFormat" /> enumeration.</summary>
		/// <param name="rect">Defines the portion of this <see cref="T:System.Drawing.Bitmap" /> to copy. Coordinates are relative to this <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="format">The pixel format for the new <see cref="T:System.Drawing.Bitmap" />. This must specify a value that begins with Format.</param>
		/// <returns>The new <see cref="T:System.Drawing.Bitmap" /> that this method creates.</returns>
		/// <exception cref="T:System.OutOfMemoryException">
		///   <paramref name="rect" /> is outside of the source bitmap bounds.</exception>
		/// <exception cref="T:System.ArgumentException">The height or width of <paramref name="rect" /> is 0.  
		///  -or-  
		///  A <see cref="T:System.Drawing.Imaging.PixelFormat" /> value is specified whose name does not start with Format. For example, specifying <see cref="F:System.Drawing.Imaging.PixelFormat.Gdi" /> will cause an <see cref="T:System.ArgumentException" />, but <see cref="F:System.Drawing.Imaging.PixelFormat.Format48bppRgb" /> will not.</exception>
		// Token: 0x06000305 RID: 773 RVA: 0x00008838 File Offset: 0x00006A38
		public Bitmap Clone(Rectangle rect, PixelFormat format)
		{
			IntPtr ptr;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneBitmapAreaI(rect.X, rect.Y, rect.Width, rect.Height, format, this.nativeObject, out ptr));
			return new Bitmap(ptr);
		}

		/// <summary>Creates a copy of the section of this <see cref="T:System.Drawing.Bitmap" /> defined with a specified <see cref="T:System.Drawing.Imaging.PixelFormat" /> enumeration.</summary>
		/// <param name="rect">Defines the portion of this <see cref="T:System.Drawing.Bitmap" /> to copy.</param>
		/// <param name="format">Specifies the <see cref="T:System.Drawing.Imaging.PixelFormat" /> enumeration for the destination <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <returns>The <see cref="T:System.Drawing.Bitmap" /> that this method creates.</returns>
		/// <exception cref="T:System.OutOfMemoryException">
		///   <paramref name="rect" /> is outside of the source bitmap bounds.</exception>
		/// <exception cref="T:System.ArgumentException">The height or width of <paramref name="rect" /> is 0.</exception>
		// Token: 0x06000306 RID: 774 RVA: 0x0000887C File Offset: 0x00006A7C
		public Bitmap Clone(RectangleF rect, PixelFormat format)
		{
			IntPtr ptr;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneBitmapArea(rect.X, rect.Y, rect.Width, rect.Height, format, this.nativeObject, out ptr));
			return new Bitmap(ptr);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Bitmap" /> from a Windows handle to an icon.</summary>
		/// <param name="hicon">A handle to an icon.</param>
		/// <returns>The <see cref="T:System.Drawing.Bitmap" /> that this method creates.</returns>
		// Token: 0x06000307 RID: 775 RVA: 0x000088C0 File Offset: 0x00006AC0
		public static Bitmap FromHicon(IntPtr hicon)
		{
			IntPtr ptr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromHICON(hicon, out ptr));
			return new Bitmap(ptr);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Bitmap" /> from the specified Windows resource.</summary>
		/// <param name="hinstance">A handle to an instance of the executable file that contains the resource.</param>
		/// <param name="bitmapName">A string that contains the name of the resource bitmap.</param>
		/// <returns>The <see cref="T:System.Drawing.Bitmap" /> that this method creates.</returns>
		// Token: 0x06000308 RID: 776 RVA: 0x000088E0 File Offset: 0x00006AE0
		public static Bitmap FromResource(IntPtr hinstance, string bitmapName)
		{
			IntPtr ptr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromResource(hinstance, bitmapName, out ptr));
			return new Bitmap(ptr);
		}

		/// <summary>Creates a GDI bitmap object from this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <returns>A handle to the GDI bitmap object that this method creates.</returns>
		/// <exception cref="T:System.ArgumentException">The height or width of the bitmap is greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x06000309 RID: 777 RVA: 0x00008901 File Offset: 0x00006B01
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IntPtr GetHbitmap()
		{
			return this.GetHbitmap(Color.Gray);
		}

		/// <summary>Creates a GDI bitmap object from this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <param name="background">A <see cref="T:System.Drawing.Color" /> structure that specifies the background color. This parameter is ignored if the bitmap is totally opaque.</param>
		/// <returns>A handle to the GDI bitmap object that this method creates.</returns>
		/// <exception cref="T:System.ArgumentException">The height or width of the bitmap is greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x0600030A RID: 778 RVA: 0x00008910 File Offset: 0x00006B10
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IntPtr GetHbitmap(Color background)
		{
			IntPtr result;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateHBITMAPFromBitmap(this.nativeObject, out result, background.ToArgb()));
			return result;
		}

		/// <summary>Returns the handle to an icon.</summary>
		/// <returns>A Windows handle to an icon with the same image as the <see cref="T:System.Drawing.Bitmap" />.</returns>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x0600030B RID: 779 RVA: 0x00008938 File Offset: 0x00006B38
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IntPtr GetHicon()
		{
			IntPtr result;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateHICONFromBitmap(this.nativeObject, out result));
			return result;
		}

		/// <summary>Locks a <see cref="T:System.Drawing.Bitmap" /> into system memory.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <see cref="T:System.Drawing.Bitmap" /> to lock.</param>
		/// <param name="flags">An <see cref="T:System.Drawing.Imaging.ImageLockMode" /> enumeration that specifies the access level (read/write) for the <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="format">A <see cref="T:System.Drawing.Imaging.PixelFormat" /> enumeration that specifies the data format of this <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <returns>A <see cref="T:System.Drawing.Imaging.BitmapData" /> that contains information about this lock operation.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Drawing.Imaging.PixelFormat" /> is not a specific bits-per-pixel value.  
		///  -or-  
		///  The incorrect <see cref="T:System.Drawing.Imaging.PixelFormat" /> is passed in for a bitmap.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x0600030C RID: 780 RVA: 0x00008958 File Offset: 0x00006B58
		public BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format)
		{
			BitmapData bitmapData = new BitmapData();
			return this.LockBits(rect, flags, format, bitmapData);
		}

		/// <summary>Locks a <see cref="T:System.Drawing.Bitmap" /> into system memory</summary>
		/// <param name="rect">A rectangle structure that specifies the portion of the <see cref="T:System.Drawing.Bitmap" /> to lock.</param>
		/// <param name="flags">One of the <see cref="T:System.Drawing.Imaging.ImageLockMode" /> values that specifies the access level (read/write) for the <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="format">One of the <see cref="T:System.Drawing.Imaging.PixelFormat" /> values that specifies the data format of the <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="bitmapData">A <see cref="T:System.Drawing.Imaging.BitmapData" /> that contains information about the lock operation.</param>
		/// <returns>A <see cref="T:System.Drawing.Imaging.BitmapData" /> that contains information about the lock operation.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="T:System.Drawing.Imaging.PixelFormat" /> value is not a specific bits-per-pixel value.  
		/// -or-  
		/// The incorrect <see cref="T:System.Drawing.Imaging.PixelFormat" /> is passed in for a bitmap.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x0600030D RID: 781 RVA: 0x00008975 File Offset: 0x00006B75
		public BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format, BitmapData bitmapData)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipBitmapLockBits(this.nativeObject, ref rect, flags, format, bitmapData));
			return bitmapData;
		}

		/// <summary>Makes the default transparent color transparent for this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The image format of the <see cref="T:System.Drawing.Bitmap" /> is an icon format.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x0600030E RID: 782 RVA: 0x00008990 File Offset: 0x00006B90
		public void MakeTransparent()
		{
			Color pixel = this.GetPixel(0, 0);
			this.MakeTransparent(pixel);
		}

		/// <summary>Makes the specified color transparent for this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <param name="transparentColor">The <see cref="T:System.Drawing.Color" /> structure that represents the color to make transparent.</param>
		/// <exception cref="T:System.InvalidOperationException">The image format of the <see cref="T:System.Drawing.Bitmap" /> is an icon format.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x0600030F RID: 783 RVA: 0x000089B0 File Offset: 0x00006BB0
		public void MakeTransparent(Color transparentColor)
		{
			Bitmap bitmap = new Bitmap(base.Width, base.Height, PixelFormat.Format32bppArgb);
			Graphics graphics = Graphics.FromImage(bitmap);
			Rectangle destRect = new Rectangle(0, 0, base.Width, base.Height);
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetColorKey(transparentColor, transparentColor);
			graphics.DrawImage(this, destRect, 0, 0, base.Width, base.Height, GraphicsUnit.Pixel, imageAttributes);
			IntPtr nativeObject = this.nativeObject;
			this.nativeObject = bitmap.nativeObject;
			bitmap.nativeObject = nativeObject;
			graphics.Dispose();
			bitmap.Dispose();
			imageAttributes.Dispose();
		}

		/// <summary>Sets the resolution for this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <param name="xDpi">The horizontal resolution, in dots per inch, of the <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="yDpi">The vertical resolution, in dots per inch, of the <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x06000310 RID: 784 RVA: 0x00008A40 File Offset: 0x00006C40
		public void SetResolution(float xDpi, float yDpi)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipBitmapSetResolution(this.nativeObject, xDpi, yDpi));
		}

		/// <summary>Unlocks this <see cref="T:System.Drawing.Bitmap" /> from system memory.</summary>
		/// <param name="bitmapdata">A <see cref="T:System.Drawing.Imaging.BitmapData" /> that specifies information about the lock operation.</param>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x06000311 RID: 785 RVA: 0x00008A54 File Offset: 0x00006C54
		public void UnlockBits(BitmapData bitmapdata)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipBitmapUnlockBits(this.nativeObject, bitmapdata));
		}
	}
}
