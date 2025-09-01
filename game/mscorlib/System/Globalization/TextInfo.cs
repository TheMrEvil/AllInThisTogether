using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using Unity;

namespace System.Globalization
{
	/// <summary>Defines text properties and behaviors, such as casing, that are specific to a writing system.</summary>
	// Token: 0x02000999 RID: 2457
	[ComVisible(true)]
	[Serializable]
	public class TextInfo : ICloneable, IDeserializationCallback
	{
		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06005821 RID: 22561 RVA: 0x0012912D File Offset: 0x0012732D
		internal static TextInfo Invariant
		{
			get
			{
				if (TextInfo.s_Invariant == null)
				{
					TextInfo.s_Invariant = new TextInfo(CultureData.Invariant);
				}
				return TextInfo.s_Invariant;
			}
		}

		// Token: 0x06005822 RID: 22562 RVA: 0x00129150 File Offset: 0x00127350
		internal TextInfo(CultureData cultureData)
		{
			this.m_cultureData = cultureData;
			this.m_cultureName = this.m_cultureData.CultureName;
			this.m_textInfoName = this.m_cultureData.STEXTINFO;
		}

		// Token: 0x06005823 RID: 22563 RVA: 0x00129181 File Offset: 0x00127381
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.m_cultureData = null;
			this.m_cultureName = null;
		}

		// Token: 0x06005824 RID: 22564 RVA: 0x00129194 File Offset: 0x00127394
		private void OnDeserialized()
		{
			if (this.m_cultureData == null)
			{
				if (this.m_cultureName == null)
				{
					if (this.customCultureName != null)
					{
						this.m_cultureName = this.customCultureName;
					}
					else if (this.m_win32LangID == 0)
					{
						this.m_cultureName = "ar-SA";
					}
					else
					{
						this.m_cultureName = CultureInfo.GetCultureInfo(this.m_win32LangID).m_cultureData.CultureName;
					}
				}
				this.m_cultureData = CultureInfo.GetCultureInfo(this.m_cultureName).m_cultureData;
				this.m_textInfoName = this.m_cultureData.STEXTINFO;
			}
		}

