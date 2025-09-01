using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Xml
{
	/// <summary>A unique identifier optimized for Guids.</summary>
	// Token: 0x02000024 RID: 36
	public class UniqueId
	{
		/// <summary>Creates a new instance of this class with a new, unique Guid.</summary>
		// Token: 0x060000CD RID: 205 RVA: 0x0000407E File Offset: 0x0000227E
		public UniqueId() : this(Guid.NewGuid())
		{
		}

		/// <summary>Creates a new instance of this class using a <see cref="T:System.Guid" />.</summary>
		/// <param name="guid">A <see cref="T:System.Guid" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="guid" /> is <see langword="null" />.</exception>
		// Token: 0x060000CE RID: 206 RVA: 0x0000408B File Offset: 0x0000228B
		public UniqueId(Guid guid) : this(guid.ToByteArray())
		{
		}

		/// <summary>Creates a new instance of this class using a byte array that represents a <see cref="T:System.Guid" />.</summary>
		/// <param name="guid">A byte array that represents a <see cref="T:System.Guid" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="guid" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="guid" /> provides less than 16 valid bytes.</exception>
		// Token: 0x060000CF RID: 207 RVA: 0x0000409A File Offset: 0x0000229A
		public UniqueId(byte[] guid) : this(guid, 0)
		{
		}

		/// <summary>Creates a new instance of this class starting from an offset within a <see langword="byte" /> array that represents a <see cref="T:System.Guid" />.</summary>
		/// <param name="guid">A <see langword="byte" /> array that represents a <see cref="T:System.Guid" />.</param>
		/// <param name="offset">Offset position within the <see langword="byte" /> array that represents a <see cref="T:System.Guid" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="guid" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> less than zero or greater than the length of the array.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="guid" /> and <paramref name="offset" /> provide less than 16 valid bytes.</exception>
		// Token: 0x060000D0 RID: 208 RVA: 0x000040A4 File Offset: 0x000022A4
		[SecuritySafeCritical]
		public unsafe UniqueId(byte[] guid, int offset)
		{
			if (guid == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("guid"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > guid.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					guid.Length
				})));
			}
			if (16 > guid.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(System.Runtime.Serialization.SR.GetString("Array too small.  Length of available data must be at least {0}.", new object[]
				{
					16
				}), "guid"));
			}
			fixed (byte* ptr = &guid[offset])
			{
				byte* ptr2 = ptr;
				this.idLow = this.UnsafeGetInt64(ptr2);
				this.idHigh = this.UnsafeGetInt64(ptr2 + 8);
			}
		}

		/// <summary>Creates a new instance of this class using a string.</summary>
		/// <param name="value">A string used to generate the <see cref="T:System.Xml.UniqueId" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">Length of <paramref name="value" /> is zero.</exception>
		// Token: 0x060000D1 RID: 209 RVA: 0x00004178 File Offset: 0x00002378
		[SecuritySafeCritical]
		public unsafe UniqueId(string value)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			if (value.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("UniqueId cannot be zero length.")));
			}
			fixed (string text = value)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				this.UnsafeParse(ptr, value.Length);
			}
			this.s = value;
		}

		/// <summary>Creates a new instance of this class starting from an offset within a <see langword="char" /> using a specified number of entries.</summary>
		/// <param name="chars">A <see langword="char" /> array that represents a <see cref="T:System.Guid" />.</param>
		/// <param name="offset">Offset position within the <see langword="char" /> array that represents a <see cref="T:System.Guid" />.</param>
		/// <param name="count">Number of array entries to use, starting from <paramref name="offset" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> less than zero or greater than the length of the array.
		/// -or-
		/// <paramref name="count" /> less than zero or greater than the length of the array minus <paramref name="offset" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="count" /> equals zero.</exception>
		// Token: 0x060000D2 RID: 210 RVA: 0x000041DC File Offset: 0x000023DC
		[SecuritySafeCritical]
		public unsafe UniqueId(char[] chars, int offset, int count)
		{
			if (chars == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("chars"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > chars.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					chars.Length
				})));
			}
			if (count < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (count > chars.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("count", System.Runtime.Serialization.SR.GetString("The specified size exceeds the remaining buffer space ({0} bytes).", new object[]
				{
					chars.Length - offset
				})));
			}
			if (count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new FormatException(System.Runtime.Serialization.SR.GetString("UniqueId cannot be zero length.")));
			}
			fixed (char* ptr = &chars[offset])
			{
				char* chars2 = ptr;
				this.UnsafeParse(chars2, count);
			}
			if (!this.IsGuid)
			{
				this.s = new string(chars, offset, count);
			}
		}

		/// <summary>Gets the length of the string representation of the <see cref="T:System.Xml.UniqueId" />.</summary>
		/// <returns>The length of the string representation of the <see cref="T:System.Xml.UniqueId" />.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000042E8 File Offset: 0x000024E8
		public int CharArrayLength
		{
			[SecuritySafeCritical]
			get
			{
				if (this.s != null)
				{
					return this.s.Length;
				}
				return 45;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004300 File Offset: 0x00002500
		[SecurityCritical]
		private unsafe int UnsafeDecode(short* char2val, char ch1, char ch2)
		{
			if ((ch1 | ch2) >= '\u0080')
			{
				return 256;
			}
			return (int)(char2val[(IntPtr)ch1] | char2val[(IntPtr)('\u0080' + ch2)]);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004327 File Offset: 0x00002527
		[SecurityCritical]
		private unsafe void UnsafeEncode(char* val2char, byte b, char* pch)
		{
			*pch = val2char[b >> 4];
			pch[1] = val2char[b & 15];
		}

		/// <summary>Indicates whether the <see cref="T:System.Xml.UniqueId" /> is a <see cref="T:System.Guid" />.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Xml.UniqueId" /> is a <see cref="T:System.Guid" />; otherwise <see langword="false" />.</returns>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004342 File Offset: 0x00002542
		public bool IsGuid
		{
			get
			{
				return (this.idLow | this.idHigh) != 0L;
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004358 File Offset: 0x00002558
		[SecurityCritical]
		private unsafe void UnsafeParse(char* chars, int charCount)
		{
			if (charCount != 45 || *chars != 'u' || chars[1] != 'r' || chars[2] != 'n' || chars[3] != ':' || chars[4] != 'u' || chars[5] != 'u' || chars[6] != 'i' || chars[7] != 'd' || chars[8] != ':' || chars[17] != '-' || chars[22] != '-' || chars[27] != '-' || chars[32] != '-')
			{
				return;
			}
			byte* ptr = stackalloc byte[(UIntPtr)16];
			int num = 0;
			short[] array;
			short* ptr2;
			if ((array = UniqueId.char2val) == null || array.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &array[0];
			}
			short* ptr3 = ptr2;
			int num2 = this.UnsafeDecode(ptr3, chars[15], chars[16]);
			*ptr = (byte)num2;
			int num3 = num | num2;
			num2 = this.UnsafeDecode(ptr3, chars[13], chars[14]);
			ptr[1] = (byte)num2;
			int num4 = num3 | num2;
			num2 = this.UnsafeDecode(ptr3, chars[11], chars[12]);
			ptr[2] = (byte)num2;
			int num5 = num4 | num2;
			num2 = this.UnsafeDecode(ptr3, chars[9], chars[10]);
			ptr[3] = (byte)num2;
			int num6 = num5 | num2;
			num2 = this.UnsafeDecode(ptr3, chars[20], chars[21]);
			ptr[4] = (byte)num2;
			int num7 = num6 | num2;
			num2 = this.UnsafeDecode(ptr3, chars[18], chars[19]);
			ptr[5] = (byte)num2;
			int num8 = num7 | num2;
			num2 = this.UnsafeDecode(ptr3, chars[25], chars[26]);
			ptr[6] = (byte)num2;
			int num9 = num8 | num2;
			num2 = this.UnsafeDecode(ptr3, chars[23], chars[24]);
			ptr[7] = (byte)num2;
			int num10 = num9 | num2;
			num2 = this.UnsafeDecode(ptr3, chars[28], chars[29]);
			ptr[8] = (byte)num2;
			int num11 = num10 | num2;
			num2 = this.UnsafeDecode(ptr3, chars[30], chars[31]);
			ptr[9] = (byte)num2;
			int num12 = num11 | num2;
			num2 = this.UnsafeDecode(ptr3, chars[33], chars[34]);
			ptr[10] = (byte)num2;
			int num13 = num12 | num2;
			num2 = this.UnsafeDecode(ptr3, chars[35], chars[36]);
			ptr[11] = (byte)num2;
			int num14 = num13 | num2;
			num2 = this.UnsafeDecode(ptr3, chars[37], chars[38]);
			ptr[12] = (byte)num2;
			int num15 = num14 | num2;
			num2 = this.UnsafeDecode(ptr3, chars[39], chars[40]);
			ptr[13] = (byte)num2;
			int num16 = num15 | num2;
			num2 = this.UnsafeDecode(ptr3, chars[41], chars[42]);
			ptr[14] = (byte)num2;
			int num17 = num16 | num2;
			num2 = this.UnsafeDecode(ptr3, chars[43], chars[44]);
			ptr[15] = (byte)num2;
			if ((num17 | num2) >= 256)
			{
				return;
			}
			this.idLow = this.UnsafeGetInt64(ptr);
			this.idHigh = this.UnsafeGetInt64(ptr + 8);
			array = null;
		}

		/// <summary>Puts the <see cref="T:System.Xml.UniqueId" /> value into a <see langword="char" /> array.</summary>
		/// <param name="chars">The <see langword="char" /> array.</param>
		/// <param name="offset">Position in the <see langword="char" /> array to start inserting the <see cref="T:System.Xml.UniqueId" /> value.</param>
		/// <returns>Number of entries in the <see langword="char" /> array filled by the <see cref="T:System.Xml.UniqueId" /> value.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="chars" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> less than zero or greater than the length of the array.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="guid" /> and <paramref name="offset" /> provide less than 16 valid bytes.</exception>
		// Token: 0x060000D8 RID: 216 RVA: 0x00004660 File Offset: 0x00002860
		[SecuritySafeCritical]
		public unsafe int ToCharArray(char[] chars, int offset)
		{
			int charArrayLength = this.CharArrayLength;
			if (chars == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("chars"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > chars.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					chars.Length
				})));
			}
			if (charArrayLength > chars.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("chars", System.Runtime.Serialization.SR.GetString("Array too small.  Must be able to hold at least {0}.", new object[]
				{
					charArrayLength
				})));
			}
			if (this.s != null)
			{
				this.s.CopyTo(0, chars, offset, charArrayLength);
			}
			else
			{
				byte* ptr = stackalloc byte[(UIntPtr)16];
				this.UnsafeSetInt64(this.idLow, ptr);
				this.UnsafeSetInt64(this.idHigh, ptr + 8);
				fixed (char* ptr2 = &chars[offset])
				{
					char* ptr3 = ptr2;
					*ptr3 = 'u';
					ptr3[1] = 'r';
					ptr3[2] = 'n';
					ptr3[3] = ':';
					ptr3[4] = 'u';
					ptr3[5] = 'u';
					ptr3[6] = 'i';
					ptr3[7] = 'd';
					ptr3[8] = ':';
					ptr3[17] = '-';
					ptr3[22] = '-';
					ptr3[27] = '-';
					ptr3[32] = '-';
					fixed (string text = "0123456789abcdef")
					{
						char* ptr4 = text;
						if (ptr4 != null)
						{
							ptr4 += RuntimeHelpers.OffsetToStringData / 2;
						}
						char* ptr5 = ptr4;
						this.UnsafeEncode(ptr5, *ptr, ptr3 + 15);
						this.UnsafeEncode(ptr5, ptr[1], ptr3 + 13);
						this.UnsafeEncode(ptr5, ptr[2], ptr3 + 11);
						this.UnsafeEncode(ptr5, ptr[3], ptr3 + 9);
						this.UnsafeEncode(ptr5, ptr[4], ptr3 + 20);
						this.UnsafeEncode(ptr5, ptr[5], ptr3 + 18);
						this.UnsafeEncode(ptr5, ptr[6], ptr3 + 25);
						this.UnsafeEncode(ptr5, ptr[7], ptr3 + 23);
						this.UnsafeEncode(ptr5, ptr[8], ptr3 + 28);
						this.UnsafeEncode(ptr5, ptr[9], ptr3 + 30);
						this.UnsafeEncode(ptr5, ptr[10], ptr3 + 33);
						this.UnsafeEncode(ptr5, ptr[11], ptr3 + 35);
						this.UnsafeEncode(ptr5, ptr[12], ptr3 + 37);
						this.UnsafeEncode(ptr5, ptr[13], ptr3 + 39);
						this.UnsafeEncode(ptr5, ptr[14], ptr3 + 41);
						this.UnsafeEncode(ptr5, ptr[15], ptr3 + 43);
					}
				}
			}
			return charArrayLength;
		}

		/// <summary>Tries to get the value of the <see cref="T:System.Xml.UniqueId" /> as a <see cref="T:System.Guid" />.</summary>
		/// <param name="guid">The <see cref="T:System.Guid" /> if successful; otherwise <see cref="F:System.Guid.Empty" />.</param>
		/// <returns>
		///   <see langword="true" /> if the UniqueId represents a <see cref="T:System.Guid" />; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffer" /> and <paramref name="offset" /> provide less than 16 valid bytes.</exception>
		// Token: 0x060000D9 RID: 217 RVA: 0x0000492C File Offset: 0x00002B2C
		public bool TryGetGuid(out Guid guid)
		{
			byte[] array = new byte[16];
			if (!this.TryGetGuid(array, 0))
			{
				guid = Guid.Empty;
				return false;
			}
			guid = new Guid(array);
			return true;
		}

		/// <summary>Tries to get the value of the <see cref="T:System.Xml.UniqueId" /> as a <see cref="T:System.Guid" /> and store it in the given byte array at the specified offest.</summary>
		/// <param name="buffer">
		///   <see langword="byte" /> array that will contain the <see cref="T:System.Guid" />.</param>
		/// <param name="offset">Position in the <see langword="byte" /> array to start inserting the <see cref="T:System.Guid" /> value.</param>
		/// <returns>
		///   <see langword="true" /> if the value stored in this instance of <see cref="T:System.Xml.UniqueId" /> is a <see cref="T:System.Guid" />; otherwise <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> less than zero or greater than the length of the array.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="buffer" /> and <paramref name="offset" /> provide less than 16 valid bytes.</exception>
		// Token: 0x060000DA RID: 218 RVA: 0x00004968 File Offset: 0x00002B68
		[SecuritySafeCritical]
		public unsafe bool TryGetGuid(byte[] buffer, int offset)
		{
			if (!this.IsGuid)
			{
				return false;
			}
			if (buffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("buffer"));
			}
			if (offset < 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The value of this argument must be non-negative.")));
			}
			if (offset > buffer.Length)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("offset", System.Runtime.Serialization.SR.GetString("The specified offset exceeds the buffer size ({0} bytes).", new object[]
				{
					buffer.Length
				})));
			}
			if (16 > buffer.Length - offset)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("buffer", System.Runtime.Serialization.SR.GetString("Array too small.  Must be able to hold at least {0}.", new object[]
				{
					16
				})));
			}
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				this.UnsafeSetInt64(this.idLow, ptr2);
				this.UnsafeSetInt64(this.idHigh, ptr2 + 8);
			}
			return true;
		}

		/// <summary>Displays the <see cref="T:System.Xml.UniqueId" /> value in string format.</summary>
		/// <returns>A string representation of the <see cref="T:System.Xml.UniqueId" /> value.</returns>
		// Token: 0x060000DB RID: 219 RVA: 0x00004A40 File Offset: 0x00002C40
		[SecuritySafeCritical]
		public override string ToString()
		{
			if (this.s == null)
			{
				int charArrayLength = this.CharArrayLength;
				char[] array = new char[charArrayLength];
				this.ToCharArray(array, 0);
				this.s = new string(array, 0, charArrayLength);
			}
			return this.s;
		}

		/// <summary>Overrides the equality operator to test for equality of two <see cref="T:System.Xml.UniqueId" />s.</summary>
		/// <param name="id1">The first <see cref="T:System.Xml.UniqueId" />.</param>
		/// <param name="id2">The second <see cref="T:System.Xml.UniqueId" />.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Xml.UniqueId" />s are equal, or are both <see langword="null" />; <see langword="false" /> if they are not equal, or if only one of them is <see langword="null" />.</returns>
		// Token: 0x060000DC RID: 220 RVA: 0x00004A80 File Offset: 0x00002C80
		public static bool operator ==(UniqueId id1, UniqueId id2)
		{
			if (id1 == null && id2 == null)
			{
				return true;
			}
			if (id1 == null || id2 == null)
			{
				return false;
			}
			if (id1.IsGuid && id2.IsGuid)
			{
				return id1.idLow == id2.idLow && id1.idHigh == id2.idHigh;
			}
			return id1.ToString() == id2.ToString();
		}

		/// <summary>Overrides the equality operator to test for inequality of two <see cref="T:System.Xml.UniqueId" />s.</summary>
		/// <param name="id1">The first <see cref="T:System.Xml.UniqueId" />.</param>
		/// <param name="id2">The second <see cref="T:System.Xml.UniqueId" />.</param>
		/// <returns>
		///   <see langword="true" /> if the overridden equality operator returns <see langword="false" />; otherwise <see langword="false" />.</returns>
		// Token: 0x060000DD RID: 221 RVA: 0x00004ADD File Offset: 0x00002CDD
		public static bool operator !=(UniqueId id1, UniqueId id2)
		{
			return !(id1 == id2);
		}

		/// <summary>Tests whether an object equals this <see cref="T:System.Xml.UniqueId" />.</summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>
		///   <see langword="true" /> if the object equals this <see cref="T:System.Xml.UniqueId" />; otherwise <see langword="false" />.</returns>
		// Token: 0x060000DE RID: 222 RVA: 0x00004AE9 File Offset: 0x00002CE9
		public override bool Equals(object obj)
		{
			return this == obj as UniqueId;
		}

		/// <summary>Creates a hash-code representation of this <see cref="T:System.Xml.UniqueId" />.</summary>
		/// <returns>An integer hash-code representation of this <see cref="T:System.Xml.UniqueId" />.</returns>
		// Token: 0x060000DF RID: 223 RVA: 0x00004AF8 File Offset: 0x00002CF8
		public override int GetHashCode()
		{
			if (this.IsGuid)
			{
				long num = this.idLow ^ this.idHigh;
				return (int)(num >> 32) ^ (int)num;
			}
			return this.ToString().GetHashCode();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00004B30 File Offset: 0x00002D30
		[SecurityCritical]
		private unsafe long UnsafeGetInt64(byte* pb)
		{
			int num = this.UnsafeGetInt32(pb);
			return (long)this.UnsafeGetInt32(pb + 4) << 32 | (long)((ulong)num);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004B56 File Offset: 0x00002D56
		[SecurityCritical]
		private unsafe int UnsafeGetInt32(byte* pb)
		{
			return (((int)pb[3] << 8 | (int)pb[2]) << 8 | (int)pb[1]) << 8 | (int)(*pb);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004B6F File Offset: 0x00002D6F
		[SecurityCritical]
		private unsafe void UnsafeSetInt64(long value, byte* pb)
		{
			this.UnsafeSetInt32((int)value, pb);
			this.UnsafeSetInt32((int)(value >> 32), pb + 4);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004B89 File Offset: 0x00002D89
		[SecurityCritical]
		private unsafe void UnsafeSetInt32(int value, byte* pb)
		{
			*pb = (byte)value;
			value >>= 8;
			pb[1] = (byte)value;
			value >>= 8;
			pb[2] = (byte)value;
			value >>= 8;
			pb[3] = (byte)value;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004BB0 File Offset: 0x00002DB0
		// Note: this type is marked as 'beforefieldinit'.
		static UniqueId()
		{
		}

		// Token: 0x04000066 RID: 102
		private long idLow;

		// Token: 0x04000067 RID: 103
		private long idHigh;

		// Token: 0x04000068 RID: 104
		[SecurityCritical]
		private string s;

		// Token: 0x04000069 RID: 105
		private const int guidLength = 16;

		// Token: 0x0400006A RID: 106
		private const int uuidLength = 45;

		// Token: 0x0400006B RID: 107
		private static short[] char2val = new short[]
		{
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			0,
			16,
			32,
			48,
			64,
			80,
			96,
			112,
			128,
			144,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			160,
			176,
			192,
			208,
			224,
			240,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			10,
			11,
			12,
			13,
			14,
			15,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256,
			256
		};

		// Token: 0x0400006C RID: 108
		private const string val2char = "0123456789abcdef";
	}
}
