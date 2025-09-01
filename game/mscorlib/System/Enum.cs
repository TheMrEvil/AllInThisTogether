using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System
{
	/// <summary>Provides the base class for enumerations.</summary>
	// Token: 0x020001F5 RID: 501
	[ComVisible(true)]
	[Serializable]
	public abstract class Enum : ValueType, IComparable, IFormattable, IConvertible
	{
		// Token: 0x06001575 RID: 5493 RVA: 0x0005500C File Offset: 0x0005320C
		[SecuritySafeCritical]
		private static Enum.ValuesAndNames GetCachedValuesAndNames(RuntimeType enumType, bool getNames)
		{
			Enum.ValuesAndNames valuesAndNames = enumType.GenericCache as Enum.ValuesAndNames;
			if (valuesAndNames == null || (getNames && valuesAndNames.Names == null))
			{
				ulong[] array = null;
				string[] array2 = null;
				if (!Enum.GetEnumValuesAndNames(enumType, out array, out array2))
				{
					Array.Sort<ulong, string>(array, array2, Comparer<ulong>.Default);
				}
				valuesAndNames = new Enum.ValuesAndNames(array, array2);
				enumType.GenericCache = valuesAndNames;
			}
			return valuesAndNames;
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00055060 File Offset: 0x00053260
		private static string InternalFormattedHexString(object value)
		{
			switch (Convert.GetTypeCode(value))
			{
			case TypeCode.Boolean:
				return Convert.ToByte((bool)value).ToString("X2", null);
			case TypeCode.Char:
				return ((ushort)((char)value)).ToString("X4", null);
			case TypeCode.SByte:
				return ((byte)((sbyte)value)).ToString("X2", null);
			case TypeCode.Byte:
				return ((byte)value).ToString("X2", null);
			case TypeCode.Int16:
				return ((ushort)((short)value)).ToString("X4", null);
			case TypeCode.UInt16:
				return ((ushort)value).ToString("X4", null);
			case TypeCode.Int32:
				return ((uint)((int)value)).ToString("X8", null);
			case TypeCode.UInt32:
				return ((uint)value).ToString("X8", null);
			case TypeCode.Int64:
				return ((ulong)((long)value)).ToString("X16", null);
			case TypeCode.UInt64:
				return ((ulong)value).ToString("X16", null);
			default:
				throw new InvalidOperationException(Environment.GetResourceString("Unknown enum type."));
			}
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x00055198 File Offset: 0x00053398
		private static string InternalFormat(RuntimeType eT, object value)
		{
			if (eT.IsDefined(typeof(FlagsAttribute), false))
			{
				return Enum.InternalFlagsFormat(eT, value);
			}
			string name = Enum.GetName(eT, value);
			if (name == null)
			{
				return value.ToString();
			}
			return name;
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x000551D4 File Offset: 0x000533D4
		private static string InternalFlagsFormat(RuntimeType eT, object value)
		{
			ulong num = Enum.ToUInt64(value);
			Enum.ValuesAndNames cachedValuesAndNames = Enum.GetCachedValuesAndNames(eT, true);
			string[] names = cachedValuesAndNames.Names;
			ulong[] values = cachedValuesAndNames.Values;
			int num2 = values.Length - 1;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			ulong num3 = num;
			while (num2 >= 0 && (num2 != 0 || values[num2] != 0UL))
			{
				if ((num & values[num2]) == values[num2])
				{
					num -= values[num2];
					if (!flag)
					{
						stringBuilder.Insert(0, ", ");
					}
					stringBuilder.Insert(0, names[num2]);
					flag = false;
				}
				num2--;
			}
			if (num != 0UL)
			{
				return value.ToString();
			}
			if (num3 != 0UL)
			{
				return stringBuilder.ToString();
			}
			if (values.Length != 0 && values[0] == 0UL)
			{
				return names[0];
			}
			return "0";
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0005527C File Offset: 0x0005347C
		internal static ulong ToUInt64(object value)
		{
			ulong result;
			switch (Convert.GetTypeCode(value))
			{
			case TypeCode.Boolean:
			case TypeCode.Char:
			case TypeCode.Byte:
			case TypeCode.UInt16:
			case TypeCode.UInt32:
			case TypeCode.UInt64:
				result = Convert.ToUInt64(value, CultureInfo.InvariantCulture);
				break;
			case TypeCode.SByte:
			case TypeCode.Int16:
			case TypeCode.Int32:
			case TypeCode.Int64:
				result = (ulong)Convert.ToInt64(value, CultureInfo.InvariantCulture);
				break;
			default:
				throw new InvalidOperationException(Environment.GetResourceString("Unknown enum type."));
			}
			return result;
		}

		// Token: 0x0600157A RID: 5498
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int InternalCompareTo(object o1, object o2);

		// Token: 0x0600157B RID: 5499
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType InternalGetUnderlyingType(RuntimeType enumType);

		// Token: 0x0600157C RID: 5500
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetEnumValuesAndNames(RuntimeType enumType, out ulong[] values, out string[] names);

		// Token: 0x0600157D RID: 5501
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object InternalBoxEnum(RuntimeType enumType, long value);

		/// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object. The return value indicates whether the conversion succeeded.</summary>
		/// <param name="value">The case-sensitive string representation of the enumeration name or underlying value to convert.</param>
		/// <param name="result">When this method returns, <paramref name="result" /> contains an object of type TEnum whose value is represented by <paramref name="value" /> if the parse operation succeeds. If the parse operation fails, <paramref name="result" /> contains the default value of the underlying type of TEnum. Note that this value need not be a member of the TEnum enumeration. This parameter is passed uninitialized.</param>
		/// <typeparam name="TEnum">The enumeration type to which to convert <paramref name="value" />.</typeparam>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="TEnum" /> is not an enumeration type.</exception>
		// Token: 0x0600157E RID: 5502 RVA: 0x000552EF File Offset: 0x000534EF
		public static bool TryParse<TEnum>(string value, out TEnum result) where TEnum : struct
		{
			return Enum.TryParse<TEnum>(value, false, out result);
		}

		/// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object. A parameter specifies whether the operation is case-sensitive. The return value indicates whether the conversion succeeded.</summary>
		/// <param name="value">The string representation of the enumeration name or underlying value to convert.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to ignore case; <see langword="false" /> to consider case.</param>
		/// <param name="result">When this method returns, <paramref name="result" /> contains an object of type TEnum whose value is represented by <paramref name="value" /> if the parse operation succeeds. If the parse operation fails, <paramref name="result" /> contains the default value of the underlying type of TEnum. Note that this value need not be a member of the TEnum enumeration. This parameter is passed uninitialized.</param>
		/// <typeparam name="TEnum">The enumeration type to which to convert <paramref name="value" />.</typeparam>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="TEnum" /> is not an enumeration type.</exception>
		// Token: 0x0600157F RID: 5503 RVA: 0x000552FC File Offset: 0x000534FC
		public static bool TryParse<TEnum>(string value, bool ignoreCase, out TEnum result) where TEnum : struct
		{
			result = default(TEnum);
			Enum.EnumResult enumResult = default(Enum.EnumResult);
			enumResult.Init(false);
			bool flag = Enum.TryParseEnum(typeof(TEnum), value, ignoreCase, ref enumResult);
			if (flag)
			{
				result = (TEnum)((object)enumResult.parsedEnum);
			}
			return flag;
		}

		/// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object.</summary>
		/// <param name="enumType">An enumeration type.</param>
		/// <param name="value">A string containing the name or value to convert.</param>
		/// <returns>An object of type <paramref name="enumType" /> whose value is represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.  
		/// -or-  
		/// <paramref name="value" /> is either an empty string or only contains white space.  
		/// -or-  
		/// <paramref name="value" /> is a name, but not one of the named constants defined for the enumeration.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is outside the range of the underlying type of <paramref name="enumType" />.</exception>
		// Token: 0x06001580 RID: 5504 RVA: 0x00055347 File Offset: 0x00053547
		[ComVisible(true)]
		public static object Parse(Type enumType, string value)
		{
			return Enum.Parse(enumType, value, false);
		}

		/// <summary>Converts the string representation of the name or numeric value of one or more enumerated constants to an equivalent enumerated object. A parameter specifies whether the operation is case-insensitive.</summary>
		/// <param name="enumType">An enumeration type.</param>
		/// <param name="value">A string containing the name or value to convert.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to ignore case; <see langword="false" /> to regard case.</param>
		/// <returns>An object of type <paramref name="enumType" /> whose value is represented by <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.  
		/// -or-  
		/// <paramref name="value" /> is either an empty string ("") or only contains white space.  
		/// -or-  
		/// <paramref name="value" /> is a name, but not one of the named constants defined for the enumeration.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is outside the range of the underlying type of <paramref name="enumType" />.</exception>
		// Token: 0x06001581 RID: 5505 RVA: 0x00055354 File Offset: 0x00053554
		[ComVisible(true)]
		public static object Parse(Type enumType, string value, bool ignoreCase)
		{
			Enum.EnumResult enumResult = default(Enum.EnumResult);
			enumResult.Init(true);
			if (Enum.TryParseEnum(enumType, value, ignoreCase, ref enumResult))
			{
				return enumResult.parsedEnum;
			}
			throw enumResult.GetEnumParseException();
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x0005538C File Offset: 0x0005358C
		private static bool TryParseEnum(Type enumType, string value, bool ignoreCase, ref Enum.EnumResult parseResult)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			if (value == null)
			{
				parseResult.SetFailure(Enum.ParseFailureKind.ArgumentNull, "value");
				return false;
			}
			value = value.Trim();
			if (value.Length == 0)
			{
				parseResult.SetFailure(Enum.ParseFailureKind.Argument, "Must specify valid information for parsing in the string.", null);
				return false;
			}
			ulong num = 0UL;
			if (char.IsDigit(value[0]) || value[0] == '-' || value[0] == '+')
			{
				Type underlyingType = Enum.GetUnderlyingType(enumType);
				try
				{
					object value2 = Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);
					parseResult.parsedEnum = Enum.ToObject(enumType, value2);
					return true;
				}
				catch (FormatException)
				{
				}
				catch (Exception failure)
				{
					if (parseResult.canThrow)
					{
						throw;
					}
					parseResult.SetFailure(failure);
					return false;
				}
			}
			string[] array = value.Split(Enum.enumSeperatorCharArray);
			Enum.ValuesAndNames cachedValuesAndNames = Enum.GetCachedValuesAndNames(runtimeType, true);
			string[] names = cachedValuesAndNames.Names;
			ulong[] values = cachedValuesAndNames.Values;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
				bool flag = false;
				int j = 0;
				while (j < names.Length)
				{
					if (ignoreCase)
					{
						if (string.Compare(names[j], array[i], StringComparison.OrdinalIgnoreCase) == 0)
						{
							goto IL_158;
						}
					}
					else if (names[j].Equals(array[i]))
					{
						goto IL_158;
					}
					j++;
					continue;
					IL_158:
					ulong num2 = values[j];
					num |= num2;
					flag = true;
					break;
				}
				if (!flag)
				{
					parseResult.SetFailure(Enum.ParseFailureKind.ArgumentWithParameter, "Requested value '{0}' was not found.", value);
					return false;
				}
			}
			bool result;
			try
			{
				parseResult.parsedEnum = Enum.ToObject(enumType, num);
				result = true;
			}
			catch (Exception failure2)
			{
				if (parseResult.canThrow)
				{
					throw;
				}
				parseResult.SetFailure(failure2);
				result = false;
			}
			return result;
		}

		/// <summary>Returns the underlying type of the specified enumeration.</summary>
		/// <param name="enumType">The enumeration whose underlying type will be retrieved.</param>
		/// <returns>The underlying type of <paramref name="enumType" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x06001583 RID: 5507 RVA: 0x00055588 File Offset: 0x00053788
		[ComVisible(true)]
		public static Type GetUnderlyingType(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumUnderlyingType();
		}

		/// <summary>Retrieves an array of the values of the constants in a specified enumeration.</summary>
		/// <param name="enumType">An enumeration type.</param>
		/// <returns>An array that contains the values of the constants in <paramref name="enumType" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method is invoked by reflection in a reflection-only context,  
		///  -or-  
		///  <paramref name="enumType" /> is a type from an assembly loaded in a reflection-only context.</exception>
		// Token: 0x06001584 RID: 5508 RVA: 0x000555A4 File Offset: 0x000537A4
		[ComVisible(true)]
		public static Array GetValues(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumValues();
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x000555C0 File Offset: 0x000537C0
		internal static ulong[] InternalGetValues(RuntimeType enumType)
		{
			return Enum.GetCachedValuesAndNames(enumType, false).Values;
		}

		/// <summary>Retrieves the name of the constant in the specified enumeration that has the specified value.</summary>
		/// <param name="enumType">An enumeration type.</param>
		/// <param name="value">The value of a particular enumerated constant in terms of its underlying type.</param>
		/// <returns>A string containing the name of the enumerated constant in <paramref name="enumType" /> whose value is <paramref name="value" />; or <see langword="null" /> if no such constant is found.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.  
		/// -or-  
		/// <paramref name="value" /> is neither of type <paramref name="enumType" /> nor does it have the same underlying type as <paramref name="enumType" />.</exception>
		// Token: 0x06001586 RID: 5510 RVA: 0x000555CE File Offset: 0x000537CE
		[ComVisible(true)]
		public static string GetName(Type enumType, object value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumName(value);
		}

		/// <summary>Retrieves an array of the names of the constants in a specified enumeration.</summary>
		/// <param name="enumType">An enumeration type.</param>
		/// <returns>A string array of the names of the constants in <paramref name="enumType" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> parameter is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x06001587 RID: 5511 RVA: 0x000555EB File Offset: 0x000537EB
		[ComVisible(true)]
		public static string[] GetNames(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumNames();
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00055607 File Offset: 0x00053807
		internal static string[] InternalGetNames(RuntimeType enumType)
		{
			return Enum.GetCachedValuesAndNames(enumType, true).Names;
		}

		/// <summary>Converts the specified object with an integer value to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value convert to an enumeration member.</param>
		/// <returns>An enumeration object whose value is <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.  
		/// -or-  
		/// <paramref name="value" /> is not type <see cref="T:System.SByte" />, <see cref="T:System.Int16" />, <see cref="T:System.Int32" />, <see cref="T:System.Int64" />, <see cref="T:System.Byte" />, <see cref="T:System.UInt16" />, <see cref="T:System.UInt32" />, or <see cref="T:System.UInt64" />.</exception>
		// Token: 0x06001589 RID: 5513 RVA: 0x00055618 File Offset: 0x00053818
		[ComVisible(true)]
		public static object ToObject(Type enumType, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			switch (Convert.GetTypeCode(value))
			{
			case TypeCode.Boolean:
				return Enum.ToObject(enumType, (bool)value);
			case TypeCode.Char:
				return Enum.ToObject(enumType, (char)value);
			case TypeCode.SByte:
				return Enum.ToObject(enumType, (sbyte)value);
			case TypeCode.Byte:
				return Enum.ToObject(enumType, (byte)value);
			case TypeCode.Int16:
				return Enum.ToObject(enumType, (short)value);
			case TypeCode.UInt16:
				return Enum.ToObject(enumType, (ushort)value);
			case TypeCode.Int32:
				return Enum.ToObject(enumType, (int)value);
			case TypeCode.UInt32:
				return Enum.ToObject(enumType, (uint)value);
			case TypeCode.Int64:
				return Enum.ToObject(enumType, (long)value);
			case TypeCode.UInt64:
				return Enum.ToObject(enumType, (ulong)value);
			default:
				throw new ArgumentException(Environment.GetResourceString("The value passed in must be an enum base or an underlying type for an enum, such as an Int32."), "value");
			}
		}

		/// <summary>Returns a Boolean telling whether a given integral value, or its name as a string, exists in a specified enumeration.</summary>
		/// <param name="enumType">An enumeration type.</param>
		/// <param name="value">The value or name of a constant in <paramref name="enumType" />.</param>
		/// <returns>
		///   <see langword="true" /> if a constant in <paramref name="enumType" /> has a value equal to <paramref name="value" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> or <paramref name="value" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see langword="Enum" />.  
		/// -or-  
		/// The type of <paramref name="value" /> is an enumeration, but it is not an enumeration of type <paramref name="enumType" />.  
		/// -or-  
		/// The type of <paramref name="value" /> is not an underlying type of <paramref name="enumType" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> is not type <see cref="T:System.SByte" />, <see cref="T:System.Int16" />, <see cref="T:System.Int32" />, <see cref="T:System.Int64" />, <see cref="T:System.Byte" />, <see cref="T:System.UInt16" />, <see cref="T:System.UInt32" />, or <see cref="T:System.UInt64" />, or <see cref="T:System.String" />.</exception>
		// Token: 0x0600158A RID: 5514 RVA: 0x00055705 File Offset: 0x00053905
		[ComVisible(true)]
		public static bool IsDefined(Type enumType, object value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.IsEnumDefined(value);
		}

		/// <summary>Converts the specified value of a specified enumerated type to its equivalent string representation according to the specified format.</summary>
		/// <param name="enumType">The enumeration type of the value to convert.</param>
		/// <param name="value">The value to convert.</param>
		/// <param name="format">The output format to use.</param>
		/// <returns>A string representation of <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="enumType" />, <paramref name="value" />, or <paramref name="format" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="enumType" /> parameter is not an <see cref="T:System.Enum" /> type.  
		///  -or-  
		///  The <paramref name="value" /> is from an enumeration that differs in type from <paramref name="enumType" />.  
		///  -or-  
		///  The type of <paramref name="value" /> is not an underlying type of <paramref name="enumType" />.</exception>
		/// <exception cref="T:System.FormatException">The <paramref name="format" /> parameter contains an invalid value.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="format" /> equals "X", but the enumeration type is unknown.</exception>
		// Token: 0x0600158B RID: 5515 RVA: 0x00055724 File Offset: 0x00053924
		[ComVisible(true)]
		public static string Format(Type enumType, object value, string format)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			Type type = value.GetType();
			Type underlyingType = Enum.GetUnderlyingType(enumType);
			if (type.IsEnum)
			{
				Type underlyingType2 = Enum.GetUnderlyingType(type);
				if (!type.IsEquivalentTo(enumType))
				{
					throw new ArgumentException(Environment.GetResourceString("Object must be the same type as the enum. The type passed in was '{0}'; the enum type was '{1}'.", new object[]
					{
						type.ToString(),
						enumType.ToString()
					}));
				}
				value = ((Enum)value).GetValue();
			}
			else if (type != underlyingType)
			{
				throw new ArgumentException(Environment.GetResourceString("Enum underlying type and the object must be same type or object. Type passed in was '{0}'; the enum underlying type was '{1}'.", new object[]
				{
					type.ToString(),
					underlyingType.ToString()
				}));
			}
			if (format.Length != 1)
			{
				throw new FormatException(Environment.GetResourceString("Format String can be only \"G\", \"g\", \"X\", \"x\", \"F\", \"f\", \"D\" or \"d\"."));
			}
			char c = format[0];
			if (c == 'D' || c == 'd')
			{
				return value.ToString();
			}
			if (c == 'X' || c == 'x')
			{
				return Enum.InternalFormattedHexString(value);
			}
			if (c == 'G' || c == 'g')
			{
				return Enum.InternalFormat(runtimeType, value);
			}
			if (c == 'F' || c == 'f')
			{
				return Enum.InternalFlagsFormat(runtimeType, value);
			}
			throw new FormatException(Environment.GetResourceString("Format String can be only \"G\", \"g\", \"X\", \"x\", \"F\", \"f\", \"D\" or \"d\"."));
		}

		// Token: 0x0600158C RID: 5516
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object get_value();

		// Token: 0x0600158D RID: 5517 RVA: 0x000558AB File Offset: 0x00053AAB
		[SecuritySafeCritical]
		internal object GetValue()
		{
			return this.get_value();
		}

		// Token: 0x0600158E RID: 5518
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool InternalHasFlag(Enum flags);

		// Token: 0x0600158F RID: 5519
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int get_hashcode();

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <param name="obj">An object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an enumeration value of the same type and with the same underlying value as this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001590 RID: 5520 RVA: 0x000558B3 File Offset: 0x00053AB3
		public override bool Equals(object obj)
		{
			return ValueType.DefaultEquals(this, obj);
		}

		/// <summary>Returns the hash code for the value of this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001591 RID: 5521 RVA: 0x000558BC File Offset: 0x00053ABC
		[SecuritySafeCritical]
		public override int GetHashCode()
		{
			return this.get_hashcode();
		}

		/// <summary>Converts the value of this instance to its equivalent string representation.</summary>
		/// <returns>The string representation of the value of this instance.</returns>
		// Token: 0x06001592 RID: 5522 RVA: 0x000558C4 File Offset: 0x00053AC4
		public override string ToString()
		{
			return Enum.InternalFormat((RuntimeType)base.GetType(), this.GetValue());
		}

		/// <summary>This method overload is obsolete; use <see cref="M:System.Enum.ToString(System.String)" />.</summary>
		/// <param name="format">A format specification.</param>
		/// <param name="provider">(Obsolete.)</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> does not contain a valid format specification.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="format" /> equals "X", but the enumeration type is unknown.</exception>
		// Token: 0x06001593 RID: 5523 RVA: 0x000558DC File Offset: 0x00053ADC
		[Obsolete("The provider argument is not used. Please use ToString(String).")]
		public string ToString(string format, IFormatProvider provider)
		{
			return this.ToString(format);
		}

		/// <summary>Compares this instance to a specified object and returns an indication of their relative values.</summary>
		/// <param name="target">An object to compare, or <see langword="null" />.</param>
		/// <returns>A signed number that indicates the relative values of this instance and <paramref name="target" />.  
		///   Value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///   The value of this instance is less than the value of <paramref name="target" />.  
		///
		///   Zero  
		///
		///   The value of this instance is equal to the value of <paramref name="target" />.  
		///
		///   Greater than zero  
		///
		///   The value of this instance is greater than the value of <paramref name="target" />.  
		///
		///  -or-  
		///
		///  <paramref name="target" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> and this instance are not the same type.</exception>
		/// <exception cref="T:System.InvalidOperationException">This instance is not type <see cref="T:System.SByte" />, <see cref="T:System.Int16" />, <see cref="T:System.Int32" />, <see cref="T:System.Int64" />, <see cref="T:System.Byte" />, <see cref="T:System.UInt16" />, <see cref="T:System.UInt32" />, or <see cref="T:System.UInt64" />.</exception>
		/// <exception cref="T:System.NullReferenceException">This instance is null.</exception>
		// Token: 0x06001594 RID: 5524 RVA: 0x000558E8 File Offset: 0x00053AE8
		[SecuritySafeCritical]
		public int CompareTo(object target)
		{
			if (this == null)
			{
				throw new NullReferenceException();
			}
			int num = Enum.InternalCompareTo(this, target);
			if (num < 2)
			{
				return num;
			}
			if (num == 2)
			{
				Type type = base.GetType();
				Type type2 = target.GetType();
				throw new ArgumentException(Environment.GetResourceString("Object must be the same type as the enum. The type passed in was '{0}'; the enum type was '{1}'.", new object[]
				{
					type2.ToString(),
					type.ToString()
				}));
			}
			throw new InvalidOperationException(Environment.GetResourceString("Unknown enum type."));
		}

		/// <summary>Converts the value of this instance to its equivalent string representation using the specified format.</summary>
		/// <param name="format">A format string.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> contains an invalid specification.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="format" /> equals "X", but the enumeration type is unknown.</exception>
		// Token: 0x06001595 RID: 5525 RVA: 0x00055958 File Offset: 0x00053B58
		public string ToString(string format)
		{
			if (format == null || format.Length == 0)
			{
				format = "G";
			}
			if (string.Compare(format, "G", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return this.ToString();
			}
			if (string.Compare(format, "D", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return this.GetValue().ToString();
			}
			if (string.Compare(format, "X", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return Enum.InternalFormattedHexString(this.GetValue());
			}
			if (string.Compare(format, "F", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return Enum.InternalFlagsFormat((RuntimeType)base.GetType(), this.GetValue());
			}
			throw new FormatException(Environment.GetResourceString("Format String can be only \"G\", \"g\", \"X\", \"x\", \"F\", \"f\", \"D\" or \"d\"."));
		}

		/// <summary>This method overload is obsolete; use <see cref="M:System.Enum.ToString" />.</summary>
		/// <param name="provider">(obsolete)</param>
		/// <returns>The string representation of the value of this instance.</returns>
		// Token: 0x06001596 RID: 5526 RVA: 0x000559F4 File Offset: 0x00053BF4
		[Obsolete("The provider argument is not used. Please use ToString().")]
		public string ToString(IFormatProvider provider)
		{
			return this.ToString();
		}

		/// <summary>Determines whether one or more bit fields are set in the current instance.</summary>
		/// <param name="flag">An enumeration value.</param>
		/// <returns>
		///   <see langword="true" /> if the bit field or bit fields that are set in <paramref name="flag" /> are also set in the current instance; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="flag" /> is a different type than the current instance.</exception>
		// Token: 0x06001597 RID: 5527 RVA: 0x000559FC File Offset: 0x00053BFC
		[SecuritySafeCritical]
		public bool HasFlag(Enum flag)
		{
			if (flag == null)
			{
				throw new ArgumentNullException("flag");
			}
			if (!base.GetType().IsEquivalentTo(flag.GetType()))
			{
				throw new ArgumentException(Environment.GetResourceString("The argument type, '{0}', is not the same as the enum type '{1}'.", new object[]
				{
					flag.GetType(),
					base.GetType()
				}));
			}
			return this.InternalHasFlag(flag);
		}

		/// <summary>Returns the type code of the underlying type of this enumeration member.</summary>
		/// <returns>The type code of the underlying type of this instance.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumeration type is unknown.</exception>
		// Token: 0x06001598 RID: 5528 RVA: 0x00055A5C File Offset: 0x00053C5C
		public TypeCode GetTypeCode()
		{
			Type underlyingType = Enum.GetUnderlyingType(base.GetType());
			if (underlyingType == typeof(int))
			{
				return TypeCode.Int32;
			}
			if (underlyingType == typeof(sbyte))
			{
				return TypeCode.SByte;
			}
			if (underlyingType == typeof(short))
			{
				return TypeCode.Int16;
			}
			if (underlyingType == typeof(long))
			{
				return TypeCode.Int64;
			}
			if (underlyingType == typeof(uint))
			{
				return TypeCode.UInt32;
			}
			if (underlyingType == typeof(byte))
			{
				return TypeCode.Byte;
			}
			if (underlyingType == typeof(ushort))
			{
				return TypeCode.UInt16;
			}
			if (underlyingType == typeof(ulong))
			{
				return TypeCode.UInt64;
			}
			if (underlyingType == typeof(bool))
			{
				return TypeCode.Boolean;
			}
			if (underlyingType == typeof(char))
			{
				return TypeCode.Char;
			}
			throw new InvalidOperationException(Environment.GetResourceString("Unknown enum type."));
		}

		/// <summary>Converts the current value to a Boolean value based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>This member always throws an exception.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06001599 RID: 5529 RVA: 0x00055B50 File Offset: 0x00053D50
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a Unicode character based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>This member always throws an exception.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x0600159A RID: 5530 RVA: 0x00055B62 File Offset: 0x00053D62
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to an 8-bit signed integer based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x0600159B RID: 5531 RVA: 0x00055B74 File Offset: 0x00053D74
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to an 8-bit unsigned integer based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x0600159C RID: 5532 RVA: 0x00055B86 File Offset: 0x00053D86
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 16-bit signed integer based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x0600159D RID: 5533 RVA: 0x00055B98 File Offset: 0x00053D98
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 16-bit unsigned integer based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x0600159E RID: 5534 RVA: 0x00055BAA File Offset: 0x00053DAA
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 32-bit signed integer based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x0600159F RID: 5535 RVA: 0x00055BBC File Offset: 0x00053DBC
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 32-bit unsigned integer based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x060015A0 RID: 5536 RVA: 0x00055BCE File Offset: 0x00053DCE
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 64-bit signed integer based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x060015A1 RID: 5537 RVA: 0x00055BE0 File Offset: 0x00053DE0
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a 64-bit unsigned integer based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x060015A2 RID: 5538 RVA: 0x00055BF2 File Offset: 0x00053DF2
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a single-precision floating-point number based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>This member always throws an exception.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x060015A3 RID: 5539 RVA: 0x00055C04 File Offset: 0x00053E04
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a double-precision floating point number based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>This member always throws an exception.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x060015A4 RID: 5540 RVA: 0x00055C16 File Offset: 0x00053E16
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a <see cref="T:System.Decimal" /> based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>This member always throws an exception.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x060015A5 RID: 5541 RVA: 0x00055C28 File Offset: 0x00053E28
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this.GetValue(), CultureInfo.CurrentCulture);
		}

		/// <summary>Converts the current value to a <see cref="T:System.DateTime" /> based on the underlying type.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>This member always throws an exception.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x060015A6 RID: 5542 RVA: 0x00055C3A File Offset: 0x00053E3A
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(Environment.GetResourceString("Invalid cast from '{0}' to '{1}'.", new object[]
			{
				"Enum",
				"DateTime"
			}));
		}

		/// <summary>Converts the current value to a specified type based on the underlying type.</summary>
		/// <param name="type">The type to convert to.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The converted value.</returns>
		// Token: 0x060015A7 RID: 5543 RVA: 0x0001931A File Offset: 0x0001751A
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		/// <summary>Converts the specified 8-bit signed integer value to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value to convert to an enumeration member.</param>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x060015A8 RID: 5544 RVA: 0x00055C64 File Offset: 0x00053E64
		[ComVisible(true)]
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		public static object ToObject(Type enumType, sbyte value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)value);
		}

		/// <summary>Converts the specified 16-bit signed integer to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value to convert to an enumeration member.</param>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x060015A9 RID: 5545 RVA: 0x00055CD0 File Offset: 0x00053ED0
		[ComVisible(true)]
		[SecuritySafeCritical]
		public static object ToObject(Type enumType, short value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)value);
		}

		/// <summary>Converts the specified 32-bit signed integer to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value to convert to an enumeration member.</param>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x060015AA RID: 5546 RVA: 0x00055D3C File Offset: 0x00053F3C
		[ComVisible(true)]
		[SecuritySafeCritical]
		public static object ToObject(Type enumType, int value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)value);
		}

		/// <summary>Converts the specified 8-bit unsigned integer to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value to convert to an enumeration member.</param>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x060015AB RID: 5547 RVA: 0x00055DA8 File Offset: 0x00053FA8
		[ComVisible(true)]
		[SecuritySafeCritical]
		public static object ToObject(Type enumType, byte value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)((ulong)value));
		}

		/// <summary>Converts the specified 16-bit unsigned integer value to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value to convert to an enumeration member.</param>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x060015AC RID: 5548 RVA: 0x00055E14 File Offset: 0x00054014
		[CLSCompliant(false)]
		[ComVisible(true)]
		[SecuritySafeCritical]
		public static object ToObject(Type enumType, ushort value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)((ulong)value));
		}

		/// <summary>Converts the specified 32-bit unsigned integer value to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value to convert to an enumeration member.</param>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x060015AD RID: 5549 RVA: 0x00055E80 File Offset: 0x00054080
		[SecuritySafeCritical]
		[CLSCompliant(false)]
		[ComVisible(true)]
		public static object ToObject(Type enumType, uint value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)((ulong)value));
		}

		/// <summary>Converts the specified 64-bit signed integer to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value to convert to an enumeration member.</param>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x060015AE RID: 5550 RVA: 0x00055EEC File Offset: 0x000540EC
		[ComVisible(true)]
		[SecuritySafeCritical]
		public static object ToObject(Type enumType, long value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, value);
		}

		/// <summary>Converts the specified 64-bit unsigned integer value to an enumeration member.</summary>
		/// <param name="enumType">The enumeration type to return.</param>
		/// <param name="value">The value to convert to an enumeration member.</param>
		/// <returns>An instance of the enumeration set to <paramref name="value" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="enumType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="enumType" /> is not an <see cref="T:System.Enum" />.</exception>
		// Token: 0x060015AF RID: 5551 RVA: 0x00055F54 File Offset: 0x00054154
		[ComVisible(true)]
		[CLSCompliant(false)]
		[SecuritySafeCritical]
		public static object ToObject(Type enumType, ulong value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)value);
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00055FBC File Offset: 0x000541BC
		[SecuritySafeCritical]
		private static object ToObject(Type enumType, char value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, (long)((ulong)value));
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x00056028 File Offset: 0x00054228
		[SecuritySafeCritical]
		private static object ToObject(Type enumType, bool value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Type must be a type provided by the runtime."), "enumType");
			}
			return Enum.InternalBoxEnum(runtimeType, value ? 1L : 0L);
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00056097 File Offset: 0x00054297
		public static TEnum Parse<TEnum>(string value) where TEnum : struct
		{
			return Enum.Parse<TEnum>(value, false);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x000560A0 File Offset: 0x000542A0
		public static TEnum Parse<TEnum>(string value, bool ignoreCase) where TEnum : struct
		{
			Enum.EnumResult enumResult = new Enum.EnumResult
			{
				canThrow = true
			};
			if (Enum.TryParseEnum(typeof(TEnum), value, ignoreCase, ref enumResult))
			{
				return (TEnum)((object)enumResult.parsedEnum);
			}
			throw enumResult.GetEnumParseException();
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x000560E8 File Offset: 0x000542E8
		public static bool TryParse(Type enumType, string value, bool ignoreCase, out object result)
		{
			result = null;
			Enum.EnumResult enumResult = default(Enum.EnumResult);
			bool flag = Enum.TryParseEnum(enumType, value, ignoreCase, ref enumResult);
			if (flag)
			{
				result = enumResult.parsedEnum;
			}
			return flag;
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00056115 File Offset: 0x00054315
		public static bool TryParse(Type enumType, string value, out object result)
		{
			return Enum.TryParse(enumType, value, false, out result);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Enum" /> class.</summary>
		// Token: 0x060015B6 RID: 5558 RVA: 0x00056120 File Offset: 0x00054320
		protected Enum()
		{
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00056128 File Offset: 0x00054328
		// Note: this type is marked as 'beforefieldinit'.
		static Enum()
		{
		}

		// Token: 0x0400150E RID: 5390
		private static readonly char[] enumSeperatorCharArray = new char[]
		{
			','
		};

		// Token: 0x0400150F RID: 5391
		private const string enumSeperator = ", ";

		// Token: 0x020001F6 RID: 502
		private enum ParseFailureKind
		{
			// Token: 0x04001511 RID: 5393
			None,
			// Token: 0x04001512 RID: 5394
			Argument,
			// Token: 0x04001513 RID: 5395
			ArgumentNull,
			// Token: 0x04001514 RID: 5396
			ArgumentWithParameter,
			// Token: 0x04001515 RID: 5397
			UnhandledException
		}

		// Token: 0x020001F7 RID: 503
		private struct EnumResult
		{
			// Token: 0x060015B8 RID: 5560 RVA: 0x0005613A File Offset: 0x0005433A
			internal void Init(bool canMethodThrow)
			{
				this.parsedEnum = 0;
				this.canThrow = canMethodThrow;
			}

			// Token: 0x060015B9 RID: 5561 RVA: 0x0005614F File Offset: 0x0005434F
			internal void SetFailure(Exception unhandledException)
			{
				this.m_failure = Enum.ParseFailureKind.UnhandledException;
				this.m_innerException = unhandledException;
			}

			// Token: 0x060015BA RID: 5562 RVA: 0x0005615F File Offset: 0x0005435F
			internal void SetFailure(Enum.ParseFailureKind failure, string failureParameter)
			{
				this.m_failure = failure;
				this.m_failureParameter = failureParameter;
				if (this.canThrow)
				{
					throw this.GetEnumParseException();
				}
			}

			// Token: 0x060015BB RID: 5563 RVA: 0x0005617E File Offset: 0x0005437E
			internal void SetFailure(Enum.ParseFailureKind failure, string failureMessageID, object failureMessageFormatArgument)
			{
				this.m_failure = failure;
				this.m_failureMessageID = failureMessageID;
				this.m_failureMessageFormatArgument = failureMessageFormatArgument;
				if (this.canThrow)
				{
					throw this.GetEnumParseException();
				}
			}

			// Token: 0x060015BC RID: 5564 RVA: 0x000561A4 File Offset: 0x000543A4
			internal Exception GetEnumParseException()
			{
				switch (this.m_failure)
				{
				case Enum.ParseFailureKind.Argument:
					return new ArgumentException(Environment.GetResourceString(this.m_failureMessageID));
				case Enum.ParseFailureKind.ArgumentNull:
					return new ArgumentNullException(this.m_failureParameter);
				case Enum.ParseFailureKind.ArgumentWithParameter:
					return new ArgumentException(Environment.GetResourceString(this.m_failureMessageID, new object[]
					{
						this.m_failureMessageFormatArgument
					}));
				case Enum.ParseFailureKind.UnhandledException:
					return this.m_innerException;
				default:
					return new ArgumentException(Environment.GetResourceString("Requested value '{0}' was not found."));
				}
			}

			// Token: 0x04001516 RID: 5398
			internal object parsedEnum;

			// Token: 0x04001517 RID: 5399
			internal bool canThrow;

			// Token: 0x04001518 RID: 5400
			internal Enum.ParseFailureKind m_failure;

			// Token: 0x04001519 RID: 5401
			internal string m_failureMessageID;

			// Token: 0x0400151A RID: 5402
			internal string m_failureParameter;

			// Token: 0x0400151B RID: 5403
			internal object m_failureMessageFormatArgument;

			// Token: 0x0400151C RID: 5404
			internal Exception m_innerException;
		}

		// Token: 0x020001F8 RID: 504
		private class ValuesAndNames
		{
			// Token: 0x060015BD RID: 5565 RVA: 0x00056225 File Offset: 0x00054425
			public ValuesAndNames(ulong[] values, string[] names)
			{
				this.Values = values;
				this.Names = names;
			}

			// Token: 0x0400151D RID: 5405
			public ulong[] Values;

			// Token: 0x0400151E RID: 5406
			public string[] Names;
		}
	}
}
