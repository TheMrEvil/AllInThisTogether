using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System
{
	/// <summary>Represents a string comparison operation that uses specific case and culture-based or ordinal comparison rules.</summary>
	// Token: 0x02000187 RID: 391
	[Serializable]
	public abstract class StringComparer : IComparer, IEqualityComparer, IComparer<string>, IEqualityComparer<string>
	{
		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-sensitive string comparison using the word comparison rules of the invariant culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x000414FB File Offset: 0x0003F6FB
		public static StringComparer InvariantCulture
		{
			get
			{
				return StringComparer.s_invariantCulture;
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-insensitive string comparison using the word comparison rules of the invariant culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x00041502 File Offset: 0x0003F702
		public static StringComparer InvariantCultureIgnoreCase
		{
			get
			{
				return StringComparer.s_invariantCultureIgnoreCase;
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-sensitive string comparison using the word comparison rules of the current culture.</summary>
		/// <returns>A new <see cref="T:System.StringComparer" /> object.</returns>
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x00041509 File Offset: 0x0003F709
		public static StringComparer CurrentCulture
		{
			get
			{
				return new CultureAwareComparer(CultureInfo.CurrentCulture, CompareOptions.None);
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs case-insensitive string comparisons using the word comparison rules of the current culture.</summary>
		/// <returns>A new object for string comparison.</returns>
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00041516 File Offset: 0x0003F716
		public static StringComparer CurrentCultureIgnoreCase
		{
			get
			{
				return new CultureAwareComparer(CultureInfo.CurrentCulture, CompareOptions.IgnoreCase);
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-sensitive ordinal string comparison.</summary>
		/// <returns>A <see cref="T:System.StringComparer" /> object.</returns>
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00041523 File Offset: 0x0003F723
		public static StringComparer Ordinal
		{
			get
			{
				return StringComparer.s_ordinal;
			}
		}

		/// <summary>Gets a <see cref="T:System.StringComparer" /> object that performs a case-insensitive ordinal string comparison.</summary>
		/// <returns>A <see cref="T:System.StringComparer" /> object.</returns>
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0004152A File Offset: 0x0003F72A
		public static StringComparer OrdinalIgnoreCase
		{
			get
			{
				return StringComparer.s_ordinalIgnoreCase;
			}
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x00041534 File Offset: 0x0003F734
		public static StringComparer FromComparison(StringComparison comparisonType)
		{
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
				return StringComparer.CurrentCulture;
			case StringComparison.CurrentCultureIgnoreCase:
				return StringComparer.CurrentCultureIgnoreCase;
			case StringComparison.InvariantCulture:
				return StringComparer.InvariantCulture;
			case StringComparison.InvariantCultureIgnoreCase:
				return StringComparer.InvariantCultureIgnoreCase;
			case StringComparison.Ordinal:
				return StringComparer.Ordinal;
			case StringComparison.OrdinalIgnoreCase:
				return StringComparer.OrdinalIgnoreCase;
			default:
				throw new ArgumentException("The string comparison type passed in is currently not supported.", "comparisonType");
			}
		}

		/// <summary>Creates a <see cref="T:System.StringComparer" /> object that compares strings according to the rules of a specified culture.</summary>
		/// <param name="culture">A culture whose linguistic rules are used to perform a string comparison.</param>
		/// <param name="ignoreCase">
		///   <see langword="true" /> to specify that comparison operations be case-insensitive; <see langword="false" /> to specify that comparison operations be case-sensitive.</param>
		/// <returns>A new <see cref="T:System.StringComparer" /> object that performs string comparisons according to the comparison rules used by the <paramref name="culture" /> parameter and the case rule specified by the <paramref name="ignoreCase" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is <see langword="null" />.</exception>
		// Token: 0x06000FA5 RID: 4005 RVA: 0x00041594 File Offset: 0x0003F794
		public static StringComparer Create(CultureInfo culture, bool ignoreCase)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return new CultureAwareComparer(culture, ignoreCase ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x000415B1 File Offset: 0x0003F7B1
		public static StringComparer Create(CultureInfo culture, CompareOptions options)
		{
			if (culture == null)
			{
				throw new ArgumentException("culture");
			}
			return new CultureAwareComparer(culture, options);
		}

		/// <summary>When overridden in a derived class, compares two objects and returns an indication of their relative sort order.</summary>
		/// <param name="x">An object to compare to <paramref name="y" />.</param>
		/// <param name="y">An object to compare to <paramref name="x" />.</param>
		/// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.  
		///   Value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///  <paramref name="x" /> precedes  <paramref name="y" /> in the sort order.  
		///
		///  -or-  
		///
		///  <paramref name="x" /> is <see langword="null" /> and <paramref name="y" /> is not <see langword="null" />.  
		///
		///   Zero  
		///
		///  <paramref name="x" /> is equal to <paramref name="y" />.  
		///
		///  -or-  
		///
		///  <paramref name="x" /> and <paramref name="y" /> are both <see langword="null" />.  
		///
		///   Greater than zero  
		///
		///  <paramref name="x" /> follows <paramref name="y" /> in the sort order.  
		///
		///  -or-  
		///
		///  <paramref name="y" /> is <see langword="null" /> and <paramref name="x" /> is not <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">Neither <paramref name="x" /> nor <paramref name="y" /> is a <see cref="T:System.String" /> object, and neither <paramref name="x" /> nor <paramref name="y" /> implements the <see cref="T:System.IComparable" /> interface.</exception>
		// Token: 0x06000FA7 RID: 4007 RVA: 0x000415C8 File Offset: 0x0003F7C8
		public int Compare(object x, object y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Compare(text, text2);
				}
			}
			IComparable comparable = x as IComparable;
			if (comparable != null)
			{
				return comparable.CompareTo(y);
			}
			throw new ArgumentException("At least one object must implement IComparable.");
		}

		/// <summary>When overridden in a derived class, indicates whether two objects are equal.</summary>
		/// <param name="x">An object to compare to <paramref name="y" />.</param>
		/// <param name="y">An object to compare to <paramref name="x" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="x" /> and <paramref name="y" /> refer to the same object, or <paramref name="x" /> and <paramref name="y" /> are both the same type of object and those objects are equal, or both <paramref name="x" /> and <paramref name="y" /> are <see langword="null" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000FA8 RID: 4008 RVA: 0x00041620 File Offset: 0x0003F820
		public bool Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			string text = x as string;
			if (text != null)
			{
				string text2 = y as string;
				if (text2 != null)
				{
					return this.Equals(text, text2);
				}
			}
			return x.Equals(y);
		}

		/// <summary>When overridden in a derived class, gets the hash code for the specified object.</summary>
		/// <param name="obj">An object.</param>
		/// <returns>A 32-bit signed hash code calculated from the value of the <paramref name="obj" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">Not enough memory is available to allocate the buffer that is required to compute the hash code.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is <see langword="null" />.</exception>
		// Token: 0x06000FA9 RID: 4009 RVA: 0x00041660 File Offset: 0x0003F860
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			if (text != null)
			{
				return this.GetHashCode(text);
			}
			return obj.GetHashCode();
		}

		/// <summary>When overridden in a derived class, compares two strings and returns an indication of their relative sort order.</summary>
		/// <param name="x">A string to compare to <paramref name="y" />.</param>
		/// <param name="y">A string to compare to <paramref name="x" />.</param>
		/// <returns>A signed integer that indicates the relative values of <paramref name="x" /> and <paramref name="y" />, as shown in the following table.  
		///   Value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///  <paramref name="x" /> precedes <paramref name="y" /> in the sort order.  
		///
		///  -or-  
		///
		///  <paramref name="x" /> is <see langword="null" /> and <paramref name="y" /> is not <see langword="null" />.  
		///
		///   Zero  
		///
		///  <paramref name="x" /> is equal to <paramref name="y" />.  
		///
		///  -or-  
		///
		///  <paramref name="x" /> and <paramref name="y" /> are both <see langword="null" />.  
		///
		///   Greater than zero  
		///
		///  <paramref name="x" /> follows <paramref name="y" /> in the sort order.  
		///
		///  -or-  
		///
		///  <paramref name="y" /> is <see langword="null" /> and <paramref name="x" /> is not <see langword="null" />.</returns>
		// Token: 0x06000FAA RID: 4010
		public abstract int Compare(string x, string y);

		/// <summary>When overridden in a derived class, indicates whether two strings are equal.</summary>
		/// <param name="x">A string to compare to <paramref name="y" />.</param>
		/// <param name="y">A string to compare to <paramref name="x" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="x" /> and <paramref name="y" /> refer to the same object, or <paramref name="x" /> and <paramref name="y" /> are equal, or <paramref name="x" /> and <paramref name="y" /> are <see langword="null" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000FAB RID: 4011
		public abstract bool Equals(string x, string y);

		/// <summary>When overridden in a derived class, gets the hash code for the specified string.</summary>
		/// <param name="obj">A string.</param>
		/// <returns>A 32-bit signed hash code calculated from the value of the <paramref name="obj" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">Not enough memory is available to allocate the buffer that is required to compute the hash code.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is <see langword="null" />.</exception>
		// Token: 0x06000FAC RID: 4012
		public abstract int GetHashCode(string obj);

		/// <summary>Initializes a new instance of the <see cref="T:System.StringComparer" /> class.</summary>
		// Token: 0x06000FAD RID: 4013 RVA: 0x0000259F File Offset: 0x0000079F
		protected StringComparer()
		{
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x00041693 File Offset: 0x0003F893
		// Note: this type is marked as 'beforefieldinit'.
		static StringComparer()
		{
		}

		// Token: 0x040012EE RID: 4846
		private static readonly CultureAwareComparer s_invariantCulture = new CultureAwareComparer(CultureInfo.InvariantCulture, CompareOptions.None);

		// Token: 0x040012EF RID: 4847
		private static readonly CultureAwareComparer s_invariantCultureIgnoreCase = new CultureAwareComparer(CultureInfo.InvariantCulture, CompareOptions.IgnoreCase);

		// Token: 0x040012F0 RID: 4848
		private static readonly OrdinalCaseSensitiveComparer s_ordinal = new OrdinalCaseSensitiveComparer();

		// Token: 0x040012F1 RID: 4849
		private static readonly OrdinalIgnoreCaseComparer s_ordinalIgnoreCase = new OrdinalIgnoreCaseComparer();
	}
}
