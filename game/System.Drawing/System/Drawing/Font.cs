using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Drawing
{
	/// <summary>Defines a particular format for text, including font face, size, and style attributes. This class cannot be inherited.</summary>
	// Token: 0x0200006C RID: 108
	[TypeConverter(typeof(FontConverter))]
	[Editor("System.Drawing.Design.FontEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[ComVisible(true)]
	[Serializable]
	public sealed class Font : MarshalByRefObject, ISerializable, ICloneable, IDisposable
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x0000AF60 File Offset: 0x00009160
		private void CreateFont(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte charSet, bool isVertical)
		{
			this.originalFontName = familyName;
			FontFamily fontFamily;
			try
			{
				fontFamily = new FontFamily(familyName);
			}
			catch (Exception)
			{
				fontFamily = FontFamily.GenericSansSerif;
			}
			this.setProperties(fontFamily, emSize, style, unit, charSet, isVertical);
			Status status = GDIPlus.GdipCreateFont(fontFamily.NativeFamily, emSize, style, unit, out this.fontObject);
			if (status == Status.FontStyleNotFound)
			{
				throw new ArgumentException(Locale.GetText("Style {0} isn't supported by font {1}.", new object[]
				{
					style.ToString(),
					familyName
				}));
			}
			GDIPlus.CheckStatus(status);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000AFF0 File Offset: 0x000091F0
		private Font(SerializationInfo info, StreamingContext context)
		{
			string familyName = (string)info.GetValue("Name", typeof(string));
			float emSize = (float)info.GetValue("Size", typeof(float));
			FontStyle style = (FontStyle)info.GetValue("Style", typeof(FontStyle));
			GraphicsUnit unit = (GraphicsUnit)info.GetValue("Unit", typeof(GraphicsUnit));
			this.CreateFont(familyName, emSize, style, unit, 1, false);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x06000445 RID: 1093 RVA: 0x0000B088 File Offset: 0x00009288
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			si.AddValue("Name", this.Name);
			si.AddValue("Size", this.Size);
			si.AddValue("Style", this.Style);
			si.AddValue("Unit", this.Unit);
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x06000446 RID: 1094 RVA: 0x0000B0E4 File Offset: 0x000092E4
		~Font()
		{
			this.Dispose();
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Font" />.</summary>
		// Token: 0x06000447 RID: 1095 RVA: 0x0000B110 File Offset: 0x00009310
		public void Dispose()
		{
			if (this.fontObject != IntPtr.Zero)
			{
				Status status = GDIPlus.GdipDeleteFont(this.fontObject);
				this.fontObject = IntPtr.Zero;
				GC.SuppressFinalize(this);
				GDIPlus.CheckStatus(status);
			}
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000B145 File Offset: 0x00009345
		internal void SetSystemFontName(string newSystemFontName)
		{
			this.systemFontName = newSystemFontName;
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000B150 File Offset: 0x00009350
		internal void unitConversion(GraphicsUnit fromUnit, GraphicsUnit toUnit, float nSrc, out float nTrg)
		{
			nTrg = 0f;
			float num;
			switch (fromUnit)
			{
			case GraphicsUnit.World:
			case GraphicsUnit.Pixel:
				num = nSrc / Graphics.systemDpiX;
				break;
			case GraphicsUnit.Display:
				num = nSrc / 75f;
				break;
			case GraphicsUnit.Point:
				num = nSrc / 72f;
				break;
			case GraphicsUnit.Inch:
				num = nSrc;
				break;
			case GraphicsUnit.Document:
				num = nSrc / 300f;
				break;
			case GraphicsUnit.Millimeter:
				num = nSrc / 25.4f;
				break;
			default:
				throw new ArgumentException("Invalid GraphicsUnit");
			}
			switch (toUnit)
			{
			case GraphicsUnit.World:
			case GraphicsUnit.Pixel:
				nTrg = num * Graphics.systemDpiX;
				return;
			case GraphicsUnit.Display:
				nTrg = num * 75f;
				return;
			case GraphicsUnit.Point:
				nTrg = num * 72f;
				return;
			case GraphicsUnit.Inch:
				nTrg = num;
				return;
			case GraphicsUnit.Document:
				nTrg = num * 300f;
				return;
			case GraphicsUnit.Millimeter:
				nTrg = num * 25.4f;
				return;
			default:
				throw new ArgumentException("Invalid GraphicsUnit");
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000B23C File Offset: 0x0000943C
		private void setProperties(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte charSet, bool isVertical)
		{
			this._name = family.Name;
			this._fontFamily = family;
			this._size = emSize;
			this._unit = unit;
			this._style = style;
			this._gdiCharSet = charSet;
			this._gdiVerticalFont = isVertical;
			this.unitConversion(unit, GraphicsUnit.Point, emSize, out this._sizeInPoints);
			this._bold = (this._italic = (this._strikeout = (this._underline = false)));
			if ((style & FontStyle.Bold) == FontStyle.Bold)
			{
				this._bold = true;
			}
			if ((style & FontStyle.Italic) == FontStyle.Italic)
			{
				this._italic = true;
			}
			if ((style & FontStyle.Strikeout) == FontStyle.Strikeout)
			{
				this._strikeout = true;
			}
			if ((style & FontStyle.Underline) == FontStyle.Underline)
			{
				this._underline = true;
			}
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Font" /> from the specified Windows handle.</summary>
		/// <param name="hfont">A Windows handle to a GDI font.</param>
		/// <returns>The <see cref="T:System.Drawing.Font" /> this method creates.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="hfont" /> points to an object that is not a TrueType font.</exception>
		// Token: 0x0600044B RID: 1099 RVA: 0x0000B2E8 File Offset: 0x000094E8
		public static Font FromHfont(IntPtr hfont)
		{
			FontStyle fontStyle = FontStyle.Regular;
			LOGFONT logfont = default(LOGFONT);
			if (hfont == IntPtr.Zero)
			{
				return new Font("Arial", 10f, FontStyle.Regular);
			}
			IntPtr newFontObject;
			if (GDIPlus.RunningOnUnix())
			{
				GDIPlus.CheckStatus(GDIPlus.GdipCreateFontFromHfont(hfont, out newFontObject, ref logfont));
			}
			else
			{
				IntPtr dc = GDIPlus.GetDC(IntPtr.Zero);
				try
				{
					return Font.FromLogFont(logfont, dc);
				}
				finally
				{
					GDIPlus.ReleaseDC(IntPtr.Zero, dc);
				}
			}
			if (logfont.lfItalic != 0)
			{
				fontStyle |= FontStyle.Italic;
			}
			if (logfont.lfUnderline != 0)
			{
				fontStyle |= FontStyle.Underline;
			}
			if (logfont.lfStrikeOut != 0)
			{
				fontStyle |= FontStyle.Strikeout;
			}
			if (logfont.lfWeight > 400U)
			{
				fontStyle |= FontStyle.Bold;
			}
			float size;
			if (logfont.lfHeight < 0)
			{
				size = (float)(logfont.lfHeight * -1);
			}
			else
			{
				size = (float)logfont.lfHeight;
			}
			return new Font(newFontObject, logfont.lfFaceName, fontStyle, size);
		}

		/// <summary>Returns a handle to this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>A Windows handle to this <see cref="T:System.Drawing.Font" />.</returns>
		/// <exception cref="T:System.ComponentModel.Win32Exception">The operation was unsuccessful.</exception>
		// Token: 0x0600044C RID: 1100 RVA: 0x0000B3DC File Offset: 0x000095DC
		public IntPtr ToHfont()
		{
			if (this.fontObject == IntPtr.Zero)
			{
				throw new ArgumentException(Locale.GetText("Object has been disposed."));
			}
			if (GDIPlus.RunningOnUnix())
			{
				return this.fontObject;
			}
			if (this.olf == null)
			{
				this.olf = default(LOGFONT);
				this.ToLogFont(this.olf);
			}
			LOGFONT logfont = (LOGFONT)this.olf;
			return GDIPlus.CreateFontIndirect(ref logfont);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000B454 File Offset: 0x00009654
		internal Font(IntPtr newFontObject, string familyName, FontStyle style, float size)
		{
			FontFamily family;
			try
			{
				family = new FontFamily(familyName);
			}
			catch (Exception)
			{
				family = FontFamily.GenericSansSerif;
			}
			this.setProperties(family, size, style, GraphicsUnit.Pixel, 0, false);
			this.fontObject = newFontObject;
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> that uses the specified existing <see cref="T:System.Drawing.Font" /> and <see cref="T:System.Drawing.FontStyle" /> enumeration.</summary>
		/// <param name="prototype">The existing <see cref="T:System.Drawing.Font" /> from which to create the new <see cref="T:System.Drawing.Font" />.</param>
		/// <param name="newStyle">The <see cref="T:System.Drawing.FontStyle" /> to apply to the new <see cref="T:System.Drawing.Font" />. Multiple values of the <see cref="T:System.Drawing.FontStyle" /> enumeration can be combined with the <see langword="OR" /> operator.</param>
		// Token: 0x0600044E RID: 1102 RVA: 0x0000B4A8 File Offset: 0x000096A8
		public Font(Font prototype, FontStyle newStyle)
		{
			this.setProperties(prototype.FontFamily, prototype.Size, newStyle, prototype.Unit, prototype.GdiCharSet, prototype.GdiVerticalFont);
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFont(this._fontFamily.NativeFamily, this.Size, this.Style, this.Unit, out this.fontObject));
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size and unit. Sets the style to <see cref="F:System.Drawing.FontStyle.Regular" />.</summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />.</param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter.</param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="family" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
		// Token: 0x0600044F RID: 1103 RVA: 0x0000B518 File Offset: 0x00009718
		public Font(FontFamily family, float emSize, GraphicsUnit unit) : this(family, emSize, FontStyle.Regular, unit, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size and unit. The style is set to <see cref="F:System.Drawing.FontStyle.Regular" />.</summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />.</param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter.</param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
		// Token: 0x06000450 RID: 1104 RVA: 0x0000B526 File Offset: 0x00009726
		public Font(string familyName, float emSize, GraphicsUnit unit) : this(new FontFamily(familyName), emSize, FontStyle.Regular, unit, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size.</summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />.</param>
		/// <param name="emSize">The em-size, in points, of the new font.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
		// Token: 0x06000451 RID: 1105 RVA: 0x0000B539 File Offset: 0x00009739
		public Font(FontFamily family, float emSize) : this(family, emSize, FontStyle.Regular, GraphicsUnit.Point, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size and style.</summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />.</param>
		/// <param name="emSize">The em-size, in points, of the new font.</param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="family" /> is <see langword="null" />.</exception>
		// Token: 0x06000452 RID: 1106 RVA: 0x0000B547 File Offset: 0x00009747
		public Font(FontFamily family, float emSize, FontStyle style) : this(family, emSize, style, GraphicsUnit.Point, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, and unit.</summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />.</param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter.</param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font.</param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="family" /> is <see langword="null" />.</exception>
		// Token: 0x06000453 RID: 1107 RVA: 0x0000B555 File Offset: 0x00009755
		public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit) : this(family, emSize, style, unit, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, unit, and character set.</summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />.</param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter.</param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font.</param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font.</param>
		/// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a  
		///  GDI character set to use for the new font.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="family" /> is <see langword="null" />.</exception>
		// Token: 0x06000454 RID: 1108 RVA: 0x0000B564 File Offset: 0x00009764
		public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet) : this(family, emSize, style, unit, gdiCharSet, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, unit, and character set.</summary>
		/// <param name="family">The <see cref="T:System.Drawing.FontFamily" /> of the new <see cref="T:System.Drawing.Font" />.</param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter.</param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font.</param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font.</param>
		/// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a  
		///  GDI character set to use for this font.</param>
		/// <param name="gdiVerticalFont">A Boolean value indicating whether the new font is derived from a GDI vertical font.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="family" /> is <see langword="null" /></exception>
		// Token: 0x06000455 RID: 1109 RVA: 0x0000B574 File Offset: 0x00009774
		public Font(FontFamily family, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
		{
			if (family == null)
			{
				throw new ArgumentNullException("family");
			}
			this.setProperties(family, emSize, style, unit, gdiCharSet, gdiVerticalFont);
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFont(family.NativeFamily, emSize, style, unit, out this.fontObject));
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size.</summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />.</param>
		/// <param name="emSize">The em-size, in points, of the new font.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity or is not a valid number.</exception>
		// Token: 0x06000456 RID: 1110 RVA: 0x0000B5C9 File Offset: 0x000097C9
		public Font(string familyName, float emSize) : this(familyName, emSize, FontStyle.Regular, GraphicsUnit.Point, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size and style.</summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />.</param>
		/// <param name="emSize">The em-size, in points, of the new font.</param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
		// Token: 0x06000457 RID: 1111 RVA: 0x0000B5D7 File Offset: 0x000097D7
		public Font(string familyName, float emSize, FontStyle style) : this(familyName, emSize, style, GraphicsUnit.Point, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, and unit.</summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />.</param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter.</param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font.</param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity or is not a valid number.</exception>
		// Token: 0x06000458 RID: 1112 RVA: 0x0000B5E5 File Offset: 0x000097E5
		public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit) : this(familyName, emSize, style, unit, 1, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using a specified size, style, unit, and character set.</summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />.</param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter.</param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font.</param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font.</param>
		/// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a GDI character set to use for this font.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
		// Token: 0x06000459 RID: 1113 RVA: 0x0000B5F4 File Offset: 0x000097F4
		public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet) : this(familyName, emSize, style, unit, gdiCharSet, false)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.Font" /> using the specified size, style, unit, and character set.</summary>
		/// <param name="familyName">A string representation of the <see cref="T:System.Drawing.FontFamily" /> for the new <see cref="T:System.Drawing.Font" />.</param>
		/// <param name="emSize">The em-size of the new font in the units specified by the <paramref name="unit" /> parameter.</param>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> of the new font.</param>
		/// <param name="unit">The <see cref="T:System.Drawing.GraphicsUnit" /> of the new font.</param>
		/// <param name="gdiCharSet">A <see cref="T:System.Byte" /> that specifies a GDI character set to use for this font.</param>
		/// <param name="gdiVerticalFont">A Boolean value indicating whether the new <see cref="T:System.Drawing.Font" /> is derived from a GDI vertical font.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="emSize" /> is less than or equal to 0, evaluates to infinity, or is not a valid number.</exception>
		// Token: 0x0600045A RID: 1114 RVA: 0x0000B604 File Offset: 0x00009804
		public Font(string familyName, float emSize, FontStyle style, GraphicsUnit unit, byte gdiCharSet, bool gdiVerticalFont)
		{
			this.CreateFont(familyName, emSize, style, unit, gdiCharSet, gdiVerticalFont);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000B626 File Offset: 0x00009826
		internal Font(string familyName, float emSize, string systemName) : this(familyName, emSize, FontStyle.Regular, GraphicsUnit.Point, 1, false)
		{
			this.systemFontName = systemName;
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> this method creates, cast as an <see cref="T:System.Object" />.</returns>
		// Token: 0x0600045C RID: 1116 RVA: 0x0000B63B File Offset: 0x0000983B
		public object Clone()
		{
			return new Font(this, this.Style);
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000B649 File Offset: 0x00009849
		internal IntPtr NativeObject
		{
			get
			{
				return this.fontObject;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Drawing.Font" /> is bold.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Drawing.Font" /> is bold; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000B651 File Offset: 0x00009851
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Bold
		{
			get
			{
				return this._bold;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.FontFamily" /> associated with this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.FontFamily" /> associated with this <see cref="T:System.Drawing.Font" />.</returns>
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000B659 File Offset: 0x00009859
		[Browsable(false)]
		public FontFamily FontFamily
		{
			get
			{
				return this._fontFamily;
			}
		}

		/// <summary>Gets a byte value that specifies the GDI character set that this <see cref="T:System.Drawing.Font" /> uses.</summary>
		/// <returns>A byte value that specifies the GDI character set that this <see cref="T:System.Drawing.Font" /> uses. The default is 1.</returns>
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000B661 File Offset: 0x00009861
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public byte GdiCharSet
		{
			get
			{
				return this._gdiCharSet;
			}
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Drawing.Font" /> is derived from a GDI vertical font.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Drawing.Font" /> is derived from a GDI vertical font; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000B669 File Offset: 0x00009869
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool GdiVerticalFont
		{
			get
			{
				return this._gdiVerticalFont;
			}
		}

		/// <summary>Gets the line spacing of this font.</summary>
		/// <returns>The line spacing, in pixels, of this font.</returns>
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000B671 File Offset: 0x00009871
		[Browsable(false)]
		public int Height
		{
			get
			{
				return (int)Math.Ceiling((double)this.GetHeight());
			}
		}

		/// <summary>Gets a value indicating whether the font is a member of <see cref="T:System.Drawing.SystemFonts" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the font is a member of <see cref="T:System.Drawing.SystemFonts" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000B680 File Offset: 0x00009880
		[Browsable(false)]
		public bool IsSystemFont
		{
			get
			{
				return !string.IsNullOrEmpty(this.systemFontName);
			}
		}

		/// <summary>Gets a value that indicates whether this font has the italic style applied.</summary>
		/// <returns>
		///   <see langword="true" /> to indicate this font has the italic style applied; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000B690 File Offset: 0x00009890
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Italic
		{
			get
			{
				return this._italic;
			}
		}

		/// <summary>Gets the face name of this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>A string representation of the face name of this <see cref="T:System.Drawing.Font" />.</returns>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000B698 File Offset: 0x00009898
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[TypeConverter(typeof(FontConverter.FontNameConverter))]
		[Editor("System.Drawing.Design.FontNameEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Gets the em-size of this <see cref="T:System.Drawing.Font" /> measured in the units specified by the <see cref="P:System.Drawing.Font.Unit" /> property.</summary>
		/// <returns>The em-size of this <see cref="T:System.Drawing.Font" />.</returns>
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0000B6A0 File Offset: 0x000098A0
		public float Size
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets the em-size, in points, of this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>The em-size, in points, of this <see cref="T:System.Drawing.Font" />.</returns>
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000B6A8 File Offset: 0x000098A8
		[Browsable(false)]
		public float SizeInPoints
		{
			get
			{
				return this._sizeInPoints;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Drawing.Font" /> specifies a horizontal line through the font.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Drawing.Font" /> has a horizontal line through it; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0000B6B0 File Offset: 0x000098B0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Strikeout
		{
			get
			{
				return this._strikeout;
			}
		}

		/// <summary>Gets style information for this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.FontStyle" /> enumeration that contains style information for this <see cref="T:System.Drawing.Font" />.</returns>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000B6B8 File Offset: 0x000098B8
		[Browsable(false)]
		public FontStyle Style
		{
			get
			{
				return this._style;
			}
		}

		/// <summary>Gets the name of the system font if the <see cref="P:System.Drawing.Font.IsSystemFont" /> property returns <see langword="true" />.</summary>
		/// <returns>The name of the system font, if <see cref="P:System.Drawing.Font.IsSystemFont" /> returns <see langword="true" />; otherwise, an empty string ("").</returns>
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000B6C0 File Offset: 0x000098C0
		[Browsable(false)]
		public string SystemFontName
		{
			get
			{
				return this.systemFontName;
			}
		}

		/// <summary>Gets the name of the font originally specified.</summary>
		/// <returns>The string representing the name of the font originally specified.</returns>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000B6C8 File Offset: 0x000098C8
		[Browsable(false)]
		public string OriginalFontName
		{
			get
			{
				return this.originalFontName;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Drawing.Font" /> is underlined.</summary>
		/// <returns>
		///   <see langword="true" /> if this <see cref="T:System.Drawing.Font" /> is underlined; otherwise, <see langword="false" />.</returns>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000B6D0 File Offset: 0x000098D0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Underline
		{
			get
			{
				return this._underline;
			}
		}

		/// <summary>Gets the unit of measure for this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.GraphicsUnit" /> that represents the unit of measure for this <see cref="T:System.Drawing.Font" />.</returns>
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000B6D8 File Offset: 0x000098D8
		[TypeConverter(typeof(FontConverter.FontUnitConverter))]
		public GraphicsUnit Unit
		{
			get
			{
				return this._unit;
			}
		}

		/// <summary>Indicates whether the specified object is a <see cref="T:System.Drawing.Font" /> and has the same <see cref="P:System.Drawing.Font.FontFamily" />, <see cref="P:System.Drawing.Font.GdiVerticalFont" />, <see cref="P:System.Drawing.Font.GdiCharSet" />, <see cref="P:System.Drawing.Font.Style" />, <see cref="P:System.Drawing.Font.Size" />, and <see cref="P:System.Drawing.Font.Unit" /> property values as this <see cref="T:System.Drawing.Font" />.</summary>
		/// <param name="obj">The object to test.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="obj" /> parameter is a <see cref="T:System.Drawing.Font" /> and has the same <see cref="P:System.Drawing.Font.FontFamily" />, <see cref="P:System.Drawing.Font.GdiVerticalFont" />, <see cref="P:System.Drawing.Font.GdiCharSet" />, <see cref="P:System.Drawing.Font.Style" />, <see cref="P:System.Drawing.Font.Size" />, and <see cref="P:System.Drawing.Font.Unit" /> property values as this <see cref="T:System.Drawing.Font" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600046E RID: 1134 RVA: 0x0000B6E0 File Offset: 0x000098E0
		public override bool Equals(object obj)
		{
			Font font = obj as Font;
			return font != null && (font.FontFamily.Equals(this.FontFamily) && font.Size == this.Size && font.Style == this.Style && font.Unit == this.Unit && font.GdiCharSet == this.GdiCharSet && font.GdiVerticalFont == this.GdiVerticalFont);
		}

		/// <summary>Gets the hash code for this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>The hash code for this <see cref="T:System.Drawing.Font" />.</returns>
		// Token: 0x0600046F RID: 1135 RVA: 0x0000B758 File Offset: 0x00009958
		public override int GetHashCode()
		{
			if (this._hashCode == 0)
			{
				this._hashCode = 17;
				this._hashCode = this._hashCode * 23 + this._name.GetHashCode();
				this._hashCode = this._hashCode * 23 + this.FontFamily.GetHashCode();
				this._hashCode = this._hashCode * 23 + this._size.GetHashCode();
				this._hashCode = this._hashCode * 23 + this._unit.GetHashCode();
				this._hashCode = this._hashCode * 23 + this._style.GetHashCode();
				this._hashCode = this._hashCode * 23 + (int)this._gdiCharSet;
				this._hashCode = this._hashCode * 23 + this._gdiVerticalFont.GetHashCode();
			}
			return this._hashCode;
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Font" /> from the specified Windows handle to a device context.</summary>
		/// <param name="hdc">A handle to a device context.</param>
		/// <returns>The <see cref="T:System.Drawing.Font" /> this method creates.</returns>
		/// <exception cref="T:System.ArgumentException">The font for the specified device context is not a TrueType font.</exception>
		// Token: 0x06000470 RID: 1136 RVA: 0x00009B6A File Offset: 0x00007D6A
		[MonoTODO("The hdc parameter has no direct equivalent in libgdiplus.")]
		public static Font FromHdc(IntPtr hdc)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Font" /> from the specified GDI logical font (LOGFONT) structure.</summary>
		/// <param name="lf">An <see cref="T:System.Object" /> that represents the GDI <see langword="LOGFONT" /> structure from which to create the <see cref="T:System.Drawing.Font" />.</param>
		/// <param name="hdc">A handle to a device context that contains additional information about the <paramref name="lf" /> structure.</param>
		/// <returns>The <see cref="T:System.Drawing.Font" /> that this method creates.</returns>
		/// <exception cref="T:System.ArgumentException">The font is not a TrueType font.</exception>
		// Token: 0x06000471 RID: 1137 RVA: 0x0000B844 File Offset: 0x00009A44
		[MonoTODO("The returned font may not have all it's properties initialized correctly.")]
		public static Font FromLogFont(object lf, IntPtr hdc)
		{
			LOGFONT logfont = (LOGFONT)lf;
			IntPtr newFontObject;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFontFromLogfont(hdc, ref logfont, out newFontObject));
			return new Font(newFontObject, "Microsoft Sans Serif", FontStyle.Regular, 10f);
		}

		/// <summary>Returns the line spacing, in pixels, of this font.</summary>
		/// <returns>The line spacing, in pixels, of this font.</returns>
		// Token: 0x06000472 RID: 1138 RVA: 0x0000B878 File Offset: 0x00009A78
		public float GetHeight()
		{
			return this.GetHeight(Graphics.systemDpiY);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Font" /> from the specified GDI logical font (LOGFONT) structure.</summary>
		/// <param name="lf">An <see cref="T:System.Object" /> that represents the GDI <see langword="LOGFONT" /> structure from which to create the <see cref="T:System.Drawing.Font" />.</param>
		/// <returns>The <see cref="T:System.Drawing.Font" /> that this method creates.</returns>
		// Token: 0x06000473 RID: 1139 RVA: 0x0000B888 File Offset: 0x00009A88
		public static Font FromLogFont(object lf)
		{
			if (GDIPlus.RunningOnUnix())
			{
				return Font.FromLogFont(lf, IntPtr.Zero);
			}
			IntPtr intPtr = IntPtr.Zero;
			Font result;
			try
			{
				intPtr = GDIPlus.GetDC(IntPtr.Zero);
				result = Font.FromLogFont(lf, intPtr);
			}
			finally
			{
				GDIPlus.ReleaseDC(IntPtr.Zero, intPtr);
			}
			return result;
		}

		/// <summary>Creates a GDI logical font (LOGFONT) structure from this <see cref="T:System.Drawing.Font" />.</summary>
		/// <param name="logFont">An <see cref="T:System.Object" /> to represent the <see langword="LOGFONT" /> structure that this method creates.</param>
		// Token: 0x06000474 RID: 1140 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		public void ToLogFont(object logFont)
		{
			if (GDIPlus.RunningOnUnix())
			{
				using (Bitmap bitmap = new Bitmap(1, 1, PixelFormat.Format32bppArgb))
				{
					using (Graphics graphics = Graphics.FromImage(bitmap))
					{
						this.ToLogFont(logFont, graphics);
						return;
					}
				}
			}
			IntPtr dc = GDIPlus.GetDC(IntPtr.Zero);
			try
			{
				using (Graphics graphics2 = Graphics.FromHdc(dc))
				{
					this.ToLogFont(logFont, graphics2);
				}
			}
			finally
			{
				GDIPlus.ReleaseDC(IntPtr.Zero, dc);
			}
		}

		/// <summary>Creates a GDI logical font (LOGFONT) structure from this <see cref="T:System.Drawing.Font" />.</summary>
		/// <param name="logFont">An <see cref="T:System.Object" /> to represent the <see langword="LOGFONT" /> structure that this method creates.</param>
		/// <param name="graphics">A <see cref="T:System.Drawing.Graphics" /> that provides additional information for the <see langword="LOGFONT" /> structure.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is <see langword="null" />.</exception>
		// Token: 0x06000475 RID: 1141 RVA: 0x0000B994 File Offset: 0x00009B94
		public void ToLogFont(object logFont, Graphics graphics)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			if (logFont == null)
			{
				throw new AccessViolationException("logFont");
			}
			if (!logFont.GetType().GetTypeInfo().IsLayoutSequential)
			{
				throw new ArgumentException("logFont", Locale.GetText("Layout must be sequential."));
			}
			Type typeFromHandle = typeof(LOGFONT);
			int num = Marshal.SizeOf(logFont);
			if (num >= Marshal.SizeOf(typeFromHandle))
			{
				IntPtr intPtr = Marshal.AllocHGlobal(num);
				Status status;
				try
				{
					Marshal.StructureToPtr(logFont, intPtr, false);
					status = GDIPlus.GdipGetLogFont(this.NativeObject, graphics.NativeObject, logFont);
					if (status != Status.Ok)
					{
						Marshal.PtrToStructure(intPtr, logFont);
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (Font.CharSetOffset == -1)
				{
					Font.CharSetOffset = (int)Marshal.OffsetOf(typeFromHandle, "lfCharSet");
				}
				GCHandle gchandle = GCHandle.Alloc(logFont, GCHandleType.Pinned);
				try
				{
					IntPtr ptr = gchandle.AddrOfPinnedObject();
					if (Marshal.ReadByte(ptr, Font.CharSetOffset) == 0)
					{
						Marshal.WriteByte(ptr, Font.CharSetOffset, 1);
					}
				}
				finally
				{
					gchandle.Free();
				}
				GDIPlus.CheckStatus(status);
			}
		}

		/// <summary>Returns the line spacing, in the current unit of a specified <see cref="T:System.Drawing.Graphics" />, of this font.</summary>
		/// <param name="graphics">A <see cref="T:System.Drawing.Graphics" /> that holds the vertical resolution, in dots per inch, of the display device as well as settings for page unit and page scale.</param>
		/// <returns>The line spacing, in pixels, of this font.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is <see langword="null" />.</exception>
		// Token: 0x06000476 RID: 1142 RVA: 0x0000BAB0 File Offset: 0x00009CB0
		public float GetHeight(Graphics graphics)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			float result;
			GDIPlus.CheckStatus(GDIPlus.GdipGetFontHeight(this.fontObject, graphics.NativeObject, out result));
			return result;
		}

		/// <summary>Returns the height, in pixels, of this <see cref="T:System.Drawing.Font" /> when drawn to a device with the specified vertical resolution.</summary>
		/// <param name="dpi">The vertical resolution, in dots per inch, used to calculate the height of the font.</param>
		/// <returns>The height, in pixels, of this <see cref="T:System.Drawing.Font" />.</returns>
		// Token: 0x06000477 RID: 1143 RVA: 0x0000BAE4 File Offset: 0x00009CE4
		public float GetHeight(float dpi)
		{
			float result;
			GDIPlus.CheckStatus(GDIPlus.GdipGetFontHeightGivenDPI(this.fontObject, dpi, out result));
			return result;
		}

		/// <summary>Returns a human-readable string representation of this <see cref="T:System.Drawing.Font" />.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.Font" />.</returns>
		// Token: 0x06000478 RID: 1144 RVA: 0x0000BB08 File Offset: 0x00009D08
		public override string ToString()
		{
			return string.Format("[Font: Name={0}, Size={1}, Units={2}, GdiCharSet={3}, GdiVerticalFont={4}]", new object[]
			{
				this._name,
				this.Size,
				(int)this._unit,
				this._gdiCharSet,
				this._gdiVerticalFont
			});
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000BB66 File Offset: 0x00009D66
		// Note: this type is marked as 'beforefieldinit'.
		static Font()
		{
		}

		// Token: 0x0400047E RID: 1150
		private IntPtr fontObject = IntPtr.Zero;

		// Token: 0x0400047F RID: 1151
		private string systemFontName;

		// Token: 0x04000480 RID: 1152
		private string originalFontName;

		// Token: 0x04000481 RID: 1153
		private float _size;

		// Token: 0x04000482 RID: 1154
		private object olf;

		// Token: 0x04000483 RID: 1155
		private const byte DefaultCharSet = 1;

		// Token: 0x04000484 RID: 1156
		private static int CharSetOffset = -1;

		// Token: 0x04000485 RID: 1157
		private bool _bold;

		// Token: 0x04000486 RID: 1158
		private FontFamily _fontFamily;

		// Token: 0x04000487 RID: 1159
		private byte _gdiCharSet;

		// Token: 0x04000488 RID: 1160
		private bool _gdiVerticalFont;

		// Token: 0x04000489 RID: 1161
		private bool _italic;

		// Token: 0x0400048A RID: 1162
		private string _name;

		// Token: 0x0400048B RID: 1163
		private float _sizeInPoints;

		// Token: 0x0400048C RID: 1164
		private bool _strikeout;

		// Token: 0x0400048D RID: 1165
		private FontStyle _style;

		// Token: 0x0400048E RID: 1166
		private bool _underline;

		// Token: 0x0400048F RID: 1167
		private GraphicsUnit _unit;

		// Token: 0x04000490 RID: 1168
		private int _hashCode;
	}
}
