using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Converts a base data type to another base data type.</summary>
	// Token: 0x02000107 RID: 263
	public static class Convert
	{
		// Token: 0x06000836 RID: 2102 RVA: 0x000232AC File Offset: 0x000214AC
		private unsafe static bool TryDecodeFromUtf16(ReadOnlySpan<char> utf16, Span<byte> bytes, out int consumed, out int written)
		{
			ref char reference = ref MemoryMarshal.GetReference<char>(utf16);
			ref byte reference2 = ref MemoryMarshal.GetReference<byte>(bytes);
			int num = utf16.Length & -4;
			int length = bytes.Length;
			int i = 0;
			int num2 = 0;
			if (utf16.Length != 0)
			{
				ref sbyte ptr = ref Convert.s_decodingMap[0];
				int num3;
				if (length >= (num >> 2) * 3)
				{
					num3 = num - 4;
				}
				else
				{
					num3 = length / 3 * 4;
				}
				while (i < num3)
				{
					int num4 = Convert.Decode(Unsafe.Add<char>(ref reference, i), ref ptr);
					if (num4 < 0)
					{
						IL_201:
						consumed = i;
						written = num2;
						return false;
					}
					Convert.WriteThreeLowOrderBytes(Unsafe.Add<byte>(ref reference2, num2), num4);
					num2 += 3;
					i += 4;
				}
				if (num3 != num - 4 || i == num)
				{
					goto IL_201;
				}
				int num5 = (int)(*Unsafe.Add<char>(ref reference, num - 4));
				int num6 = (int)(*Unsafe.Add<char>(ref reference, num - 3));
				int num7 = (int)(*Unsafe.Add<char>(ref reference, num - 2));
				int num8 = (int)(*Unsafe.Add<char>(ref reference, num - 1));
				if (((long)(num5 | num6 | num7 | num8) & (long)((ulong)-256)) != 0L)
				{
					goto IL_201;
				}
				num5 = (int)(*Unsafe.Add<sbyte>(ref ptr, num5));
				num6 = (int)(*Unsafe.Add<sbyte>(ref ptr, num6));
				num5 <<= 18;
				num6 <<= 12;
				num5 |= num6;
				if (num8 != 61)
				{
					num7 = (int)(*Unsafe.Add<sbyte>(ref ptr, num7));
					num8 = (int)(*Unsafe.Add<sbyte>(ref ptr, num8));
					num7 <<= 6;
					num5 |= num8;
					num5 |= num7;
					if (num5 < 0 || num2 > length - 3)
					{
						goto IL_201;
					}
					Convert.WriteThreeLowOrderBytes(Unsafe.Add<byte>(ref reference2, num2), num5);
					num2 += 3;
				}
				else if (num7 != 61)
				{
					num7 = (int)(*Unsafe.Add<sbyte>(ref ptr, num7));
					num7 <<= 6;
					num5 |= num7;
					if (num5 < 0 || num2 > length - 2)
					{
						goto IL_201;
					}
					*Unsafe.Add<byte>(ref reference2, num2) = (byte)(num5 >> 16);
					*Unsafe.Add<byte>(ref reference2, num2 + 1) = (byte)(num5 >> 8);
					num2 += 2;
				}
				else
				{
					if (num5 < 0 || num2 > length - 1)
					{
						goto IL_201;
					}
					*Unsafe.Add<byte>(ref reference2, num2) = (byte)(num5 >> 16);
					num2++;
				}
				i += 4;
				if (num != utf16.Length)
				{
					goto IL_201;
				}
			}
			consumed = i;
			written = num2;
			return true;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x000234C4 File Offset: 0x000216C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static int Decode(ref char encodedChars, ref sbyte decodingMap)
		{
			int num = (int)encodedChars;
			int num2 = (int)(*Unsafe.Add<char>(ref encodedChars, 1));
			int num3 = (int)(*Unsafe.Add<char>(ref encodedChars, 2));
			int num4 = (int)(*Unsafe.Add<char>(ref encodedChars, 3));
			if (((long)(num | num2 | num3 | num4) & (long)((ulong)-256)) != 0L)
			{
				return -1;
			}
			num = (int)(*Unsafe.Add<sbyte>(ref decodingMap, num));
			num2 = (int)(*Unsafe.Add<sbyte>(ref decodingMap, num2));
			num3 = (int)(*Unsafe.Add<sbyte>(ref decodingMap, num3));
			num4 = (int)(*Unsafe.Add<sbyte>(ref decodingMap, num4));
			num <<= 18;
			num2 <<= 12;
			num3 <<= 6;
			num |= num4;
			num2 |= num3;
			return num | num2;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00023541 File Offset: 0x00021741
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void WriteThreeLowOrderBytes(ref byte destination, int value)
		{
			destination = (byte)(value >> 16);
			*Unsafe.Add<byte>(ref destination, 1) = (byte)(value >> 8);
			*Unsafe.Add<byte>(ref destination, 2) = (byte)value;
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for the specified object.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <returns>The <see cref="T:System.TypeCode" /> for <paramref name="value" />, or <see cref="F:System.TypeCode.Empty" /> if <paramref name="value" /> is <see langword="null" />.</returns>
		// Token: 0x06000839 RID: 2105 RVA: 0x00023560 File Offset: 0x00021760
		public static TypeCode GetTypeCode(object value)
		{
			if (value == null)
			{
				return TypeCode.Empty;
			}
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				return convertible.GetTypeCode();
			}
			return TypeCode.Object;
		}

		/// <summary>Returns an indication whether the specified object is of type <see cref="T:System.DBNull" />.</summary>
		/// <param name="value">An object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is of type <see cref="T:System.DBNull" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600083A RID: 2106 RVA: 0x00023584 File Offset: 0x00021784
		public static bool IsDBNull(object value)
		{
			if (value == System.DBNull.Value)
			{
				return true;
			}
			IConvertible convertible = value as IConvertible;
			return convertible != null && convertible.GetTypeCode() == TypeCode.DBNull;
		}

		/// <summary>Returns an object of the specified type whose value is equivalent to the specified object.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <param name="typeCode">The type of object to return.</param>
		/// <returns>An object whose underlying type is <paramref name="typeCode" /> and whose value is equivalent to <paramref name="value" />.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic), if <paramref name="value" /> is <see langword="null" /> and <paramref name="typeCode" /> is <see cref="F:System.TypeCode.Empty" />, <see cref="F:System.TypeCode.String" />, or <see cref="F:System.TypeCode.Object" />.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.  
		///  -or-  
		///  <paramref name="value" /> is <see langword="null" /> and <paramref name="typeCode" /> specifies a value type.  
		///  -or-  
		///  <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in a format recognized by the <paramref name="typeCode" /> type.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is out of the range of the <paramref name="typeCode" /> type.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeCode" /> is invalid.</exception>
		// Token: 0x0600083B RID: 2107 RVA: 0x000235B0 File Offset: 0x000217B0
		public static object ChangeType(object value, TypeCode typeCode)
		{
			return Convert.ChangeType(value, typeCode, CultureInfo.CurrentCulture);
		}

		/// <summary>Returns an object of the specified type whose value is equivalent to the specified object. A parameter supplies culture-specific formatting information.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <param name="typeCode">The type of object to return.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>An object whose underlying type is <paramref name="typeCode" /> and whose value is equivalent to <paramref name="value" />.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic), if <paramref name="value" /> is <see langword="null" /> and <paramref name="typeCode" /> is <see cref="F:System.TypeCode.Empty" />, <see cref="F:System.TypeCode.String" />, or <see cref="F:System.TypeCode.Object" />.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.  
		///  -or-  
		///  <paramref name="value" /> is <see langword="null" /> and <paramref name="typeCode" /> specifies a value type.  
		///  -or-  
		///  <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in a format for the <paramref name="typeCode" /> type recognized by <paramref name="provider" />.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is out of the range of the <paramref name="typeCode" /> type.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeCode" /> is invalid.</exception>
		// Token: 0x0600083C RID: 2108 RVA: 0x000235C0 File Offset: 0x000217C0
		public static object ChangeType(object value, TypeCode typeCode, IFormatProvider provider)
		{
			if (value == null && (typeCode == TypeCode.Empty || typeCode == TypeCode.String || typeCode == TypeCode.Object))
			{
				return null;
			}
			IConvertible convertible = value as IConvertible;
			if (convertible == null)
			{
				throw new InvalidCastException("Object must implement IConvertible.");
			}
			switch (typeCode)
			{
			case TypeCode.Empty:
				throw new InvalidCastException("Object cannot be cast to Empty.");
			case TypeCode.Object:
				return value;
			case TypeCode.DBNull:
				throw new InvalidCastException("Object cannot be cast to DBNull.");
			case TypeCode.Boolean:
				return convertible.ToBoolean(provider);
			case TypeCode.Char:
				return convertible.ToChar(provider);
			case TypeCode.SByte:
				return convertible.ToSByte(provider);
			case TypeCode.Byte:
				return convertible.ToByte(provider);
			case TypeCode.Int16:
				return convertible.ToInt16(provider);
			case TypeCode.UInt16:
				return convertible.ToUInt16(provider);
			case TypeCode.Int32:
				return convertible.ToInt32(provider);
			case TypeCode.UInt32:
				return convertible.ToUInt32(provider);
			case TypeCode.Int64:
				return convertible.ToInt64(provider);
			case TypeCode.UInt64:
				return convertible.ToUInt64(provider);
			case TypeCode.Single:
				return convertible.ToSingle(provider);
			case TypeCode.Double:
				return convertible.ToDouble(provider);
			case TypeCode.Decimal:
				return convertible.ToDecimal(provider);
			case TypeCode.DateTime:
				return convertible.ToDateTime(provider);
			case TypeCode.String:
				return convertible.ToString(provider);
			}
			throw new ArgumentException("Unknown TypeCode value.");
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0002372C File Offset: 0x0002192C
		internal static object DefaultToType(IConvertible value, Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			if (value.GetType() == targetType)
			{
				return value;
			}
			if (targetType == Convert.ConvertTypes[3])
			{
				return value.ToBoolean(provider);
			}
			if (targetType == Convert.ConvertTypes[4])
			{
				return value.ToChar(provider);
			}
			if (targetType == Convert.ConvertTypes[5])
			{
				return value.ToSByte(provider);
			}
			if (targetType == Convert.ConvertTypes[6])
			{
				return value.ToByte(provider);
			}
			if (targetType == Convert.ConvertTypes[7])
			{
				return value.ToInt16(provider);
			}
			if (targetType == Convert.ConvertTypes[8])
			{
				return value.ToUInt16(provider);
			}
			if (targetType == Convert.ConvertTypes[9])
			{
				return value.ToInt32(provider);
			}
			if (targetType == Convert.ConvertTypes[10])
			{
				return value.ToUInt32(provider);
			}
			if (targetType == Convert.ConvertTypes[11])
			{
				return value.ToInt64(provider);
			}
			if (targetType == Convert.ConvertTypes[12])
			{
				return value.ToUInt64(provider);
			}
			if (targetType == Convert.ConvertTypes[13])
			{
				return value.ToSingle(provider);
			}
			if (targetType == Convert.ConvertTypes[14])
			{
				return value.ToDouble(provider);
			}
			if (targetType == Convert.ConvertTypes[15])
			{
				return value.ToDecimal(provider);
			}
			if (targetType == Convert.ConvertTypes[16])
			{
				return value.ToDateTime(provider);
			}
			if (targetType == Convert.ConvertTypes[18])
			{
				return value.ToString(provider);
			}
			if (targetType == Convert.ConvertTypes[1])
			{
				return value;
			}
			if (targetType == Convert.EnumType)
			{
				return (Enum)value;
			}
			if (targetType == Convert.ConvertTypes[2])
			{
				throw new InvalidCastException("Object cannot be cast to DBNull.");
			}
			if (targetType == Convert.ConvertTypes[0])
			{
				throw new InvalidCastException("Object cannot be cast to Empty.");
			}
			throw new InvalidCastException(string.Format("Invalid cast from '{0}' to '{1}'.", value.GetType().FullName, targetType.FullName));
		}

		/// <summary>Returns an object of the specified type and whose value is equivalent to the specified object.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <param name="conversionType">The type of object to return.</param>
		/// <returns>An object whose type is <paramref name="conversionType" /> and whose value is equivalent to <paramref name="value" />.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic), if <paramref name="value" /> is <see langword="null" /> and <paramref name="conversionType" /> is not a value type.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.  
		///  -or-  
		///  <paramref name="value" /> is <see langword="null" /> and <paramref name="conversionType" /> is a value type.  
		///  -or-  
		///  <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in a format recognized by <paramref name="conversionType" />.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is out of the range of <paramref name="conversionType" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="conversionType" /> is <see langword="null" />.</exception>
		// Token: 0x0600083E RID: 2110 RVA: 0x0002391A File Offset: 0x00021B1A
		public static object ChangeType(object value, Type conversionType)
		{
			return Convert.ChangeType(value, conversionType, CultureInfo.CurrentCulture);
		}

		/// <summary>Returns an object of the specified type whose value is equivalent to the specified object. A parameter supplies culture-specific formatting information.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <param name="conversionType">The type of object to return.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>An object whose type is <paramref name="conversionType" /> and whose value is equivalent to <paramref name="value" />.  
		///  -or-  
		///  <paramref name="value" />, if the <see cref="T:System.Type" /> of <paramref name="value" /> and <paramref name="conversionType" /> are equal.  
		///  -or-  
		///  A null reference (<see langword="Nothing" /> in Visual Basic), if <paramref name="value" /> is <see langword="null" /> and <paramref name="conversionType" /> is not a value type.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.  
		///  -or-  
		///  <paramref name="value" /> is <see langword="null" /> and <paramref name="conversionType" /> is a value type.  
		///  -or-  
		///  <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in a format for <paramref name="conversionType" /> recognized by <paramref name="provider" />.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is out of the range of <paramref name="conversionType" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="conversionType" /> is <see langword="null" />.</exception>
		// Token: 0x0600083F RID: 2111 RVA: 0x00023928 File Offset: 0x00021B28
		public static object ChangeType(object value, Type conversionType, IFormatProvider provider)
		{
			if (conversionType == null)
			{
				throw new ArgumentNullException("conversionType");
			}
			if (value == null)
			{
				if (conversionType.IsValueType)
				{
					throw new InvalidCastException("Null object cannot be converted to a value type.");
				}
				return null;
			}
			else
			{
				IConvertible convertible = value as IConvertible;
				if (convertible == null)
				{
					if (value.GetType() == conversionType)
					{
						return value;
					}
					throw new InvalidCastException("Object must implement IConvertible.");
				}
				else
				{
					if (conversionType == Convert.ConvertTypes[3])
					{
						return convertible.ToBoolean(provider);
					}
					if (conversionType == Convert.ConvertTypes[4])
					{
						return convertible.ToChar(provider);
					}
					if (conversionType == Convert.ConvertTypes[5])
					{
						return convertible.ToSByte(provider);
					}
					if (conversionType == Convert.ConvertTypes[6])
					{
						return convertible.ToByte(provider);
					}
					if (conversionType == Convert.ConvertTypes[7])
					{
						return convertible.ToInt16(provider);
					}
					if (conversionType == Convert.ConvertTypes[8])
					{
						return convertible.ToUInt16(provider);
					}
					if (conversionType == Convert.ConvertTypes[9])
					{
						return convertible.ToInt32(provider);
					}
					if (conversionType == Convert.ConvertTypes[10])
					{
						return convertible.ToUInt32(provider);
					}
					if (conversionType == Convert.ConvertTypes[11])
					{
						return convertible.ToInt64(provider);
					}
					if (conversionType == Convert.ConvertTypes[12])
					{
						return convertible.ToUInt64(provider);
					}
					if (conversionType == Convert.ConvertTypes[13])
					{
						return convertible.ToSingle(provider);
					}
					if (conversionType == Convert.ConvertTypes[14])
					{
						return convertible.ToDouble(provider);
					}
					if (conversionType == Convert.ConvertTypes[15])
					{
						return convertible.ToDecimal(provider);
					}
					if (conversionType == Convert.ConvertTypes[16])
					{
						return convertible.ToDateTime(provider);
					}
					if (conversionType == Convert.ConvertTypes[18])
					{
						return convertible.ToString(provider);
					}
					if (conversionType == Convert.ConvertTypes[1])
					{
						return value;
					}
					return convertible.ToType(conversionType, provider);
				}
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00023AF1 File Offset: 0x00021CF1
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowCharOverflowException()
		{
			throw new OverflowException("Value was either too large or too small for a character.");
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00023AFD File Offset: 0x00021CFD
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowByteOverflowException()
		{
			throw new OverflowException("Value was either too large or too small for an unsigned byte.");
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00023B09 File Offset: 0x00021D09
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowSByteOverflowException()
		{
			throw new OverflowException("Value was either too large or too small for a signed byte.");
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00023B15 File Offset: 0x00021D15
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowInt16OverflowException()
		{
			throw new OverflowException("Value was either too large or too small for an Int16.");
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00023B21 File Offset: 0x00021D21
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowUInt16OverflowException()
		{
			throw new OverflowException("Value was either too large or too small for a UInt16.");
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00023B2D File Offset: 0x00021D2D
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowInt32OverflowException()
		{
			throw new OverflowException("Value was either too large or too small for an Int32.");
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00023B39 File Offset: 0x00021D39
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowUInt32OverflowException()
		{
			throw new OverflowException("Value was either too large or too small for a UInt32.");
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00023B45 File Offset: 0x00021D45
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowInt64OverflowException()
		{
			throw new OverflowException("Value was either too large or too small for an Int64.");
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00023B51 File Offset: 0x00021D51
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowUInt64OverflowException()
		{
			throw new OverflowException("Value was either too large or too small for a UInt64.");
		}

		/// <summary>Converts the value of a specified object to an equivalent Boolean value.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> or <see langword="false" />, which reflects the value returned by invoking the <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" /> method for the underlying type of <paramref name="value" />. If <paramref name="value" /> is <see langword="null" />, the method returns <see langword="false" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is a string that does not equal <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion of <paramref name="value" /> to a <see cref="T:System.Boolean" /> is not supported.</exception>
		// Token: 0x06000849 RID: 2121 RVA: 0x00023B5D File Offset: 0x00021D5D
		public static bool ToBoolean(object value)
		{
			return value != null && ((IConvertible)value).ToBoolean(null);
		}

		/// <summary>Converts the value of the specified object to an equivalent Boolean value, using the specified culture-specific formatting information.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or <see langword="null" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>
		///   <see langword="true" /> or <see langword="false" />, which reflects the value returned by invoking the <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" /> method for the underlying type of <paramref name="value" />. If <paramref name="value" /> is <see langword="null" />, the method returns <see langword="false" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is a string that does not equal <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion of <paramref name="value" /> to a <see cref="T:System.Boolean" /> is not supported.</exception>
		// Token: 0x0600084A RID: 2122 RVA: 0x00023B70 File Offset: 0x00021D70
		public static bool ToBoolean(object value, IFormatProvider provider)
		{
			return value != null && ((IConvertible)value).ToBoolean(provider);
		}

		/// <summary>Returns the specified Boolean value; no actual conversion is performed.</summary>
		/// <param name="value">The Boolean value to return.</param>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		// Token: 0x0600084B RID: 2123 RVA: 0x0000270D File Offset: 0x0000090D
		public static bool ToBoolean(bool value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to an equivalent Boolean value.</summary>
		/// <param name="value">The 8-bit signed integer to convert.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600084C RID: 2124 RVA: 0x00023B83 File Offset: 0x00021D83
		[CLSCompliant(false)]
		public static bool ToBoolean(sbyte value)
		{
			return value != 0;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The Unicode character to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x0600084D RID: 2125 RVA: 0x00023B89 File Offset: 0x00021D89
		public static bool ToBoolean(char value)
		{
			return ((IConvertible)value).ToBoolean(null);
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to an equivalent Boolean value.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600084E RID: 2126 RVA: 0x00023B83 File Offset: 0x00021D83
		public static bool ToBoolean(byte value)
		{
			return value > 0;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent Boolean value.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600084F RID: 2127 RVA: 0x00023B83 File Offset: 0x00021D83
		public static bool ToBoolean(short value)
		{
			return value != 0;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to an equivalent Boolean value.</summary>
		/// <param name="value">The 16-bit unsigned integer to convert.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000850 RID: 2128 RVA: 0x00023B83 File Offset: 0x00021D83
		[CLSCompliant(false)]
		public static bool ToBoolean(ushort value)
		{
			return value > 0;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent Boolean value.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000851 RID: 2129 RVA: 0x00023B83 File Offset: 0x00021D83
		public static bool ToBoolean(int value)
		{
			return value != 0;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent Boolean value.</summary>
		/// <param name="value">The 32-bit unsigned integer to convert.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000852 RID: 2130 RVA: 0x00023B83 File Offset: 0x00021D83
		[CLSCompliant(false)]
		public static bool ToBoolean(uint value)
		{
			return value > 0U;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent Boolean value.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000853 RID: 2131 RVA: 0x00023B97 File Offset: 0x00021D97
		public static bool ToBoolean(long value)
		{
			return value != 0L;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent Boolean value.</summary>
		/// <param name="value">The 64-bit unsigned integer to convert.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000854 RID: 2132 RVA: 0x00023B97 File Offset: 0x00021D97
		[CLSCompliant(false)]
		public static bool ToBoolean(ulong value)
		{
			return value > 0UL;
		}

		/// <summary>Converts the specified string representation of a logical value to its Boolean equivalent.</summary>
		/// <param name="value">A string that contains the value of either <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> equals <see cref="F:System.Boolean.TrueString" />, or <see langword="false" /> if <paramref name="value" /> equals <see cref="F:System.Boolean.FalseString" /> or <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not equal to <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</exception>
		// Token: 0x06000855 RID: 2133 RVA: 0x00023B9E File Offset: 0x00021D9E
		public static bool ToBoolean(string value)
		{
			return value != null && bool.Parse(value);
		}

		/// <summary>Converts the specified string representation of a logical value to its Boolean equivalent, using the specified culture-specific formatting information.</summary>
		/// <param name="value">A string that contains the value of either <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information. This parameter is ignored.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> equals <see cref="F:System.Boolean.TrueString" />, or <see langword="false" /> if <paramref name="value" /> equals <see cref="F:System.Boolean.FalseString" /> or <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not equal to <see cref="F:System.Boolean.TrueString" /> or <see cref="F:System.Boolean.FalseString" />.</exception>
		// Token: 0x06000856 RID: 2134 RVA: 0x00023B9E File Offset: 0x00021D9E
		public static bool ToBoolean(string value, IFormatProvider provider)
		{
			return value != null && bool.Parse(value);
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent Boolean value.</summary>
		/// <param name="value">The single-precision floating-point number to convert.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000857 RID: 2135 RVA: 0x00023BAB File Offset: 0x00021DAB
		public static bool ToBoolean(float value)
		{
			return value != 0f;
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent Boolean value.</summary>
		/// <param name="value">The double-precision floating-point number to convert.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000858 RID: 2136 RVA: 0x00023BB8 File Offset: 0x00021DB8
		public static bool ToBoolean(double value)
		{
			return value != 0.0;
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent Boolean value.</summary>
		/// <param name="value">The number to convert.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000859 RID: 2137 RVA: 0x00023BC9 File Offset: 0x00021DC9
		public static bool ToBoolean(decimal value)
		{
			return value != 0m;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The date and time value to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x0600085A RID: 2138 RVA: 0x00023BD6 File Offset: 0x00021DD6
		public static bool ToBoolean(DateTime value)
		{
			return ((IConvertible)value).ToBoolean(null);
		}

		/// <summary>Converts the value of the specified object to a Unicode character.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <returns>A Unicode character that is equivalent to value, or <see cref="F:System.Char.MinValue" /> if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is a null string.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion of <paramref name="value" /> to a <see cref="T:System.Char" /> is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" /> or greater than <see cref="F:System.Char.MaxValue" />.</exception>
		// Token: 0x0600085B RID: 2139 RVA: 0x00023BE4 File Offset: 0x00021DE4
		public static char ToChar(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToChar(null);
			}
			return '\0';
		}

		/// <summary>Converts the value of the specified object to its equivalent Unicode character, using the specified culture-specific formatting information.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />, or <see cref="F:System.Char.MinValue" /> if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is a null string.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion of <paramref name="value" /> to a <see cref="T:System.Char" /> is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" /> or greater than <see cref="F:System.Char.MaxValue" />.</exception>
		// Token: 0x0600085C RID: 2140 RVA: 0x00023BF7 File Offset: 0x00021DF7
		public static char ToChar(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToChar(provider);
			}
			return '\0';
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The Boolean value to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x0600085D RID: 2141 RVA: 0x00023C0A File Offset: 0x00021E0A
		public static char ToChar(bool value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		/// <summary>Returns the specified Unicode character value; no actual conversion is performed.</summary>
		/// <param name="value">The Unicode character to return.</param>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		// Token: 0x0600085E RID: 2142 RVA: 0x0000270D File Offset: 0x0000090D
		public static char ToChar(char value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to its equivalent Unicode character.</summary>
		/// <param name="value">The 8-bit signed integer to convert.</param>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" />.</exception>
		// Token: 0x0600085F RID: 2143 RVA: 0x00023C18 File Offset: 0x00021E18
		[CLSCompliant(false)]
		public static char ToChar(sbyte value)
		{
			if (value < 0)
			{
				Convert.ThrowCharOverflowException();
			}
			return (char)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to its equivalent Unicode character.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000860 RID: 2144 RVA: 0x0000270D File Offset: 0x0000090D
		public static char ToChar(byte value)
		{
			return (char)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to its equivalent Unicode character.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" />.</exception>
		// Token: 0x06000861 RID: 2145 RVA: 0x00023C18 File Offset: 0x00021E18
		public static char ToChar(short value)
		{
			if (value < 0)
			{
				Convert.ThrowCharOverflowException();
			}
			return (char)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to its equivalent Unicode character.</summary>
		/// <param name="value">The 16-bit unsigned integer to convert.</param>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000862 RID: 2146 RVA: 0x0000270D File Offset: 0x0000090D
		[CLSCompliant(false)]
		public static char ToChar(ushort value)
		{
			return (char)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to its equivalent Unicode character.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" /> or greater than <see cref="F:System.Char.MaxValue" />.</exception>
		// Token: 0x06000863 RID: 2147 RVA: 0x00023C25 File Offset: 0x00021E25
		public static char ToChar(int value)
		{
			if (value < 0 || value > 65535)
			{
				Convert.ThrowCharOverflowException();
			}
			return (char)value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to its equivalent Unicode character.</summary>
		/// <param name="value">The 32-bit unsigned integer to convert.</param>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Char.MaxValue" />.</exception>
		// Token: 0x06000864 RID: 2148 RVA: 0x00023C3A File Offset: 0x00021E3A
		[CLSCompliant(false)]
		public static char ToChar(uint value)
		{
			if (value > 65535U)
			{
				Convert.ThrowCharOverflowException();
			}
			return (char)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to its equivalent Unicode character.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" /> or greater than <see cref="F:System.Char.MaxValue" />.</exception>
		// Token: 0x06000865 RID: 2149 RVA: 0x00023C4B File Offset: 0x00021E4B
		public static char ToChar(long value)
		{
			if (value < 0L || value > 65535L)
			{
				Convert.ThrowCharOverflowException();
			}
			return (char)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to its equivalent Unicode character.</summary>
		/// <param name="value">The 64-bit unsigned integer to convert.</param>
		/// <returns>A Unicode character that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Char.MaxValue" />.</exception>
		// Token: 0x06000866 RID: 2150 RVA: 0x00023C62 File Offset: 0x00021E62
		[CLSCompliant(false)]
		public static char ToChar(ulong value)
		{
			if (value > 65535UL)
			{
				Convert.ThrowCharOverflowException();
			}
			return (char)value;
		}

		/// <summary>Converts the first character of a specified string to a Unicode character.</summary>
		/// <param name="value">A string of length 1.</param>
		/// <returns>A Unicode character that is equivalent to the first and only character in <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The length of <paramref name="value" /> is not 1.</exception>
		// Token: 0x06000867 RID: 2151 RVA: 0x00023C74 File Offset: 0x00021E74
		public static char ToChar(string value)
		{
			return Convert.ToChar(value, null);
		}

		/// <summary>Converts the first character of a specified string to a Unicode character, using specified culture-specific formatting information.</summary>
		/// <param name="value">A string of length 1 or <see langword="null" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information. This parameter is ignored.</param>
		/// <returns>A Unicode character that is equivalent to the first and only character in <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The length of <paramref name="value" /> is not 1.</exception>
		// Token: 0x06000868 RID: 2152 RVA: 0x00023C7D File Offset: 0x00021E7D
		public static char ToChar(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length != 1)
			{
				throw new FormatException("String must be exactly one character long.");
			}
			return value[0];
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The single-precision floating-point number to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000869 RID: 2153 RVA: 0x00023CA8 File Offset: 0x00021EA8
		public static char ToChar(float value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The double-precision floating-point number to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x0600086A RID: 2154 RVA: 0x00023CB6 File Offset: 0x00021EB6
		public static char ToChar(double value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x0600086B RID: 2155 RVA: 0x00023CC4 File Offset: 0x00021EC4
		public static char ToChar(decimal value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The date and time value to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x0600086C RID: 2156 RVA: 0x00023CD2 File Offset: 0x00021ED2
		public static char ToChar(DateTime value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		/// <summary>Converts the value of the specified object to an 8-bit signed integer.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or <see langword="null" />.</param>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		// Token: 0x0600086D RID: 2157 RVA: 0x00023CE0 File Offset: 0x00021EE0
		[CLSCompliant(false)]
		public static sbyte ToSByte(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSByte(null);
			}
			return 0;
		}

		/// <summary>Converts the value of the specified object to an 8-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		// Token: 0x0600086E RID: 2158 RVA: 0x00023CF3 File Offset: 0x00021EF3
		[CLSCompliant(false)]
		public static sbyte ToSByte(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSByte(provider);
			}
			return 0;
		}

		/// <summary>Converts the specified Boolean value to the equivalent 8-bit signed integer.</summary>
		/// <param name="value">The Boolean value to convert.</param>
		/// <returns>The number 1 if <paramref name="value" /> is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x0600086F RID: 2159 RVA: 0x00023D06 File Offset: 0x00021F06
		[CLSCompliant(false)]
		public static sbyte ToSByte(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		/// <summary>Returns the specified 8-bit signed integer; no actual conversion is performed.</summary>
		/// <param name="value">The 8-bit signed integer to return.</param>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		// Token: 0x06000870 RID: 2160 RVA: 0x0000270D File Offset: 0x0000090D
		[CLSCompliant(false)]
		public static sbyte ToSByte(sbyte value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 8-bit signed integer.</summary>
		/// <param name="value">The Unicode character to convert.</param>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		// Token: 0x06000871 RID: 2161 RVA: 0x00023D0E File Offset: 0x00021F0E
		[CLSCompliant(false)]
		public static sbyte ToSByte(char value)
		{
			if (value > '\u007f')
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 8-bit signed integer.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		// Token: 0x06000872 RID: 2162 RVA: 0x00023D0E File Offset: 0x00021F0E
		[CLSCompliant(false)]
		public static sbyte ToSByte(byte value)
		{
			if (value > 127)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to the equivalent 8-bit signed integer.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />.</exception>
		// Token: 0x06000873 RID: 2163 RVA: 0x00023D1C File Offset: 0x00021F1C
		[CLSCompliant(false)]
		public static sbyte ToSByte(short value)
		{
			if (value < -128 || value > 127)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 8-bit signed integer.</summary>
		/// <param name="value">The 16-bit unsigned integer to convert.</param>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		// Token: 0x06000874 RID: 2164 RVA: 0x00023D0E File Offset: 0x00021F0E
		[CLSCompliant(false)]
		public static sbyte ToSByte(ushort value)
		{
			if (value > 127)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 8-bit signed integer.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />.</exception>
		// Token: 0x06000875 RID: 2165 RVA: 0x00023D1C File Offset: 0x00021F1C
		[CLSCompliant(false)]
		public static sbyte ToSByte(int value)
		{
			if (value < -128 || value > 127)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 8-bit signed integer.</summary>
		/// <param name="value">The 32-bit unsigned integer to convert.</param>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />.</exception>
		// Token: 0x06000876 RID: 2166 RVA: 0x00023D2F File Offset: 0x00021F2F
		[CLSCompliant(false)]
		public static sbyte ToSByte(uint value)
		{
			if ((ulong)value > 127UL)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 8-bit signed integer.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />.</exception>
		// Token: 0x06000877 RID: 2167 RVA: 0x00023D3F File Offset: 0x00021F3F
		[CLSCompliant(false)]
		public static sbyte ToSByte(long value)
		{
			if (value < -128L || value > 127L)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 8-bit signed integer.</summary>
		/// <param name="value">The 64-bit unsigned integer to convert.</param>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />.</exception>
		// Token: 0x06000878 RID: 2168 RVA: 0x00023D54 File Offset: 0x00021F54
		[CLSCompliant(false)]
		public static sbyte ToSByte(ulong value)
		{
			if (value > 127UL)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 8-bit signed integer.</summary>
		/// <param name="value">The single-precision floating-point number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />.</exception>
		// Token: 0x06000879 RID: 2169 RVA: 0x00023D63 File Offset: 0x00021F63
		[CLSCompliant(false)]
		public static sbyte ToSByte(float value)
		{
			return Convert.ToSByte((double)value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 8-bit signed integer.</summary>
		/// <param name="value">The double-precision floating-point number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />.</exception>
		// Token: 0x0600087A RID: 2170 RVA: 0x00023D6C File Offset: 0x00021F6C
		[CLSCompliant(false)]
		public static sbyte ToSByte(double value)
		{
			return Convert.ToSByte(Convert.ToInt32(value));
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 8-bit signed integer.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.SByte.MaxValue" /> or less than <see cref="F:System.SByte.MinValue" />.</exception>
		// Token: 0x0600087B RID: 2171 RVA: 0x00023D79 File Offset: 0x00021F79
		[CLSCompliant(false)]
		public static sbyte ToSByte(decimal value)
		{
			return decimal.ToSByte(decimal.Round(value, 0));
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 8-bit signed integer.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <returns>An 8-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if value is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9).</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		// Token: 0x0600087C RID: 2172 RVA: 0x00023D87 File Offset: 0x00021F87
		[CLSCompliant(false)]
		public static sbyte ToSByte(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return sbyte.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 8-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9).</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		// Token: 0x0600087D RID: 2173 RVA: 0x00023D99 File Offset: 0x00021F99
		[CLSCompliant(false)]
		public static sbyte ToSByte(string value, IFormatProvider provider)
		{
			return sbyte.Parse(value, NumberStyles.Integer, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The date and time value to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x0600087E RID: 2174 RVA: 0x00023DA3 File Offset: 0x00021FA3
		[CLSCompliant(false)]
		public static sbyte ToSByte(DateTime value)
		{
			return ((IConvertible)value).ToSByte(null);
		}

		/// <summary>Converts the value of the specified object to an 8-bit unsigned integer.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or <see langword="null" />.</param>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in the property format for a <see cref="T:System.Byte" /> value.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement <see cref="T:System.IConvertible" />.  
		/// -or-  
		/// Conversion from <paramref name="value" /> to the <see cref="T:System.Byte" /> type is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x0600087F RID: 2175 RVA: 0x00023DB1 File Offset: 0x00021FB1
		public static byte ToByte(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToByte(null);
			}
			return 0;
		}

		/// <summary>Converts the value of the specified object to an 8-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in the property format for a <see cref="T:System.Byte" /> value.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement <see cref="T:System.IConvertible" />.  
		/// -or-  
		/// Conversion from <paramref name="value" /> to the <see cref="T:System.Byte" /> type is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x06000880 RID: 2176 RVA: 0x00023DC4 File Offset: 0x00021FC4
		public static byte ToByte(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToByte(provider);
			}
			return 0;
		}

		/// <summary>Converts the specified Boolean value to the equivalent 8-bit unsigned integer.</summary>
		/// <param name="value">The Boolean value to convert.</param>
		/// <returns>The number 1 if <paramref name="value" /> is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x06000881 RID: 2177 RVA: 0x00023D06 File Offset: 0x00021F06
		public static byte ToByte(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		/// <summary>Returns the specified 8-bit unsigned integer; no actual conversion is performed.</summary>
		/// <param name="value">The 8-bit unsigned integer to return.</param>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		// Token: 0x06000882 RID: 2178 RVA: 0x0000270D File Offset: 0x0000090D
		public static byte ToByte(byte value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 8-bit unsigned integer.</summary>
		/// <param name="value">The Unicode character to convert.</param>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x06000883 RID: 2179 RVA: 0x00023DD7 File Offset: 0x00021FD7
		public static byte ToByte(char value)
		{
			if (value > 'ÿ')
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to an equivalent 8-bit unsigned integer.</summary>
		/// <param name="value">The 8-bit signed integer to be converted.</param>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" />.</exception>
		// Token: 0x06000884 RID: 2180 RVA: 0x00023DE8 File Offset: 0x00021FE8
		[CLSCompliant(false)]
		public static byte ToByte(sbyte value)
		{
			if (value < 0)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent 8-bit unsigned integer.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x06000885 RID: 2181 RVA: 0x00023DF5 File Offset: 0x00021FF5
		public static byte ToByte(short value)
		{
			if (value < 0 || value > 255)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to an equivalent 8-bit unsigned integer.</summary>
		/// <param name="value">The 16-bit unsigned integer to convert.</param>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x06000886 RID: 2182 RVA: 0x00023DD7 File Offset: 0x00021FD7
		[CLSCompliant(false)]
		public static byte ToByte(ushort value)
		{
			if (value > 255)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 8-bit unsigned integer.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x06000887 RID: 2183 RVA: 0x00023DF5 File Offset: 0x00021FF5
		public static byte ToByte(int value)
		{
			if (value < 0 || value > 255)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 8-bit unsigned integer.</summary>
		/// <param name="value">The 32-bit unsigned integer to convert.</param>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x06000888 RID: 2184 RVA: 0x00023E0A File Offset: 0x0002200A
		[CLSCompliant(false)]
		public static byte ToByte(uint value)
		{
			if (value > 255U)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 8-bit unsigned integer.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x06000889 RID: 2185 RVA: 0x00023E1B File Offset: 0x0002201B
		public static byte ToByte(long value)
		{
			if (value < 0L || value > 255L)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 8-bit unsigned integer.</summary>
		/// <param name="value">The 64-bit unsigned integer to convert.</param>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x0600088A RID: 2186 RVA: 0x00023E32 File Offset: 0x00022032
		[CLSCompliant(false)]
		public static byte ToByte(ulong value)
		{
			if (value > 255UL)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 8-bit unsigned integer.</summary>
		/// <param name="value">A single-precision floating-point number.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" /> or less than <see cref="F:System.Byte.MinValue" />.</exception>
		// Token: 0x0600088B RID: 2187 RVA: 0x00023E44 File Offset: 0x00022044
		public static byte ToByte(float value)
		{
			return Convert.ToByte((double)value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 8-bit unsigned integer.</summary>
		/// <param name="value">The double-precision floating-point number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" /> or less than <see cref="F:System.Byte.MinValue" />.</exception>
		// Token: 0x0600088C RID: 2188 RVA: 0x00023E4D File Offset: 0x0002204D
		public static byte ToByte(double value)
		{
			return Convert.ToByte(Convert.ToInt32(value));
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 8-bit unsigned integer.</summary>
		/// <param name="value">The number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 8-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Byte.MaxValue" /> or less than <see cref="F:System.Byte.MinValue" />.</exception>
		// Token: 0x0600088D RID: 2189 RVA: 0x00023E5A File Offset: 0x0002205A
		public static byte ToByte(decimal value)
		{
			return decimal.ToByte(decimal.Round(value, 0));
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 8-bit unsigned integer.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9).</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x0600088E RID: 2190 RVA: 0x00023E68 File Offset: 0x00022068
		public static byte ToByte(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return byte.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 8-bit unsigned integer, using specified culture-specific formatting information.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>An 8-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9).</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x0600088F RID: 2191 RVA: 0x00023E7A File Offset: 0x0002207A
		public static byte ToByte(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return byte.Parse(value, NumberStyles.Integer, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The date and time value to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000890 RID: 2192 RVA: 0x00023E89 File Offset: 0x00022089
		public static byte ToByte(DateTime value)
		{
			return ((IConvertible)value).ToByte(null);
		}

		/// <summary>Converts the value of the specified object to a 16-bit signed integer.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or <see langword="null" />.</param>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for an <see cref="T:System.Int16" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x06000891 RID: 2193 RVA: 0x00023E97 File Offset: 0x00022097
		public static short ToInt16(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt16(null);
			}
			return 0;
		}

		/// <summary>Converts the value of the specified object to a 16-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for an <see cref="T:System.Int16" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement <see cref="T:System.IConvertible" />.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x06000892 RID: 2194 RVA: 0x00023EAA File Offset: 0x000220AA
		public static short ToInt16(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt16(provider);
			}
			return 0;
		}

		/// <summary>Converts the specified Boolean value to the equivalent 16-bit signed integer.</summary>
		/// <param name="value">The Boolean value to convert.</param>
		/// <returns>The number 1 if <paramref name="value" /> is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x06000893 RID: 2195 RVA: 0x00023D06 File Offset: 0x00021F06
		public static short ToInt16(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 16-bit signed integer.</summary>
		/// <param name="value">The Unicode character to convert.</param>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x06000894 RID: 2196 RVA: 0x00023EBD File Offset: 0x000220BD
		public static short ToInt16(char value)
		{
			if (value > '翿')
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 16-bit signed integer.</summary>
		/// <param name="value">The 8-bit signed integer to convert.</param>
		/// <returns>A 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000895 RID: 2197 RVA: 0x0000270D File Offset: 0x0000090D
		[CLSCompliant(false)]
		public static short ToInt16(sbyte value)
		{
			return (short)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 16-bit signed integer.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000896 RID: 2198 RVA: 0x0000270D File Offset: 0x0000090D
		public static short ToInt16(byte value)
		{
			return (short)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 16-bit signed integer.</summary>
		/// <param name="value">The 16-bit unsigned integer to convert.</param>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x06000897 RID: 2199 RVA: 0x00023EBD File Offset: 0x000220BD
		[CLSCompliant(false)]
		public static short ToInt16(ushort value)
		{
			if (value > 32767)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 16-bit signed integer.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <returns>The 16-bit signed integer equivalent of <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />.</exception>
		// Token: 0x06000898 RID: 2200 RVA: 0x00023ECE File Offset: 0x000220CE
		public static short ToInt16(int value)
		{
			if (value < -32768 || value > 32767)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 16-bit signed integer.</summary>
		/// <param name="value">The 32-bit unsigned integer to convert.</param>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x06000899 RID: 2201 RVA: 0x00023EE7 File Offset: 0x000220E7
		[CLSCompliant(false)]
		public static short ToInt16(uint value)
		{
			if ((ulong)value > 32767UL)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		/// <summary>Returns the specified 16-bit signed integer; no actual conversion is performed.</summary>
		/// <param name="value">The 16-bit signed integer to return.</param>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		// Token: 0x0600089A RID: 2202 RVA: 0x0000270D File Offset: 0x0000090D
		public static short ToInt16(short value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 16-bit signed integer.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />.</exception>
		// Token: 0x0600089B RID: 2203 RVA: 0x00023EFA File Offset: 0x000220FA
		public static short ToInt16(long value)
		{
			if (value < -32768L || value > 32767L)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 16-bit signed integer.</summary>
		/// <param name="value">The 64-bit unsigned integer to convert.</param>
		/// <returns>A 16-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x0600089C RID: 2204 RVA: 0x00023F15 File Offset: 0x00022115
		[CLSCompliant(false)]
		public static short ToInt16(ulong value)
		{
			if (value > 32767UL)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 16-bit signed integer.</summary>
		/// <param name="value">The single-precision floating-point number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />.</exception>
		// Token: 0x0600089D RID: 2205 RVA: 0x00023F27 File Offset: 0x00022127
		public static short ToInt16(float value)
		{
			return Convert.ToInt16((double)value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 16-bit signed integer.</summary>
		/// <param name="value">The double-precision floating-point number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />.</exception>
		// Token: 0x0600089E RID: 2206 RVA: 0x00023F30 File Offset: 0x00022130
		public static short ToInt16(double value)
		{
			return Convert.ToInt16(Convert.ToInt32(value));
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 16-bit signed integer.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int16.MaxValue" /> or less than <see cref="F:System.Int16.MinValue" />.</exception>
		// Token: 0x0600089F RID: 2207 RVA: 0x00023F3D File Offset: 0x0002213D
		public static short ToInt16(decimal value)
		{
			return decimal.ToInt16(decimal.Round(value, 0));
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 16-bit signed integer.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <returns>A 16-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9).</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x060008A0 RID: 2208 RVA: 0x00023F4B File Offset: 0x0002214B
		public static short ToInt16(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return short.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 16-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A 16-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9).</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x060008A1 RID: 2209 RVA: 0x00023F5D File Offset: 0x0002215D
		public static short ToInt16(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return short.Parse(value, NumberStyles.Integer, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The date and time value to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x060008A2 RID: 2210 RVA: 0x00023F6C File Offset: 0x0002216C
		public static short ToInt16(DateTime value)
		{
			return ((IConvertible)value).ToInt16(null);
		}

		/// <summary>Converts the value of the specified object to a 16-bit unsigned integer.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or <see langword="null" />.</param>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the  <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x060008A3 RID: 2211 RVA: 0x00023F7A File Offset: 0x0002217A
		[CLSCompliant(false)]
		public static ushort ToUInt16(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt16(null);
			}
			return 0;
		}

		/// <summary>Converts the value of the specified object to a 16-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the  <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x060008A4 RID: 2212 RVA: 0x00023F8D File Offset: 0x0002218D
		[CLSCompliant(false)]
		public static ushort ToUInt16(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt16(provider);
			}
			return 0;
		}

		/// <summary>Converts the specified Boolean value to the equivalent 16-bit unsigned integer.</summary>
		/// <param name="value">The Boolean value to convert.</param>
		/// <returns>The number 1 if <paramref name="value" /> is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x060008A5 RID: 2213 RVA: 0x00023D06 File Offset: 0x00021F06
		[CLSCompliant(false)]
		public static ushort ToUInt16(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 16-bit unsigned integer.</summary>
		/// <param name="value">The Unicode character to convert.</param>
		/// <returns>The 16-bit unsigned integer equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008A6 RID: 2214 RVA: 0x0000270D File Offset: 0x0000090D
		[CLSCompliant(false)]
		public static ushort ToUInt16(char value)
		{
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 16-bit unsigned integer.</summary>
		/// <param name="value">The 8-bit signed integer to convert.</param>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero.</exception>
		// Token: 0x060008A7 RID: 2215 RVA: 0x00023FA0 File Offset: 0x000221A0
		[CLSCompliant(false)]
		public static ushort ToUInt16(sbyte value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 16-bit unsigned integer.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008A8 RID: 2216 RVA: 0x0000270D File Offset: 0x0000090D
		[CLSCompliant(false)]
		public static ushort ToUInt16(byte value)
		{
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to the equivalent 16-bit unsigned integer.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero.</exception>
		// Token: 0x060008A9 RID: 2217 RVA: 0x00023FA0 File Offset: 0x000221A0
		[CLSCompliant(false)]
		public static ushort ToUInt16(short value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 16-bit unsigned integer.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x060008AA RID: 2218 RVA: 0x00023FAD File Offset: 0x000221AD
		[CLSCompliant(false)]
		public static ushort ToUInt16(int value)
		{
			if (value < 0 || value > 65535)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)value;
		}

		/// <summary>Returns the specified 16-bit unsigned integer; no actual conversion is performed.</summary>
		/// <param name="value">The 16-bit unsigned integer to return.</param>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		// Token: 0x060008AB RID: 2219 RVA: 0x0000270D File Offset: 0x0000090D
		[CLSCompliant(false)]
		public static ushort ToUInt16(ushort value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 16-bit unsigned integer.</summary>
		/// <param name="value">The 32-bit unsigned integer to convert.</param>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x060008AC RID: 2220 RVA: 0x00023FC2 File Offset: 0x000221C2
		[CLSCompliant(false)]
		public static ushort ToUInt16(uint value)
		{
			if (value > 65535U)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 16-bit unsigned integer.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x060008AD RID: 2221 RVA: 0x00023FD3 File Offset: 0x000221D3
		[CLSCompliant(false)]
		public static ushort ToUInt16(long value)
		{
			if (value < 0L || value > 65535L)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 16-bit unsigned integer.</summary>
		/// <param name="value">The 64-bit unsigned integer to convert.</param>
		/// <returns>A 16-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x060008AE RID: 2222 RVA: 0x00023FEA File Offset: 0x000221EA
		[CLSCompliant(false)]
		public static ushort ToUInt16(ulong value)
		{
			if (value > 65535UL)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 16-bit unsigned integer.</summary>
		/// <param name="value">The single-precision floating-point number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x060008AF RID: 2223 RVA: 0x00023FFC File Offset: 0x000221FC
		[CLSCompliant(false)]
		public static ushort ToUInt16(float value)
		{
			return Convert.ToUInt16((double)value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 16-bit unsigned integer.</summary>
		/// <param name="value">The double-precision floating-point number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x060008B0 RID: 2224 RVA: 0x00024005 File Offset: 0x00022205
		[CLSCompliant(false)]
		public static ushort ToUInt16(double value)
		{
			return Convert.ToUInt16(Convert.ToInt32(value));
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 16-bit unsigned integer.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 16-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x060008B1 RID: 2225 RVA: 0x00024012 File Offset: 0x00022212
		[CLSCompliant(false)]
		public static ushort ToUInt16(decimal value)
		{
			return decimal.ToUInt16(decimal.Round(value, 0));
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 16-bit unsigned integer.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <returns>A 16-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9).</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x060008B2 RID: 2226 RVA: 0x00024020 File Offset: 0x00022220
		[CLSCompliant(false)]
		public static ushort ToUInt16(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return ushort.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 16-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A 16-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9).</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x060008B3 RID: 2227 RVA: 0x00024032 File Offset: 0x00022232
		[CLSCompliant(false)]
		public static ushort ToUInt16(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ushort.Parse(value, NumberStyles.Integer, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The date and time value to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x060008B4 RID: 2228 RVA: 0x00024041 File Offset: 0x00022241
		[CLSCompliant(false)]
		public static ushort ToUInt16(DateTime value)
		{
			return ((IConvertible)value).ToUInt16(null);
		}

		/// <summary>Converts the value of the specified object to a 32-bit signed integer.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or <see langword="null" />.</param>
		/// <returns>A 32-bit signed integer equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the  <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060008B5 RID: 2229 RVA: 0x0002404F File Offset: 0x0002224F
		public static int ToInt32(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt32(null);
			}
			return 0;
		}

		/// <summary>Converts the value of the specified object to a 32-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement <see cref="T:System.IConvertible" />.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060008B6 RID: 2230 RVA: 0x00024062 File Offset: 0x00022262
		public static int ToInt32(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt32(provider);
			}
			return 0;
		}

		/// <summary>Converts the specified Boolean value to the equivalent 32-bit signed integer.</summary>
		/// <param name="value">The Boolean value to convert.</param>
		/// <returns>The number 1 if <paramref name="value" /> is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x060008B7 RID: 2231 RVA: 0x00023D06 File Offset: 0x00021F06
		public static int ToInt32(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 32-bit signed integer.</summary>
		/// <param name="value">The Unicode character to convert.</param>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008B8 RID: 2232 RVA: 0x0000270D File Offset: 0x0000090D
		public static int ToInt32(char value)
		{
			return (int)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 32-bit signed integer.</summary>
		/// <param name="value">The 8-bit signed integer to convert.</param>
		/// <returns>A 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008B9 RID: 2233 RVA: 0x0000270D File Offset: 0x0000090D
		[CLSCompliant(false)]
		public static int ToInt32(sbyte value)
		{
			return (int)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 32-bit signed integer.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008BA RID: 2234 RVA: 0x0000270D File Offset: 0x0000090D
		public static int ToInt32(byte value)
		{
			return (int)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent 32-bit signed integer.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008BB RID: 2235 RVA: 0x0000270D File Offset: 0x0000090D
		public static int ToInt32(short value)
		{
			return (int)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 32-bit signed integer.</summary>
		/// <param name="value">The 16-bit unsigned integer to convert.</param>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008BC RID: 2236 RVA: 0x0000270D File Offset: 0x0000090D
		[CLSCompliant(false)]
		public static int ToInt32(ushort value)
		{
			return (int)value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 32-bit signed integer.</summary>
		/// <param name="value">The 32-bit unsigned integer to convert.</param>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060008BD RID: 2237 RVA: 0x00024075 File Offset: 0x00022275
		[CLSCompliant(false)]
		public static int ToInt32(uint value)
		{
			if (value > 2147483647U)
			{
				Convert.ThrowInt32OverflowException();
			}
			return (int)value;
		}

		/// <summary>Returns the specified 32-bit signed integer; no actual conversion is performed.</summary>
		/// <param name="value">The 32-bit signed integer to return.</param>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		// Token: 0x060008BE RID: 2238 RVA: 0x0000270D File Offset: 0x0000090D
		public static int ToInt32(int value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 32-bit signed integer.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" /> or less than <see cref="F:System.Int32.MinValue" />.</exception>
		// Token: 0x060008BF RID: 2239 RVA: 0x00024085 File Offset: 0x00022285
		public static int ToInt32(long value)
		{
			if (value < -2147483648L || value > 2147483647L)
			{
				Convert.ThrowInt32OverflowException();
			}
			return (int)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 32-bit signed integer.</summary>
		/// <param name="value">The 64-bit unsigned integer to convert.</param>
		/// <returns>A 32-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060008C0 RID: 2240 RVA: 0x000240A0 File Offset: 0x000222A0
		[CLSCompliant(false)]
		public static int ToInt32(ulong value)
		{
			if (value > 2147483647UL)
			{
				Convert.ThrowInt32OverflowException();
			}
			return (int)value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 32-bit signed integer.</summary>
		/// <param name="value">The single-precision floating-point number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" /> or less than <see cref="F:System.Int32.MinValue" />.</exception>
		// Token: 0x060008C1 RID: 2241 RVA: 0x000240B2 File Offset: 0x000222B2
		public static int ToInt32(float value)
		{
			return Convert.ToInt32((double)value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 32-bit signed integer.</summary>
		/// <param name="value">The double-precision floating-point number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" /> or less than <see cref="F:System.Int32.MinValue" />.</exception>
		// Token: 0x060008C2 RID: 2242 RVA: 0x000240BC File Offset: 0x000222BC
		public static int ToInt32(double value)
		{
			if (value >= 0.0)
			{
				if (value < 2147483647.5)
				{
					int num = (int)value;
					double num2 = value - (double)num;
					if (num2 > 0.5 || (num2 == 0.5 && (num & 1) != 0))
					{
						num++;
					}
					return num;
				}
			}
			else if (value >= -2147483648.5)
			{
				int num3 = (int)value;
				double num4 = value - (double)num3;
				if (num4 < -0.5 || (num4 == -0.5 && (num3 & 1) != 0))
				{
					num3--;
				}
				return num3;
			}
			throw new OverflowException("Value was either too large or too small for an Int32.");
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 32-bit signed integer.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int32.MaxValue" /> or less than <see cref="F:System.Int32.MinValue" />.</exception>
		// Token: 0x060008C3 RID: 2243 RVA: 0x0002414D File Offset: 0x0002234D
		public static int ToInt32(decimal value)
		{
			return decimal.ToInt32(decimal.Round(value, 0));
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 32-bit signed integer.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <returns>A 32-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9).</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060008C4 RID: 2244 RVA: 0x0002415B File Offset: 0x0002235B
		public static int ToInt32(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return int.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 32-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A 32-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9).</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060008C5 RID: 2245 RVA: 0x0002416D File Offset: 0x0002236D
		public static int ToInt32(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return int.Parse(value, NumberStyles.Integer, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The date and time value to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x060008C6 RID: 2246 RVA: 0x0002417C File Offset: 0x0002237C
		public static int ToInt32(DateTime value)
		{
			return ((IConvertible)value).ToInt32(null);
		}

		/// <summary>Converts the value of the specified object to a 32-bit unsigned integer.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or <see langword="null" />.</param>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x060008C7 RID: 2247 RVA: 0x0002418A File Offset: 0x0002238A
		[CLSCompliant(false)]
		public static uint ToUInt32(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt32(null);
			}
			return 0U;
		}

		/// <summary>Converts the value of the specified object to a 32-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x060008C8 RID: 2248 RVA: 0x0002419D File Offset: 0x0002239D
		[CLSCompliant(false)]
		public static uint ToUInt32(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt32(provider);
			}
			return 0U;
		}

		/// <summary>Converts the specified Boolean value to the equivalent 32-bit unsigned integer.</summary>
		/// <param name="value">The Boolean value to convert.</param>
		/// <returns>The number 1 if <paramref name="value" /> is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x060008C9 RID: 2249 RVA: 0x00023D06 File Offset: 0x00021F06
		[CLSCompliant(false)]
		public static uint ToUInt32(bool value)
		{
			if (!value)
			{
				return 0U;
			}
			return 1U;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 32-bit unsigned integer.</summary>
		/// <param name="value">The Unicode character to convert.</param>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008CA RID: 2250 RVA: 0x0000270D File Offset: 0x0000090D
		[CLSCompliant(false)]
		public static uint ToUInt32(char value)
		{
			return (uint)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 32-bit unsigned integer.</summary>
		/// <param name="value">The 8-bit signed integer to convert.</param>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero.</exception>
		// Token: 0x060008CB RID: 2251 RVA: 0x000241B0 File Offset: 0x000223B0
		[CLSCompliant(false)]
		public static uint ToUInt32(sbyte value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt32OverflowException();
			}
			return (uint)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 32-bit unsigned integer.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008CC RID: 2252 RVA: 0x0000270D File Offset: 0x0000090D
		[CLSCompliant(false)]
		public static uint ToUInt32(byte value)
		{
			return (uint)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to the equivalent 32-bit unsigned integer.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero.</exception>
		// Token: 0x060008CD RID: 2253 RVA: 0x000241B0 File Offset: 0x000223B0
		[CLSCompliant(false)]
		public static uint ToUInt32(short value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt32OverflowException();
			}
			return (uint)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 32-bit unsigned integer.</summary>
		/// <param name="value">The 16-bit unsigned integer to convert.</param>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008CE RID: 2254 RVA: 0x0000270D File Offset: 0x0000090D
		[CLSCompliant(false)]
		public static uint ToUInt32(ushort value)
		{
			return (uint)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 32-bit unsigned integer.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero.</exception>
		// Token: 0x060008CF RID: 2255 RVA: 0x000241B0 File Offset: 0x000223B0
		[CLSCompliant(false)]
		public static uint ToUInt32(int value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt32OverflowException();
			}
			return (uint)value;
		}

		/// <summary>Returns the specified 32-bit unsigned integer; no actual conversion is performed.</summary>
		/// <param name="value">The 32-bit unsigned integer to return.</param>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		// Token: 0x060008D0 RID: 2256 RVA: 0x0000270D File Offset: 0x0000090D
		[CLSCompliant(false)]
		public static uint ToUInt32(uint value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 32-bit unsigned integer.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x060008D1 RID: 2257 RVA: 0x000241BC File Offset: 0x000223BC
		[CLSCompliant(false)]
		public static uint ToUInt32(long value)
		{
			if (value < 0L || value > (long)((ulong)-1))
			{
				Convert.ThrowUInt32OverflowException();
			}
			return (uint)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 32-bit unsigned integer.</summary>
		/// <param name="value">The 64-bit unsigned integer to convert.</param>
		/// <returns>A 32-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x060008D2 RID: 2258 RVA: 0x000241CF File Offset: 0x000223CF
		[CLSCompliant(false)]
		public static uint ToUInt32(ulong value)
		{
			if (value > (ulong)-1)
			{
				Convert.ThrowUInt32OverflowException();
			}
			return (uint)value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 32-bit unsigned integer.</summary>
		/// <param name="value">The single-precision floating-point number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x060008D3 RID: 2259 RVA: 0x000241DD File Offset: 0x000223DD
		[CLSCompliant(false)]
		public static uint ToUInt32(float value)
		{
			return Convert.ToUInt32((double)value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 32-bit unsigned integer.</summary>
		/// <param name="value">The double-precision floating-point number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x060008D4 RID: 2260 RVA: 0x000241E8 File Offset: 0x000223E8
		[CLSCompliant(false)]
		public static uint ToUInt32(double value)
		{
			if (value >= -0.5 && value < 4294967295.5)
			{
				uint num = (uint)value;
				double num2 = value - num;
				if (num2 > 0.5 || (num2 == 0.5 && (num & 1U) != 0U))
				{
					num += 1U;
				}
				return num;
			}
			throw new OverflowException("Value was either too large or too small for a UInt32.");
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 32-bit unsigned integer.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 32-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x060008D5 RID: 2261 RVA: 0x00024243 File Offset: 0x00022443
		[CLSCompliant(false)]
		public static uint ToUInt32(decimal value)
		{
			return decimal.ToUInt32(decimal.Round(value, 0));
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 32-bit unsigned integer.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <returns>A 32-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9).</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x060008D6 RID: 2262 RVA: 0x00024251 File Offset: 0x00022451
		[CLSCompliant(false)]
		public static uint ToUInt32(string value)
		{
			if (value == null)
			{
				return 0U;
			}
			return uint.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 32-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A 32-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9).</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x060008D7 RID: 2263 RVA: 0x00024263 File Offset: 0x00022463
		[CLSCompliant(false)]
		public static uint ToUInt32(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0U;
			}
			return uint.Parse(value, NumberStyles.Integer, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The date and time value to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x060008D8 RID: 2264 RVA: 0x00024272 File Offset: 0x00022472
		[CLSCompliant(false)]
		public static uint ToUInt32(DateTime value)
		{
			return ((IConvertible)value).ToUInt32(null);
		}

		/// <summary>Converts the value of the specified object to a 64-bit signed integer.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or <see langword="null" />.</param>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060008D9 RID: 2265 RVA: 0x00024280 File Offset: 0x00022480
		public static long ToInt64(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt64(null);
			}
			return 0L;
		}

		/// <summary>Converts the value of the specified object to a 64-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060008DA RID: 2266 RVA: 0x00024294 File Offset: 0x00022494
		public static long ToInt64(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt64(provider);
			}
			return 0L;
		}

		/// <summary>Converts the specified Boolean value to the equivalent 64-bit signed integer.</summary>
		/// <param name="value">The Boolean value to convert.</param>
		/// <returns>The number 1 if <paramref name="value" /> is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x060008DB RID: 2267 RVA: 0x000242A8 File Offset: 0x000224A8
		public static long ToInt64(bool value)
		{
			return value ? 1L : 0L;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 64-bit signed integer.</summary>
		/// <param name="value">The Unicode character to convert.</param>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008DC RID: 2268 RVA: 0x000242B2 File Offset: 0x000224B2
		public static long ToInt64(char value)
		{
			return (long)((ulong)value);
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 64-bit signed integer.</summary>
		/// <param name="value">The 8-bit signed integer to convert.</param>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008DD RID: 2269 RVA: 0x000242B6 File Offset: 0x000224B6
		[CLSCompliant(false)]
		public static long ToInt64(sbyte value)
		{
			return (long)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 64-bit signed integer.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008DE RID: 2270 RVA: 0x000242B2 File Offset: 0x000224B2
		public static long ToInt64(byte value)
		{
			return (long)((ulong)value);
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent 64-bit signed integer.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008DF RID: 2271 RVA: 0x000242B6 File Offset: 0x000224B6
		public static long ToInt64(short value)
		{
			return (long)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 64-bit signed integer.</summary>
		/// <param name="value">The 16-bit unsigned integer to convert.</param>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008E0 RID: 2272 RVA: 0x000242B2 File Offset: 0x000224B2
		[CLSCompliant(false)]
		public static long ToInt64(ushort value)
		{
			return (long)((ulong)value);
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 64-bit signed integer.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008E1 RID: 2273 RVA: 0x000242B6 File Offset: 0x000224B6
		public static long ToInt64(int value)
		{
			return (long)value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 64-bit signed integer.</summary>
		/// <param name="value">The 32-bit unsigned integer to convert.</param>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008E2 RID: 2274 RVA: 0x000242B2 File Offset: 0x000224B2
		[CLSCompliant(false)]
		public static long ToInt64(uint value)
		{
			return (long)((ulong)value);
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent 64-bit signed integer.</summary>
		/// <param name="value">The 64-bit unsigned integer to convert.</param>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060008E3 RID: 2275 RVA: 0x000242BA File Offset: 0x000224BA
		[CLSCompliant(false)]
		public static long ToInt64(ulong value)
		{
			if (value > 9223372036854775807UL)
			{
				Convert.ThrowInt64OverflowException();
			}
			return (long)value;
		}

		/// <summary>Returns the specified 64-bit signed integer; no actual conversion is performed.</summary>
		/// <param name="value">A 64-bit signed integer.</param>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		// Token: 0x060008E4 RID: 2276 RVA: 0x0000270D File Offset: 0x0000090D
		public static long ToInt64(long value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 64-bit signed integer.</summary>
		/// <param name="value">The single-precision floating-point number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int64.MaxValue" /> or less than <see cref="F:System.Int64.MinValue" />.</exception>
		// Token: 0x060008E5 RID: 2277 RVA: 0x000242CE File Offset: 0x000224CE
		public static long ToInt64(float value)
		{
			return Convert.ToInt64((double)value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 64-bit signed integer.</summary>
		/// <param name="value">The double-precision floating-point number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int64.MaxValue" /> or less than <see cref="F:System.Int64.MinValue" />.</exception>
		// Token: 0x060008E6 RID: 2278 RVA: 0x000242D7 File Offset: 0x000224D7
		public static long ToInt64(double value)
		{
			return checked((long)Math.Round(value));
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 64-bit signed integer.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit signed integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Int64.MaxValue" /> or less than <see cref="F:System.Int64.MinValue" />.</exception>
		// Token: 0x060008E7 RID: 2279 RVA: 0x000242E0 File Offset: 0x000224E0
		public static long ToInt64(decimal value)
		{
			return decimal.ToInt64(decimal.Round(value, 0));
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 64-bit signed integer.</summary>
		/// <param name="value">A string that contains a number to convert.</param>
		/// <returns>A 64-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9).</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060008E8 RID: 2280 RVA: 0x000242EE File Offset: 0x000224EE
		public static long ToInt64(string value)
		{
			if (value == null)
			{
				return 0L;
			}
			return long.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 64-bit signed integer, using the specified culture-specific formatting information.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A 64-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9).</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x060008E9 RID: 2281 RVA: 0x00024301 File Offset: 0x00022501
		public static long ToInt64(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0L;
			}
			return long.Parse(value, NumberStyles.Integer, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The date and time value to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x060008EA RID: 2282 RVA: 0x00024311 File Offset: 0x00022511
		public static long ToInt64(DateTime value)
		{
			return ((IConvertible)value).ToInt64(null);
		}

		/// <summary>Converts the value of the specified object to a 64-bit unsigned integer.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or <see langword="null" />.</param>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x060008EB RID: 2283 RVA: 0x0002431F File Offset: 0x0002251F
		[CLSCompliant(false)]
		public static ulong ToUInt64(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt64(null);
			}
			return 0UL;
		}

		/// <summary>Converts the value of the specified object to a 64-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x060008EC RID: 2284 RVA: 0x00024333 File Offset: 0x00022533
		[CLSCompliant(false)]
		public static ulong ToUInt64(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt64(provider);
			}
			return 0UL;
		}

		/// <summary>Converts the specified Boolean value to the equivalent 64-bit unsigned integer.</summary>
		/// <param name="value">The Boolean value to convert.</param>
		/// <returns>The number 1 if <paramref name="value" /> is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x060008ED RID: 2285 RVA: 0x00024347 File Offset: 0x00022547
		[CLSCompliant(false)]
		public static ulong ToUInt64(bool value)
		{
			if (!value)
			{
				return 0UL;
			}
			return 1UL;
		}

		/// <summary>Converts the value of the specified Unicode character to the equivalent 64-bit unsigned integer.</summary>
		/// <param name="value">The Unicode character to convert.</param>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008EE RID: 2286 RVA: 0x000242B2 File Offset: 0x000224B2
		[CLSCompliant(false)]
		public static ulong ToUInt64(char value)
		{
			return (ulong)value;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent 64-bit unsigned integer.</summary>
		/// <param name="value">The 8-bit signed integer to convert.</param>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero.</exception>
		// Token: 0x060008EF RID: 2287 RVA: 0x00024351 File Offset: 0x00022551
		[CLSCompliant(false)]
		public static ulong ToUInt64(sbyte value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt64OverflowException();
			}
			return (ulong)((long)value);
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent 64-bit unsigned integer.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <returns>A 64-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008F0 RID: 2288 RVA: 0x000242B2 File Offset: 0x000224B2
		[CLSCompliant(false)]
		public static ulong ToUInt64(byte value)
		{
			return (ulong)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to the equivalent 64-bit unsigned integer.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero.</exception>
		// Token: 0x060008F1 RID: 2289 RVA: 0x00024351 File Offset: 0x00022551
		[CLSCompliant(false)]
		public static ulong ToUInt64(short value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt64OverflowException();
			}
			return (ulong)((long)value);
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent 64-bit unsigned integer.</summary>
		/// <param name="value">The 16-bit unsigned integer to convert.</param>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008F2 RID: 2290 RVA: 0x000242B2 File Offset: 0x000224B2
		[CLSCompliant(false)]
		public static ulong ToUInt64(ushort value)
		{
			return (ulong)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent 64-bit unsigned integer.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero.</exception>
		// Token: 0x060008F3 RID: 2291 RVA: 0x00024351 File Offset: 0x00022551
		[CLSCompliant(false)]
		public static ulong ToUInt64(int value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt64OverflowException();
			}
			return (ulong)((long)value);
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent 64-bit unsigned integer.</summary>
		/// <param name="value">The 32-bit unsigned integer to convert.</param>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008F4 RID: 2292 RVA: 0x000242B2 File Offset: 0x000224B2
		[CLSCompliant(false)]
		public static ulong ToUInt64(uint value)
		{
			return (ulong)value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent 64-bit unsigned integer.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <returns>A 64-bit unsigned integer that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero.</exception>
		// Token: 0x060008F5 RID: 2293 RVA: 0x0002435E File Offset: 0x0002255E
		[CLSCompliant(false)]
		public static ulong ToUInt64(long value)
		{
			if (value < 0L)
			{
				Convert.ThrowUInt64OverflowException();
			}
			return (ulong)value;
		}

		/// <summary>Returns the specified 64-bit unsigned integer; no actual conversion is performed.</summary>
		/// <param name="value">The 64-bit unsigned integer to return.</param>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		// Token: 0x060008F6 RID: 2294 RVA: 0x0000270D File Offset: 0x0000090D
		[CLSCompliant(false)]
		public static ulong ToUInt64(ulong value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent 64-bit unsigned integer.</summary>
		/// <param name="value">The single-precision floating-point number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x060008F7 RID: 2295 RVA: 0x0002436B File Offset: 0x0002256B
		[CLSCompliant(false)]
		public static ulong ToUInt64(float value)
		{
			return Convert.ToUInt64((double)value);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent 64-bit unsigned integer.</summary>
		/// <param name="value">The double-precision floating-point number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x060008F8 RID: 2296 RVA: 0x00024374 File Offset: 0x00022574
		[CLSCompliant(false)]
		public static ulong ToUInt64(double value)
		{
			return checked((ulong)Math.Round(value));
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent 64-bit unsigned integer.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>
		///   <paramref name="value" />, rounded to the nearest 64-bit unsigned integer. If <paramref name="value" /> is halfway between two whole numbers, the even number is returned; that is, 4.5 is converted to 4, and 5.5 is converted to 6.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than zero or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x060008F9 RID: 2297 RVA: 0x0002437D File Offset: 0x0002257D
		[CLSCompliant(false)]
		public static ulong ToUInt64(decimal value)
		{
			return decimal.ToUInt64(decimal.Round(value, 0));
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 64-bit unsigned integer.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <returns>A 64-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9).</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x060008FA RID: 2298 RVA: 0x0002438B File Offset: 0x0002258B
		[CLSCompliant(false)]
		public static ulong ToUInt64(string value)
		{
			if (value == null)
			{
				return 0UL;
			}
			return ulong.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent 64-bit unsigned integer, using the specified culture-specific formatting information.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A 64-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> does not consist of an optional sign followed by a sequence of digits (0 through 9).</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x060008FB RID: 2299 RVA: 0x0002439E File Offset: 0x0002259E
		[CLSCompliant(false)]
		public static ulong ToUInt64(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0UL;
			}
			return ulong.Parse(value, NumberStyles.Integer, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The date and time value to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x060008FC RID: 2300 RVA: 0x000243AE File Offset: 0x000225AE
		[CLSCompliant(false)]
		public static ulong ToUInt64(DateTime value)
		{
			return ((IConvertible)value).ToUInt64(null);
		}

		/// <summary>Converts the value of the specified object to a single-precision floating-point number.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or <see langword="null" />.</param>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />.</exception>
		// Token: 0x060008FD RID: 2301 RVA: 0x000243BC File Offset: 0x000225BC
		public static float ToSingle(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSingle(null);
			}
			return 0f;
		}

		/// <summary>Converts the value of the specified object to an single-precision floating-point number, using the specified culture-specific formatting information.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement <see cref="T:System.IConvertible" />.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />.</exception>
		// Token: 0x060008FE RID: 2302 RVA: 0x000243D3 File Offset: 0x000225D3
		public static float ToSingle(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSingle(provider);
			}
			return 0f;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent single-precision floating-point number.</summary>
		/// <param name="value">The 8-bit signed integer to convert.</param>
		/// <returns>An 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x060008FF RID: 2303 RVA: 0x000243EA File Offset: 0x000225EA
		[CLSCompliant(false)]
		public static float ToSingle(sbyte value)
		{
			return (float)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent single-precision floating-point number.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000900 RID: 2304 RVA: 0x000243EA File Offset: 0x000225EA
		public static float ToSingle(byte value)
		{
			return (float)value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The Unicode character to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000901 RID: 2305 RVA: 0x000243EE File Offset: 0x000225EE
		public static float ToSingle(char value)
		{
			return ((IConvertible)value).ToSingle(null);
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent single-precision floating-point number.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000902 RID: 2306 RVA: 0x000243EA File Offset: 0x000225EA
		public static float ToSingle(short value)
		{
			return (float)value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent single-precision floating-point number.</summary>
		/// <param name="value">The 16-bit unsigned integer to convert.</param>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000903 RID: 2307 RVA: 0x000243EA File Offset: 0x000225EA
		[CLSCompliant(false)]
		public static float ToSingle(ushort value)
		{
			return (float)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent single-precision floating-point number.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000904 RID: 2308 RVA: 0x000243EA File Offset: 0x000225EA
		public static float ToSingle(int value)
		{
			return (float)value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent single-precision floating-point number.</summary>
		/// <param name="value">The 32-bit unsigned integer to convert.</param>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000905 RID: 2309 RVA: 0x000243FC File Offset: 0x000225FC
		[CLSCompliant(false)]
		public static float ToSingle(uint value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent single-precision floating-point number.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000906 RID: 2310 RVA: 0x000243EA File Offset: 0x000225EA
		public static float ToSingle(long value)
		{
			return (float)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent single-precision floating-point number.</summary>
		/// <param name="value">The 64-bit unsigned integer to convert.</param>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000907 RID: 2311 RVA: 0x000243FC File Offset: 0x000225FC
		[CLSCompliant(false)]
		public static float ToSingle(ulong value)
		{
			return value;
		}

		/// <summary>Returns the specified single-precision floating-point number; no actual conversion is performed.</summary>
		/// <param name="value">The single-precision floating-point number to return.</param>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		// Token: 0x06000908 RID: 2312 RVA: 0x0000270D File Offset: 0x0000090D
		public static float ToSingle(float value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent single-precision floating-point number.</summary>
		/// <param name="value">The double-precision floating-point number to convert.</param>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.  
		///  <paramref name="value" /> is rounded using rounding to nearest. For example, when rounded to two decimals, the value 2.345 becomes 2.34 and the value 2.355 becomes 2.36.</returns>
		// Token: 0x06000909 RID: 2313 RVA: 0x000243EA File Offset: 0x000225EA
		public static float ToSingle(double value)
		{
			return (float)value;
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent single-precision floating-point number.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>A single-precision floating-point number that is equivalent to <paramref name="value" />.  
		///  <paramref name="value" /> is rounded using rounding to nearest. For example, when rounded to two decimals, the value 2.345 becomes 2.34 and the value 2.355 becomes 2.36.</returns>
		// Token: 0x0600090A RID: 2314 RVA: 0x00024401 File Offset: 0x00022601
		public static float ToSingle(decimal value)
		{
			return (float)value;
		}

		/// <summary>Converts the specified string representation of a number to an equivalent single-precision floating-point number.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <returns>A single-precision floating-point number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />.</exception>
		// Token: 0x0600090B RID: 2315 RVA: 0x0002440A File Offset: 0x0002260A
		public static float ToSingle(string value)
		{
			if (value == null)
			{
				return 0f;
			}
			return float.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent single-precision floating-point number, using the specified culture-specific formatting information.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A single-precision floating-point number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Single.MinValue" /> or greater than <see cref="F:System.Single.MaxValue" />.</exception>
		// Token: 0x0600090C RID: 2316 RVA: 0x00024420 File Offset: 0x00022620
		public static float ToSingle(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0f;
			}
			return float.Parse(value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, provider);
		}

		/// <summary>Converts the specified Boolean value to the equivalent single-precision floating-point number.</summary>
		/// <param name="value">The Boolean value to convert.</param>
		/// <returns>The number 1 if <paramref name="value" /> is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x0600090D RID: 2317 RVA: 0x00024437 File Offset: 0x00022637
		public static float ToSingle(bool value)
		{
			return (float)(value ? 1 : 0);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The date and time value to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x0600090E RID: 2318 RVA: 0x00024441 File Offset: 0x00022641
		public static float ToSingle(DateTime value)
		{
			return ((IConvertible)value).ToSingle(null);
		}

		/// <summary>Converts the value of the specified object to a double-precision floating-point number.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or <see langword="null" />.</param>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for a <see cref="T:System.Double" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />.</exception>
		// Token: 0x0600090F RID: 2319 RVA: 0x0002444F File Offset: 0x0002264F
		public static double ToDouble(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDouble(null);
			}
			return 0.0;
		}

		/// <summary>Converts the value of the specified object to an double-precision floating-point number, using the specified culture-specific formatting information.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />, or zero if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for a <see cref="T:System.Double" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />.</exception>
		// Token: 0x06000910 RID: 2320 RVA: 0x0002446A File Offset: 0x0002266A
		public static double ToDouble(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDouble(provider);
			}
			return 0.0;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent double-precision floating-point number.</summary>
		/// <param name="value">The 8-bit signed integer to convert.</param>
		/// <returns>The 8-bit signed integer that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000911 RID: 2321 RVA: 0x00024485 File Offset: 0x00022685
		[CLSCompliant(false)]
		public static double ToDouble(sbyte value)
		{
			return (double)value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent double-precision floating-point number.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <returns>The double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000912 RID: 2322 RVA: 0x00024485 File Offset: 0x00022685
		public static double ToDouble(byte value)
		{
			return (double)value;
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent double-precision floating-point number.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <returns>A double-precision floating-point number equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000913 RID: 2323 RVA: 0x00024485 File Offset: 0x00022685
		public static double ToDouble(short value)
		{
			return (double)value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The Unicode character to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000914 RID: 2324 RVA: 0x00024489 File Offset: 0x00022689
		public static double ToDouble(char value)
		{
			return ((IConvertible)value).ToDouble(null);
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to the equivalent double-precision floating-point number.</summary>
		/// <param name="value">The 16-bit unsigned integer to convert.</param>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000915 RID: 2325 RVA: 0x00024485 File Offset: 0x00022685
		[CLSCompliant(false)]
		public static double ToDouble(ushort value)
		{
			return (double)value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent double-precision floating-point number.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000916 RID: 2326 RVA: 0x00024485 File Offset: 0x00022685
		public static double ToDouble(int value)
		{
			return (double)value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent double-precision floating-point number.</summary>
		/// <param name="value">The 32-bit unsigned integer to convert.</param>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000917 RID: 2327 RVA: 0x00024497 File Offset: 0x00022697
		[CLSCompliant(false)]
		public static double ToDouble(uint value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent double-precision floating-point number.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000918 RID: 2328 RVA: 0x00024485 File Offset: 0x00022685
		public static double ToDouble(long value)
		{
			return (double)value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent double-precision floating-point number.</summary>
		/// <param name="value">The 64-bit unsigned integer to convert.</param>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000919 RID: 2329 RVA: 0x00024497 File Offset: 0x00022697
		[CLSCompliant(false)]
		public static double ToDouble(ulong value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to an equivalent double-precision floating-point number.</summary>
		/// <param name="value">The single-precision floating-point number.</param>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x0600091A RID: 2330 RVA: 0x00024485 File Offset: 0x00022685
		public static double ToDouble(float value)
		{
			return (double)value;
		}

		/// <summary>Returns the specified double-precision floating-point number; no actual conversion is performed.</summary>
		/// <param name="value">The double-precision floating-point number to return.</param>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		// Token: 0x0600091B RID: 2331 RVA: 0x0000270D File Offset: 0x0000090D
		public static double ToDouble(double value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified decimal number to an equivalent double-precision floating-point number.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>A double-precision floating-point number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x0600091C RID: 2332 RVA: 0x0002449C File Offset: 0x0002269C
		public static double ToDouble(decimal value)
		{
			return (double)value;
		}

		/// <summary>Converts the specified string representation of a number to an equivalent double-precision floating-point number.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <returns>A double-precision floating-point number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />.</exception>
		// Token: 0x0600091D RID: 2333 RVA: 0x000244A5 File Offset: 0x000226A5
		public static double ToDouble(string value)
		{
			if (value == null)
			{
				return 0.0;
			}
			return double.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent double-precision floating-point number, using the specified culture-specific formatting information.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A double-precision floating-point number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Double.MinValue" /> or greater than <see cref="F:System.Double.MaxValue" />.</exception>
		// Token: 0x0600091E RID: 2334 RVA: 0x000244BF File Offset: 0x000226BF
		public static double ToDouble(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0.0;
			}
			return double.Parse(value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, provider);
		}

		/// <summary>Converts the specified Boolean value to the equivalent double-precision floating-point number.</summary>
		/// <param name="value">The Boolean value to convert.</param>
		/// <returns>The number 1 if <paramref name="value" /> is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x0600091F RID: 2335 RVA: 0x000244DA File Offset: 0x000226DA
		public static double ToDouble(bool value)
		{
			return (double)(value ? 1 : 0);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The date and time value to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000920 RID: 2336 RVA: 0x000244E4 File Offset: 0x000226E4
		public static double ToDouble(DateTime value)
		{
			return ((IConvertible)value).ToDouble(null);
		}

		/// <summary>Converts the value of the specified object to an equivalent decimal number.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or <see langword="null" />.</param>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for a <see cref="T:System.Decimal" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000921 RID: 2337 RVA: 0x000244F2 File Offset: 0x000226F2
		public static decimal ToDecimal(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDecimal(null);
			}
			return 0m;
		}

		/// <summary>Converts the value of the specified object to an equivalent decimal number, using the specified culture-specific formatting information.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not in an appropriate format for a <see cref="T:System.Decimal" /> type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06000922 RID: 2338 RVA: 0x00024509 File Offset: 0x00022709
		public static decimal ToDecimal(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDecimal(provider);
			}
			return 0m;
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to the equivalent decimal number.</summary>
		/// <param name="value">The 8-bit signed integer to convert.</param>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000923 RID: 2339 RVA: 0x00024520 File Offset: 0x00022720
		[CLSCompliant(false)]
		public static decimal ToDecimal(sbyte value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to the equivalent decimal number.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <returns>The decimal number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000924 RID: 2340 RVA: 0x00024528 File Offset: 0x00022728
		public static decimal ToDecimal(byte value)
		{
			return value;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The Unicode character to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000925 RID: 2341 RVA: 0x00024530 File Offset: 0x00022730
		public static decimal ToDecimal(char value)
		{
			return ((IConvertible)value).ToDecimal(null);
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to an equivalent decimal number.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000926 RID: 2342 RVA: 0x0002453E File Offset: 0x0002273E
		public static decimal ToDecimal(short value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to an equivalent decimal number.</summary>
		/// <param name="value">The 16-bit unsigned integer to convert.</param>
		/// <returns>The decimal number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000927 RID: 2343 RVA: 0x00024546 File Offset: 0x00022746
		[CLSCompliant(false)]
		public static decimal ToDecimal(ushort value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to an equivalent decimal number.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000928 RID: 2344 RVA: 0x0002454E File Offset: 0x0002274E
		public static decimal ToDecimal(int value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to an equivalent decimal number.</summary>
		/// <param name="value">The 32-bit unsigned integer to convert.</param>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x06000929 RID: 2345 RVA: 0x00024556 File Offset: 0x00022756
		[CLSCompliant(false)]
		public static decimal ToDecimal(uint value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to an equivalent decimal number.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x0600092A RID: 2346 RVA: 0x0002455E File Offset: 0x0002275E
		public static decimal ToDecimal(long value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to an equivalent decimal number.</summary>
		/// <param name="value">The 64-bit unsigned integer to convert.</param>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		// Token: 0x0600092B RID: 2347 RVA: 0x00024566 File Offset: 0x00022766
		[CLSCompliant(false)]
		public static decimal ToDecimal(ulong value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to the equivalent decimal number.</summary>
		/// <param name="value">The single-precision floating-point number to convert.</param>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.</exception>
		// Token: 0x0600092C RID: 2348 RVA: 0x0002456E File Offset: 0x0002276E
		public static decimal ToDecimal(float value)
		{
			return (decimal)value;
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to an equivalent decimal number.</summary>
		/// <param name="value">The double-precision floating-point number to convert.</param>
		/// <returns>A decimal number that is equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.</exception>
		// Token: 0x0600092D RID: 2349 RVA: 0x00024576 File Offset: 0x00022776
		public static decimal ToDecimal(double value)
		{
			return (decimal)value;
		}

		/// <summary>Converts the specified string representation of a number to an equivalent decimal number.</summary>
		/// <param name="value">A string that contains a number to convert.</param>
		/// <returns>A decimal number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x0600092E RID: 2350 RVA: 0x0002457E File Offset: 0x0002277E
		public static decimal ToDecimal(string value)
		{
			if (value == null)
			{
				return 0m;
			}
			return decimal.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent decimal number, using the specified culture-specific formatting information.</summary>
		/// <param name="value">A string that contains a number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>A decimal number that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a number in a valid format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> represents a number that is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x0600092F RID: 2351 RVA: 0x00024594 File Offset: 0x00022794
		public static decimal ToDecimal(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0m;
			}
			return decimal.Parse(value, NumberStyles.Number, provider);
		}

		/// <summary>Returns the specified decimal number; no actual conversion is performed.</summary>
		/// <param name="value">A decimal number.</param>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		// Token: 0x06000930 RID: 2352 RVA: 0x0000270D File Offset: 0x0000090D
		public static decimal ToDecimal(decimal value)
		{
			return value;
		}

		/// <summary>Converts the specified Boolean value to the equivalent decimal number.</summary>
		/// <param name="value">The Boolean value to convert.</param>
		/// <returns>The number 1 if <paramref name="value" /> is <see langword="true" />; otherwise, 0.</returns>
		// Token: 0x06000931 RID: 2353 RVA: 0x000245A8 File Offset: 0x000227A8
		public static decimal ToDecimal(bool value)
		{
			return value ? 1 : 0;
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The date and time value to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000932 RID: 2354 RVA: 0x000245B6 File Offset: 0x000227B6
		public static decimal ToDecimal(DateTime value)
		{
			return ((IConvertible)value).ToDecimal(null);
		}

		/// <summary>Returns the specified <see cref="T:System.DateTime" /> object; no actual conversion is performed.</summary>
		/// <param name="value">A date and time value.</param>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		// Token: 0x06000933 RID: 2355 RVA: 0x0000270D File Offset: 0x0000090D
		public static DateTime ToDateTime(DateTime value)
		{
			return value;
		}

		/// <summary>Converts the value of the specified object to a <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface, or <see langword="null" />.</param>
		/// <returns>The date and time equivalent of the value of <paramref name="value" />, or a date and time equivalent of <see cref="F:System.DateTime.MinValue" /> if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a valid date and time value.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		// Token: 0x06000934 RID: 2356 RVA: 0x000245C4 File Offset: 0x000227C4
		public static DateTime ToDateTime(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDateTime(null);
			}
			return DateTime.MinValue;
		}

		/// <summary>Converts the value of the specified object to a <see cref="T:System.DateTime" /> object, using the specified culture-specific formatting information.</summary>
		/// <param name="value">An object that implements the <see cref="T:System.IConvertible" /> interface.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The date and time equivalent of the value of <paramref name="value" />, or the date and time equivalent of <see cref="F:System.DateTime.MinValue" /> if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a valid date and time value.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="value" /> does not implement the <see cref="T:System.IConvertible" /> interface.  
		/// -or-  
		/// The conversion is not supported.</exception>
		// Token: 0x06000935 RID: 2357 RVA: 0x000245DB File Offset: 0x000227DB
		public static DateTime ToDateTime(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDateTime(provider);
			}
			return DateTime.MinValue;
		}

		/// <summary>Converts the specified string representation of a date and time to an equivalent date and time value.</summary>
		/// <param name="value">The string representation of a date and time.</param>
		/// <returns>The date and time equivalent of the value of <paramref name="value" />, or the date and time equivalent of <see cref="F:System.DateTime.MinValue" /> if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a properly formatted date and time string.</exception>
		// Token: 0x06000936 RID: 2358 RVA: 0x000245F2 File Offset: 0x000227F2
		public static DateTime ToDateTime(string value)
		{
			if (value == null)
			{
				return new DateTime(0L);
			}
			return DateTime.Parse(value, CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the specified string representation of a number to an equivalent date and time, using the specified culture-specific formatting information.</summary>
		/// <param name="value">A string that contains a date and time to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The date and time equivalent of the value of <paramref name="value" />, or the date and time equivalent of <see cref="F:System.DateTime.MinValue" /> if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a properly formatted date and time string.</exception>
		// Token: 0x06000937 RID: 2359 RVA: 0x0002460A File Offset: 0x0002280A
		public static DateTime ToDateTime(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return new DateTime(0L);
			}
			return DateTime.Parse(value, provider);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The 8-bit signed integer to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000938 RID: 2360 RVA: 0x0002461E File Offset: 0x0002281E
		[CLSCompliant(false)]
		public static DateTime ToDateTime(sbyte value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000939 RID: 2361 RVA: 0x0002462C File Offset: 0x0002282C
		public static DateTime ToDateTime(byte value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x0600093A RID: 2362 RVA: 0x0002463A File Offset: 0x0002283A
		public static DateTime ToDateTime(short value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The 16-bit unsigned integer to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x0600093B RID: 2363 RVA: 0x00024648 File Offset: 0x00022848
		[CLSCompliant(false)]
		public static DateTime ToDateTime(ushort value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x0600093C RID: 2364 RVA: 0x00024656 File Offset: 0x00022856
		public static DateTime ToDateTime(int value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The 32-bit unsigned integer to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x0600093D RID: 2365 RVA: 0x00024664 File Offset: 0x00022864
		[CLSCompliant(false)]
		public static DateTime ToDateTime(uint value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x0600093E RID: 2366 RVA: 0x00024672 File Offset: 0x00022872
		public static DateTime ToDateTime(long value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The 64-bit unsigned integer to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x0600093F RID: 2367 RVA: 0x00024680 File Offset: 0x00022880
		[CLSCompliant(false)]
		public static DateTime ToDateTime(ulong value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The Boolean value to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000940 RID: 2368 RVA: 0x0002468E File Offset: 0x0002288E
		public static DateTime ToDateTime(bool value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The Unicode character to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000941 RID: 2369 RVA: 0x0002469C File Offset: 0x0002289C
		public static DateTime ToDateTime(char value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The single-precision floating-point value to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000942 RID: 2370 RVA: 0x000246AA File Offset: 0x000228AA
		public static DateTime ToDateTime(float value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The double-precision floating-point value to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000943 RID: 2371 RVA: 0x000246B8 File Offset: 0x000228B8
		public static DateTime ToDateTime(double value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Calling this method always throws <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="value">The number to convert.</param>
		/// <returns>This conversion is not supported. No value is returned.</returns>
		/// <exception cref="T:System.InvalidCastException">This conversion is not supported.</exception>
		// Token: 0x06000944 RID: 2372 RVA: 0x000246C6 File Offset: 0x000228C6
		public static DateTime ToDateTime(decimal value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		/// <summary>Converts the value of the specified object to its equivalent string representation.</summary>
		/// <param name="value">An object that supplies the value to convert, or <see langword="null" />.</param>
		/// <returns>The string representation of <paramref name="value" />, or <see cref="F:System.String.Empty" /> if <paramref name="value" /> is <see langword="null" />.</returns>
		// Token: 0x06000945 RID: 2373 RVA: 0x000246D4 File Offset: 0x000228D4
		public static string ToString(object value)
		{
			return Convert.ToString(value, null);
		}

		/// <summary>Converts the value of the specified object to its equivalent string representation using the specified culture-specific formatting information.</summary>
		/// <param name="value">An object that supplies the value to convert, or <see langword="null" />.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of <paramref name="value" />, or <see cref="F:System.String.Empty" /> if <paramref name="value" /> is an object whose value is <see langword="null" />. If <paramref name="value" /> is <see langword="null" />, the method returns <see langword="null" />.</returns>
		// Token: 0x06000946 RID: 2374 RVA: 0x000246E0 File Offset: 0x000228E0
		public static string ToString(object value, IFormatProvider provider)
		{
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				return convertible.ToString(provider);
			}
			IFormattable formattable = value as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(null, provider);
			}
			if (value != null)
			{
				return value.ToString();
			}
			return string.Empty;
		}

		/// <summary>Converts the specified Boolean value to its equivalent string representation.</summary>
		/// <param name="value">The Boolean value to convert.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x06000947 RID: 2375 RVA: 0x00024721 File Offset: 0x00022921
		public static string ToString(bool value)
		{
			return value.ToString();
		}

		/// <summary>Converts the specified Boolean value to its equivalent string representation.</summary>
		/// <param name="value">The Boolean value to convert.</param>
		/// <param name="provider">An instance of an object. This parameter is ignored.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x06000948 RID: 2376 RVA: 0x00024721 File Offset: 0x00022921
		public static string ToString(bool value, IFormatProvider provider)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of the specified Unicode character to its equivalent string representation.</summary>
		/// <param name="value">The Unicode character to convert.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x06000949 RID: 2377 RVA: 0x0002472A File Offset: 0x0002292A
		public static string ToString(char value)
		{
			return char.ToString(value);
		}

		/// <summary>Converts the value of the specified Unicode character to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <param name="value">The Unicode character to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information. This parameter is ignored.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x0600094A RID: 2378 RVA: 0x00024732 File Offset: 0x00022932
		public static string ToString(char value, IFormatProvider provider)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to its equivalent string representation.</summary>
		/// <param name="value">The 8-bit signed integer to convert.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x0600094B RID: 2379 RVA: 0x0002473B File Offset: 0x0002293B
		[CLSCompliant(false)]
		public static string ToString(sbyte value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified 8-bit signed integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <param name="value">The 8-bit signed integer to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x0600094C RID: 2380 RVA: 0x00024749 File Offset: 0x00022949
		[CLSCompliant(false)]
		public static string ToString(sbyte value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to its equivalent string representation.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x0600094D RID: 2381 RVA: 0x00024753 File Offset: 0x00022953
		public static string ToString(byte value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified 8-bit unsigned integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x0600094E RID: 2382 RVA: 0x00024761 File Offset: 0x00022961
		public static string ToString(byte value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to its equivalent string representation.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x0600094F RID: 2383 RVA: 0x0002476B File Offset: 0x0002296B
		public static string ToString(short value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified 16-bit signed integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x06000950 RID: 2384 RVA: 0x00024779 File Offset: 0x00022979
		public static string ToString(short value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to its equivalent string representation.</summary>
		/// <param name="value">The 16-bit unsigned integer to convert.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x06000951 RID: 2385 RVA: 0x00024783 File Offset: 0x00022983
		[CLSCompliant(false)]
		public static string ToString(ushort value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified 16-bit unsigned integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <param name="value">The 16-bit unsigned integer to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x06000952 RID: 2386 RVA: 0x00024791 File Offset: 0x00022991
		[CLSCompliant(false)]
		public static string ToString(ushort value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to its equivalent string representation.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x06000953 RID: 2387 RVA: 0x0002479B File Offset: 0x0002299B
		public static string ToString(int value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified 32-bit signed integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x06000954 RID: 2388 RVA: 0x000247A9 File Offset: 0x000229A9
		public static string ToString(int value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to its equivalent string representation.</summary>
		/// <param name="value">The 32-bit unsigned integer to convert.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x06000955 RID: 2389 RVA: 0x000247B3 File Offset: 0x000229B3
		[CLSCompliant(false)]
		public static string ToString(uint value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified 32-bit unsigned integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <param name="value">The 32-bit unsigned integer to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x06000956 RID: 2390 RVA: 0x000247C1 File Offset: 0x000229C1
		[CLSCompliant(false)]
		public static string ToString(uint value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to its equivalent string representation.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x06000957 RID: 2391 RVA: 0x000247CB File Offset: 0x000229CB
		public static string ToString(long value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified 64-bit signed integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x06000958 RID: 2392 RVA: 0x000247D9 File Offset: 0x000229D9
		public static string ToString(long value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to its equivalent string representation.</summary>
		/// <param name="value">The 64-bit unsigned integer to convert.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x06000959 RID: 2393 RVA: 0x000247E3 File Offset: 0x000229E3
		[CLSCompliant(false)]
		public static string ToString(ulong value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified 64-bit unsigned integer to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <param name="value">The 64-bit unsigned integer to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x0600095A RID: 2394 RVA: 0x000247F1 File Offset: 0x000229F1
		[CLSCompliant(false)]
		public static string ToString(ulong value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to its equivalent string representation.</summary>
		/// <param name="value">The single-precision floating-point number to convert.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x0600095B RID: 2395 RVA: 0x000247FB File Offset: 0x000229FB
		public static string ToString(float value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified single-precision floating-point number to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <param name="value">The single-precision floating-point number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x0600095C RID: 2396 RVA: 0x00024809 File Offset: 0x00022A09
		public static string ToString(float value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to its equivalent string representation.</summary>
		/// <param name="value">The double-precision floating-point number to convert.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x0600095D RID: 2397 RVA: 0x00024813 File Offset: 0x00022A13
		public static string ToString(double value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified double-precision floating-point number to its equivalent string representation.</summary>
		/// <param name="value">The double-precision floating-point number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x0600095E RID: 2398 RVA: 0x00024821 File Offset: 0x00022A21
		public static string ToString(double value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified decimal number to its equivalent string representation.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x0600095F RID: 2399 RVA: 0x0002482B File Offset: 0x00022A2B
		public static string ToString(decimal value)
		{
			return value.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the value of the specified decimal number to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x06000960 RID: 2400 RVA: 0x00024839 File Offset: 0x00022A39
		public static string ToString(decimal value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Converts the value of the specified <see cref="T:System.DateTime" /> to its equivalent string representation.</summary>
		/// <param name="value">The date and time value to convert.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x06000961 RID: 2401 RVA: 0x00024843 File Offset: 0x00022A43
		public static string ToString(DateTime value)
		{
			return value.ToString();
		}

		/// <summary>Converts the value of the specified <see cref="T:System.DateTime" /> to its equivalent string representation, using the specified culture-specific formatting information.</summary>
		/// <param name="value">The date and time value to convert.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of <paramref name="value" />.</returns>
		// Token: 0x06000962 RID: 2402 RVA: 0x0002484C File Offset: 0x00022A4C
		public static string ToString(DateTime value, IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		/// <summary>Returns the specified string instance; no actual conversion is performed.</summary>
		/// <param name="value">The string to return.</param>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		// Token: 0x06000963 RID: 2403 RVA: 0x0000270D File Offset: 0x0000090D
		public static string ToString(string value)
		{
			return value;
		}

		/// <summary>Returns the specified string instance; no actual conversion is performed.</summary>
		/// <param name="value">The string to return.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information. This parameter is ignored.</param>
		/// <returns>
		///   <paramref name="value" /> is returned unchanged.</returns>
		// Token: 0x06000964 RID: 2404 RVA: 0x0000270D File Offset: 0x0000090D
		public static string ToString(string value, IFormatProvider provider)
		{
			return value;
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 8-bit unsigned integer.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16.</param>
		/// <returns>An 8-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16.  
		/// -or-  
		/// <paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a base 10 unsigned number, is prefixed with a negative sign.  
		/// -or-  
		/// <paramref name="value" /> represents a number that is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x06000965 RID: 2405 RVA: 0x00024858 File Offset: 0x00022A58
		public static byte ToByte(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			if (value == null)
			{
				return 0;
			}
			int num = ParseNumbers.StringToInt(value.AsSpan(), fromBase, 4608);
			if (num < 0 || num > 255)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)num;
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 8-bit signed integer.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16.</param>
		/// <returns>An 8-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16.  
		/// -or-  
		/// <paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign.  
		/// -or-  
		/// <paramref name="value" /> represents a number that is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		// Token: 0x06000966 RID: 2406 RVA: 0x000248AC File Offset: 0x00022AAC
		[CLSCompliant(false)]
		public static sbyte ToSByte(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			if (value == null)
			{
				return 0;
			}
			int num = ParseNumbers.StringToInt(value.AsSpan(), fromBase, 5120);
			if (fromBase != 10 && num <= 255)
			{
				return (sbyte)num;
			}
			if (num < -128 || num > 127)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)num;
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 16-bit signed integer.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16.</param>
		/// <returns>A 16-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16.  
		/// -or-  
		/// <paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign.  
		/// -or-  
		/// <paramref name="value" /> represents a number that is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x06000967 RID: 2407 RVA: 0x00024910 File Offset: 0x00022B10
		public static short ToInt16(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			if (value == null)
			{
				return 0;
			}
			int num = ParseNumbers.StringToInt(value.AsSpan(), fromBase, 6144);
			if (fromBase != 10 && num <= 65535)
			{
				return (short)num;
			}
			if (num < -32768 || num > 32767)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)num;
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 16-bit unsigned integer.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16.</param>
		/// <returns>A 16-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16.  
		/// -or-  
		/// <paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign.  
		/// -or-  
		/// <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x06000968 RID: 2408 RVA: 0x00024978 File Offset: 0x00022B78
		[CLSCompliant(false)]
		public static ushort ToUInt16(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			if (value == null)
			{
				return 0;
			}
			int num = ParseNumbers.StringToInt(value.AsSpan(), fromBase, 4608);
			if (num < 0 || num > 65535)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)num;
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 32-bit signed integer.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16.</param>
		/// <returns>A 32-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16.  
		/// -or-  
		/// <paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign.  
		/// -or-  
		/// <paramref name="value" /> represents a number that is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06000969 RID: 2409 RVA: 0x000249CC File Offset: 0x00022BCC
		public static int ToInt32(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			if (value == null)
			{
				return 0;
			}
			return ParseNumbers.StringToInt(value.AsSpan(), fromBase, 4096);
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 32-bit unsigned integer.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16.</param>
		/// <returns>A 32-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16.  
		/// -or-  
		/// <paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign.  
		/// -or-  
		/// <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x0600096A RID: 2410 RVA: 0x00024A01 File Offset: 0x00022C01
		[CLSCompliant(false)]
		public static uint ToUInt32(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			if (value == null)
			{
				return 0U;
			}
			return (uint)ParseNumbers.StringToInt(value.AsSpan(), fromBase, 4608);
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 64-bit signed integer.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16.</param>
		/// <returns>A 64-bit signed integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16.  
		/// -or-  
		/// <paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 signed number, is prefixed with a negative sign.  
		/// -or-  
		/// <paramref name="value" /> represents a number that is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x0600096B RID: 2411 RVA: 0x00024A36 File Offset: 0x00022C36
		public static long ToInt64(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			if (value == null)
			{
				return 0L;
			}
			return ParseNumbers.StringToLong(value.AsSpan(), fromBase, 4096);
		}

		/// <summary>Converts the string representation of a number in a specified base to an equivalent 64-bit unsigned integer.</summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <param name="fromBase">The base of the number in <paramref name="value" />, which must be 2, 8, 10, or 16.</param>
		/// <returns>A 64-bit unsigned integer that is equivalent to the number in <paramref name="value" />, or 0 (zero) if <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fromBase" /> is not 2, 8, 10, or 16.  
		/// -or-  
		/// <paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="value" /> is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> contains a character that is not a valid digit in the base specified by <paramref name="fromBase" />. The exception message indicates that there are no digits to convert if the first character in <paramref name="value" /> is invalid; otherwise, the message indicates that <paramref name="value" /> contains invalid trailing characters.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" />, which represents a non-base 10 unsigned number, is prefixed with a negative sign.  
		/// -or-  
		/// <paramref name="value" /> represents a number that is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x0600096C RID: 2412 RVA: 0x00024A6C File Offset: 0x00022C6C
		[CLSCompliant(false)]
		public static ulong ToUInt64(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			if (value == null)
			{
				return 0UL;
			}
			return (ulong)ParseNumbers.StringToLong(value.AsSpan(), fromBase, 4608);
		}

		/// <summary>Converts the value of an 8-bit unsigned integer to its equivalent string representation in a specified base.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <param name="toBase">The base of the return value, which must be 2, 8, 10, or 16.</param>
		/// <returns>The string representation of <paramref name="value" /> in base <paramref name="toBase" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toBase" /> is not 2, 8, 10, or 16.</exception>
		// Token: 0x0600096D RID: 2413 RVA: 0x00024AA2 File Offset: 0x00022CA2
		public static string ToString(byte value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			return ParseNumbers.IntToString((int)value, toBase, -1, ' ', 64);
		}

		/// <summary>Converts the value of a 16-bit signed integer to its equivalent string representation in a specified base.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <param name="toBase">The base of the return value, which must be 2, 8, 10, or 16.</param>
		/// <returns>The string representation of <paramref name="value" /> in base <paramref name="toBase" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toBase" /> is not 2, 8, 10, or 16.</exception>
		// Token: 0x0600096E RID: 2414 RVA: 0x00024ACD File Offset: 0x00022CCD
		public static string ToString(short value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			return ParseNumbers.IntToString((int)value, toBase, -1, ' ', 128);
		}

		/// <summary>Converts the value of a 32-bit signed integer to its equivalent string representation in a specified base.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <param name="toBase">The base of the return value, which must be 2, 8, 10, or 16.</param>
		/// <returns>The string representation of <paramref name="value" /> in base <paramref name="toBase" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toBase" /> is not 2, 8, 10, or 16.</exception>
		// Token: 0x0600096F RID: 2415 RVA: 0x00024AFB File Offset: 0x00022CFB
		public static string ToString(int value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			return ParseNumbers.IntToString(value, toBase, -1, ' ', 0);
		}

		/// <summary>Converts the value of a 64-bit signed integer to its equivalent string representation in a specified base.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <param name="toBase">The base of the return value, which must be 2, 8, 10, or 16.</param>
		/// <returns>The string representation of <paramref name="value" /> in base <paramref name="toBase" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="toBase" /> is not 2, 8, 10, or 16.</exception>
		// Token: 0x06000970 RID: 2416 RVA: 0x00024B25 File Offset: 0x00022D25
		public static string ToString(long value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException("Invalid Base.");
			}
			return ParseNumbers.LongToString(value, toBase, -1, ' ', 0);
		}

		/// <summary>Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits.</summary>
		/// <param name="inArray">An array of 8-bit unsigned integers.</param>
		/// <returns>The string representation, in base 64, of the contents of <paramref name="inArray" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> is <see langword="null" />.</exception>
		// Token: 0x06000971 RID: 2417 RVA: 0x00024B4F File Offset: 0x00022D4F
		public static string ToBase64String(byte[] inArray)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return Convert.ToBase64String(new ReadOnlySpan<byte>(inArray), Base64FormattingOptions.None);
		}

		/// <summary>Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits. A parameter specifies whether to insert line breaks in the return value.</summary>
		/// <param name="inArray">An array of 8-bit unsigned integers.</param>
		/// <param name="options">
		///   <see cref="F:System.Base64FormattingOptions.InsertLineBreaks" /> to insert a line break every 76 characters, or <see cref="F:System.Base64FormattingOptions.None" /> to not insert line breaks.</param>
		/// <returns>The string representation in base 64 of the elements in <paramref name="inArray" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Base64FormattingOptions" /> value.</exception>
		// Token: 0x06000972 RID: 2418 RVA: 0x00024B6B File Offset: 0x00022D6B
		public static string ToBase64String(byte[] inArray, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return Convert.ToBase64String(new ReadOnlySpan<byte>(inArray), options);
		}

		/// <summary>Converts a subset of an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits. Parameters specify the subset as an offset in the input array, and the number of elements in the array to convert.</summary>
		/// <param name="inArray">An array of 8-bit unsigned integers.</param>
		/// <param name="offset">An offset in <paramref name="inArray" />.</param>
		/// <param name="length">The number of elements of <paramref name="inArray" /> to convert.</param>
		/// <returns>The string representation in base 64 of <paramref name="length" /> elements of <paramref name="inArray" />, starting at position <paramref name="offset" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="length" /> is negative.  
		/// -or-  
		/// <paramref name="offset" /> plus <paramref name="length" /> is greater than the length of <paramref name="inArray" />.</exception>
		// Token: 0x06000973 RID: 2419 RVA: 0x00024B87 File Offset: 0x00022D87
		public static string ToBase64String(byte[] inArray, int offset, int length)
		{
			return Convert.ToBase64String(inArray, offset, length, Base64FormattingOptions.None);
		}

		/// <summary>Converts a subset of an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits. Parameters specify the subset as an offset in the input array, the number of elements in the array to convert, and whether to insert line breaks in the return value.</summary>
		/// <param name="inArray">An array of 8-bit unsigned integers.</param>
		/// <param name="offset">An offset in <paramref name="inArray" />.</param>
		/// <param name="length">The number of elements of <paramref name="inArray" /> to convert.</param>
		/// <param name="options">
		///   <see cref="F:System.Base64FormattingOptions.InsertLineBreaks" /> to insert a line break every 76 characters, or <see cref="F:System.Base64FormattingOptions.None" /> to not insert line breaks.</param>
		/// <returns>The string representation in base 64 of <paramref name="length" /> elements of <paramref name="inArray" />, starting at position <paramref name="offset" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="length" /> is negative.  
		/// -or-  
		/// <paramref name="offset" /> plus <paramref name="length" /> is greater than the length of <paramref name="inArray" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Base64FormattingOptions" /> value.</exception>
		// Token: 0x06000974 RID: 2420 RVA: 0x00024B94 File Offset: 0x00022D94
		public static string ToBase64String(byte[] inArray, int offset, int length, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Value must be positive.");
			}
			if (offset > inArray.Length - length)
			{
				throw new ArgumentOutOfRangeException("offset", "Offset and length must refer to a position in the string.");
			}
			return Convert.ToBase64String(new ReadOnlySpan<byte>(inArray, offset, length), options);
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x00024C00 File Offset: 0x00022E00
		public unsafe static string ToBase64String(ReadOnlySpan<byte> bytes, Base64FormattingOptions options = Base64FormattingOptions.None)
		{
			if (options < Base64FormattingOptions.None || options > Base64FormattingOptions.InsertLineBreaks)
			{
				throw new ArgumentException(string.Format("Illegal enum value: {0}.", (int)options), "options");
			}
			if (bytes.Length == 0)
			{
				return string.Empty;
			}
			bool insertLineBreaks = options == Base64FormattingOptions.InsertLineBreaks;
			string text = string.FastAllocateString(Convert.ToBase64_CalculateAndValidateOutputLength(bytes.Length, insertLineBreaks));
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* inData = reference;
				fixed (string text2 = text)
				{
					char* ptr = text2;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					Convert.ConvertToBase64Array(ptr, inData, 0, bytes.Length, insertLineBreaks);
				}
			}
			return text;
		}

		/// <summary>Converts a subset of an 8-bit unsigned integer array to an equivalent subset of a Unicode character array encoded with base-64 digits. Parameters specify the subsets as offsets in the input and output arrays, and the number of elements in the input array to convert.</summary>
		/// <param name="inArray">An input array of 8-bit unsigned integers.</param>
		/// <param name="offsetIn">A position within <paramref name="inArray" />.</param>
		/// <param name="length">The number of elements of <paramref name="inArray" /> to convert.</param>
		/// <param name="outArray">An output array of Unicode characters.</param>
		/// <param name="offsetOut">A position within <paramref name="outArray" />.</param>
		/// <returns>A 32-bit signed integer containing the number of bytes in <paramref name="outArray" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> or <paramref name="outArray" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offsetIn" />, <paramref name="offsetOut" />, or <paramref name="length" /> is negative.  
		/// -or-  
		/// <paramref name="offsetIn" /> plus <paramref name="length" /> is greater than the length of <paramref name="inArray" />.  
		/// -or-  
		/// <paramref name="offsetOut" /> plus the number of elements to return is greater than the length of <paramref name="outArray" />.</exception>
		// Token: 0x06000976 RID: 2422 RVA: 0x00024C89 File Offset: 0x00022E89
		public static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut)
		{
			return Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut, Base64FormattingOptions.None);
		}

		/// <summary>Converts a subset of an 8-bit unsigned integer array to an equivalent subset of a Unicode character array encoded with base-64 digits. Parameters specify the subsets as offsets in the input and output arrays, the number of elements in the input array to convert, and whether line breaks are inserted in the output array.</summary>
		/// <param name="inArray">An input array of 8-bit unsigned integers.</param>
		/// <param name="offsetIn">A position within <paramref name="inArray" />.</param>
		/// <param name="length">The number of elements of <paramref name="inArray" /> to convert.</param>
		/// <param name="outArray">An output array of Unicode characters.</param>
		/// <param name="offsetOut">A position within <paramref name="outArray" />.</param>
		/// <param name="options">
		///   <see cref="F:System.Base64FormattingOptions.InsertLineBreaks" /> to insert a line break every 76 characters, or <see cref="F:System.Base64FormattingOptions.None" /> to not insert line breaks.</param>
		/// <returns>A 32-bit signed integer containing the number of bytes in <paramref name="outArray" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> or <paramref name="outArray" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offsetIn" />, <paramref name="offsetOut" />, or <paramref name="length" /> is negative.  
		/// -or-  
		/// <paramref name="offsetIn" /> plus <paramref name="length" /> is greater than the length of <paramref name="inArray" />.  
		/// -or-  
		/// <paramref name="offsetOut" /> plus the number of elements to return is greater than the length of <paramref name="outArray" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is not a valid <see cref="T:System.Base64FormattingOptions" /> value.</exception>
		// Token: 0x06000977 RID: 2423 RVA: 0x00024C98 File Offset: 0x00022E98
		public unsafe static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (outArray == null)
			{
				throw new ArgumentNullException("outArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (offsetIn < 0)
			{
				throw new ArgumentOutOfRangeException("offsetIn", "Value must be positive.");
			}
			if (offsetOut < 0)
			{
				throw new ArgumentOutOfRangeException("offsetOut", "Value must be positive.");
			}
			if (options < Base64FormattingOptions.None || options > Base64FormattingOptions.InsertLineBreaks)
			{
				throw new ArgumentException(string.Format("Illegal enum value: {0}.", (int)options), "options");
			}
			int num = inArray.Length;
			if (offsetIn > num - length)
			{
				throw new ArgumentOutOfRangeException("offsetIn", "Offset and length must refer to a position in the string.");
			}
			if (num == 0)
			{
				return 0;
			}
			bool insertLineBreaks = options == Base64FormattingOptions.InsertLineBreaks;
			int num2 = outArray.Length;
			int num3 = Convert.ToBase64_CalculateAndValidateOutputLength(length, insertLineBreaks);
			if (offsetOut > num2 - num3)
			{
				throw new ArgumentOutOfRangeException("offsetOut", "Either offset did not refer to a position in the string, or there is an insufficient length of destination character array.");
			}
			int result;
			fixed (char* ptr = &outArray[offsetOut])
			{
				char* outChars = ptr;
				fixed (byte* ptr2 = &inArray[0])
				{
					byte* inData = ptr2;
					result = Convert.ConvertToBase64Array(outChars, inData, offsetIn, length, insertLineBreaks);
				}
			}
			return result;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00024D9C File Offset: 0x00022F9C
		public unsafe static bool TryToBase64Chars(ReadOnlySpan<byte> bytes, Span<char> chars, out int charsWritten, Base64FormattingOptions options = Base64FormattingOptions.None)
		{
			if (options < Base64FormattingOptions.None || options > Base64FormattingOptions.InsertLineBreaks)
			{
				throw new ArgumentException(string.Format("Illegal enum value: {0}.", (int)options), "options");
			}
			if (bytes.Length == 0)
			{
				charsWritten = 0;
				return true;
			}
			bool insertLineBreaks = options == Base64FormattingOptions.InsertLineBreaks;
			if (Convert.ToBase64_CalculateAndValidateOutputLength(bytes.Length, insertLineBreaks) > chars.Length)
			{
				charsWritten = 0;
				return false;
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* outChars = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(bytes))
				{
					byte* inData = reference2;
					charsWritten = Convert.ConvertToBase64Array(outChars, inData, 0, bytes.Length, insertLineBreaks);
					return true;
				}
			}
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00024E24 File Offset: 0x00023024
		private unsafe static int ConvertToBase64Array(char* outChars, byte* inData, int offset, int length, bool insertLineBreaks)
		{
			int num = length % 3;
			int num2 = offset + (length - num);
			int num3 = 0;
			int num4 = 0;
			fixed (char* ptr = &Convert.base64Table[0])
			{
				char* ptr2 = ptr;
				int i;
				for (i = offset; i < num2; i += 3)
				{
					if (insertLineBreaks)
					{
						if (num4 == 76)
						{
							outChars[num3++] = '\r';
							outChars[num3++] = '\n';
							num4 = 0;
						}
						num4 += 4;
					}
					outChars[num3] = ptr2[(inData[i] & 252) >> 2];
					outChars[num3 + 1] = ptr2[(int)(inData[i] & 3) << 4 | (inData[i + 1] & 240) >> 4];
					outChars[num3 + 2] = ptr2[(int)(inData[i + 1] & 15) << 2 | (inData[i + 2] & 192) >> 6];
					outChars[num3 + 3] = ptr2[inData[i + 2] & 63];
					num3 += 4;
				}
				i = num2;
				if (insertLineBreaks && num != 0 && num4 == 76)
				{
					outChars[num3++] = '\r';
					outChars[num3++] = '\n';
				}
				if (num != 1)
				{
					if (num == 2)
					{
						outChars[num3] = ptr2[(inData[i] & 252) >> 2];
						outChars[num3 + 1] = ptr2[(int)(inData[i] & 3) << 4 | (inData[i + 1] & 240) >> 4];
						outChars[num3 + 2] = ptr2[(inData[i + 1] & 15) << 2];
						outChars[num3 + 3] = ptr2[64];
						num3 += 4;
					}
				}
				else
				{
					outChars[num3] = ptr2[(inData[i] & 252) >> 2];
					outChars[num3 + 1] = ptr2[(inData[i] & 3) << 4];
					outChars[num3 + 2] = ptr2[64];
					outChars[num3 + 3] = ptr2[64];
					num3 += 4;
				}
			}
			return num3;
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x0002502C File Offset: 0x0002322C
		private static int ToBase64_CalculateAndValidateOutputLength(int inputLength, bool insertLineBreaks)
		{
			long num = (long)inputLength / 3L * 4L;
			num += ((inputLength % 3 != 0) ? 4L : 0L);
			if (num == 0L)
			{
				return 0;
			}
			if (insertLineBreaks)
			{
				long num2 = num / 76L;
				if (num % 76L == 0L)
				{
					num2 -= 1L;
				}
				num += num2 * 2L;
			}
			if (num > 2147483647L)
			{
				throw new OutOfMemoryException();
			}
			return (int)num;
		}

		/// <summary>Converts the specified string, which encodes binary data as base-64 digits, to an equivalent 8-bit unsigned integer array.</summary>
		/// <param name="s">The string to convert.</param>
		/// <returns>An array of 8-bit unsigned integers that is equivalent to <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">The length of <paramref name="s" />, ignoring white-space characters, is not zero or a multiple of 4.  
		///  -or-  
		///  The format of <paramref name="s" /> is invalid. <paramref name="s" /> contains a non-base-64 character, more than two padding characters, or a non-white space-character among the padding characters.</exception>
		// Token: 0x0600097B RID: 2427 RVA: 0x00025084 File Offset: 0x00023284
		public unsafe static byte[] FromBase64String(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return Convert.FromBase64CharPtr(ptr, s.Length);
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x000250BB File Offset: 0x000232BB
		public static bool TryFromBase64String(string s, Span<byte> bytes, out int bytesWritten)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return Convert.TryFromBase64Chars(s.AsSpan(), bytes, out bytesWritten);
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x000250D8 File Offset: 0x000232D8
		public unsafe static bool TryFromBase64Chars(ReadOnlySpan<char> chars, Span<byte> bytes, out int bytesWritten)
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)8], 4);
			bytesWritten = 0;
			while (chars.Length != 0)
			{
				int start;
				int num;
				bool flag = Convert.TryDecodeFromUtf16(chars, bytes, out start, out num);
				bytesWritten += num;
				if (flag)
				{
					return true;
				}
				chars = chars.Slice(start);
				bytes = bytes.Slice(num);
				if (((char)(*chars[0])).IsSpace())
				{
					int num2 = 1;
					while (num2 != chars.Length && ((char)(*chars[num2])).IsSpace())
					{
						num2++;
					}
					chars = chars.Slice(num2);
					if (num % 3 != 0 && chars.Length != 0)
					{
						bytesWritten = 0;
						return false;
					}
				}
				else
				{
					int start2;
					int num3;
					Convert.CopyToTempBufferWithoutWhiteSpace(chars, span, out start2, out num3);
					if ((num3 & 3) != 0)
					{
						bytesWritten = 0;
						return false;
					}
					span = span.Slice(0, num3);
					int num4;
					int num5;
					if (!Convert.TryDecodeFromUtf16(span, bytes, out num4, out num5))
					{
						bytesWritten = 0;
						return false;
					}
					bytesWritten += num5;
					chars = chars.Slice(start2);
					bytes = bytes.Slice(num5);
					if (num5 % 3 != 0)
					{
						for (int i = 0; i < chars.Length; i++)
						{
							if (!((char)(*chars[i])).IsSpace())
							{
								bytesWritten = 0;
								return false;
							}
						}
						return true;
					}
				}
			}
			return true;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0002520C File Offset: 0x0002340C
		private unsafe static void CopyToTempBufferWithoutWhiteSpace(ReadOnlySpan<char> chars, Span<char> tempBuffer, out int consumed, out int charsWritten)
		{
			charsWritten = 0;
			for (int i = 0; i < chars.Length; i++)
			{
				char c = (char)(*chars[i]);
				if (!c.IsSpace())
				{
					int num = charsWritten;
					charsWritten = num + 1;
					*tempBuffer[num] = c;
					if (charsWritten == tempBuffer.Length)
					{
						consumed = i + 1;
						return;
					}
				}
			}
			consumed = chars.Length;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0002526C File Offset: 0x0002346C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsSpace(this char c)
		{
			return c == ' ' || c == '\t' || c == '\r' || c == '\n';
		}

		/// <summary>Converts a subset of a Unicode character array, which encodes binary data as base-64 digits, to an equivalent 8-bit unsigned integer array. Parameters specify the subset in the input array and the number of elements to convert.</summary>
		/// <param name="inArray">A Unicode character array.</param>
		/// <param name="offset">A position within <paramref name="inArray" />.</param>
		/// <param name="length">The number of elements in <paramref name="inArray" /> to convert.</param>
		/// <returns>An array of 8-bit unsigned integers equivalent to <paramref name="length" /> elements at position <paramref name="offset" /> in <paramref name="inArray" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inArray" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> or <paramref name="length" /> is less than 0.  
		/// -or-  
		/// <paramref name="offset" /> plus <paramref name="length" /> indicates a position not within <paramref name="inArray" />.</exception>
		/// <exception cref="T:System.FormatException">The length of <paramref name="inArray" />, ignoring white-space characters, is not zero or a multiple of 4.  
		///  -or-  
		///  The format of <paramref name="inArray" /> is invalid. <paramref name="inArray" /> contains a non-base-64 character, more than two padding characters, or a non-white-space character among the padding characters.</exception>
		// Token: 0x06000980 RID: 2432 RVA: 0x00025284 File Offset: 0x00023484
		public unsafe static byte[] FromBase64CharArray(char[] inArray, int offset, int length)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "Index was out of range. Must be non-negative and less than the size of the collection.");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Value must be positive.");
			}
			if (offset > inArray.Length - length)
			{
				throw new ArgumentOutOfRangeException("offset", "Offset and length must refer to a position in the string.");
			}
			if (inArray.Length == 0)
			{
				return Array.Empty<byte>();
			}
			fixed (char* ptr = &inArray[0])
			{
				return Convert.FromBase64CharPtr(ptr + offset, length);
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00025300 File Offset: 0x00023500
		private unsafe static byte[] FromBase64CharPtr(char* inputPtr, int inputLength)
		{
			while (inputLength > 0)
			{
				int num = (int)inputPtr[inputLength - 1];
				if (num != 32 && num != 10 && num != 13 && num != 9)
				{
					break;
				}
				inputLength--;
			}
			byte[] array = new byte[Convert.FromBase64_ComputeResultLength(inputPtr, inputLength)];
			int num2;
			if (!Convert.TryFromBase64Chars(new ReadOnlySpan<char>((void*)inputPtr, inputLength), array, out num2))
			{
				throw new FormatException("The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or an illegal character among the padding characters.");
			}
			return array;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00025368 File Offset: 0x00023568
		private unsafe static int FromBase64_ComputeResultLength(char* inputPtr, int inputLength)
		{
			char* ptr = inputPtr + inputLength;
			int num = inputLength;
			int num2 = 0;
			while (inputPtr < ptr)
			{
				uint num3 = (uint)(*inputPtr);
				inputPtr++;
				if (num3 <= 32U)
				{
					num--;
				}
				else if (num3 == 61U)
				{
					num--;
					num2++;
				}
			}
			if (num2 != 0)
			{
				if (num2 == 1)
				{
					num2 = 2;
				}
				else
				{
					if (num2 != 2)
					{
						throw new FormatException("The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or an illegal character among the padding characters.");
					}
					num2 = 1;
				}
			}
			return num / 4 * 3 + num2;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000253CC File Offset: 0x000235CC
		// Note: this type is marked as 'beforefieldinit'.
		static Convert()
		{
		}

		// Token: 0x04001084 RID: 4228
		private static readonly sbyte[] s_decodingMap = new sbyte[]
		{
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			62,
			-1,
			-1,
			-1,
			63,
			52,
			53,
			54,
			55,
			56,
			57,
			58,
			59,
			60,
			61,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
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
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17,
			18,
			19,
			20,
			21,
			22,
			23,
			24,
			25,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			26,
			27,
			28,
			29,
			30,
			31,
			32,
			33,
			34,
			35,
			36,
			37,
			38,
			39,
			40,
			41,
			42,
			43,
			44,
			45,
			46,
			47,
			48,
			49,
			50,
			51,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1,
			-1
		};

		// Token: 0x04001085 RID: 4229
		private const byte EncodingPad = 61;

		// Token: 0x04001086 RID: 4230
		internal static readonly Type[] ConvertTypes = new Type[]
		{
			typeof(Empty),
			typeof(object),
			typeof(DBNull),
			typeof(bool),
			typeof(char),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(DateTime),
			typeof(object),
			typeof(string)
		};

		// Token: 0x04001087 RID: 4231
		private static readonly Type EnumType = typeof(Enum);

		// Token: 0x04001088 RID: 4232
		internal static readonly char[] base64Table = new char[]
		{
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'G',
			'H',
			'I',
			'J',
			'K',
			'L',
			'M',
			'N',
			'O',
			'P',
			'Q',
			'R',
			'S',
			'T',
			'U',
			'V',
			'W',
			'X',
			'Y',
			'Z',
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'i',
			'j',
			'k',
			'l',
			'm',
			'n',
			'o',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z',
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'+',
			'/',
			'='
		};

		// Token: 0x04001089 RID: 4233
		private const int base64LineBreakPosition = 76;

		/// <summary>A constant that represents a database column that is absent of data; that is, database null.</summary>
		// Token: 0x0400108A RID: 4234
		public static readonly object DBNull = System.DBNull.Value;
	}
}
