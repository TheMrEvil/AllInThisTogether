using System;
using System.ComponentModel;
using System.Drawing.Text;

namespace System.Drawing
{
	/// <summary>Encapsulates text layout information (such as alignment, orientation and tab stops) display manipulations (such as ellipsis insertion and national digit substitution) and OpenType features. This class cannot be inherited.</summary>
	// Token: 0x0200008A RID: 138
	public sealed class StringFormat : MarshalByRefObject, IDisposable, ICloneable
	{
		/// <summary>Initializes a new <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		// Token: 0x06000750 RID: 1872 RVA: 0x00015C04 File Offset: 0x00013E04
		public StringFormat() : this((StringFormatFlags)0, 0)
		{
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.StringFormat" /> object with the specified <see cref="T:System.Drawing.StringFormatFlags" /> enumeration and language.</summary>
		/// <param name="options">The <see cref="T:System.Drawing.StringFormatFlags" /> enumeration for the new <see cref="T:System.Drawing.StringFormat" /> object.</param>
		/// <param name="language">A value that indicates the language of the text.</param>
		// Token: 0x06000751 RID: 1873 RVA: 0x00015C0E File Offset: 0x00013E0E
		public StringFormat(StringFormatFlags options, int language)
		{
			this.nativeStrFmt = IntPtr.Zero;
			base..ctor();
			GDIPlus.CheckStatus(GDIPlus.GdipCreateStringFormat(options, language, out this.nativeStrFmt));
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00015C33 File Offset: 0x00013E33
		internal StringFormat(IntPtr native)
		{
			this.nativeStrFmt = IntPtr.Zero;
			base..ctor();
			this.nativeStrFmt = native;
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x06000753 RID: 1875 RVA: 0x00015C50 File Offset: 0x00013E50
		~StringFormat()
		{
			this.Dispose(false);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		// Token: 0x06000754 RID: 1876 RVA: 0x00015C80 File Offset: 0x00013E80
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00015C8F File Offset: 0x00013E8F
		private void Dispose(bool disposing)
		{
			if (this.nativeStrFmt != IntPtr.Zero)
			{
				Status status = GDIPlus.GdipDeleteStringFormat(this.nativeStrFmt);
				this.nativeStrFmt = IntPtr.Zero;
				GDIPlus.CheckStatus(status);
			}
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.StringFormat" /> object from the specified existing <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <param name="format">The <see cref="T:System.Drawing.StringFormat" /> object from which to initialize the new <see cref="T:System.Drawing.StringFormat" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is <see langword="null" />.</exception>
		// Token: 0x06000756 RID: 1878 RVA: 0x00015CBE File Offset: 0x00013EBE
		public StringFormat(StringFormat format)
		{
			this.nativeStrFmt = IntPtr.Zero;
			base..ctor();
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			GDIPlus.CheckStatus(GDIPlus.GdipCloneStringFormat(format.NativeObject, out this.nativeStrFmt));
		}

		/// <summary>Initializes a new <see cref="T:System.Drawing.StringFormat" /> object with the specified <see cref="T:System.Drawing.StringFormatFlags" /> enumeration.</summary>
		/// <param name="options">The <see cref="T:System.Drawing.StringFormatFlags" /> enumeration for the new <see cref="T:System.Drawing.StringFormat" /> object.</param>
		// Token: 0x06000757 RID: 1879 RVA: 0x00015CF5 File Offset: 0x00013EF5
		public StringFormat(StringFormatFlags options)
		{
			this.nativeStrFmt = IntPtr.Zero;
			base..ctor();
			GDIPlus.CheckStatus(GDIPlus.GdipCreateStringFormat(options, 0, out this.nativeStrFmt));
		}

		/// <summary>Gets or sets horizontal alignment of the string.</summary>
		/// <returns>A <see cref="T:System.Drawing.StringAlignment" /> enumeration that specifies the horizontal  alignment of the string.</returns>
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x00015D1C File Offset: 0x00013F1C
		// (set) Token: 0x06000759 RID: 1881 RVA: 0x00015D3C File Offset: 0x00013F3C
		public StringAlignment Alignment
		{
			get
			{
				StringAlignment result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatAlign(this.nativeStrFmt, out result));
				return result;
			}
			set
			{
				if (value < StringAlignment.Near || value > StringAlignment.Far)
				{
					throw new InvalidEnumArgumentException("Alignment");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatAlign(this.nativeStrFmt, value));
			}
		}

		/// <summary>Gets or sets the vertical alignment of the string.</summary>
		/// <returns>A <see cref="T:System.Drawing.StringAlignment" /> enumeration that represents the vertical line alignment.</returns>
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00015D64 File Offset: 0x00013F64
		// (set) Token: 0x0600075B RID: 1883 RVA: 0x00015D84 File Offset: 0x00013F84
		public StringAlignment LineAlignment
		{
			get
			{
				StringAlignment result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatLineAlign(this.nativeStrFmt, out result));
				return result;
			}
			set
			{
				if (value < StringAlignment.Near || value > StringAlignment.Far)
				{
					throw new InvalidEnumArgumentException("Alignment");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatLineAlign(this.nativeStrFmt, value));
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Drawing.StringFormatFlags" /> enumeration that contains formatting information.</summary>
		/// <returns>A <see cref="T:System.Drawing.StringFormatFlags" /> enumeration that contains formatting information.</returns>
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00015DAC File Offset: 0x00013FAC
		// (set) Token: 0x0600075D RID: 1885 RVA: 0x00015DCC File Offset: 0x00013FCC
		public StringFormatFlags FormatFlags
		{
			get
			{
				StringFormatFlags result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatFlags(this.nativeStrFmt, out result));
				return result;
			}
			set
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatFlags(this.nativeStrFmt, value));
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.Text.HotkeyPrefix" /> object for this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <returns>The <see cref="T:System.Drawing.Text.HotkeyPrefix" /> object for this <see cref="T:System.Drawing.StringFormat" /> object, the default is <see cref="F:System.Drawing.Text.HotkeyPrefix.None" />.</returns>
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00015DE0 File Offset: 0x00013FE0
		// (set) Token: 0x0600075F RID: 1887 RVA: 0x00015E00 File Offset: 0x00014000
		public HotkeyPrefix HotkeyPrefix
		{
			get
			{
				HotkeyPrefix result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatHotkeyPrefix(this.nativeStrFmt, out result));
				return result;
			}
			set
			{
				if (value < HotkeyPrefix.None || value > HotkeyPrefix.Hide)
				{
					throw new InvalidEnumArgumentException("HotkeyPrefix");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatHotkeyPrefix(this.nativeStrFmt, value));
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Drawing.StringTrimming" /> enumeration for this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <returns>A <see cref="T:System.Drawing.StringTrimming" /> enumeration that indicates how text drawn with this <see cref="T:System.Drawing.StringFormat" /> object is trimmed when it exceeds the edges of the layout rectangle.</returns>
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x00015E28 File Offset: 0x00014028
		// (set) Token: 0x06000761 RID: 1889 RVA: 0x00015E48 File Offset: 0x00014048
		public StringTrimming Trimming
		{
			get
			{
				StringTrimming result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatTrimming(this.nativeStrFmt, out result));
				return result;
			}
			set
			{
				if (value < StringTrimming.None || value > StringTrimming.EllipsisPath)
				{
					throw new InvalidEnumArgumentException("Trimming");
				}
				GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatTrimming(this.nativeStrFmt, value));
			}
		}

		/// <summary>Gets a generic default <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <returns>The generic default <see cref="T:System.Drawing.StringFormat" /> object.</returns>
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00015E70 File Offset: 0x00014070
		public static StringFormat GenericDefault
		{
			get
			{
				IntPtr native;
				GDIPlus.CheckStatus(GDIPlus.GdipStringFormatGetGenericDefault(out native));
				return new StringFormat(native);
			}
		}

		/// <summary>Gets the language that is used when local digits are substituted for western digits.</summary>
		/// <returns>A National Language Support (NLS) language identifier that identifies the language that will be used when local digits are substituted for western digits. You can pass the <see cref="P:System.Globalization.CultureInfo.LCID" /> property of a <see cref="T:System.Globalization.CultureInfo" /> object as the NLS language identifier. For example, suppose you create a <see cref="T:System.Globalization.CultureInfo" /> object by passing the string "ar-EG" to a <see cref="T:System.Globalization.CultureInfo" /> constructor. If you pass the <see cref="P:System.Globalization.CultureInfo.LCID" /> property of that <see cref="T:System.Globalization.CultureInfo" /> object along with <see cref="F:System.Drawing.StringDigitSubstitute.Traditional" /> to the <see cref="M:System.Drawing.StringFormat.SetDigitSubstitution(System.Int32,System.Drawing.StringDigitSubstitute)" /> method, then Arabic-Indic digits will be substituted for western digits at display time.</returns>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00015E8F File Offset: 0x0001408F
		public int DigitSubstitutionLanguage
		{
			get
			{
				return this.language;
			}
		}

		/// <summary>Gets a generic typographic <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <returns>A generic typographic <see cref="T:System.Drawing.StringFormat" /> object.</returns>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x00015E98 File Offset: 0x00014098
		public static StringFormat GenericTypographic
		{
			get
			{
				IntPtr native;
				GDIPlus.CheckStatus(GDIPlus.GdipStringFormatGetGenericTypographic(out native));
				return new StringFormat(native);
			}
		}

		/// <summary>Gets the method to be used for digit substitution.</summary>
		/// <returns>A <see cref="T:System.Drawing.StringDigitSubstitute" /> enumeration value that specifies how to substitute characters in a string that cannot be displayed because they are not supported by the current font.</returns>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x00015EB8 File Offset: 0x000140B8
		public StringDigitSubstitute DigitSubstitutionMethod
		{
			get
			{
				StringDigitSubstitute result;
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatDigitSubstitution(this.nativeStrFmt, this.language, out result));
				return result;
			}
		}

		/// <summary>Specifies an array of <see cref="T:System.Drawing.CharacterRange" /> structures that represent the ranges of characters measured by a call to the <see cref="M:System.Drawing.Graphics.MeasureCharacterRanges(System.String,System.Drawing.Font,System.Drawing.RectangleF,System.Drawing.StringFormat)" /> method.</summary>
		/// <param name="ranges">An array of <see cref="T:System.Drawing.CharacterRange" /> structures that specifies the ranges of characters measured by a call to the <see cref="M:System.Drawing.Graphics.MeasureCharacterRanges(System.String,System.Drawing.Font,System.Drawing.RectangleF,System.Drawing.StringFormat)" /> method.</param>
		/// <exception cref="T:System.OverflowException">More than 32 character ranges are set.</exception>
		// Token: 0x06000766 RID: 1894 RVA: 0x00015EDE File Offset: 0x000140DE
		public void SetMeasurableCharacterRanges(CharacterRange[] ranges)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatMeasurableCharacterRanges(this.nativeStrFmt, ranges.Length, ranges));
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00015EF4 File Offset: 0x000140F4
		internal int GetMeasurableCharacterRangeCount()
		{
			int result;
			GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatMeasurableCharacterRangeCount(this.nativeStrFmt, out result));
			return result;
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <returns>The <see cref="T:System.Drawing.StringFormat" /> object this method creates.</returns>
		// Token: 0x06000768 RID: 1896 RVA: 0x00015F14 File Offset: 0x00014114
		public object Clone()
		{
			IntPtr native;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneStringFormat(this.nativeStrFmt, out native));
			return new StringFormat(native);
		}

