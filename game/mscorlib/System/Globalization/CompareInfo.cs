using System;
using System.Buffers;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Threading;
using Mono.Globalization.Unicode;
using Unity;

namespace System.Globalization
{
	/// <summary>Implements a set of methods for culture-sensitive string comparisons.</summary>
	// Token: 0x02000955 RID: 2389
	[Serializable]
	public class CompareInfo : IDeserializationCallback
	{
		// Token: 0x0600542E RID: 21550 RVA: 0x001191A8 File Offset: 0x001173A8
		internal unsafe static int InvariantIndexOf(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			char* ptr = source;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = value;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = CompareInfo.InvariantFindString(ptr + startIndex, count, ptr2, value.Length, ignoreCase, true);
			if (num >= 0)
			{
				return num + startIndex;
			}
			return -1;
		}

		// Token: 0x0600542F RID: 21551 RVA: 0x001191FC File Offset: 0x001173FC
		internal unsafe static int InvariantIndexOf(ReadOnlySpan<char> source, ReadOnlySpan<char> value, bool ignoreCase)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(source))
			{
				char* source2 = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(value))
				{
					char* value2 = reference2;
					return CompareInfo.InvariantFindString(source2, source.Length, value2, value.Length, ignoreCase, true);
				}
			}
		}

		// Token: 0x06005430 RID: 21552 RVA: 0x00119234 File Offset: 0x00117434
		internal unsafe static int InvariantLastIndexOf(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			char* ptr = source;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = value;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			int num = CompareInfo.InvariantFindString(ptr + (startIndex - count + 1), count, ptr2, value.Length, ignoreCase, false);
			if (num >= 0)
			{
				return num + startIndex - count + 1;
			}
			return -1;
		}

		// Token: 0x06005431 RID: 21553 RVA: 0x00119290 File Offset: 0x00117490
		private unsafe static int InvariantFindString(char* source, int sourceCount, char* value, int valueCount, bool ignoreCase, bool start)
		{
			if (valueCount == 0)
			{
				if (!start)
				{
					return sourceCount - 1;
				}
				return 0;
			}
			else
			{
				if (sourceCount < valueCount)
				{
					return -1;
				}
				if (start)
				{
					int num = sourceCount - valueCount;
					if (ignoreCase)
					{
						char c = CompareInfo.InvariantToUpper(*value);
						for (int i = 0; i <= num; i++)
						{
							if (CompareInfo.InvariantToUpper(source[i]) == c)
							{
								int j;
								for (j = 1; j < valueCount; j++)
								{
									char c2 = CompareInfo.InvariantToUpper(source[i + j]);
									char c3 = CompareInfo.InvariantToUpper(value[j]);
									if (c2 != c3)
									{
										break;
									}
								}
								if (j == valueCount)
								{
									return i;
								}
							}
						}
					}
					else
					{
						char c4 = *value;
						for (int i = 0; i <= num; i++)
						{
							if (source[i] == c4)
							{
								int j;
								for (j = 1; j < valueCount; j++)
								{
									char c5 = source[i + j];
									char c3 = value[j];
									if (c5 != c3)
									{
										break;
									}
								}
								if (j == valueCount)
								{
									return i;
								}
							}
						}
					}
				}
				else
				{
					int num = sourceCount - valueCount;
					if (ignoreCase)
					{
						char c6 = CompareInfo.InvariantToUpper(*value);
						for (int i = num; i >= 0; i--)
						{
							if (CompareInfo.InvariantToUpper(source[i]) == c6)
							{
								int j;
								for (j = 1; j < valueCount; j++)
								{
									char c7 = CompareInfo.InvariantToUpper(source[i + j]);
									char c3 = CompareInfo.InvariantToUpper(value[j]);
									if (c7 != c3)
									{
										break;
									}
								}
								if (j == valueCount)
								{
									return i;
								}
							}
						}
					}
					else
					{
						char c8 = *value;
						for (int i = num; i >= 0; i--)
						{
							if (source[i] == c8)
							{
								int j;
								for (j = 1; j < valueCount; j++)
								{
									char c9 = source[i + j];
									char c3 = value[j];
									if (c9 != c3)
									{
										break;
									}
								}
								if (j == valueCount)
								{
									return i;
								}
							}
						}
					}
				}
				return -1;
			}
		}

		// Token: 0x06005432 RID: 21554 RVA: 0x00119404 File Offset: 0x00117604
		private static char InvariantToUpper(char c)
		{
			if (c - 'a' > '\u0019')
			{
				return c;
			}
			return c - ' ';
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x00119418 File Offset: 0x00117618
		private unsafe SortKey InvariantCreateSortKey(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			byte[] array;
			if (source.Length == 0)
			{
				array = Array.Empty<byte>();
			}
			else
			{
				array = new byte[source.Length * 2];
				fixed (string text = source)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					byte[] array2;
					byte* ptr2;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array2[0];
					}
					if ((options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) != CompareOptions.None)
					{
						short* ptr3 = (short*)ptr2;
						for (int i = 0; i < source.Length; i++)
						{
							ptr3[i] = (short)CompareInfo.InvariantToUpper(source[i]);
						}
					}
					else
					{
						Buffer.MemoryCopy((void*)ptr, (void*)ptr2, (long)array.Length, (long)array.Length);
					}
					array2 = null;
				}
			}
			return new SortKey(this.Name, source, options, array);
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x001194F4 File Offset: 0x001176F4
		internal CompareInfo(CultureInfo culture)
		{
			this.m_name = culture._name;
			this.InitSort(culture);
		}

		/// <summary>Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated with the specified culture and that uses string comparison methods in the specified <see cref="T:System.Reflection.Assembly" />.</summary>
		/// <param name="culture">An integer representing the culture identifier.</param>
		/// <param name="assembly">An <see cref="T:System.Reflection.Assembly" /> that contains the string comparison methods to use.</param>
		/// <returns>A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture with the specified identifier and using string comparison methods in the current <see cref="T:System.Reflection.Assembly" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assembly" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assembly" /> is of an invalid type.</exception>
		// Token: 0x06005435 RID: 21557 RVA: 0x00119510 File Offset: 0x00117710
		public static CompareInfo GetCompareInfo(int culture, Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (assembly != typeof(object).Module.Assembly)
			{
				throw new ArgumentException("Only mscorlib's assembly is valid.");
			}
			return CompareInfo.GetCompareInfo(culture);
		}

		/// <summary>Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated with the specified culture and that uses string comparison methods in the specified <see cref="T:System.Reflection.Assembly" />.</summary>
		/// <param name="name">A string representing the culture name.</param>
		/// <param name="assembly">An <see cref="T:System.Reflection.Assembly" /> that contains the string comparison methods to use.</param>
		/// <returns>A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture with the specified identifier and using string comparison methods in the current <see cref="T:System.Reflection.Assembly" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="assembly" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an invalid culture name.  
		/// -or-  
		/// <paramref name="assembly" /> is of an invalid type.</exception>
		// Token: 0x06005436 RID: 21558 RVA: 0x00119560 File Offset: 0x00117760
		public static CompareInfo GetCompareInfo(string name, Assembly assembly)
		{
			if (name == null || assembly == null)
			{
				throw new ArgumentNullException((name == null) ? "name" : "assembly");
			}
			if (assembly != typeof(object).Module.Assembly)
			{
				throw new ArgumentException("Only mscorlib's assembly is valid.");
			}
			return CompareInfo.GetCompareInfo(name);
		}

		/// <summary>Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated with the culture with the specified identifier.</summary>
		/// <param name="culture">An integer representing the culture identifier.</param>
		/// <returns>A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture with the specified identifier and using string comparison methods in the current <see cref="T:System.Reflection.Assembly" />.</returns>
		// Token: 0x06005437 RID: 21559 RVA: 0x001195BB File Offset: 0x001177BB
		public static CompareInfo GetCompareInfo(int culture)
		{
			if (CultureData.IsCustomCultureId(culture))
			{
				throw new ArgumentException("Customized cultures cannot be passed by LCID, only by name.", "culture");
			}
			return CultureInfo.GetCultureInfo(culture).CompareInfo;
		}

		/// <summary>Initializes a new <see cref="T:System.Globalization.CompareInfo" /> object that is associated with the culture with the specified name.</summary>
		/// <param name="name">A string representing the culture name.</param>
		/// <returns>A new <see cref="T:System.Globalization.CompareInfo" /> object associated with the culture with the specified identifier and using string comparison methods in the current <see cref="T:System.Reflection.Assembly" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is an invalid culture name.</exception>
		// Token: 0x06005438 RID: 21560 RVA: 0x001195E0 File Offset: 0x001177E0
		public static CompareInfo GetCompareInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return CultureInfo.GetCultureInfo(name).CompareInfo;
		}

		/// <summary>Indicates whether a specified Unicode character is sortable.</summary>
		/// <param name="ch">A Unicode character.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="ch" /> parameter is sortable; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005439 RID: 21561 RVA: 0x001195FB File Offset: 0x001177FB
		public unsafe static bool IsSortable(char ch)
		{
			return GlobalizationMode.Invariant || CompareInfo.IsSortable(&ch, 1);
		}

		/// <summary>Indicates whether a specified Unicode string is sortable.</summary>
		/// <param name="text">A string of zero or more Unicode characters.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="str" /> parameter is not an empty string ("") and all the Unicode characters in <paramref name="str" /> are sortable; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is <see langword="null" />.</exception>
		// Token: 0x0600543A RID: 21562 RVA: 0x00119610 File Offset: 0x00117810
		public unsafe static bool IsSortable(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (text.Length == 0)
			{
				return false;
			}
			if (GlobalizationMode.Invariant)
			{
				return true;
			}
			char* ptr = text;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return CompareInfo.IsSortable(ptr, text.Length);
		}

		// Token: 0x0600543B RID: 21563 RVA: 0x0011965A File Offset: 0x0011785A
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.m_name = null;
		}

		/// <summary>Runs when the entire object graph has been deserialized.</summary>
		/// <param name="sender">The object that initiated the callback.</param>
		// Token: 0x0600543C RID: 21564 RVA: 0x00119663 File Offset: 0x00117863
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.OnDeserialized();
		}

		// Token: 0x0600543D RID: 21565 RVA: 0x00119663 File Offset: 0x00117863
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserialized();
		}

		// Token: 0x0600543E RID: 21566 RVA: 0x0011966C File Offset: 0x0011786C
		private void OnDeserialized()
		{
			if (this.m_name == null)
			{
				CultureInfo cultureInfo = CultureInfo.GetCultureInfo(this.culture);
				this.m_name = cultureInfo._name;
				return;
			}
			this.InitSort(CultureInfo.GetCultureInfo(this.m_name));
		}

		// Token: 0x0600543F RID: 21567 RVA: 0x001196AB File Offset: 0x001178AB
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.culture = CultureInfo.GetCultureInfo(this.Name).LCID;
		}

		/// <summary>Gets the name of the culture used for sorting operations by this <see cref="T:System.Globalization.CompareInfo" /> object.</summary>
		/// <returns>The name of a culture.</returns>
		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06005440 RID: 21568 RVA: 0x001196C3 File Offset: 0x001178C3
		public virtual string Name
		{
			get
			{
				if (this.m_name == "zh-CHT" || this.m_name == "zh-CHS")
				{
					return this.m_name;
				}
				return this._sortName;
			}
		}

		/// <summary>Compares two strings.</summary>
		/// <param name="string1">The first string to compare.</param>
		/// <param name="string2">The second string to compare.</param>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.  
		///   Value  
		///
		///   Condition  
		///
		///   zero  
		///
		///   The two strings are equal.  
		///
		///   less than zero  
		///
		///  <paramref name="string1" /> is less than <paramref name="string2" />.  
		///
		///   greater than zero  
		///
		///  <paramref name="string1" /> is greater than <paramref name="string2" />.</returns>
		// Token: 0x06005441 RID: 21569 RVA: 0x001196F6 File Offset: 0x001178F6
		public virtual int Compare(string string1, string string2)
		{
			return this.Compare(string1, string2, CompareOptions.None);
		}

		/// <summary>Compares two strings using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="string1">The first string to compare.</param>
		/// <param name="string2">The second string to compare.</param>
		/// <param name="options">A value that defines how <paramref name="string1" /> and <paramref name="string2" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.  
		///   Value  
		///
		///   Condition  
		///
		///   zero  
		///
		///   The two strings are equal.  
		///
		///   less than zero  
		///
		///  <paramref name="string1" /> is less than <paramref name="string2" />.  
		///
		///   greater than zero  
		///
		///  <paramref name="string1" /> is greater than <paramref name="string2" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06005442 RID: 21570 RVA: 0x00119704 File Offset: 0x00117904
		public virtual int Compare(string string1, string string2, CompareOptions options)
		{
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return string.Compare(string1, string2, StringComparison.OrdinalIgnoreCase);
			}
			if ((options & CompareOptions.Ordinal) != CompareOptions.None)
			{
				if (options != CompareOptions.Ordinal)
				{
					throw new ArgumentException("CompareOption.Ordinal cannot be used with other options.", "options");
				}
				return string.CompareOrdinal(string1, string2);
			}
			else
			{
				if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
				{
					throw new ArgumentException("Value of flags is invalid.", "options");
				}
				if (string1 == null)
				{
					if (string2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (string2 == null)
					{
						return 1;
					}
					if (!GlobalizationMode.Invariant)
					{
						return this.internal_compare_switch(string1, 0, string1.Length, string2, 0, string2.Length, options);
					}
					if ((options & CompareOptions.IgnoreCase) != CompareOptions.None)
					{
						return CompareInfo.CompareOrdinalIgnoreCase(string1, string2);
					}
					return string.CompareOrdinal(string1, string2);
				}
			}
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x001197B0 File Offset: 0x001179B0
		internal int Compare(ReadOnlySpan<char> string1, string string2, CompareOptions options)
		{
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return CompareInfo.CompareOrdinalIgnoreCase(string1, string2.AsSpan());
			}
			if ((options & CompareOptions.Ordinal) != CompareOptions.None)
			{
				if (options != CompareOptions.Ordinal)
				{
					throw new ArgumentException("CompareOption.Ordinal cannot be used with other options.", "options");
				}
				return string.CompareOrdinal(string1, string2.AsSpan());
			}
			else
			{
				if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
				{
					throw new ArgumentException("Value of flags is invalid.", "options");
				}
				if (string2 == null)
				{
					return 1;
				}
				if (!GlobalizationMode.Invariant)
				{
					return this.CompareString(string1, string2, options);
				}
				if ((options & CompareOptions.IgnoreCase) == CompareOptions.None)
				{
					return string.CompareOrdinal(string1, string2.AsSpan());
				}
				return CompareInfo.CompareOrdinalIgnoreCase(string1, string2.AsSpan());
			}
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x0011984D File Offset: 0x00117A4D
		internal int CompareOptionNone(ReadOnlySpan<char> string1, ReadOnlySpan<char> string2)
		{
			if (string1.Length == 0 || string2.Length == 0)
			{
				return string1.Length - string2.Length;
			}
			if (!GlobalizationMode.Invariant)
			{
				return this.CompareString(string1, string2, CompareOptions.None);
			}
			return string.CompareOrdinal(string1, string2);
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x00119889 File Offset: 0x00117A89
		internal int CompareOptionIgnoreCase(ReadOnlySpan<char> string1, ReadOnlySpan<char> string2)
		{
			if (string1.Length == 0 || string2.Length == 0)
			{
				return string1.Length - string2.Length;
			}
			if (!GlobalizationMode.Invariant)
			{
				return this.CompareString(string1, string2, CompareOptions.IgnoreCase);
			}
			return CompareInfo.CompareOrdinalIgnoreCase(string1, string2);
		}

		/// <summary>Compares a section of one string with a section of another string.</summary>
		/// <param name="string1">The first string to compare.</param>
		/// <param name="offset1">The zero-based index of the character in <paramref name="string1" /> at which to start comparing.</param>
		/// <param name="length1">The number of consecutive characters in <paramref name="string1" /> to compare.</param>
		/// <param name="string2">The second string to compare.</param>
		/// <param name="offset2">The zero-based index of the character in <paramref name="string2" /> at which to start comparing.</param>
		/// <param name="length2">The number of consecutive characters in <paramref name="string2" /> to compare.</param>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.  
		///   Value  
		///
		///   Condition  
		///
		///   zero  
		///
		///   The two strings are equal.  
		///
		///   less than zero  
		///
		///   The specified section of <paramref name="string1" /> is less than the specified section of <paramref name="string2" />.  
		///
		///   greater than zero  
		///
		///   The specified section of <paramref name="string1" /> is greater than the specified section of <paramref name="string2" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset1" /> or <paramref name="length1" /> or <paramref name="offset2" /> or <paramref name="length2" /> is less than zero.  
		/// -or-  
		/// <paramref name="offset1" /> is greater than or equal to the number of characters in <paramref name="string1" />.  
		/// -or-  
		/// <paramref name="offset2" /> is greater than or equal to the number of characters in <paramref name="string2" />.  
		/// -or-  
		/// <paramref name="length1" /> is greater than the number of characters from <paramref name="offset1" /> to the end of <paramref name="string1" />.  
		/// -or-  
		/// <paramref name="length2" /> is greater than the number of characters from <paramref name="offset2" /> to the end of <paramref name="string2" />.</exception>
		// Token: 0x06005446 RID: 21574 RVA: 0x001198C5 File Offset: 0x00117AC5
		public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2)
		{
			return this.Compare(string1, offset1, length1, string2, offset2, length2, CompareOptions.None);
		}

		/// <summary>Compares the end section of a string with the end section of another string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="string1">The first string to compare.</param>
		/// <param name="offset1">The zero-based index of the character in <paramref name="string1" /> at which to start comparing.</param>
		/// <param name="string2">The second string to compare.</param>
		/// <param name="offset2">The zero-based index of the character in <paramref name="string2" /> at which to start comparing.</param>
		/// <param name="options">A value that defines how <paramref name="string1" /> and <paramref name="string2" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.  
		///   Value  
		///
		///   Condition  
		///
		///   zero  
		///
		///   The two strings are equal.  
		///
		///   less than zero  
		///
		///   The specified section of <paramref name="string1" /> is less than the specified section of <paramref name="string2" />.  
		///
		///   greater than zero  
		///
		///   The specified section of <paramref name="string1" /> is greater than the specified section of <paramref name="string2" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset1" /> or <paramref name="offset2" /> is less than zero.  
		/// -or-  
		/// <paramref name="offset1" /> is greater than or equal to the number of characters in <paramref name="string1" />.  
		/// -or-  
		/// <paramref name="offset2" /> is greater than or equal to the number of characters in <paramref name="string2" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06005447 RID: 21575 RVA: 0x001198D7 File Offset: 0x00117AD7
		public virtual int Compare(string string1, int offset1, string string2, int offset2, CompareOptions options)
		{
			return this.Compare(string1, offset1, (string1 == null) ? 0 : (string1.Length - offset1), string2, offset2, (string2 == null) ? 0 : (string2.Length - offset2), options);
		}

		/// <summary>Compares the end section of a string with the end section of another string.</summary>
		/// <param name="string1">The first string to compare.</param>
		/// <param name="offset1">The zero-based index of the character in <paramref name="string1" /> at which to start comparing.</param>
		/// <param name="string2">The second string to compare.</param>
		/// <param name="offset2">The zero-based index of the character in <paramref name="string2" /> at which to start comparing.</param>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.  
		///   Value  
		///
		///   Condition  
		///
		///   zero  
		///
		///   The two strings are equal.  
		///
		///   less than zero  
		///
		///   The specified section of <paramref name="string1" /> is less than the specified section of <paramref name="string2" />.  
		///
		///   greater than zero  
		///
		///   The specified section of <paramref name="string1" /> is greater than the specified section of <paramref name="string2" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset1" /> or <paramref name="offset2" /> is less than zero.  
		/// -or-  
		/// <paramref name="offset1" /> is greater than or equal to the number of characters in <paramref name="string1" />.  
		/// -or-  
		/// <paramref name="offset2" /> is greater than or equal to the number of characters in <paramref name="string2" />.</exception>
		// Token: 0x06005448 RID: 21576 RVA: 0x00119903 File Offset: 0x00117B03
		public virtual int Compare(string string1, int offset1, string string2, int offset2)
		{
			return this.Compare(string1, offset1, string2, offset2, CompareOptions.None);
		}

		/// <summary>Compares a section of one string with a section of another string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="string1">The first string to compare.</param>
		/// <param name="offset1">The zero-based index of the character in <paramref name="string1" /> at which to start comparing.</param>
		/// <param name="length1">The number of consecutive characters in <paramref name="string1" /> to compare.</param>
		/// <param name="string2">The second string to compare.</param>
		/// <param name="offset2">The zero-based index of the character in <paramref name="string2" /> at which to start comparing.</param>
		/// <param name="length2">The number of consecutive characters in <paramref name="string2" /> to compare.</param>
		/// <param name="options">A value that defines how <paramref name="string1" /> and <paramref name="string2" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <returns>A 32-bit signed integer indicating the lexical relationship between the two comparands.  
		///   Value  
		///
		///   Condition  
		///
		///   zero  
		///
		///   The two strings are equal.  
		///
		///   less than zero  
		///
		///   The specified section of <paramref name="string1" /> is less than the specified section of <paramref name="string2" />.  
		///
		///   greater than zero  
		///
		///   The specified section of <paramref name="string1" /> is greater than the specified section of <paramref name="string2" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset1" /> or <paramref name="length1" /> or <paramref name="offset2" /> or <paramref name="length2" /> is less than zero.  
		/// -or-  
		/// <paramref name="offset1" /> is greater than or equal to the number of characters in <paramref name="string1" />.  
		/// -or-  
		/// <paramref name="offset2" /> is greater than or equal to the number of characters in <paramref name="string2" />.  
		/// -or-  
		/// <paramref name="length1" /> is greater than the number of characters from <paramref name="offset1" /> to the end of <paramref name="string1" />.  
		/// -or-  
		/// <paramref name="length2" /> is greater than the number of characters from <paramref name="offset2" /> to the end of <paramref name="string2" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06005449 RID: 21577 RVA: 0x00119914 File Offset: 0x00117B14
		public virtual int Compare(string string1, int offset1, int length1, string string2, int offset2, int length2, CompareOptions options)
		{
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				int num = string.Compare(string1, offset1, string2, offset2, (length1 < length2) ? length1 : length2, StringComparison.OrdinalIgnoreCase);
				if (length1 == length2 || num != 0)
				{
					return num;
				}
				if (length1 <= length2)
				{
					return -1;
				}
				return 1;
			}
			else
			{
				if (length1 < 0 || length2 < 0)
				{
					throw new ArgumentOutOfRangeException((length1 < 0) ? "length1" : "length2", "Positive number required.");
				}
				if (offset1 < 0 || offset2 < 0)
				{
					throw new ArgumentOutOfRangeException((offset1 < 0) ? "offset1" : "offset2", "Positive number required.");
				}
				if (offset1 > ((string1 == null) ? 0 : string1.Length) - length1)
				{
					throw new ArgumentOutOfRangeException("string1", "Offset and length must refer to a position in the string.");
				}
				if (offset2 > ((string2 == null) ? 0 : string2.Length) - length2)
				{
					throw new ArgumentOutOfRangeException("string2", "Offset and length must refer to a position in the string.");
				}
				if ((options & CompareOptions.Ordinal) != CompareOptions.None)
				{
					if (options != CompareOptions.Ordinal)
					{
						throw new ArgumentException("CompareOption.Ordinal cannot be used with other options.", "options");
					}
				}
				else if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
				{
					throw new ArgumentException("Value of flags is invalid.", "options");
				}
				if (string1 == null)
				{
					if (string2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (string2 == null)
					{
						return 1;
					}
					ReadOnlySpan<char> strA = string1.AsSpan(offset1, length1);
					ReadOnlySpan<char> strB = string2.AsSpan(offset2, length2);
					if (options == CompareOptions.Ordinal)
					{
						return string.CompareOrdinal(strA, strB);
					}
					if (!GlobalizationMode.Invariant)
					{
						return this.internal_compare_switch(string1, offset1, length1, string2, offset2, length2, options);
					}
					if ((options & CompareOptions.IgnoreCase) != CompareOptions.None)
					{
						return CompareInfo.CompareOrdinalIgnoreCase(strA, strB);
					}
					return string.CompareOrdinal(strA, strB);
				}
			}
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x00119A84 File Offset: 0x00117C84
		internal static int CompareOrdinalIgnoreCase(string strA, int indexA, int lengthA, string strB, int indexB, int lengthB)
		{
			return CompareInfo.CompareOrdinalIgnoreCase(strA.AsSpan(indexA, lengthA), strB.AsSpan(indexB, lengthB));
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x00119AA0 File Offset: 0x00117CA0
		internal unsafe static int CompareOrdinalIgnoreCase(ReadOnlySpan<char> strA, ReadOnlySpan<char> strB)
		{
			int num = Math.Min(strA.Length, strB.Length);
			int num2 = num;
			fixed (char* reference = MemoryMarshal.GetReference<char>(strA))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(strB))
				{
					char* ptr2 = reference2;
					char* ptr3 = ptr;
					char* ptr4 = ptr2;
					char c = GlobalizationMode.Invariant ? char.MaxValue : '\u007f';
					while (num != 0 && *ptr3 <= c && *ptr4 <= c)
					{
						int num3 = (int)(*ptr3);
						int num4 = (int)(*ptr4);
						if (num3 == num4)
						{
							ptr3++;
							ptr4++;
							num--;
						}
						else
						{
							if (num3 - 97 <= 25)
							{
								num3 -= 32;
							}
							if (num4 - 97 <= 25)
							{
								num4 -= 32;
							}
							if (num3 != num4)
							{
								return num3 - num4;
							}
							ptr3++;
							ptr4++;
							num--;
						}
					}
					if (num == 0)
					{
						return strA.Length - strB.Length;
					}
					num2 -= num;
					return CompareInfo.CompareStringOrdinalIgnoreCase(ptr3, strA.Length - num2, ptr4, strB.Length - num2);
				}
			}
		}

		/// <summary>Determines whether the specified source string starts with the specified prefix using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search in.</param>
		/// <param name="prefix">The string to compare with the beginning of <paramref name="source" />.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="prefix" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>
		///   <see langword="true" /> if the length of <paramref name="prefix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> starts with <paramref name="prefix" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="prefix" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x0600544C RID: 21580 RVA: 0x00119B94 File Offset: 0x00117D94
		public virtual bool IsPrefix(string source, string prefix, CompareOptions options)
		{
			if (source == null || prefix == null)
			{
				throw new ArgumentNullException((source == null) ? "source" : "prefix", "String reference not set to an instance of a String.");
			}
			if (prefix.Length == 0)
			{
				return true;
			}
			if (source.Length == 0)
			{
				return false;
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
			}
			if (options == CompareOptions.Ordinal)
			{
				return source.StartsWith(prefix, StringComparison.Ordinal);
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			if (GlobalizationMode.Invariant)
			{
				return source.StartsWith(prefix, ((options & CompareOptions.IgnoreCase) != CompareOptions.None) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			}
			return this.StartsWith(source, prefix, options);
		}

		// Token: 0x0600544D RID: 21581 RVA: 0x00119C2E File Offset: 0x00117E2E
		internal bool IsPrefix(ReadOnlySpan<char> source, ReadOnlySpan<char> prefix, CompareOptions options)
		{
			return this.StartsWith(source, prefix, options);
		}

		/// <summary>Determines whether the specified source string starts with the specified prefix.</summary>
		/// <param name="source">The string to search in.</param>
		/// <param name="prefix">The string to compare with the beginning of <paramref name="source" />.</param>
		/// <returns>
		///   <see langword="true" /> if the length of <paramref name="prefix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> starts with <paramref name="prefix" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="prefix" /> is <see langword="null" />.</exception>
		// Token: 0x0600544E RID: 21582 RVA: 0x00119C39 File Offset: 0x00117E39
		public virtual bool IsPrefix(string source, string prefix)
		{
			return this.IsPrefix(source, prefix, CompareOptions.None);
		}

		/// <summary>Determines whether the specified source string ends with the specified suffix using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search in.</param>
		/// <param name="suffix">The string to compare with the end of <paramref name="source" />.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="suffix" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" /> used by itself, or the bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>
		///   <see langword="true" /> if the length of <paramref name="suffix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> ends with <paramref name="suffix" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="suffix" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x0600544F RID: 21583 RVA: 0x00119C44 File Offset: 0x00117E44
		public virtual bool IsSuffix(string source, string suffix, CompareOptions options)
		{
			if (source == null || suffix == null)
			{
				throw new ArgumentNullException((source == null) ? "source" : "suffix", "String reference not set to an instance of a String.");
			}
			if (suffix.Length == 0)
			{
				return true;
			}
			if (source.Length == 0)
			{
				return false;
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
			}
			if (options == CompareOptions.Ordinal)
			{
				return source.EndsWith(suffix, StringComparison.Ordinal);
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			if (GlobalizationMode.Invariant)
			{
				return source.EndsWith(suffix, ((options & CompareOptions.IgnoreCase) != CompareOptions.None) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			}
			return this.EndsWith(source, suffix, options);
		}

		// Token: 0x06005450 RID: 21584 RVA: 0x00119CDE File Offset: 0x00117EDE
		internal bool IsSuffix(ReadOnlySpan<char> source, ReadOnlySpan<char> suffix, CompareOptions options)
		{
			return this.EndsWith(source, suffix, options);
		}

		/// <summary>Determines whether the specified source string ends with the specified suffix.</summary>
		/// <param name="source">The string to search in.</param>
		/// <param name="suffix">The string to compare with the end of <paramref name="source" />.</param>
		/// <returns>
		///   <see langword="true" /> if the length of <paramref name="suffix" /> is less than or equal to the length of <paramref name="source" /> and <paramref name="source" /> ends with <paramref name="suffix" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="suffix" /> is <see langword="null" />.</exception>
		// Token: 0x06005451 RID: 21585 RVA: 0x00119CE9 File Offset: 0x00117EE9
		public virtual bool IsSuffix(string source, string suffix)
		{
			return this.IsSuffix(source, suffix, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the entire source string.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within <paramref name="source" />; otherwise, -1. Returns 0 (zero) if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06005452 RID: 21586 RVA: 0x00119CF4 File Offset: 0x00117EF4
		public virtual int IndexOf(string source, char value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the entire source string.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within <paramref name="source" />; otherwise, -1. Returns 0 (zero) if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06005453 RID: 21587 RVA: 0x00119D14 File Offset: 0x00117F14
		public virtual int IndexOf(string source, string value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="options">A value that defines how the strings should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within <paramref name="source" />, using the specified comparison options; otherwise, -1. Returns 0 (zero) if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06005454 RID: 21588 RVA: 0x00119D34 File Offset: 0x00117F34
		public virtual int IndexOf(string source, char value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within <paramref name="source" />, using the specified comparison options; otherwise, -1. Returns 0 (zero) if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06005455 RID: 21589 RVA: 0x00119D54 File Offset: 0x00117F54
		public virtual int IndexOf(string source, string value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, 0, source.Length, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the section of the source string that extends from the specified index to the end of the string.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from <paramref name="startIndex" /> to the end of <paramref name="source" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.</exception>
		// Token: 0x06005456 RID: 21590 RVA: 0x00119D74 File Offset: 0x00117F74
		public virtual int IndexOf(string source, char value, int startIndex)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the section of the source string that extends from the specified index to the end of the string.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from <paramref name="startIndex" /> to the end of <paramref name="source" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.</exception>
		// Token: 0x06005457 RID: 21591 RVA: 0x00119D96 File Offset: 0x00117F96
		public virtual int IndexOf(string source, string value, int startIndex)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the section of the source string that extends from the specified index to the end of the string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from <paramref name="startIndex" /> to the end of <paramref name="source" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06005458 RID: 21592 RVA: 0x00119DB8 File Offset: 0x00117FB8
		public virtual int IndexOf(string source, char value, int startIndex, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the section of the source string that extends from the specified index to the end of the string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from <paramref name="startIndex" /> to the end of <paramref name="source" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06005459 RID: 21593 RVA: 0x00119DDB File Offset: 0x00117FDB
		public virtual int IndexOf(string source, string value, int startIndex, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.IndexOf(source, value, startIndex, source.Length - startIndex, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />.</exception>
		// Token: 0x0600545A RID: 21594 RVA: 0x00119DFE File Offset: 0x00117FFE
		public virtual int IndexOf(string source, char value, int startIndex, int count)
		{
			return this.IndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />.</exception>
		// Token: 0x0600545B RID: 21595 RVA: 0x00119E0C File Offset: 0x0011800C
		public virtual int IndexOf(string source, string value, int startIndex, int count)
		{
			return this.IndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x0600545C RID: 21596 RVA: 0x00119E1C File Offset: 0x0011801C
		public virtual int IndexOf(string source, char value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (startIndex < 0 || startIndex > source.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (count < 0 || startIndex > source.Length - count)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			if (source.Length == 0)
			{
				return -1;
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.IndexOf(value.ToString(), startIndex, count, StringComparison.OrdinalIgnoreCase);
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			if (GlobalizationMode.Invariant)
			{
				return this.IndexOfOrdinal(source, new string(value, 1), startIndex, count, (options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) > CompareOptions.None);
			}
			return this.IndexOfCore(source, new string(value, 1), startIndex, count, options, null);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the first occurrence within the section of the source string that starts at the specified index and contains the specified number of elements using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the first occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that starts at <paramref name="startIndex" /> and contains the number of elements specified by <paramref name="count" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x0600545D RID: 21597 RVA: 0x00119EF4 File Offset: 0x001180F4
		public virtual int IndexOf(string source, string value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (startIndex > source.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (source.Length == 0)
			{
				if (value.Length == 0)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (count < 0 || startIndex > source.Length - count)
				{
					throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					return this.IndexOfOrdinal(source, value, startIndex, count, true);
				}
				if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal)
				{
					throw new ArgumentException("Value of flags is invalid.", "options");
				}
				if (GlobalizationMode.Invariant)
				{
					return this.IndexOfOrdinal(source, value, startIndex, count, (options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) > CompareOptions.None);
				}
				return this.IndexOfCore(source, value, startIndex, count, options, null);
			}
		}

		// Token: 0x0600545E RID: 21598 RVA: 0x00119FE2 File Offset: 0x001181E2
		internal int IndexOfOrdinal(ReadOnlySpan<char> source, ReadOnlySpan<char> value, bool ignoreCase)
		{
			return this.IndexOfOrdinalCore(source, value, ignoreCase);
		}

		// Token: 0x0600545F RID: 21599 RVA: 0x00119FED File Offset: 0x001181ED
		internal int IndexOf(ReadOnlySpan<char> source, ReadOnlySpan<char> value, CompareOptions options)
		{
			return this.IndexOfCore(source, value, options, null);
		}

		// Token: 0x06005460 RID: 21600 RVA: 0x00119FFC File Offset: 0x001181FC
		internal unsafe int IndexOf(string source, string value, int startIndex, int count, CompareOptions options, int* matchLengthPtr)
		{
			*matchLengthPtr = 0;
			if (source.Length == 0)
			{
				if (value.Length == 0)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (startIndex >= source.Length)
				{
					return -1;
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					int num = this.IndexOfOrdinal(source, value, startIndex, count, true);
					if (num >= 0)
					{
						*matchLengthPtr = value.Length;
					}
					return num;
				}
				if (GlobalizationMode.Invariant)
				{
					int num2 = this.IndexOfOrdinal(source, value, startIndex, count, (options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) > CompareOptions.None);
					if (num2 >= 0)
					{
						*matchLengthPtr = value.Length;
					}
					return num2;
				}
				return this.IndexOfCore(source, value, startIndex, count, options, matchLengthPtr);
			}
		}

		// Token: 0x06005461 RID: 21601 RVA: 0x0011A089 File Offset: 0x00118289
		internal int IndexOfOrdinal(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			if (GlobalizationMode.Invariant)
			{
				return CompareInfo.InvariantIndexOf(source, value, startIndex, count, ignoreCase);
			}
			return CompareInfo.IndexOfOrdinalCore(source, value, startIndex, count, ignoreCase);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the entire source string.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within <paramref name="source" />; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06005462 RID: 21602 RVA: 0x0011A0AB File Offset: 0x001182AB
		public virtual int LastIndexOf(string source, char value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the entire source string.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within <paramref name="source" />; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		// Token: 0x06005463 RID: 21603 RVA: 0x0011A0D2 File Offset: 0x001182D2
		public virtual int LastIndexOf(string source, string value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within <paramref name="source" />, using the specified comparison options; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06005464 RID: 21604 RVA: 0x0011A0F9 File Offset: 0x001182F9
		public virtual int LastIndexOf(string source, char value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the entire source string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within <paramref name="source" />, using the specified comparison options; otherwise, -1.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06005465 RID: 21605 RVA: 0x0011A120 File Offset: 0x00118320
		public virtual int LastIndexOf(string source, string value, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return this.LastIndexOf(source, value, source.Length - 1, source.Length, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the section of the source string that extends from the beginning of the string to the specified index.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" /> to <paramref name="startIndex" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.</exception>
		// Token: 0x06005466 RID: 21606 RVA: 0x0011A147 File Offset: 0x00118347
		public virtual int LastIndexOf(string source, char value, int startIndex)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the section of the source string that extends from the beginning of the string to the specified index.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" /> to <paramref name="startIndex" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.</exception>
		// Token: 0x06005467 RID: 21607 RVA: 0x0011A156 File Offset: 0x00118356
		public virtual int LastIndexOf(string source, string value, int startIndex)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the section of the source string that extends from the beginning of the string to the specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" /> to <paramref name="startIndex" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06005468 RID: 21608 RVA: 0x0011A165 File Offset: 0x00118365
		public virtual int LastIndexOf(string source, char value, int startIndex, CompareOptions options)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the section of the source string that extends from the beginning of the string to the specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that extends from the beginning of <paramref name="source" /> to <paramref name="startIndex" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x06005469 RID: 21609 RVA: 0x0011A175 File Offset: 0x00118375
		public virtual int LastIndexOf(string source, string value, int startIndex, CompareOptions options)
		{
			return this.LastIndexOf(source, value, startIndex, startIndex + 1, options);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the section of the source string that contains the specified number of elements and ends at the specified index.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that contains the number of elements specified by <paramref name="count" /> and that ends at <paramref name="startIndex" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />.</exception>
		// Token: 0x0600546A RID: 21610 RVA: 0x0011A185 File Offset: 0x00118385
		public virtual int LastIndexOf(string source, char value, int startIndex, int count)
		{
			return this.LastIndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the section of the source string that contains the specified number of elements and ends at the specified index.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that contains the number of elements specified by <paramref name="count" /> and that ends at <paramref name="startIndex" />; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />.</exception>
		// Token: 0x0600546B RID: 21611 RVA: 0x0011A193 File Offset: 0x00118393
		public virtual int LastIndexOf(string source, string value, int startIndex, int count)
		{
			return this.LastIndexOf(source, value, startIndex, count, CompareOptions.None);
		}

		/// <summary>Searches for the specified character and returns the zero-based index of the last occurrence within the section of the source string that contains the specified number of elements and ends at the specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The character to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that contains the number of elements specified by <paramref name="count" /> and that ends at <paramref name="startIndex" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x0600546C RID: 21612 RVA: 0x0011A1A4 File Offset: 0x001183A4
		public virtual int LastIndexOf(string source, char value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal && options != CompareOptions.OrdinalIgnoreCase)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			if (source.Length == 0 && (startIndex == -1 || startIndex == 0))
			{
				return -1;
			}
			if (startIndex < 0 || startIndex > source.Length)
			{
				throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (startIndex == source.Length)
			{
				startIndex--;
				if (count > 0)
				{
					count--;
				}
			}
			if (count < 0 || startIndex - count + 1 < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return source.LastIndexOf(value.ToString(), startIndex, count, StringComparison.OrdinalIgnoreCase);
			}
			if (GlobalizationMode.Invariant)
			{
				return CompareInfo.InvariantLastIndexOf(source, new string(value, 1), startIndex, count, (options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) > CompareOptions.None);
			}
			return this.LastIndexOfCore(source, value.ToString(), startIndex, count, options);
		}

		/// <summary>Searches for the specified substring and returns the zero-based index of the last occurrence within the section of the source string that contains the specified number of elements and ends at the specified index using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string to search.</param>
		/// <param name="value">The string to locate within <paramref name="source" />.</param>
		/// <param name="startIndex">The zero-based starting index of the backward search.</param>
		/// <param name="count">The number of elements in the section to search.</param>
		/// <param name="options">A value that defines how <paramref name="source" /> and <paramref name="value" /> should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, and <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />.</param>
		/// <returns>The zero-based index of the last occurrence of <paramref name="value" />, if found, within the section of <paramref name="source" /> that contains the number of elements specified by <paramref name="count" /> and that ends at <paramref name="startIndex" />, using the specified comparison options; otherwise, -1. Returns <paramref name="startIndex" /> if <paramref name="value" /> is an ignorable character.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> is outside the range of valid indexes for <paramref name="source" />.  
		/// -or-  
		/// <paramref name="count" /> is less than zero.  
		/// -or-  
		/// <paramref name="startIndex" /> and <paramref name="count" /> do not specify a valid section in <paramref name="source" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x0600546D RID: 21613 RVA: 0x0011A2A0 File Offset: 0x001184A0
		public virtual int LastIndexOf(string source, string value, int startIndex, int count, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None && options != CompareOptions.Ordinal && options != CompareOptions.OrdinalIgnoreCase)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			if (source.Length == 0 && (startIndex == -1 || startIndex == 0))
			{
				if (value.Length != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (startIndex < 0 || startIndex > source.Length)
				{
					throw new ArgumentOutOfRangeException("startIndex", "Index was out of range. Must be non-negative and less than the size of the collection.");
				}
				if (startIndex == source.Length)
				{
					startIndex--;
					if (count > 0)
					{
						count--;
					}
					if (value.Length == 0 && count >= 0 && startIndex - count + 1 >= 0)
					{
						return startIndex;
					}
				}
				if (count < 0 || startIndex - count + 1 < 0)
				{
					throw new ArgumentOutOfRangeException("count", "Count must be positive and count must refer to a location within the string/array/collection.");
				}
				if (options == CompareOptions.OrdinalIgnoreCase)
				{
					return this.LastIndexOfOrdinal(source, value, startIndex, count, true);
				}
				if (GlobalizationMode.Invariant)
				{
					return CompareInfo.InvariantLastIndexOf(source, value, startIndex, count, (options & (CompareOptions.IgnoreCase | CompareOptions.OrdinalIgnoreCase)) > CompareOptions.None);
				}
				return this.LastIndexOfCore(source, value, startIndex, count, options);
			}
		}

		// Token: 0x0600546E RID: 21614 RVA: 0x0011A3B9 File Offset: 0x001185B9
		internal int LastIndexOfOrdinal(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			if (GlobalizationMode.Invariant)
			{
				return CompareInfo.InvariantLastIndexOf(source, value, startIndex, count, ignoreCase);
			}
			return CompareInfo.LastIndexOfOrdinalCore(source, value, startIndex, count, ignoreCase);
		}

		/// <summary>Gets a <see cref="T:System.Globalization.SortKey" /> object for the specified string using the specified <see cref="T:System.Globalization.CompareOptions" /> value.</summary>
		/// <param name="source">The string for which a <see cref="T:System.Globalization.SortKey" /> object is obtained.</param>
		/// <param name="options">A bitwise combination of one or more of the following enumeration values that define how the sort key is calculated: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <returns>The <see cref="T:System.Globalization.SortKey" /> object that contains the sort key for the specified string.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> contains an invalid <see cref="T:System.Globalization.CompareOptions" /> value.</exception>
		// Token: 0x0600546F RID: 21615 RVA: 0x0011A3DB File Offset: 0x001185DB
		public virtual SortKey GetSortKey(string source, CompareOptions options)
		{
			if (GlobalizationMode.Invariant)
			{
				return this.InvariantCreateSortKey(source, options);
			}
			return this.CreateSortKey(source, options);
		}

		/// <summary>Gets the sort key for the specified string.</summary>
		/// <param name="source">The string for which a <see cref="T:System.Globalization.SortKey" /> object is obtained.</param>
		/// <returns>The <see cref="T:System.Globalization.SortKey" /> object that contains the sort key for the specified string.</returns>
		// Token: 0x06005470 RID: 21616 RVA: 0x0011A3F5 File Offset: 0x001185F5
		public virtual SortKey GetSortKey(string source)
		{
			if (GlobalizationMode.Invariant)
			{
				return this.InvariantCreateSortKey(source, CompareOptions.None);
			}
			return this.CreateSortKey(source, CompareOptions.None);
		}

		/// <summary>Determines whether the specified object is equal to the current <see cref="T:System.Globalization.CompareInfo" /> object.</summary>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.CompareInfo" />.</param>
		/// <returns>
		///   <see langword="true" /> if the specified object is equal to the current <see cref="T:System.Globalization.CompareInfo" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005471 RID: 21617 RVA: 0x0011A410 File Offset: 0x00118610
		public override bool Equals(object value)
		{
			CompareInfo compareInfo = value as CompareInfo;
			return compareInfo != null && this.Name == compareInfo.Name;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.CompareInfo" /> for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.CompareInfo" />.</returns>
		// Token: 0x06005472 RID: 21618 RVA: 0x0011A43A File Offset: 0x0011863A
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06005473 RID: 21619 RVA: 0x0011A448 File Offset: 0x00118648
		internal unsafe static int GetIgnoreCaseHash(string source)
		{
			if (source.Length == 0)
			{
				return source.GetHashCode();
			}
			char[] array = null;
			Span<char> span;
			if (source.Length <= 255)
			{
				span = new Span<char>(stackalloc byte[(UIntPtr)510], 255);
			}
			else
			{
				span = (array = ArrayPool<char>.Shared.Rent(source.Length));
			}
			Span<char> destination = span;
			int length = source.AsSpan().ToUpperInvariant(destination);
			int result = Marvin.ComputeHash32(MemoryMarshal.AsBytes<char>(destination.Slice(0, length)), Marvin.DefaultSeed);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			return result;
		}

		// Token: 0x06005474 RID: 21620 RVA: 0x0011A4DC File Offset: 0x001186DC
		internal int GetHashCodeOfString(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth)) != CompareOptions.None)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			if (!GlobalizationMode.Invariant)
			{
				return this.GetHashCodeOfStringCore(source, options);
			}
			if ((options & CompareOptions.IgnoreCase) == CompareOptions.None)
			{
				return source.GetHashCode();
			}
			return CompareInfo.GetIgnoreCaseHash(source);
		}

		/// <summary>Gets the hash code for a string based on specified comparison options.</summary>
		/// <param name="source">The string whose hash code is to be returned.</param>
		/// <param name="options">A value that determines how strings are compared.</param>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is <see langword="null" />.</exception>
		// Token: 0x06005475 RID: 21621 RVA: 0x0011A52F File Offset: 0x0011872F
		public virtual int GetHashCode(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (options == CompareOptions.Ordinal)
			{
				return source.GetHashCode();
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return CompareInfo.GetIgnoreCaseHash(source);
			}
			return this.GetHashCodeOfString(source, options);
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Globalization.CompareInfo" /> object.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Globalization.CompareInfo" /> object.</returns>
		// Token: 0x06005476 RID: 21622 RVA: 0x0011A565 File Offset: 0x00118765
		public override string ToString()
		{
			return "CompareInfo - " + this.Name;
		}

		/// <summary>Gets information about the version of Unicode used for comparing and sorting strings.</summary>
		/// <returns>An object that contains information about the Unicode version used for comparing and sorting strings.</returns>
		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06005477 RID: 21623 RVA: 0x0011A578 File Offset: 0x00118778
		public SortVersion Version
		{
			get
			{
				if (this.m_SortVersion == null)
				{
					if (GlobalizationMode.Invariant)
					{
						this.m_SortVersion = new SortVersion(0, 127, new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 127));
					}
					else
					{
						this.m_SortVersion = this.GetSortVersion();
					}
				}
				return this.m_SortVersion;
			}
		}

		/// <summary>Gets the properly formed culture identifier for the current <see cref="T:System.Globalization.CompareInfo" />.</summary>
		/// <returns>The properly formed culture identifier for the current <see cref="T:System.Globalization.CompareInfo" />.</returns>
		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06005478 RID: 21624 RVA: 0x0011A5CD File Offset: 0x001187CD
		public int LCID
		{
			get
			{
				return CultureInfo.GetCultureInfo(this.Name).LCID;
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x06005479 RID: 21625 RVA: 0x0011A5DF File Offset: 0x001187DF
		private static bool UseManagedCollation
		{
			get
			{
				if (!CompareInfo.managedCollationChecked)
				{
					CompareInfo.managedCollation = (Environment.internalGetEnvironmentVariable("MONO_DISABLE_MANAGED_COLLATION") != "yes" && MSCompatUnicodeTable.IsReady);
					CompareInfo.managedCollationChecked = true;
				}
				return CompareInfo.managedCollation;
			}
		}

		// Token: 0x0600547A RID: 21626 RVA: 0x0011A618 File Offset: 0x00118818
		private ISimpleCollator GetCollator()
		{
			if (this.collator != null)
			{
				return this.collator;
			}
			if (CompareInfo.collators == null)
			{
				Interlocked.CompareExchange<Dictionary<string, ISimpleCollator>>(ref CompareInfo.collators, new Dictionary<string, ISimpleCollator>(StringComparer.Ordinal), null);
			}
			Dictionary<string, ISimpleCollator> obj = CompareInfo.collators;
			lock (obj)
			{
				if (!CompareInfo.collators.TryGetValue(this._sortName, out this.collator))
				{
					this.collator = new SimpleCollator(CultureInfo.GetCultureInfo(this.m_name));
					CompareInfo.collators[this._sortName] = this.collator;
				}
			}
			return this.collator;
		}

		// Token: 0x0600547B RID: 21627 RVA: 0x0011A6C8 File Offset: 0x001188C8
		private SortKey CreateSortKeyCore(string source, CompareOptions options)
		{
			if (CompareInfo.UseManagedCollation)
			{
				return this.GetCollator().GetSortKey(source, options);
			}
			return new SortKey(this.culture, source, options);
		}

		// Token: 0x0600547C RID: 21628 RVA: 0x0011A6EC File Offset: 0x001188EC
		private int internal_index_switch(string s1, int sindex, int count, string s2, CompareOptions opt, bool first)
		{
			if (opt == CompareOptions.Ordinal)
			{
				if (!first)
				{
					return s1.LastIndexOfUnchecked(s2, sindex, count);
				}
				return s1.IndexOfUnchecked(s2, sindex, count);
			}
			else
			{
				if (!CompareInfo.UseManagedCollation)
				{
					return CompareInfo.internal_index(s1, sindex, count, s2, first);
				}
				return this.internal_index_managed(s1, sindex, count, s2, opt, first);
			}
		}

		// Token: 0x0600547D RID: 21629 RVA: 0x0011A73F File Offset: 0x0011893F
		private int internal_compare_switch(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options)
		{
			if (!CompareInfo.UseManagedCollation)
			{
				return CompareInfo.internal_compare(str1, offset1, length1, str2, offset2, length2, options);
			}
			return this.internal_compare_managed(str1, offset1, length1, str2, offset2, length2, options);
		}

		// Token: 0x0600547E RID: 21630 RVA: 0x0011A76A File Offset: 0x0011896A
		private int internal_compare_managed(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options)
		{
			return this.GetCollator().Compare(str1, offset1, length1, str2, offset2, length2, options);
		}

		// Token: 0x0600547F RID: 21631 RVA: 0x0011A782 File Offset: 0x00118982
		private int internal_index_managed(string s, int sindex, int count, char c, CompareOptions opt, bool first)
		{
			if (!first)
			{
				return this.GetCollator().LastIndexOf(s, c, sindex, count, opt);
			}
			return this.GetCollator().IndexOf(s, c, sindex, count, opt);
		}

		// Token: 0x06005480 RID: 21632 RVA: 0x0011A7AD File Offset: 0x001189AD
		private int internal_index_managed(string s1, int sindex, int count, string s2, CompareOptions opt, bool first)
		{
			if (!first)
			{
				return this.GetCollator().LastIndexOf(s1, s2, sindex, count, opt);
			}
			return this.GetCollator().IndexOf(s1, s2, sindex, count, opt);
		}

		// Token: 0x06005481 RID: 21633
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int internal_compare_icall(char* str1, int length1, char* str2, int length2, CompareOptions options);

		// Token: 0x06005482 RID: 21634 RVA: 0x0011A7D8 File Offset: 0x001189D8
		private unsafe static int internal_compare(string str1, int offset1, int length1, string str2, int offset2, int length2, CompareOptions options)
		{
			char* ptr = str1;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = str2;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return CompareInfo.internal_compare_icall(ptr + offset1, length1, ptr2 + offset2, length2, options);
		}

		// Token: 0x06005483 RID: 21635
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int internal_index_icall(char* source, int sindex, int count, char* value, int value_length, bool first);

		// Token: 0x06005484 RID: 21636 RVA: 0x0011A81C File Offset: 0x00118A1C
		private unsafe static int internal_index(string source, int sindex, int count, string value, bool first)
		{
			char* ptr = source;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			char* ptr2 = value;
			if (ptr2 != null)
			{
				ptr2 += RuntimeHelpers.OffsetToStringData / 2;
			}
			return CompareInfo.internal_index_icall(ptr, sindex, count, ptr2, (value != null) ? value.Length : 0, first);
		}

		// Token: 0x06005485 RID: 21637 RVA: 0x0011A860 File Offset: 0x00118A60
		private void InitSort(CultureInfo culture)
		{
			this._sortName = culture.SortName;
		}

		// Token: 0x06005486 RID: 21638 RVA: 0x0011A870 File Offset: 0x00118A70
		private unsafe static int CompareStringOrdinalIgnoreCase(char* pString1, int length1, char* pString2, int length2)
		{
			TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
			int num = 0;
			while (num < length1 && num < length2 && textInfo.ToUpper(*pString1) == textInfo.ToUpper(*pString2))
			{
				num++;
				pString1++;
				pString2++;
			}
			if (num >= length1)
			{
				if (num >= length2)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (num >= length2)
				{
					return 1;
				}
				return (int)(textInfo.ToUpper(*pString1) - textInfo.ToUpper(*pString2));
			}
		}

		// Token: 0x06005487 RID: 21639 RVA: 0x0011A8D7 File Offset: 0x00118AD7
		internal static int IndexOfOrdinalCore(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			if (!ignoreCase)
			{
				return source.IndexOfUnchecked(value, startIndex, count);
			}
			return source.IndexOfUncheckedIgnoreCase(value, startIndex, count);
		}

		// Token: 0x06005488 RID: 21640 RVA: 0x0011A8F0 File Offset: 0x00118AF0
		internal static int LastIndexOfOrdinalCore(string source, string value, int startIndex, int count, bool ignoreCase)
		{
			if (!ignoreCase)
			{
				return source.LastIndexOfUnchecked(value, startIndex, count);
			}
			return source.LastIndexOfUncheckedIgnoreCase(value, startIndex, count);
		}

		// Token: 0x06005489 RID: 21641 RVA: 0x0011A909 File Offset: 0x00118B09
		private int LastIndexOfCore(string source, string target, int startIndex, int count, CompareOptions options)
		{
			return this.internal_index_switch(source, startIndex, count, target, options, false);
		}

		// Token: 0x0600548A RID: 21642 RVA: 0x0011A919 File Offset: 0x00118B19
		private unsafe int IndexOfCore(string source, string target, int startIndex, int count, CompareOptions options, int* matchLengthPtr)
		{
			if (matchLengthPtr != null)
			{
				throw new NotImplementedException();
			}
			return this.internal_index_switch(source, startIndex, count, target, options, true);
		}

		// Token: 0x0600548B RID: 21643 RVA: 0x0011A938 File Offset: 0x00118B38
		private unsafe int IndexOfCore(ReadOnlySpan<char> source, ReadOnlySpan<char> target, CompareOptions options, int* matchLengthPtr)
		{
			string text = new string(source);
			string target2 = new string(target);
			return this.IndexOfCore(text, target2, 0, text.Length, options, matchLengthPtr);
		}

		// Token: 0x0600548C RID: 21644 RVA: 0x0011A968 File Offset: 0x00118B68
		private int IndexOfOrdinalCore(ReadOnlySpan<char> source, ReadOnlySpan<char> value, bool ignoreCase)
		{
			string text = new string(source);
			string value2 = new string(value);
			if (!ignoreCase)
			{
				return text.IndexOfUnchecked(value2, 0, text.Length);
			}
			return text.IndexOfUncheckedIgnoreCase(value2, 0, text.Length);
		}

		// Token: 0x0600548D RID: 21645 RVA: 0x0011A9A4 File Offset: 0x00118BA4
		private int CompareString(ReadOnlySpan<char> string1, string string2, CompareOptions options)
		{
			string text = new string(string1);
			return this.internal_compare_switch(text, 0, text.Length, string2, 0, string2.Length, options);
		}

		// Token: 0x0600548E RID: 21646 RVA: 0x0011A9D0 File Offset: 0x00118BD0
		private int CompareString(ReadOnlySpan<char> string1, ReadOnlySpan<char> string2, CompareOptions options)
		{
			string text = new string(string1);
			string text2 = new string(string2);
			return this.internal_compare_switch(text, 0, text.Length, new string(text2), 0, text2.Length, options);
		}

		// Token: 0x0600548F RID: 21647 RVA: 0x0011AA0C File Offset: 0x00118C0C
		private unsafe static bool IsSortable(char* text, int length)
		{
			return MSCompatUnicodeTable.IsSortable(new string(text, 0, length));
		}

		// Token: 0x06005490 RID: 21648 RVA: 0x0011AA1B File Offset: 0x00118C1B
		private SortKey CreateSortKey(string source, CompareOptions options)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			return this.CreateSortKeyCore(source, options);
		}

		// Token: 0x06005491 RID: 21649 RVA: 0x0011AA4C File Offset: 0x00118C4C
		private bool StartsWith(string source, string prefix, CompareOptions options)
		{
			if (CompareInfo.UseManagedCollation)
			{
				return this.GetCollator().IsPrefix(source, prefix, options);
			}
			return source.Length >= prefix.Length && this.Compare(source, 0, prefix.Length, prefix, 0, prefix.Length, options) == 0;
		}

		// Token: 0x06005492 RID: 21650 RVA: 0x0011AA99 File Offset: 0x00118C99
		private bool StartsWith(ReadOnlySpan<char> source, ReadOnlySpan<char> prefix, CompareOptions options)
		{
			return this.StartsWith(new string(source), new string(prefix), options);
		}

		// Token: 0x06005493 RID: 21651 RVA: 0x0011AAB0 File Offset: 0x00118CB0
		private bool EndsWith(string source, string suffix, CompareOptions options)
		{
			if (CompareInfo.UseManagedCollation)
			{
				return this.GetCollator().IsSuffix(source, suffix, options);
			}
			return source.Length >= suffix.Length && this.Compare(source, source.Length - suffix.Length, suffix.Length, suffix, 0, suffix.Length, options) == 0;
		}

		// Token: 0x06005494 RID: 21652 RVA: 0x0011AB09 File Offset: 0x00118D09
		private bool EndsWith(ReadOnlySpan<char> source, ReadOnlySpan<char> suffix, CompareOptions options)
		{
			return this.EndsWith(new string(source), new string(suffix), options);
		}

		// Token: 0x06005495 RID: 21653 RVA: 0x0011AB1E File Offset: 0x00118D1E
		internal int GetHashCodeOfStringCore(string source, CompareOptions options)
		{
			return this.GetSortKey(source, options).GetHashCode();
		}

		// Token: 0x06005496 RID: 21654 RVA: 0x000479F8 File Offset: 0x00045BF8
		private SortVersion GetSortVersion()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005497 RID: 21655 RVA: 0x0011AB2D File Offset: 0x00118D2D
		// Note: this type is marked as 'beforefieldinit'.
		static CompareInfo()
		{
		}

		// Token: 0x06005498 RID: 21656 RVA: 0x000173AD File Offset: 0x000155AD
		internal CompareInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040033BB RID: 13243
		private const CompareOptions ValidIndexMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);

		// Token: 0x040033BC RID: 13244
		private const CompareOptions ValidCompareMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort);

		// Token: 0x040033BD RID: 13245
		private const CompareOptions ValidHashCodeOfStringMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);

		// Token: 0x040033BE RID: 13246
		private const CompareOptions ValidSortkeyCtorMaskOffFlags = ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort);

		// Token: 0x040033BF RID: 13247
		internal static readonly CompareInfo Invariant = CultureInfo.InvariantCulture.CompareInfo;

		// Token: 0x040033C0 RID: 13248
		[OptionalField(VersionAdded = 2)]
		private string m_name;

		// Token: 0x040033C1 RID: 13249
		[NonSerialized]
		private string _sortName;

		// Token: 0x040033C2 RID: 13250
		[OptionalField(VersionAdded = 3)]
		private SortVersion m_SortVersion;

		// Token: 0x040033C3 RID: 13251
		private int culture;

		// Token: 0x040033C4 RID: 13252
		[NonSerialized]
		private ISimpleCollator collator;

		// Token: 0x040033C5 RID: 13253
		private static Dictionary<string, ISimpleCollator> collators;

		// Token: 0x040033C6 RID: 13254
		private static bool managedCollation;

		// Token: 0x040033C7 RID: 13255
		private static bool managedCollationChecked;
	}
}
