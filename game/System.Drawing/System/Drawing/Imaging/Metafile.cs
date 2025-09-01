using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Defines a graphic metafile. A metafile contains records that describe a sequence of graphics operations that can be recorded (constructed) and played back (displayed). This class is not inheritable.</summary>
	// Token: 0x0200011B RID: 283
	[Editor("System.Drawing.Design.MetafileEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[MonoTODO("Metafiles, both WMF and EMF formats, are only partially supported.")]
	[Serializable]
	public sealed class Metafile : Image
	{
		// Token: 0x06000D1F RID: 3359 RVA: 0x0001E629 File Offset: 0x0001C829
		internal Metafile.MetafileHolder AddMetafileHolder()
		{
			if (this._metafileHolder != null && !this._metafileHolder.Disposed)
			{
				return null;
			}
			this._metafileHolder = new Metafile.MetafileHolder();
			return this._metafileHolder;
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x000085B7 File Offset: 0x000067B7
		internal Metafile(IntPtr ptr)
		{
			this.nativeObject = ptr;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x000085C6 File Offset: 0x000067C6
		internal Metafile(IntPtr ptr, Stream stream)
		{
			if (GDIPlus.RunningOnWindows())
			{
				this.stream = stream;
			}
			this.nativeObject = ptr;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> from which to create the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		// Token: 0x06000D22 RID: 3362 RVA: 0x0001E654 File Offset: 0x0001C854
		public Metafile(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentException("stream");
			}
			Status status;
			if (GDIPlus.RunningOnUnix())
			{
				GDIPlus.GdiPlusStreamHelper gdiPlusStreamHelper = new GDIPlus.GdiPlusStreamHelper(stream, false);
				status = GDIPlus.GdipCreateMetafileFromDelegate_linux(gdiPlusStreamHelper.GetHeaderDelegate, gdiPlusStreamHelper.GetBytesDelegate, gdiPlusStreamHelper.PutBytesDelegate, gdiPlusStreamHelper.SeekDelegate, gdiPlusStreamHelper.CloseDelegate, gdiPlusStreamHelper.SizeDelegate, out this.nativeObject);
			}
			else
			{
				status = GDIPlus.GdipCreateMetafileFromStream(new ComIStreamWrapper(stream), out this.nativeObject);
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified file name.</summary>
		/// <param name="filename">A <see cref="T:System.String" /> that represents the file name from which to create the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D23 RID: 3363 RVA: 0x0001E6D0 File Offset: 0x0001C8D0
		public Metafile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			if (filename.Length == 0)
			{
				throw new ArgumentException("filename");
			}
			Status status = GDIPlus.GdipCreateMetafileFromFile(filename, out this.nativeObject);
			if (status == Status.GenericError)
			{
				throw new ExternalException("Couldn't load specified file.");
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified handle.</summary>
		/// <param name="henhmetafile">A handle to an enhanced metafile.</param>
		/// <param name="deleteEmf">
		///   <see langword="true" /> to delete the enhanced metafile handle when the <see cref="T:System.Drawing.Imaging.Metafile" /> is deleted; otherwise, <see langword="false" />.</param>
		// Token: 0x06000D24 RID: 3364 RVA: 0x0001E724 File Offset: 0x0001C924
		public Metafile(IntPtr henhmetafile, bool deleteEmf)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateMetafileFromEmf(henhmetafile, deleteEmf, out this.nativeObject));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified handle to a device context and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="referenceHdc">The handle to a device context.</param>
		/// <param name="emfType">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D25 RID: 3365 RVA: 0x0001E740 File Offset: 0x0001C940
		public Metafile(IntPtr referenceHdc, EmfType emfType) : this(referenceHdc, default(RectangleF), MetafileFrameUnit.GdiCompatible, emfType, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle.</summary>
		/// <param name="referenceHdc">The handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D26 RID: 3366 RVA: 0x0001E760 File Offset: 0x0001C960
		public Metafile(IntPtr referenceHdc, Rectangle frameRect) : this(referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle.</summary>
		/// <param name="referenceHdc">The handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D27 RID: 3367 RVA: 0x0001E76D File Offset: 0x0001C96D
		public Metafile(IntPtr referenceHdc, RectangleF frameRect) : this(referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified handle and a <see cref="T:System.Drawing.Imaging.WmfPlaceableFileHeader" />.</summary>
		/// <param name="hmetafile">A windows handle to a <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="wmfHeader">A <see cref="T:System.Drawing.Imaging.WmfPlaceableFileHeader" />.</param>
		// Token: 0x06000D28 RID: 3368 RVA: 0x0001E77A File Offset: 0x0001C97A
		public Metafile(IntPtr hmetafile, WmfPlaceableFileHeader wmfHeader)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateMetafileFromEmf(hmetafile, false, out this.nativeObject));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		// Token: 0x06000D29 RID: 3369 RVA: 0x0001E794 File Offset: 0x0001C994
		public Metafile(Stream stream, IntPtr referenceHdc) : this(stream, referenceHdc, default(RectangleF), MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		// Token: 0x06000D2A RID: 3370 RVA: 0x0001E7B8 File Offset: 0x0001C9B8
		public Metafile(string fileName, IntPtr referenceHdc) : this(fileName, referenceHdc, default(RectangleF), MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified handle to a device context and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A string can be supplied to name the file.</summary>
		/// <param name="referenceHdc">The handle to a device context.</param>
		/// <param name="emfType">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="description">A descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D2B RID: 3371 RVA: 0x0001E7DC File Offset: 0x0001C9DC
		public Metafile(IntPtr referenceHdc, EmfType emfType, string description) : this(referenceHdc, default(RectangleF), MetafileFrameUnit.GdiCompatible, emfType, description)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure.</summary>
		/// <param name="referenceHdc">The handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		// Token: 0x06000D2C RID: 3372 RVA: 0x0001E7FC File Offset: 0x0001C9FC
		public Metafile(IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit) : this(referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure.</summary>
		/// <param name="referenceHdc">The handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		// Token: 0x06000D2D RID: 3373 RVA: 0x0001E809 File Offset: 0x0001CA09
		public Metafile(IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit) : this(referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified handle and a <see cref="T:System.Drawing.Imaging.WmfPlaceableFileHeader" />. Also, the <paramref name="deleteWmf" /> parameter can be used to delete the handle when the metafile is deleted.</summary>
		/// <param name="hmetafile">A windows handle to a <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="wmfHeader">A <see cref="T:System.Drawing.Imaging.WmfPlaceableFileHeader" />.</param>
		/// <param name="deleteWmf">
		///   <see langword="true" /> to delete the handle to the new <see cref="T:System.Drawing.Imaging.Metafile" /> when the <see cref="T:System.Drawing.Imaging.Metafile" /> is deleted; otherwise, <see langword="false" />.</param>
		// Token: 0x06000D2E RID: 3374 RVA: 0x0001E816 File Offset: 0x0001CA16
		public Metafile(IntPtr hmetafile, WmfPlaceableFileHeader wmfHeader, bool deleteWmf)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateMetafileFromEmf(hmetafile, deleteWmf, out this.nativeObject));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D2F RID: 3375 RVA: 0x0001E830 File Offset: 0x0001CA30
		public Metafile(Stream stream, IntPtr referenceHdc, EmfType type) : this(stream, referenceHdc, default(RectangleF), MetafileFrameUnit.GdiCompatible, type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, and a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D30 RID: 3376 RVA: 0x0001E851 File Offset: 0x0001CA51
		public Metafile(Stream stream, IntPtr referenceHdc, Rectangle frameRect) : this(stream, referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, and a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D31 RID: 3377 RVA: 0x0001E85F File Offset: 0x0001CA5F
		public Metafile(Stream stream, IntPtr referenceHdc, RectangleF frameRect) : this(stream, referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D32 RID: 3378 RVA: 0x0001E870 File Offset: 0x0001CA70
		public Metafile(string fileName, IntPtr referenceHdc, EmfType type) : this(fileName, referenceHdc, default(RectangleF), MetafileFrameUnit.GdiCompatible, type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, and a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D33 RID: 3379 RVA: 0x0001E891 File Offset: 0x0001CA91
		public Metafile(string fileName, IntPtr referenceHdc, Rectangle frameRect) : this(fileName, referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, and a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D34 RID: 3380 RVA: 0x0001E89F File Offset: 0x0001CA9F
		public Metafile(string fileName, IntPtr referenceHdc, RectangleF frameRect) : this(fileName, referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="referenceHdc">The handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D35 RID: 3381 RVA: 0x0001E8AD File Offset: 0x0001CAAD
		public Metafile(IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit, EmfType type) : this(referenceHdc, frameRect, frameUnit, type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="referenceHdc">The handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D36 RID: 3382 RVA: 0x0001E8BB File Offset: 0x0001CABB
		public Metafile(IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit, EmfType type) : this(referenceHdc, frameRect, frameUnit, type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. Also, a string that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" /> can be added.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D37 RID: 3383 RVA: 0x0001E8CC File Offset: 0x0001CACC
		public Metafile(Stream stream, IntPtr referenceHdc, EmfType type, string description) : this(stream, referenceHdc, default(RectangleF), MetafileFrameUnit.GdiCompatible, type, description)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		// Token: 0x06000D38 RID: 3384 RVA: 0x0001E8EE File Offset: 0x0001CAEE
		public Metafile(Stream stream, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit) : this(stream, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		// Token: 0x06000D39 RID: 3385 RVA: 0x0001E8FD File Offset: 0x0001CAFD
		public Metafile(Stream stream, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit) : this(stream, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A descriptive string can be added, as well.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D3A RID: 3386 RVA: 0x0001E90C File Offset: 0x0001CB0C
		public Metafile(string fileName, IntPtr referenceHdc, EmfType type, string description) : this(fileName, referenceHdc, default(RectangleF), MetafileFrameUnit.GdiCompatible, type, description)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		// Token: 0x06000D3B RID: 3387 RVA: 0x0001E92E File Offset: 0x0001CB2E
		public Metafile(string fileName, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit) : this(fileName, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		// Token: 0x06000D3C RID: 3388 RVA: 0x0001E93D File Offset: 0x0001CB3D
		public Metafile(string fileName, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit) : this(fileName, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A string can be provided to name the file.</summary>
		/// <param name="referenceHdc">The handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="desc">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D3D RID: 3389 RVA: 0x0001E94C File Offset: 0x0001CB4C
		public Metafile(IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit, EmfType type, string desc)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRecordMetafileI(referenceHdc, type, ref frameRect, frameUnit, desc, out this.nativeObject));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A string can be provided to name the file.</summary>
		/// <param name="referenceHdc">The handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D3E RID: 3390 RVA: 0x0001E96C File Offset: 0x0001CB6C
		public Metafile(IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit, EmfType type, string description)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRecordMetafile(referenceHdc, type, ref frameRect, frameUnit, description, out this.nativeObject));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D3F RID: 3391 RVA: 0x0001E98C File Offset: 0x0001CB8C
		public Metafile(Stream stream, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit, EmfType type) : this(stream, referenceHdc, frameRect, frameUnit, type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D40 RID: 3392 RVA: 0x0001E99C File Offset: 0x0001CB9C
		public Metafile(Stream stream, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit, EmfType type) : this(stream, referenceHdc, frameRect, frameUnit, type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D41 RID: 3393 RVA: 0x0001E9AC File Offset: 0x0001CBAC
		public Metafile(string fileName, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit, EmfType type) : this(fileName, referenceHdc, frameRect, frameUnit, type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure. A descriptive string can also be added.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		/// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D42 RID: 3394 RVA: 0x0001E9BC File Offset: 0x0001CBBC
		public Metafile(string fileName, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit, string description) : this(fileName, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, description)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D43 RID: 3395 RVA: 0x0001E9CC File Offset: 0x0001CBCC
		public Metafile(string fileName, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit, EmfType type) : this(fileName, referenceHdc, frameRect, frameUnit, type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure. A descriptive string can also be added.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		/// <param name="desc">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D44 RID: 3396 RVA: 0x0001E9DC File Offset: 0x0001CBDC
		public Metafile(string fileName, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit, string desc) : this(fileName, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, desc)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A string that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" /> can be added.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D45 RID: 3397 RVA: 0x0001E9EC File Offset: 0x0001CBEC
		public Metafile(Stream stream, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit, EmfType type, string description)
		{
			if (stream == null)
			{
				throw new NullReferenceException("stream");
			}
			Status status;
			if (GDIPlus.RunningOnUnix())
			{
				GDIPlus.GdiPlusStreamHelper gdiPlusStreamHelper = new GDIPlus.GdiPlusStreamHelper(stream, false);
				status = GDIPlus.GdipRecordMetafileFromDelegateI_linux(gdiPlusStreamHelper.GetHeaderDelegate, gdiPlusStreamHelper.GetBytesDelegate, gdiPlusStreamHelper.PutBytesDelegate, gdiPlusStreamHelper.SeekDelegate, gdiPlusStreamHelper.CloseDelegate, gdiPlusStreamHelper.SizeDelegate, referenceHdc, type, ref frameRect, frameUnit, description, out this.nativeObject);
			}
			else
			{
				status = GDIPlus.GdipRecordMetafileStreamI(new ComIStreamWrapper(stream), referenceHdc, type, ref frameRect, frameUnit, description, out this.nativeObject);
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A string that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" /> can be added.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D46 RID: 3398 RVA: 0x0001EA7C File Offset: 0x0001CC7C
		public Metafile(Stream stream, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit, EmfType type, string description)
		{
			if (stream == null)
			{
				throw new NullReferenceException("stream");
			}
			Status status;
			if (GDIPlus.RunningOnUnix())
			{
				GDIPlus.GdiPlusStreamHelper gdiPlusStreamHelper = new GDIPlus.GdiPlusStreamHelper(stream, false);
				status = GDIPlus.GdipRecordMetafileFromDelegate_linux(gdiPlusStreamHelper.GetHeaderDelegate, gdiPlusStreamHelper.GetBytesDelegate, gdiPlusStreamHelper.PutBytesDelegate, gdiPlusStreamHelper.SeekDelegate, gdiPlusStreamHelper.CloseDelegate, gdiPlusStreamHelper.SizeDelegate, referenceHdc, type, ref frameRect, frameUnit, description, out this.nativeObject);
			}
			else
			{
				status = GDIPlus.GdipRecordMetafileStream(new ComIStreamWrapper(stream), referenceHdc, type, ref frameRect, frameUnit, description, out this.nativeObject);
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A descriptive string can also be added.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D47 RID: 3399 RVA: 0x0001EB0A File Offset: 0x0001CD0A
		public Metafile(string fileName, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit, EmfType type, string description)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRecordMetafileFileNameI(fileName, referenceHdc, type, ref frameRect, frameUnit, description, out this.nativeObject));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A descriptive string can also be added.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="referenceHdc">A Windows handle to a device context.</param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />.</param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		/// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />.</param>
		// Token: 0x06000D48 RID: 3400 RVA: 0x0001EB2C File Offset: 0x0001CD2C
		public Metafile(string fileName, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit, EmfType type, string description)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRecordMetafileFileName(fileName, referenceHdc, type, ref frameRect, frameUnit, description, out this.nativeObject));
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0001EB50 File Offset: 0x0001CD50
		protected override void Dispose(bool disposing)
		{
			if (this._metafileHolder != null && !this._metafileHolder.Disposed)
			{
				this._metafileHolder.MetafileDisposed(this.nativeObject);
				this._metafileHolder = null;
				this.nativeObject = IntPtr.Zero;
			}
			base.Dispose(disposing);
		}

		/// <summary>Returns a Windows handle to an enhanced <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <returns>A Windows handle to this enhanced <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		// Token: 0x06000D4A RID: 3402 RVA: 0x00011B94 File Offset: 0x0000FD94
		public IntPtr GetHenhmetafile()
		{
			return this.nativeObject;
		}

		/// <summary>Returns the <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with this <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with this <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		// Token: 0x06000D4B RID: 3403 RVA: 0x0001EB9C File Offset: 0x0001CD9C
		[MonoLimitation("Metafiles aren't only partially supported by libgdiplus.")]
		public MetafileHeader GetMetafileHeader()
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MetafileHeader)));
			MetafileHeader result;
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetMetafileHeaderFromMetafile(this.nativeObject, intPtr));
				result = new MetafileHeader(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		/// <summary>Returns the <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="henhmetafile">The handle to the enhanced <see cref="T:System.Drawing.Imaging.Metafile" /> for which a header is returned.</param>
		/// <returns>The <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		// Token: 0x06000D4C RID: 3404 RVA: 0x0001EBF0 File Offset: 0x0001CDF0
		[MonoLimitation("Metafiles aren't only partially supported by libgdiplus.")]
		public static MetafileHeader GetMetafileHeader(IntPtr henhmetafile)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MetafileHeader)));
			MetafileHeader result;
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetMetafileHeaderFromEmf(henhmetafile, intPtr));
				result = new MetafileHeader(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		/// <summary>Returns the <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> containing the <see cref="T:System.Drawing.Imaging.Metafile" /> for which a header is retrieved.</param>
		/// <returns>The <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		// Token: 0x06000D4D RID: 3405 RVA: 0x0001EC40 File Offset: 0x0001CE40
		[MonoLimitation("Metafiles aren't only partially supported by libgdiplus.")]
		public static MetafileHeader GetMetafileHeader(Stream stream)
		{
			if (stream == null)
			{
				throw new NullReferenceException("stream");
			}
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MetafileHeader)));
			MetafileHeader result;
			try
			{
				Status status;
				if (GDIPlus.RunningOnUnix())
				{
					GDIPlus.GdiPlusStreamHelper gdiPlusStreamHelper = new GDIPlus.GdiPlusStreamHelper(stream, false);
					status = GDIPlus.GdipGetMetafileHeaderFromDelegate_linux(gdiPlusStreamHelper.GetHeaderDelegate, gdiPlusStreamHelper.GetBytesDelegate, gdiPlusStreamHelper.PutBytesDelegate, gdiPlusStreamHelper.SeekDelegate, gdiPlusStreamHelper.CloseDelegate, gdiPlusStreamHelper.SizeDelegate, intPtr);
				}
				else
				{
					status = GDIPlus.GdipGetMetafileHeaderFromStream(new ComIStreamWrapper(stream), intPtr);
				}
				GDIPlus.CheckStatus(status);
				result = new MetafileHeader(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		/// <summary>Returns the <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> containing the name of the <see cref="T:System.Drawing.Imaging.Metafile" /> for which a header is retrieved.</param>
		/// <returns>The <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		// Token: 0x06000D4E RID: 3406 RVA: 0x0001ECE0 File Offset: 0x0001CEE0
		[MonoLimitation("Metafiles aren't only partially supported by libgdiplus.")]
		public static MetafileHeader GetMetafileHeader(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MetafileHeader)));
			MetafileHeader result;
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetMetafileHeaderFromFile(fileName, intPtr));
				result = new MetafileHeader(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		/// <summary>Returns the <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="hmetafile">The handle to the <see cref="T:System.Drawing.Imaging.Metafile" /> for which to return a header.</param>
		/// <param name="wmfHeader">A <see cref="T:System.Drawing.Imaging.WmfPlaceableFileHeader" />.</param>
		/// <returns>The <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		// Token: 0x06000D4F RID: 3407 RVA: 0x0001ED40 File Offset: 0x0001CF40
		[MonoLimitation("Metafiles aren't only partially supported by libgdiplus.")]
		public static MetafileHeader GetMetafileHeader(IntPtr hmetafile, WmfPlaceableFileHeader wmfHeader)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MetafileHeader)));
			MetafileHeader result;
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetMetafileHeaderFromEmf(hmetafile, intPtr));
				result = new MetafileHeader(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		/// <summary>Plays an individual metafile record.</summary>
		/// <param name="recordType">Element of the <see cref="T:System.Drawing.Imaging.EmfPlusRecordType" /> that specifies the type of metafile record being played.</param>
		/// <param name="flags">A set of flags that specify attributes of the record.</param>
		/// <param name="dataSize">The number of bytes in the record data.</param>
		/// <param name="data">An array of bytes that contains the record data.</param>
		// Token: 0x06000D50 RID: 3408 RVA: 0x0001ED90 File Offset: 0x0001CF90
		[MonoLimitation("Metafiles aren't only partially supported by libgdiplus.")]
		public void PlayRecord(EmfPlusRecordType recordType, int flags, int dataSize, byte[] data)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipPlayMetafileRecord(this.nativeObject, recordType, flags, dataSize, data));
		}

		// Token: 0x04000A79 RID: 2681
		private Metafile.MetafileHolder _metafileHolder;

		// Token: 0x0200011C RID: 284
		internal sealed class MetafileHolder : IDisposable
		{
			// Token: 0x17000393 RID: 915
			// (get) Token: 0x06000D51 RID: 3409 RVA: 0x0001EDA7 File Offset: 0x0001CFA7
			internal bool Disposed
			{
				get
				{
					return this._disposed;
				}
			}

			// Token: 0x06000D52 RID: 3410 RVA: 0x0001EDAF File Offset: 0x0001CFAF
			internal MetafileHolder()
			{
				this._disposed = false;
				this._nativeImage = IntPtr.Zero;
			}

			// Token: 0x06000D53 RID: 3411 RVA: 0x0001EDCC File Offset: 0x0001CFCC
			~MetafileHolder()
			{
				this.Dispose(false);
			}

			// Token: 0x06000D54 RID: 3412 RVA: 0x0001EDFC File Offset: 0x0001CFFC
			public void Dispose()
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}

			// Token: 0x06000D55 RID: 3413 RVA: 0x0001EE0C File Offset: 0x0001D00C
			internal void Dispose(bool disposing)
			{
				if (!this._disposed)
				{
					IntPtr nativeImage = this._nativeImage;
					this._nativeImage = IntPtr.Zero;
					this._disposed = true;
					if (nativeImage != IntPtr.Zero)
					{
						GDIPlus.CheckStatus(GDIPlus.GdipDisposeImage(nativeImage));
					}
				}
			}

			// Token: 0x06000D56 RID: 3414 RVA: 0x0001EE52 File Offset: 0x0001D052
			internal void MetafileDisposed(IntPtr nativeImage)
			{
				this._nativeImage = nativeImage;
			}

			// Token: 0x06000D57 RID: 3415 RVA: 0x0001EE5B File Offset: 0x0001D05B
			internal void GraphicsDisposed()
			{
				this.Dispose();
			}

			// Token: 0x04000A7A RID: 2682
			private bool _disposed;

			// Token: 0x04000A7B RID: 2683
			private IntPtr _nativeImage;
		}
	}
}
