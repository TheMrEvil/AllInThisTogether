using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Globalization
{
	/// <summary>Provides culture-specific information for formatting and parsing numeric values.</summary>
	// Token: 0x02000994 RID: 2452
	[ComVisible(true)]
	[Serializable]
	public sealed class NumberFormatInfo : ICloneable, IFormatProvider
	{
		/// <summary>Initializes a new writable instance of the <see cref="T:System.Globalization.NumberFormatInfo" /> class that is culture-independent (invariant).</summary>
		// Token: 0x0600578E RID: 22414 RVA: 0x00127B73 File Offset: 0x00125D73
		public NumberFormatInfo() : this(null)
		{
		}

		// Token: 0x0600578F RID: 22415 RVA: 0x00127B7C File Offset: 0x00125D7C
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			if (this.numberDecimalSeparator != this.numberGroupSeparator)
			{
				this.validForParseAsNumber = true;
			}
			else
			{
				this.validForParseAsNumber = false;
			}
			if (this.numberDecimalSeparator != this.numberGroupSeparator && this.numberDecimalSeparator != this.currencyGroupSeparator && this.currencyDecimalSeparator != this.numberGroupSeparator && this.currencyDecimalSeparator != this.currencyGroupSeparator)
			{
				this.validForParseAsCurrency = true;
				return;
			}
			this.validForParseAsCurrency = false;
		}

		// Token: 0x06005790 RID: 22416 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
		}

		// Token: 0x06005791 RID: 22417 RVA: 0x00004BF9 File Offset: 0x00002DF9
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
		}

		// Token: 0x06005792 RID: 22418 RVA: 0x00127C07 File Offset: 0x00125E07
		private static void VerifyDecimalSeparator(string decSep, string propertyName)
		{
			if (decSep == null)
			{
				throw new ArgumentNullException(propertyName, Environment.GetResourceString("String reference not set to an instance of a String."));
			}
			if (decSep.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Decimal separator cannot be the empty string."));
			}
		}

		// Token: 0x06005793 RID: 22419 RVA: 0x00127C35 File Offset: 0x00125E35
		private static void VerifyGroupSeparator(string groupSep, string propertyName)
		{
			if (groupSep == null)
			{
				throw new ArgumentNullException(propertyName, Environment.GetResourceString("String reference not set to an instance of a String."));
			}
		}

		// Token: 0x06005794 RID: 22420 RVA: 0x00127C4C File Offset: 0x00125E4C
		private static void VerifyNativeDigits(string[] nativeDig, string propertyName)
		{
			if (nativeDig == null)
			{
				throw new ArgumentNullException(propertyName, Environment.GetResourceString("Array cannot be null."));
			}
			if (nativeDig.Length != 10)
			{
				throw new ArgumentException(Environment.GetResourceString("The NativeDigits array must contain exactly ten members."), propertyName);
			}
			for (int i = 0; i < nativeDig.Length; i++)
			{
				if (nativeDig[i] == null)
				{
					throw new ArgumentNullException(propertyName, Environment.GetResourceString("Found a null value within an array."));
				}
				if (nativeDig[i].Length != 1)
				{
					if (nativeDig[i].Length != 2)
					{
						throw new ArgumentException(Environment.GetResourceString("Each member of the NativeDigits array must be a single text element (one or more UTF16 code points) with a Unicode Nd (Number, Decimal Digit) property indicating it is a digit."), propertyName);
					}
					if (!char.IsSurrogatePair(nativeDig[i][0], nativeDig[i][1]))
					{
						throw new ArgumentException(Environment.GetResourceString("Each member of the NativeDigits array must be a single text element (one or more UTF16 code points) with a Unicode Nd (Number, Decimal Digit) property indicating it is a digit."), propertyName);
					}
				}
				if (CharUnicodeInfo.GetDecimalDigitValue(nativeDig[i], 0) != i && CharUnicodeInfo.GetUnicodeCategory(nativeDig[i], 0) != UnicodeCategory.PrivateUse)
				{
					throw new ArgumentException(Environment.GetResourceString("Each member of the NativeDigits array must be a single text element (one or more UTF16 code points) with a Unicode Nd (Number, Decimal Digit) property indicating it is a digit."), propertyName);
				}
			}
		}

		// Token: 0x06005795 RID: 22421 RVA: 0x00127D2A File Offset: 0x00125F2A
		private static void VerifyDigitSubstitution(DigitShapes digitSub, string propertyName)
		{
			if (digitSub > DigitShapes.NativeNational)
			{
				throw new ArgumentException(Environment.GetResourceString("The DigitSubstitution property must be of a valid member of the DigitShapes enumeration. Valid entries include Context, NativeNational or None."), propertyName);
			}
		}

		// Token: 0x06005796 RID: 22422 RVA: 0x00127D44 File Offset: 0x00125F44
		[SecuritySafeCritical]
		internal NumberFormatInfo(CultureData cultureData)
		{
			if (GlobalizationMode.Invariant)
			{
				this.m_isInvariant = true;
				return;
			}
			if (cultureData != null)
			{
				cultureData.GetNFIValues(this);
				if (cultureData.IsInvariantCulture)
				{
					this.m_isInvariant = true;
				}
			}
		}

		// Token: 0x06005797 RID: 22423 RVA: 0x00127ED8 File Offset: 0x001260D8
		private void VerifyWritable()
		{
			if (this.isReadOnly)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Instance is read-only."));
			}
		}

		/// <summary>Gets a read-only <see cref="T:System.Globalization.NumberFormatInfo" /> object that is culture-independent (invariant).</summary>
		/// <returns>A read-only  object that is culture-independent (invariant).</returns>
		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06005798 RID: 22424 RVA: 0x00127EF2 File Offset: 0x001260F2
		public static NumberFormatInfo InvariantInfo
		{
			get
			{
				if (NumberFormatInfo.invariantInfo == null)
				{
					NumberFormatInfo.invariantInfo = NumberFormatInfo.ReadOnly(new NumberFormatInfo
					{
						m_isInvariant = true
					});
				}
				return NumberFormatInfo.invariantInfo;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.NumberFormatInfo" /> associated with the specified <see cref="T:System.IFormatProvider" />.</summary>
		/// <param name="formatProvider">The <see cref="T:System.IFormatProvider" /> used to get the <see cref="T:System.Globalization.NumberFormatInfo" />.  
		///  -or-  
		///  <see langword="null" /> to get <see cref="P:System.Globalization.NumberFormatInfo.CurrentInfo" />.</param>
		/// <returns>The <see cref="T:System.Globalization.NumberFormatInfo" /> associated with the specified <see cref="T:System.IFormatProvider" />.</returns>
		// Token: 0x06005799 RID: 22425 RVA: 0x00127F1C File Offset: 0x0012611C
		public static NumberFormatInfo GetInstance(IFormatProvider formatProvider)
		{
			CultureInfo cultureInfo = formatProvider as CultureInfo;
			if (cultureInfo != null && !cultureInfo.m_isInherited)
			{
				NumberFormatInfo numberFormatInfo = cultureInfo.numInfo;
				if (numberFormatInfo != null)
				{
					return numberFormatInfo;
				}
				return cultureInfo.NumberFormat;
			}
			else
			{
				NumberFormatInfo numberFormatInfo = formatProvider as NumberFormatInfo;
				if (numberFormatInfo != null)
				{
					return numberFormatInfo;
				}
				if (formatProvider != null)
				{
					numberFormatInfo = (formatProvider.GetFormat(typeof(NumberFormatInfo)) as NumberFormatInfo);
					if (numberFormatInfo != null)
					{
						return numberFormatInfo;
					}
				}
				return NumberFormatInfo.CurrentInfo;
			}
		}

		/// <summary>Creates a shallow copy of the <see cref="T:System.Globalization.NumberFormatInfo" /> object.</summary>
		/// <returns>A new object copied from the original <see cref="T:System.Globalization.NumberFormatInfo" /> object.</returns>
		// Token: 0x0600579A RID: 22426 RVA: 0x00127F7F File Offset: 0x0012617F
		public object Clone()
		{
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)base.MemberwiseClone();
			numberFormatInfo.isReadOnly = false;
			return numberFormatInfo;
		}

		/// <summary>Gets or sets the number of decimal places to use in currency values.</summary>
		/// <returns>The number of decimal places to use in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 99.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x0600579B RID: 22427 RVA: 0x00127F93 File Offset: 0x00126193
		// (set) Token: 0x0600579C RID: 22428 RVA: 0x00127F9C File Offset: 0x0012619C
		public int CurrencyDecimalDigits
		{
			get
			{
				return this.currencyDecimalDigits;
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("CurrencyDecimalDigits", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 99));
				}
				this.VerifyWritable();
				this.currencyDecimalDigits = value;
			}
		}

		/// <summary>Gets or sets the string to use as the decimal separator in currency values.</summary>
		/// <returns>The string to use as the decimal separator in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x0600579D RID: 22429 RVA: 0x00127FEB File Offset: 0x001261EB
		// (set) Token: 0x0600579E RID: 22430 RVA: 0x00127FF3 File Offset: 0x001261F3
		public string CurrencyDecimalSeparator
		{
			get
			{
				return this.currencyDecimalSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "CurrencyDecimalSeparator");
				this.currencyDecimalSeparator = value;
			}
		}

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the <see cref="T:System.Globalization.NumberFormatInfo" /> is read-only; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x0600579F RID: 22431 RVA: 0x0012800D File Offset: 0x0012620D
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
		}

		// Token: 0x060057A0 RID: 22432 RVA: 0x00128018 File Offset: 0x00126218
		internal static void CheckGroupSize(string propName, int[] groupSize)
		{
			int i = 0;
			while (i < groupSize.Length)
			{
				if (groupSize[i] < 1)
				{
					if (i == groupSize.Length - 1 && groupSize[i] == 0)
					{
						return;
					}
					throw new ArgumentException(Environment.GetResourceString("Every element in the value array should be between one and nine, except for the last element, which can be zero."), propName);
				}
				else
				{
					if (groupSize[i] > 9)
					{
						throw new ArgumentException(Environment.GetResourceString("Every element in the value array should be between one and nine, except for the last element, which can be zero."), propName);
					}
					i++;
				}
			}
		}

		/// <summary>Gets or sets the number of digits in each group to the left of the decimal in currency values.</summary>
		/// <returns>The number of digits in each group to the left of the decimal in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional array with only one element, which is set to 3.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The property is being set and the array contains an entry that is less than 0 or greater than 9.  
		///  -or-  
		///  The property is being set and the array contains an entry, other than the last entry, that is set to 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x060057A1 RID: 22433 RVA: 0x00128070 File Offset: 0x00126270
		// (set) Token: 0x060057A2 RID: 22434 RVA: 0x00128084 File Offset: 0x00126284
		public int[] CurrencyGroupSizes
		{
			get
			{
				return (int[])this.currencyGroupSizes.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("CurrencyGroupSizes", Environment.GetResourceString("Object cannot be null."));
				}
				this.VerifyWritable();
				int[] groupSize = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("CurrencyGroupSizes", groupSize);
				this.currencyGroupSizes = groupSize;
			}
		}

		/// <summary>Gets or sets the number of digits in each group to the left of the decimal in numeric values.</summary>
		/// <returns>The number of digits in each group to the left of the decimal in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional array with only one element, which is set to 3.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The property is being set and the array contains an entry that is less than 0 or greater than 9.  
		///  -or-  
		///  The property is being set and the array contains an entry, other than the last entry, that is set to 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x060057A3 RID: 22435 RVA: 0x001280CD File Offset: 0x001262CD
		// (set) Token: 0x060057A4 RID: 22436 RVA: 0x001280E0 File Offset: 0x001262E0
		public int[] NumberGroupSizes
		{
			get
			{
				return (int[])this.numberGroupSizes.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NumberGroupSizes", Environment.GetResourceString("Object cannot be null."));
				}
				this.VerifyWritable();
				int[] groupSize = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("NumberGroupSizes", groupSize);
				this.numberGroupSizes = groupSize;
			}
		}

		/// <summary>Gets or sets the number of digits in each group to the left of the decimal in percent values.</summary>
		/// <returns>The number of digits in each group to the left of the decimal in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is a one-dimensional array with only one element, which is set to 3.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The property is being set and the array contains an entry that is less than 0 or greater than 9.  
		///  -or-  
		///  The property is being set and the array contains an entry, other than the last entry, that is set to 0.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EA2 RID: 3746
		// (get) Token: 0x060057A5 RID: 22437 RVA: 0x00128129 File Offset: 0x00126329
		// (set) Token: 0x060057A6 RID: 22438 RVA: 0x0012813C File Offset: 0x0012633C
		public int[] PercentGroupSizes
		{
			get
			{
				return (int[])this.percentGroupSizes.Clone();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PercentGroupSizes", Environment.GetResourceString("Object cannot be null."));
				}
				this.VerifyWritable();
				int[] groupSize = (int[])value.Clone();
				NumberFormatInfo.CheckGroupSize("PercentGroupSizes", groupSize);
				this.percentGroupSizes = groupSize;
			}
		}

		/// <summary>Gets or sets the string that separates groups of digits to the left of the decimal in currency values.</summary>
		/// <returns>The string that separates groups of digits to the left of the decimal in currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EA3 RID: 3747
		// (get) Token: 0x060057A7 RID: 22439 RVA: 0x00128185 File Offset: 0x00126385
		// (set) Token: 0x060057A8 RID: 22440 RVA: 0x0012818D File Offset: 0x0012638D
		public string CurrencyGroupSeparator
		{
			get
			{
				return this.currencyGroupSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "CurrencyGroupSeparator");
				this.currencyGroupSeparator = value;
			}
		}

		/// <summary>Gets or sets the string to use as the currency symbol.</summary>
		/// <returns>The string to use as the currency symbol. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "¤".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EA4 RID: 3748
		// (get) Token: 0x060057A9 RID: 22441 RVA: 0x001281A7 File Offset: 0x001263A7
		// (set) Token: 0x060057AA RID: 22442 RVA: 0x001281AF File Offset: 0x001263AF
		public string CurrencySymbol
		{
			get
			{
				return this.currencySymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("CurrencySymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.currencySymbol = value;
			}
		}

		/// <summary>Gets a read-only <see cref="T:System.Globalization.NumberFormatInfo" /> that formats values based on the current culture.</summary>
		/// <returns>A read-only <see cref="T:System.Globalization.NumberFormatInfo" /> based on the culture of the current thread.</returns>
		// Token: 0x17000EA5 RID: 3749
		// (get) Token: 0x060057AB RID: 22443 RVA: 0x001281D8 File Offset: 0x001263D8
		public static NumberFormatInfo CurrentInfo
		{
			get
			{
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				if (!currentCulture.m_isInherited)
				{
					NumberFormatInfo numInfo = currentCulture.numInfo;
					if (numInfo != null)
					{
						return numInfo;
					}
				}
				return (NumberFormatInfo)currentCulture.GetFormat(typeof(NumberFormatInfo));
			}
		}

		/// <summary>Gets or sets the string that represents the IEEE NaN (not a number) value.</summary>
		/// <returns>The string that represents the IEEE NaN (not a number) value. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "NaN".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EA6 RID: 3750
		// (get) Token: 0x060057AC RID: 22444 RVA: 0x0012821B File Offset: 0x0012641B
		// (set) Token: 0x060057AD RID: 22445 RVA: 0x00128223 File Offset: 0x00126423
		public string NaNSymbol
		{
			get
			{
				return this.nanSymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NaNSymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.nanSymbol = value;
			}
		}

		/// <summary>Gets or sets the format pattern for negative currency values.</summary>
		/// <returns>The format pattern for negative currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "($n)", where "$" is the <see cref="P:System.Globalization.NumberFormatInfo.CurrencySymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 15.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EA7 RID: 3751
		// (get) Token: 0x060057AE RID: 22446 RVA: 0x0012824A File Offset: 0x0012644A
		// (set) Token: 0x060057AF RID: 22447 RVA: 0x00128254 File Offset: 0x00126454
		public int CurrencyNegativePattern
		{
			get
			{
				return this.currencyNegativePattern;
			}
			set
			{
				if (value < 0 || value > 15)
				{
					throw new ArgumentOutOfRangeException("CurrencyNegativePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 15));
				}
				this.VerifyWritable();
				this.currencyNegativePattern = value;
			}
		}

		/// <summary>Gets or sets the format pattern for negative numeric values.</summary>
		/// <returns>The format pattern for negative numeric values.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 4.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EA8 RID: 3752
		// (get) Token: 0x060057B0 RID: 22448 RVA: 0x001282A3 File Offset: 0x001264A3
		// (set) Token: 0x060057B1 RID: 22449 RVA: 0x001282AC File Offset: 0x001264AC
		public int NumberNegativePattern
		{
			get
			{
				return this.numberNegativePattern;
			}
			set
			{
				if (value < 0 || value > 4)
				{
					throw new ArgumentOutOfRangeException("NumberNegativePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 4));
				}
				this.VerifyWritable();
				this.numberNegativePattern = value;
			}
		}

		/// <summary>Gets or sets the format pattern for positive percent values.</summary>
		/// <returns>The format pattern for positive percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "n %", where "%" is the <see cref="P:System.Globalization.NumberFormatInfo.PercentSymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 3.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x060057B2 RID: 22450 RVA: 0x001282F9 File Offset: 0x001264F9
		// (set) Token: 0x060057B3 RID: 22451 RVA: 0x00128304 File Offset: 0x00126504
		public int PercentPositivePattern
		{
			get
			{
				return this.percentPositivePattern;
			}
			set
			{
				if (value < 0 || value > 3)
				{
					throw new ArgumentOutOfRangeException("PercentPositivePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 3));
				}
				this.VerifyWritable();
				this.percentPositivePattern = value;
			}
		}

		/// <summary>Gets or sets the format pattern for negative percent values.</summary>
		/// <returns>The format pattern for negative percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "-n %", where "%" is the <see cref="P:System.Globalization.NumberFormatInfo.PercentSymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 11.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x060057B4 RID: 22452 RVA: 0x00128351 File Offset: 0x00126551
		// (set) Token: 0x060057B5 RID: 22453 RVA: 0x0012835C File Offset: 0x0012655C
		public int PercentNegativePattern
		{
			get
			{
				return this.percentNegativePattern;
			}
			set
			{
				if (value < 0 || value > 11)
				{
					throw new ArgumentOutOfRangeException("PercentNegativePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 11));
				}
				this.VerifyWritable();
				this.percentNegativePattern = value;
			}
		}

		/// <summary>Gets or sets the string that represents negative infinity.</summary>
		/// <returns>The string that represents negative infinity. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "-Infinity".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x060057B6 RID: 22454 RVA: 0x001283AB File Offset: 0x001265AB
		// (set) Token: 0x060057B7 RID: 22455 RVA: 0x001283B3 File Offset: 0x001265B3
		public string NegativeInfinitySymbol
		{
			get
			{
				return this.negativeInfinitySymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NegativeInfinitySymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.negativeInfinitySymbol = value;
			}
		}

		/// <summary>Gets or sets the string that denotes that the associated number is negative.</summary>
		/// <returns>The string that denotes that the associated number is negative. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "-".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EAC RID: 3756
		// (get) Token: 0x060057B8 RID: 22456 RVA: 0x001283DA File Offset: 0x001265DA
		// (set) Token: 0x060057B9 RID: 22457 RVA: 0x001283E2 File Offset: 0x001265E2
		public string NegativeSign
		{
			get
			{
				return this.negativeSign;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("NegativeSign", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.negativeSign = value;
			}
		}

		/// <summary>Gets or sets the number of decimal places to use in numeric values.</summary>
		/// <returns>The number of decimal places to use in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 99.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x060057BA RID: 22458 RVA: 0x00128409 File Offset: 0x00126609
		// (set) Token: 0x060057BB RID: 22459 RVA: 0x00128414 File Offset: 0x00126614
		public int NumberDecimalDigits
		{
			get
			{
				return this.numberDecimalDigits;
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("NumberDecimalDigits", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 99));
				}
				this.VerifyWritable();
				this.numberDecimalDigits = value;
			}
		}

		/// <summary>Gets or sets the string to use as the decimal separator in numeric values.</summary>
		/// <returns>The string to use as the decimal separator in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x060057BC RID: 22460 RVA: 0x00128463 File Offset: 0x00126663
		// (set) Token: 0x060057BD RID: 22461 RVA: 0x0012846B File Offset: 0x0012666B
		public string NumberDecimalSeparator
		{
			get
			{
				return this.numberDecimalSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "NumberDecimalSeparator");
				this.numberDecimalSeparator = value;
			}
		}

		/// <summary>Gets or sets the string that separates groups of digits to the left of the decimal in numeric values.</summary>
		/// <returns>The string that separates groups of digits to the left of the decimal in numeric values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x060057BE RID: 22462 RVA: 0x00128485 File Offset: 0x00126685
		// (set) Token: 0x060057BF RID: 22463 RVA: 0x0012848D File Offset: 0x0012668D
		public string NumberGroupSeparator
		{
			get
			{
				return this.numberGroupSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "NumberGroupSeparator");
				this.numberGroupSeparator = value;
			}
		}

		/// <summary>Gets or sets the format pattern for positive currency values.</summary>
		/// <returns>The format pattern for positive currency values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 0, which represents "$n", where "$" is the <see cref="P:System.Globalization.NumberFormatInfo.CurrencySymbol" /> and <paramref name="n" /> is a number.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 3.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x060057C0 RID: 22464 RVA: 0x001284A7 File Offset: 0x001266A7
		// (set) Token: 0x060057C1 RID: 22465 RVA: 0x001284B0 File Offset: 0x001266B0
		public int CurrencyPositivePattern
		{
			get
			{
				return this.currencyPositivePattern;
			}
			set
			{
				if (value < 0 || value > 3)
				{
					throw new ArgumentOutOfRangeException("CurrencyPositivePattern", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 3));
				}
				this.VerifyWritable();
				this.currencyPositivePattern = value;
			}
		}

		/// <summary>Gets or sets the string that represents positive infinity.</summary>
		/// <returns>The string that represents positive infinity. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "Infinity".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x060057C2 RID: 22466 RVA: 0x001284FD File Offset: 0x001266FD
		// (set) Token: 0x060057C3 RID: 22467 RVA: 0x00128505 File Offset: 0x00126705
		public string PositiveInfinitySymbol
		{
			get
			{
				return this.positiveInfinitySymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PositiveInfinitySymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.positiveInfinitySymbol = value;
			}
		}

		/// <summary>Gets or sets the string that denotes that the associated number is positive.</summary>
		/// <returns>The string that denotes that the associated number is positive. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "+".</returns>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the value to be assigned is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x060057C4 RID: 22468 RVA: 0x0012852C File Offset: 0x0012672C
		// (set) Token: 0x060057C5 RID: 22469 RVA: 0x00128534 File Offset: 0x00126734
		public string PositiveSign
		{
			get
			{
				return this.positiveSign;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PositiveSign", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.positiveSign = value;
			}
		}

		/// <summary>Gets or sets the number of decimal places to use in percent values.</summary>
		/// <returns>The number of decimal places to use in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property is being set to a value that is less than 0 or greater than 99.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EB3 RID: 3763
		// (get) Token: 0x060057C6 RID: 22470 RVA: 0x0012855B File Offset: 0x0012675B
		// (set) Token: 0x060057C7 RID: 22471 RVA: 0x00128564 File Offset: 0x00126764
		public int PercentDecimalDigits
		{
			get
			{
				return this.percentDecimalDigits;
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("PercentDecimalDigits", string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Valid values are between {0} and {1}, inclusive."), 0, 99));
				}
				this.VerifyWritable();
				this.percentDecimalDigits = value;
			}
		}

		/// <summary>Gets or sets the string to use as the decimal separator in percent values.</summary>
		/// <returns>The string to use as the decimal separator in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ".".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">The property is being set to an empty string.</exception>
		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x060057C8 RID: 22472 RVA: 0x001285B3 File Offset: 0x001267B3
		// (set) Token: 0x060057C9 RID: 22473 RVA: 0x001285BB File Offset: 0x001267BB
		public string PercentDecimalSeparator
		{
			get
			{
				return this.percentDecimalSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDecimalSeparator(value, "PercentDecimalSeparator");
				this.percentDecimalSeparator = value;
			}
		}

		/// <summary>Gets or sets the string that separates groups of digits to the left of the decimal in percent values.</summary>
		/// <returns>The string that separates groups of digits to the left of the decimal in percent values. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is ",".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x060057CA RID: 22474 RVA: 0x001285D5 File Offset: 0x001267D5
		// (set) Token: 0x060057CB RID: 22475 RVA: 0x001285DD File Offset: 0x001267DD
		public string PercentGroupSeparator
		{
			get
			{
				return this.percentGroupSeparator;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyGroupSeparator(value, "PercentGroupSeparator");
				this.percentGroupSeparator = value;
			}
		}

		/// <summary>Gets or sets the string to use as the percent symbol.</summary>
		/// <returns>The string to use as the percent symbol. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "%".</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x060057CC RID: 22476 RVA: 0x001285F7 File Offset: 0x001267F7
		// (set) Token: 0x060057CD RID: 22477 RVA: 0x001285FF File Offset: 0x001267FF
		public string PercentSymbol
		{
			get
			{
				return this.percentSymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PercentSymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.percentSymbol = value;
			}
		}

		/// <summary>Gets or sets the string to use as the per mille symbol.</summary>
		/// <returns>The string to use as the per mille symbol. The default for <see cref="P:System.Globalization.NumberFormatInfo.InvariantInfo" /> is "‰", which is the Unicode character U+2030.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is being set to <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The property is being set and the <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x060057CE RID: 22478 RVA: 0x00128626 File Offset: 0x00126826
		// (set) Token: 0x060057CF RID: 22479 RVA: 0x0012862E File Offset: 0x0012682E
		public string PerMilleSymbol
		{
			get
			{
				return this.perMilleSymbol;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("PerMilleSymbol", Environment.GetResourceString("String reference not set to an instance of a String."));
				}
				this.VerifyWritable();
				this.perMilleSymbol = value;
			}
		}

		/// <summary>Gets or sets a string array of native digits equivalent to the Western digits 0 through 9.</summary>
		/// <returns>A string array that contains the native equivalent of the Western digits 0 through 9. The default is an array having the elements "0", "1", "2", "3", "4", "5", "6", "7", "8", and "9".</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the value is <see langword="null" />.  
		///  -or-  
		///  In a set operation, an element of the value array is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">In a set operation, the value array does not contain 10 elements.  
		///  -or-  
		///  In a set operation, an element of the value array does not contain either a single <see cref="T:System.Char" /> object or a pair of <see cref="T:System.Char" /> objects that comprise a surrogate pair.  
		///  -or-  
		///  In a set operation, an element of the value array is not a number digit as defined by the Unicode Standard. That is, the digit in the array element does not have the Unicode <see langword="Number, Decimal Digit" /> (Nd) General Category value.  
		///  -or-  
		///  In a set operation, the numeric value of an element in the value array does not correspond to the element's position in the array. That is, the element at index 0, which is the first element of the array, does not have a numeric value of 0, or the element at index 1 does not have a numeric value of 1.</exception>
		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x060057D0 RID: 22480 RVA: 0x00128655 File Offset: 0x00126855
		// (set) Token: 0x060057D1 RID: 22481 RVA: 0x00128667 File Offset: 0x00126867
		[ComVisible(false)]
		public string[] NativeDigits
		{
			get
			{
				return (string[])this.nativeDigits.Clone();
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyNativeDigits(value, "NativeDigits");
				this.nativeDigits = value;
			}
		}

		/// <summary>Gets or sets a value that specifies how the graphical user interface displays the shape of a digit.</summary>
		/// <returns>One of the enumeration values that specifies the culture-specific digit shape.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Globalization.NumberFormatInfo" /> object is read-only.</exception>
		/// <exception cref="T:System.ArgumentException">The value in a set operation is not a valid <see cref="T:System.Globalization.DigitShapes" /> value.</exception>
		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x060057D2 RID: 22482 RVA: 0x00128681 File Offset: 0x00126881
		// (set) Token: 0x060057D3 RID: 22483 RVA: 0x00128689 File Offset: 0x00126889
		[ComVisible(false)]
		public DigitShapes DigitSubstitution
		{
			get
			{
				return (DigitShapes)this.digitSubstitution;
			}
			set
			{
				this.VerifyWritable();
				NumberFormatInfo.VerifyDigitSubstitution(value, "DigitSubstitution");
				this.digitSubstitution = (int)value;
			}
		}

		/// <summary>Gets an object of the specified type that provides a number formatting service.</summary>
		/// <param name="formatType">The <see cref="T:System.Type" /> of the required formatting service.</param>
		/// <returns>The current <see cref="T:System.Globalization.NumberFormatInfo" />, if <paramref name="formatType" /> is the same as the type of the current <see cref="T:System.Globalization.NumberFormatInfo" />; otherwise, <see langword="null" />.</returns>
		// Token: 0x060057D4 RID: 22484 RVA: 0x001286A3 File Offset: 0x001268A3
		public object GetFormat(Type formatType)
		{
			if (!(formatType == typeof(NumberFormatInfo)))
			{
				return null;
			}
			return this;
		}

		/// <summary>Returns a read-only <see cref="T:System.Globalization.NumberFormatInfo" /> wrapper.</summary>
		/// <param name="nfi">The <see cref="T:System.Globalization.NumberFormatInfo" /> to wrap.</param>
		/// <returns>A read-only <see cref="T:System.Globalization.NumberFormatInfo" /> wrapper around <paramref name="nfi" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="nfi" /> is <see langword="null" />.</exception>
		// Token: 0x060057D5 RID: 22485 RVA: 0x001286BA File Offset: 0x001268BA
		public static NumberFormatInfo ReadOnly(NumberFormatInfo nfi)
		{
			if (nfi == null)
			{
				throw new ArgumentNullException("nfi");
			}
			if (nfi.IsReadOnly)
			{
				return nfi;
			}
			NumberFormatInfo numberFormatInfo = (NumberFormatInfo)nfi.MemberwiseClone();
			numberFormatInfo.isReadOnly = true;
			return numberFormatInfo;
		}

		// Token: 0x060057D6 RID: 22486 RVA: 0x001286E8 File Offset: 0x001268E8
		internal static void ValidateParseStyleInteger(NumberStyles style)
		{
			if ((style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("An undefined NumberStyles value is being used."), "style");
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None && (style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("With the AllowHexSpecifier bit set in the enum bit field, the only other valid bits that can be combined into the enum value must be a subset of those in HexNumber."));
			}
		}

		// Token: 0x060057D7 RID: 22487 RVA: 0x00128735 File Offset: 0x00126935
		internal static void ValidateParseStyleFloatingPoint(NumberStyles style)
		{
			if ((style & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier)) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("An undefined NumberStyles value is being used."), "style");
			}
			if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				throw new ArgumentException(Environment.GetResourceString("The number style AllowHexSpecifier is not supported on floating point data types."));
			}
		}

		// Token: 0x04003668 RID: 13928
		private static volatile NumberFormatInfo invariantInfo;

		// Token: 0x04003669 RID: 13929
		internal int[] numberGroupSizes = new int[]
		{
			3
		};

		// Token: 0x0400366A RID: 13930
		internal int[] currencyGroupSizes = new int[]
		{
			3
		};

		// Token: 0x0400366B RID: 13931
		internal int[] percentGroupSizes = new int[]
		{
			3
		};

		// Token: 0x0400366C RID: 13932
		internal string positiveSign = "+";

		// Token: 0x0400366D RID: 13933
		internal string negativeSign = "-";

		// Token: 0x0400366E RID: 13934
		internal string numberDecimalSeparator = ".";

		// Token: 0x0400366F RID: 13935
		internal string numberGroupSeparator = ",";

		// Token: 0x04003670 RID: 13936
		internal string currencyGroupSeparator = ",";

		// Token: 0x04003671 RID: 13937
		internal string currencyDecimalSeparator = ".";

		// Token: 0x04003672 RID: 13938
		internal string currencySymbol = "¤";

		// Token: 0x04003673 RID: 13939
		internal string ansiCurrencySymbol;

		// Token: 0x04003674 RID: 13940
		internal string nanSymbol = "NaN";

		// Token: 0x04003675 RID: 13941
		internal string positiveInfinitySymbol = "Infinity";

		// Token: 0x04003676 RID: 13942
		internal string negativeInfinitySymbol = "-Infinity";

		// Token: 0x04003677 RID: 13943
		internal string percentDecimalSeparator = ".";

		// Token: 0x04003678 RID: 13944
		internal string percentGroupSeparator = ",";

		// Token: 0x04003679 RID: 13945
		internal string percentSymbol = "%";

		// Token: 0x0400367A RID: 13946
		internal string perMilleSymbol = "‰";

		// Token: 0x0400367B RID: 13947
		[OptionalField(VersionAdded = 2)]
		internal string[] nativeDigits = new string[]
		{
			"0",
			"1",
			"2",
			"3",
			"4",
			"5",
			"6",
			"7",
			"8",
			"9"
		};

		// Token: 0x0400367C RID: 13948
		[OptionalField(VersionAdded = 1)]
		internal int m_dataItem;

		// Token: 0x0400367D RID: 13949
		internal int numberDecimalDigits = 2;

		// Token: 0x0400367E RID: 13950
		internal int currencyDecimalDigits = 2;

		// Token: 0x0400367F RID: 13951
		internal int currencyPositivePattern;

		// Token: 0x04003680 RID: 13952
		internal int currencyNegativePattern;

		// Token: 0x04003681 RID: 13953
		internal int numberNegativePattern = 1;

		// Token: 0x04003682 RID: 13954
		internal int percentPositivePattern;

		// Token: 0x04003683 RID: 13955
		internal int percentNegativePattern;

		// Token: 0x04003684 RID: 13956
		internal int percentDecimalDigits = 2;

		// Token: 0x04003685 RID: 13957
		[OptionalField(VersionAdded = 2)]
		internal int digitSubstitution = 1;

		// Token: 0x04003686 RID: 13958
		internal bool isReadOnly;

		// Token: 0x04003687 RID: 13959
		[OptionalField(VersionAdded = 1)]
		internal bool m_useUserOverride;

		// Token: 0x04003688 RID: 13960
		[OptionalField(VersionAdded = 2)]
		internal bool m_isInvariant;

		// Token: 0x04003689 RID: 13961
		[OptionalField(VersionAdded = 1)]
		internal bool validForParseAsNumber = true;

		// Token: 0x0400368A RID: 13962
		[OptionalField(VersionAdded = 1)]
		internal bool validForParseAsCurrency = true;

		// Token: 0x0400368B RID: 13963
		private const NumberStyles InvalidNumberStyles = ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowParentheses | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent | NumberStyles.AllowCurrencySymbol | NumberStyles.AllowHexSpecifier);
	}
}
