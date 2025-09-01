using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Globalization
{
	/// <summary>Contains information about the country/region.</summary>
	// Token: 0x020009AD RID: 2477
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class RegionInfo
	{
		/// <summary>Gets the <see cref="T:System.Globalization.RegionInfo" /> that represents the country/region used by the current thread.</summary>
		/// <returns>The <see cref="T:System.Globalization.RegionInfo" /> that represents the country/region used by the current thread.</returns>
		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06005973 RID: 22899 RVA: 0x00132C34 File Offset: 0x00130E34
		public static RegionInfo CurrentRegion
		{
			get
			{
				RegionInfo regionInfo = RegionInfo.currentRegion;
				if (regionInfo == null)
				{
					CultureInfo currentCulture = CultureInfo.CurrentCulture;
					if (currentCulture != null)
					{
						regionInfo = new RegionInfo(currentCulture);
					}
					if (Interlocked.CompareExchange<RegionInfo>(ref RegionInfo.currentRegion, regionInfo, null) != null)
					{
						regionInfo = RegionInfo.currentRegion;
					}
				}
				return regionInfo;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.RegionInfo" /> class based on the country/region associated with the specified culture identifier.</summary>
		/// <param name="culture">A culture identifier.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="culture" /> specifies either an invariant, custom, or neutral culture.</exception>
		// Token: 0x06005974 RID: 22900 RVA: 0x00132C6F File Offset: 0x00130E6F
		public RegionInfo(int culture)
		{
			if (!this.GetByTerritory(CultureInfo.GetCultureInfo(culture)))
			{
				throw new ArgumentException(string.Format("Region ID {0} (0x{0:X4}) is not a supported region.", culture), "culture");
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.RegionInfo" /> class based on the country/region or specific culture, specified by name.</summary>
		/// <param name="name">A string that contains a two-letter code defined in ISO 3166 for country/region.  
		///  -or-  
		///  A string that contains the culture name for a specific culture, custom culture, or Windows-only culture. If the culture name is not in RFC 4646 format, your application should specify the entire culture name instead of just the country/region.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is not a valid country/region name or specific culture name.</exception>
		// Token: 0x06005975 RID: 22901 RVA: 0x00132CA0 File Offset: 0x00130EA0
		public RegionInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException();
			}
			if (this.construct_internal_region_from_name(name.ToUpperInvariant()))
			{
				return;
			}
			if (!this.GetByTerritory(CultureInfo.GetCultureInfo(name)))
			{
				throw new ArgumentException(string.Format("Region name {0} is not supported.", name), "name");
			}
		}

		// Token: 0x06005976 RID: 22902 RVA: 0x00132CF0 File Offset: 0x00130EF0
		private RegionInfo(CultureInfo ci)
		{
			if (ci.LCID == 127)
			{
				this.regionId = 244;
				this.iso2Name = "IV";
				this.iso3Name = "ivc";
				this.win3Name = "IVC";
				this.nativeName = (this.englishName = "Invariant Country");
				this.currencySymbol = "¤";
				this.isoCurrencySymbol = "XDR";
				this.currencyEnglishName = (this.currencyNativeName = "International Monetary Fund");
				return;
			}
			if (ci.Territory == null)
			{
				throw new NotImplementedException("Neutral region info");
			}
			this.construct_internal_region_from_name(ci.Territory.ToUpperInvariant());
		}

		// Token: 0x06005977 RID: 22903 RVA: 0x00132D9D File Offset: 0x00130F9D
		private bool GetByTerritory(CultureInfo ci)
		{
			if (ci == null)
			{
				throw new Exception("INTERNAL ERROR: should not happen.");
			}
			return !ci.IsNeutralCulture && ci.Territory != null && this.construct_internal_region_from_name(ci.Territory.ToUpperInvariant());
		}

		// Token: 0x06005978 RID: 22904
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool construct_internal_region_from_name(string name);

		/// <summary>Gets the name, in English, of the currency used in the country/region.</summary>
		/// <returns>The name, in English, of the currency used in the country/region.</returns>
		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06005979 RID: 22905 RVA: 0x00132DD0 File Offset: 0x00130FD0
		[ComVisible(false)]
		public virtual string CurrencyEnglishName
		{
			get
			{
				return this.currencyEnglishName;
			}
		}

		/// <summary>Gets the currency symbol associated with the country/region.</summary>
		/// <returns>The currency symbol associated with the country/region.</returns>
		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x0600597A RID: 22906 RVA: 0x00132DD8 File Offset: 0x00130FD8
		public virtual string CurrencySymbol
		{
			get
			{
				return this.currencySymbol;
			}
		}

		/// <summary>Gets the full name of the country/region in the language of the localized version of .NET Framework.</summary>
		/// <returns>The full name of the country/region in the language of the localized version of .NET Framework.</returns>
		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x0600597B RID: 22907 RVA: 0x00132DE0 File Offset: 0x00130FE0
		[MonoTODO("DisplayName currently only returns the EnglishName")]
		public virtual string DisplayName
		{
			get
			{
				return this.englishName;
			}
		}

		/// <summary>Gets the full name of the country/region in English.</summary>
		/// <returns>The full name of the country/region in English.</returns>
		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x0600597C RID: 22908 RVA: 0x00132DE0 File Offset: 0x00130FE0
		public virtual string EnglishName
		{
			get
			{
				return this.englishName;
			}
		}

		/// <summary>Gets a unique identification number for a geographical region, country, city, or location.</summary>
		/// <returns>A 32-bit signed number that uniquely identifies a geographical location.</returns>
		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x0600597D RID: 22909 RVA: 0x00132DE8 File Offset: 0x00130FE8
		[ComVisible(false)]
		public virtual int GeoId
		{
			get
			{
				return this.regionId;
			}
		}

		/// <summary>Gets a value indicating whether the country/region uses the metric system for measurements.</summary>
		/// <returns>
		///   <see langword="true" /> if the country/region uses the metric system for measurements; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x0600597E RID: 22910 RVA: 0x00132DF0 File Offset: 0x00130FF0
		public virtual bool IsMetric
		{
			get
			{
				string a = this.iso2Name;
				return !(a == "US") && !(a == "UK");
			}
		}

		/// <summary>Gets the three-character ISO 4217 currency symbol associated with the country/region.</summary>
		/// <returns>The three-character ISO 4217 currency symbol associated with the country/region.</returns>
		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x0600597F RID: 22911 RVA: 0x00132E21 File Offset: 0x00131021
		public virtual string ISOCurrencySymbol
		{
			get
			{
				return this.isoCurrencySymbol;
			}
		}

		/// <summary>Gets the name of a country/region formatted in the native language of the country/region.</summary>
		/// <returns>The native name of the country/region formatted in the language associated with the ISO 3166 country/region code.</returns>
		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06005980 RID: 22912 RVA: 0x00132E29 File Offset: 0x00131029
		[ComVisible(false)]
		public virtual string NativeName
		{
			get
			{
				return this.nativeName;
			}
		}

		/// <summary>Gets the name of the currency used in the country/region, formatted in the native language of the country/region.</summary>
		/// <returns>The native name of the currency used in the country/region, formatted in the language associated with the ISO 3166 country/region code.</returns>
		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06005981 RID: 22913 RVA: 0x00132E31 File Offset: 0x00131031
		[ComVisible(false)]
		public virtual string CurrencyNativeName
		{
			get
			{
				return this.currencyNativeName;
			}
		}

		/// <summary>Gets the name or ISO 3166 two-letter country/region code for the current <see cref="T:System.Globalization.RegionInfo" /> object.</summary>
		/// <returns>The value specified by the <paramref name="name" /> parameter of the <see cref="M:System.Globalization.RegionInfo.#ctor(System.String)" /> constructor. The return value is in uppercase.  
		///  -or-  
		///  The two-letter code defined in ISO 3166 for the country/region specified by the <paramref name="culture" /> parameter of the <see cref="M:System.Globalization.RegionInfo.#ctor(System.Int32)" /> constructor. The return value is in uppercase.</returns>
		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06005982 RID: 22914 RVA: 0x00132E39 File Offset: 0x00131039
		public virtual string Name
		{
			get
			{
				return this.iso2Name;
			}
		}

		/// <summary>Gets the three-letter code defined in ISO 3166 for the country/region.</summary>
		/// <returns>The three-letter code defined in ISO 3166 for the country/region.</returns>
		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06005983 RID: 22915 RVA: 0x00132E41 File Offset: 0x00131041
		public virtual string ThreeLetterISORegionName
		{
			get
			{
				return this.iso3Name;
			}
		}

		/// <summary>Gets the three-letter code assigned by Windows to the country/region represented by this <see cref="T:System.Globalization.RegionInfo" />.</summary>
		/// <returns>The three-letter code assigned by Windows to the country/region represented by this <see cref="T:System.Globalization.RegionInfo" />.</returns>
		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06005984 RID: 22916 RVA: 0x00132E49 File Offset: 0x00131049
		public virtual string ThreeLetterWindowsRegionName
		{
			get
			{
				return this.win3Name;
			}
		}

		/// <summary>Gets the two-letter code defined in ISO 3166 for the country/region.</summary>
		/// <returns>The two-letter code defined in ISO 3166 for the country/region.</returns>
		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06005985 RID: 22917 RVA: 0x00132E39 File Offset: 0x00131039
		public virtual string TwoLetterISORegionName
		{
			get
			{
				return this.iso2Name;
			}
		}

		/// <summary>Determines whether the specified object is the same instance as the current <see cref="T:System.Globalization.RegionInfo" />.</summary>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.RegionInfo" />.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="value" /> parameter is a <see cref="T:System.Globalization.RegionInfo" /> object and its <see cref="P:System.Globalization.RegionInfo.Name" /> property is the same as the <see cref="P:System.Globalization.RegionInfo.Name" /> property of the current <see cref="T:System.Globalization.RegionInfo" /> object; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005986 RID: 22918 RVA: 0x00132E54 File Offset: 0x00131054
		public override bool Equals(object value)
		{
			RegionInfo regionInfo = value as RegionInfo;
			return regionInfo != null && this.Name == regionInfo.Name;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.RegionInfo" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.RegionInfo" />.</returns>
		// Token: 0x06005987 RID: 22919 RVA: 0x00132E7E File Offset: 0x0013107E
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		/// <summary>Returns a string containing the culture name or ISO 3166 two-letter country/region codes specified for the current <see cref="T:System.Globalization.RegionInfo" />.</summary>
		/// <returns>A string containing the culture name or ISO 3166 two-letter country/region codes defined for the current <see cref="T:System.Globalization.RegionInfo" />.</returns>
		// Token: 0x06005988 RID: 22920 RVA: 0x00132E8B File Offset: 0x0013108B
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06005989 RID: 22921 RVA: 0x00132E93 File Offset: 0x00131093
		internal static void ClearCachedData()
		{
			RegionInfo.currentRegion = null;
		}

		// Token: 0x04003761 RID: 14177
		private static RegionInfo currentRegion;

		// Token: 0x04003762 RID: 14178
		private int regionId;

		// Token: 0x04003763 RID: 14179
		private string iso2Name;

		// Token: 0x04003764 RID: 14180
		private string iso3Name;

		// Token: 0x04003765 RID: 14181
		private string win3Name;

		// Token: 0x04003766 RID: 14182
		private string englishName;

		// Token: 0x04003767 RID: 14183
		private string nativeName;

		// Token: 0x04003768 RID: 14184
		private string currencySymbol;

		// Token: 0x04003769 RID: 14185
		private string isoCurrencySymbol;

		// Token: 0x0400376A RID: 14186
		private string currencyEnglishName;

		// Token: 0x0400376B RID: 14187
		private string currencyNativeName;
	}
}
