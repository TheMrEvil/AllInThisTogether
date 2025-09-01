using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Globalization
{
	/// <summary>Provides functionality to split a string into text elements and to iterate through those text elements.</summary>
	// Token: 0x02000995 RID: 2453
	[ComVisible(true)]
	[Serializable]
	public class StringInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.StringInfo" /> class.</summary>
		// Token: 0x060057D8 RID: 22488 RVA: 0x0012876E File Offset: 0x0012696E
		public StringInfo() : this("")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.StringInfo" /> class to a specified string.</summary>
		/// <param name="value">A string to initialize this <see cref="T:System.Globalization.StringInfo" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x060057D9 RID: 22489 RVA: 0x0012877B File Offset: 0x0012697B
		public StringInfo(string value)
		{
			this.String = value;
		}

		// Token: 0x060057DA RID: 22490 RVA: 0x0012878A File Offset: 0x0012698A
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.m_str = string.Empty;
		}

		// Token: 0x060057DB RID: 22491 RVA: 0x00128797 File Offset: 0x00126997
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			if (this.m_str.Length == 0)
			{
				this.m_indexes = null;
			}
		}

		/// <summary>Indicates whether the current <see cref="T:System.Globalization.StringInfo" /> object is equal to a specified object.</summary>
		/// <param name="value">An object.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter is a <see cref="T:System.Globalization.StringInfo" /> object and its <see cref="P:System.Globalization.StringInfo.String" /> property equals the <see cref="P:System.Globalization.StringInfo.String" /> property of this <see cref="T:System.Globalization.StringInfo" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x060057DC RID: 22492 RVA: 0x001287B0 File Offset: 0x001269B0
		[ComVisible(false)]
		public override bool Equals(object value)
		{
			StringInfo stringInfo = value as StringInfo;
			return stringInfo != null && this.m_str.Equals(stringInfo.m_str);
		}

		/// <summary>Calculates a hash code for the value of the current <see cref="T:System.Globalization.StringInfo" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code based on the string value of this <see cref="T:System.Globalization.StringInfo" /> object.</returns>
		// Token: 0x060057DD RID: 22493 RVA: 0x001287DA File Offset: 0x001269DA
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return this.m_str.GetHashCode();
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x060057DE RID: 22494 RVA: 0x001287E7 File Offset: 0x001269E7
		private int[] Indexes
		{
			get
			{
				if (this.m_indexes == null && 0 < this.String.Length)
				{
					this.m_indexes = StringInfo.ParseCombiningCharacters(this.String);
				}
				return this.m_indexes;
			}
		}

		/// <summary>Gets or sets the value of the current <see cref="T:System.Globalization.StringInfo" /> object.</summary>
		/// <returns>The string that is the value of the current <see cref="T:System.Globalization.StringInfo" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is <see langword="null" />.</exception>
		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x060057DF RID: 22495 RVA: 0x00128816 File Offset: 0x00126A16
		// (set) Token: 0x060057E0 RID: 22496 RVA: 0x0012881E File Offset: 0x00126A1E
		public string String
		{
			get
			{
				return this.m_str;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("String", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.m_str = value;
				this.m_indexes = null;
			}
		}

		/// <summary>Gets the number of text elements in the current <see cref="T:System.Globalization.StringInfo" /> object.</summary>
		/// <returns>The number of base characters, surrogate pairs, and combining character sequences in this <see cref="T:System.Globalization.StringInfo" /> object.</returns>
		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x060057E1 RID: 22497 RVA: 0x00128846 File Offset: 0x00126A46
		public int LengthInTextElements
		{
			get
			{
				if (this.Indexes == null)
				{
					return 0;
				}
				return this.Indexes.Length;
			}
		}

		/// <summary>Retrieves a substring of text elements from the current <see cref="T:System.Globalization.StringInfo" /> object starting from a specified text element and continuing through the last text element.</summary>
		/// <param name="startingTextElement">The zero-based index of a text element in this <see cref="T:System.Globalization.StringInfo" /> object.</param>
		/// <returns>A substring of text elements in this <see cref="T:System.Globalization.StringInfo" /> object, starting from the text element index specified by the <paramref name="startingTextElement" /> parameter and continuing through the last text element in this object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startingTextElement" /> is less than zero.  
		/// -or-  
		/// The string that is the value of the current <see cref="T:System.Globalization.StringInfo" /> object is the empty string ("").</exception>
		// Token: 0x060057E2 RID: 22498 RVA: 0x0012885C File Offset: 0x00126A5C
		public string SubstringByTextElements(int startingTextElement)
		{
			if (this.Indexes != null)
			{
				return this.SubstringByTextElements(startingTextElement, this.Indexes.Length - startingTextElement);
			}
			if (startingTextElement < 0)
			{
				throw new ArgumentOutOfRangeException("startingTextElement", Environment.GetResourceString("Positive number required."));
			}
			throw new ArgumentOutOfRangeException("startingTextElement", Environment.GetResourceString("Specified argument was out of the range of valid values."));
		}

		/// <summary>Retrieves a substring of text elements from the current <see cref="T:System.Globalization.StringInfo" /> object starting from a specified text element and continuing through the specified number of text elements.</summary>
		/// <param name="startingTextElement">The zero-based index of a text element in this <see cref="T:System.Globalization.StringInfo" /> object.</param>
		/// <param name="lengthInTextElements">The number of text elements to retrieve.</param>
		/// <returns>A substring of text elements in this <see cref="T:System.Globalization.StringInfo" /> object. The substring consists of the number of text elements specified by the <paramref name="lengthInTextElements" /> parameter and starts from the text element index specified by the <paramref name="startingTextElement" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startingTextElement" /> is less than zero.  
		/// -or-  
		/// <paramref name="startingTextElement" /> is greater than or equal to the length of the string that is the value of the current <see cref="T:System.Globalization.StringInfo" /> object.  
		/// -or-  
		/// <paramref name="lengthInTextElements" /> is less than zero.  
		/// -or-  
		/// The string that is the value of the current <see cref="T:System.Globalization.StringInfo" /> object is the empty string ("").  
		/// -or-  
		/// <paramref name="startingTextElement" /> + <paramref name="lengthInTextElements" /> specify an index that is greater than the number of text elements in this <see cref="T:System.Globalization.StringInfo" /> object.</exception>
		// Token: 0x060057E3 RID: 22499 RVA: 0x001288B0 File Offset: 0x00126AB0
		public string SubstringByTextElements(int startingTextElement, int lengthInTextElements)
		{
			if (startingTextElement < 0)
			{
				throw new ArgumentOutOfRangeException("startingTextElement", Environment.GetResourceString("Positive number required."));
			}
			if (this.String.Length == 0 || startingTextElement >= this.Indexes.Length)
			{
				throw new ArgumentOutOfRangeException("startingTextElement", Environment.GetResourceString("Specified argument was out of the range of valid values."));
			}
			if (lengthInTextElements < 0)
			{
				throw new ArgumentOutOfRangeException("lengthInTextElements", Environment.GetResourceString("Positive number required."));
			}
			if (startingTextElement > this.Indexes.Length - lengthInTextElements)
			{
				throw new ArgumentOutOfRangeException("lengthInTextElements", Environment.GetResourceString("Specified argument was out of the range of valid values."));
			}
			int num = this.Indexes[startingTextElement];
			if (startingTextElement + lengthInTextElements == this.Indexes.Length)
			{
				return this.String.Substring(num);
			}
			return this.String.Substring(num, this.Indexes[lengthInTextElements + startingTextElement] - num);
		}

		/// <summary>Gets the first text element in a specified string.</summary>
		/// <param name="str">The string from which to get the text element.</param>
		/// <returns>A string containing the first text element in the specified string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is <see langword="null" />.</exception>
		// Token: 0x060057E4 RID: 22500 RVA: 0x00128979 File Offset: 0x00126B79
		public static string GetNextTextElement(string str)
		{
			return StringInfo.GetNextTextElement(str, 0);
		}

		// Token: 0x060057E5 RID: 22501 RVA: 0x00128984 File Offset: 0x00126B84
		internal static int GetCurrentTextElementLen(string str, int index, int len, ref UnicodeCategory ucCurrent, ref int currentCharCount)
		{
			if (index + currentCharCount == len)
			{
				return currentCharCount;
			}
			int num;
			UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, index + currentCharCount, out num);
			if (CharUnicodeInfo.IsCombiningCategory(unicodeCategory) && !CharUnicodeInfo.IsCombiningCategory(ucCurrent) && ucCurrent != UnicodeCategory.Format && ucCurrent != UnicodeCategory.Control && ucCurrent != UnicodeCategory.OtherNotAssigned && ucCurrent != UnicodeCategory.Surrogate)
			{
				int num2 = index;
				for (index += currentCharCount + num; index < len; index += num)
				{
					unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, index, out num);
					if (!CharUnicodeInfo.IsCombiningCategory(unicodeCategory))
					{
						ucCurrent = unicodeCategory;
						currentCharCount = num;
						break;
					}
				}
				return index - num2;
			}
			int result = currentCharCount;
			ucCurrent = unicodeCategory;
			currentCharCount = num;
			return result;
		}

		/// <summary>Gets the text element at the specified index of the specified string.</summary>
		/// <param name="str">The string from which to get the text element.</param>
		/// <param name="index">The zero-based index at which the text element starts.</param>
		/// <returns>A string containing the text element at the specified index of the specified string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for <paramref name="str" />.</exception>
		// Token: 0x060057E6 RID: 22502 RVA: 0x00128A14 File Offset: 0x00126C14
		public static string GetNextTextElement(string str, int index)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int length = str.Length;
			if (index >= 0 && index < length)
			{
				int num;
				UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, index, out num);
				return str.Substring(index, StringInfo.GetCurrentTextElementLen(str, index, length, ref unicodeCategory, ref num));
			}
			if (index == length)
			{
				return string.Empty;
			}
			throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
		}

		/// <summary>Returns an enumerator that iterates through the text elements of the entire string.</summary>
		/// <param name="str">The string to iterate through.</param>
		/// <returns>A <see cref="T:System.Globalization.TextElementEnumerator" /> for the entire string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is <see langword="null" />.</exception>
		// Token: 0x060057E7 RID: 22503 RVA: 0x00128A7A File Offset: 0x00126C7A
		public static TextElementEnumerator GetTextElementEnumerator(string str)
		{
			return StringInfo.GetTextElementEnumerator(str, 0);
		}

		/// <summary>Returns an enumerator that iterates through the text elements of the string, starting at the specified index.</summary>
		/// <param name="str">The string to iterate through.</param>
		/// <param name="index">The zero-based index at which to start iterating.</param>
		/// <returns>A <see cref="T:System.Globalization.TextElementEnumerator" /> for the string starting at <paramref name="index" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is outside the range of valid indexes for <paramref name="str" />.</exception>
		// Token: 0x060057E8 RID: 22504 RVA: 0x00128A84 File Offset: 0x00126C84
		public static TextElementEnumerator GetTextElementEnumerator(string str, int index)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int length = str.Length;
			if (index < 0 || index > length)
			{
				throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Index was out of range. Must be non-negative and less than the size of the collection."));
			}
			return new TextElementEnumerator(str, index, length);
		}

		/// <summary>Returns the indexes of each base character, high surrogate, or control character within the specified string.</summary>
		/// <param name="str">The string to search.</param>
		/// <returns>An array of integers that contains the zero-based indexes of each base character, high surrogate, or control character within the specified string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is <see langword="null" />.</exception>
		// Token: 0x060057E9 RID: 22505 RVA: 0x00128ACC File Offset: 0x00126CCC
		public static int[] ParseCombiningCharacters(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			int length = str.Length;
			int[] array = new int[length];
			if (length == 0)
			{
				return array;
			}
			int num = 0;
			int i = 0;
			int num2;
			UnicodeCategory unicodeCategory = CharUnicodeInfo.InternalGetUnicodeCategory(str, 0, out num2);
			while (i < length)
			{
				array[num++] = i;
				i += StringInfo.GetCurrentTextElementLen(str, i, length, ref unicodeCategory, ref num2);
			}
			if (num < length)
			{
				int[] array2 = new int[num];
				Array.Copy(array, array2, num);
				return array2;
			}
			return array;
		}

		// Token: 0x0400368C RID: 13964
		[OptionalField(VersionAdded = 2)]
		private string m_str;

		// Token: 0x0400368D RID: 13965
		[NonSerialized]
		private int[] m_indexes;
	}
}
