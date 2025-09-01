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
	/// <summary>Represents a Windows icon, which is a small bitmap image that is used to represent an object. Icons can be thought of as transparent bitmaps, although their size is determined by the system.</summary>
	// Token: 0x02000075 RID: 117
	[Editor("System.Drawing.Design.IconEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[TypeConverter(typeof(IconConverter))]
	[Serializable]
	public sealed class Icon : MarshalByRefObject, ISerializable, ICloneable, IDisposable
	{
		// Token: 0x060005B4 RID: 1460 RVA: 0x0000F698 File Offset: 0x0000D898
		private Icon()
		{
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0000F6AC File Offset: 0x0000D8AC
		private Icon(IntPtr handle)
		{
			this.handle = handle;
			this.bitmap = Bitmap.FromHicon(handle);
			this.iconSize = new Size(this.bitmap.Width, this.bitmap.Height);
			if (GDIPlus.RunningOnUnix())
			{
				this.bitmap = Bitmap.FromHicon(handle);
				this.iconSize = new Size(this.bitmap.Width, this.bitmap.Height);
			}
			else
			{
				IconInfo iconInfo;
				GDIPlus.GetIconInfo(handle, out iconInfo);
				if (!iconInfo.IsIcon)
				{
					throw new NotImplementedException(Locale.GetText("Handle doesn't represent an ICON."));
				}
				this.iconSize = new Size(iconInfo.xHotspot * 2, iconInfo.yHotspot * 2);
				this.bitmap = Image.FromHbitmap(iconInfo.hbmColor);
			}
			this.undisposable = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class and attempts to find a version of the icon that matches the requested size.</summary>
		/// <param name="original">The icon to load the different size from.</param>
		/// <param name="width">The width of the new icon.</param>
		/// <param name="height">The height of the new icon.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="original" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060005B6 RID: 1462 RVA: 0x0000F789 File Offset: 0x0000D989
		public Icon(Icon original, int width, int height) : this(original, new Size(width, height))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class and attempts to find a version of the icon that matches the requested size.</summary>
		/// <param name="original">The <see cref="T:System.Drawing.Icon" /> from which to load the newly sized icon.</param>
		/// <param name="size">A <see cref="T:System.Drawing.Size" /> structure that specifies the height and width of the new <see cref="T:System.Drawing.Icon" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="original" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060005B7 RID: 1463 RVA: 0x0000F79C File Offset: 0x0000D99C
		public Icon(Icon original, Size size)
		{
			if (original == null)
			{
				throw new ArgumentException("original");
			}
			this.iconSize = size;
			this.iconDir = original.iconDir;
			int idCount = (int)this.iconDir.idCount;
			if (idCount > 0)
			{
				this.imageData = original.imageData;
				this.id = ushort.MaxValue;
				ushort num = 0;
				while ((int)num < idCount)
				{
					Icon.IconDirEntry iconDirEntry = this.iconDir.idEntries[(int)num];
					if (((int)iconDirEntry.height == size.Height || (int)iconDirEntry.width == size.Width) && !iconDirEntry.ignore)
					{
						this.id = num;
						break;
					}
					num += 1;
				}
				if (this.id == 65535)
				{
					int num2 = Math.Min(size.Height, size.Width);
					Icon.IconDirEntry? iconDirEntry2 = null;
					ushort num3 = 0;
					while ((int)num3 < idCount)
					{
						Icon.IconDirEntry iconDirEntry3 = this.iconDir.idEntries[(int)num3];
						if (((int)iconDirEntry3.height < num2 || (int)iconDirEntry3.width < num2) && !iconDirEntry3.ignore)
						{
							if (iconDirEntry2 == null)
							{
								iconDirEntry2 = new Icon.IconDirEntry?(iconDirEntry3);
								this.id = num3;
							}
							else if (iconDirEntry3.height > iconDirEntry2.Value.height || iconDirEntry3.width > iconDirEntry2.Value.width)
							{
								iconDirEntry2 = new Icon.IconDirEntry?(iconDirEntry3);
								this.id = num3;
							}
						}
						num3 += 1;
					}
				}
				if (this.id == 65535)
				{
					int num4 = idCount;
					while (this.id == 65535 && num4 > 0)
					{
						num4--;
						if (!this.iconDir.idEntries[num4].ignore)
						{
							this.id = (ushort)num4;
						}
					}
				}
				if (this.id == 65535)
				{
					throw new ArgumentException("Icon", "No valid icon image found");
				}
				this.iconSize.Height = (int)this.iconDir.idEntries[(int)this.id].height;
				this.iconSize.Width = (int)this.iconDir.idEntries[(int)this.id].width;
			}
			else
			{
				this.iconSize.Height = size.Height;
				this.iconSize.Width = size.Width;
			}
			if (original.bitmap != null)
			{
				this.bitmap = (Bitmap)original.bitmap.Clone();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class from the specified data stream.</summary>
		/// <param name="stream">The data stream from which to load the <see cref="T:System.Drawing.Icon" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060005B8 RID: 1464 RVA: 0x0000FA17 File Offset: 0x0000DC17
		public Icon(Stream stream) : this(stream, 32, 32)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class from the specified data stream and with the specified width and height.</summary>
		/// <param name="stream">The data stream from which to load the icon.</param>
		/// <param name="width">The width, in pixels, of the icon.</param>
		/// <param name="height">The height, in pixels, of the icon.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060005B9 RID: 1465 RVA: 0x0000FA24 File Offset: 0x0000DC24
		public Icon(Stream stream, int width, int height)
		{
			this.InitFromStreamWithSize(stream, width, height);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class from the specified file name.</summary>
		/// <param name="fileName">The file to load the <see cref="T:System.Drawing.Icon" /> from.</param>
		// Token: 0x060005BA RID: 1466 RVA: 0x0000FA40 File Offset: 0x0000DC40
		public Icon(string fileName)
		{
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				this.InitFromStreamWithSize(fileStream, 32, 32);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class from a resource in the specified assembly.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that specifies the assembly in which to look for the resource.</param>
		/// <param name="resource">The resource name to load.</param>
		/// <exception cref="T:System.ArgumentException">An icon specified by <paramref name="resource" /> cannot be found in the assembly that contains the specified <paramref name="type" />.</exception>
		// Token: 0x060005BB RID: 1467 RVA: 0x0000FA8C File Offset: 0x0000DC8C
		public Icon(Type type, string resource)
		{
			if (resource == null)
			{
				throw new ArgumentException("resource");
			}
			if (type == null)
			{
				throw new NullReferenceException();
			}
			using (Stream manifestResourceStream = type.GetTypeInfo().Assembly.GetManifestResourceStream(type, resource))
			{
				if (manifestResourceStream == null)
				{
					throw new FileNotFoundException(Locale.GetText("Resource '{0}' was not found.", new object[]
					{
						resource
					}));
				}
				this.InitFromStreamWithSize(manifestResourceStream, 32, 32);
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0000FB20 File Offset: 0x0000DD20
		private Icon(SerializationInfo info, StreamingContext context)
		{
			MemoryStream memoryStream = null;
			int width = 0;
			int height = 0;
			foreach (SerializationEntry serializationEntry in info)
			{
				if (string.Compare(serializationEntry.Name, "IconData", true) == 0)
				{
					memoryStream = new MemoryStream((byte[])serializationEntry.Value);
				}
				if (string.Compare(serializationEntry.Name, "IconSize", true) == 0)
				{
					Size size = (Size)serializationEntry.Value;
					width = size.Width;
					height = size.Height;
				}
			}
			if (memoryStream != null)
			{
				memoryStream.Seek(0L, SeekOrigin.Begin);
				this.InitFromStreamWithSize(memoryStream, width, height);
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0000FBCC File Offset: 0x0000DDCC
		internal Icon(string resourceName, bool undisposable)
		{
			using (Stream manifestResourceStream = typeof(Icon).GetTypeInfo().Assembly.GetManifestResourceStream(resourceName))
			{
				if (manifestResourceStream == null)
				{
					throw new FileNotFoundException(Locale.GetText("Resource '{0}' was not found.", new object[]
					{
						resourceName
					}));
				}
				this.InitFromStreamWithSize(manifestResourceStream, 32, 32);
			}
			this.undisposable = true;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data that is required to serialize the target object.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x060005BE RID: 1470 RVA: 0x0000FC50 File Offset: 0x0000DE50
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Save(memoryStream);
			si.AddValue("IconSize", this.Size, typeof(Size));
			si.AddValue("IconData", memoryStream.ToArray());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class of the specified size from the specified stream.</summary>
		/// <param name="stream">The stream that contains the icon data.</param>
		/// <param name="size">The desired size of the icon.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> is <see langword="null" /> or does not contain image data.</exception>
		// Token: 0x060005BF RID: 1471 RVA: 0x0000FC9B File Offset: 0x0000DE9B
		public Icon(Stream stream, Size size) : this(stream, size.Width, size.Height)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class with the specified width and height from the specified file.</summary>
		/// <param name="fileName">The name and path to the file that contains the <see cref="T:System.Drawing.Icon" /> data.</param>
		/// <param name="width">The desired width of the <see cref="T:System.Drawing.Icon" />.</param>
		/// <param name="height">The desired height of the <see cref="T:System.Drawing.Icon" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="string" /> is <see langword="null" /> or does not contain image data.</exception>
		// Token: 0x060005C0 RID: 1472 RVA: 0x0000FCB4 File Offset: 0x0000DEB4
		public Icon(string fileName, int width, int height)
		{
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				this.InitFromStreamWithSize(fileStream, width, height);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class of the specified size from the specified file.</summary>
		/// <param name="fileName">The name and path to the file that contains the icon data.</param>
		/// <param name="size">The desired size of the icon.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="string" /> is <see langword="null" /> or does not contain image data.</exception>
		// Token: 0x060005C1 RID: 1473 RVA: 0x0000FD00 File Offset: 0x0000DF00
		public Icon(string fileName, Size size)
		{
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				this.InitFromStreamWithSize(fileStream, size.Width, size.Height);
			}
		}

		/// <summary>Returns an icon representation of an image that is contained in the specified file.</summary>
		/// <param name="filePath">The path to the file that contains an image.</param>
		/// <returns>The <see cref="T:System.Drawing.Icon" /> representation of the image that is contained in the specified file.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="filePath" /> does not indicate a valid file.  
		///  -or-  
		///  The <paramref name="filePath" /> indicates a Universal Naming Convention (UNC) path.</exception>
		// Token: 0x060005C2 RID: 1474 RVA: 0x0000FD58 File Offset: 0x0000DF58
		[MonoLimitation("The same icon, SystemIcons.WinLogo, is returned for all file types.")]
		public static Icon ExtractAssociatedIcon(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentException(Locale.GetText("Null or empty path."), "filePath");
			}
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException(Locale.GetText("Couldn't find specified file."), filePath);
			}
			return SystemIcons.WinLogo;
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Icon" />.</summary>
		// Token: 0x060005C3 RID: 1475 RVA: 0x0000FD98 File Offset: 0x0000DF98
		public void Dispose()
		{
			if (this.undisposable)
			{
				return;
			}
			if (!this.disposed)
			{
				if (GDIPlus.RunningOnWindows() && this.handle != IntPtr.Zero)
				{
					GDIPlus.DestroyIcon(this.handle);
					this.handle = IntPtr.Zero;
				}
				if (this.bitmap != null)
				{
					this.bitmap.Dispose();
					this.bitmap = null;
				}
				GC.SuppressFinalize(this);
			}
			this.disposed = true;
		}

		/// <summary>Clones the <see cref="T:System.Drawing.Icon" />, creating a duplicate image.</summary>
		/// <returns>An object that can be cast to an <see cref="T:System.Drawing.Icon" />.</returns>
		// Token: 0x060005C4 RID: 1476 RVA: 0x0000FE0D File Offset: 0x0000E00D
		public object Clone()
		{
			return new Icon(this, this.Size);
		}

		/// <summary>Creates a GDI+ <see cref="T:System.Drawing.Icon" /> from the specified Windows handle to an icon (<see langword="HICON" />).</summary>
		/// <param name="handle">A Windows handle to an icon.</param>
		/// <returns>The <see cref="T:System.Drawing.Icon" /> this method creates.</returns>
		// Token: 0x060005C5 RID: 1477 RVA: 0x0000FE1B File Offset: 0x0000E01B
		public static Icon FromHandle(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentException("handle");
			}
			return new Icon(handle);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0000FE3C File Offset: 0x0000E03C
		private void SaveIconImage(BinaryWriter writer, Icon.IconImage ii)
		{
			Icon.BitmapInfoHeader iconHeader = ii.iconHeader;
			writer.Write(iconHeader.biSize);
			writer.Write(iconHeader.biWidth);
			writer.Write(iconHeader.biHeight);
			writer.Write(iconHeader.biPlanes);
			writer.Write(iconHeader.biBitCount);
			writer.Write(iconHeader.biCompression);
			writer.Write(iconHeader.biSizeImage);
			writer.Write(iconHeader.biXPelsPerMeter);
			writer.Write(iconHeader.biYPelsPerMeter);
			writer.Write(iconHeader.biClrUsed);
			writer.Write(iconHeader.biClrImportant);
			int num = ii.iconColors.Length;
			for (int i = 0; i < num; i++)
			{
				writer.Write(ii.iconColors[i]);
			}
			writer.Write(ii.iconXOR);
			writer.Write(ii.iconAND);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x0000FF0F File Offset: 0x0000E10F
		private void SaveIconDump(BinaryWriter writer, Icon.IconDump id)
		{
			writer.Write(id.data);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x0000FF20 File Offset: 0x0000E120
		private void SaveIconDirEntry(BinaryWriter writer, Icon.IconDirEntry ide, uint offset)
		{
			writer.Write(ide.width);
			writer.Write(ide.height);
			writer.Write(ide.colorCount);
			writer.Write(ide.reserved);
			writer.Write(ide.planes);
			writer.Write(ide.bitCount);
			writer.Write(ide.bytesInRes);
			writer.Write((offset == uint.MaxValue) ? ide.imageOffset : offset);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0000FF94 File Offset: 0x0000E194
		private void SaveAll(BinaryWriter writer)
		{
			writer.Write(this.iconDir.idReserved);
			writer.Write(this.iconDir.idType);
			ushort idCount = this.iconDir.idCount;
			writer.Write(idCount);
			for (int i = 0; i < (int)idCount; i++)
			{
				this.SaveIconDirEntry(writer, this.iconDir.idEntries[i], uint.MaxValue);
			}
			for (int j = 0; j < (int)idCount; j++)
			{
				while (writer.BaseStream.Length < (long)((ulong)this.iconDir.idEntries[j].imageOffset))
				{
					writer.Write(0);
				}
				if (this.imageData[j] is Icon.IconDump)
				{
					this.SaveIconDump(writer, (Icon.IconDump)this.imageData[j]);
				}
				else
				{
					this.SaveIconImage(writer, (Icon.IconImage)this.imageData[j]);
				}
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0001006C File Offset: 0x0000E26C
		private void SaveBestSingleIcon(BinaryWriter writer, int width, int height)
		{
			writer.Write(this.iconDir.idReserved);
			writer.Write(this.iconDir.idType);
			writer.Write(1);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < (int)this.iconDir.idCount; i++)
			{
				Icon.IconDirEntry iconDirEntry = this.iconDir.idEntries[i];
				if (width == (int)iconDirEntry.width && height == (int)iconDirEntry.height && (int)iconDirEntry.bitCount >= num2)
				{
					num2 = (int)iconDirEntry.bitCount;
					num = i;
				}
			}
			this.SaveIconDirEntry(writer, this.iconDir.idEntries[num], 22U);
			this.SaveIconImage(writer, (Icon.IconImage)this.imageData[num]);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00010120 File Offset: 0x0000E320
		private void SaveBitmapAsIcon(BinaryWriter writer)
		{
			writer.Write(0);
			writer.Write(1);
			writer.Write(1);
			Icon.IconDirEntry ide = default(Icon.IconDirEntry);
			ide.width = (byte)this.bitmap.Width;
			ide.height = (byte)this.bitmap.Height;
			ide.colorCount = 0;
			ide.reserved = 0;
			ide.planes = 0;
			ide.bitCount = 32;
			ide.imageOffset = 22U;
			Icon.BitmapInfoHeader bitmapInfoHeader = default(Icon.BitmapInfoHeader);
			bitmapInfoHeader.biSize = (uint)Marshal.SizeOf(typeof(Icon.BitmapInfoHeader));
			bitmapInfoHeader.biWidth = this.bitmap.Width;
			bitmapInfoHeader.biHeight = 2 * this.bitmap.Height;
			bitmapInfoHeader.biPlanes = 1;
			bitmapInfoHeader.biBitCount = 32;
			bitmapInfoHeader.biCompression = 0U;
			bitmapInfoHeader.biSizeImage = 0U;
			bitmapInfoHeader.biXPelsPerMeter = 0;
			bitmapInfoHeader.biYPelsPerMeter = 0;
			bitmapInfoHeader.biClrUsed = 0U;
			bitmapInfoHeader.biClrImportant = 0U;
			Icon.IconImage iconImage = new Icon.IconImage();
			iconImage.iconHeader = bitmapInfoHeader;
			iconImage.iconColors = new uint[0];
			int num = (((int)bitmapInfoHeader.biBitCount * this.bitmap.Width + 31 & -32) >> 3) * this.bitmap.Height;
			iconImage.iconXOR = new byte[num];
			int num2 = 0;
			for (int i = this.bitmap.Height - 1; i >= 0; i--)
			{
				for (int j = 0; j < this.bitmap.Width; j++)
				{
					Color pixel = this.bitmap.GetPixel(j, i);
					iconImage.iconXOR[num2++] = pixel.B;
					iconImage.iconXOR[num2++] = pixel.G;
					iconImage.iconXOR[num2++] = pixel.R;
					iconImage.iconXOR[num2++] = pixel.A;
				}
			}
			int num3 = ((this.Width + 31 & -32) >> 3) * this.bitmap.Height;
			iconImage.iconAND = new byte[num3];
			ide.bytesInRes = (uint)((ulong)bitmapInfoHeader.biSize + (ulong)((long)num) + (ulong)((long)num3));
			this.SaveIconDirEntry(writer, ide, uint.MaxValue);
			this.SaveIconImage(writer, iconImage);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00010360 File Offset: 0x0000E560
		private void Save(Stream outputStream, int width, int height)
		{
			BinaryWriter binaryWriter = new BinaryWriter(outputStream);
			if (this.iconDir.idEntries != null)
			{
				if (width == -1 && height == -1)
				{
					this.SaveAll(binaryWriter);
				}
				else
				{
					this.SaveBestSingleIcon(binaryWriter, width, height);
				}
			}
			else if (this.bitmap != null)
			{
				this.SaveBitmapAsIcon(binaryWriter);
			}
			binaryWriter.Flush();
		}

		/// <summary>Saves this <see cref="T:System.Drawing.Icon" /> to the specified output <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="outputStream">The <see cref="T:System.IO.Stream" /> to save to.</param>
		// Token: 0x060005CD RID: 1485 RVA: 0x000103B2 File Offset: 0x0000E5B2
		public void Save(Stream outputStream)
		{
			if (outputStream == null)
			{
				throw new NullReferenceException("outputStream");
			}
			this.Save(outputStream, -1, -1);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000103CC File Offset: 0x0000E5CC
		internal Bitmap BuildBitmapOnWin32()
		{
			if (this.imageData == null)
			{
				return new Bitmap(32, 32);
			}
			Icon.IconImage iconImage = (Icon.IconImage)this.imageData[(int)this.id];
			Icon.BitmapInfoHeader iconHeader = iconImage.iconHeader;
			int num = iconHeader.biHeight / 2;
			if (iconHeader.biClrUsed == 0U)
			{
				ushort biBitCount = iconHeader.biBitCount;
			}
			ushort biBitCount2 = iconHeader.biBitCount;
			Bitmap bitmap;
			if (biBitCount2 <= 4)
			{
				if (biBitCount2 == 1)
				{
					bitmap = new Bitmap(iconHeader.biWidth, num, PixelFormat.Format1bppIndexed);
					goto IL_FB;
				}
				if (biBitCount2 == 4)
				{
					bitmap = new Bitmap(iconHeader.biWidth, num, PixelFormat.Format4bppIndexed);
					goto IL_FB;
				}
			}
			else
			{
				if (biBitCount2 == 8)
				{
					bitmap = new Bitmap(iconHeader.biWidth, num, PixelFormat.Format8bppIndexed);
					goto IL_FB;
				}
				if (biBitCount2 == 24)
				{
					bitmap = new Bitmap(iconHeader.biWidth, num, PixelFormat.Format24bppRgb);
					goto IL_FB;
				}
				if (biBitCount2 == 32)
				{
					bitmap = new Bitmap(iconHeader.biWidth, num, PixelFormat.Format32bppArgb);
					goto IL_FB;
				}
			}
			throw new Exception(Locale.GetText("Unexpected number of bits: {0}", new object[]
			{
				iconHeader.biBitCount
			}));
			IL_FB:
			if (iconHeader.biBitCount < 24)
			{
				ColorPalette palette = bitmap.Palette;
				for (int i = 0; i < iconImage.iconColors.Length; i++)
				{
					palette.Entries[i] = Color.FromArgb((int)(iconImage.iconColors[i] | 4278190080U));
				}
				bitmap.Palette = palette;
			}
			int num2 = (iconHeader.biWidth * (int)iconHeader.biBitCount + 31 & -32) >> 3;
			BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
			for (int j = 0; j < num; j++)
			{
				Marshal.Copy(iconImage.iconXOR, num2 * j, (IntPtr)(bitmapData.Scan0.ToInt64() + (long)(bitmapData.Stride * (num - 1 - j))), num2);
			}
			bitmap.UnlockBits(bitmapData);
			bitmap = new Bitmap(bitmap);
			num2 = (iconHeader.biWidth + 31 & -32) >> 3;
			for (int k = 0; k < num; k++)
			{
				for (int l = 0; l < iconHeader.biWidth / 8; l++)
				{
					for (int m = 7; m >= 0; m--)
					{
						if ((iconImage.iconAND[k * num2 + l] >> m & 1) != 0)
						{
							bitmap.SetPixel(l * 8 + 7 - m, num - k - 1, Color.Transparent);
						}
					}
				}
			}
			return bitmap;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00010634 File Offset: 0x0000E834
		internal Bitmap GetInternalBitmap()
		{
			if (this.bitmap == null)
			{
				if (GDIPlus.RunningOnUnix())
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						this.Save(memoryStream, this.Width, this.Height);
						memoryStream.Position = 0L;
						this.bitmap = (Bitmap)Image.LoadFromStream(memoryStream, false);
						goto IL_5A;
					}
				}
				this.bitmap = this.BuildBitmapOnWin32();
			}
			IL_5A:
			return this.bitmap;
		}

		/// <summary>Converts this <see cref="T:System.Drawing.Icon" /> to a GDI+ <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Bitmap" /> that represents the converted <see cref="T:System.Drawing.Icon" />.</returns>
		// Token: 0x060005D0 RID: 1488 RVA: 0x000106B4 File Offset: 0x0000E8B4
		public Bitmap ToBitmap()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Icon instance was disposed."));
			}
			return new Bitmap(this.GetInternalBitmap());
		}

		/// <summary>Gets a human-readable string that describes the <see cref="T:System.Drawing.Icon" />.</summary>
		/// <returns>A string that describes the <see cref="T:System.Drawing.Icon" />.</returns>
		// Token: 0x060005D1 RID: 1489 RVA: 0x000106D9 File Offset: 0x0000E8D9
		public override string ToString()
		{
			return "<Icon>";
		}

		/// <summary>Gets the Windows handle for this <see cref="T:System.Drawing.Icon" />. This is not a copy of the handle; do not free it.</summary>
		/// <returns>The Windows handle for the icon.</returns>
		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x000106E0 File Offset: 0x0000E8E0
		[Browsable(false)]
		public IntPtr Handle
		{
			get
			{
				if (!this.disposed && this.handle == IntPtr.Zero)
				{
					if (GDIPlus.RunningOnUnix())
					{
						this.handle = this.GetInternalBitmap().NativeObject;
					}
					else
					{
						IconInfo iconInfo = default(IconInfo);
						iconInfo.IsIcon = true;
						iconInfo.hbmColor = this.ToBitmap().GetHbitmap();
						iconInfo.hbmMask = iconInfo.hbmColor;
						this.handle = GDIPlus.CreateIconIndirect(ref iconInfo);
					}
				}
				return this.handle;
			}
		}

		/// <summary>Gets the height of this <see cref="T:System.Drawing.Icon" />.</summary>
		/// <returns>The height of this <see cref="T:System.Drawing.Icon" />.</returns>
		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00010763 File Offset: 0x0000E963
		[Browsable(false)]
		public int Height
		{
			get
			{
				return this.iconSize.Height;
			}
		}

		/// <summary>Gets the size of this <see cref="T:System.Drawing.Icon" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure that specifies the width and height of this <see cref="T:System.Drawing.Icon" />.</returns>
		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x00010770 File Offset: 0x0000E970
		public Size Size
		{
			get
			{
				return this.iconSize;
			}
		}

		/// <summary>Gets the width of this <see cref="T:System.Drawing.Icon" />.</summary>
		/// <returns>The width of this <see cref="T:System.Drawing.Icon" />.</returns>
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x00010778 File Offset: 0x0000E978
		[Browsable(false)]
		public int Width
		{
			get
			{
				return this.iconSize.Width;
			}
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x060005D6 RID: 1494 RVA: 0x00010788 File Offset: 0x0000E988
		~Icon()
		{
			this.Dispose();
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000107B4 File Offset: 0x0000E9B4
		private void InitFromStreamWithSize(Stream stream, int width, int height)
		{
			if (stream == null || stream.Length == 0L)
			{
				throw new ArgumentException("The argument 'stream' must be a picture that can be used as a Icon", "stream");
			}
			BinaryReader binaryReader = new BinaryReader(stream);
			this.iconDir.idReserved = binaryReader.ReadUInt16();
			if (this.iconDir.idReserved != 0)
			{
				throw new ArgumentException("Invalid Argument", "stream");
			}
			this.iconDir.idType = binaryReader.ReadUInt16();
			if (this.iconDir.idType != 1)
			{
				throw new ArgumentException("Invalid Argument", "stream");
			}
			ushort num = binaryReader.ReadUInt16();
			this.imageData = new Icon.ImageData[(int)num];
			this.iconDir.idCount = num;
			this.iconDir.idEntries = new Icon.IconDirEntry[(int)num];
			bool flag = false;
			for (int i = 0; i < (int)num; i++)
			{
				Icon.IconDirEntry iconDirEntry;
				iconDirEntry.width = binaryReader.ReadByte();
				iconDirEntry.height = binaryReader.ReadByte();
				iconDirEntry.colorCount = binaryReader.ReadByte();
				iconDirEntry.reserved = binaryReader.ReadByte();
				iconDirEntry.planes = binaryReader.ReadUInt16();
				iconDirEntry.bitCount = binaryReader.ReadUInt16();
				iconDirEntry.bytesInRes = binaryReader.ReadUInt32();
				iconDirEntry.imageOffset = binaryReader.ReadUInt32();
				if (iconDirEntry.width == 0 && iconDirEntry.height == 0)
				{
					iconDirEntry.ignore = true;
				}
				else
				{
					iconDirEntry.ignore = false;
				}
				this.iconDir.idEntries[i] = iconDirEntry;
				if (!flag && ((int)iconDirEntry.height == height || (int)iconDirEntry.width == width) && !iconDirEntry.ignore)
				{
					this.id = (ushort)i;
					flag = true;
					this.iconSize.Height = (int)iconDirEntry.height;
					this.iconSize.Width = (int)iconDirEntry.width;
				}
			}
			int num2 = 0;
			for (int j = 0; j < (int)num; j++)
			{
				if (!this.iconDir.idEntries[j].ignore)
				{
					num2++;
				}
			}
			if (num2 == 0)
			{
				throw new Win32Exception(0, "No valid icon entry were found.");
			}
			if (!flag)
			{
				uint num3 = 0U;
				for (int k = 0; k < (int)num; k++)
				{
					if (this.iconDir.idEntries[k].bytesInRes >= num3 && !this.iconDir.idEntries[k].ignore)
					{
						num3 = this.iconDir.idEntries[k].bytesInRes;
						this.id = (ushort)k;
						this.iconSize.Height = (int)this.iconDir.idEntries[k].height;
						this.iconSize.Width = (int)this.iconDir.idEntries[k].width;
					}
				}
			}
			for (int l = 0; l < (int)num; l++)
			{
				if (this.iconDir.idEntries[l].ignore)
				{
					Icon.IconDump iconDump = new Icon.IconDump();
					stream.Seek((long)((ulong)this.iconDir.idEntries[l].imageOffset), SeekOrigin.Begin);
					iconDump.data = new byte[this.iconDir.idEntries[l].bytesInRes];
					stream.Read(iconDump.data, 0, iconDump.data.Length);
					this.imageData[l] = iconDump;
				}
				else
				{
					Icon.IconImage iconImage = new Icon.IconImage();
					Icon.BitmapInfoHeader bitmapInfoHeader = default(Icon.BitmapInfoHeader);
					stream.Seek((long)((ulong)this.iconDir.idEntries[l].imageOffset), SeekOrigin.Begin);
					byte[] array = new byte[this.iconDir.idEntries[l].bytesInRes];
					stream.Read(array, 0, array.Length);
					BinaryReader binaryReader2 = new BinaryReader(new MemoryStream(array));
					bitmapInfoHeader.biSize = binaryReader2.ReadUInt32();
					bitmapInfoHeader.biWidth = binaryReader2.ReadInt32();
					bitmapInfoHeader.biHeight = binaryReader2.ReadInt32();
					bitmapInfoHeader.biPlanes = binaryReader2.ReadUInt16();
					bitmapInfoHeader.biBitCount = binaryReader2.ReadUInt16();
					bitmapInfoHeader.biCompression = binaryReader2.ReadUInt32();
					bitmapInfoHeader.biSizeImage = binaryReader2.ReadUInt32();
					bitmapInfoHeader.biXPelsPerMeter = binaryReader2.ReadInt32();
					bitmapInfoHeader.biYPelsPerMeter = binaryReader2.ReadInt32();
					bitmapInfoHeader.biClrUsed = binaryReader2.ReadUInt32();
					bitmapInfoHeader.biClrImportant = binaryReader2.ReadUInt32();
					iconImage.iconHeader = bitmapInfoHeader;
					ushort biBitCount = bitmapInfoHeader.biBitCount;
					int num4;
					if (biBitCount != 1)
					{
						if (biBitCount != 4)
						{
							if (biBitCount != 8)
							{
								num4 = 0;
							}
							else
							{
								num4 = 256;
							}
						}
						else
						{
							num4 = 16;
						}
					}
					else
					{
						num4 = 2;
					}
					iconImage.iconColors = new uint[num4];
					for (int m = 0; m < num4; m++)
					{
						iconImage.iconColors[m] = binaryReader2.ReadUInt32();
					}
					int num5 = bitmapInfoHeader.biHeight / 2;
					int num6 = (bitmapInfoHeader.biWidth * (int)bitmapInfoHeader.biPlanes * (int)bitmapInfoHeader.biBitCount + 31 >> 5 << 2) * num5;
					iconImage.iconXOR = new byte[num6];
					int num7 = binaryReader2.Read(iconImage.iconXOR, 0, num6);
					if (num7 != num6)
					{
						throw new ArgumentException(Locale.GetText("{0} data length expected {1}, read {2}", new object[]
						{
							"XOR",
							num6,
							num7
						}), "stream");
					}
					int num8 = ((bitmapInfoHeader.biWidth + 31 & -32) >> 3) * num5;
					iconImage.iconAND = new byte[num8];
					num7 = binaryReader2.Read(iconImage.iconAND, 0, num8);
					if (num7 != num8)
					{
						throw new ArgumentException(Locale.GetText("{0} data length expected {1}, read {2}", new object[]
						{
							"AND",
							num8,
							num7
						}), "stream");
					}
					this.imageData[l] = iconImage;
					binaryReader2.Dispose();
				}
			}
			binaryReader.Dispose();
		}

		// Token: 0x040004A4 RID: 1188
		private Size iconSize;

		// Token: 0x040004A5 RID: 1189
		private IntPtr handle = IntPtr.Zero;

		// Token: 0x040004A6 RID: 1190
		private Icon.IconDir iconDir;

		// Token: 0x040004A7 RID: 1191
		private ushort id;

		// Token: 0x040004A8 RID: 1192
		private Icon.ImageData[] imageData;

		// Token: 0x040004A9 RID: 1193
		private bool undisposable;

		// Token: 0x040004AA RID: 1194
		private bool disposed;

		// Token: 0x040004AB RID: 1195
		private Bitmap bitmap;

		// Token: 0x02000076 RID: 118
		internal struct IconDirEntry
		{
			// Token: 0x040004AC RID: 1196
			internal byte width;

			// Token: 0x040004AD RID: 1197
			internal byte height;

			// Token: 0x040004AE RID: 1198
			internal byte colorCount;

			// Token: 0x040004AF RID: 1199
			internal byte reserved;

			// Token: 0x040004B0 RID: 1200
			internal ushort planes;

			// Token: 0x040004B1 RID: 1201
			internal ushort bitCount;

			// Token: 0x040004B2 RID: 1202
			internal uint bytesInRes;

			// Token: 0x040004B3 RID: 1203
			internal uint imageOffset;

			// Token: 0x040004B4 RID: 1204
			internal bool ignore;
		}

		// Token: 0x02000077 RID: 119
		internal struct IconDir
		{
			// Token: 0x040004B5 RID: 1205
			internal ushort idReserved;

			// Token: 0x040004B6 RID: 1206
			internal ushort idType;

			// Token: 0x040004B7 RID: 1207
			internal ushort idCount;

			// Token: 0x040004B8 RID: 1208
			internal Icon.IconDirEntry[] idEntries;
		}

		// Token: 0x02000078 RID: 120
		internal struct BitmapInfoHeader
		{
			// Token: 0x040004B9 RID: 1209
			internal uint biSize;

			// Token: 0x040004BA RID: 1210
			internal int biWidth;

			// Token: 0x040004BB RID: 1211
			internal int biHeight;

			// Token: 0x040004BC RID: 1212
			internal ushort biPlanes;

			// Token: 0x040004BD RID: 1213
			internal ushort biBitCount;

			// Token: 0x040004BE RID: 1214
			internal uint biCompression;

			// Token: 0x040004BF RID: 1215
			internal uint biSizeImage;

			// Token: 0x040004C0 RID: 1216
			internal int biXPelsPerMeter;

			// Token: 0x040004C1 RID: 1217
			internal int biYPelsPerMeter;

			// Token: 0x040004C2 RID: 1218
			internal uint biClrUsed;

			// Token: 0x040004C3 RID: 1219
			internal uint biClrImportant;
		}

		// Token: 0x02000079 RID: 121
		[StructLayout(LayoutKind.Sequential)]
		internal abstract class ImageData
		{
			// Token: 0x060005D8 RID: 1496 RVA: 0x00002050 File Offset: 0x00000250
			protected ImageData()
			{
			}
		}

		// Token: 0x0200007A RID: 122
		[StructLayout(LayoutKind.Sequential)]
		internal class IconImage : Icon.ImageData
		{
			// Token: 0x060005D9 RID: 1497 RVA: 0x00010D92 File Offset: 0x0000EF92
			public IconImage()
			{
			}

			// Token: 0x040004C4 RID: 1220
			internal Icon.BitmapInfoHeader iconHeader;

			// Token: 0x040004C5 RID: 1221
			internal uint[] iconColors;

			// Token: 0x040004C6 RID: 1222
			internal byte[] iconXOR;

			// Token: 0x040004C7 RID: 1223
			internal byte[] iconAND;
		}

		// Token: 0x0200007B RID: 123
		[StructLayout(LayoutKind.Sequential)]
		internal class IconDump : Icon.ImageData
		{
			// Token: 0x060005DA RID: 1498 RVA: 0x00010D92 File Offset: 0x0000EF92
			public IconDump()
			{
			}

			// Token: 0x040004C8 RID: 1224
			internal byte[] data;
		}
	}
}