		/// <summary>Converts this <see cref="T:System.Drawing.StringFormat" /> object to a human-readable string.</summary>
		/// <returns>A string representation of this <see cref="T:System.Drawing.StringFormat" /> object.</returns>
		// Token: 0x06000769 RID: 1897 RVA: 0x00015F3C File Offset: 0x0001413C
		public override string ToString()
		{
			return "[StringFormat, FormatFlags=" + this.FormatFlags.ToString() + "]";
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x00015F6C File Offset: 0x0001416C
		// (set) Token: 0x0600076B RID: 1899 RVA: 0x00015F74 File Offset: 0x00014174
		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeStrFmt;
			}
			set
			{
				this.nativeStrFmt = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x00015F6C File Offset: 0x0001416C
		internal IntPtr nativeFormat
		{
			get
			{
				return this.nativeStrFmt;
			}
		}

		/// <summary>Sets tab stops for this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <param name="firstTabOffset">The number of spaces between the beginning of a line of text and the first tab stop.</param>
		/// <param name="tabStops">An array of distances between tab stops in the units specified by the <see cref="P:System.Drawing.Graphics.PageUnit" /> property.</param>
		// Token: 0x0600076D RID: 1901 RVA: 0x00015F7D File Offset: 0x0001417D
		public void SetTabStops(float firstTabOffset, float[] tabStops)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatTabStops(this.nativeStrFmt, firstTabOffset, tabStops.Length, tabStops));
		}