		// Token: 0x06005825 RID: 22565 RVA: 0x0012921E File Offset: 0x0012741E
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserialized();
		}

		// Token: 0x06005826 RID: 22566 RVA: 0x00129226 File Offset: 0x00127426
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.m_useUserOverride = false;
			this.customCultureName = this.m_cultureName;
			this.m_win32LangID = CultureInfo.GetCultureInfo(this.m_cultureName).LCID;
		}

		// Token: 0x06005827 RID: 22567 RVA: 0x00129251 File Offset: 0x00127451
		internal static int GetHashCodeOrdinalIgnoreCase(string s)
		{
			return TextInfo.GetHashCodeOrdinalIgnoreCase(s, false, 0L);
		}

		// Token: 0x06005828 RID: 22568 RVA: 0x0012925C File Offset: 0x0012745C
		internal static int GetHashCodeOrdinalIgnoreCase(string s, bool forceRandomizedHashing, long additionalEntropy)
		{
			return TextInfo.Invariant.GetCaseInsensitiveHashCode(s, forceRandomizedHashing, additionalEntropy);
		}

		// Token: 0x06005829 RID: 22569 RVA: 0x0012926B File Offset: 0x0012746B
		[SecuritySafeCritical]
		internal static int CompareOrdinalIgnoreCaseEx(string strA, int indexA, string strB, int indexB, int lengthA, int lengthB)
		{
			return TextInfo.InternalCompareStringOrdinalIgnoreCase(strA, indexA, strB, indexB, lengthA, lengthB);
		}

		// Token: 0x0600582A RID: 22570 RVA: 0x0012927C File Offset: 0x0012747C
		internal static int IndexOfStringOrdinalIgnoreCase(string source, string value, int startIndex, int count)
		{
			if (source.Length == 0 && value.Length == 0)
			{
				return 0;
			}
			int num = startIndex + count - value.Length;
			while (startIndex <= num)
			{
				if (TextInfo.CompareOrdinalIgnoreCaseEx(source, startIndex, value, 0, value.Length, value.Length) == 0)
				{
					return startIndex;
				}
				startIndex++;
			}
			return -1;
		}

		// Token: 0x0600582B RID: 22571 RVA: 0x001292CC File Offset: 0x001274CC
		internal static int LastIndexOfStringOrdinalIgnoreCase(string source, string value, int startIndex, int count)
		{
			if (value.Length == 0)
			{
				return startIndex;
			}
			int num = startIndex - count + 1;
			if (value.Length > 0)
			{
				startIndex -= value.Length - 1;
			}
			while (startIndex >= num)
			{
				if (TextInfo.CompareOrdinalIgnoreCaseEx(source, startIndex, value, 0, value.Length, value.Length) == 0)
				{
					return startIndex;
				}
				startIndex--;
			}
			return -1;
		}

		/// <summary>Gets the American National Standards Institute (ANSI) code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>The ANSI code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x0600582C RID: 22572 RVA: 0x00129323 File Offset: 0x00127523
		public virtual int ANSICodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTANSICODEPAGE;
			}
		}

		/// <summary>Gets the original equipment manufacturer (OEM) code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>The OEM code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x0600582D RID: 22573 RVA: 0x00129330 File Offset: 0x00127530
		public virtual int OEMCodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTOEMCODEPAGE;
			}
		}

		/// <summary>Gets the Macintosh code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>The Macintosh code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x0600582E RID: 22574 RVA: 0x0012933D File Offset: 0x0012753D
		public virtual int MacCodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTMACCODEPAGE;
			}
		}

		/// <summary>Gets the Extended Binary Coded Decimal Interchange Code (EBCDIC) code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>The EBCDIC code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x0600582F RID: 22575 RVA: 0x0012934A File Offset: 0x0012754A
		public virtual int EBCDICCodePage
		{
			get
			{
				return this.m_cultureData.IDEFAULTEBCDICCODEPAGE;
			}
		}

		/// <summary>Gets the culture identifier for the culture associated with the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>A number that identifies the culture from which the current <see cref="T:System.Globalization.TextInfo" /> object was created.</returns>
		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06005830 RID: 22576 RVA: 0x00129357 File Offset: 0x00127557
		[ComVisible(false)]
		public int LCID
		{
			get
			{
				return CultureInfo.GetCultureInfo(this.m_textInfoName).LCID;
			}
		}

		/// <summary>Gets the name of the culture associated with the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>The name of a culture.</returns>
		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06005831 RID: 22577 RVA: 0x00129369 File Offset: 0x00127569
		[ComVisible(false)]
		public string CultureName
		{
			get
			{
				return this.m_textInfoName;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.TextInfo" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Globalization.TextInfo" /> object is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06005832 RID: 22578 RVA: 0x00129371 File Offset: 0x00127571
		[ComVisible(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>A new instance of <see cref="T:System.Object" /> that is the memberwise clone of the current <see cref="T:System.Globalization.TextInfo" /> object.</returns>
		// Token: 0x06005833 RID: 22579 RVA: 0x00129379 File Offset: 0x00127579
		[ComVisible(false)]
		public virtual object Clone()
		{
			object obj = base.MemberwiseClone();
			((TextInfo)obj).SetReadOnlyState(false);
			return obj;
		}

		/// <summary>Returns a read-only version of the specified <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <param name="textInfo">A <see cref="T:System.Globalization.TextInfo" /> object.</param>
		/// <returns>The <see cref="T:System.Globalization.TextInfo" /> object specified by the <paramref name="textInfo" /> parameter, if <paramref name="textInfo" /> is read-only.  
		///  -or-  
		///  A read-only memberwise clone of the <see cref="T:System.Globalization.TextInfo" /> object specified by <paramref name="textInfo" />, if <paramref name="textInfo" /> is not read-only.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="textInfo" /> is null.</exception>
		// Token: 0x06005834 RID: 22580 RVA: 0x0012938D File Offset: 0x0012758D
		[ComVisible(false)]
		public static TextInfo ReadOnly(TextInfo textInfo)
		{
			if (textInfo == null)
			{
				throw new ArgumentNullException("textInfo");
			}
			if (textInfo.IsReadOnly)
			{
				return textInfo;
			}
			TextInfo textInfo2 = (TextInfo)textInfo.MemberwiseClone();
			textInfo2.SetReadOnlyState(true);
			return textInfo2;
		}

		// Token: 0x06005835 RID: 22581 RVA: 0x001293B9 File Offset: 0x001275B9
		private void VerifyWritable()
		{
			if (this.m_isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
			}
		}

		// Token: 0x06005836 RID: 22582 RVA: 0x001293D3 File Offset: 0x001275D3
		internal void SetReadOnlyState(bool readOnly)
		{
			this.m_isReadOnly = readOnly;
		}

		/// <summary>Gets or sets the string that separates items in a list.</summary>
		/// <returns>The string that separates items in a list.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the current <see cref="T:System.Globalization.TextInfo" /> object is read-only.</exception>
		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06005837 RID: 22583 RVA: 0x001293DC File Offset: 0x001275DC
		// (set) Token: 0x06005838 RID: 22584 RVA: 0x001293FD File Offset: 0x001275FD
		public virtual string ListSeparator
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_listSeparator == null)
				{
					this.m_listSeparator = this.m_cultureData.SLIST;
				}
				return this.m_listSeparator;
			}
			[ComVisible(false)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.m_listSeparator = value;
			}
		}

		/// <summary>Converts the specified character to lowercase.</summary>
		/// <param name="c">The character to convert to lowercase.</param>
		/// <returns>The specified character converted to lowercase.</returns>
		// Token: 0x06005839 RID: 22585 RVA: 0x00129424 File Offset: 0x00127624
		[SecuritySafeCritical]
		public virtual char ToLower(char c)
		{
			if (TextInfo.IsAscii(c) && this.IsAsciiCasingSameAsInvariant)
			{
				return TextInfo.ToLowerAsciiInvariant(c);
			}
			return this.ToLowerInternal(c);
		}

		/// <summary>Converts the specified string to lowercase.</summary>
		/// <param name="str">The string to convert to lowercase.</param>
		/// <returns>The specified string converted to lowercase.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null.</exception>
		// Token: 0x0600583A RID: 22586 RVA: 0x00129444 File Offset: 0x00127644
		[SecuritySafeCritical]
		public virtual string ToLower(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return this.ToLowerInternal(str);
		}

		// Token: 0x0600583B RID: 22587 RVA: 0x0012945B File Offset: 0x0012765B
		private static char ToLowerAsciiInvariant(char c)
		{
			if ('A' <= c && c <= 'Z')
			{
				c |= ' ';
			}
			return c;
		}

		/// <summary>Converts the specified character to uppercase.</summary>
		/// <param name="c">The character to convert to uppercase.</param>
		/// <returns>The specified character converted to uppercase.</returns>
		// Token: 0x0600583C RID: 22588 RVA: 0x0012946F File Offset: 0x0012766F
		[SecuritySafeCritical]
		public virtual char ToUpper(char c)
		{
			if (TextInfo.IsAscii(c) && this.IsAsciiCasingSameAsInvariant)
			{
				return TextInfo.ToUpperAsciiInvariant(c);
			}
			return this.ToUpperInternal(c);
		}

		/// <summary>Converts the specified string to uppercase.</summary>
		/// <param name="str">The string to convert to uppercase.</param>
		/// <returns>The specified string converted to uppercase.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null.</exception>
		// Token: 0x0600583D RID: 22589 RVA: 0x0012948F File Offset: 0x0012768F
		[SecuritySafeCritical]
		public virtual string ToUpper(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			return this.ToUpperInternal(str);
		}

		// Token: 0x0600583E RID: 22590 RVA: 0x001294A6 File Offset: 0x001276A6
		internal static char ToUpperAsciiInvariant(char c)
		{
			if ('a' <= c && c <= 'z')
			{
				c = (char)((int)c & -33);
			}
			return c;
		}

		// Token: 0x0600583F RID: 22591 RVA: 0x001294BA File Offset: 0x001276BA
		private static bool IsAscii(char c)
		{
			return c < '\u0080';
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06005840 RID: 22592 RVA: 0x001294C4 File Offset: 0x001276C4
		private bool IsAsciiCasingSameAsInvariant
		{
			get
			{
				if (this.m_IsAsciiCasingSameAsInvariant == null)
				{
					this.m_IsAsciiCasingSameAsInvariant = new bool?(!(this.m_cultureData.SISO639LANGNAME == "az") && !(this.m_cultureData.SISO639LANGNAME == "tr"));
				}
				return this.m_IsAsciiCasingSameAsInvariant.Value;
			}
		}

		/// <summary>Determines whether the specified object represents the same writing system as the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <param name="obj">The object to compare with the current <see cref="T:System.Globalization.TextInfo" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> represents the same writing system as the current <see cref="T:System.Globalization.TextInfo" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005841 RID: 22593 RVA: 0x00129528 File Offset: 0x00127728
		public override bool Equals(object obj)
		{
			TextInfo textInfo = obj as TextInfo;
			return textInfo != null && this.CultureName.Equals(textInfo.CultureName);
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.TextInfo" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x06005842 RID: 22594 RVA: 0x00129552 File Offset: 0x00127752
		public override int GetHashCode()
		{
			return this.CultureName.GetHashCode();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x06005843 RID: 22595 RVA: 0x0012955F File Offset: 0x0012775F
		public override string ToString()
		{
			return "TextInfo - " + this.m_cultureData.CultureName;
		}

		/// <summary>Converts the specified string to title case (except for words that are entirely in uppercase, which are considered to be acronyms).</summary>
		/// <param name="str">The string to convert to title case.</param>
		/// <returns>The specified string converted to title case.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is <see langword="null" />.</exception>
		// Token: 0x06005844 RID: 22596 RVA: 0x00129578 File Offset: 0x00127778
		public string ToTitleCase(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (str.Length == 0)
			{
				return str;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			for (int i = 0; i < str.Length; i++)
			{
				int num;
				UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, i, out num);
				if (char.CheckLetter(unicodeCategory))
				{
					i = this.AddTitlecaseLetter(ref stringBuilder, ref str, i, num) + 1;
					int num2 = i;
					bool flag = unicodeCategory == UnicodeCategory.LowercaseLetter;
					while (i < str.Length)
					{
						unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, i, out num);
						if (TextInfo.IsLetterCategory(unicodeCategory))
						{
							if (unicodeCategory == UnicodeCategory.LowercaseLetter)
							{
								flag = true;
							}
							i += num;
						}
						else if (str[i] == '\'')
						{
							i++;
							if (flag)
							{
								if (text == null)
								{
									text = this.ToLower(str);
								}
								stringBuilder.Append(text, num2, i - num2);
							}
							else
							{
								stringBuilder.Append(str, num2, i - num2);
							}
							num2 = i;
							flag = true;
						}
						else
						{
							if (TextInfo.IsWordSeparator(unicodeCategory))
							{
								break;
							}
							i += num;
						}
					}
					int num3 = i - num2;
					if (num3 > 0)
					{
						if (flag)
						{
							if (text == null)
							{
								text = this.ToLower(str);
							}
							stringBuilder.Append(text, num2, num3);
						}
						else
						{
							stringBuilder.Append(str, num2, num3);
						}
					}
					if (i < str.Length)
					{
						i = TextInfo.AddNonLetter(ref stringBuilder, ref str, i, num);
					}
				}
				else
				{
					i = TextInfo.AddNonLetter(ref stringBuilder, ref str, i, num);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005845 RID: 22597 RVA: 0x001296C5 File Offset: 0x001278C5
		private static int AddNonLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
		{
			if (charLen == 2)
			{
				result.Append(input[inputIndex++]);
				result.Append(input[inputIndex]);
			}
			else
			{
				result.Append(input[inputIndex]);
			}
			return inputIndex;
		}

		// Token: 0x06005846 RID: 22598 RVA: 0x00129704 File Offset: 0x00127904
		private int AddTitlecaseLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
		{
			if (charLen == 2)
			{
				result.Append(this.ToUpper(input.Substring(inputIndex, charLen)));
				inputIndex++;
			}
			else
			{
				char c = input[inputIndex];
				switch (c)
				{
				case 'Ǆ':
				case 'ǅ':
				case 'ǆ':
					result.Append('ǅ');
					break;
				case 'Ǉ':
				case 'ǈ':
				case 'ǉ':
					result.Append('ǈ');
					break;
				case 'Ǌ':
				case 'ǋ':
				case 'ǌ':
					result.Append('ǋ');
					break;
				default:
					switch (c)
					{
					case 'Ǳ':
					case 'ǲ':
					case 'ǳ':
						result.Append('ǲ');
						break;
					default:
						result.Append(this.ToUpper(input[inputIndex]));
						break;
					}
					break;
				}
			}
			return inputIndex;
		}

		// Token: 0x06005847 RID: 22599 RVA: 0x001297DE File Offset: 0x001279DE
		private static bool IsWordSeparator(UnicodeCategory category)
		{
			return (536672256 & 1 << (int)category) != 0;
		}

		// Token: 0x06005848 RID: 22600 RVA: 0x001297EF File Offset: 0x001279EF
		private static bool IsLetterCategory(UnicodeCategory uc)
		{
			return uc == UnicodeCategory.UppercaseLetter || uc == UnicodeCategory.LowercaseLetter || uc == UnicodeCategory.TitlecaseLetter || uc == UnicodeCategory.ModifierLetter || uc == UnicodeCategory.OtherLetter;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.TextInfo" /> object represents a writing system where text flows from right to left.</summary>
		/// <returns>
		///   <see langword="true" /> if text flows from right to left; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06005849 RID: 22601 RVA: 0x00129806 File Offset: 0x00127A06
		[ComVisible(false)]
		public bool IsRightToLeft
		{
			get
			{
				return this.m_cultureData.IsRightToLeft;
			}
		}

		/// <summary>Raises the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x0600584A RID: 22602 RVA: 0x0012921E File Offset: 0x0012741E
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialized();
		}

		// Token: 0x0600584B RID: 22603 RVA: 0x00129813 File Offset: 0x00127A13
		[SecuritySafeCritical]
		internal int GetCaseInsensitiveHashCode(string str)
		{
			return this.GetCaseInsensitiveHashCode(str, false, 0L);
		}

		// Token: 0x0600584C RID: 22604 RVA: 0x0012981F File Offset: 0x00127A1F
		[SecuritySafeCritical]
		internal int GetCaseInsensitiveHashCode(string str, bool forceRandomizedHashing, long additionalEntropy)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (this != TextInfo.s_Invariant)
			{
				return StringComparer.CurrentCultureIgnoreCase.GetHashCode(str);
			}
			return this.GetInvariantCaseInsensitiveHashCode(str);
		}

		// Token: 0x0600584D RID: 22605 RVA: 0x0012984C File Offset: 0x00127A4C
		private unsafe int GetInvariantCaseInsensitiveHashCode(string str)
		{
			char* ptr = str;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = ptr;
			char* ptr3 = ptr2 + str.Length - 1;
			int num = 0;
			while (ptr2 < ptr3)
			{
				num = (num << 5) - num + (int)char.ToUpperInvariant(*ptr2);
				num = (num << 5) - num + (int)char.ToUpperInvariant(ptr2[1]);
				ptr2 += 2;
			}
			ptr3++;
			if (ptr2 < ptr3)
			{
				num = (num << 5) - num + (int)char.ToUpperInvariant(*ptr2);
			}
			return num;
		}

		// Token: 0x0600584E RID: 22606 RVA: 0x001298C8 File Offset: 0x00127AC8
		private unsafe string ToUpperInternal(string str)
		{
			if (str.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(str.Length);
			fixed (string text2 = str)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text3 = text)
				{
					char* ptr2 = text3;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr3 = ptr2;
					char* ptr4 = ptr;
					for (int i = 0; i < str.Length; i++)
					{
						*ptr3 = this.ToUpper(*ptr4);
						ptr4++;
						ptr3++;
					}
					text2 = null;
				}
				return text;
			}
		}

		// Token: 0x0600584F RID: 22607 RVA: 0x0012994C File Offset: 0x00127B4C
		private unsafe string ToLowerInternal(string str)
		{
			if (str.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(str.Length);
			fixed (string text2 = str)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				fixed (string text3 = text)
				{
					char* ptr2 = text3;
					if (ptr2 != null)
					{
						ptr2 += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr3 = ptr2;
					char* ptr4 = ptr;
					for (int i = 0; i < str.Length; i++)
					{
						*ptr3 = this.ToLower(*ptr4);
						ptr4++;
						ptr3++;
					}
					text2 = null;
				}
				return text;
			}
		}

		// Token: 0x06005850 RID: 22608 RVA: 0x001299D0 File Offset: 0x00127BD0
		private char ToUpperInternal(char c)
		{
			if (!this.m_cultureData.IsInvariantCulture)
			{
				if (c <= 'ǲ')
				{
					if (c > 'ſ')
					{
						if (c <= 'ǈ')
						{
							if (c != 'ǅ' && c != 'ǈ')
							{
								goto IL_15A;
							}
						}
						else if (c != 'ǋ' && c != 'ǲ')
						{
							goto IL_15A;
						}
						return c - '\u0001';
					}
					if (c == 'µ')
					{
						return 'Μ';
					}
					if (c == 'ı')
					{
						return 'I';
					}
					if (c == 'ſ')
					{
						return 'S';
					}
				}
				else if (c <= 'ϰ')
				{
					if (c <= 'ς')
					{
						if (c == 'ͅ')
						{
							return 'Ι';
						}
						if (c == 'ς')
						{
							return 'Σ';
						}
					}
					else
					{
						switch (c)
						{
						case 'ϐ':
							return 'Β';
						case 'ϑ':
							return 'Θ';
						case 'ϒ':
						case 'ϓ':
						case 'ϔ':
							break;
						case 'ϕ':
							return 'Φ';
						case 'ϖ':
							return 'Π';
						default:
							if (c == 'ϰ')
							{
								return 'Κ';
							}
							break;
						}
					}
				}
				else if (c <= 'ϵ')
				{
					if (c == 'ϱ')
					{
						return 'Ρ';
					}
					if (c == 'ϵ')
					{
						return 'Ε';
					}
				}
				else
				{
					if (c == 'ẛ')
					{
						return 'Ṡ';
					}
					if (c == 'ι')
					{
						return 'Ι';
					}
				}
				IL_15A:
				if (!this.IsAsciiCasingSameAsInvariant)
				{
					if (c == 'i')
					{
						return 'İ';
					}
					if (TextInfo.IsAscii(c))
					{
						return TextInfo.ToUpperAsciiInvariant(c);
					}
				}
			}
			if (c >= 'à' && c <= 'ֆ')
			{
				return TextInfoToUpperData.range_00e0_0586[(int)(c - 'à')];
			}
			if (c >= 'ḁ' && c <= 'ῳ')
			{
				return TextInfoToUpperData.range_1e01_1ff3[(int)(c - 'ḁ')];
			}
			if (c >= 'ⅰ' && c <= 'ↄ')
			{
				return TextInfoToUpperData.range_2170_2184[(int)(c - 'ⅰ')];
			}
			if (c >= 'ⓐ' && c <= 'ⓩ')
			{
				return TextInfoToUpperData.range_24d0_24e9[(int)(c - 'ⓐ')];
			}
			if (c >= 'ⰰ' && c <= 'ⳣ')
			{
				return TextInfoToUpperData.range_2c30_2ce3[(int)(c - 'ⰰ')];
			}
			if (c >= 'ⴀ' && c <= 'ⴥ')
			{
				return TextInfoToUpperData.range_2d00_2d25[(int)(c - 'ⴀ')];
			}
			if (c >= 'ꙁ' && c <= 'ꚗ')
			{
				return TextInfoToUpperData.range_a641_a697[(int)(c - 'ꙁ')];
			}
			if (c >= 'ꜣ' && c <= 'ꞌ')
			{
				return TextInfoToUpperData.range_a723_a78c[(int)(c - 'ꜣ')];
			}
			if ('ａ' <= c && c <= 'ｚ')
			{
				return c - ' ';
			}
			if (c == 'ᵹ')
			{
				return 'Ᵹ';
			}
			if (c == 'ᵽ')
			{
				return 'Ᵽ';
			}
			if (c != 'ⅎ')
			{
				return c;
			}
			return 'Ⅎ';
		}

		// Token: 0x06005851 RID: 22609 RVA: 0x00129C8C File Offset: 0x00127E8C
		private char ToLowerInternal(char c)
		{
			if (!this.m_cultureData.IsInvariantCulture)
			{
				if (c <= 'ǲ')
				{
					if (c <= 'ǅ')
					{
						if (c == 'İ')
						{
							return 'i';
						}
						if (c != 'ǅ')
						{
							goto IL_D3;
						}
					}
					else if (c != 'ǈ' && c != 'ǋ' && c != 'ǲ')
					{
						goto IL_D3;
					}
					return c + '\u0001';
				}
				if (c <= 'ẞ')
				{
					switch (c)
					{
					case 'ϒ':
						return 'υ';
					case 'ϓ':
						return 'ύ';
					case 'ϔ':
						return 'ϋ';
					default:
						if (c == 'ϴ')
						{
							return 'θ';
						}
						if (c == 'ẞ')
						{
							return 'ß';
						}
						break;
					}
				}
				else
				{
					if (c == 'Ω')
					{
						return 'ω';
					}
					if (c == 'K')
					{
						return 'k';
					}
					if (c == 'Å')
					{
						return 'å';
					}
				}
				IL_D3:
				if (!this.IsAsciiCasingSameAsInvariant)
				{
					if (c == 'I')
					{
						return 'ı';
					}
					if (TextInfo.IsAscii(c))
					{
						return TextInfo.ToLowerAsciiInvariant(c);
					}
				}
			}
			if (c >= 'À' && c <= 'Ֆ')
			{
				return TextInfoToLowerData.range_00c0_0556[(int)(c - 'À')];
			}
			if (c >= 'Ⴀ' && c <= 'Ⴥ')
			{
				return TextInfoToLowerData.range_10a0_10c5[(int)(c - 'Ⴀ')];
			}
			if (c >= 'Ḁ' && c <= 'ῼ')
			{
				return TextInfoToLowerData.range_1e00_1ffc[(int)(c - 'Ḁ')];
			}
			if (c >= 'Ⅰ' && c <= 'Ⅿ')
			{
				return TextInfoToLowerData.range_2160_216f[(int)(c - 'Ⅰ')];
			}
			if (c >= 'Ⓐ' && c <= 'Ⓩ')
			{
				return TextInfoToLowerData.range_24b6_24cf[(int)(c - 'Ⓐ')];
			}
			if (c >= 'Ⰰ' && c <= 'Ⱞ')
			{
				return TextInfoToLowerData.range_2c00_2c2e[(int)(c - 'Ⰰ')];
			}
			if (c >= 'Ⱡ' && c <= 'Ⳣ')
			{
				return TextInfoToLowerData.range_2c60_2ce2[(int)(c - 'Ⱡ')];
			}
			if (c >= 'Ꙁ' && c <= 'Ꚗ')
			{
				return TextInfoToLowerData.range_a640_a696[(int)(c - 'Ꙁ')];
			}
			if (c >= 'Ꜣ' && c <= 'Ꞌ')
			{
				return TextInfoToLowerData.range_a722_a78b[(int)(c - 'Ꜣ')];
			}
			if ('Ａ' <= c && c <= 'Ｚ')
			{
				return c + ' ';
			}
			if (c == 'Ⅎ')
			{
				return 'ⅎ';
			}
			if (c != 'Ↄ')
			{
				return c;
			}
			return 'ↄ';
		}

		// Token: 0x06005852 RID: 22610 RVA: 0x00129ED4 File Offset: 0x001280D4
		internal unsafe static int InternalCompareStringOrdinalIgnoreCase(string strA, int indexA, string strB, int indexB, int lenA, int lenB)
		{
			if (strA == null)
			{
				if (strB != null)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (strB == null)
				{
					return 1;
				}
				int num = Math.Min(lenA, strA.Length - indexA);
				int num2 = Math.Min(lenB, strB.Length - indexB);
				if (num == num2 && strA == strB)
				{
					return 0;
				}
				char* ptr = strA;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = strB;
				if (ptr2 != null)
				{
					ptr2 += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr3 = ptr + indexA;
				char* ptr4 = ptr3 + Math.Min(num, num2);
				char* ptr5 = ptr2 + indexB;
				while (ptr3 < ptr4)
				{
					if (*ptr3 != *ptr5)
					{
						char c = char.ToUpperInvariant(*ptr3);
						char c2 = char.ToUpperInvariant(*ptr5);
						if (c != c2)
						{
							return (int)(c - c2);
						}
					}
					ptr3++;
					ptr5++;
				}
				return num - num2;
			}
		}

		// Token: 0x06005853 RID: 22611 RVA: 0x00129F9C File Offset: 0x0012819C
		internal unsafe void ToLowerAsciiInvariant(ReadOnlySpan<char> source, Span<char> destination)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = TextInfo.ToLowerAsciiInvariant((char)(*source[i]));
			}
		}

		// Token: 0x06005854 RID: 22612 RVA: 0x00129FD4 File Offset: 0x001281D4
		internal unsafe void ToUpperAsciiInvariant(ReadOnlySpan<char> source, Span<char> destination)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = TextInfo.ToUpperAsciiInvariant((char)(*source[i]));
			}
		}

		// Token: 0x06005855 RID: 22613 RVA: 0x0012A00C File Offset: 0x0012820C
		internal unsafe void ChangeCase(ReadOnlySpan<char> source, Span<char> destination, bool toUpper)
		{
			if (source.IsEmpty)
			{
				return;
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(destination))
				{
					char* ptr2 = reference2;
					int i = 0;
					char* ptr3 = ptr;
					char* ptr4 = ptr2;
					if (toUpper)
					{
						while (i < source.Length)
						{
							*(ptr4++) = this.ToUpper(*(ptr3++));
							i++;
						}
					}
					else
					{
						while (i < source.Length)
						{
							*(ptr4++) = this.ToLower(*(ptr3++));
							i++;
						}
					}
				}
			}
		}

		// Token: 0x06005856 RID: 22614 RVA: 0x000173AD File Offset: 0x000155AD
		internal TextInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040036A9 RID: 13993
		[OptionalField(VersionAdded = 2)]
		private string m_listSeparator;

		// Token: 0x040036AA RID: 13994
		[OptionalField(VersionAdded = 2)]
		private bool m_isReadOnly;

		// Token: 0x040036AB RID: 13995
		[OptionalField(VersionAdded = 3)]
		private string m_cultureName;

		// Token: 0x040036AC RID: 13996
		[NonSerialized]
		private CultureData m_cultureData;

		// Token: 0x040036AD RID: 13997
		[NonSerialized]
		private string m_textInfoName;

		// Token: 0x040036AE RID: 13998
		[NonSerialized]
		private bool? m_IsAsciiCasingSameAsInvariant;

		// Token: 0x040036AF RID: 13999
		internal static volatile TextInfo s_Invariant;

		// Token: 0x040036B0 RID: 14000
		[OptionalField(VersionAdded = 2)]
		private string customCultureName;

		// Token: 0x040036B1 RID: 14001
		[OptionalField(VersionAdded = 1)]
		internal int m_nDataItem;

		// Token: 0x040036B2 RID: 14002
		[OptionalField(VersionAdded = 1)]
		internal bool m_useUserOverride;

		// Token: 0x040036B3 RID: 14003
		[OptionalField(VersionAdded = 1)]
		internal int m_win32LangID;

		// Token: 0x040036B4 RID: 14004
		private const int wordSeparatorMask = 536672256;
	}
}
