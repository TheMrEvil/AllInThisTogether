using System;
using System.Globalization;

namespace System.Collections
{
	/// <summary>Compares two objects for equivalence, ignoring the case of strings.</summary>
	// Token: 0x02000A22 RID: 2594
	[Serializable]
	public class CaseInsensitiveComparer : IComparer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CaseInsensitiveComparer" /> class using the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread.</summary>
		// Token: 0x06005BCE RID: 23502 RVA: 0x001353FE File Offset: 0x001335FE
		public CaseInsensitiveComparer()
		{
			this._compareInfo = CultureInfo.CurrentCulture.CompareInfo;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CaseInsensitiveComparer" /> class using the specified <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use for the new <see cref="T:System.Collections.CaseInsensitiveComparer" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is <see langword="null" />.</exception>
		// Token: 0x06005BCF RID: 23503 RVA: 0x00135416 File Offset: 0x00133616
		public CaseInsensitiveComparer(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			this._compareInfo = culture.CompareInfo;
		}

		/// <summary>Gets an instance of <see cref="T:System.Collections.CaseInsensitiveComparer" /> that is associated with the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread and that is always available.</summary>
		/// <returns>An instance of <see cref="T:System.Collections.CaseInsensitiveComparer" /> that is associated with the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread.</returns>
		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06005BD0 RID: 23504 RVA: 0x00135438 File Offset: 0x00133638
		public static CaseInsensitiveComparer Default
		{
			get
			{
				return new CaseInsensitiveComparer(CultureInfo.CurrentCulture);
			}
		}

		/// <summary>Gets an instance of <see cref="T:System.Collections.CaseInsensitiveComparer" /> that is associated with <see cref="P:System.Globalization.CultureInfo.InvariantCulture" /> and that is always available.</summary>
		/// <returns>An instance of <see cref="T:System.Collections.CaseInsensitiveComparer" /> that is associated with <see cref="P:System.Globalization.CultureInfo.InvariantCulture" />.</returns>
		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06005BD1 RID: 23505 RVA: 0x00135444 File Offset: 0x00133644
		public static CaseInsensitiveComparer DefaultInvariant
		{
			get
			{
				if (CaseInsensitiveComparer.s_InvariantCaseInsensitiveComparer == null)
				{
					CaseInsensitiveComparer.s_InvariantCaseInsensitiveComparer = new CaseInsensitiveComparer(CultureInfo.InvariantCulture);
				}
				return CaseInsensitiveComparer.s_InvariantCaseInsensitiveComparer;
			}
		}

		/// <summary>Performs a case-insensitive comparison of two objects of the same type and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
		/// <param name="a">The first object to compare.</param>
		/// <param name="b">The second object to compare.</param>
		/// <returns>A signed integer that indicates the relative values of <paramref name="a" /> and <paramref name="b" />, as shown in the following table.  
		///   Value  
		///
		///   Meaning  
		///
		///   Less than zero  
		///
		///  <paramref name="a" /> is less than <paramref name="b" />, with casing ignored.  
		///
		///   Zero  
		///
		///  <paramref name="a" /> equals <paramref name="b" />, with casing ignored.  
		///
		///   Greater than zero  
		///
		///  <paramref name="a" /> is greater than <paramref name="b" />, with casing ignored.</returns>
		/// <exception cref="T:System.ArgumentException">Neither <paramref name="a" /> nor <paramref name="b" /> implements the <see cref="T:System.IComparable" /> interface.  
		///  -or-  
		///  <paramref name="a" /> and <paramref name="b" /> are of different types.</exception>
		// Token: 0x06005BD2 RID: 23506 RVA: 0x00135468 File Offset: 0x00133668
		public int Compare(object a, object b)
		{
			string text = a as string;
			string text2 = b as string;
			if (text != null && text2 != null)
			{
				return this._compareInfo.Compare(text, text2, CompareOptions.IgnoreCase);
			}
			return Comparer.Default.Compare(a, b);
		}

		// Token: 0x04003882 RID: 14466
		private CompareInfo _compareInfo;

		// Token: 0x04003883 RID: 14467
		private static volatile CaseInsensitiveComparer s_InvariantCaseInsensitiveComparer;
	}
}