		/// <summary>Specifies the language and method to be used when local digits are substituted for western digits.</summary>
		/// <param name="language">A National Language Support (NLS) language identifier that identifies the language that will be used when local digits are substituted for western digits. You can pass the <see cref="P:System.Globalization.CultureInfo.LCID" /> property of a <see cref="T:System.Globalization.CultureInfo" /> object as the NLS language identifier. For example, suppose you create a <see cref="T:System.Globalization.CultureInfo" /> object by passing the string "ar-EG" to a <see cref="T:System.Globalization.CultureInfo" /> constructor. If you pass the <see cref="P:System.Globalization.CultureInfo.LCID" /> property of that <see cref="T:System.Globalization.CultureInfo" /> object along with <see cref="F:System.Drawing.StringDigitSubstitute.Traditional" /> to the <see cref="M:System.Drawing.StringFormat.SetDigitSubstitution(System.Int32,System.Drawing.StringDigitSubstitute)" /> method, then Arabic-Indic digits will be substituted for western digits at display time.</param>
		/// <param name="substitute">An element of the <see cref="T:System.Drawing.StringDigitSubstitute" /> enumeration that specifies how digits are displayed.</param>
		// Token: 0x0600076E RID: 1902 RVA: 0x00015F94 File Offset: 0x00014194
		public void SetDigitSubstitution(int language, StringDigitSubstitute substitute)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipSetStringFormatDigitSubstitution(this.nativeStrFmt, this.language, substitute));
		}

		/// <summary>Gets the tab stops for this <see cref="T:System.Drawing.StringFormat" /> object.</summary>
		/// <param name="firstTabOffset">The number of spaces between the beginning of a text line and the first tab stop.</param>
		/// <returns>An array of distances (in number of spaces) between tab stops.</returns>
		// Token: 0x0600076F RID: 1903 RVA: 0x00015FB0 File Offset: 0x000141B0
		public float[] GetTabStops(out float firstTabOffset)
		{
			int num = 0;
			firstTabOffset = 0f;
			GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatTabStopCount(this.nativeStrFmt, out num));
			float[] array = new float[num];
			if (num != 0)
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetStringFormatTabStops(this.nativeStrFmt, num, out firstTabOffset, array));
			}
			return array;
		}

		// Token: 0x04000568 RID: 1384
		private IntPtr nativeStrFmt;

		// Token: 0x04000569 RID: 1385
		private int language;
	}
}
