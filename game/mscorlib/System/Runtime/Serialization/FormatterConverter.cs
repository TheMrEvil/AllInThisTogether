using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Runtime.Serialization
{
	/// <summary>Represents a base implementation of the <see cref="T:System.Runtime.Serialization.IFormatterConverter" /> interface that uses the <see cref="T:System.Convert" /> class and the <see cref="T:System.IConvertible" /> interface.</summary>
	// Token: 0x02000651 RID: 1617
	public class FormatterConverter : IFormatterConverter
	{
		/// <summary>Converts a value to the given <see cref="T:System.Type" />.</summary>
		/// <param name="value">The object to convert.</param>
		/// <param name="type">The <see cref="T:System.Type" /> into which <paramref name="value" /> is converted.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C87 RID: 15495 RVA: 0x000D1911 File Offset: 0x000CFB11
		public object Convert(object value, Type type)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return System.Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to the given <see cref="T:System.TypeCode" />.</summary>
		/// <param name="value">The object to convert.</param>
		/// <param name="typeCode">The <see cref="T:System.TypeCode" /> into which <paramref name="value" /> is converted.</param>
		/// <returns>The converted <paramref name="value" />, or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C88 RID: 15496 RVA: 0x000D1927 File Offset: 0x000CFB27
		public object Convert(object value, TypeCode typeCode)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return System.Convert.ChangeType(value, typeCode, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a <see cref="T:System.Boolean" />.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C89 RID: 15497 RVA: 0x000D193D File Offset: 0x000CFB3D
		public bool ToBoolean(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a Unicode character.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C8A RID: 15498 RVA: 0x000D1952 File Offset: 0x000CFB52
		public char ToChar(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return System.Convert.ToChar(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a <see cref="T:System.SByte" />.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C8B RID: 15499 RVA: 0x000D1967 File Offset: 0x000CFB67
		[CLSCompliant(false)]
		public sbyte ToSByte(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return System.Convert.ToSByte(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to an 8-bit unsigned integer.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C8C RID: 15500 RVA: 0x000D197C File Offset: 0x000CFB7C
		public byte ToByte(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return System.Convert.ToByte(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 16-bit signed integer.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C8D RID: 15501 RVA: 0x000D1991 File Offset: 0x000CFB91
		public short ToInt16(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return System.Convert.ToInt16(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 16-bit unsigned integer.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C8E RID: 15502 RVA: 0x000D19A6 File Offset: 0x000CFBA6
		[CLSCompliant(false)]
		public ushort ToUInt16(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return System.Convert.ToUInt16(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 32-bit signed integer.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C8F RID: 15503 RVA: 0x000D19BB File Offset: 0x000CFBBB
		public int ToInt32(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return System.Convert.ToInt32(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 32-bit unsigned integer.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C90 RID: 15504 RVA: 0x000D19D0 File Offset: 0x000CFBD0
		[CLSCompliant(false)]
		public uint ToUInt32(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return System.Convert.ToUInt32(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 64-bit signed integer.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C91 RID: 15505 RVA: 0x000D19E5 File Offset: 0x000CFBE5
		public long ToInt64(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return System.Convert.ToInt64(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a 64-bit unsigned integer.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C92 RID: 15506 RVA: 0x000D19FA File Offset: 0x000CFBFA
		[CLSCompliant(false)]
		public ulong ToUInt64(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return System.Convert.ToUInt64(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a single-precision floating-point number.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C93 RID: 15507 RVA: 0x000D1A0F File Offset: 0x000CFC0F
		public float ToSingle(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return System.Convert.ToSingle(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a double-precision floating-point number.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C94 RID: 15508 RVA: 0x000D1A24 File Offset: 0x000CFC24
		public double ToDouble(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C95 RID: 15509 RVA: 0x000D1A39 File Offset: 0x000CFC39
		public decimal ToDecimal(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return System.Convert.ToDecimal(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts a value to a <see cref="T:System.DateTime" />.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C96 RID: 15510 RVA: 0x000D1A4E File Offset: 0x000CFC4E
		public DateTime ToDateTime(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return System.Convert.ToDateTime(value, CultureInfo.InvariantCulture);
		}

		/// <summary>Converts the specified object to a <see cref="T:System.String" />.</summary>
		/// <param name="value">The object to convert.</param>
		/// <returns>The converted <paramref name="value" /> or <see langword="null" /> if the <paramref name="type" /> parameter is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06003C97 RID: 15511 RVA: 0x000D1A63 File Offset: 0x000CFC63
		public string ToString(object value)
		{
			if (value == null)
			{
				FormatterConverter.ThrowValueNullException();
			}
			return System.Convert.ToString(value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003C98 RID: 15512 RVA: 0x000D1A78 File Offset: 0x000CFC78
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static void ThrowValueNullException()
		{
			throw new ArgumentNullException("value");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.FormatterConverter" /> class.</summary>
		// Token: 0x06003C99 RID: 15513 RVA: 0x0000259F File Offset: 0x0000079F
		public FormatterConverter()
		{
		}
	}
}
