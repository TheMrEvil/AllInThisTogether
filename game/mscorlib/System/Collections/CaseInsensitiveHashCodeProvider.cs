using System;
using System.Globalization;

namespace System.Collections
{
	/// <summary>Supplies a hash code for an object, using a hashing algorithm that ignores the case of strings.</summary>
	// Token: 0x02000A23 RID: 2595
	[Obsolete("Please use StringComparer instead.")]
	[Serializable]
	public class CaseInsensitiveHashCodeProvider : IHashCodeProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> class using the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread.</summary>
		// Token: 0x06005BD3 RID: 23507 RVA: 0x001354A4 File Offset: 0x001336A4
		public CaseInsensitiveHashCodeProvider()
		{
			this._compareInfo = CultureInfo.CurrentCulture.CompareInfo;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> class using the specified <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use for the new <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="culture" /> is <see langword="null" />.</exception>
		// Token: 0x06005BD4 RID: 23508 RVA: 0x001354BC File Offset: 0x001336BC
		public CaseInsensitiveHashCodeProvider(CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			this._compareInfo = culture.CompareInfo;
		}

		/// <summary>Gets an instance of <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> that is associated with the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread and that is always available.</summary>
		/// <returns>An instance of <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> that is associated with the <see cref="P:System.Threading.Thread.CurrentCulture" /> of the current thread.</returns>
		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06005BD5 RID: 23509 RVA: 0x001354DE File Offset: 0x001336DE
		public static CaseInsensitiveHashCodeProvider Default
		{
			get
			{
				return new CaseInsensitiveHashCodeProvider();
			}
		}

		/// <summary>Gets an instance of <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> that is associated with <see cref="P:System.Globalization.CultureInfo.InvariantCulture" /> and that is always available.</summary>
		/// <returns>An instance of <see cref="T:System.Collections.CaseInsensitiveHashCodeProvider" /> that is associated with <see cref="P:System.Globalization.CultureInfo.InvariantCulture" />.</returns>
		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06005BD6 RID: 23510 RVA: 0x001354E5 File Offset: 0x001336E5
		public static CaseInsensitiveHashCodeProvider DefaultInvariant
		{
			get
			{
				CaseInsensitiveHashCodeProvider result;
				if ((result = CaseInsensitiveHashCodeProvider.s_invariantCaseInsensitiveHashCodeProvider) == null)
				{
					result = (CaseInsensitiveHashCodeProvider.s_invariantCaseInsensitiveHashCodeProvider = new CaseInsensitiveHashCodeProvider(CultureInfo.InvariantCulture));
				}
				return result;
			}
		}

		/// <summary>Returns a hash code for the given object, using a hashing algorithm that ignores the case of strings.</summary>
		/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
		/// <returns>A hash code for the given object, using a hashing algorithm that ignores the case of strings.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is <see langword="null" />.</exception>
		// Token: 0x06005BD7 RID: 23511 RVA: 0x00135504 File Offset: 0x00133704
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			string text = obj as string;
			if (text == null)
			{
				return obj.GetHashCode();
			}
			return this._compareInfo.GetHashCode(text, CompareOptions.IgnoreCase);
		}

		// Token: 0x04003884 RID: 14468
		private static volatile CaseInsensitiveHashCodeProvider s_invariantCaseInsensitiveHashCodeProvider;

		// Token: 0x04003885 RID: 14469
		private readonly CompareInfo _compareInfo;
	}
}
