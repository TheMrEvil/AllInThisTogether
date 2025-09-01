using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Drawing
{
	/// <summary>An abstract base class that provides functionality for the <see cref="T:System.Drawing.Bitmap" /> and <see cref="T:System.Drawing.Imaging.Metafile" /> descended classes.</summary>
	// Token: 0x0200007D RID: 125
	[ImmutableObject(true)]
	[TypeConverter(typeof(ImageConverter))]
	[Editor("System.Drawing.Design.ImageEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[ComVisible(true)]
	[Serializable]
	public abstract class Image : MarshalByRefObject, IDisposable, ICloneable, ISerializable
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x00010EC0 File Offset: 0x0000F0C0
		internal Image()
		{
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00010ED4 File Offset: 0x0000F0D4
		internal Image(SerializationInfo info, StreamingContext context)
		{
			foreach (SerializationEntry serializationEntry in info)
			{
				if (string.Compare(serializationEntry.Name, "Data", true) == 0)
				{
					byte[] array = (byte[])serializationEntry.Value;
					if (array != null)
					{
						MemoryStream memoryStream = new MemoryStream(array);
						this.nativeObject = Image.InitFromStream(memoryStream);
						if (GDIPlus.RunningOnWindows())
						{
							this.stream = memoryStream;
						}
					}
				}
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x060005E2 RID: 1506 RVA: 0x00010F50 File Offset: 0x0000F150
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				if (this.RawFormat.Equals(ImageFormat.Icon))
				{
					this.Save(memoryStream, ImageFormat.Png);
				}
				else
				{
					this.Save(memoryStream, this.RawFormat);
				}
				si.AddValue("Data", memoryStream.ToArray());
			}
		}

		/// <summary>Creates an <see cref="T:System.Drawing.Image" /> from the specified file.</summary>
		/// <param name="filename">A string that contains the name of the file from which to create the <see cref="T:System.Drawing.Image" />.</param>
		/// <returns>The <see cref="T:System.Drawing.Image" /> this method creates.</returns>
		/// <exception cref="T:System.OutOfMemoryException">The file does not have a valid image format.  
		///  -or-  
		///  GDI+ does not support the pixel format of the file.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file does not exist.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="filename" /> is a <see cref="T:System.Uri" />.</exception>
		// Token: 0x060005E3 RID: 1507 RVA: 0x00010FC0 File Offset: 0x0000F1C0
		public static Image FromFile(string filename)
		{
			return Image.FromFile(filename, false);
		}

		/// <summary>Creates an <see cref="T:System.Drawing.Image" /> from the specified file using embedded color management information in that file.</summary>
		/// <param name="filename">A string that contains the name of the file from which to create the <see cref="T:System.Drawing.Image" />.</param>
		/// <param name="useEmbeddedColorManagement">Set to <see langword="true" /> to use color management information embedded in the image file; otherwise, <see langword="false" />.</param>
		/// <returns>The <see cref="T:System.Drawing.Image" /> this method creates.</returns>
		/// <exception cref="T:System.OutOfMemoryException">The file does not have a valid image format.  
		///  -or-  
		///  GDI+ does not support the pixel format of the file.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file does not exist.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="filename" /> is a <see cref="T:System.Uri" />.</exception>
		// Token: 0x060005E4 RID: 1508 RVA: 0x00010FCC File Offset: 0x0000F1CC
		public static Image FromFile(string filename, bool useEmbeddedColorManagement)
		{
			if (!File.Exists(filename))
			{
				throw new FileNotFoundException(filename);
			}
			IntPtr handle;
			Status status;
			if (useEmbeddedColorManagement)
			{
				status = GDIPlus.GdipLoadImageFromFileICM(filename, out handle);
			}
			else
			{
				status = GDIPlus.GdipLoadImageFromFile(filename, out handle);
			}
			GDIPlus.CheckStatus(status);
			return Image.CreateFromHandle(handle);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Bitmap" /> from a handle to a GDI bitmap.</summary>
		/// <param name="hbitmap">The GDI bitmap handle from which to create the <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <returns>The <see cref="T:System.Drawing.Bitmap" /> this method creates.</returns>
		// Token: 0x060005E5 RID: 1509 RVA: 0x0001100B File Offset: 0x0000F20B
		public static Bitmap FromHbitmap(IntPtr hbitmap)
		{
			return Image.FromHbitmap(hbitmap, IntPtr.Zero);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Bitmap" /> from a handle to a GDI bitmap and a handle to a GDI palette.</summary>
		/// <param name="hbitmap">The GDI bitmap handle from which to create the <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="hpalette">A handle to a GDI palette used to define the bitmap colors if the bitmap specified in the <paramref name="hbitmap" /> parameter is not a device-independent bitmap (DIB).</param>
		/// <returns>The <see cref="T:System.Drawing.Bitmap" /> this method creates.</returns>
		// Token: 0x060005E6 RID: 1510 RVA: 0x00011018 File Offset: 0x0000F218
		public static Bitmap FromHbitmap(IntPtr hbitmap, IntPtr hpalette)
		{
			IntPtr ptr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromHBITMAP(hbitmap, hpalette, out ptr));
			return new Bitmap(ptr);
		}

		/// <summary>Creates an <see cref="T:System.Drawing.Image" /> from the specified data stream.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Image" />.</param>
		/// <returns>The <see cref="T:System.Drawing.Image" /> this method creates.</returns>
		/// <exception cref="T:System.ArgumentException">The stream does not have a valid image format  
		///  -or-  
		///  <paramref name="stream" /> is <see langword="null" />.</exception>
		// Token: 0x060005E7 RID: 1511 RVA: 0x00011039 File Offset: 0x0000F239
		public static Image FromStream(Stream stream)
		{
			return Image.LoadFromStream(stream, false);
		}

		/// <summary>Creates an <see cref="T:System.Drawing.Image" /> from the specified data stream, optionally using embedded color management information in that stream.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Image" />.</param>
		/// <param name="useEmbeddedColorManagement">
		///   <see langword="true" /> to use color management information embedded in the data stream; otherwise, <see langword="false" />.</param>
		/// <returns>The <see cref="T:System.Drawing.Image" /> this method creates.</returns>
		/// <exception cref="T:System.ArgumentException">The stream does not have a valid image format  
		///  -or-  
		///  <paramref name="stream" /> is <see langword="null" />.</exception>
		// Token: 0x060005E8 RID: 1512 RVA: 0x00011039 File Offset: 0x0000F239
		[MonoLimitation("useEmbeddedColorManagement  isn't supported.")]
		public static Image FromStream(Stream stream, bool useEmbeddedColorManagement)
		{
			return Image.LoadFromStream(stream, false);
		}

		/// <summary>Creates an <see cref="T:System.Drawing.Image" /> from the specified data stream, optionally using embedded color management information and validating the image data.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Image" />.</param>
		/// <param name="useEmbeddedColorManagement">
		///   <see langword="true" /> to use color management information embedded in the data stream; otherwise, <see langword="false" />.</param>
		/// <param name="validateImageData">
		///   <see langword="true" /> to validate the image data; otherwise, <see langword="false" />.</param>
		/// <returns>The <see cref="T:System.Drawing.Image" /> this method creates.</returns>
		/// <exception cref="T:System.ArgumentException">The stream does not have a valid image format.</exception>
		// Token: 0x060005E9 RID: 1513 RVA: 0x00011039 File Offset: 0x0000F239
		[MonoLimitation("useEmbeddedColorManagement  and validateImageData aren't supported.")]
		public static Image FromStream(Stream stream, bool useEmbeddedColorManagement, bool validateImageData)
		{
			return Image.LoadFromStream(stream, false);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00011044 File Offset: 0x0000F244
		internal static Image LoadFromStream(Stream stream, bool keepAlive)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			Image image = Image.CreateFromHandle(Image.InitFromStream(stream));
			if (keepAlive && GDIPlus.RunningOnWindows())
			{
				image.stream = stream;
			}
			return image;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001107D File Offset: 0x0000F27D
		internal static Image CreateImageObject(IntPtr nativeImage)
		{
			return Image.CreateFromHandle(nativeImage);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00011088 File Offset: 0x0000F288
		internal static Image CreateFromHandle(IntPtr handle)
		{
			ImageType imageType;
			GDIPlus.CheckStatus(GDIPlus.GdipGetImageType(handle, out imageType));
			if (imageType == ImageType.Bitmap)
			{
				return new Bitmap(handle);
			}
			if (imageType != ImageType.Metafile)
			{
				throw new NotSupportedException(Locale.GetText("Unknown image type."));
			}
			return new Metafile(handle);
		}

		/// <summary>Returns the color depth, in number of bits per pixel, of the specified pixel format.</summary>
		/// <param name="pixfmt">The <see cref="T:System.Drawing.Imaging.PixelFormat" /> member that specifies the format for which to find the size.</param>
		/// <returns>The color depth of the specified pixel format.</returns>
		// Token: 0x060005ED RID: 1517 RVA: 0x000110CC File Offset: 0x0000F2CC
		public static int GetPixelFormatSize(PixelFormat pixfmt)
		{
			int result = 0;
			if (pixfmt <= PixelFormat.Format8bppIndexed)
			{
				if (pixfmt <= PixelFormat.Format32bppRgb)
				{
					if (pixfmt - PixelFormat.Format16bppRgb555 > 1)
					{
						if (pixfmt == PixelFormat.Format24bppRgb)
						{
							return 24;
						}
						if (pixfmt != PixelFormat.Format32bppRgb)
						{
							return result;
						}
						goto IL_A7;
					}
				}
				else
				{
					if (pixfmt == PixelFormat.Format1bppIndexed)
					{
						return 1;
					}
					if (pixfmt == PixelFormat.Format4bppIndexed)
					{
						return 4;
					}
					if (pixfmt != PixelFormat.Format8bppIndexed)
					{
						return result;
					}
					return 8;
				}
			}
			else
			{
				if (pixfmt > PixelFormat.Format16bppGrayScale)
				{
					if (pixfmt <= PixelFormat.Format64bppPArgb)
					{
						if (pixfmt == PixelFormat.Format48bppRgb)
						{
							return 48;
						}
						if (pixfmt != PixelFormat.Format64bppPArgb)
						{
							return result;
						}
					}
					else
					{
						if (pixfmt == PixelFormat.Format32bppArgb)
						{
							goto IL_A7;
						}
						if (pixfmt != PixelFormat.Format64bppArgb)
						{
							return result;
						}
					}
					return 64;
				}
				if (pixfmt != PixelFormat.Format16bppArgb1555)
				{
					if (pixfmt == PixelFormat.Format32bppPArgb)
					{
						goto IL_A7;
					}
					if (pixfmt != PixelFormat.Format16bppGrayScale)
					{
						return result;
					}
				}
			}
			return 16;
			IL_A7:
			result = 32;
			return result;
		}

		/// <summary>Returns a value that indicates whether the pixel format for this <see cref="T:System.Drawing.Image" /> contains alpha information.</summary>
		/// <param name="pixfmt">The <see cref="T:System.Drawing.Imaging.PixelFormat" /> to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="pixfmt" /> contains alpha information; otherwise, <see langword="false" />.</returns>
		// Token: 0x060005EE RID: 1518 RVA: 0x00011198 File Offset: 0x0000F398
		public static bool IsAlphaPixelFormat(PixelFormat pixfmt)
		{
			bool result = false;
			if (pixfmt > PixelFormat.Format8bppIndexed)
			{
				if (pixfmt <= PixelFormat.Format16bppGrayScale)
				{
					if (pixfmt != PixelFormat.Format16bppArgb1555 && pixfmt != PixelFormat.Format32bppPArgb)
					{
						if (pixfmt != PixelFormat.Format16bppGrayScale)
						{
							return result;
						}
						goto IL_98;
					}
				}
				else if (pixfmt <= PixelFormat.Format64bppPArgb)
				{
					if (pixfmt == PixelFormat.Format48bppRgb)
					{
						goto IL_98;
					}
					if (pixfmt != PixelFormat.Format64bppPArgb)
					{
						return result;
					}
				}
				else if (pixfmt != PixelFormat.Format32bppArgb && pixfmt != PixelFormat.Format64bppArgb)
				{
					return result;
				}
				return true;
			}
			if (pixfmt <= PixelFormat.Format32bppRgb)
			{
				if (pixfmt - PixelFormat.Format16bppRgb555 > 1 && pixfmt != PixelFormat.Format24bppRgb && pixfmt != PixelFormat.Format32bppRgb)
				{
					return result;
				}
			}
			else if (pixfmt != PixelFormat.Format1bppIndexed && pixfmt != PixelFormat.Format4bppIndexed && pixfmt != PixelFormat.Format8bppIndexed)
			{
				return result;
			}
			IL_98:
			result = false;
			return result;
		}

		/// <summary>Returns a value that indicates whether the pixel format is 32 bits per pixel.</summary>
		/// <param name="pixfmt">The <see cref="T:System.Drawing.Imaging.PixelFormat" /> to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="pixfmt" /> is canonical; otherwise, <see langword="false" />.</returns>
		// Token: 0x060005EF RID: 1519 RVA: 0x00011240 File Offset: 0x0000F440
		public static bool IsCanonicalPixelFormat(PixelFormat pixfmt)
		{
			return (pixfmt & PixelFormat.Canonical) > PixelFormat.Undefined;
		}

		/// <summary>Returns a value that indicates whether the pixel format is 64 bits per pixel.</summary>
		/// <param name="pixfmt">The <see cref="T:System.Drawing.Imaging.PixelFormat" /> enumeration to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="pixfmt" /> is extended; otherwise, <see langword="false" />.</returns>
		// Token: 0x060005F0 RID: 1520 RVA: 0x0001124C File Offset: 0x0000F44C
		public static bool IsExtendedPixelFormat(PixelFormat pixfmt)
		{
			return (pixfmt & PixelFormat.Extended) > PixelFormat.Undefined;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00011258 File Offset: 0x0000F458
		internal static IntPtr InitFromStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentException("stream");
			}
			if (!stream.CanSeek)
			{
				byte[] array = new byte[256];
				int num = 0;
				int num2;
				do
				{
					if (array.Length < num + 256)
					{
						byte[] array2 = new byte[array.Length * 2];
						Array.Copy(array, array2, array.Length);
						array = array2;
					}
					num2 = stream.Read(array, num, 256);
					num += num2;
				}
				while (num2 != 0);
				stream = new MemoryStream(array, 0, num);
			}
			IntPtr result;
			Status status;
			if (GDIPlus.RunningOnUnix())
			{
				GDIPlus.GdiPlusStreamHelper gdiPlusStreamHelper = new GDIPlus.GdiPlusStreamHelper(stream, true);
				status = GDIPlus.GdipLoadImageFromDelegate_linux(gdiPlusStreamHelper.GetHeaderDelegate, gdiPlusStreamHelper.GetBytesDelegate, gdiPlusStreamHelper.PutBytesDelegate, gdiPlusStreamHelper.SeekDelegate, gdiPlusStreamHelper.CloseDelegate, gdiPlusStreamHelper.SizeDelegate, out result);
			}
			else
			{
				status = GDIPlus.GdipLoadImageFromStream(new ComIStreamWrapper(stream), out result);
			}
			if (status != Status.Ok)
			{
				return IntPtr.Zero;
			}
			return result;
		}

		/// <summary>Gets the bounds of the image in the specified unit.</summary>
		/// <param name="pageUnit">One of the <see cref="T:System.Drawing.GraphicsUnit" /> values indicating the unit of measure for the bounding rectangle.</param>
		/// <returns>The <see cref="T:System.Drawing.RectangleF" /> that represents the bounds of the image, in the specified unit.</returns>
		// Token: 0x060005F2 RID: 1522 RVA: 0x0001132C File Offset: 0x0000F52C
		public RectangleF GetBounds(ref GraphicsUnit pageUnit)
		{
			RectangleF result;
			GDIPlus.CheckStatus(GDIPlus.GdipGetImageBounds(this.nativeObject, out result, ref pageUnit));
			return result;
		}

		/// <summary>Returns information about the parameters supported by the specified image encoder.</summary>
		/// <param name="encoder">A GUID that specifies the image encoder.</param>
		/// <returns>An <see cref="T:System.Drawing.Imaging.EncoderParameters" /> that contains an array of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects. Each <see cref="T:System.Drawing.Imaging.EncoderParameter" /> contains information about one of the parameters supported by the specified image encoder.</returns>
		// Token: 0x060005F3 RID: 1523 RVA: 0x00011350 File Offset: 0x0000F550
		public EncoderParameters GetEncoderParameterList(Guid encoder)
		{
			uint num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetEncoderParameterListSize(this.nativeObject, ref encoder, out num));
			IntPtr intPtr = Marshal.AllocHGlobal((int)num);
			EncoderParameters result;
			try
			{
				Status status = GDIPlus.GdipGetEncoderParameterList(this.nativeObject, ref encoder, num, intPtr);
				result = EncoderParameters.ConvertFromMemory(intPtr);
				GDIPlus.CheckStatus(status);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		/// <summary>Returns the number of frames of the specified dimension.</summary>
		/// <param name="dimension">A <see cref="T:System.Drawing.Imaging.FrameDimension" /> that specifies the identity of the dimension type.</param>
		/// <returns>The number of frames in the specified dimension.</returns>
		// Token: 0x060005F4 RID: 1524 RVA: 0x000113B0 File Offset: 0x0000F5B0
		public int GetFrameCount(FrameDimension dimension)
		{
			Guid guid = dimension.Guid;
			uint result;
			GDIPlus.CheckStatus(GDIPlus.GdipImageGetFrameCount(this.nativeObject, ref guid, out result));
			return (int)result;
		}

		/// <summary>Gets the specified property item from this <see cref="T:System.Drawing.Image" />.</summary>
		/// <param name="propid">The ID of the property item to get.</param>
		/// <returns>The <see cref="T:System.Drawing.Imaging.PropertyItem" /> this method gets.</returns>
		/// <exception cref="T:System.ArgumentException">The image format of this image does not support property items.</exception>
		// Token: 0x060005F5 RID: 1525 RVA: 0x000113DC File Offset: 0x0000F5DC
		public PropertyItem GetPropertyItem(int propid)
		{
			PropertyItem propertyItem = new PropertyItem();
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetPropertyItemSize(this.nativeObject, propid, out num));
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetPropertyItem(this.nativeObject, propid, num, intPtr));
				GdipPropertyItem.MarshalTo((GdipPropertyItem)Marshal.PtrToStructure(intPtr, typeof(GdipPropertyItem)), propertyItem);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return propertyItem;
		}

		/// <summary>Returns a thumbnail for this <see cref="T:System.Drawing.Image" />.</summary>
		/// <param name="thumbWidth">The width, in pixels, of the requested thumbnail image.</param>
		/// <param name="thumbHeight">The height, in pixels, of the requested thumbnail image.</param>
		/// <param name="callback">A <see cref="T:System.Drawing.Image.GetThumbnailImageAbort" /> delegate.  
		///  Note You must create a delegate and pass a reference to the delegate as the <paramref name="callback" /> parameter, but the delegate is not used.</param>
		/// <param name="callbackData">Must be <see cref="F:System.IntPtr.Zero" />.</param>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the thumbnail.</returns>
		// Token: 0x060005F6 RID: 1526 RVA: 0x00011454 File Offset: 0x0000F654
		public Image GetThumbnailImage(int thumbWidth, int thumbHeight, Image.GetThumbnailImageAbort callback, IntPtr callbackData)
		{
			if (thumbWidth <= 0 || thumbHeight <= 0)
			{
				throw new OutOfMemoryException("Invalid thumbnail size");
			}
			Image image = new Bitmap(thumbWidth, thumbHeight);
			using (Graphics graphics = Graphics.FromImage(image))
			{
				GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRectI(graphics.nativeObject, this.nativeObject, 0, 0, thumbWidth, thumbHeight, 0, 0, this.Width, this.Height, GraphicsUnit.Pixel, IntPtr.Zero, null, IntPtr.Zero));
			}
			return image;
		}

		/// <summary>Removes the specified property item from this <see cref="T:System.Drawing.Image" />.</summary>
		/// <param name="propid">The ID of the property item to remove.</param>
		/// <exception cref="T:System.ArgumentException">The image does not contain the requested property item.  
		///  -or-  
		///  The image format for this image does not support property items.</exception>
		// Token: 0x060005F7 RID: 1527 RVA: 0x000114D4 File Offset: 0x0000F6D4
		public void RemovePropertyItem(int propid)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRemovePropertyItem(this.nativeObject, propid));
		}

		/// <summary>Rotates, flips, or rotates and flips the <see cref="T:System.Drawing.Image" />.</summary>
		/// <param name="rotateFlipType">A <see cref="T:System.Drawing.RotateFlipType" /> member that specifies the type of rotation and flip to apply to the image.</param>
		// Token: 0x060005F8 RID: 1528 RVA: 0x000114E7 File Offset: 0x0000F6E7
		public void RotateFlip(RotateFlipType rotateFlipType)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipImageRotateFlip(this.nativeObject, rotateFlipType));
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x000114FC File Offset: 0x0000F6FC
		internal ImageCodecInfo findEncoderForFormat(ImageFormat format)
		{
			ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
			ImageCodecInfo result = null;
			if (format.Guid.Equals(ImageFormat.MemoryBmp.Guid))
			{
				format = ImageFormat.Png;
			}
			for (int i = 0; i < imageEncoders.Length; i++)
			{
				if (imageEncoders[i].FormatID.Equals(format.Guid))
				{
					result = imageEncoders[i];
					break;
				}
			}
			return result;
		}

		/// <summary>Saves this <see cref="T:System.Drawing.Image" /> to the specified file or stream.</summary>
		/// <param name="filename">A string that contains the name of the file to which to save this <see cref="T:System.Drawing.Image" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filename" /> is <see langword="null." /></exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format.  
		///  -or-  
		///  The image was saved to the same file it was created from.</exception>
		// Token: 0x060005FA RID: 1530 RVA: 0x0001155F File Offset: 0x0000F75F
		public void Save(string filename)
		{
			this.Save(filename, this.RawFormat);
		}

		/// <summary>Saves this <see cref="T:System.Drawing.Image" /> to the specified file in the specified format.</summary>
		/// <param name="filename">A string that contains the name of the file to which to save this <see cref="T:System.Drawing.Image" />.</param>
		/// <param name="format">The <see cref="T:System.Drawing.Imaging.ImageFormat" /> for this <see cref="T:System.Drawing.Image" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filename" /> or <paramref name="format" /> is <see langword="null." /></exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format.  
		///  -or-  
		///  The image was saved to the same file it was created from.</exception>
		// Token: 0x060005FB RID: 1531 RVA: 0x00011570 File Offset: 0x0000F770
		public void Save(string filename, ImageFormat format)
		{
			ImageCodecInfo imageCodecInfo = this.findEncoderForFormat(format);
			if (imageCodecInfo == null)
			{
				imageCodecInfo = this.findEncoderForFormat(this.RawFormat);
				if (imageCodecInfo == null)
				{
					throw new ArgumentException(Locale.GetText("No codec available for saving format '{0}'.", new object[]
					{
						format.Guid
					}), "format");
				}
			}
			this.Save(filename, imageCodecInfo, null);
		}

		/// <summary>Saves this <see cref="T:System.Drawing.Image" /> to the specified file, with the specified encoder and image-encoder parameters.</summary>
		/// <param name="filename">A string that contains the name of the file to which to save this <see cref="T:System.Drawing.Image" />.</param>
		/// <param name="encoder">The <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> for this <see cref="T:System.Drawing.Image" />.</param>
		/// <param name="encoderParams">An <see cref="T:System.Drawing.Imaging.EncoderParameters" /> to use for this <see cref="T:System.Drawing.Image" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filename" /> or <paramref name="encoder" /> is <see langword="null." /></exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format.  
		///  -or-  
		///  The image was saved to the same file it was created from.</exception>
		// Token: 0x060005FC RID: 1532 RVA: 0x000115CC File Offset: 0x0000F7CC
		public void Save(string filename, ImageCodecInfo encoder, EncoderParameters encoderParams)
		{
			Guid clsid = encoder.Clsid;
			Status status;
			if (encoderParams == null)
			{
				status = GDIPlus.GdipSaveImageToFile(this.nativeObject, filename, ref clsid, IntPtr.Zero);
			}
			else
			{
				IntPtr intPtr = encoderParams.ConvertToMemory();
				status = GDIPlus.GdipSaveImageToFile(this.nativeObject, filename, ref clsid, intPtr);
				Marshal.FreeHGlobal(intPtr);
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Saves this image to the specified stream in the specified format.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> where the image will be saved.</param>
		/// <param name="format">An <see cref="T:System.Drawing.Imaging.ImageFormat" /> that specifies the format of the saved image.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="format" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format</exception>
		// Token: 0x060005FD RID: 1533 RVA: 0x0001161C File Offset: 0x0000F81C
		public void Save(Stream stream, ImageFormat format)
		{
			ImageCodecInfo imageCodecInfo = this.findEncoderForFormat(format);
			if (imageCodecInfo == null)
			{
				throw new ArgumentException("No codec available for format:" + format.Guid.ToString());
			}
			this.Save(stream, imageCodecInfo, null);
		}

		/// <summary>Saves this image to the specified stream, with the specified encoder and image encoder parameters.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> where the image will be saved.</param>
		/// <param name="encoder">The <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> for this <see cref="T:System.Drawing.Image" />.</param>
		/// <param name="encoderParams">An <see cref="T:System.Drawing.Imaging.EncoderParameters" /> that specifies parameters used by the image encoder.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format.</exception>
		// Token: 0x060005FE RID: 1534 RVA: 0x00011664 File Offset: 0x0000F864
		public void Save(Stream stream, ImageCodecInfo encoder, EncoderParameters encoderParams)
		{
			Guid clsid = encoder.Clsid;
			IntPtr intPtr;
			if (encoderParams == null)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				intPtr = encoderParams.ConvertToMemory();
			}
			Status status;
			try
			{
				if (GDIPlus.RunningOnUnix())
				{
					GDIPlus.GdiPlusStreamHelper gdiPlusStreamHelper = new GDIPlus.GdiPlusStreamHelper(stream, false);
					status = GDIPlus.GdipSaveImageToDelegate_linux(this.nativeObject, gdiPlusStreamHelper.GetBytesDelegate, gdiPlusStreamHelper.PutBytesDelegate, gdiPlusStreamHelper.SeekDelegate, gdiPlusStreamHelper.CloseDelegate, gdiPlusStreamHelper.SizeDelegate, ref clsid, intPtr);
				}
				else
				{
					status = GDIPlus.GdipSaveImageToStream(new HandleRef(this, this.nativeObject), new ComIStreamWrapper(stream), ref clsid, new HandleRef(encoderParams, intPtr));
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Adds a frame to the file or stream specified in a previous call to the <see cref="Overload:System.Drawing.Image.Save" /> method. Use this method to save selected frames from a multiple-frame image to another multiple-frame image.</summary>
		/// <param name="encoderParams">An <see cref="T:System.Drawing.Imaging.EncoderParameters" /> that holds parameters required by the image encoder that is used by the save-add operation.</param>
		// Token: 0x060005FF RID: 1535 RVA: 0x00011718 File Offset: 0x0000F918
		public void SaveAdd(EncoderParameters encoderParams)
		{
			IntPtr intPtr = encoderParams.ConvertToMemory();
			Status status = GDIPlus.GdipSaveAdd(this.nativeObject, intPtr);
			Marshal.FreeHGlobal(intPtr);
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Adds a frame to the file or stream specified in a previous call to the <see cref="Overload:System.Drawing.Image.Save" /> method.</summary>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> that contains the frame to add.</param>
		/// <param name="encoderParams">An <see cref="T:System.Drawing.Imaging.EncoderParameters" /> that holds parameters required by the image encoder that is used by the save-add operation.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is <see langword="null" />.</exception>
		// Token: 0x06000600 RID: 1536 RVA: 0x00011744 File Offset: 0x0000F944
		public void SaveAdd(Image image, EncoderParameters encoderParams)
		{
			IntPtr intPtr = encoderParams.ConvertToMemory();
			Status status = GDIPlus.GdipSaveAddImage(this.nativeObject, image.NativeObject, intPtr);
			Marshal.FreeHGlobal(intPtr);
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Selects the frame specified by the dimension and index.</summary>
		/// <param name="dimension">A <see cref="T:System.Drawing.Imaging.FrameDimension" /> that specifies the identity of the dimension type.</param>
		/// <param name="frameIndex">The index of the active frame.</param>
		/// <returns>Always returns 0.</returns>
		// Token: 0x06000601 RID: 1537 RVA: 0x00011778 File Offset: 0x0000F978
		public int SelectActiveFrame(FrameDimension dimension, int frameIndex)
		{
			Guid guid = dimension.Guid;
			GDIPlus.CheckStatus(GDIPlus.GdipImageSelectActiveFrame(this.nativeObject, ref guid, frameIndex));
			return frameIndex;
		}

		/// <summary>Stores a property item (piece of metadata) in this <see cref="T:System.Drawing.Image" />.</summary>
		/// <param name="propitem">The <see cref="T:System.Drawing.Imaging.PropertyItem" /> to be stored.</param>
		/// <exception cref="T:System.ArgumentException">The image format of this image does not support property items.</exception>
		// Token: 0x06000602 RID: 1538 RVA: 0x000117A0 File Offset: 0x0000F9A0
		public unsafe void SetPropertyItem(PropertyItem propitem)
		{
			if (propitem == null)
			{
				throw new ArgumentNullException("propitem");
			}
			int num = Marshal.SizeOf<byte>(propitem.Value[0]) * propitem.Value.Length;
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			try
			{
				GdipPropertyItem gdipPropertyItem = default(GdipPropertyItem);
				gdipPropertyItem.id = propitem.Id;
				gdipPropertyItem.len = propitem.Len;
				gdipPropertyItem.type = propitem.Type;
				Marshal.Copy(propitem.Value, 0, intPtr, num);
				gdipPropertyItem.value = intPtr;
				GDIPlus.CheckStatus(GDIPlus.GdipSetPropertyItem(this.nativeObject, &gdipPropertyItem));
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Gets attribute flags for the pixel data of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The integer representing a bitwise combination of <see cref="T:System.Drawing.Imaging.ImageFlags" /> for this <see cref="T:System.Drawing.Image" />.</returns>
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0001184C File Offset: 0x0000FA4C
		[Browsable(false)]
		public int Flags
		{
			get
			{
				int result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageFlags(this.nativeObject, out result));
				return result;
			}
		}

		/// <summary>Gets an array of GUIDs that represent the dimensions of frames within this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>An array of GUIDs that specify the dimensions of frames within this <see cref="T:System.Drawing.Image" /> from most significant to least significant.</returns>
		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0001186C File Offset: 0x0000FA6C
		[Browsable(false)]
		public Guid[] FrameDimensionsList
		{
			get
			{
				uint num;
				GDIPlus.CheckStatus(GDIPlus.GdipImageGetFrameDimensionsCount(this.nativeObject, out num));
				Guid[] array = new Guid[num];
				GDIPlus.CheckStatus(GDIPlus.GdipImageGetFrameDimensionsList(this.nativeObject, array, num));
				return array;
			}
		}

		/// <summary>Gets the height, in pixels, of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The height, in pixels, of this <see cref="T:System.Drawing.Image" />.</returns>
		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x000118A8 File Offset: 0x0000FAA8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[DefaultValue(false)]
		public int Height
		{
			get
			{
				uint result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageHeight(this.nativeObject, out result));
				return (int)result;
			}
		}

		/// <summary>Gets the horizontal resolution, in pixels per inch, of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The horizontal resolution, in pixels per inch, of this <see cref="T:System.Drawing.Image" />.</returns>
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x000118C8 File Offset: 0x0000FAC8
		public float HorizontalResolution
		{
			get
			{
				float result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageHorizontalResolution(this.nativeObject, out result));
				return result;
			}
		}

		/// <summary>Gets or sets the color palette used for this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Imaging.ColorPalette" /> that represents the color palette used for this <see cref="T:System.Drawing.Image" />.</returns>
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x000118E8 File Offset: 0x0000FAE8
		// (set) Token: 0x06000608 RID: 1544 RVA: 0x000118F0 File Offset: 0x0000FAF0
		[Browsable(false)]
		public ColorPalette Palette
		{
			get
			{
				return this.retrieveGDIPalette();
			}
			set
			{
				this.storeGDIPalette(value);
			}
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x000118FC File Offset: 0x0000FAFC
		internal ColorPalette retrieveGDIPalette()
		{
			ColorPalette colorPalette = new ColorPalette();
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetImagePaletteSize(this.nativeObject, out num));
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			ColorPalette result;
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetImagePalette(this.nativeObject, intPtr, num));
				colorPalette.ConvertFromMemory(intPtr);
				result = colorPalette;
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x00011960 File Offset: 0x0000FB60
		internal void storeGDIPalette(ColorPalette palette)
		{
			if (palette == null)
			{
				throw new ArgumentNullException("palette");
			}
			IntPtr intPtr = palette.ConvertToMemory();
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetImagePalette(this.nativeObject, intPtr));
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Gets the width and height of this image.</summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> structure that represents the width and height of this <see cref="T:System.Drawing.Image" />.</returns>
		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x000119BC File Offset: 0x0000FBBC
		public SizeF PhysicalDimension
		{
			get
			{
				float width;
				float height;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageDimension(this.nativeObject, out width, out height));
				return new SizeF(width, height);
			}
		}

		/// <summary>Gets the pixel format for this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Imaging.PixelFormat" /> that represents the pixel format for this <see cref="T:System.Drawing.Image" />.</returns>
		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x000119E4 File Offset: 0x0000FBE4
		public PixelFormat PixelFormat
		{
			get
			{
				PixelFormat result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImagePixelFormat(this.nativeObject, out result));
				return result;
			}
		}

		/// <summary>Gets IDs of the property items stored in this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>An array of the property IDs, one for each property item stored in this image.</returns>
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x00011A04 File Offset: 0x0000FC04
		[Browsable(false)]
		public int[] PropertyIdList
		{
			get
			{
				uint num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPropertyCount(this.nativeObject, out num));
				int[] array = new int[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetPropertyIdList(this.nativeObject, num, array));
				return array;
			}
		}

		/// <summary>Gets all the property items (pieces of metadata) stored in this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.Imaging.PropertyItem" /> objects, one for each property item stored in the image.</returns>
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00011A40 File Offset: 0x0000FC40
		[Browsable(false)]
		public PropertyItem[] PropertyItems
		{
			get
			{
				GdipPropertyItem gdipPropertyItem = default(GdipPropertyItem);
				int num;
				int num2;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPropertySize(this.nativeObject, out num, out num2));
				PropertyItem[] array = new PropertyItem[num2];
				if (num2 == 0)
				{
					return array;
				}
				IntPtr intPtr = Marshal.AllocHGlobal(num * num2);
				try
				{
					GDIPlus.CheckStatus(GDIPlus.GdipGetAllPropertyItems(this.nativeObject, num, num2, intPtr));
					int num3 = Marshal.SizeOf<GdipPropertyItem>(gdipPropertyItem);
					IntPtr ptr = intPtr;
					int i = 0;
					while (i < num2)
					{
						gdipPropertyItem = (GdipPropertyItem)Marshal.PtrToStructure(ptr, typeof(GdipPropertyItem));
						array[i] = new PropertyItem();
						GdipPropertyItem.MarshalTo(gdipPropertyItem, array[i]);
						i++;
						ptr = new IntPtr(ptr.ToInt64() + (long)num3);
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				return array;
			}
		}

		/// <summary>Gets the file format of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Imaging.ImageFormat" /> that represents the file format of this <see cref="T:System.Drawing.Image" />.</returns>
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00011B08 File Offset: 0x0000FD08
		public ImageFormat RawFormat
		{
			get
			{
				Guid guid;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageRawFormat(this.nativeObject, out guid));
				return new ImageFormat(guid);
			}
		}

		/// <summary>Gets the width and height, in pixels, of this image.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure that represents the width and height, in pixels, of this image.</returns>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00011B2D File Offset: 0x0000FD2D
		public Size Size
		{
			get
			{
				return new Size(this.Width, this.Height);
			}
		}

		/// <summary>Gets or sets an object that provides additional data about the image.</summary>
		/// <returns>The <see cref="T:System.Object" /> that provides additional data about the image.</returns>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00011B40 File Offset: 0x0000FD40
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x00011B48 File Offset: 0x0000FD48
		[DefaultValue(null)]
		[Localizable(false)]
		[Bindable(true)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		/// <summary>Gets the vertical resolution, in pixels per inch, of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The vertical resolution, in pixels per inch, of this <see cref="T:System.Drawing.Image" />.</returns>
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x00011B54 File Offset: 0x0000FD54
		public float VerticalResolution
		{
			get
			{
				float result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageVerticalResolution(this.nativeObject, out result));
				return result;
			}
		}

		/// <summary>Gets the width, in pixels, of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The width, in pixels, of this <see cref="T:System.Drawing.Image" />.</returns>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x00011B74 File Offset: 0x0000FD74
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(false)]
		public int Width
		{
			get
			{
				uint result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageWidth(this.nativeObject, out result));
				return (int)result;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00011B94 File Offset: 0x0000FD94
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x00011B9C File Offset: 0x0000FD9C
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

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00011B94 File Offset: 0x0000FD94
		internal IntPtr nativeImage
		{
			get
			{
				return this.nativeObject;
			}
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Image" />.</summary>
		// Token: 0x06000618 RID: 1560 RVA: 0x00011BA5 File Offset: 0x0000FDA5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x06000619 RID: 1561 RVA: 0x00011BB4 File Offset: 0x0000FDB4
		~Image()
		{
			this.Dispose(false);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Drawing.Image" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">
		///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
		// Token: 0x0600061A RID: 1562 RVA: 0x00011BE4 File Offset: 0x0000FDE4
		protected virtual void Dispose(bool disposing)
		{
			if (GDIPlus.GdiPlusToken != 0UL && this.nativeObject != IntPtr.Zero)
			{
				Status status = GDIPlus.GdipDisposeImage(this.nativeObject);
				if (this.stream != null)
				{
					this.stream.Dispose();
					this.stream = null;
				}
				this.nativeObject = IntPtr.Zero;
				GDIPlus.CheckStatus(status);
			}
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> this method creates, cast as an object.</returns>
		// Token: 0x0600061B RID: 1563 RVA: 0x00011C40 File Offset: 0x0000FE40
		public object Clone()
		{
			if (GDIPlus.RunningOnWindows() && this.stream != null)
			{
				return this.CloneFromStream();
			}
			IntPtr zero = IntPtr.Zero;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneImage(this.NativeObject, out zero));
			if (this is Bitmap)
			{
				return new Bitmap(zero);
			}
			return new Metafile(zero);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00011C90 File Offset: 0x0000FE90
		private object CloneFromStream()
		{
			MemoryStream memoryStream = new MemoryStream(new byte[this.stream.Length]);
			int num = (this.stream.Length < 4096L) ? ((int)this.stream.Length) : 4096;
			byte[] buffer = new byte[num];
			this.stream.Position = 0L;
			do
			{
				num = this.stream.Read(buffer, 0, num);
				memoryStream.Write(buffer, 0, num);
			}
			while (num == 4096);
			IntPtr ptr = IntPtr.Zero;
			ptr = Image.InitFromStream(memoryStream);
			if (this is Bitmap)
			{
				return new Bitmap(ptr, memoryStream);
			}
			return new Metafile(ptr, memoryStream);
		}

		// Token: 0x040004C9 RID: 1225
		private object tag;

		// Token: 0x040004CA RID: 1226
		internal IntPtr nativeObject = IntPtr.Zero;

		// Token: 0x040004CB RID: 1227
		internal Stream stream;

		/// <summary>Provides a callback method for determining when the <see cref="M:System.Drawing.Image.GetThumbnailImage(System.Int32,System.Int32,System.Drawing.Image.GetThumbnailImageAbort,System.IntPtr)" /> method should prematurely cancel execution.</summary>
		/// <returns>This method returns <see langword="true" /> if it decides that the <see cref="M:System.Drawing.Image.GetThumbnailImage(System.Int32,System.Int32,System.Drawing.Image.GetThumbnailImageAbort,System.IntPtr)" /> method should prematurely stop execution; otherwise, it returns <see langword="false" />.</returns>
		// Token: 0x0200007E RID: 126
		// (Invoke) Token: 0x0600061E RID: 1566
		public delegate bool GetThumbnailImageAbort();
	}
}
