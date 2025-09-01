using System;
using System.Text;

namespace System.Globalization
{
	/// <summary>Supports the use of non-ASCII characters for Internet domain names. This class cannot be inherited.</summary>
	// Token: 0x020009AA RID: 2474
	public sealed class IdnMapping
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.IdnMapping" /> class.</summary>
		// Token: 0x06005958 RID: 22872 RVA: 0x00132134 File Offset: 0x00130334
		public IdnMapping()
		{
		}

		/// <summary>Gets or sets a value that indicates whether unassigned Unicode code points are used in operations performed by members of the current <see cref="T:System.Globalization.IdnMapping" /> object.</summary>
		/// <returns>
		///   <see langword="true" /> if unassigned code points are used in operations; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06005959 RID: 22873 RVA: 0x00132147 File Offset: 0x00130347
		// (set) Token: 0x0600595A RID: 22874 RVA: 0x0013214F File Offset: 0x0013034F
		public bool AllowUnassigned
		{
			get
			{
				return this.allow_unassigned;
			}
			set
			{
				this.allow_unassigned = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether standard or relaxed naming conventions are used in operations performed by members of the current <see cref="T:System.Globalization.IdnMapping" /> object.</summary>
		/// <returns>
		///   <see langword="true" /> if standard naming conventions are used in operations; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x0600595B RID: 22875 RVA: 0x00132158 File Offset: 0x00130358
		// (set) Token: 0x0600595C RID: 22876 RVA: 0x00132160 File Offset: 0x00130360
		public bool UseStd3AsciiRules
		{
			get
			{
				return this.use_std3;
			}
			set
			{
				this.use_std3 = value;
			}
		}

		/// <summary>Indicates whether a specified object and the current <see cref="T:System.Globalization.IdnMapping" /> object are equal.</summary>
		/// <param name="obj">The object to compare to the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the object specified by the <paramref name="obj" /> parameter is derived from <see cref="T:System.Globalization.IdnMapping" /> and its <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600595D RID: 22877 RVA: 0x0013216C File Offset: 0x0013036C
		public override bool Equals(object obj)
		{
			IdnMapping idnMapping = obj as IdnMapping;
			return idnMapping != null && this.allow_unassigned == idnMapping.allow_unassigned && this.use_std3 == idnMapping.use_std3;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Globalization.IdnMapping" /> object.</summary>
		/// <returns>One of four 32-bit signed constants derived from the properties of an <see cref="T:System.Globalization.IdnMapping" /> object.  The return value has no special meaning and is not suitable for use in a hash code algorithm.</returns>
		// Token: 0x0600595E RID: 22878 RVA: 0x001321A1 File Offset: 0x001303A1
		public override int GetHashCode()
		{
			return (this.allow_unassigned ? 2 : 0) + (this.use_std3 ? 1 : 0);
		}

		/// <summary>Encodes a string of domain name labels that consist of Unicode characters to a string of displayable Unicode characters in the US-ASCII character range. The string is formatted according to the IDNA standard.</summary>
		/// <param name="unicode">The string to convert, which consists of one or more domain name labels delimited with label separators.</param>
		/// <returns>The equivalent of the string specified by the <paramref name="unicode" /> parameter, consisting of displayable Unicode characters in the US-ASCII character range (U+0020 to U+007E) and formatted according to the IDNA standard.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unicode" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="unicode" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x0600595F RID: 22879 RVA: 0x001321BC File Offset: 0x001303BC
		public string GetAscii(string unicode)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			return this.GetAscii(unicode, 0, unicode.Length);
		}

		/// <summary>Encodes a substring of domain name labels that include Unicode characters outside the US-ASCII character range. The substring is converted to a string of displayable Unicode characters in the US-ASCII character range and is formatted according to the IDNA standard.</summary>
		/// <param name="unicode">The string to convert, which consists of one or more domain name labels delimited with label separators.</param>
		/// <param name="index">A zero-based offset into <paramref name="unicode" /> that specifies the start of the substring to convert. The conversion operation continues to the end of the <paramref name="unicode" /> string.</param>
		/// <returns>The equivalent of the substring specified by the <paramref name="unicode" /> and <paramref name="index" /> parameters, consisting of displayable Unicode characters in the US-ASCII character range (U+0020 to U+007E) and formatted according to the IDNA standard.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unicode" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than the length of <paramref name="unicode" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="unicode" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x06005960 RID: 22880 RVA: 0x001321DA File Offset: 0x001303DA
		public string GetAscii(string unicode, int index)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			return this.GetAscii(unicode, index, unicode.Length - index);
		}

		/// <summary>Encodes the specified number of characters in a  substring of domain name labels that include Unicode characters outside the US-ASCII character range. The substring is converted to a string of displayable Unicode characters in the US-ASCII character range and is formatted according to the IDNA standard.</summary>
		/// <param name="unicode">The string to convert, which consists of one or more domain name labels delimited with label separators.</param>
		/// <param name="index">A zero-based offset into <paramref name="unicode" /> that specifies the start of the substring.</param>
		/// <param name="count">The number of characters to convert in the substring that starts at the position specified by  <paramref name="index" /> in the <paramref name="unicode" /> string.</param>
		/// <returns>The equivalent of the substring specified by the <paramref name="unicode" />, <paramref name="index" />, and <paramref name="count" /> parameters, consisting of displayable Unicode characters in the US-ASCII character range (U+0020 to U+007E) and formatted according to the IDNA standard.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unicode" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than the length of <paramref name="unicode" />.  
		/// -or-  
		/// <paramref name="index" /> is greater than the length of <paramref name="unicode" /> minus <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="unicode" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x06005961 RID: 22881 RVA: 0x001321FC File Offset: 0x001303FC
		public string GetAscii(string unicode, int index, int count)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index must be non-negative value");
			}
			if (count < 0 || index + count > unicode.Length)
			{
				throw new ArgumentOutOfRangeException("index + count must point inside the argument unicode string");
			}
			return this.Convert(unicode, index, count, true);
		}

		// Token: 0x06005962 RID: 22882 RVA: 0x0013224C File Offset: 0x0013044C
		private string Convert(string input, int index, int count, bool toAscii)
		{
			string text = input.Substring(index, count);
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] >= '\u0080')
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					break;
				}
			}
			string[] array = text.Split(new char[]
			{
				'.',
				'。',
				'．',
				'｡'
			});
			int num = 0;
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j].Length != 0 || j + 1 != array.Length)
				{
					if (toAscii)
					{
						array[j] = this.ToAscii(array[j], num);
					}
					else
					{
						array[j] = this.ToUnicode(array[j], num);
					}
				}
				num += array[j].Length;
			}
			return string.Join(".", array);
		}

		// Token: 0x06005963 RID: 22883 RVA: 0x0013230C File Offset: 0x0013050C
		private string ToAscii(string s, int offset)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] < ' ' || s[i] == '\u007f')
				{
					throw new ArgumentException(string.Format("Not allowed character was found, at {0}", offset + i));
				}
				if (s[i] >= '\u0080')
				{
					s = this.NamePrep(s, offset);
					break;
				}
			}
			if (this.use_std3)
			{
				this.VerifyStd3AsciiRules(s, offset);
			}
			int j = 0;
			while (j < s.Length)
			{
				if (s[j] >= '\u0080')
				{
					if (s.StartsWith("xn--", StringComparison.OrdinalIgnoreCase))
					{
						throw new ArgumentException(string.Format("The input string must not start with ACE (xn--), at {0}", offset + j));
					}
					s = this.puny.Encode(s, offset);
					s = "xn--" + s;
					break;
				}
				else
				{
					j++;
				}
			}
			this.VerifyLength(s, offset);
			return s;
		}

		// Token: 0x06005964 RID: 22884 RVA: 0x001323EE File Offset: 0x001305EE
		private void VerifyLength(string s, int offset)
		{
			if (s.Length == 0)
			{
				throw new ArgumentException(string.Format("A label in the input string resulted in an invalid zero-length string, at {0}", offset));
			}
			if (s.Length > 63)
			{
				throw new ArgumentException(string.Format("A label in the input string exceeded the length in ASCII representation, at {0}", offset));
			}
		}

		// Token: 0x06005965 RID: 22885 RVA: 0x00132430 File Offset: 0x00130630
		private string NamePrep(string s, int offset)
		{
			s = s.Normalize(NormalizationForm.FormKC);
			this.VerifyProhibitedCharacters(s, offset);
			if (!this.allow_unassigned)
			{
				for (int i = 0; i < s.Length; i++)
				{
					if (char.GetUnicodeCategory(s, i) == UnicodeCategory.OtherNotAssigned)
					{
						throw new ArgumentException(string.Format("Use of unassigned Unicode characer is prohibited in this IdnMapping, at {0}", offset + i));
					}
				}
			}
			return s;
		}

		// Token: 0x06005966 RID: 22886 RVA: 0x0013248C File Offset: 0x0013068C
		private void VerifyProhibitedCharacters(string s, int offset)
		{
			int i = 0;
			while (i < s.Length)
			{
				switch (char.GetUnicodeCategory(s, i))
				{
				case UnicodeCategory.SpaceSeparator:
					if (s[i] >= '\u0080')
					{
						goto IL_111;
					}
					break;
				case UnicodeCategory.LineSeparator:
				case UnicodeCategory.ParagraphSeparator:
				case UnicodeCategory.Format:
					goto IL_6E;
				case UnicodeCategory.Control:
					if (s[i] == '\0' || s[i] >= '\u0080')
					{
						goto IL_111;
					}
					break;
				case UnicodeCategory.Surrogate:
				case UnicodeCategory.PrivateUse:
					goto IL_111;
				default:
					goto IL_6E;
				}
				IL_129:
				i++;
				continue;
				IL_111:
				throw new ArgumentException(string.Format("Not allowed character was in the input string, at {0}", offset + i));
				IL_6E:
				char c = s[i];
				if (('﷟' <= c && c <= '﷯') || (c & '￿') == '￾' || ('￹' <= c && c <= '�') || ('⿰' <= c && c <= '⿻') || ('‪' <= c && c <= '‮') || ('⁪' <= c && c <= '⁯'))
				{
					goto IL_111;
				}
				if (c <= '‎')
				{
					if (c != '̀' && c != '́' && c != '‎')
					{
						goto IL_129;
					}
					goto IL_111;
				}
				else
				{
					if (c == '‏' || c == '\u2028' || c == '\u2029')
					{
						goto IL_111;
					}
					goto IL_129;
				}
			}
		}

		// Token: 0x06005967 RID: 22887 RVA: 0x001325D4 File Offset: 0x001307D4
		private void VerifyStd3AsciiRules(string s, int offset)
		{
			if (s.Length > 0 && s[0] == '-')
			{
				throw new ArgumentException(string.Format("'-' is not allowed at head of a sequence in STD3 mode, found at {0}", offset));
			}
			if (s.Length > 0 && s[s.Length - 1] == '-')
			{
				throw new ArgumentException(string.Format("'-' is not allowed at tail of a sequence in STD3 mode, found at {0}", offset + s.Length - 1));
			}
			for (int i = 0; i < s.Length; i++)
			{
				char c = s[i];
				if (c != '-' && (c <= '/' || (':' <= c && c <= '@') || ('[' <= c && c <= '`') || ('{' <= c && c <= '\u007f')))
				{
					throw new ArgumentException(string.Format("Not allowed character in STD3 mode, found at {0}", offset + i));
				}
			}
		}

		/// <summary>Decodes a string of one or more domain name labels, encoded according to the IDNA standard, to a string of Unicode characters.</summary>
		/// <param name="ascii">The string to decode, which consists of one or more labels in the US-ASCII character range (U+0020 to U+007E) encoded according to the IDNA standard.</param>
		/// <returns>The Unicode equivalent of the IDNA substring specified by the <paramref name="ascii" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ascii" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ascii" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x06005968 RID: 22888 RVA: 0x0013269E File Offset: 0x0013089E
		public string GetUnicode(string ascii)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			return this.GetUnicode(ascii, 0, ascii.Length);
		}

		/// <summary>Decodes a substring of one or more domain name labels, encoded according to the IDNA standard, to a string of Unicode characters.</summary>
		/// <param name="ascii">The string to decode, which consists of one or more labels in the US-ASCII character range (U+0020 to U+007E) encoded according to the IDNA standard.</param>
		/// <param name="index">A zero-based offset into <paramref name="ascii" /> that specifies the start of the substring to decode. The decoding operation continues to the end of the <paramref name="ascii" /> string.</param>
		/// <returns>The Unicode equivalent of the IDNA substring specified by the <paramref name="ascii" /> and <paramref name="index" /> parameters.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ascii" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than the length of <paramref name="ascii" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ascii" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x06005969 RID: 22889 RVA: 0x001326BC File Offset: 0x001308BC
		public string GetUnicode(string ascii, int index)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			return this.GetUnicode(ascii, index, ascii.Length - index);
		}

		/// <summary>Decodes a substring of a specified length that contains one or more domain name labels, encoded according to the IDNA standard, to a string of Unicode characters.</summary>
		/// <param name="ascii">The string to decode, which consists of one or more labels in the US-ASCII character range (U+0020 to U+007E) encoded according to the IDNA standard.</param>
		/// <param name="index">A zero-based offset into <paramref name="ascii" /> that specifies the start of the substring.</param>
		/// <param name="count">The number of characters to convert in the substring that starts at the position specified by <paramref name="index" /> in the <paramref name="ascii" /> string.</param>
		/// <returns>The Unicode equivalent of the IDNA substring specified by the <paramref name="ascii" />, <paramref name="index" />, and <paramref name="count" /> parameters.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ascii" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="index" /> is greater than the length of <paramref name="ascii" />.  
		/// -or-  
		/// <paramref name="index" /> is greater than the length of <paramref name="ascii" /> minus <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ascii" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x0600596A RID: 22890 RVA: 0x001326DC File Offset: 0x001308DC
		public string GetUnicode(string ascii, int index, int count)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index must be non-negative value");
			}
			if (count < 0 || index + count > ascii.Length)
			{
				throw new ArgumentOutOfRangeException("index + count must point inside the argument ascii string");
			}
			return this.Convert(ascii, index, count, false);
		}

		// Token: 0x0600596B RID: 22891 RVA: 0x0013272C File Offset: 0x0013092C
		private string ToUnicode(string s, int offset)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] >= '\u0080')
				{
					s = this.NamePrep(s, offset);
					break;
				}
			}
			if (!s.StartsWith("xn--", StringComparison.OrdinalIgnoreCase))
			{
				return s;
			}
			s = s.ToLower(CultureInfo.InvariantCulture);
			string strA = s;
			s = s.Substring(4);
			s = this.puny.Decode(s, offset);
			string result = s;
			s = this.ToAscii(s, offset);
			if (string.Compare(strA, s, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException(string.Format("ToUnicode() failed at verifying the result, at label part from {0}", offset));
			}
			return result;
		}

		// Token: 0x04003756 RID: 14166
		private bool allow_unassigned;

		// Token: 0x04003757 RID: 14167
		private bool use_std3;

		// Token: 0x04003758 RID: 14168
		private Punycode puny = new Punycode();
	}
}
