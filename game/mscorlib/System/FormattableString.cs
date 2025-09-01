using System;
using System.Globalization;

namespace System
{
	/// <summary>Represents a composite format string, along with the arguments to be formatted.</summary>
	// Token: 0x0200011C RID: 284
	public abstract class FormattableString : IFormattable
	{
		/// <summary>Returns the composite format string.</summary>
		/// <returns>The composite format string.</returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000AE6 RID: 2790
		public abstract string Format { get; }

		/// <summary>Returns an object array that contains one or more objects to format.</summary>
		/// <returns>An object array that contains one or more objects to format.</returns>
		// Token: 0x06000AE7 RID: 2791
		public abstract object[] GetArguments();

		/// <summary>Gets the number of arguments to be formatted.</summary>
		/// <returns>The number of arguments to be formatted.</returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000AE8 RID: 2792
		public abstract int ArgumentCount { get; }

		/// <summary>Returns the argument at the specified index position.</summary>
		/// <param name="index">The index of the argument. Its value can range from zero to one less than the value of <see cref="P:System.FormattableString.ArgumentCount" />.</param>
		/// <returns>The argument.</returns>
		// Token: 0x06000AE9 RID: 2793
		public abstract object GetArgument(int index);

		/// <summary>Returns the string that results from formatting the composite format string along with its arguments by using the formatting conventions of a specified culture.</summary>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <returns>A result string formatted by using the conventions of <paramref name="formatProvider" />.</returns>
		// Token: 0x06000AEA RID: 2794
		public abstract string ToString(IFormatProvider formatProvider);

		/// <summary>Returns the string that results from formatting the format string along with its arguments by using the formatting conventions of a specified culture.</summary>
		/// <param name="ignored">A string. This argument is ignored.</param>
		/// <param name="formatProvider">An object that provides culture-specific formatting information.</param>
		/// <returns>A string formatted using the conventions of the <paramref name="formatProvider" /> parameter.</returns>
		// Token: 0x06000AEB RID: 2795 RVA: 0x000288E6 File Offset: 0x00026AE6
		string IFormattable.ToString(string ignored, IFormatProvider formatProvider)
		{
			return this.ToString(formatProvider);
		}

		/// <summary>Returns a result string in which arguments are formatted by using the conventions of the invariant culture.</summary>
		/// <param name="formattable">The object to convert to a result string.</param>
		/// <returns>The string that results from formatting the current instance by using the conventions of the invariant culture.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="formattable" /> is <see langword="null" />.</exception>
		// Token: 0x06000AEC RID: 2796 RVA: 0x000288EF File Offset: 0x00026AEF
		public static string Invariant(FormattableString formattable)
		{
			if (formattable == null)
			{
				throw new ArgumentNullException("formattable");
			}
			return formattable.ToString(CultureInfo.InvariantCulture);
		}

		/// <summary>Returns the string that results from formatting the composite format string along with its arguments by using the formatting conventions of the current culture.</summary>
		/// <returns>A result string formatted by using the conventions of the current culture.</returns>
		// Token: 0x06000AED RID: 2797 RVA: 0x0002890A File Offset: 0x00026B0A
		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>Instantiates a new instance of the <see cref="T:System.FormattableString" /> class.</summary>
		// Token: 0x06000AEE RID: 2798 RVA: 0x0000259F File Offset: 0x0000079F
		protected FormattableString()
		{
		}
	}
}
