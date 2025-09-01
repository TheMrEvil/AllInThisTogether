using System;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Defines a group of type faces having a similar basic design and certain variations in styles. This class cannot be inherited.</summary>
	// Token: 0x02000070 RID: 112
	public sealed class FontFamily : MarshalByRefObject, IDisposable
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x0000C246 File Offset: 0x0000A446
		internal FontFamily(IntPtr fntfamily)
		{
			this.nativeFontFamily = fntfamily;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000C260 File Offset: 0x0000A460
		internal unsafe void refreshName()
		{
			if (this.nativeFontFamily == IntPtr.Zero)
			{
				return;
			}
			char* value = stackalloc char[(UIntPtr)64];
			GDIPlus.CheckStatus(GDIPlus.GdipGetFamilyName(this.nativeFontFamily, (IntPtr)((void*)value), 0));
			this.name = Marshal.PtrToStringUni((IntPtr)((void*)value));
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x0600048F RID: 1167 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		~FontFamily()
		{
			this.Dispose();
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000C2DC File Offset: 0x0000A4DC
		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeFontFamily;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000C2DC File Offset: 0x0000A4DC
		internal IntPtr NativeFamily
		{
			get
			{
				return this.nativeFontFamily;
			}
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.FontFamily" /> from the specified generic font family.</summary>
		/// <param name="genericFamily">The <see cref="T:System.Drawing.Text.GenericFontFamilies" /> from which to create the new <see cref="T:System.Drawing.FontFamily" />.</param>
		// Token: 0x06000492 RID: 1170 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		public FontFamily(GenericFontFamilies genericFamily)
		{
			Status status;
			switch (genericFamily)
			{
			case GenericFontFamilies.Serif:
				status = GDIPlus.GdipGetGenericFontFamilySerif(out this.nativeFontFamily);
				goto IL_4D;
			case GenericFontFamilies.SansSerif:
				status = GDIPlus.GdipGetGenericFontFamilySansSerif(out this.nativeFontFamily);
				goto IL_4D;
			}
			status = GDIPlus.GdipGetGenericFontFamilyMonospace(out this.nativeFontFamily);
			IL_4D:
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.FontFamily" /> with the specified name.</summary>
		/// <param name="name">The name of the new <see cref="T:System.Drawing.FontFamily" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="name" /> specifies a font that is not installed on the computer running the application.  
		/// -or-  
		/// <paramref name="name" /> specifies a font that is not a TrueType font.</exception>
		// Token: 0x06000493 RID: 1171 RVA: 0x0000C344 File Offset: 0x0000A544
		public FontFamily(string name) : this(name, null)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.FontFamily" /> in the specified <see cref="T:System.Drawing.Text.FontCollection" /> with the specified name.</summary>
		/// <param name="name">A <see cref="T:System.String" /> that represents the name of the new <see cref="T:System.Drawing.FontFamily" />.</param>
		/// <param name="fontCollection">The <see cref="T:System.Drawing.Text.FontCollection" /> that contains this <see cref="T:System.Drawing.FontFamily" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an empty string ("").  
		/// -or-  
		/// <paramref name="name" /> specifies a font that is not installed on the computer running the application.  
		/// -or-  
		/// <paramref name="name" /> specifies a font that is not a TrueType font.</exception>
		// Token: 0x06000494 RID: 1172 RVA: 0x0000C350 File Offset: 0x0000A550
		public FontFamily(string name, FontCollection fontCollection)
		{
			IntPtr collection = (fontCollection == null) ? IntPtr.Zero : fontCollection._nativeFontCollection;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateFontFamilyFromName(name, collection, out this.nativeFontFamily));
		}

		/// <summary>Gets the name of this <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the name of this <see cref="T:System.Drawing.FontFamily" />.</returns>
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000C391 File Offset: 0x0000A591
		public string Name
		{
			get
			{
				if (this.nativeFontFamily == IntPtr.Zero)
				{
					throw new ArgumentException("Name", Locale.GetText("Object was disposed."));
				}
				if (this.name == null)
				{
					this.refreshName();
				}
				return this.name;
			}
		}

		/// <summary>Gets a generic monospace <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.FontFamily" /> that represents a generic monospace font.</returns>
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000C3CE File Offset: 0x0000A5CE
		public static FontFamily GenericMonospace
		{
			get
			{
				return new FontFamily(GenericFontFamilies.Monospace);
			}
		}

		/// <summary>Gets a generic sans serif <see cref="T:System.Drawing.FontFamily" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.FontFamily" /> object that represents a generic sans serif font.</returns>
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000C3D6 File Offset: 0x0000A5D6
		public static FontFamily GenericSansSerif
		{
			get
			{
				return new FontFamily(GenericFontFamilies.SansSerif);
			}
		}

		/// <summary>Gets a generic serif <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.FontFamily" /> that represents a generic serif font.</returns>
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000C3DE File Offset: 0x0000A5DE
		public static FontFamily GenericSerif
		{
			get
			{
				return new FontFamily(GenericFontFamilies.Serif);
			}
		}

		/// <summary>Returns the cell ascent, in design units, of the <see cref="T:System.Drawing.FontFamily" /> of the specified style.</summary>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> that contains style information for the font.</param>
		/// <returns>The cell ascent for this <see cref="T:System.Drawing.FontFamily" /> that uses the specified <see cref="T:System.Drawing.FontStyle" />.</returns>
		// Token: 0x06000499 RID: 1177 RVA: 0x0000C3E8 File Offset: 0x0000A5E8
		public int GetCellAscent(FontStyle style)
		{
			short result;
			GDIPlus.CheckStatus(GDIPlus.GdipGetCellAscent(this.nativeFontFamily, (int)style, out result));
			return (int)result;
		}

		/// <summary>Returns the cell descent, in design units, of the <see cref="T:System.Drawing.FontFamily" /> of the specified style.</summary>
		/// <param name="style">A <see cref="T:System.Drawing.FontStyle" /> that contains style information for the font.</param>
		/// <returns>The cell descent metric for this <see cref="T:System.Drawing.FontFamily" /> that uses the specified <see cref="T:System.Drawing.FontStyle" />.</returns>
		// Token: 0x0600049A RID: 1178 RVA: 0x0000C40C File Offset: 0x0000A60C
		public int GetCellDescent(FontStyle style)
		{
			short result;
			GDIPlus.CheckStatus(GDIPlus.GdipGetCellDescent(this.nativeFontFamily, (int)style, out result));
			return (int)result;
		}

		/// <summary>Gets the height, in font design units, of the em square for the specified style.</summary>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> for which to get the em height.</param>
		/// <returns>The height of the em square.</returns>
		// Token: 0x0600049B RID: 1179 RVA: 0x0000C430 File Offset: 0x0000A630
		public int GetEmHeight(FontStyle style)
		{
			short result;
			GDIPlus.CheckStatus(GDIPlus.GdipGetEmHeight(this.nativeFontFamily, (int)style, out result));
			return (int)result;
		}

		/// <summary>Returns the line spacing, in design units, of the <see cref="T:System.Drawing.FontFamily" /> of the specified style. The line spacing is the vertical distance between the base lines of two consecutive lines of text.</summary>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> to apply.</param>
		/// <returns>The distance between two consecutive lines of text.</returns>
		// Token: 0x0600049C RID: 1180 RVA: 0x0000C454 File Offset: 0x0000A654
		public int GetLineSpacing(FontStyle style)
		{
			short result;
			GDIPlus.CheckStatus(GDIPlus.GdipGetLineSpacing(this.nativeFontFamily, (int)style, out result));
			return (int)result;
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Drawing.FontStyle" /> enumeration is available.</summary>
		/// <param name="style">The <see cref="T:System.Drawing.FontStyle" /> to test.</param>
		/// <returns>
		///   <see langword="true" /> if the specified <see cref="T:System.Drawing.FontStyle" /> is available; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600049D RID: 1181 RVA: 0x0000C478 File Offset: 0x0000A678
		[MonoDocumentationNote("When used with libgdiplus this method always return true (styles are created on demand).")]
		public bool IsStyleAvailable(FontStyle style)
		{
			bool result;
			GDIPlus.CheckStatus(GDIPlus.GdipIsStyleAvailable(this.nativeFontFamily, (int)style, out result));
			return result;
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.FontFamily" />.</summary>
		// Token: 0x0600049E RID: 1182 RVA: 0x0000C499 File Offset: 0x0000A699
		public void Dispose()
		{
			if (this.nativeFontFamily != IntPtr.Zero)
			{
				Status status = GDIPlus.GdipDeleteFontFamily(this.nativeFontFamily);
				this.nativeFontFamily = IntPtr.Zero;
				GC.SuppressFinalize(this);
				GDIPlus.CheckStatus(status);
			}
		}

		/// <summary>Indicates whether the specified object is a <see cref="T:System.Drawing.FontFamily" /> and is identical to this <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <param name="obj">The object to test.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.Drawing.FontFamily" /> and is identical to this <see cref="T:System.Drawing.FontFamily" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600049F RID: 1183 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
		public override bool Equals(object obj)
		{
			FontFamily fontFamily = obj as FontFamily;
			return fontFamily != null && this.Name == fontFamily.Name;
		}

		/// <summary>Gets a hash code for this <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <returns>The hash code for this <see cref="T:System.Drawing.FontFamily" />.</returns>
		// Token: 0x060004A0 RID: 1184 RVA: 0x0000C4FA File Offset: 0x0000A6FA
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		/// <summary>Returns an array that contains all the <see cref="T:System.Drawing.FontFamily" /> objects associated with the current graphics context.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.FontFamily" /> objects associated with the current graphics context.</returns>
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000C507 File Offset: 0x0000A707
		public static FontFamily[] Families
		{
			get
			{
				return new InstalledFontCollection().Families;
			}
		}

		/// <summary>Returns an array that contains all the <see cref="T:System.Drawing.FontFamily" /> objects available for the specified graphics context.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> object from which to return <see cref="T:System.Drawing.FontFamily" /> objects.</param>
		/// <returns>An array of <see cref="T:System.Drawing.FontFamily" /> objects available for the specified <see cref="T:System.Drawing.Graphics" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="graphics" /> is <see langword="null" />.</exception>
		// Token: 0x060004A2 RID: 1186 RVA: 0x0000C513 File Offset: 0x0000A713
		public static FontFamily[] GetFamilies(Graphics graphics)
		{
			if (graphics == null)
			{
				throw new ArgumentNullException("graphics");
			}
			return new InstalledFontCollection().Families;
		}

		/// <summary>Returns the name, in the specified language, of this <see cref="T:System.Drawing.FontFamily" />.</summary>
		/// <param name="language">The language in which the name is returned.</param>
		/// <returns>A <see cref="T:System.String" /> that represents the name, in the specified language, of this <see cref="T:System.Drawing.FontFamily" />.</returns>
		// Token: 0x060004A3 RID: 1187 RVA: 0x0000C52D File Offset: 0x0000A72D
		[MonoLimitation("The language parameter is ignored. We always return the name using the default system language.")]
		public string GetName(int language)
		{
			return this.Name;
		}

		/// <summary>Converts this <see cref="T:System.Drawing.FontFamily" /> to a human-readable string representation.</summary>
		/// <returns>The string that represents this <see cref="T:System.Drawing.FontFamily" />.</returns>
		// Token: 0x060004A4 RID: 1188 RVA: 0x0000C535 File Offset: 0x0000A735
		public override string ToString()
		{
			return "[FontFamily: Name=" + this.Name + "]";
		}

		// Token: 0x04000492 RID: 1170
		private string name;

		// Token: 0x04000493 RID: 1171
		private IntPtr nativeFontFamily = IntPtr.Zero;
	}
}
