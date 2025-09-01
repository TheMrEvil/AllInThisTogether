using System;

namespace System.Globalization
{
	/// <summary>Provides globalization-related extension methods.</summary>
	// Token: 0x02000965 RID: 2405
	public static class GlobalizationExtensions
	{
		/// <summary>Returns a <see cref="T:System.StringComparer" /> object based on the culture-sensitive string comparison rules of a specified <see cref="T:System.Globalization.CompareInfo" /> object.</summary>
		/// <param name="compareInfo">An object that supports culture-sensitive string comparison.</param>
		/// <param name="options">A value that defines how strings should be compared. <paramref name="options" /> is either the enumeration value <see cref="F:System.Globalization.CompareOptions.Ordinal" />, the enumeration value <see cref="F:System.Globalization.CompareOptions.OrdinalIgnoreCase" />, or a bitwise combination of one or more of the following values: <see cref="F:System.Globalization.CompareOptions.IgnoreCase" />, <see cref="F:System.Globalization.CompareOptions.IgnoreSymbols" />, <see cref="F:System.Globalization.CompareOptions.IgnoreNonSpace" />, <see cref="F:System.Globalization.CompareOptions.IgnoreWidth" />, <see cref="F:System.Globalization.CompareOptions.IgnoreKanaType" />, and <see cref="F:System.Globalization.CompareOptions.StringSort" />.</param>
		/// <returns>An object that can be used to perform string comparisons.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="compareInfo" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> is invalid.</exception>
		// Token: 0x0600553F RID: 21823 RVA: 0x0011DE84 File Offset: 0x0011C084
		public static StringComparer GetStringComparer(this CompareInfo compareInfo, CompareOptions options)
		{
			if (compareInfo == null)
			{
				throw new ArgumentNullException("compareInfo");
			}
			if (options == CompareOptions.Ordinal)
			{
				return StringComparer.Ordinal;
			}
			if (options == CompareOptions.OrdinalIgnoreCase)
			{
				return StringComparer.OrdinalIgnoreCase;
			}
			return new CultureAwareComparer(compareInfo, options);
		}
	}
}
