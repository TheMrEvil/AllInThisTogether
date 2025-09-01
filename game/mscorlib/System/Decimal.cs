using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents a decimal floating-point number.</summary>
	// Token: 0x02000275 RID: 629
	[Serializable]
	[StructLayout(LayoutKind.Explicit)]
	public readonly struct Decimal : IFormattable, IComparable, IConvertible, IComparable<decimal>, IEquatable<decimal>, IDeserializationCallback, ISpanFormattable
	{
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x0006A410 File Offset: 0x00068610
		internal uint High
		{
			get
			{
				return (uint)this.hi;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x0006A418 File Offset: 0x00068618
		internal uint Low
		{
			get
			{
				return (uint)this.lo;
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x0006A420 File Offset: 0x00068620
		internal uint Mid
		{
			get
			{
				return (uint)this.mid;
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x0006A428 File Offset: 0x00068628
		internal bool IsNegative
		{
			get
			{
				return this.flags < 0;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06001C8F RID: 7311 RVA: 0x0006A433 File Offset: 0x00068633
		internal int Scale
		{
			get
			{
				return (int)((byte)(this.flags >> 16));
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x0006A43F File Offset: 0x0006863F
		private ulong Low64
		{
			get
			{
				if (!BitConverter.IsLittleEndian)
				{
					return (ulong)this.Mid << 32 | (ulong)this.Low;
				}
				return this.ulomidLE;
			}
		}

		// Token: 0x06001C91 RID: 7313 RVA: 0x0006A461 File Offset: 0x00068661
		private static ref decimal.DecCalc AsMutable(ref decimal d)
		{
			return Unsafe.As<decimal, decimal.DecCalc>(ref d);
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x0006A469 File Offset: 0x00068669
		internal static uint DecDivMod1E9(ref decimal value)
		{
			return decimal.DecCalc.DecDivMod1E9(decimal.AsMutable(ref value));
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified 32-bit signed integer.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />.</param>
		// Token: 0x06001C93 RID: 7315 RVA: 0x0006A476 File Offset: 0x00068676
		public Decimal(int value)
		{
			if (value >= 0)
			{
				this.flags = 0;
			}
			else
			{
				this.flags = int.MinValue;
				value = -value;
			}
			this.lo = value;
			this.mid = 0;
			this.hi = 0;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified 32-bit unsigned integer.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />.</param>
		// Token: 0x06001C94 RID: 7316 RVA: 0x0006A4A9 File Offset: 0x000686A9
		[CLSCompliant(false)]
		public Decimal(uint value)
		{
			this.flags = 0;
			this.lo = (int)value;
			this.mid = 0;
			this.hi = 0;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified 64-bit signed integer.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />.</param>
		// Token: 0x06001C95 RID: 7317 RVA: 0x0006A4C7 File Offset: 0x000686C7
		public Decimal(long value)
		{
			if (value >= 0L)
			{
				this.flags = 0;
			}
			else
			{
				this.flags = int.MinValue;
				value = -value;
			}
			this.lo = (int)value;
			this.mid = (int)(value >> 32);
			this.hi = 0;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified 64-bit unsigned integer.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />.</param>
		// Token: 0x06001C96 RID: 7318 RVA: 0x0006A500 File Offset: 0x00068700
		[CLSCompliant(false)]
		public Decimal(ulong value)
		{
			this.flags = 0;
			this.lo = (int)value;
			this.mid = (int)(value >> 32);
			this.hi = 0;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified single-precision floating-point number.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.PositiveInfinity" />, or <see cref="F:System.Single.NegativeInfinity" />.</exception>
		// Token: 0x06001C97 RID: 7319 RVA: 0x0006A523 File Offset: 0x00068723
		public Decimal(float value)
		{
			decimal.DecCalc.VarDecFromR4(value, decimal.AsMutable(ref this));
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to the value of the specified double-precision floating-point number.</summary>
		/// <param name="value">The value to represent as a <see cref="T:System.Decimal" />.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" />.</exception>
		// Token: 0x06001C98 RID: 7320 RVA: 0x0006A531 File Offset: 0x00068731
		public Decimal(double value)
		{
			decimal.DecCalc.VarDecFromR8(value, decimal.AsMutable(ref this));
		}

		/// <summary>Converts the specified 64-bit signed integer, which contains an OLE Automation Currency value, to the equivalent <see cref="T:System.Decimal" /> value.</summary>
		/// <param name="cy">An OLE Automation Currency value.</param>
		/// <returns>A <see cref="T:System.Decimal" /> that contains the equivalent of <paramref name="cy" />.</returns>
		// Token: 0x06001C99 RID: 7321 RVA: 0x0006A540 File Offset: 0x00068740
		public static decimal FromOACurrency(long cy)
		{
			bool isNegative = false;
			ulong num;
			if (cy < 0L)
			{
				isNegative = true;
				num = (ulong)(-(ulong)cy);
			}
			else
			{
				num = (ulong)cy;
			}
			int num2 = 4;
			if (num != 0UL)
			{
				while (num2 != 0 && num % 10UL == 0UL)
				{
					num2--;
					num /= 10UL;
				}
			}
			return new decimal((int)num, (int)(num >> 32), 0, isNegative, (byte)num2);
		}

		/// <summary>Converts the specified <see cref="T:System.Decimal" /> value to the equivalent OLE Automation Currency value, which is contained in a 64-bit signed integer.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>A 64-bit signed integer that contains the OLE Automation equivalent of <paramref name="value" />.</returns>
		// Token: 0x06001C9A RID: 7322 RVA: 0x0006A588 File Offset: 0x00068788
		public static long ToOACurrency(decimal value)
		{
			return decimal.DecCalc.VarCyFromDec(decimal.AsMutable(ref value));
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x0006A596 File Offset: 0x00068796
		private static bool IsValid(int flags)
		{
			return (flags & 2130771967) == 0 && (flags & 16711680) <= 1835008;
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> to a decimal value represented in binary and contained in a specified array.</summary>
		/// <param name="bits">An array of 32-bit signed integers containing a representation of a decimal value.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="bits" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The length of the <paramref name="bits" /> is not 4.  
		///  -or-  
		///  The representation of the decimal value in <paramref name="bits" /> is not valid.</exception>
		// Token: 0x06001C9C RID: 7324 RVA: 0x0006A5B4 File Offset: 0x000687B4
		public Decimal(int[] bits)
		{
			if (bits == null)
			{
				throw new ArgumentNullException("bits");
			}
			if (bits.Length == 4)
			{
				int num = bits[3];
				if (decimal.IsValid(num))
				{
					this.lo = bits[0];
					this.mid = bits[1];
					this.hi = bits[2];
					this.flags = num;
					return;
				}
			}
			throw new ArgumentException("Decimal byte array constructor requires an array of length four containing valid decimal bytes.");
		}

		/// <summary>Initializes a new instance of <see cref="T:System.Decimal" /> from parameters specifying the instance's constituent parts.</summary>
		/// <param name="lo">The low 32 bits of a 96-bit integer.</param>
		/// <param name="mid">The middle 32 bits of a 96-bit integer.</param>
		/// <param name="hi">The high 32 bits of a 96-bit integer.</param>
		/// <param name="isNegative">
		///   <see langword="true" /> to indicate a negative number; <see langword="false" /> to indicate a positive number.</param>
		/// <param name="scale">A power of 10 ranging from 0 to 28.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scale" /> is greater than 28.</exception>
		// Token: 0x06001C9D RID: 7325 RVA: 0x0006A610 File Offset: 0x00068810
		public Decimal(int lo, int mid, int hi, bool isNegative, byte scale)
		{
			if (scale > 28)
			{
				throw new ArgumentOutOfRangeException("scale", "Decimal's scale value must be between 0 and 28, inclusive.");
			}
			this.lo = lo;
			this.mid = mid;
			this.hi = hi;
			this.flags = (int)scale << 16;
			if (isNegative)
			{
				this.flags |= int.MinValue;
			}
		}

		/// <summary>Runs when the deserialization of an object has been completed.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.Decimal" /> object contains invalid or corrupted data.</exception>
		// Token: 0x06001C9E RID: 7326 RVA: 0x0006A669 File Offset: 0x00068869
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			if (!decimal.IsValid(this.flags))
			{
				throw new SerializationException("Value was either too large or too small for a Decimal.");
			}
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x0006A683 File Offset: 0x00068883
		private Decimal(int lo, int mid, int hi, int flags)
		{
			if (decimal.IsValid(flags))
			{
				this.lo = lo;
				this.mid = mid;
				this.hi = hi;
				this.flags = flags;
				return;
			}
			throw new ArgumentException("Decimal byte array constructor requires an array of length four containing valid decimal bytes.");
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x0006A6B6 File Offset: 0x000688B6
		private Decimal(in decimal d, int flags)
		{
			this = d;
			this.flags = flags;
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x0006A6CB File Offset: 0x000688CB
		internal static decimal Abs(ref decimal d)
		{
			return new decimal(ref d, d.flags & int.MaxValue);
		}

		/// <summary>Adds two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The first value to add.</param>
		/// <param name="d2">The second value to add.</param>
		/// <returns>The sum of <paramref name="d1" /> and <paramref name="d2" />.</returns>
		/// <exception cref="T:System.OverflowException">The sum of <paramref name="d1" /> and <paramref name="d2" /> is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06001CA2 RID: 7330 RVA: 0x0006A6DF File Offset: 0x000688DF
		public static decimal Add(decimal d1, decimal d2)
		{
			decimal.DecCalc.DecAddSub(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2), false);
			return d1;
		}

		/// <summary>Returns the smallest integral value that is greater than or equal to the specified decimal number.</summary>
		/// <param name="d">A decimal number.</param>
		/// <returns>The smallest integral value that is greater than or equal to the <paramref name="d" /> parameter. Note that this method returns a <see cref="T:System.Decimal" /> instead of an integral type.</returns>
		// Token: 0x06001CA3 RID: 7331 RVA: 0x0006A6F8 File Offset: 0x000688F8
		public static decimal Ceiling(decimal d)
		{
			int num = d.flags;
			if ((num & 16711680) != 0)
			{
				decimal.DecCalc.InternalRound(decimal.AsMutable(ref d), (uint)((byte)(num >> 16)), decimal.DecCalc.RoundingMode.Ceiling);
			}
			return d;
		}

		/// <summary>Compares two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The first value to compare.</param>
		/// <param name="d2">The second value to compare.</param>
		/// <returns>A signed number indicating the relative values of <paramref name="d1" /> and <paramref name="d2" />.  
		///   Return value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///  <paramref name="d1" /> is less than <paramref name="d2" />.  
		///
		///   Zero  
		///
		///  <paramref name="d1" /> and <paramref name="d2" /> are equal.  
		///
		///   Greater than zero  
		///
		///  <paramref name="d1" /> is greater than <paramref name="d2" />.</returns>
		// Token: 0x06001CA4 RID: 7332 RVA: 0x0006A728 File Offset: 0x00068928
		public static int Compare(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(d1, d2);
		}

		/// <summary>Compares this instance to a specified object and returns a comparison of their relative values.</summary>
		/// <param name="value">The object to compare with this instance, or <see langword="null" />.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.  
		///   Return value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///   This instance is less than <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance is equal to <paramref name="value" />.  
		///
		///   Greater than zero  
		///
		///   This instance is greater than <paramref name="value" />.  
		///
		///  -or-  
		///
		///  <paramref name="value" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not a <see cref="T:System.Decimal" />.</exception>
		// Token: 0x06001CA5 RID: 7333 RVA: 0x0006A734 File Offset: 0x00068934
		[SecuritySafeCritical]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is decimal))
			{
				throw new ArgumentException("Object must be of type Decimal.");
			}
			decimal num = (decimal)value;
			return decimal.DecCalc.VarDecCmp(this, num);
		}

		/// <summary>Compares this instance to a specified <see cref="T:System.Decimal" /> object and returns a comparison of their relative values.</summary>
		/// <param name="value">The object to compare with this instance.</param>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="value" />.  
		///   Return value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///   This instance is less than <paramref name="value" />.  
		///
		///   Zero  
		///
		///   This instance is equal to <paramref name="value" />.  
		///
		///   Greater than zero  
		///
		///   This instance is greater than <paramref name="value" />.</returns>
		// Token: 0x06001CA6 RID: 7334 RVA: 0x0006A768 File Offset: 0x00068968
		public int CompareTo(decimal value)
		{
			return decimal.DecCalc.VarDecCmp(this, value);
		}

		/// <summary>Divides two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The dividend.</param>
		/// <param name="d2">The divisor.</param>
		/// <returns>The result of dividing <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="d2" /> is zero.</exception>
		/// <exception cref="T:System.OverflowException">The return value (that is, the quotient) is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06001CA7 RID: 7335 RVA: 0x0006A772 File Offset: 0x00068972
		public static decimal Divide(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecDiv(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		/// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Object" /> represent the same type and value.</summary>
		/// <param name="value">The object to compare with this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is a <see cref="T:System.Decimal" /> and equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CA8 RID: 7336 RVA: 0x0006A788 File Offset: 0x00068988
		public override bool Equals(object value)
		{
			if (value is decimal)
			{
				decimal num = (decimal)value;
				return decimal.DecCalc.VarDecCmp(this, num) == 0;
			}
			return false;
		}

		/// <summary>Returns a value indicating whether this instance and a specified <see cref="T:System.Decimal" /> object represent the same value.</summary>
		/// <param name="value">An object to compare to this instance.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is equal to this instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CA9 RID: 7337 RVA: 0x0006A7B1 File Offset: 0x000689B1
		public bool Equals(decimal value)
		{
			return decimal.DecCalc.VarDecCmp(this, value) == 0;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001CAA RID: 7338 RVA: 0x0006A7BE File Offset: 0x000689BE
		public override int GetHashCode()
		{
			return decimal.DecCalc.GetHashCode(this);
		}

		/// <summary>Returns a value indicating whether two specified instances of <see cref="T:System.Decimal" /> represent the same value.</summary>
		/// <param name="d1">The first value to compare.</param>
		/// <param name="d2">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> and <paramref name="d2" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CAB RID: 7339 RVA: 0x0006A7C6 File Offset: 0x000689C6
		public static bool Equals(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(d1, d2) == 0;
		}

		/// <summary>Rounds a specified <see cref="T:System.Decimal" /> number to the closest integer toward negative infinity.</summary>
		/// <param name="d">The value to round.</param>
		/// <returns>If <paramref name="d" /> has a fractional part, the next whole <see cref="T:System.Decimal" /> number toward negative infinity that is less than <paramref name="d" />.  
		///  -or-  
		///  If <paramref name="d" /> doesn't have a fractional part, <paramref name="d" /> is returned unchanged. Note that the method returns an integral value of type <see cref="T:System.Decimal" />.</returns>
		// Token: 0x06001CAC RID: 7340 RVA: 0x0006A7D4 File Offset: 0x000689D4
		public static decimal Floor(decimal d)
		{
			int num = d.flags;
			if ((num & 16711680) != 0)
			{
				decimal.DecCalc.InternalRound(decimal.AsMutable(ref d), (uint)((byte)(num >> 16)), decimal.DecCalc.RoundingMode.Floor);
			}
			return d;
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation.</summary>
		/// <returns>A string that represents the value of this instance.</returns>
		// Token: 0x06001CAD RID: 7341 RVA: 0x0006A804 File Offset: 0x00068A04
		public override string ToString()
		{
			return Number.FormatDecimal(this, null, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation, using the specified format.</summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.</exception>
		// Token: 0x06001CAE RID: 7342 RVA: 0x0006A81C File Offset: 0x00068A1C
		public string ToString(string format)
		{
			return Number.FormatDecimal(this, format, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.</summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="provider" />.</returns>
		// Token: 0x06001CAF RID: 7343 RVA: 0x0006A834 File Offset: 0x00068A34
		[SecuritySafeCritical]
		public string ToString(IFormatProvider provider)
		{
			return Number.FormatDecimal(this, null, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.</summary>
		/// <param name="format">A numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the value of this instance as specified by <paramref name="format" /> and <paramref name="provider" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="format" /> is invalid.</exception>
		// Token: 0x06001CB0 RID: 7344 RVA: 0x0006A84D File Offset: 0x00068A4D
		[SecuritySafeCritical]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatDecimal(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x0006A866 File Offset: 0x00068A66
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), IFormatProvider provider = null)
		{
			return Number.TryFormatDecimal(this, format, NumberFormatInfo.GetInstance(provider), destination, out charsWritten);
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent.</summary>
		/// <param name="s">The string representation of the number to convert.</param>
		/// <returns>The equivalent to the number contained in <paramref name="s" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06001CB2 RID: 7346 RVA: 0x0006A87D File Offset: 0x00068A7D
		public static decimal Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number in a specified style to its <see cref="T:System.Decimal" /> equivalent.</summary>
		/// <param name="s">The string representation of the number to convert.</param>
		/// <param name="style">A bitwise combination of <see cref="T:System.Globalization.NumberStyles" /> values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Number" />.</param>
		/// <returns>The <see cref="T:System.Decimal" /> number equivalent to the number contained in <paramref name="s" /> as specified by <paramref name="style" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" /></exception>
		// Token: 0x06001CB3 RID: 7347 RVA: 0x0006A89B File Offset: 0x00068A9B
		public static decimal Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDecimal(s, style, NumberFormatInfo.CurrentInfo);
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent using the specified culture-specific format information.</summary>
		/// <param name="s">The string representation of the number to convert.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> that supplies culture-specific parsing information about <paramref name="s" />.</param>
		/// <returns>The <see cref="T:System.Decimal" /> number equivalent to the number contained in <paramref name="s" /> as specified by <paramref name="provider" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not of the correct format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06001CB4 RID: 7348 RVA: 0x0006A8BE File Offset: 0x00068ABE
		public static decimal Parse(string s, IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDecimal(s, NumberStyles.Number, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent using the specified style and culture-specific format.</summary>
		/// <param name="s">The string representation of the number to convert.</param>
		/// <param name="style">A bitwise combination of <see cref="T:System.Globalization.NumberStyles" /> values that indicates the style elements that can be present in <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Number" />.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> object that supplies culture-specific information about the format of <paramref name="s" />.</param>
		/// <returns>The <see cref="T:System.Decimal" /> number equivalent to the number contained in <paramref name="s" /> as specified by <paramref name="style" /> and <paramref name="provider" />.</returns>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="s" /> is not in the correct format.</exception>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="s" /> represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="s" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		// Token: 0x06001CB5 RID: 7349 RVA: 0x0006A8DD File Offset: 0x00068ADD
		public static decimal Parse(string s, NumberStyles style, IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDecimal(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0006A901 File Offset: 0x00068B01
		public static decimal Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Number, IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.ParseDecimal(s, style, NumberFormatInfo.GetInstance(provider));
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <param name="s">The string representation of the number to convert.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.Decimal" /> number that is equivalent to the numeric value contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, is not a number in a valid format, or represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. This parameter is passed uininitialized; any value originally supplied in <paramref name="result" /> is overwritten.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CB7 RID: 7351 RVA: 0x0006A916 File Offset: 0x00068B16
		public static bool TryParse(string s, out decimal result)
		{
			if (s == null)
			{
				result = 0m;
				return false;
			}
			return Number.TryParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0006A937 File Offset: 0x00068B37
		public static bool TryParse(ReadOnlySpan<char> s, out decimal result)
		{
			return Number.TryParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result);
		}

		/// <summary>Converts the string representation of a number to its <see cref="T:System.Decimal" /> equivalent using the specified style and culture-specific format. A return value indicates whether the conversion succeeded or failed.</summary>
		/// <param name="s">The string representation of the number to convert.</param>
		/// <param name="style">A bitwise combination of enumeration values that indicates the permitted format of <paramref name="s" />. A typical value to specify is <see cref="F:System.Globalization.NumberStyles.Number" />.</param>
		/// <param name="provider">An object that supplies culture-specific parsing information about <paramref name="s" />.</param>
		/// <param name="result">When this method returns, contains the <see cref="T:System.Decimal" /> number that is equivalent to the numeric value contained in <paramref name="s" />, if the conversion succeeded, or zero if the conversion failed. The conversion fails if the <paramref name="s" /> parameter is <see langword="null" /> or <see cref="F:System.String.Empty" />, is not a number in a format compliant with <paramref name="style" />, or represents a number less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />. This parameter is passed uininitialized; any value originally supplied in <paramref name="result" /> is overwritten.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="s" /> was converted successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="style" /> is not a <see cref="T:System.Globalization.NumberStyles" /> value.  
		/// -or-  
		/// <paramref name="style" /> is the <see cref="F:System.Globalization.NumberStyles.AllowHexSpecifier" /> value.</exception>
		// Token: 0x06001CB9 RID: 7353 RVA: 0x0006A947 File Offset: 0x00068B47
		public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out decimal result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				result = 0m;
				return false;
			}
			return Number.TryParseDecimal(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x0006A96E File Offset: 0x00068B6E
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider provider, out decimal result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.TryParseDecimal(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		/// <summary>Converts the value of a specified instance of <see cref="T:System.Decimal" /> to its equivalent binary representation.</summary>
		/// <param name="d">The value to convert.</param>
		/// <returns>A 32-bit signed integer array with four elements that contain the binary representation of <paramref name="d" />.</returns>
		// Token: 0x06001CBB RID: 7355 RVA: 0x0006A984 File Offset: 0x00068B84
		public static int[] GetBits(decimal d)
		{
			return new int[]
			{
				d.lo,
				d.mid,
				d.hi,
				d.flags
			};
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0006A9B0 File Offset: 0x00068BB0
		internal static void GetBytes(in decimal d, byte[] buffer)
		{
			buffer[0] = (byte)d.lo;
			buffer[1] = (byte)(d.lo >> 8);
			buffer[2] = (byte)(d.lo >> 16);
			buffer[3] = (byte)(d.lo >> 24);
			buffer[4] = (byte)d.mid;
			buffer[5] = (byte)(d.mid >> 8);
			buffer[6] = (byte)(d.mid >> 16);
			buffer[7] = (byte)(d.mid >> 24);
			buffer[8] = (byte)d.hi;
			buffer[9] = (byte)(d.hi >> 8);
			buffer[10] = (byte)(d.hi >> 16);
			buffer[11] = (byte)(d.hi >> 24);
			buffer[12] = (byte)d.flags;
			buffer[13] = (byte)(d.flags >> 8);
			buffer[14] = (byte)(d.flags >> 16);
			buffer[15] = (byte)(d.flags >> 24);
		}

		// Token: 0x06001CBD RID: 7357 RVA: 0x0006AA84 File Offset: 0x00068C84
		internal static decimal ToDecimal(byte[] buffer)
		{
			int num = (int)buffer[0] | (int)buffer[1] << 8 | (int)buffer[2] << 16 | (int)buffer[3] << 24;
			int num2 = (int)buffer[4] | (int)buffer[5] << 8 | (int)buffer[6] << 16 | (int)buffer[7] << 24;
			int num3 = (int)buffer[8] | (int)buffer[9] << 8 | (int)buffer[10] << 16 | (int)buffer[11] << 24;
			int num4 = (int)buffer[12] | (int)buffer[13] << 8 | (int)buffer[14] << 16 | (int)buffer[15] << 24;
			return new decimal(num, num2, num3, num4);
		}

		// Token: 0x06001CBE RID: 7358 RVA: 0x0006AAFF File Offset: 0x00068CFF
		internal static ref readonly decimal Max(ref decimal d1, ref decimal d2)
		{
			if (decimal.DecCalc.VarDecCmp(d1, d2) < 0)
			{
				return ref d2;
			}
			return ref d1;
		}

		// Token: 0x06001CBF RID: 7359 RVA: 0x0006AB0E File Offset: 0x00068D0E
		internal static ref readonly decimal Min(ref decimal d1, ref decimal d2)
		{
			if (decimal.DecCalc.VarDecCmp(d1, d2) >= 0)
			{
				return ref d2;
			}
			return ref d1;
		}

		/// <summary>Computes the remainder after dividing two <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The dividend.</param>
		/// <param name="d2">The divisor.</param>
		/// <returns>The remainder after dividing <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="d2" /> is zero.</exception>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06001CC0 RID: 7360 RVA: 0x0006AB1D File Offset: 0x00068D1D
		public static decimal Remainder(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecMod(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		/// <summary>Multiplies two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The multiplicand.</param>
		/// <param name="d2">The multiplier.</param>
		/// <returns>The result of multiplying <paramref name="d1" /> and <paramref name="d2" />.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06001CC1 RID: 7361 RVA: 0x0006AB33 File Offset: 0x00068D33
		public static decimal Multiply(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecMul(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		/// <summary>Returns the result of multiplying the specified <see cref="T:System.Decimal" /> value by negative one.</summary>
		/// <param name="d">The value to negate.</param>
		/// <returns>A decimal number with the value of <paramref name="d" />, but the opposite sign.  
		///  -or-  
		///  Zero, if <paramref name="d" /> is zero.</returns>
		// Token: 0x06001CC2 RID: 7362 RVA: 0x0006AB49 File Offset: 0x00068D49
		public static decimal Negate(decimal d)
		{
			return new decimal(ref d, d.flags ^ int.MinValue);
		}

		/// <summary>Rounds a decimal value to the nearest integer.</summary>
		/// <param name="d">A decimal number to round.</param>
		/// <returns>The integer that is nearest to the <paramref name="d" /> parameter. If <paramref name="d" /> is halfway between two integers, one of which is even and the other odd, the even number is returned.</returns>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" /> value.</exception>
		// Token: 0x06001CC3 RID: 7363 RVA: 0x0006AB5E File Offset: 0x00068D5E
		public static decimal Round(decimal d)
		{
			return decimal.Round(ref d, 0, MidpointRounding.ToEven);
		}

		/// <summary>Rounds a <see cref="T:System.Decimal" /> value to a specified number of decimal places.</summary>
		/// <param name="d">A decimal number to round.</param>
		/// <param name="decimals">A value from 0 to 28 that specifies the number of decimal places to round to.</param>
		/// <returns>The decimal number equivalent to <paramref name="d" /> rounded to <paramref name="decimals" /> number of decimal places.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="decimals" /> is not a value from 0 to 28.</exception>
		// Token: 0x06001CC4 RID: 7364 RVA: 0x0006AB69 File Offset: 0x00068D69
		public static decimal Round(decimal d, int decimals)
		{
			return decimal.Round(ref d, decimals, MidpointRounding.ToEven);
		}

		/// <summary>Rounds a decimal value to the nearest integer. A parameter specifies how to round the value if it is midway between two other numbers.</summary>
		/// <param name="d">A decimal number to round.</param>
		/// <param name="mode">A value that specifies how to round <paramref name="d" /> if it is midway between two other numbers.</param>
		/// <returns>The integer that is nearest to the <paramref name="d" /> parameter. If <paramref name="d" /> is halfway between two numbers, one of which is even and the other odd, the <paramref name="mode" /> parameter determines which of the two numbers is returned.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a <see cref="T:System.MidpointRounding" /> value.</exception>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" /> object.</exception>
		// Token: 0x06001CC5 RID: 7365 RVA: 0x0006AB74 File Offset: 0x00068D74
		public static decimal Round(decimal d, MidpointRounding mode)
		{
			return decimal.Round(ref d, 0, mode);
		}

		/// <summary>Rounds a decimal value to a specified precision. A parameter specifies how to round the value if it is midway between two other numbers.</summary>
		/// <param name="d">A decimal number to round.</param>
		/// <param name="decimals">The number of significant decimal places (precision) in the return value.</param>
		/// <param name="mode">A value that specifies how to round <paramref name="d" /> if it is midway between two other numbers.</param>
		/// <returns>The number that is nearest to the <paramref name="d" /> parameter with a precision equal to the <paramref name="decimals" /> parameter. If <paramref name="d" /> is halfway between two numbers, one of which is even and the other odd, the <paramref name="mode" /> parameter determines which of the two numbers is returned. If the precision of <paramref name="d" /> is less than <paramref name="decimals" />, <paramref name="d" /> is returned unchanged.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="decimals" /> is less than 0 or greater than 28.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a <see cref="T:System.MidpointRounding" /> value.</exception>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" /> object.</exception>
		// Token: 0x06001CC6 RID: 7366 RVA: 0x0006AB7F File Offset: 0x00068D7F
		public static decimal Round(decimal d, int decimals, MidpointRounding mode)
		{
			return decimal.Round(ref d, decimals, mode);
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0006AB8C File Offset: 0x00068D8C
		private static decimal Round(ref decimal d, int decimals, MidpointRounding mode)
		{
			if (decimals > 28)
			{
				throw new ArgumentOutOfRangeException("decimals", "Decimal can only round to between 0 and 28 digits of precision.");
			}
			if (mode > MidpointRounding.AwayFromZero)
			{
				throw new ArgumentException(SR.Format("The value '{0}' is not valid for this usage of the type {1}.", mode, "MidpointRounding"), "mode");
			}
			int num = d.Scale - decimals;
			if (num > 0)
			{
				decimal.DecCalc.InternalRound(decimal.AsMutable(ref d), (uint)num, (decimal.DecCalc.RoundingMode)mode);
			}
			return d;
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0006ABF2 File Offset: 0x00068DF2
		internal static int Sign(ref decimal d)
		{
			if ((d.lo | d.mid | d.hi) != 0)
			{
				return d.flags >> 31 | 1;
			}
			return 0;
		}

		/// <summary>Subtracts one specified <see cref="T:System.Decimal" /> value from another.</summary>
		/// <param name="d1">The minuend.</param>
		/// <param name="d2">The subtrahend.</param>
		/// <returns>The result of subtracting <paramref name="d2" /> from <paramref name="d1" />.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06001CC9 RID: 7369 RVA: 0x0006AC17 File Offset: 0x00068E17
		public static decimal Subtract(decimal d1, decimal d2)
		{
			decimal.DecCalc.DecAddSub(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2), true);
			return d1;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 8-bit unsigned integer.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>An 8-bit unsigned integer equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x06001CCA RID: 7370 RVA: 0x0006AC30 File Offset: 0x00068E30
		public static byte ToByte(decimal value)
		{
			uint num;
			try
			{
				num = decimal.ToUInt32(value);
			}
			catch (OverflowException innerException)
			{
				throw new OverflowException("Value was either too large or too small for an unsigned byte.", innerException);
			}
			if (num != (uint)((byte)num))
			{
				throw new OverflowException("Value was either too large or too small for an unsigned byte.");
			}
			return (byte)num;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 8-bit signed integer.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>An 8-bit signed integer equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		// Token: 0x06001CCB RID: 7371 RVA: 0x0006AC78 File Offset: 0x00068E78
		[CLSCompliant(false)]
		public static sbyte ToSByte(decimal value)
		{
			int num;
			try
			{
				num = decimal.ToInt32(value);
			}
			catch (OverflowException innerException)
			{
				throw new OverflowException("Value was either too large or too small for a signed byte.", innerException);
			}
			if (num != (int)((sbyte)num))
			{
				throw new OverflowException("Value was either too large or too small for a signed byte.");
			}
			return (sbyte)num;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 16-bit signed integer.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>A 16-bit signed integer equivalent to <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x06001CCC RID: 7372 RVA: 0x0006ACC0 File Offset: 0x00068EC0
		public static short ToInt16(decimal value)
		{
			int num;
			try
			{
				num = decimal.ToInt32(value);
			}
			catch (OverflowException innerException)
			{
				throw new OverflowException("Value was either too large or too small for an Int16.", innerException);
			}
			if (num != (int)((short)num))
			{
				throw new OverflowException("Value was either too large or too small for an Int16.");
			}
			return (short)num;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent double-precision floating-point number.</summary>
		/// <param name="d">The decimal number to convert.</param>
		/// <returns>A double-precision floating-point number equivalent to <paramref name="d" />.</returns>
		// Token: 0x06001CCD RID: 7373 RVA: 0x0006AD08 File Offset: 0x00068F08
		public static double ToDouble(decimal d)
		{
			return decimal.DecCalc.VarR8FromDec(d);
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 32-bit signed integer.</summary>
		/// <param name="d">The decimal number to convert.</param>
		/// <returns>A 32-bit signed integer equivalent to the value of <paramref name="d" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="d" /> is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001CCE RID: 7374 RVA: 0x0006AD14 File Offset: 0x00068F14
		public static int ToInt32(decimal d)
		{
			decimal.Truncate(ref d);
			if ((d.hi | d.mid) == 0)
			{
				int num = d.lo;
				if (!d.IsNegative)
				{
					if (num >= 0)
					{
						return num;
					}
				}
				else
				{
					num = -num;
					if (num <= 0)
					{
						return num;
					}
				}
			}
			throw new OverflowException("Value was either too large or too small for an Int32.");
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 64-bit signed integer.</summary>
		/// <param name="d">The decimal number to convert.</param>
		/// <returns>A 64-bit signed integer equivalent to the value of <paramref name="d" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="d" /> is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x06001CCF RID: 7375 RVA: 0x0006AD60 File Offset: 0x00068F60
		public static long ToInt64(decimal d)
		{
			decimal.Truncate(ref d);
			if (d.hi == 0)
			{
				long num = (long)d.Low64;
				if (!d.IsNegative)
				{
					if (num >= 0L)
					{
						return num;
					}
				}
				else
				{
					num = -num;
					if (num <= 0L)
					{
						return num;
					}
				}
			}
			throw new OverflowException("Value was either too large or too small for an Int64.");
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 16-bit unsigned integer.</summary>
		/// <param name="value">The decimal number to convert.</param>
		/// <returns>A 16-bit unsigned integer equivalent to the value of <paramref name="value" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.UInt16.MaxValue" /> or less than <see cref="F:System.UInt16.MinValue" />.</exception>
		// Token: 0x06001CD0 RID: 7376 RVA: 0x0006ADA8 File Offset: 0x00068FA8
		[CLSCompliant(false)]
		public static ushort ToUInt16(decimal value)
		{
			uint num;
			try
			{
				num = decimal.ToUInt32(value);
			}
			catch (OverflowException innerException)
			{
				throw new OverflowException("Value was either too large or too small for a UInt16.", innerException);
			}
			if (num != (uint)((ushort)num))
			{
				throw new OverflowException("Value was either too large or too small for a UInt16.");
			}
			return (ushort)num;
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 32-bit unsigned integer.</summary>
		/// <param name="d">The decimal number to convert.</param>
		/// <returns>A 32-bit unsigned integer equivalent to the value of <paramref name="d" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="d" /> is negative or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x06001CD1 RID: 7377 RVA: 0x0006ADF0 File Offset: 0x00068FF0
		[CLSCompliant(false)]
		public static uint ToUInt32(decimal d)
		{
			decimal.Truncate(ref d);
			if ((d.hi | d.mid) == 0)
			{
				uint low = d.Low;
				if (!d.IsNegative || low == 0U)
				{
					return low;
				}
			}
			throw new OverflowException("Value was either too large or too small for a UInt32.");
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent 64-bit unsigned integer.</summary>
		/// <param name="d">The decimal number to convert.</param>
		/// <returns>A 64-bit unsigned integer equivalent to the value of <paramref name="d" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="d" /> is negative or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x06001CD2 RID: 7378 RVA: 0x0006AE34 File Offset: 0x00069034
		[CLSCompliant(false)]
		public static ulong ToUInt64(decimal d)
		{
			decimal.Truncate(ref d);
			if (d.hi == 0)
			{
				ulong low = d.Low64;
				if (!d.IsNegative || low == 0UL)
				{
					return low;
				}
			}
			throw new OverflowException("Value was either too large or too small for a UInt64.");
		}

		/// <summary>Converts the value of the specified <see cref="T:System.Decimal" /> to the equivalent single-precision floating-point number.</summary>
		/// <param name="d">The decimal number to convert.</param>
		/// <returns>A single-precision floating-point number equivalent to the value of <paramref name="d" />.</returns>
		// Token: 0x06001CD3 RID: 7379 RVA: 0x0006AE70 File Offset: 0x00069070
		public static float ToSingle(decimal d)
		{
			return decimal.DecCalc.VarR4FromDec(d);
		}

		/// <summary>Returns the integral digits of the specified <see cref="T:System.Decimal" />; any fractional digits are discarded.</summary>
		/// <param name="d">The decimal number to truncate.</param>
		/// <returns>The result of <paramref name="d" /> rounded toward zero, to the nearest whole number.</returns>
		// Token: 0x06001CD4 RID: 7380 RVA: 0x0006AE79 File Offset: 0x00069079
		public static decimal Truncate(decimal d)
		{
			decimal.Truncate(ref d);
			return d;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x0006AE84 File Offset: 0x00069084
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Truncate(ref decimal d)
		{
			int num = d.flags;
			if ((num & 16711680) != 0)
			{
				decimal.DecCalc.InternalRound(decimal.AsMutable(ref d), (uint)((byte)(num >> 16)), decimal.DecCalc.RoundingMode.Truncate);
			}
		}

		/// <summary>Defines an implicit conversion of an 8-bit unsigned integer to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The 8-bit unsigned integer to convert.</param>
		/// <returns>The converted 8-bit unsigned integer.</returns>
		// Token: 0x06001CD6 RID: 7382 RVA: 0x0006AEB2 File Offset: 0x000690B2
		public static implicit operator decimal(byte value)
		{
			return new decimal((uint)value);
		}

		/// <summary>Defines an implicit conversion of an 8-bit signed integer to a <see cref="T:System.Decimal" />.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The 8-bit signed integer to convert.</param>
		/// <returns>The converted 8-bit signed integer.</returns>
		// Token: 0x06001CD7 RID: 7383 RVA: 0x0006AEBA File Offset: 0x000690BA
		[CLSCompliant(false)]
		public static implicit operator decimal(sbyte value)
		{
			return new decimal((int)value);
		}

		/// <summary>Defines an implicit conversion of a 16-bit signed integer to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The 16-bit signed integer to convert.</param>
		/// <returns>The converted 16-bit signed integer.</returns>
		// Token: 0x06001CD8 RID: 7384 RVA: 0x0006AEBA File Offset: 0x000690BA
		public static implicit operator decimal(short value)
		{
			return new decimal((int)value);
		}

		/// <summary>Defines an implicit conversion of a 16-bit unsigned integer to a <see cref="T:System.Decimal" />.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The 16-bit unsigned integer to convert.</param>
		/// <returns>The converted 16-bit unsigned integer.</returns>
		// Token: 0x06001CD9 RID: 7385 RVA: 0x0006AEB2 File Offset: 0x000690B2
		[CLSCompliant(false)]
		public static implicit operator decimal(ushort value)
		{
			return new decimal((uint)value);
		}

		/// <summary>Defines an implicit conversion of a Unicode character to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The Unicode character to convert.</param>
		/// <returns>The converted Unicode character.</returns>
		// Token: 0x06001CDA RID: 7386 RVA: 0x0006AEB2 File Offset: 0x000690B2
		public static implicit operator decimal(char value)
		{
			return new decimal((uint)value);
		}

		/// <summary>Defines an implicit conversion of a 32-bit signed integer to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The 32-bit signed integer to convert.</param>
		/// <returns>The converted 32-bit signed integer.</returns>
		// Token: 0x06001CDB RID: 7387 RVA: 0x0006AEBA File Offset: 0x000690BA
		public static implicit operator decimal(int value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an implicit conversion of a 32-bit unsigned integer to a <see cref="T:System.Decimal" />.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The 32-bit unsigned integer to convert.</param>
		/// <returns>The converted 32-bit unsigned integer.</returns>
		// Token: 0x06001CDC RID: 7388 RVA: 0x0006AEB2 File Offset: 0x000690B2
		[CLSCompliant(false)]
		public static implicit operator decimal(uint value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an implicit conversion of a 64-bit signed integer to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The 64-bit signed integer to convert.</param>
		/// <returns>The converted 64-bit signed integer.</returns>
		// Token: 0x06001CDD RID: 7389 RVA: 0x0006AEC2 File Offset: 0x000690C2
		public static implicit operator decimal(long value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an implicit conversion of a 64-bit unsigned integer to a <see cref="T:System.Decimal" />.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The 64-bit unsigned integer to convert.</param>
		/// <returns>The converted 64-bit unsigned integer.</returns>
		// Token: 0x06001CDE RID: 7390 RVA: 0x0006AECA File Offset: 0x000690CA
		[CLSCompliant(false)]
		public static implicit operator decimal(ulong value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a single-precision floating-point number to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The single-precision floating-point number to convert.</param>
		/// <returns>The converted single-precision floating point number.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.PositiveInfinity" />, or <see cref="F:System.Single.NegativeInfinity" />.</exception>
		// Token: 0x06001CDF RID: 7391 RVA: 0x0006AED2 File Offset: 0x000690D2
		public static explicit operator decimal(float value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a double-precision floating-point number to a <see cref="T:System.Decimal" />.</summary>
		/// <param name="value">The double-precision floating-point number to convert.</param>
		/// <returns>The converted double-precision floating point number.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is greater than <see cref="F:System.Decimal.MaxValue" /> or less than <see cref="F:System.Decimal.MinValue" />.  
		/// -or-  
		/// <paramref name="value" /> is <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NegativeInfinity" />.</exception>
		// Token: 0x06001CE0 RID: 7392 RVA: 0x0006AEDA File Offset: 0x000690DA
		public static explicit operator decimal(double value)
		{
			return new decimal(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to an 8-bit unsigned integer.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>An 8-bit unsigned integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x06001CE1 RID: 7393 RVA: 0x0006AEE2 File Offset: 0x000690E2
		public static explicit operator byte(decimal value)
		{
			return decimal.ToByte(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to an 8-bit signed integer.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>An 8-bit signed integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		// Token: 0x06001CE2 RID: 7394 RVA: 0x0006AEEA File Offset: 0x000690EA
		[CLSCompliant(false)]
		public static explicit operator sbyte(decimal value)
		{
			return decimal.ToSByte(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a Unicode character.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A Unicode character that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Char.MinValue" /> or greater than <see cref="F:System.Char.MaxValue" />.</exception>
		// Token: 0x06001CE3 RID: 7395 RVA: 0x0006AEF4 File Offset: 0x000690F4
		public static explicit operator char(decimal value)
		{
			ushort result;
			try
			{
				result = decimal.ToUInt16(value);
			}
			catch (OverflowException innerException)
			{
				throw new OverflowException("Value was either too large or too small for a character.", innerException);
			}
			return (char)result;
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 16-bit signed integer.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A 16-bit signed integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x06001CE4 RID: 7396 RVA: 0x0006AF28 File Offset: 0x00069128
		public static explicit operator short(decimal value)
		{
			return decimal.ToInt16(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 16-bit unsigned integer.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A 16-bit unsigned integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x06001CE5 RID: 7397 RVA: 0x0006AF30 File Offset: 0x00069130
		[CLSCompliant(false)]
		public static explicit operator ushort(decimal value)
		{
			return decimal.ToUInt16(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 32-bit signed integer.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A 32-bit signed integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001CE6 RID: 7398 RVA: 0x0006AF38 File Offset: 0x00069138
		public static explicit operator int(decimal value)
		{
			return decimal.ToInt32(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 32-bit unsigned integer.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A 32-bit unsigned integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x06001CE7 RID: 7399 RVA: 0x0006AF40 File Offset: 0x00069140
		[CLSCompliant(false)]
		public static explicit operator uint(decimal value)
		{
			return decimal.ToUInt32(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 64-bit signed integer.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A 64-bit signed integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x06001CE8 RID: 7400 RVA: 0x0006AF48 File Offset: 0x00069148
		public static explicit operator long(decimal value)
		{
			return decimal.ToInt64(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a 64-bit unsigned integer.  
		///  This API is not CLS-compliant.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A 64-bit unsigned integer that represents the converted <see cref="T:System.Decimal" />.</returns>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> is negative or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x06001CE9 RID: 7401 RVA: 0x0006AF50 File Offset: 0x00069150
		[CLSCompliant(false)]
		public static explicit operator ulong(decimal value)
		{
			return decimal.ToUInt64(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a single-precision floating-point number.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A single-precision floating-point number that represents the converted <see cref="T:System.Decimal" />.</returns>
		// Token: 0x06001CEA RID: 7402 RVA: 0x0006AF58 File Offset: 0x00069158
		public static explicit operator float(decimal value)
		{
			return decimal.ToSingle(value);
		}

		/// <summary>Defines an explicit conversion of a <see cref="T:System.Decimal" /> to a double-precision floating-point number.</summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>A double-precision floating-point number that represents the converted <see cref="T:System.Decimal" />.</returns>
		// Token: 0x06001CEB RID: 7403 RVA: 0x0006AF60 File Offset: 0x00069160
		public static explicit operator double(decimal value)
		{
			return decimal.ToDouble(value);
		}

		/// <summary>Returns the value of the <see cref="T:System.Decimal" /> operand (the sign of the operand is unchanged).</summary>
		/// <param name="d">The operand to return.</param>
		/// <returns>The value of the operand, <paramref name="d" />.</returns>
		// Token: 0x06001CEC RID: 7404 RVA: 0x0000270D File Offset: 0x0000090D
		public static decimal operator +(decimal d)
		{
			return d;
		}

		/// <summary>Negates the value of the specified <see cref="T:System.Decimal" /> operand.</summary>
		/// <param name="d">The value to negate.</param>
		/// <returns>The result of <paramref name="d" /> multiplied by negative one (-1).</returns>
		// Token: 0x06001CED RID: 7405 RVA: 0x0006AB49 File Offset: 0x00068D49
		public static decimal operator -(decimal d)
		{
			return new decimal(ref d, d.flags ^ int.MinValue);
		}

		/// <summary>Increments the <see cref="T:System.Decimal" /> operand by 1.</summary>
		/// <param name="d">The value to increment.</param>
		/// <returns>The value of <paramref name="d" /> incremented by 1.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06001CEE RID: 7406 RVA: 0x0006AF68 File Offset: 0x00069168
		public static decimal operator ++(decimal d)
		{
			return decimal.Add(d, 1m);
		}

		/// <summary>Decrements the <see cref="T:System.Decimal" /> operand by one.</summary>
		/// <param name="d">The value to decrement.</param>
		/// <returns>The value of <paramref name="d" /> decremented by 1.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06001CEF RID: 7407 RVA: 0x0006AF75 File Offset: 0x00069175
		public static decimal operator --(decimal d)
		{
			return decimal.Subtract(d, 1m);
		}

		/// <summary>Adds two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The first value to add.</param>
		/// <param name="d2">The second value to add.</param>
		/// <returns>The result of adding <paramref name="d1" /> and <paramref name="d2" />.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06001CF0 RID: 7408 RVA: 0x0006A6DF File Offset: 0x000688DF
		public static decimal operator +(decimal d1, decimal d2)
		{
			decimal.DecCalc.DecAddSub(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2), false);
			return d1;
		}

		/// <summary>Subtracts two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The minuend.</param>
		/// <param name="d2">The subtrahend.</param>
		/// <returns>The result of subtracting <paramref name="d2" /> from <paramref name="d1" />.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06001CF1 RID: 7409 RVA: 0x0006AC17 File Offset: 0x00068E17
		public static decimal operator -(decimal d1, decimal d2)
		{
			decimal.DecCalc.DecAddSub(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2), true);
			return d1;
		}

		/// <summary>Multiplies two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The first value to multiply.</param>
		/// <param name="d2">The second value to multiply.</param>
		/// <returns>The result of multiplying <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06001CF2 RID: 7410 RVA: 0x0006AB33 File Offset: 0x00068D33
		public static decimal operator *(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecMul(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		/// <summary>Divides two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The dividend.</param>
		/// <param name="d2">The divisor.</param>
		/// <returns>The result of dividing <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="d2" /> is zero.</exception>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06001CF3 RID: 7411 RVA: 0x0006A772 File Offset: 0x00068972
		public static decimal operator /(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecDiv(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		/// <summary>Returns the remainder resulting from dividing two specified <see cref="T:System.Decimal" /> values.</summary>
		/// <param name="d1">The dividend.</param>
		/// <param name="d2">The divisor.</param>
		/// <returns>The remainder resulting from dividing <paramref name="d1" /> by <paramref name="d2" />.</returns>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="d2" /> is <see langword="zero" />.</exception>
		/// <exception cref="T:System.OverflowException">The return value is less than <see cref="F:System.Decimal.MinValue" /> or greater than <see cref="F:System.Decimal.MaxValue" />.</exception>
		// Token: 0x06001CF4 RID: 7412 RVA: 0x0006AB1D File Offset: 0x00068D1D
		public static decimal operator %(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecMod(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Decimal" /> values are equal.</summary>
		/// <param name="d1">The first value to compare.</param>
		/// <param name="d2">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> and <paramref name="d2" /> are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CF5 RID: 7413 RVA: 0x0006A7C6 File Offset: 0x000689C6
		public static bool operator ==(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(d1, d2) == 0;
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Decimal" /> objects have different values.</summary>
		/// <param name="d1">The first value to compare.</param>
		/// <param name="d2">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> and <paramref name="d2" /> are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CF6 RID: 7414 RVA: 0x0006AF82 File Offset: 0x00069182
		public static bool operator !=(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(d1, d2) != 0;
		}

		/// <summary>Returns a value indicating whether a specified <see cref="T:System.Decimal" /> is less than another specified <see cref="T:System.Decimal" />.</summary>
		/// <param name="d1">The first value to compare.</param>
		/// <param name="d2">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> is less than <paramref name="d2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CF7 RID: 7415 RVA: 0x0006AF90 File Offset: 0x00069190
		public static bool operator <(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(d1, d2) < 0;
		}

		/// <summary>Returns a value indicating whether a specified <see cref="T:System.Decimal" /> is less than or equal to another specified <see cref="T:System.Decimal" />.</summary>
		/// <param name="d1">The first value to compare.</param>
		/// <param name="d2">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> is less than or equal to <paramref name="d2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CF8 RID: 7416 RVA: 0x0006AF9E File Offset: 0x0006919E
		public static bool operator <=(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(d1, d2) <= 0;
		}

		/// <summary>Returns a value indicating whether a specified <see cref="T:System.Decimal" /> is greater than another specified <see cref="T:System.Decimal" />.</summary>
		/// <param name="d1">The first value to compare.</param>
		/// <param name="d2">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> is greater than <paramref name="d2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CF9 RID: 7417 RVA: 0x0006AFAF File Offset: 0x000691AF
		public static bool operator >(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(d1, d2) > 0;
		}

		/// <summary>Returns a value indicating whether a specified <see cref="T:System.Decimal" /> is greater than or equal to another specified <see cref="T:System.Decimal" />.</summary>
		/// <param name="d1">The first value to compare.</param>
		/// <param name="d2">The second value to compare.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="d1" /> is greater than or equal to <paramref name="d2" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CFA RID: 7418 RVA: 0x0006AFBD File Offset: 0x000691BD
		public static bool operator >=(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(d1, d2) >= 0;
		}

		/// <summary>Returns the <see cref="T:System.TypeCode" /> for value type <see cref="T:System.Decimal" />.</summary>
		/// <returns>The enumerated constant <see cref="F:System.TypeCode.Decimal" />.</returns>
		// Token: 0x06001CFB RID: 7419 RVA: 0x0006AFCE File Offset: 0x000691CE
		public TypeCode GetTypeCode()
		{
			return TypeCode.Decimal;
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToBoolean(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>
		///   <see langword="true" /> if the value of the current instance is not zero; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001CFC RID: 7420 RVA: 0x0006AFD2 File Offset: 0x000691D2
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>None. This conversion is not supported.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06001CFD RID: 7421 RVA: 0x0006AFDF File Offset: 0x000691DF
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Decimal", "Char"));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.SByte" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.SByte.MinValue" /> or greater than <see cref="F:System.SByte.MaxValue" />.</exception>
		// Token: 0x06001CFE RID: 7422 RVA: 0x0006AFFA File Offset: 0x000691FA
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToByte(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Byte" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.Byte.MinValue" /> or greater than <see cref="F:System.Byte.MaxValue" />.</exception>
		// Token: 0x06001CFF RID: 7423 RVA: 0x0006B007 File Offset: 0x00069207
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Int16" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.Int16.MinValue" /> or greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		// Token: 0x06001D00 RID: 7424 RVA: 0x0006B014 File Offset: 0x00069214
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToUInt16(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt16" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.UInt16.MinValue" /> or greater than <see cref="F:System.UInt16.MaxValue" />.</exception>
		// Token: 0x06001D01 RID: 7425 RVA: 0x0006B021 File Offset: 0x00069221
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">The parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Int32" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001D02 RID: 7426 RVA: 0x0006B02E File Offset: 0x0006922E
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt32(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt32" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.UInt32.MinValue" /> or greater than <see cref="F:System.UInt32.MaxValue" />.</exception>
		// Token: 0x06001D03 RID: 7427 RVA: 0x0006B03B File Offset: 0x0006923B
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Int64" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.Int64.MinValue" /> or greater than <see cref="F:System.Int64.MaxValue" />.</exception>
		// Token: 0x06001D04 RID: 7428 RVA: 0x0006B048 File Offset: 0x00069248
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToInt64(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.UInt64" />.</returns>
		/// <exception cref="T:System.OverflowException">The resulting integer value is less than <see cref="F:System.UInt64.MinValue" /> or greater than <see cref="F:System.UInt64.MaxValue" />.</exception>
		// Token: 0x06001D05 RID: 7429 RVA: 0x0006B055 File Offset: 0x00069255
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToSingle(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Single" />.</returns>
		// Token: 0x06001D06 RID: 7430 RVA: 0x0006B062 File Offset: 0x00069262
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDouble(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, converted to a <see cref="T:System.Double" />.</returns>
		// Token: 0x06001D07 RID: 7431 RVA: 0x0006B06F File Offset: 0x0006926F
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToDecimal(System.IFormatProvider)" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>The value of the current instance, unchanged.</returns>
		// Token: 0x06001D08 RID: 7432 RVA: 0x0006B07C File Offset: 0x0006927C
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return this;
		}

		/// <summary>This conversion is not supported. Attempting to use this method throws an <see cref="T:System.InvalidCastException" />.</summary>
		/// <param name="provider">This parameter is ignored.</param>
		/// <returns>None. This conversion is not supported.</returns>
		/// <exception cref="T:System.InvalidCastException">In all cases.</exception>
		// Token: 0x06001D09 RID: 7433 RVA: 0x0006B084 File Offset: 0x00069284
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format("Invalid cast from '{0}' to '{1}'.", "Decimal", "DateTime"));
		}

		/// <summary>For a description of this member, see <see cref="M:System.IConvertible.ToType(System.Type,System.IFormatProvider)" />.</summary>
		/// <param name="type">The type to which to convert the value of this <see cref="T:System.Decimal" /> instance.</param>
		/// <param name="provider">An <see cref="T:System.IFormatProvider" /> implementation that supplies culture-specific information about the format of the returned value.</param>
		/// <returns>The value of the current instance, converted to a <paramref name="type" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The requested type conversion is not supported.</exception>
		// Token: 0x06001D0A RID: 7434 RVA: 0x0006B09F File Offset: 0x0006929F
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x06001D0B RID: 7435 RVA: 0x0006B0B4 File Offset: 0x000692B4
		// Note: this type is marked as 'beforefieldinit'.
		static Decimal()
		{
		}

		// Token: 0x040019E3 RID: 6627
		private const int SignMask = -2147483648;

		// Token: 0x040019E4 RID: 6628
		private const int ScaleMask = 16711680;

		// Token: 0x040019E5 RID: 6629
		private const int ScaleShift = 16;

		/// <summary>Represents the number zero (0).</summary>
		// Token: 0x040019E6 RID: 6630
		public const decimal Zero = 0m;

		/// <summary>Represents the number one (1).</summary>
		// Token: 0x040019E7 RID: 6631
		public const decimal One = 1m;

		/// <summary>Represents the number negative one (-1).</summary>
		// Token: 0x040019E8 RID: 6632
		public const decimal MinusOne = -1m;

		/// <summary>Represents the largest possible value of <see cref="T:System.Decimal" />. This field is constant and read-only.</summary>
		// Token: 0x040019E9 RID: 6633
		public const decimal MaxValue = 79228162514264337593543950335m;

		/// <summary>Represents the smallest possible value of <see cref="T:System.Decimal" />. This field is constant and read-only.</summary>
		// Token: 0x040019EA RID: 6634
		public const decimal MinValue = -79228162514264337593543950335m;

		// Token: 0x040019EB RID: 6635
		[FieldOffset(0)]
		private readonly int flags;

		// Token: 0x040019EC RID: 6636
		[FieldOffset(4)]
		private readonly int hi;

		// Token: 0x040019ED RID: 6637
		[FieldOffset(8)]
		private readonly int lo;

		// Token: 0x040019EE RID: 6638
		[FieldOffset(12)]
		private readonly int mid;

		// Token: 0x040019EF RID: 6639
		[NonSerialized]
		[FieldOffset(8)]
		private readonly ulong ulomidLE;

		// Token: 0x02000276 RID: 630
		[StructLayout(LayoutKind.Explicit)]
		private struct DecCalc
		{
			// Token: 0x17000362 RID: 866
			// (get) Token: 0x06001D0C RID: 7436 RVA: 0x0006B100 File Offset: 0x00069300
			// (set) Token: 0x06001D0D RID: 7437 RVA: 0x0006B108 File Offset: 0x00069308
			private uint High
			{
				get
				{
					return this.uhi;
				}
				set
				{
					this.uhi = value;
				}
			}

			// Token: 0x17000363 RID: 867
			// (get) Token: 0x06001D0E RID: 7438 RVA: 0x0006B111 File Offset: 0x00069311
			// (set) Token: 0x06001D0F RID: 7439 RVA: 0x0006B119 File Offset: 0x00069319
			private uint Low
			{
				get
				{
					return this.ulo;
				}
				set
				{
					this.ulo = value;
				}
			}

			// Token: 0x17000364 RID: 868
			// (get) Token: 0x06001D10 RID: 7440 RVA: 0x0006B122 File Offset: 0x00069322
			// (set) Token: 0x06001D11 RID: 7441 RVA: 0x0006B12A File Offset: 0x0006932A
			private uint Mid
			{
				get
				{
					return this.umid;
				}
				set
				{
					this.umid = value;
				}
			}

			// Token: 0x17000365 RID: 869
			// (get) Token: 0x06001D12 RID: 7442 RVA: 0x0006B133 File Offset: 0x00069333
			private bool IsNegative
			{
				get
				{
					return this.uflags < 0U;
				}
			}

			// Token: 0x17000366 RID: 870
			// (get) Token: 0x06001D13 RID: 7443 RVA: 0x0006B13E File Offset: 0x0006933E
			private int Scale
			{
				get
				{
					return (int)((byte)(this.uflags >> 16));
				}
			}

			// Token: 0x17000367 RID: 871
			// (get) Token: 0x06001D14 RID: 7444 RVA: 0x0006B14A File Offset: 0x0006934A
			// (set) Token: 0x06001D15 RID: 7445 RVA: 0x0006B16C File Offset: 0x0006936C
			private ulong Low64
			{
				get
				{
					if (!BitConverter.IsLittleEndian)
					{
						return (ulong)this.umid << 32 | (ulong)this.ulo;
					}
					return this.ulomidLE;
				}
				set
				{
					if (BitConverter.IsLittleEndian)
					{
						this.ulomidLE = value;
						return;
					}
					this.umid = (uint)(value >> 32);
					this.ulo = (uint)value;
				}
			}

			// Token: 0x06001D16 RID: 7446 RVA: 0x0006B190 File Offset: 0x00069390
			private unsafe static uint GetExponent(float f)
			{
				return (uint)((byte)(*(uint*)(&f) >> 23));
			}

			// Token: 0x06001D17 RID: 7447 RVA: 0x0006B19A File Offset: 0x0006939A
			private unsafe static uint GetExponent(double d)
			{
				return (uint)((ulong)(*(long*)(&d)) >> 52) & 2047U;
			}

			// Token: 0x06001D18 RID: 7448 RVA: 0x0003A652 File Offset: 0x00038852
			private static ulong UInt32x32To64(uint a, uint b)
			{
				return (ulong)a * (ulong)b;
			}

			// Token: 0x06001D19 RID: 7449 RVA: 0x0006B1AC File Offset: 0x000693AC
			private static void UInt64x64To128(ulong a, ulong b, ref decimal.DecCalc result)
			{
				ulong num = decimal.DecCalc.UInt32x32To64((uint)a, (uint)b);
				ulong num2 = decimal.DecCalc.UInt32x32To64((uint)a, (uint)(b >> 32));
				ulong num3 = decimal.DecCalc.UInt32x32To64((uint)(a >> 32), (uint)(b >> 32));
				num3 += num2 >> 32;
				num += (num2 <<= 32);
				if (num < num2)
				{
					num3 += 1UL;
				}
				num2 = decimal.DecCalc.UInt32x32To64((uint)(a >> 32), (uint)b);
				num3 += num2 >> 32;
				num += (num2 <<= 32);
				if (num < num2)
				{
					num3 += 1UL;
				}
				if (num3 > (ulong)-1)
				{
					throw new OverflowException("Value was either too large or too small for a Decimal.");
				}
				result.Low64 = num;
				result.High = (uint)num3;
			}

			// Token: 0x06001D1A RID: 7450 RVA: 0x0006B240 File Offset: 0x00069440
			private static uint Div96By32(ref decimal.DecCalc.Buf12 bufNum, uint den)
			{
				if (bufNum.U2 != 0U)
				{
					ulong num = bufNum.High64;
					ulong num2 = num / (ulong)den;
					bufNum.High64 = num2;
					num = (num - (ulong)((uint)num2 * den) << 32 | (ulong)bufNum.U0);
					if (num == 0UL)
					{
						return 0U;
					}
					uint num3 = (uint)(num / (ulong)den);
					bufNum.U0 = num3;
					return (uint)num - num3 * den;
				}
				else
				{
					ulong num = bufNum.Low64;
					if (num == 0UL)
					{
						return 0U;
					}
					ulong num2 = num / (ulong)den;
					bufNum.Low64 = num2;
					return (uint)(num - num2 * (ulong)den);
				}
			}

			// Token: 0x06001D1B RID: 7451 RVA: 0x0006B2B4 File Offset: 0x000694B4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static bool Div96ByConst(ref ulong high64, ref uint low, uint pow)
			{
				ulong num = high64 / (ulong)pow;
				uint num2 = (uint)(((high64 - num * (ulong)pow << 32) + (ulong)low) / (ulong)pow);
				if (low == num2 * pow)
				{
					high64 = num;
					low = num2;
					return true;
				}
				return false;
			}

			// Token: 0x06001D1C RID: 7452 RVA: 0x0006B2EC File Offset: 0x000694EC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static void Unscale(ref uint low, ref ulong high64, ref int scale)
			{
				while ((byte)low == 0 && scale >= 8 && decimal.DecCalc.Div96ByConst(ref high64, ref low, 100000000U))
				{
					scale -= 8;
				}
				if ((low & 15U) == 0U && scale >= 4 && decimal.DecCalc.Div96ByConst(ref high64, ref low, 10000U))
				{
					scale -= 4;
				}
				if ((low & 3U) == 0U && scale >= 2 && decimal.DecCalc.Div96ByConst(ref high64, ref low, 100U))
				{
					scale -= 2;
				}
				if ((low & 1U) == 0U && scale >= 1 && decimal.DecCalc.Div96ByConst(ref high64, ref low, 10U))
				{
					scale--;
				}
			}

			// Token: 0x06001D1D RID: 7453 RVA: 0x0006B374 File Offset: 0x00069574
			private static uint Div96By64(ref decimal.DecCalc.Buf12 bufNum, ulong den)
			{
				uint u = bufNum.U2;
				if (u == 0U)
				{
					ulong num = bufNum.Low64;
					if (num < den)
					{
						return 0U;
					}
					uint num2 = (uint)(num / den);
					num -= (ulong)num2 * den;
					bufNum.Low64 = num;
					return num2;
				}
				else
				{
					uint num3 = (uint)(den >> 32);
					ulong num;
					uint num2;
					if (u >= num3)
					{
						num = bufNum.Low64;
						num -= den << 32;
						num2 = 0U;
						do
						{
							num2 -= 1U;
							num += den;
						}
						while (num >= den);
						bufNum.Low64 = num;
						return num2;
					}
					ulong high = bufNum.High64;
					if (high < (ulong)num3)
					{
						return 0U;
					}
					num2 = (uint)(high / (ulong)num3);
					num = ((ulong)bufNum.U0 | high - (ulong)(num2 * num3) << 32);
					ulong num4 = decimal.DecCalc.UInt32x32To64(num2, (uint)den);
					num -= num4;
					if (num > ~num4)
					{
						do
						{
							num2 -= 1U;
							num += den;
						}
						while (num >= den);
					}
					bufNum.Low64 = num;
					return num2;
				}
			}

			// Token: 0x06001D1E RID: 7454 RVA: 0x0006B430 File Offset: 0x00069630
			private static uint Div128By96(ref decimal.DecCalc.Buf16 bufNum, ref decimal.DecCalc.Buf12 bufDen)
			{
				ulong high = bufNum.High64;
				uint u = bufDen.U2;
				if (high < (ulong)u)
				{
					return 0U;
				}
				uint num = (uint)(high / (ulong)u);
				uint num2 = (uint)high - num * u;
				ulong num3 = decimal.DecCalc.UInt32x32To64(num, bufDen.U0);
				ulong num4 = decimal.DecCalc.UInt32x32To64(num, bufDen.U1);
				num4 += num3 >> 32;
				num3 = ((ulong)((uint)num3) | num4 << 32);
				num4 >>= 32;
				ulong num5 = bufNum.Low64;
				num5 -= num3;
				num2 -= (uint)num4;
				if (num5 > ~num3)
				{
					num2 -= 1U;
					if (num2 < ~(uint)num4)
					{
						goto IL_B4;
					}
				}
				else if (num2 <= ~(uint)num4)
				{
					goto IL_B4;
				}
				num3 = bufDen.Low64;
				do
				{
					num -= 1U;
					num5 += num3;
					num2 += u;
				}
				while ((num5 >= num3 || num2++ >= u) && num2 >= u);
				IL_B4:
				bufNum.Low64 = num5;
				bufNum.U2 = num2;
				return num;
			}

			// Token: 0x06001D1F RID: 7455 RVA: 0x0006B504 File Offset: 0x00069704
			private static uint IncreaseScale(ref decimal.DecCalc.Buf12 bufNum, uint power)
			{
				ulong num = decimal.DecCalc.UInt32x32To64(bufNum.U0, power);
				bufNum.U0 = (uint)num;
				num >>= 32;
				num += decimal.DecCalc.UInt32x32To64(bufNum.U1, power);
				bufNum.U1 = (uint)num;
				num >>= 32;
				num += decimal.DecCalc.UInt32x32To64(bufNum.U2, power);
				bufNum.U2 = (uint)num;
				return (uint)(num >> 32);
			}

			// Token: 0x06001D20 RID: 7456 RVA: 0x0006B564 File Offset: 0x00069764
			private static void IncreaseScale64(ref decimal.DecCalc.Buf12 bufNum, uint power)
			{
				ulong num = decimal.DecCalc.UInt32x32To64(bufNum.U0, power);
				bufNum.U0 = (uint)num;
				num >>= 32;
				num += decimal.DecCalc.UInt32x32To64(bufNum.U1, power);
				bufNum.High64 = num;
			}

			// Token: 0x06001D21 RID: 7457 RVA: 0x0006B5A4 File Offset: 0x000697A4
			private unsafe static int ScaleResult(decimal.DecCalc.Buf24* bufRes, uint hiRes, int scale)
			{
				int num = 0;
				if (hiRes > 2U)
				{
					num = (int)(hiRes * 32U - 64U - 1U);
					num -= decimal.DecCalc.LeadingZeroCount(*(uint*)(bufRes + (ulong)hiRes * 4UL / (ulong)sizeof(decimal.DecCalc.Buf24)));
					num = (num * 77 >> 8) + 1;
					if (num > scale)
					{
						goto IL_1CC;
					}
				}
				if (num < scale - 28)
				{
					num = scale - 28;
				}
				if (num != 0)
				{
					scale -= num;
					uint num2 = 0U;
					uint num3 = 0U;
					for (;;)
					{
						num2 |= num3;
						uint num5;
						uint num4;
						switch (num)
						{
						case 1:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 10U);
							break;
						case 2:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 100U);
							break;
						case 3:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 1000U);
							break;
						case 4:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 10000U);
							break;
						case 5:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 100000U);
							break;
						case 6:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 1000000U);
							break;
						case 7:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 10000000U);
							break;
						case 8:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 100000000U);
							break;
						default:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 1000000000U);
							break;
						}
						*(int*)(bufRes + (ulong)hiRes * 4UL / (ulong)sizeof(decimal.DecCalc.Buf24)) = (int)num5;
						if (num5 == 0U && hiRes != 0U)
						{
							hiRes -= 1U;
						}
						num -= 9;
						if (num <= 0)
						{
							if (hiRes > 2U)
							{
								if (scale == 0)
								{
									goto IL_1CC;
								}
								num = 1;
								scale--;
							}
							else
							{
								num4 >>= 1;
								if (num4 > num3 || (num4 >= num3 && ((*(uint*)bufRes & 1U) | num2) == 0U))
								{
									break;
								}
								uint num6 = *(uint*)bufRes + 1U;
								*(int*)bufRes = (int)num6;
								if (num6 != 0U)
								{
									break;
								}
								uint num7 = 0U;
								do
								{
									decimal.DecCalc.Buf24* ptr = bufRes + (ulong)(num7 += 1U) * 4UL / (ulong)sizeof(decimal.DecCalc.Buf24);
									num6 = *(uint*)ptr + 1U;
									*(int*)ptr = (int)num6;
								}
								while (num6 == 0U);
								if (num7 <= 2U)
								{
									break;
								}
								if (scale == 0)
								{
									goto IL_1CC;
								}
								hiRes = num7;
								num2 = 0U;
								num3 = 0U;
								num = 1;
								scale--;
							}
						}
					}
				}
				return scale;
				IL_1CC:
				throw new OverflowException("Value was either too large or too small for a Decimal.");
			}

			// Token: 0x06001D22 RID: 7458 RVA: 0x0006B788 File Offset: 0x00069988
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private unsafe static uint DivByConst(uint* result, uint hiRes, out uint quotient, out uint remainder, uint power)
			{
				uint num = result[(ulong)hiRes * 4UL / 4UL];
				remainder = num - (quotient = num / power) * power;
				for (uint num2 = hiRes - 1U; num2 >= 0U; num2 -= 1U)
				{
					ulong num3 = (ulong)result[(ulong)num2 * 4UL / 4UL] + ((ulong)remainder << 32);
					remainder = (uint)num3 - (result[(ulong)num2 * 4UL / 4UL] = (uint)(num3 / (ulong)power)) * power;
				}
				return power;
			}

			// Token: 0x06001D23 RID: 7459 RVA: 0x0006B7EC File Offset: 0x000699EC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static int LeadingZeroCount(uint value)
			{
				int num = 1;
				if ((value & 4294901760U) == 0U)
				{
					value <<= 16;
					num += 16;
				}
				if ((value & 4278190080U) == 0U)
				{
					value <<= 8;
					num += 8;
				}
				if ((value & 4026531840U) == 0U)
				{
					value <<= 4;
					num += 4;
				}
				if ((value & 3221225472U) == 0U)
				{
					value <<= 2;
					num += 2;
				}
				return num + ((int)value >> 31);
			}

			// Token: 0x06001D24 RID: 7460 RVA: 0x0006B84C File Offset: 0x00069A4C
			private static int OverflowUnscale(ref decimal.DecCalc.Buf12 bufQuo, int scale, bool sticky)
			{
				if (--scale < 0)
				{
					throw new OverflowException("Value was either too large or too small for a Decimal.");
				}
				bufQuo.U2 = 429496729U;
				ulong num = 25769803776UL + (ulong)bufQuo.U1;
				uint num2 = (uint)(num / 10UL);
				bufQuo.U1 = num2;
				ulong num3 = (num - (ulong)(num2 * 10U) << 32) + (ulong)bufQuo.U0;
				num2 = (uint)(num3 / 10UL);
				bufQuo.U0 = num2;
				uint num4 = (uint)(num3 - (ulong)(num2 * 10U));
				if (num4 > 5U || (num4 == 5U && (sticky || (bufQuo.U0 & 1U) != 0U)))
				{
					decimal.DecCalc.Add32To96(ref bufQuo, 1U);
				}
				return scale;
			}

			// Token: 0x06001D25 RID: 7461 RVA: 0x0006B8DC File Offset: 0x00069ADC
			private static int SearchScale(ref decimal.DecCalc.Buf12 bufQuo, int scale)
			{
				uint u = bufQuo.U2;
				ulong low = bufQuo.Low64;
				int num = 0;
				if (u <= 429496729U)
				{
					decimal.DecCalc.PowerOvfl[] powerOvflValues = decimal.DecCalc.PowerOvflValues;
					if (scale > 19)
					{
						num = 28 - scale;
						if (u < powerOvflValues[num - 1].Hi)
						{
							goto IL_D1;
						}
					}
					else if (u < 4U || (u == 4U && low <= 5441186219426131129UL))
					{
						return 9;
					}
					if (u > 42949U)
					{
						if (u > 4294967U)
						{
							num = 2;
							if (u > 42949672U)
							{
								num--;
							}
						}
						else
						{
							num = 4;
							if (u > 429496U)
							{
								num--;
							}
						}
					}
					else if (u > 429U)
					{
						num = 6;
						if (u > 4294U)
						{
							num--;
						}
					}
					else
					{
						num = 8;
						if (u > 42U)
						{
							num--;
						}
					}
					if (u == powerOvflValues[num - 1].Hi && low > powerOvflValues[num - 1].MidLo)
					{
						num--;
					}
				}
				IL_D1:
				if (num + scale < 0)
				{
					throw new OverflowException("Value was either too large or too small for a Decimal.");
				}
				return num;
			}

			// Token: 0x06001D26 RID: 7462 RVA: 0x0006B9CC File Offset: 0x00069BCC
			private static bool Add32To96(ref decimal.DecCalc.Buf12 bufNum, uint value)
			{
				if ((bufNum.Low64 += (ulong)value) < (ulong)value)
				{
					uint num = bufNum.U2 + 1U;
					bufNum.U2 = num;
					if (num == 0U)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06001D27 RID: 7463 RVA: 0x0006BA04 File Offset: 0x00069C04
			internal unsafe static void DecAddSub(ref decimal.DecCalc d1, ref decimal.DecCalc d2, bool sign)
			{
				ulong num = d1.Low64;
				uint num2 = d1.High;
				uint num3 = d1.uflags;
				uint num4 = d2.uflags;
				uint num5 = num4 ^ num3;
				sign ^= ((num5 & 2147483648U) > 0U);
				if ((num5 & 16711680U) != 0U)
				{
					uint num6 = num3;
					num3 = ((num4 & 16711680U) | (num3 & 2147483648U));
					int i = (int)(num3 - num6) >> 16;
					if (i < 0)
					{
						i = -i;
						num3 = num6;
						if (sign)
						{
							num3 ^= 2147483648U;
						}
						num = d2.Low64;
						num2 = d2.High;
						d2 = d1;
					}
					ulong num9;
					if (num2 == 0U)
					{
						if (num <= (ulong)-1)
						{
							if ((uint)num == 0U)
							{
								uint num7 = num3 & 2147483648U;
								if (sign)
								{
									num7 ^= 2147483648U;
								}
								d1 = d2;
								d1.uflags = ((d2.uflags & 16711680U) | num7);
								return;
							}
							while (i > 9)
							{
								i -= 9;
								num = decimal.DecCalc.UInt32x32To64((uint)num, 1000000000U);
								if (num > (ulong)-1)
								{
									goto IL_106;
								}
							}
							num = decimal.DecCalc.UInt32x32To64((uint)num, decimal.DecCalc.s_powers10[i]);
							goto IL_441;
						}
						do
						{
							IL_106:
							uint b = 1000000000U;
							if (i < 9)
							{
								b = decimal.DecCalc.s_powers10[i];
							}
							ulong num8 = decimal.DecCalc.UInt32x32To64((uint)num, b);
							num9 = decimal.DecCalc.UInt32x32To64((uint)(num >> 32), b) + (num8 >> 32);
							num = (ulong)((uint)num8) + (num9 << 32);
							num2 = (uint)(num9 >> 32);
							if ((i -= 9) <= 0)
							{
								goto IL_441;
							}
						}
						while (num2 == 0U);
					}
					do
					{
						uint b = 1000000000U;
						if (i < 9)
						{
							b = decimal.DecCalc.s_powers10[i];
						}
						ulong num8 = decimal.DecCalc.UInt32x32To64((uint)num, b);
						num9 = decimal.DecCalc.UInt32x32To64((uint)(num >> 32), b) + (num8 >> 32);
						num = (ulong)((uint)num8) + (num9 << 32);
						num9 >>= 32;
						num9 += decimal.DecCalc.UInt32x32To64(num2, b);
						i -= 9;
						if (num9 > (ulong)-1)
						{
							goto IL_1CF;
						}
						num2 = (uint)num9;
					}
					while (i > 0);
					goto IL_441;
					IL_1CF:
					decimal.DecCalc.Buf24 buf;
					buf.Low64 = num;
					buf.Mid64 = num9;
					uint num10 = 3U;
					while (i > 0)
					{
						uint b = 1000000000U;
						if (i < 9)
						{
							b = decimal.DecCalc.s_powers10[i];
						}
						num9 = 0UL;
						uint* ptr = (uint*)(&buf);
						uint num11 = 0U;
						do
						{
							num9 += decimal.DecCalc.UInt32x32To64(ptr[(ulong)num11 * 4UL / 4UL], b);
							ptr[(ulong)num11 * 4UL / 4UL] = (uint)num9;
							num11 += 1U;
							num9 >>= 32;
						}
						while (num11 <= num10);
						if ((uint)num9 != 0U)
						{
							ptr[(IntPtr)((ulong)(num10 += 1U) * 4UL)] = (uint)num9;
						}
						i -= 9;
					}
					num9 = buf.Low64;
					num = d2.Low64;
					uint u = buf.U2;
					num2 = d2.High;
					if (sign)
					{
						num = num9 - num;
						num2 = u - num2;
						if (num > num9)
						{
							num2 -= 1U;
							if (num2 < u)
							{
								goto IL_34C;
							}
						}
						else if (num2 <= u)
						{
							goto IL_34C;
						}
						uint* ptr2 = (uint*)(&buf);
						uint num12 = 3U;
						uint num13;
						do
						{
							uint* ptr3 = ptr2 + (IntPtr)((ulong)num12++ * 4UL);
							num13 = *ptr3;
							*ptr3 = num13 - 1U;
						}
						while (num13 == 0U);
						if (ptr2[(ulong)num10 * 4UL / 4UL] == 0U && (num10 -= 1U) <= 2U)
						{
							goto IL_4AA;
						}
					}
					else
					{
						num += num9;
						num2 += u;
						if (num < num9)
						{
							num2 += 1U;
							if (num2 > u)
							{
								goto IL_34C;
							}
						}
						else if (num2 >= u)
						{
							goto IL_34C;
						}
						uint* ptr4 = (uint*)(&buf);
						uint num14 = 3U;
						do
						{
							uint* ptr5 = ptr4 + (IntPtr)((ulong)num14++ * 4UL);
							uint num13 = *ptr5 + 1U;
							*ptr5 = num13;
							if (num13 != 0U)
							{
								goto IL_34C;
							}
						}
						while (num10 >= num14);
						ptr4[(ulong)num14 * 4UL / 4UL] = 1U;
						num10 = num14;
					}
					IL_34C:
					buf.Low64 = num;
					buf.U2 = num2;
					i = decimal.DecCalc.ScaleResult(&buf, num10, (int)((byte)(num3 >> 16)));
					num3 = ((num3 & 4278255615U) | (uint)((uint)i << 16));
					num = buf.Low64;
					num2 = buf.U2;
					goto IL_4AA;
				}
				IL_441:
				ulong num15 = num;
				uint num16 = num2;
				if (sign)
				{
					num = num15 - d2.Low64;
					num2 = num16 - d2.High;
					if (num > num15)
					{
						num2 -= 1U;
						if (num2 < num16)
						{
							goto IL_4AA;
						}
					}
					else if (num2 <= num16)
					{
						goto IL_4AA;
					}
					num3 ^= 2147483648U;
					num2 = ~num2;
					num = -num;
					if (num == 0UL)
					{
						num2 += 1U;
					}
				}
				else
				{
					num = num15 + d2.Low64;
					num2 = num16 + d2.High;
					if (num < num15)
					{
						num2 += 1U;
						if (num2 > num16)
						{
							goto IL_4AA;
						}
					}
					else if (num2 >= num16)
					{
						goto IL_4AA;
					}
					if ((num3 & 16711680U) == 0U)
					{
						throw new OverflowException("Value was either too large or too small for a Decimal.");
					}
					num3 -= 65536U;
					ulong num17 = (ulong)num2 + 4294967296UL;
					num2 = (uint)(num17 / 10UL);
					ulong num18 = (num17 - (ulong)(num2 * 10U) << 32) + (num >> 32);
					uint num19 = (uint)(num18 / 10UL);
					ulong num20 = (num18 - (ulong)(num19 * 10U) << 32) + (ulong)((uint)num);
					num = (ulong)num19;
					num <<= 32;
					num19 = (uint)(num20 / 10UL);
					num += (ulong)num19;
					num19 = (uint)num20 - num19 * 10U;
					if (num19 >= 5U && (num19 > 5U || (num & 1UL) != 0UL) && (num += 1UL) == 0UL)
					{
						num2 += 1U;
					}
				}
				IL_4AA:
				d1.uflags = num3;
				d1.High = num2;
				d1.Low64 = num;
			}

			// Token: 0x06001D28 RID: 7464 RVA: 0x0006BED0 File Offset: 0x0006A0D0
			internal static long VarCyFromDec(ref decimal.DecCalc pdecIn)
			{
				int num = pdecIn.Scale - 4;
				long num4;
				if (num < 0)
				{
					if (pdecIn.High != 0U)
					{
						goto IL_93;
					}
					uint a = decimal.DecCalc.s_powers10[-num];
					ulong num2 = decimal.DecCalc.UInt32x32To64(a, pdecIn.Mid);
					if (num2 > (ulong)-1)
					{
						goto IL_93;
					}
					ulong num3 = decimal.DecCalc.UInt32x32To64(a, pdecIn.Low);
					num3 += (num2 <<= 32);
					if (num3 < num2)
					{
						goto IL_93;
					}
					num4 = (long)num3;
				}
				else
				{
					if (num != 0)
					{
						decimal.DecCalc.InternalRound(ref pdecIn, (uint)num, decimal.DecCalc.RoundingMode.ToEven);
					}
					if (pdecIn.High != 0U)
					{
						goto IL_93;
					}
					num4 = (long)pdecIn.Low64;
				}
				if (num4 >= 0L || (num4 == -9223372036854775808L && pdecIn.IsNegative))
				{
					if (pdecIn.IsNegative)
					{
						num4 = -num4;
					}
					return num4;
				}
				IL_93:
				throw new OverflowException("Value was either too large or too small for a Currency.");
			}

			// Token: 0x06001D29 RID: 7465 RVA: 0x0006BF7C File Offset: 0x0006A17C
			internal static int VarDecCmp(in decimal d1, in decimal d2)
			{
				if ((d2.Low | d2.Mid | d2.High) == 0U)
				{
					if ((d1.Low | d1.Mid | d1.High) == 0U)
					{
						return 0;
					}
					return d1.flags >> 31 | 1;
				}
				else
				{
					if ((d1.Low | d1.Mid | d1.High) == 0U)
					{
						return -(d2.flags >> 31 | 1);
					}
					int num = (d1.flags >> 31) - (d2.flags >> 31);
					if (num != 0)
					{
						return num;
					}
					return decimal.DecCalc.VarDecCmpSub(d1, d2);
				}
			}

			// Token: 0x06001D2A RID: 7466 RVA: 0x0006C008 File Offset: 0x0006A208
			private static int VarDecCmpSub(in decimal d1, in decimal d2)
			{
				int flags = d2.flags;
				int num = flags >> 31 | 1;
				int num2 = flags - d1.flags;
				ulong num3 = d1.Low64;
				uint num4 = d1.High;
				ulong num5 = d2.Low64;
				uint num6 = d2.High;
				if (num2 != 0)
				{
					num2 >>= 16;
					if (num2 < 0)
					{
						num2 = -num2;
						num = -num;
						ulong num7 = num3;
						num3 = num5;
						num5 = num7;
						uint num8 = num4;
						num4 = num6;
						num6 = num8;
					}
					for (;;)
					{
						uint b = (num2 >= 9) ? 1000000000U : decimal.DecCalc.s_powers10[num2];
						ulong num9 = decimal.DecCalc.UInt32x32To64((uint)num3, b);
						ulong num10 = decimal.DecCalc.UInt32x32To64((uint)(num3 >> 32), b) + (num9 >> 32);
						num3 = (ulong)((uint)num9) + (num10 << 32);
						num10 >>= 32;
						num10 += decimal.DecCalc.UInt32x32To64(num4, b);
						if (num10 > (ulong)-1)
						{
							break;
						}
						num4 = (uint)num10;
						if ((num2 -= 9) <= 0)
						{
							goto IL_BC;
						}
					}
					return num;
				}
				IL_BC:
				uint num11 = num4 - num6;
				if (num11 != 0U)
				{
					if (num11 > num4)
					{
						num = -num;
					}
					return num;
				}
				ulong num12 = num3 - num5;
				if (num12 == 0UL)
				{
					num = 0;
				}
				else if (num12 > num3)
				{
					num = -num;
				}
				return num;
			}

			// Token: 0x06001D2B RID: 7467 RVA: 0x0006C0FC File Offset: 0x0006A2FC
			internal unsafe static void VarDecMul(ref decimal.DecCalc d1, ref decimal.DecCalc d2)
			{
				int num = (int)((byte)(d1.uflags + d2.uflags >> 16));
				decimal.DecCalc.Buf24 buf;
				uint num6;
				if ((d1.High | d1.Mid) == 0U)
				{
					ulong num4;
					if ((d2.High | d2.Mid) == 0U)
					{
						ulong num2 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Low);
						if (num > 28)
						{
							if (num > 47)
							{
								goto IL_3CD;
							}
							num -= 29;
							ulong num3 = decimal.DecCalc.s_ulongPowers10[num];
							num4 = num2 / num3;
							ulong num5 = num2 - num4 * num3;
							num2 = num4;
							num3 >>= 1;
							if (num5 >= num3 && (num5 > num3 || ((uint)num2 & 1U) > 0U))
							{
								num2 += 1UL;
							}
							num = 28;
						}
						d1.Low64 = num2;
						d1.uflags = (((d2.uflags ^ d1.uflags) & 2147483648U) | (uint)((uint)num << 16));
						return;
					}
					num4 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Low);
					buf.U0 = (uint)num4;
					num4 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Mid) + (num4 >> 32);
					buf.U1 = (uint)num4;
					num4 >>= 32;
					if (d2.High != 0U)
					{
						num4 += decimal.DecCalc.UInt32x32To64(d1.Low, d2.High);
						if (num4 > (ulong)-1)
						{
							buf.Mid64 = num4;
							num6 = 3U;
							goto IL_381;
						}
					}
					if ((uint)num4 != 0U)
					{
						buf.U2 = (uint)num4;
						num6 = 2U;
						goto IL_381;
					}
					num6 = 1U;
				}
				else if ((d2.High | d2.Mid) == 0U)
				{
					ulong num4 = decimal.DecCalc.UInt32x32To64(d2.Low, d1.Low);
					buf.U0 = (uint)num4;
					num4 = decimal.DecCalc.UInt32x32To64(d2.Low, d1.Mid) + (num4 >> 32);
					buf.U1 = (uint)num4;
					num4 >>= 32;
					if (d1.High != 0U)
					{
						num4 += decimal.DecCalc.UInt32x32To64(d2.Low, d1.High);
						if (num4 > (ulong)-1)
						{
							buf.Mid64 = num4;
							num6 = 3U;
							goto IL_381;
						}
					}
					if ((uint)num4 != 0U)
					{
						buf.U2 = (uint)num4;
						num6 = 2U;
						goto IL_381;
					}
					num6 = 1U;
				}
				else
				{
					ulong num4 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Low);
					buf.U0 = (uint)num4;
					ulong num7 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Mid) + (num4 >> 32);
					num4 = decimal.DecCalc.UInt32x32To64(d1.Mid, d2.Low);
					num4 += num7;
					buf.U1 = (uint)num4;
					if (num4 < num7)
					{
						num7 = (num4 >> 32 | 4294967296UL);
					}
					else
					{
						num7 = num4 >> 32;
					}
					num4 = decimal.DecCalc.UInt32x32To64(d1.Mid, d2.Mid) + num7;
					if ((d1.High | d2.High) > 0U)
					{
						num7 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.High);
						num4 += num7;
						uint num8 = 0U;
						if (num4 < num7)
						{
							num8 = 1U;
						}
						num7 = decimal.DecCalc.UInt32x32To64(d1.High, d2.Low);
						num4 += num7;
						buf.U2 = (uint)num4;
						if (num4 < num7)
						{
							num8 += 1U;
						}
						num7 = ((ulong)num8 << 32 | num4 >> 32);
						num4 = decimal.DecCalc.UInt32x32To64(d1.Mid, d2.High);
						num4 += num7;
						num8 = 0U;
						if (num4 < num7)
						{
							num8 = 1U;
						}
						num7 = decimal.DecCalc.UInt32x32To64(d1.High, d2.Mid);
						num4 += num7;
						buf.U3 = (uint)num4;
						if (num4 < num7)
						{
							num8 += 1U;
						}
						num4 = ((ulong)num8 << 32 | num4 >> 32);
						buf.High64 = decimal.DecCalc.UInt32x32To64(d1.High, d2.High) + num4;
						num6 = 5U;
					}
					else if (num4 != 0UL)
					{
						buf.Mid64 = num4;
						num6 = 3U;
					}
					else
					{
						num6 = 1U;
					}
				}
				uint* ptr = (uint*)(&buf);
				while (ptr[num6] == 0U)
				{
					if (num6 == 0U)
					{
						goto IL_3CD;
					}
					num6 -= 1U;
				}
				IL_381:
				if (num6 > 2U || num > 28)
				{
					num = decimal.DecCalc.ScaleResult(&buf, num6, num);
				}
				d1.Low64 = buf.Low64;
				d1.High = buf.U2;
				d1.uflags = (((d2.uflags ^ d1.uflags) & 2147483648U) | (uint)((uint)num << 16));
				return;
				IL_3CD:
				d1 = default(decimal.DecCalc);
			}

			// Token: 0x06001D2C RID: 7468 RVA: 0x0006C4E0 File Offset: 0x0006A6E0
			internal static void VarDecFromR4(float input, out decimal.DecCalc result)
			{
				result = default(decimal.DecCalc);
				int num = (int)(decimal.DecCalc.GetExponent(input) - 126U);
				if (num < -94)
				{
					return;
				}
				if (num > 96)
				{
					throw new OverflowException("Value was either too large or too small for a Decimal.");
				}
				uint num2 = 0U;
				if (input < 0f)
				{
					input = -input;
					num2 = 2147483648U;
				}
				double num3 = (double)input;
				int num4 = 6 - (num * 19728 >> 16);
				if (num4 >= 0)
				{
					if (num4 > 28)
					{
						num4 = 28;
					}
					num3 *= decimal.DecCalc.s_doublePowers10[num4];
				}
				else if (num4 != -1 || num3 >= 10000000.0)
				{
					num3 /= decimal.DecCalc.s_doublePowers10[-num4];
				}
				else
				{
					num4 = 0;
				}
				if (num3 < 1000000.0 && num4 < 28)
				{
					num3 *= 10.0;
					num4++;
				}
				uint num5 = (uint)((int)num3);
				num3 -= (double)num5;
				if (num3 > 0.5 || (num3 == 0.5 && (num5 & 1U) != 0U))
				{
					num5 += 1U;
				}
				if (num5 == 0U)
				{
					return;
				}
				if (num4 < 0)
				{
					num4 = -num4;
					if (num4 < 10)
					{
						result.Low64 = decimal.DecCalc.UInt32x32To64(num5, decimal.DecCalc.s_powers10[num4]);
					}
					else if (num4 > 18)
					{
						decimal.DecCalc.UInt64x64To128(decimal.DecCalc.UInt32x32To64(num5, decimal.DecCalc.s_powers10[num4 - 18]), 1000000000000000000UL, ref result);
					}
					else
					{
						ulong num6 = decimal.DecCalc.UInt32x32To64(num5, decimal.DecCalc.s_powers10[num4 - 9]);
						ulong num7 = decimal.DecCalc.UInt32x32To64(1000000000U, (uint)(num6 >> 32));
						num6 = decimal.DecCalc.UInt32x32To64(1000000000U, (uint)num6);
						result.Low = (uint)num6;
						num7 += num6 >> 32;
						result.Mid = (uint)num7;
						num7 >>= 32;
						result.High = (uint)num7;
					}
				}
				else
				{
					int num8 = num4;
					if (num8 > 6)
					{
						num8 = 6;
					}
					if ((num5 & 15U) == 0U && num8 >= 4)
					{
						uint num9 = num5 / 10000U;
						if (num5 == num9 * 10000U)
						{
							num5 = num9;
							num4 -= 4;
							num8 -= 4;
						}
					}
					if ((num5 & 3U) == 0U && num8 >= 2)
					{
						uint num10 = num5 / 100U;
						if (num5 == num10 * 100U)
						{
							num5 = num10;
							num4 -= 2;
							num8 -= 2;
						}
					}
					if ((num5 & 1U) == 0U && num8 >= 1)
					{
						uint num11 = num5 / 10U;
						if (num5 == num11 * 10U)
						{
							num5 = num11;
							num4--;
						}
					}
					num2 |= (uint)((uint)num4 << 16);
					result.Low = num5;
				}
				result.uflags = num2;
			}

			// Token: 0x06001D2D RID: 7469 RVA: 0x0006C718 File Offset: 0x0006A918
			internal static void VarDecFromR8(double input, out decimal.DecCalc result)
			{
				result = default(decimal.DecCalc);
				int num = (int)(decimal.DecCalc.GetExponent(input) - 1022U);
				if (num < -94)
				{
					return;
				}
				if (num > 96)
				{
					throw new OverflowException("Value was either too large or too small for a Decimal.");
				}
				uint num2 = 0U;
				if (input < 0.0)
				{
					input = -input;
					num2 = 2147483648U;
				}
				double num3 = input;
				int num4 = 14 - (num * 19728 >> 16);
				if (num4 >= 0)
				{
					if (num4 > 28)
					{
						num4 = 28;
					}
					num3 *= decimal.DecCalc.s_doublePowers10[num4];
				}
				else if (num4 != -1 || num3 >= 1000000000000000.0)
				{
					num3 /= decimal.DecCalc.s_doublePowers10[-num4];
				}
				else
				{
					num4 = 0;
				}
				if (num3 < 100000000000000.0 && num4 < 28)
				{
					num3 *= 10.0;
					num4++;
				}
				ulong num5 = (ulong)((long)num3);
				num3 -= (double)num5;
				if (num3 > 0.5 || (num3 == 0.5 && (num5 & 1UL) != 0UL))
				{
					num5 += 1UL;
				}
				if (num5 == 0UL)
				{
					return;
				}
				if (num4 < 0)
				{
					num4 = -num4;
					if (num4 < 10)
					{
						uint b = decimal.DecCalc.s_powers10[num4];
						ulong num6 = decimal.DecCalc.UInt32x32To64((uint)num5, b);
						ulong num7 = decimal.DecCalc.UInt32x32To64((uint)(num5 >> 32), b);
						result.Low = (uint)num6;
						num7 += num6 >> 32;
						result.Mid = (uint)num7;
						num7 >>= 32;
						result.High = (uint)num7;
					}
					else
					{
						decimal.DecCalc.UInt64x64To128(num5, decimal.DecCalc.s_ulongPowers10[num4 - 1], ref result);
					}
				}
				else
				{
					int num8 = num4;
					if (num8 > 14)
					{
						num8 = 14;
					}
					if ((byte)num5 == 0 && num8 >= 8)
					{
						ulong num9 = num5 / 100000000UL;
						if ((uint)num5 == (uint)(num9 * 100000000UL))
						{
							num5 = num9;
							num4 -= 8;
							num8 -= 8;
						}
					}
					if (((uint)num5 & 15U) == 0U && num8 >= 4)
					{
						ulong num10 = num5 / 10000UL;
						if ((uint)num5 == (uint)(num10 * 10000UL))
						{
							num5 = num10;
							num4 -= 4;
							num8 -= 4;
						}
					}
					if (((uint)num5 & 3U) == 0U && num8 >= 2)
					{
						ulong num11 = num5 / 100UL;
						if ((uint)num5 == (uint)(num11 * 100UL))
						{
							num5 = num11;
							num4 -= 2;
							num8 -= 2;
						}
					}
					if (((uint)num5 & 1U) == 0U && num8 >= 1)
					{
						ulong num12 = num5 / 10UL;
						if ((uint)num5 == (uint)(num12 * 10UL))
						{
							num5 = num12;
							num4--;
						}
					}
					num2 |= (uint)((uint)num4 << 16);
					result.Low64 = num5;
				}
				result.uflags = num2;
			}

			// Token: 0x06001D2E RID: 7470 RVA: 0x0006C95B File Offset: 0x0006AB5B
			internal static float VarR4FromDec(in decimal value)
			{
				return (float)decimal.DecCalc.VarR8FromDec(value);
			}

			// Token: 0x06001D2F RID: 7471 RVA: 0x0006C964 File Offset: 0x0006AB64
			internal static double VarR8FromDec(in decimal value)
			{
				double num = (value.Low64 + value.High * 1.8446744073709552E+19) / decimal.DecCalc.s_doublePowers10[value.Scale];
				if (value.IsNegative)
				{
					num = -num;
				}
				return num;
			}

			// Token: 0x06001D30 RID: 7472 RVA: 0x0006C9A8 File Offset: 0x0006ABA8
			internal static int GetHashCode(in decimal d)
			{
				if ((d.Low | d.Mid | d.High) == 0U)
				{
					return 0;
				}
				uint num = (uint)d.flags;
				if ((num & 16711680U) == 0U || (d.Low & 1U) != 0U)
				{
					return (int)(num ^ d.High ^ d.Mid ^ d.Low);
				}
				int num2 = (int)((byte)(num >> 16));
				uint low = d.Low;
				ulong num3 = (ulong)d.High << 32 | (ulong)d.Mid;
				decimal.DecCalc.Unscale(ref low, ref num3, ref num2);
				num = ((num & 4278255615U) | (uint)((uint)num2 << 16));
				return (int)(num ^ (uint)(num3 >> 32) ^ (uint)num3 ^ low);
			}

			// Token: 0x06001D31 RID: 7473 RVA: 0x0006CA44 File Offset: 0x0006AC44
			internal unsafe static void VarDecDiv(ref decimal.DecCalc d1, ref decimal.DecCalc d2)
			{
				int num = (int)((sbyte)(d1.uflags - d2.uflags >> 16));
				bool flag = false;
				decimal.DecCalc.Buf12 buf;
				if ((d2.High | d2.Mid) == 0U)
				{
					uint low = d2.Low;
					if (low == 0U)
					{
						throw new DivideByZeroException();
					}
					buf.Low64 = d1.Low64;
					buf.U2 = d1.High;
					uint num2 = decimal.DecCalc.Div96By32(ref buf, low);
					for (;;)
					{
						int num3;
						if (num2 == 0U)
						{
							if (num >= 0)
							{
								goto IL_3D2;
							}
							num3 = Math.Min(9, -num);
						}
						else
						{
							flag = true;
							if (num == 28 || (num3 = decimal.DecCalc.SearchScale(ref buf, num)) == 0)
							{
								break;
							}
						}
						uint num4 = decimal.DecCalc.s_powers10[num3];
						num += num3;
						if (decimal.DecCalc.IncreaseScale(ref buf, num4) != 0U)
						{
							goto IL_48A;
						}
						ulong num5 = decimal.DecCalc.UInt32x32To64(num2, num4);
						uint num6 = (uint)(num5 / (ulong)low);
						num2 = (uint)num5 - num6 * low;
						if (!decimal.DecCalc.Add32To96(ref buf, num6))
						{
							goto Block_11;
						}
					}
					uint num7 = num2 << 1;
					if (num7 < num2)
					{
						goto IL_449;
					}
					if (num7 < low)
					{
						goto IL_3D2;
					}
					if (num7 > low)
					{
						goto IL_449;
					}
					if ((buf.U0 & 1U) != 0U)
					{
						goto IL_449;
					}
					goto IL_3D2;
					Block_11:
					num = decimal.DecCalc.OverflowUnscale(ref buf, num, num2 > 0U);
				}
				else
				{
					uint num7 = d2.High;
					if (num7 == 0U)
					{
						num7 = d2.Mid;
					}
					int num3 = decimal.DecCalc.LeadingZeroCount(num7);
					decimal.DecCalc.Buf16 buf2;
					buf2.Low64 = d1.Low64 << num3;
					buf2.High64 = (ulong)d1.Mid + ((ulong)d1.High << 32) >> 32 - num3;
					ulong num8 = d2.Low64 << num3;
					if (d2.High == 0U)
					{
						buf.U1 = decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf2.U1), num8);
						buf.U0 = decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf2), num8);
						for (;;)
						{
							if (buf2.Low64 == 0UL)
							{
								if (num >= 0)
								{
									goto IL_3D2;
								}
								num3 = Math.Min(9, -num);
							}
							else
							{
								flag = true;
								if (num == 28 || (num3 = decimal.DecCalc.SearchScale(ref buf, num)) == 0)
								{
									break;
								}
							}
							uint num4 = decimal.DecCalc.s_powers10[num3];
							num += num3;
							if (decimal.DecCalc.IncreaseScale(ref buf, num4) != 0U)
							{
								goto IL_48A;
							}
							decimal.DecCalc.IncreaseScale64(ref *(decimal.DecCalc.Buf12*)(&buf2), num4);
							num7 = decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf2), num8);
							if (!decimal.DecCalc.Add32To96(ref buf, num7))
							{
								goto Block_22;
							}
						}
						ulong num9 = buf2.Low64;
						if (num9 < 0UL || (num9 <<= 1) > num8)
						{
							goto IL_449;
						}
						if (num9 == num8 && (buf.U0 & 1U) != 0U)
						{
							goto IL_449;
						}
						goto IL_3D2;
						Block_22:
						num = decimal.DecCalc.OverflowUnscale(ref buf, num, buf2.Low64 > 0UL);
					}
					else
					{
						decimal.DecCalc.Buf12 buf3;
						buf3.Low64 = num8;
						buf3.U2 = (uint)((ulong)d2.Mid + ((ulong)d2.High << 32) >> 32 - num3);
						buf.Low64 = (ulong)decimal.DecCalc.Div128By96(ref buf2, ref buf3);
						for (;;)
						{
							if ((buf2.Low64 | (ulong)buf2.U2) == 0UL)
							{
								if (num >= 0)
								{
									goto IL_3D2;
								}
								num3 = Math.Min(9, -num);
							}
							else
							{
								flag = true;
								if (num == 28 || (num3 = decimal.DecCalc.SearchScale(ref buf, num)) == 0)
								{
									break;
								}
							}
							uint num4 = decimal.DecCalc.s_powers10[num3];
							num += num3;
							if (decimal.DecCalc.IncreaseScale(ref buf, num4) != 0U)
							{
								goto IL_48A;
							}
							buf2.U3 = decimal.DecCalc.IncreaseScale(ref *(decimal.DecCalc.Buf12*)(&buf2), num4);
							num7 = decimal.DecCalc.Div128By96(ref buf2, ref buf3);
							if (!decimal.DecCalc.Add32To96(ref buf, num7))
							{
								goto Block_33;
							}
						}
						if (buf2.U2 < 0U)
						{
							goto IL_449;
						}
						num7 = buf2.U1 >> 31;
						buf2.Low64 <<= 1;
						buf2.U2 = (buf2.U2 << 1) + num7;
						if (buf2.U2 > buf3.U2)
						{
							goto IL_449;
						}
						if (buf2.U2 != buf3.U2)
						{
							goto IL_3D2;
						}
						if (buf2.Low64 > buf3.Low64)
						{
							goto IL_449;
						}
						if (buf2.Low64 == buf3.Low64 && (buf.U0 & 1U) != 0U)
						{
							goto IL_449;
						}
						goto IL_3D2;
						Block_33:
						num = decimal.DecCalc.OverflowUnscale(ref buf, num, (buf2.Low64 | buf2.High64) > 0UL);
					}
				}
				IL_3D2:
				if (flag)
				{
					uint u = buf.U0;
					ulong high = buf.High64;
					decimal.DecCalc.Unscale(ref u, ref high, ref num);
					d1.Low = u;
					d1.Mid = (uint)high;
					d1.High = (uint)(high >> 32);
				}
				else
				{
					d1.Low64 = buf.Low64;
					d1.High = buf.U2;
				}
				d1.uflags = (((d1.uflags ^ d2.uflags) & 2147483648U) | (uint)((uint)num << 16));
				return;
				IL_449:
				ulong num10 = buf.Low64 + 1UL;
				buf.Low64 = num10;
				if (num10 != 0UL)
				{
					goto IL_3D2;
				}
				uint num11 = buf.U2 + 1U;
				buf.U2 = num11;
				if (num11 == 0U)
				{
					num = decimal.DecCalc.OverflowUnscale(ref buf, num, true);
					goto IL_3D2;
				}
				goto IL_3D2;
				IL_48A:
				throw new OverflowException("Value was either too large or too small for a Decimal.");
			}

			// Token: 0x06001D32 RID: 7474 RVA: 0x0006CEE8 File Offset: 0x0006B0E8
			internal static void VarDecMod(ref decimal.DecCalc d1, ref decimal.DecCalc d2)
			{
				if ((d2.ulo | d2.umid | d2.uhi) == 0U)
				{
					throw new DivideByZeroException();
				}
				if ((d1.ulo | d1.umid | d1.uhi) == 0U)
				{
					return;
				}
				d2.uflags = ((d2.uflags & 2147483647U) | (d1.uflags & 2147483648U));
				int num = decimal.DecCalc.VarDecCmpSub(Unsafe.As<decimal.DecCalc, decimal>(ref d1), Unsafe.As<decimal.DecCalc, decimal>(ref d2));
				if (num == 0)
				{
					d1.ulo = 0U;
					d1.umid = 0U;
					d1.uhi = 0U;
					if (d2.uflags > d1.uflags)
					{
						d1.uflags = d2.uflags;
					}
					return;
				}
				if ((num ^ (int)(d1.uflags & 2147483648U)) < 0)
				{
					return;
				}
				int num2 = (int)((sbyte)(d1.uflags - d2.uflags >> 16));
				if (num2 > 0)
				{
					do
					{
						uint num3 = (num2 >= 9) ? 1000000000U : decimal.DecCalc.s_powers10[num2];
						ulong num4 = decimal.DecCalc.UInt32x32To64(d2.Low, num3);
						d2.Low = (uint)num4;
						num4 >>= 32;
						num4 += ((ulong)d2.Mid + ((ulong)d2.High << 32)) * (ulong)num3;
						d2.Mid = (uint)num4;
						d2.High = (uint)(num4 >> 32);
					}
					while ((num2 -= 9) > 0);
					num2 = 0;
				}
				for (;;)
				{
					if (num2 < 0)
					{
						d1.uflags = d2.uflags;
						decimal.DecCalc.Buf12 buf;
						buf.Low64 = d1.Low64;
						buf.U2 = d1.High;
						uint num6;
						do
						{
							int num5 = decimal.DecCalc.SearchScale(ref buf, 28 + num2);
							if (num5 == 0)
							{
								break;
							}
							num6 = ((num5 >= 9) ? 1000000000U : decimal.DecCalc.s_powers10[num5]);
							num2 += num5;
							ulong num7 = decimal.DecCalc.UInt32x32To64(buf.U0, num6);
							buf.U0 = (uint)num7;
							num7 >>= 32;
							buf.High64 = num7 + buf.High64 * (ulong)num6;
						}
						while (num6 == 1000000000U && num2 < 0);
						d1.Low64 = buf.Low64;
						d1.High = buf.U2;
					}
					if (d1.High == 0U)
					{
						break;
					}
					if ((d2.High | d2.Mid) != 0U)
					{
						goto IL_24C;
					}
					uint low = d2.Low;
					ulong num8 = (ulong)d1.High << 32 | (ulong)d1.Mid;
					num8 = (num8 % (ulong)low << 32 | (ulong)d1.Low);
					d1.Low64 = num8 % (ulong)low;
					d1.High = 0U;
					if (num2 >= 0)
					{
						return;
					}
				}
				d1.Low64 %= d2.Low64;
				return;
				IL_24C:
				decimal.DecCalc.VarDecModFull(ref d1, ref d2, num2);
			}

			// Token: 0x06001D33 RID: 7475 RVA: 0x0006D154 File Offset: 0x0006B354
			private unsafe static void VarDecModFull(ref decimal.DecCalc d1, ref decimal.DecCalc d2, int scale)
			{
				uint num = d2.High;
				if (num == 0U)
				{
					num = d2.Mid;
				}
				int num2 = decimal.DecCalc.LeadingZeroCount(num);
				decimal.DecCalc.Buf28 buf;
				buf.Buf24.Low64 = d1.Low64 << num2;
				buf.Buf24.Mid64 = (ulong)d1.Mid + ((ulong)d1.High << 32) >> 32 - num2;
				uint num3 = 3U;
				while (scale < 0)
				{
					uint b = (scale <= -9) ? 1000000000U : decimal.DecCalc.s_powers10[-scale];
					uint* ptr = (uint*)(&buf);
					ulong num4 = decimal.DecCalc.UInt32x32To64(buf.Buf24.U0, b);
					buf.Buf24.U0 = (uint)num4;
					int num5 = 1;
					while ((long)num5 <= (long)((ulong)num3))
					{
						num4 >>= 32;
						num4 += decimal.DecCalc.UInt32x32To64(ptr[num5], b);
						ptr[num5] = (uint)num4;
						num5++;
					}
					if (num4 > 2147483647UL)
					{
						ptr[(IntPtr)((ulong)(num3 += 1U) * 4UL)] = (uint)(num4 >> 32);
					}
					scale += 9;
				}
				if (d2.High == 0U)
				{
					ulong den = d2.Low64 << num2;
					switch (num3)
					{
					case 4U:
						goto IL_15A;
					case 5U:
						break;
					case 6U:
						decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf.Buf24.U4), den);
						break;
					default:
						goto IL_16F;
					}
					decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf.Buf24.U3), den);
					IL_15A:
					decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf.Buf24.U2), den);
					IL_16F:
					decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf.Buf24.U1), den);
					decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf), den);
					d1.Low64 = buf.Buf24.Low64 >> num2;
					d1.High = 0U;
					return;
				}
				decimal.DecCalc.Buf12 buf2;
				buf2.Low64 = d2.Low64 << num2;
				buf2.U2 = (uint)((ulong)d2.Mid + ((ulong)d2.High << 32) >> 32 - num2);
				switch (num3)
				{
				case 4U:
					goto IL_225;
				case 5U:
					break;
				case 6U:
					decimal.DecCalc.Div128By96(ref *(decimal.DecCalc.Buf16*)(&buf.Buf24.U3), ref buf2);
					break;
				default:
					goto IL_23A;
				}
				decimal.DecCalc.Div128By96(ref *(decimal.DecCalc.Buf16*)(&buf.Buf24.U2), ref buf2);
				IL_225:
				decimal.DecCalc.Div128By96(ref *(decimal.DecCalc.Buf16*)(&buf.Buf24.U1), ref buf2);
				IL_23A:
				decimal.DecCalc.Div128By96(ref *(decimal.DecCalc.Buf16*)(&buf), ref buf2);
				d1.Low64 = (buf.Buf24.Low64 >> num2) + ((ulong)buf.Buf24.U2 << 32 - num2 << 32);
				d1.High = buf.Buf24.U2 >> num2;
			}

			// Token: 0x06001D34 RID: 7476 RVA: 0x0006D3EC File Offset: 0x0006B5EC
			internal static void InternalRound(ref decimal.DecCalc d, uint scale, decimal.DecCalc.RoundingMode mode)
			{
				d.uflags -= scale << 16;
				uint num = 0U;
				uint num5;
				while (scale >= 9U)
				{
					scale -= 9U;
					uint num2 = d.uhi;
					uint num4;
					if (num2 == 0U)
					{
						ulong low = d.Low64;
						ulong num3 = low / 1000000000UL;
						d.Low64 = num3;
						num4 = (uint)(low - num3 * 1000000000UL);
					}
					else
					{
						num4 = num2 - (d.uhi = num2 / 1000000000U) * 1000000000U;
						num2 = d.umid;
						if ((num2 | num4) != 0U)
						{
							num4 = num2 - (d.umid = (uint)(((ulong)num4 << 32 | (ulong)num2) / 1000000000UL)) * 1000000000U;
						}
						num2 = d.ulo;
						if ((num2 | num4) != 0U)
						{
							num4 = num2 - (d.ulo = (uint)(((ulong)num4 << 32 | (ulong)num2) / 1000000000UL)) * 1000000000U;
						}
					}
					num5 = 1000000000U;
					if (scale == 0U)
					{
						IL_194:
						if (mode != decimal.DecCalc.RoundingMode.Truncate)
						{
							if (mode == decimal.DecCalc.RoundingMode.ToEven)
							{
								num4 <<= 1;
								if ((num | (d.ulo & 1U)) != 0U)
								{
									num4 += 1U;
								}
								if (num5 >= num4)
								{
									return;
								}
							}
							else if (mode == decimal.DecCalc.RoundingMode.AwayFromZero)
							{
								num4 <<= 1;
								if (num5 > num4)
								{
									return;
								}
							}
							else if (mode == decimal.DecCalc.RoundingMode.Floor)
							{
								if ((num4 | num) == 0U)
								{
									return;
								}
								if (!d.IsNegative)
								{
									return;
								}
							}
							else if ((num4 | num) == 0U || d.IsNegative)
							{
								return;
							}
							ulong num6 = d.Low64 + 1UL;
							d.Low64 = num6;
							if (num6 == 0UL)
							{
								d.uhi += 1U;
							}
						}
						return;
					}
					num |= num4;
				}
				num5 = decimal.DecCalc.s_powers10[(int)scale];
				uint num7 = d.uhi;
				if (num7 == 0U)
				{
					ulong low2 = d.Low64;
					if (low2 != 0UL)
					{
						ulong num8 = low2 / (ulong)num5;
						d.Low64 = num8;
						uint num4 = (uint)(low2 - num8 * (ulong)num5);
						goto IL_194;
					}
					if (mode > decimal.DecCalc.RoundingMode.Truncate)
					{
						uint num4 = 0U;
						goto IL_194;
					}
					return;
				}
				else
				{
					uint num4 = num7 - (d.uhi = num7 / num5) * num5;
					num7 = d.umid;
					if ((num7 | num4) != 0U)
					{
						num4 = num7 - (d.umid = (uint)(((ulong)num4 << 32 | (ulong)num7) / (ulong)num5)) * num5;
					}
					num7 = d.ulo;
					if ((num7 | num4) != 0U)
					{
						num4 = num7 - (d.ulo = (uint)(((ulong)num4 << 32 | (ulong)num7) / (ulong)num5)) * num5;
						goto IL_194;
					}
					goto IL_194;
				}
			}

			// Token: 0x06001D35 RID: 7477 RVA: 0x0006D5FC File Offset: 0x0006B7FC
			internal static uint DecDivMod1E9(ref decimal.DecCalc value)
			{
				ulong num = ((ulong)value.uhi << 32) + (ulong)value.umid;
				ulong num2 = num / 1000000000UL;
				value.uhi = (uint)(num2 >> 32);
				value.umid = (uint)num2;
				ulong num3 = (num - (ulong)((uint)num2 * 1000000000U) << 32) + (ulong)value.ulo;
				uint num4 = (uint)(num3 / 1000000000UL);
				value.ulo = num4;
				return (uint)num3 - num4 * 1000000000U;
			}

			// Token: 0x06001D36 RID: 7478 RVA: 0x0006D668 File Offset: 0x0006B868
			// Note: this type is marked as 'beforefieldinit'.
			static DecCalc()
			{
			}

			// Token: 0x040019F0 RID: 6640
			[FieldOffset(0)]
			private uint uflags;

			// Token: 0x040019F1 RID: 6641
			[FieldOffset(4)]
			private uint uhi;

			// Token: 0x040019F2 RID: 6642
			[FieldOffset(8)]
			private uint ulo;

			// Token: 0x040019F3 RID: 6643
			[FieldOffset(12)]
			private uint umid;

			// Token: 0x040019F4 RID: 6644
			[FieldOffset(8)]
			private ulong ulomidLE;

			// Token: 0x040019F5 RID: 6645
			private const uint SignMask = 2147483648U;

			// Token: 0x040019F6 RID: 6646
			private const uint ScaleMask = 16711680U;

			// Token: 0x040019F7 RID: 6647
			private const int DEC_SCALE_MAX = 28;

			// Token: 0x040019F8 RID: 6648
			private const uint TenToPowerNine = 1000000000U;

			// Token: 0x040019F9 RID: 6649
			private const ulong TenToPowerEighteen = 1000000000000000000UL;

			// Token: 0x040019FA RID: 6650
			private const int MaxInt32Scale = 9;

			// Token: 0x040019FB RID: 6651
			private const int MaxInt64Scale = 19;

			// Token: 0x040019FC RID: 6652
			private static readonly uint[] s_powers10 = new uint[]
			{
				1U,
				10U,
				100U,
				1000U,
				10000U,
				100000U,
				1000000U,
				10000000U,
				100000000U,
				1000000000U
			};

			// Token: 0x040019FD RID: 6653
			private static readonly ulong[] s_ulongPowers10 = new ulong[]
			{
				10UL,
				100UL,
				1000UL,
				10000UL,
				100000UL,
				1000000UL,
				10000000UL,
				100000000UL,
				1000000000UL,
				10000000000UL,
				100000000000UL,
				1000000000000UL,
				10000000000000UL,
				100000000000000UL,
				1000000000000000UL,
				10000000000000000UL,
				100000000000000000UL,
				1000000000000000000UL,
				10000000000000000000UL
			};

			// Token: 0x040019FE RID: 6654
			private static readonly double[] s_doublePowers10 = new double[]
			{
				1.0,
				10.0,
				100.0,
				1000.0,
				10000.0,
				100000.0,
				1000000.0,
				10000000.0,
				100000000.0,
				1000000000.0,
				10000000000.0,
				100000000000.0,
				1000000000000.0,
				10000000000000.0,
				100000000000000.0,
				1000000000000000.0,
				10000000000000000.0,
				1E+17,
				1E+18,
				1E+19,
				1E+20,
				1E+21,
				1E+22,
				1E+23,
				1E+24,
				1E+25,
				1E+26,
				1E+27,
				1E+28,
				1E+29,
				1E+30,
				1E+31,
				1E+32,
				1E+33,
				1E+34,
				1E+35,
				1E+36,
				1E+37,
				1E+38,
				1E+39,
				1E+40,
				1E+41,
				1E+42,
				1E+43,
				1E+44,
				1E+45,
				1E+46,
				1E+47,
				1E+48,
				1E+49,
				1E+50,
				1E+51,
				1E+52,
				1E+53,
				1E+54,
				1E+55,
				1E+56,
				1E+57,
				1E+58,
				1E+59,
				1E+60,
				1E+61,
				1E+62,
				1E+63,
				1E+64,
				1E+65,
				1E+66,
				1E+67,
				1E+68,
				1E+69,
				1E+70,
				1E+71,
				1E+72,
				1E+73,
				1E+74,
				1E+75,
				1E+76,
				1E+77,
				1E+78,
				1E+79,
				1E+80
			};

			// Token: 0x040019FF RID: 6655
			private static readonly decimal.DecCalc.PowerOvfl[] PowerOvflValues = new decimal.DecCalc.PowerOvfl[]
			{
				new decimal.DecCalc.PowerOvfl(429496729U, 2576980377U, 2576980377U),
				new decimal.DecCalc.PowerOvfl(42949672U, 4123168604U, 687194767U),
				new decimal.DecCalc.PowerOvfl(4294967U, 1271310319U, 2645699854U),
				new decimal.DecCalc.PowerOvfl(429496U, 3133608139U, 694066715U),
				new decimal.DecCalc.PowerOvfl(42949U, 2890341191U, 2216890319U),
				new decimal.DecCalc.PowerOvfl(4294U, 4154504685U, 2369172679U),
				new decimal.DecCalc.PowerOvfl(429U, 2133437386U, 4102387834U),
				new decimal.DecCalc.PowerOvfl(42U, 4078814305U, 410238783U)
			};

			// Token: 0x02000277 RID: 631
			internal enum RoundingMode
			{
				// Token: 0x04001A01 RID: 6657
				ToEven,
				// Token: 0x04001A02 RID: 6658
				AwayFromZero,
				// Token: 0x04001A03 RID: 6659
				Truncate,
				// Token: 0x04001A04 RID: 6660
				Floor,
				// Token: 0x04001A05 RID: 6661
				Ceiling
			}

			// Token: 0x02000278 RID: 632
			private struct PowerOvfl
			{
				// Token: 0x06001D37 RID: 7479 RVA: 0x0006D79A File Offset: 0x0006B99A
				public PowerOvfl(uint hi, uint mid, uint lo)
				{
					this.Hi = hi;
					this.MidLo = ((ulong)mid << 32) + (ulong)lo;
				}

				// Token: 0x04001A06 RID: 6662
				public readonly uint Hi;

				// Token: 0x04001A07 RID: 6663
				public readonly ulong MidLo;
			}

			// Token: 0x02000279 RID: 633
			[StructLayout(LayoutKind.Explicit)]
			private struct Buf12
			{
				// Token: 0x17000368 RID: 872
				// (get) Token: 0x06001D38 RID: 7480 RVA: 0x0006D7B1 File Offset: 0x0006B9B1
				// (set) Token: 0x06001D39 RID: 7481 RVA: 0x0006D7D3 File Offset: 0x0006B9D3
				public ulong Low64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return (ulong)this.U1 << 32 | (ulong)this.U0;
						}
						return this.ulo64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.ulo64LE = value;
							return;
						}
						this.U1 = (uint)(value >> 32);
						this.U0 = (uint)value;
					}
				}

				// Token: 0x17000369 RID: 873
				// (get) Token: 0x06001D3A RID: 7482 RVA: 0x0006D7F7 File Offset: 0x0006B9F7
				// (set) Token: 0x06001D3B RID: 7483 RVA: 0x0006D819 File Offset: 0x0006BA19
				public ulong High64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return (ulong)this.U2 << 32 | (ulong)this.U1;
						}
						return this.uhigh64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.uhigh64LE = value;
							return;
						}
						this.U2 = (uint)(value >> 32);
						this.U1 = (uint)value;
					}
				}

				// Token: 0x04001A08 RID: 6664
				[FieldOffset(0)]
				public uint U0;

				// Token: 0x04001A09 RID: 6665
				[FieldOffset(4)]
				public uint U1;

				// Token: 0x04001A0A RID: 6666
				[FieldOffset(8)]
				public uint U2;

				// Token: 0x04001A0B RID: 6667
				[FieldOffset(0)]
				private ulong ulo64LE;

				// Token: 0x04001A0C RID: 6668
				[FieldOffset(4)]
				private ulong uhigh64LE;
			}

			// Token: 0x0200027A RID: 634
			[StructLayout(LayoutKind.Explicit)]
			private struct Buf16
			{
				// Token: 0x1700036A RID: 874
				// (get) Token: 0x06001D3C RID: 7484 RVA: 0x0006D83D File Offset: 0x0006BA3D
				// (set) Token: 0x06001D3D RID: 7485 RVA: 0x0006D85F File Offset: 0x0006BA5F
				public ulong Low64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return (ulong)this.U1 << 32 | (ulong)this.U0;
						}
						return this.ulo64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.ulo64LE = value;
							return;
						}
						this.U1 = (uint)(value >> 32);
						this.U0 = (uint)value;
					}
				}

				// Token: 0x1700036B RID: 875
				// (get) Token: 0x06001D3E RID: 7486 RVA: 0x0006D883 File Offset: 0x0006BA83
				// (set) Token: 0x06001D3F RID: 7487 RVA: 0x0006D8A5 File Offset: 0x0006BAA5
				public ulong High64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return (ulong)this.U3 << 32 | (ulong)this.U2;
						}
						return this.uhigh64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.uhigh64LE = value;
							return;
						}
						this.U3 = (uint)(value >> 32);
						this.U2 = (uint)value;
					}
				}

				// Token: 0x04001A0D RID: 6669
				[FieldOffset(0)]
				public uint U0;

				// Token: 0x04001A0E RID: 6670
				[FieldOffset(4)]
				public uint U1;

				// Token: 0x04001A0F RID: 6671
				[FieldOffset(8)]
				public uint U2;

				// Token: 0x04001A10 RID: 6672
				[FieldOffset(12)]
				public uint U3;

				// Token: 0x04001A11 RID: 6673
				[FieldOffset(0)]
				private ulong ulo64LE;

				// Token: 0x04001A12 RID: 6674
				[FieldOffset(8)]
				private ulong uhigh64LE;
			}

			// Token: 0x0200027B RID: 635
			[StructLayout(LayoutKind.Explicit)]
			private struct Buf24
			{
				// Token: 0x1700036C RID: 876
				// (get) Token: 0x06001D40 RID: 7488 RVA: 0x0006D8C9 File Offset: 0x0006BAC9
				// (set) Token: 0x06001D41 RID: 7489 RVA: 0x0006D8EB File Offset: 0x0006BAEB
				public ulong Low64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return (ulong)this.U1 << 32 | (ulong)this.U0;
						}
						return this.ulo64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.ulo64LE = value;
							return;
						}
						this.U1 = (uint)(value >> 32);
						this.U0 = (uint)value;
					}
				}

				// Token: 0x1700036D RID: 877
				// (get) Token: 0x06001D42 RID: 7490 RVA: 0x0006D90F File Offset: 0x0006BB0F
				// (set) Token: 0x06001D43 RID: 7491 RVA: 0x0006D931 File Offset: 0x0006BB31
				public ulong Mid64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return (ulong)this.U3 << 32 | (ulong)this.U2;
						}
						return this.umid64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.umid64LE = value;
							return;
						}
						this.U3 = (uint)(value >> 32);
						this.U2 = (uint)value;
					}
				}

				// Token: 0x1700036E RID: 878
				// (get) Token: 0x06001D44 RID: 7492 RVA: 0x0006D955 File Offset: 0x0006BB55
				// (set) Token: 0x06001D45 RID: 7493 RVA: 0x0006D977 File Offset: 0x0006BB77
				public ulong High64
				{
					get
					{
						if (!BitConverter.IsLittleEndian)
						{
							return (ulong)this.U5 << 32 | (ulong)this.U4;
						}
						return this.uhigh64LE;
					}
					set
					{
						if (BitConverter.IsLittleEndian)
						{
							this.uhigh64LE = value;
							return;
						}
						this.U5 = (uint)(value >> 32);
						this.U4 = (uint)value;
					}
				}

				// Token: 0x1700036F RID: 879
				// (get) Token: 0x06001D46 RID: 7494 RVA: 0x000224A3 File Offset: 0x000206A3
				public int Length
				{
					get
					{
						return 6;
					}
				}

				// Token: 0x04001A13 RID: 6675
				[FieldOffset(0)]
				public uint U0;

				// Token: 0x04001A14 RID: 6676
				[FieldOffset(4)]
				public uint U1;

				// Token: 0x04001A15 RID: 6677
				[FieldOffset(8)]
				public uint U2;

				// Token: 0x04001A16 RID: 6678
				[FieldOffset(12)]
				public uint U3;

				// Token: 0x04001A17 RID: 6679
				[FieldOffset(16)]
				public uint U4;

				// Token: 0x04001A18 RID: 6680
				[FieldOffset(20)]
				public uint U5;

				// Token: 0x04001A19 RID: 6681
				[FieldOffset(0)]
				private ulong ulo64LE;

				// Token: 0x04001A1A RID: 6682
				[FieldOffset(8)]
				private ulong umid64LE;

				// Token: 0x04001A1B RID: 6683
				[FieldOffset(16)]
				private ulong uhigh64LE;
			}

			// Token: 0x0200027C RID: 636
			private struct Buf28
			{
				// Token: 0x17000370 RID: 880
				// (get) Token: 0x06001D47 RID: 7495 RVA: 0x0003227E File Offset: 0x0003047E
				public int Length
				{
					get
					{
						return 7;
					}
				}

				// Token: 0x04001A1C RID: 6684
				public decimal.DecCalc.Buf24 Buf24;

				// Token: 0x04001A1D RID: 6685
				public uint U6;
			}
		}
	}
}
