using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Mono.Interop;

namespace System.Globalization
{
	/// <summary>Provides information about a specific culture (called a locale for unmanaged code development). The information includes the names for the culture, the writing system, the calendar used, the sort order of strings, and formatting for dates and numbers.</summary>
	// Token: 0x020009A7 RID: 2471
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class CultureInfo : ICloneable, IFormatProvider
	{
		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x060058FA RID: 22778 RVA: 0x0012EDD9 File Offset: 0x0012CFD9
		internal CultureData _cultureData
		{
			get
			{
				return this.m_cultureData;
			}
		}

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x060058FB RID: 22779 RVA: 0x0012EDE1 File Offset: 0x0012CFE1
		internal bool _isInherited
		{
			get
			{
				return this.m_isInherited;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> object that is culture-independent (invariant).</summary>
		/// <returns>The object that is culture-independent (invariant).</returns>
		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x060058FC RID: 22780 RVA: 0x0012EDE9 File Offset: 0x0012CFE9
		public static CultureInfo InvariantCulture
		{
			get
			{
				return CultureInfo.invariant_culture_info;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture used by the current thread.</summary>
		/// <returns>An object that represents the culture used by the current thread.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to <see langword="null" />.</exception>
		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x060058FD RID: 22781 RVA: 0x0012EDF2 File Offset: 0x0012CFF2
		// (set) Token: 0x060058FE RID: 22782 RVA: 0x0012EDFE File Offset: 0x0012CFFE
		public static CultureInfo CurrentCulture
		{
			get
			{
				return Thread.CurrentThread.CurrentCulture;
			}
			set
			{
				Thread.CurrentThread.CurrentCulture = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Globalization.CultureInfo" /> object that represents the current user interface culture used by the Resource Manager to look up culture-specific resources at run time.</summary>
		/// <returns>The culture used by the Resource Manager to look up culture-specific resources at run time.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The property is set to a culture name that cannot be used to locate a resource file. Resource filenames can include only letters, numbers, hyphens, or underscores.</exception>
		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x060058FF RID: 22783 RVA: 0x0012EE0B File Offset: 0x0012D00B
		// (set) Token: 0x06005900 RID: 22784 RVA: 0x0012EE17 File Offset: 0x0012D017
		public static CultureInfo CurrentUICulture
		{
			get
			{
				return Thread.CurrentThread.CurrentUICulture;
			}
			set
			{
				Thread.CurrentThread.CurrentUICulture = value;
			}
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x0012EE24 File Offset: 0x0012D024
		internal static CultureInfo ConstructCurrentCulture()
		{
			if (CultureInfo.default_current_culture != null)
			{
				return CultureInfo.default_current_culture;
			}
			if (GlobalizationMode.Invariant)
			{
				return CultureInfo.InvariantCulture;
			}
			string current_locale_name = CultureInfo.get_current_locale_name();
			CultureInfo cultureInfo = null;
			if (current_locale_name != null)
			{
				try
				{
					cultureInfo = CultureInfo.CreateSpecificCulture(current_locale_name);
				}
				catch
				{
				}
			}
			if (cultureInfo == null)
			{
				cultureInfo = CultureInfo.InvariantCulture;
			}
			else
			{
				cultureInfo.m_isReadOnly = true;
				cultureInfo.m_useUserOverride = true;
			}
			CultureInfo.default_current_culture = cultureInfo;
			return cultureInfo;
		}

		// Token: 0x06005902 RID: 22786 RVA: 0x0012EE94 File Offset: 0x0012D094
		internal static CultureInfo ConstructCurrentUICulture()
		{
			return CultureInfo.ConstructCurrentCulture();
		}

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06005903 RID: 22787 RVA: 0x0012EE9B File Offset: 0x0012D09B
		internal string Territory
		{
			get
			{
				return this.territory;
			}
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06005904 RID: 22788 RVA: 0x0012EEA3 File Offset: 0x0012D0A3
		internal string _name
		{
			get
			{
				return this.m_name;
			}
		}

		/// <summary>Gets the culture types that pertain to the current <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
		/// <returns>A bitwise combination of one or more <see cref="T:System.Globalization.CultureTypes" /> values. There is no default value.</returns>
		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06005905 RID: 22789 RVA: 0x0012EEAC File Offset: 0x0012D0AC
		[ComVisible(false)]
		public CultureTypes CultureTypes
		{
			get
			{
				CultureTypes cultureTypes = (CultureTypes)0;
				foreach (object obj in Enum.GetValues(typeof(CultureTypes)))
				{
					CultureTypes cultureTypes2 = (CultureTypes)obj;
					if (Array.IndexOf<CultureInfo>(CultureInfo.GetCultures(cultureTypes2), this) >= 0)
					{
						cultureTypes |= cultureTypes2;
					}
				}
				return cultureTypes;
			}
		}

		/// <summary>Gets an alternate user interface culture suitable for console applications when the default graphic user interface culture is unsuitable.</summary>
		/// <returns>An alternate culture that is used to read and display text on the console.</returns>
		// Token: 0x06005906 RID: 22790 RVA: 0x0012EF20 File Offset: 0x0012D120
		[ComVisible(false)]
		public CultureInfo GetConsoleFallbackUICulture()
		{
			string name = this.Name;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
			if (num <= 1260172255U)
			{
				if (num <= 939759947U)
				{
					if (num <= 249681006U)
					{
						if (num <= 198587497U)
						{
							if (num != 64366545U)
							{
								if (num != 77939050U)
								{
									if (num != 198587497U)
									{
										goto IL_6C2;
									}
									if (!(name == "ar-SA"))
									{
										goto IL_6C2;
									}
								}
								else if (!(name == "mr-IN"))
								{
									goto IL_6C2;
								}
							}
							else if (!(name == "ar-SY"))
							{
								goto IL_6C2;
							}
						}
						else if (num != 233820021U)
						{
							if (num != 236085687U)
							{
								if (num != 249681006U)
								{
									goto IL_6C2;
								}
								if (!(name == "hi-IN"))
								{
									goto IL_6C2;
								}
							}
							else if (!(name == "ar-KW"))
							{
								goto IL_6C2;
							}
						}
						else if (!(name == "ar-EG"))
						{
							goto IL_6C2;
						}
					}
					else if (num <= 469295067U)
					{
						if (num != 419506663U)
						{
							if (num != 434712723U)
							{
								if (num != 469295067U)
								{
									goto IL_6C2;
								}
								if (!(name == "ar-AE"))
								{
									goto IL_6C2;
								}
							}
							else if (!(name == "sa-IN"))
							{
								goto IL_6C2;
							}
						}
						else if (!(name == "ar-BH"))
						{
							goto IL_6C2;
						}
					}
					else if (num != 511763911U)
					{
						if (num != 907337542U)
						{
							if (num != 939759947U)
							{
								goto IL_6C2;
							}
							if (!(name == "ar-MA"))
							{
								goto IL_6C2;
							}
							goto IL_6B7;
						}
						else if (!(name == "ar-JO"))
						{
							goto IL_6C2;
						}
					}
					else if (!(name == "vi-VN"))
					{
						goto IL_6C2;
					}
				}
				else if (num <= 1074569279U)
				{
					if (num <= 1011170994U)
					{
						if (num != 944060518U)
						{
							if (num != 944899161U)
							{
								if (num != 1011170994U)
								{
									goto IL_6C2;
								}
								if (!(name == "te"))
								{
									goto IL_6C2;
								}
							}
							else if (!(name == "sa"))
							{
								goto IL_6C2;
							}
						}
						else if (!(name == "ta"))
						{
							goto IL_6C2;
						}
					}
					else if (num != 1011465184U)
					{
						if (num != 1070729495U)
						{
							if (num != 1074569279U)
							{
								goto IL_6C2;
							}
							if (!(name == "ar-IQ"))
							{
								goto IL_6C2;
							}
						}
						else if (!(name == "ar-QA"))
						{
							goto IL_6C2;
						}
					}
					else if (!(name == "vi"))
					{
						goto IL_6C2;
					}
				}
				else if (num <= 1123180923U)
				{
					if (num != 1094514636U)
					{
						if (num != 1095059089U)
						{
							if (num != 1123180923U)
							{
								goto IL_6C2;
							}
							if (!(name == "ar-DZ"))
							{
								goto IL_6C2;
							}
							goto IL_6B7;
						}
						else if (!(name == "th"))
						{
							goto IL_6C2;
						}
					}
					else if (!(name == "kn"))
					{
						goto IL_6C2;
					}
				}
				else if (num != 1141238470U)
				{
					if (num != 1162022470U)
					{
						if (num != 1260172255U)
						{
							goto IL_6C2;
						}
						if (!(name == "dv"))
						{
							goto IL_6C2;
						}
					}
					else if (!(name == "ur"))
					{
						goto IL_6C2;
					}
				}
				else if (!(name == "ar-LY"))
				{
					goto IL_6C2;
				}
			}
			else if (num <= 1756775346U)
			{
				if (num <= 1527123707U)
				{
					if (num <= 1429081278U)
					{
						if (num != 1277200137U)
						{
							if (num != 1347311754U)
							{
								if (num != 1429081278U)
								{
									goto IL_6C2;
								}
								if (!(name == "mr"))
								{
									goto IL_6C2;
								}
							}
							else if (!(name == "pa"))
							{
								goto IL_6C2;
							}
						}
						else if (!(name == "gu"))
						{
							goto IL_6C2;
						}
					}
					else if (num != 1456070279U)
					{
						if (num != 1458211363U)
						{
							if (num != 1527123707U)
							{
								goto IL_6C2;
							}
							if (!(name == "ar-LB"))
							{
								goto IL_6C2;
							}
						}
						else if (!(name == "gu-IN"))
						{
							goto IL_6C2;
						}
					}
					else
					{
						if (!(name == "ar-TN"))
						{
							goto IL_6C2;
						}
						goto IL_6B7;
					}
				}
				else if (num <= 1622153968U)
				{
					if (num != 1547363254U)
					{
						if (num != 1562713850U)
						{
							if (num != 1622153968U)
							{
								goto IL_6C2;
							}
							if (!(name == "kok-IN"))
							{
								goto IL_6C2;
							}
						}
						else if (!(name == "ar"))
						{
							goto IL_6C2;
						}
					}
					else if (!(name == "he"))
					{
						goto IL_6C2;
					}
				}
				else if (num != 1680010088U)
				{
					if (num != 1748694682U)
					{
						if (num != 1756775346U)
						{
							goto IL_6C2;
						}
						if (!(name == "ta-IN"))
						{
							goto IL_6C2;
						}
					}
					else if (!(name == "hi"))
					{
						goto IL_6C2;
					}
				}
				else if (!(name == "fa"))
				{
					goto IL_6C2;
				}
			}
			else if (num <= 3073845542U)
			{
				if (num <= 2153224060U)
				{
					if (num != 1846834581U)
					{
						if (num != 2046577884U)
						{
							if (num != 2153224060U)
							{
								goto IL_6C2;
							}
							if (!(name == "he-IL"))
							{
								goto IL_6C2;
							}
						}
						else if (!(name == "kok"))
						{
							goto IL_6C2;
						}
					}
					else if (!(name == "dv-MV"))
					{
						goto IL_6C2;
					}
				}
				else if (num != 2902799296U)
				{
					if (num != 3060605246U)
					{
						if (num != 3073845542U)
						{
							goto IL_6C2;
						}
						if (!(name == "te-IN"))
						{
							goto IL_6C2;
						}
					}
					else if (!(name == "pa-IN"))
					{
						goto IL_6C2;
					}
				}
				else if (!(name == "kn-IN"))
				{
					goto IL_6C2;
				}
			}
			else if (num <= 3477219856U)
			{
				if (num != 3294142633U)
				{
					if (num != 3311105148U)
					{
						if (num != 3477219856U)
						{
							goto IL_6C2;
						}
						if (!(name == "fa-IR"))
						{
							goto IL_6C2;
						}
					}
					else if (!(name == "syr-SY"))
					{
						goto IL_6C2;
					}
				}
				else if (!(name == "syr"))
				{
					goto IL_6C2;
				}
			}
			else if (num != 3957656723U)
			{
				if (num != 4027935912U)
				{
					if (num != 4091062904U)
					{
						goto IL_6C2;
					}
					if (!(name == "th-TH"))
					{
						goto IL_6C2;
					}
				}
				else if (!(name == "ur-PK"))
				{
					goto IL_6C2;
				}
			}
			else if (!(name == "ar-YE"))
			{
				goto IL_6C2;
			}
			return CultureInfo.GetCultureInfo("en");
			IL_6B7:
			return CultureInfo.GetCultureInfo("fr");
			IL_6C2:
			if ((this.CultureTypes & CultureTypes.WindowsOnlyCultures) == (CultureTypes)0)
			{
				return this;
			}
			return CultureInfo.InvariantCulture;
		}

		/// <summary>Deprecated. Gets the RFC 4646 standard identification for a language.</summary>
		/// <returns>A string that is the RFC 4646 standard identification for a language.</returns>
		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06005907 RID: 22791 RVA: 0x0012F604 File Offset: 0x0012D804
		[ComVisible(false)]
		public string IetfLanguageTag
		{
			get
			{
				string name = this.Name;
				if (name == "zh-CHS")
				{
					return "zh-Hans";
				}
				if (!(name == "zh-CHT"))
				{
					return this.Name;
				}
				return "zh-Hant";
			}
		}

		/// <summary>Gets the active input locale identifier.</summary>
		/// <returns>A 32-bit signed number that specifies an input locale identifier.</returns>
		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06005908 RID: 22792 RVA: 0x0012F648 File Offset: 0x0012D848
		[ComVisible(false)]
		public virtual int KeyboardLayoutId
		{
			get
			{
				int lcid = this.LCID;
				if (lcid <= 1034)
				{
					if (lcid == 4)
					{
						return 2052;
					}
					if (lcid == 1034)
					{
						return 3082;
					}
				}
				else
				{
					if (lcid == 31748)
					{
						return 1028;
					}
					if (lcid == 31770)
					{
						return 2074;
					}
				}
				if (this.LCID >= 1024)
				{
					return this.LCID;
				}
				return this.LCID + 1024;
			}
		}

		/// <summary>Gets the culture identifier for the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The culture identifier for the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06005909 RID: 22793 RVA: 0x0012F6BC File Offset: 0x0012D8BC
		public virtual int LCID
		{
			get
			{
				return this.cultureID;
			}
		}

		/// <summary>Gets the culture name in the format languagecode2-country/regioncode2.</summary>
		/// <returns>The culture name in the format languagecode2-country/regioncode2. languagecode2 is a lowercase two-letter code derived from ISO 639-1. country/regioncode2 is derived from ISO 3166 and usually consists of two uppercase letters, or a BCP-47 language tag.</returns>
		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x0600590A RID: 22794 RVA: 0x0012EEA3 File Offset: 0x0012D0A3
		public virtual string Name
		{
			get
			{
				return this.m_name;
			}
		}

		/// <summary>Gets the culture name, consisting of the language, the country/region, and the optional script, that the culture is set to display.</summary>
		/// <returns>The culture name. consisting of the full name of the language, the full name of the country/region, and the optional script. The format is discussed in the description of the <see cref="T:System.Globalization.CultureInfo" /> class.</returns>
		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x0600590B RID: 22795 RVA: 0x0012F6C4 File Offset: 0x0012D8C4
		public virtual string NativeName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.nativename;
			}
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x0600590C RID: 22796 RVA: 0x0012F6DA File Offset: 0x0012D8DA
		internal string NativeCalendarName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.native_calendar_names[(this.default_calendar_type >> 8) - 1];
			}
		}

		/// <summary>Gets the default calendar used by the culture.</summary>
		/// <returns>A <see cref="T:System.Globalization.Calendar" /> that represents the default calendar used by the culture.</returns>
		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x0600590D RID: 22797 RVA: 0x0012F6FB File Offset: 0x0012D8FB
		public virtual Calendar Calendar
		{
			get
			{
				if (this.calendar == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					this.calendar = CultureInfo.CreateCalendar(this.default_calendar_type);
				}
				return this.calendar;
			}
		}

		/// <summary>Gets the list of calendars that can be used by the culture.</summary>
		/// <returns>An array of type <see cref="T:System.Globalization.Calendar" /> that represents the calendars that can be used by the culture represented by the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x0600590E RID: 22798 RVA: 0x0012F72A File Offset: 0x0012D92A
		[MonoLimitation("Optional calendars are not supported only default calendar is returned")]
		public virtual Calendar[] OptionalCalendars
		{
			get
			{
				return new Calendar[]
				{
					this.Calendar
				};
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> that represents the parent culture of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The <see cref="T:System.Globalization.CultureInfo" /> that represents the parent culture of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x0600590F RID: 22799 RVA: 0x0012F73C File Offset: 0x0012D93C
		public virtual CultureInfo Parent
		{
			get
			{
				if (this.parent_culture == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					if (this.parent_lcid == this.cultureID)
					{
						if (this.parent_lcid == 31748 && this.EnglishName[this.EnglishName.Length - 1] == 'y')
						{
							return this.parent_culture = new CultureInfo("zh-Hant");
						}
						if (this.parent_lcid == 4 && this.EnglishName[this.EnglishName.Length - 1] == 'y')
						{
							return this.parent_culture = new CultureInfo("zh-Hans");
						}
						return null;
					}
					else if (this.parent_lcid == 127)
					{
						this.parent_culture = CultureInfo.InvariantCulture;
					}
					else if (this.cultureID == 127)
					{
						this.parent_culture = this;
					}
					else if (this.cultureID == 1028)
					{
						this.parent_culture = new CultureInfo("zh-CHT");
					}
					else
					{
						this.parent_culture = new CultureInfo(this.parent_lcid);
					}
				}
				return this.parent_culture;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.TextInfo" /> that defines the writing system associated with the culture.</summary>
		/// <returns>The <see cref="T:System.Globalization.TextInfo" /> that defines the writing system associated with the culture.</returns>
		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06005910 RID: 22800 RVA: 0x0012F848 File Offset: 0x0012DA48
		public virtual TextInfo TextInfo
		{
			get
			{
				if (this.textInfo == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					lock (this)
					{
						if (this.textInfo == null)
						{
							this.textInfo = this.CreateTextInfo(this.m_isReadOnly);
						}
					}
				}
				return this.textInfo;
			}
		}

		/// <summary>Gets the ISO 639-2 three-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The ISO 639-2 three-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06005911 RID: 22801 RVA: 0x0012F8BC File Offset: 0x0012DABC
		public virtual string ThreeLetterISOLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.iso3lang;
			}
		}

		/// <summary>Gets the three-letter code for the language as defined in the Windows API.</summary>
		/// <returns>The three-letter code for the language as defined in the Windows API.</returns>
		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06005912 RID: 22802 RVA: 0x0012F8D2 File Offset: 0x0012DAD2
		public virtual string ThreeLetterWindowsLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.win3lang;
			}
		}

		/// <summary>Gets the ISO 639-1 two-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>The ISO 639-1 two-letter code for the language of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06005913 RID: 22803 RVA: 0x0012F8E8 File Offset: 0x0012DAE8
		public virtual string TwoLetterISOLanguageName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.iso2lang;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.CultureInfo" /> object uses the user-selected culture settings.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Globalization.CultureInfo" /> uses the user-selected culture settings; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06005914 RID: 22804 RVA: 0x0012F8FE File Offset: 0x0012DAFE
		public bool UseUserOverride
		{
			get
			{
				return this.m_useUserOverride;
			}
		}

		/// <summary>Refreshes cached culture-related information.</summary>
		// Token: 0x06005915 RID: 22805 RVA: 0x0012F908 File Offset: 0x0012DB08
		public void ClearCachedData()
		{
			object obj = CultureInfo.shared_table_lock;
			lock (obj)
			{
				CultureInfo.shared_by_number = null;
				CultureInfo.shared_by_name = null;
			}
			CultureInfo.default_current_culture = null;
			RegionInfo.ClearCachedData();
			TimeZone.ClearCachedData();
			TimeZoneInfo.ClearCachedData();
		}

		/// <summary>Creates a copy of the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <returns>A copy of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x06005916 RID: 22806 RVA: 0x0012F964 File Offset: 0x0012DB64
		public virtual object Clone()
		{
			if (!this.constructed)
			{
				this.Construct();
			}
			CultureInfo cultureInfo = (CultureInfo)base.MemberwiseClone();
			cultureInfo.m_isReadOnly = false;
			cultureInfo.cached_serialized_form = null;
			if (!this.IsNeutralCulture)
			{
				cultureInfo.NumberFormat = (NumberFormatInfo)this.NumberFormat.Clone();
				cultureInfo.DateTimeFormat = (DateTimeFormatInfo)this.DateTimeFormat.Clone();
			}
			return cultureInfo;
		}

		/// <summary>Determines whether the specified object is the same culture as the current <see cref="T:System.Globalization.CultureInfo" />.</summary>
		/// <param name="value">The object to compare with the current <see cref="T:System.Globalization.CultureInfo" />.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="value" /> is the same culture as the current <see cref="T:System.Globalization.CultureInfo" />; otherwise, <see langword="false" />.</returns>
		// Token: 0x06005917 RID: 22807 RVA: 0x0012F9D0 File Offset: 0x0012DBD0
		public override bool Equals(object value)
		{
			CultureInfo cultureInfo = value as CultureInfo;
			return cultureInfo != null && cultureInfo.cultureID == this.cultureID && cultureInfo.m_name == this.m_name;
		}

		/// <summary>Gets the list of supported cultures filtered by the specified <see cref="T:System.Globalization.CultureTypes" /> parameter.</summary>
		/// <param name="types">A bitwise combination of the enumeration values that filter the cultures to retrieve.</param>
		/// <returns>An array that contains the cultures specified by the <paramref name="types" /> parameter. The array of cultures is unsorted.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="types" /> specifies an invalid combination of <see cref="T:System.Globalization.CultureTypes" /> values.</exception>
		// Token: 0x06005918 RID: 22808 RVA: 0x0012FA08 File Offset: 0x0012DC08
		public static CultureInfo[] GetCultures(CultureTypes types)
		{
			bool flag = (types & CultureTypes.NeutralCultures) > (CultureTypes)0;
			bool specific = (types & CultureTypes.SpecificCultures) > (CultureTypes)0;
			bool installed = (types & CultureTypes.InstalledWin32Cultures) > (CultureTypes)0;
			CultureInfo[] array = CultureInfo.internal_get_cultures(flag, specific, installed);
			int i = 0;
			if (flag && array.Length != 0 && array[0] == null)
			{
				array[i++] = (CultureInfo)CultureInfo.InvariantCulture.Clone();
			}
			while (i < array.Length)
			{
				CultureInfo cultureInfo = array[i];
				CultureInfo.Data textInfoData = cultureInfo.GetTextInfoData();
				CultureInfo cultureInfo2 = array[i];
				string name = cultureInfo.m_name;
				bool useUserOverride = false;
				int datetimeIndex = cultureInfo.datetime_index;
				int calendarType = cultureInfo.CalendarType;
				int numberIndex = cultureInfo.number_index;
				string text = cultureInfo.iso2lang;
				int ansi = textInfoData.ansi;
				int oem = textInfoData.oem;
				int mac = textInfoData.mac;
				int ebcdic = textInfoData.ebcdic;
				bool right_to_left = textInfoData.right_to_left;
				char list_sep = (char)textInfoData.list_sep;
				cultureInfo2.m_cultureData = CultureData.GetCultureData(name, useUserOverride, datetimeIndex, calendarType, numberIndex, text, ansi, oem, mac, ebcdic, right_to_left, list_sep.ToString());
				i++;
			}
			return array;
		}

		// Token: 0x06005919 RID: 22809 RVA: 0x0012FADD File Offset: 0x0012DCDD
		private unsafe CultureInfo.Data GetTextInfoData()
		{
			return *(CultureInfo.Data*)this.textinfo_data;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.CultureInfo" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x0600591A RID: 22810 RVA: 0x0012FAEA File Offset: 0x0012DCEA
		public override int GetHashCode()
		{
			return this.cultureID.GetHashCode();
		}

		/// <summary>Returns a read-only wrapper around the specified <see cref="T:System.Globalization.CultureInfo" /> object.</summary>
		/// <param name="ci">The <see cref="T:System.Globalization.CultureInfo" /> object to wrap.</param>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> wrapper around <paramref name="ci" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ci" /> is null.</exception>
		// Token: 0x0600591B RID: 22811 RVA: 0x0012FAF8 File Offset: 0x0012DCF8
		public static CultureInfo ReadOnly(CultureInfo ci)
		{
			if (ci == null)
			{
				throw new ArgumentNullException("ci");
			}
			if (ci.m_isReadOnly)
			{
				return ci;
			}
			CultureInfo cultureInfo = (CultureInfo)ci.Clone();
			cultureInfo.m_isReadOnly = true;
			if (cultureInfo.numInfo != null)
			{
				cultureInfo.numInfo = NumberFormatInfo.ReadOnly(cultureInfo.numInfo);
			}
			if (cultureInfo.dateTimeInfo != null)
			{
				cultureInfo.dateTimeInfo = DateTimeFormatInfo.ReadOnly(cultureInfo.dateTimeInfo);
			}
			if (cultureInfo.textInfo != null)
			{
				cultureInfo.textInfo = TextInfo.ReadOnly(cultureInfo.textInfo);
			}
			return cultureInfo;
		}

		/// <summary>Returns a string containing the name of the current <see cref="T:System.Globalization.CultureInfo" /> in the format languagecode2-country/regioncode2.</summary>
		/// <returns>A string containing the name of the current <see cref="T:System.Globalization.CultureInfo" />.</returns>
		// Token: 0x0600591C RID: 22812 RVA: 0x0012EEA3 File Offset: 0x0012D0A3
		public override string ToString()
		{
			return this.m_name;
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CompareInfo" /> that defines how to compare strings for the culture.</summary>
		/// <returns>The <see cref="T:System.Globalization.CompareInfo" /> that defines how to compare strings for the culture.</returns>
		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x0600591D RID: 22813 RVA: 0x0012FB90 File Offset: 0x0012DD90
		public virtual CompareInfo CompareInfo
		{
			get
			{
				if (this.compareInfo == null)
				{
					if (!this.constructed)
					{
						this.Construct();
					}
					lock (this)
					{
						if (this.compareInfo == null)
						{
							this.compareInfo = new CompareInfo(this);
						}
					}
				}
				return this.compareInfo;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.CultureInfo" /> represents a neutral culture.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Globalization.CultureInfo" /> represents a neutral culture; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x0600591E RID: 22814 RVA: 0x0012FC00 File Offset: 0x0012DE00
		public virtual bool IsNeutralCulture
		{
			get
			{
				if (this.cultureID == 127)
				{
					return false;
				}
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.territory == null;
			}
		}

		// Token: 0x0600591F RID: 22815 RVA: 0x00004BF9 File Offset: 0x00002DF9
		private void CheckNeutral()
		{
		}

		/// <summary>Gets or sets a <see cref="T:System.Globalization.NumberFormatInfo" /> that defines the culturally appropriate format of displaying numbers, currency, and percentage.</summary>
		/// <returns>A <see cref="T:System.Globalization.NumberFormatInfo" /> that defines the culturally appropriate format of displaying numbers, currency, and percentage.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Globalization.CultureInfo.NumberFormat" /> property or any of the <see cref="T:System.Globalization.NumberFormatInfo" /> properties is set, and the <see cref="T:System.Globalization.CultureInfo" /> is read-only.</exception>
		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06005920 RID: 22816 RVA: 0x0012FC28 File Offset: 0x0012DE28
		// (set) Token: 0x06005921 RID: 22817 RVA: 0x0012FC68 File Offset: 0x0012DE68
		public virtual NumberFormatInfo NumberFormat
		{
			get
			{
				if (this.numInfo == null)
				{
					this.numInfo = new NumberFormatInfo(this.m_cultureData)
					{
						isReadOnly = this.m_isReadOnly
					};
				}
				return this.numInfo;
			}
			set
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				if (this.m_isReadOnly)
				{
					throw new InvalidOperationException("This instance is read only");
				}
				if (value == null)
				{
					throw new ArgumentNullException("NumberFormat");
				}
				this.numInfo = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Globalization.DateTimeFormatInfo" /> that defines the culturally appropriate format of displaying dates and times.</summary>
		/// <returns>A <see cref="T:System.Globalization.DateTimeFormatInfo" /> that defines the culturally appropriate format of displaying dates and times.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property is set to null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Globalization.CultureInfo.DateTimeFormat" /> property or any of the <see cref="T:System.Globalization.DateTimeFormatInfo" /> properties is set, and the <see cref="T:System.Globalization.CultureInfo" /> is read-only.</exception>
		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06005922 RID: 22818 RVA: 0x0012FCA4 File Offset: 0x0012DEA4
		// (set) Token: 0x06005923 RID: 22819 RVA: 0x0012FD1B File Offset: 0x0012DF1B
		public virtual DateTimeFormatInfo DateTimeFormat
		{
			get
			{
				if (this.dateTimeInfo != null)
				{
					return this.dateTimeInfo;
				}
				if (!this.constructed)
				{
					this.Construct();
				}
				this.CheckNeutral();
				DateTimeFormatInfo dateTimeFormatInfo;
				if (GlobalizationMode.Invariant)
				{
					dateTimeFormatInfo = new DateTimeFormatInfo();
				}
				else
				{
					dateTimeFormatInfo = new DateTimeFormatInfo(this.m_cultureData, this.Calendar);
				}
				dateTimeFormatInfo._isReadOnly = this.m_isReadOnly;
				Thread.MemoryBarrier();
				this.dateTimeInfo = dateTimeFormatInfo;
				return this.dateTimeInfo;
			}
			set
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				if (this.m_isReadOnly)
				{
					throw new InvalidOperationException("This instance is read only");
				}
				if (value == null)
				{
					throw new ArgumentNullException("DateTimeFormat");
				}
				this.dateTimeInfo = value;
			}
		}

		/// <summary>Gets the full localized culture name.</summary>
		/// <returns>The full localized culture name in the format languagefull [country/regionfull], where languagefull is the full name of the language and country/regionfull is the full name of the country/region.</returns>
		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06005924 RID: 22820 RVA: 0x0012FD55 File Offset: 0x0012DF55
		public virtual string DisplayName
		{
			get
			{
				return this.EnglishName;
			}
		}

		/// <summary>Gets the culture name in the format languagefull [country/regionfull] in English.</summary>
		/// <returns>The culture name in the format languagefull [country/regionfull] in English, where languagefull is the full name of the language and country/regionfull is the full name of the country/region.</returns>
		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06005925 RID: 22821 RVA: 0x0012FD5D File Offset: 0x0012DF5D
		public virtual string EnglishName
		{
			get
			{
				if (!this.constructed)
				{
					this.Construct();
				}
				return this.englishname;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> that represents the culture installed with the operating system.</summary>
		/// <returns>The <see cref="T:System.Globalization.CultureInfo" /> that represents the culture installed with the operating system.</returns>
		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06005926 RID: 22822 RVA: 0x0012EE94 File Offset: 0x0012D094
		public static CultureInfo InstalledUICulture
		{
			get
			{
				return CultureInfo.ConstructCurrentCulture();
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.CultureInfo" /> is read-only.</summary>
		/// <returns>
		///   <see langword="true" /> if the current <see cref="T:System.Globalization.CultureInfo" /> is read-only; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06005927 RID: 22823 RVA: 0x0012FD73 File Offset: 0x0012DF73
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		/// <summary>Gets an object that defines how to format the specified type.</summary>
		/// <param name="formatType">The <see cref="T:System.Type" /> for which to get a formatting object. This method only supports the <see cref="T:System.Globalization.NumberFormatInfo" /> and <see cref="T:System.Globalization.DateTimeFormatInfo" /> types.</param>
		/// <returns>The value of the <see cref="P:System.Globalization.CultureInfo.NumberFormat" /> property, which is a <see cref="T:System.Globalization.NumberFormatInfo" /> containing the default number format information for the current <see cref="T:System.Globalization.CultureInfo" />, if <paramref name="formatType" /> is the <see cref="T:System.Type" /> object for the <see cref="T:System.Globalization.NumberFormatInfo" /> class.  
		///  -or-  
		///  The value of the <see cref="P:System.Globalization.CultureInfo.DateTimeFormat" /> property, which is a <see cref="T:System.Globalization.DateTimeFormatInfo" /> containing the default date and time format information for the current <see cref="T:System.Globalization.CultureInfo" />, if <paramref name="formatType" /> is the <see cref="T:System.Type" /> object for the <see cref="T:System.Globalization.DateTimeFormatInfo" /> class.  
		///  -or-  
		///  null, if <paramref name="formatType" /> is any other object.</returns>
		// Token: 0x06005928 RID: 22824 RVA: 0x0012FD7C File Offset: 0x0012DF7C
		public virtual object GetFormat(Type formatType)
		{
			object result = null;
			if (formatType == typeof(NumberFormatInfo))
			{
				result = this.NumberFormat;
			}
			else if (formatType == typeof(DateTimeFormatInfo))
			{
				result = this.DateTimeFormat;
			}
			return result;
		}

		// Token: 0x06005929 RID: 22825 RVA: 0x0012FDC0 File Offset: 0x0012DFC0
		private void Construct()
		{
			this.construct_internal_locale_from_lcid(this.cultureID);
			this.constructed = true;
		}

		// Token: 0x0600592A RID: 22826
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool construct_internal_locale_from_lcid(int lcid);

		// Token: 0x0600592B RID: 22827
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool construct_internal_locale_from_name(string name);

		// Token: 0x0600592C RID: 22828
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string get_current_locale_name();

		// Token: 0x0600592D RID: 22829
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern CultureInfo[] internal_get_cultures(bool neutral, bool specific, bool installed);

		// Token: 0x0600592E RID: 22830 RVA: 0x0012FDD8 File Offset: 0x0012DFD8
		private void ConstructInvariant(bool read_only)
		{
			this.cultureID = 127;
			this.numInfo = NumberFormatInfo.InvariantInfo;
			if (!read_only)
			{
				this.numInfo = (NumberFormatInfo)this.numInfo.Clone();
			}
			this.textInfo = TextInfo.Invariant;
			this.m_name = string.Empty;
			this.englishname = (this.nativename = "Invariant Language (Invariant Country)");
			this.iso3lang = "IVL";
			this.iso2lang = "iv";
			this.win3lang = "IVL";
			this.default_calendar_type = 257;
		}

		// Token: 0x0600592F RID: 22831 RVA: 0x0012FE6F File Offset: 0x0012E06F
		private TextInfo CreateTextInfo(bool readOnly)
		{
			TextInfo textInfo = new TextInfo(this.m_cultureData);
			textInfo.SetReadOnlyState(readOnly);
			return textInfo;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by the culture identifier.</summary>
		/// <param name="culture">A predefined <see cref="T:System.Globalization.CultureInfo" /> identifier, <see cref="P:System.Globalization.CultureInfo.LCID" /> property of an existing <see cref="T:System.Globalization.CultureInfo" /> object, or Windows-only culture identifier.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="culture" /> is less than zero.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="culture" /> is not a valid culture identifier. See the Notes to Callers section for more information.</exception>
		// Token: 0x06005930 RID: 22832 RVA: 0x0012FE83 File Offset: 0x0012E083
		public CultureInfo(int culture) : this(culture, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by the culture identifier and on the Boolean that specifies whether to use the user-selected culture settings from the system.</summary>
		/// <param name="culture">A predefined <see cref="T:System.Globalization.CultureInfo" /> identifier, <see cref="P:System.Globalization.CultureInfo.LCID" /> property of an existing <see cref="T:System.Globalization.CultureInfo" /> object, or Windows-only culture identifier.</param>
		/// <param name="useUserOverride">A Boolean that denotes whether to use the user-selected culture settings (<see langword="true" />) or the default culture settings (<see langword="false" />).</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="culture" /> is less than zero.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="culture" /> is not a valid culture identifier. See the Notes to Callers section for more information.</exception>
		// Token: 0x06005931 RID: 22833 RVA: 0x0012FE8D File Offset: 0x0012E08D
		public CultureInfo(int culture, bool useUserOverride) : this(culture, useUserOverride, false)
		{
		}

		// Token: 0x06005932 RID: 22834 RVA: 0x0012FE98 File Offset: 0x0012E098
		private CultureInfo(int culture, bool useUserOverride, bool read_only)
		{
			if (culture < 0)
			{
				throw new ArgumentOutOfRangeException("culture", "Positive number required.");
			}
			this.constructed = true;
			this.m_isReadOnly = read_only;
			this.m_useUserOverride = useUserOverride;
			if (culture == 127)
			{
				this.m_cultureData = CultureData.Invariant;
				this.ConstructInvariant(read_only);
				return;
			}
			if (!this.construct_internal_locale_from_lcid(culture))
			{
				string message = string.Format(CultureInfo.InvariantCulture, "Culture ID {0} (0x{1}) is not a supported culture.", culture.ToString(CultureInfo.InvariantCulture), culture.ToString("X4", CultureInfo.InvariantCulture));
				throw new CultureNotFoundException("culture", message);
			}
			CultureInfo.Data textInfoData = this.GetTextInfoData();
			string name = this.m_name;
			bool useUserOverride2 = this.m_useUserOverride;
			int datetimeIndex = this.datetime_index;
			int calendarType = this.CalendarType;
			int numberIndex = this.number_index;
			string text = this.iso2lang;
			int ansi = textInfoData.ansi;
			int oem = textInfoData.oem;
			int mac = textInfoData.mac;
			int ebcdic = textInfoData.ebcdic;
			bool right_to_left = textInfoData.right_to_left;
			char list_sep = (char)textInfoData.list_sep;
			this.m_cultureData = CultureData.GetCultureData(name, useUserOverride2, datetimeIndex, calendarType, numberIndex, text, ansi, oem, mac, ebcdic, right_to_left, list_sep.ToString());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by name.</summary>
		/// <param name="name">A predefined <see cref="T:System.Globalization.CultureInfo" /> name, <see cref="P:System.Globalization.CultureInfo.Name" /> of an existing <see cref="T:System.Globalization.CultureInfo" />, or Windows-only culture name. <paramref name="name" /> is not case-sensitive.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> is not a valid culture name. For more information, see the Notes to Callers section.</exception>
		// Token: 0x06005933 RID: 22835 RVA: 0x0012FF90 File Offset: 0x0012E190
		public CultureInfo(string name) : this(name, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Globalization.CultureInfo" /> class based on the culture specified by name and on the Boolean that specifies whether to use the user-selected culture settings from the system.</summary>
		/// <param name="name">A predefined <see cref="T:System.Globalization.CultureInfo" /> name, <see cref="P:System.Globalization.CultureInfo.Name" /> of an existing <see cref="T:System.Globalization.CultureInfo" />, or Windows-only culture name. <paramref name="name" /> is not case-sensitive.</param>
		/// <param name="useUserOverride">A Boolean that denotes whether to use the user-selected culture settings (<see langword="true" />) or the default culture settings (<see langword="false" />).</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> is not a valid culture name. See the Notes to Callers section for more information.</exception>
		// Token: 0x06005934 RID: 22836 RVA: 0x0012FF9A File Offset: 0x0012E19A
		public CultureInfo(string name, bool useUserOverride) : this(name, useUserOverride, false)
		{
		}

		// Token: 0x06005935 RID: 22837 RVA: 0x0012FFA8 File Offset: 0x0012E1A8
		private CultureInfo(string name, bool useUserOverride, bool read_only)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.constructed = true;
			this.m_isReadOnly = read_only;
			this.m_useUserOverride = useUserOverride;
			this.m_isInherited = (base.GetType() != typeof(CultureInfo));
			if (name.Length == 0)
			{
				this.m_cultureData = CultureData.Invariant;
				this.ConstructInvariant(read_only);
				return;
			}
			if (!this.ConstructLocaleFromName(name.ToLowerInvariant()))
			{
				throw CultureInfo.CreateNotFoundException(name);
			}
			CultureInfo.Data textInfoData = this.GetTextInfoData();
			string name2 = this.m_name;
			int datetimeIndex = this.datetime_index;
			int calendarType = this.CalendarType;
			int numberIndex = this.number_index;
			string text = this.iso2lang;
			int ansi = textInfoData.ansi;
			int oem = textInfoData.oem;
			int mac = textInfoData.mac;
			int ebcdic = textInfoData.ebcdic;
			bool right_to_left = textInfoData.right_to_left;
			char list_sep = (char)textInfoData.list_sep;
			this.m_cultureData = CultureData.GetCultureData(name2, useUserOverride, datetimeIndex, calendarType, numberIndex, text, ansi, oem, mac, ebcdic, right_to_left, list_sep.ToString());
		}

		// Token: 0x06005936 RID: 22838 RVA: 0x00130086 File Offset: 0x0012E286
		private CultureInfo()
		{
			this.constructed = true;
		}

		// Token: 0x06005937 RID: 22839 RVA: 0x00130095 File Offset: 0x0012E295
		private static void insert_into_shared_tables(CultureInfo c)
		{
			if (CultureInfo.shared_by_number == null)
			{
				CultureInfo.shared_by_number = new Dictionary<int, CultureInfo>();
				CultureInfo.shared_by_name = new Dictionary<string, CultureInfo>();
			}
			CultureInfo.shared_by_number[c.cultureID] = c;
			CultureInfo.shared_by_name[c.m_name] = c;
		}

		/// <summary>Retrieves a cached, read-only instance of a culture by using the specified culture identifier.</summary>
		/// <param name="culture">A locale identifier (LCID).</param>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="culture" /> is less than zero.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="culture" /> specifies a culture that is not supported. See the Notes to Caller section for more information.</exception>
		// Token: 0x06005938 RID: 22840 RVA: 0x001300D4 File Offset: 0x0012E2D4
		public static CultureInfo GetCultureInfo(int culture)
		{
			if (culture < 1)
			{
				throw new ArgumentOutOfRangeException("culture", "Positive number required.");
			}
			object obj = CultureInfo.shared_table_lock;
			CultureInfo result;
			lock (obj)
			{
				CultureInfo cultureInfo;
				if (CultureInfo.shared_by_number != null && CultureInfo.shared_by_number.TryGetValue(culture, out cultureInfo))
				{
					result = cultureInfo;
				}
				else
				{
					cultureInfo = new CultureInfo(culture, false, true);
					CultureInfo.insert_into_shared_tables(cultureInfo);
					result = cultureInfo;
				}
			}
			return result;
		}

		/// <summary>Retrieves a cached, read-only instance of a culture using the specified culture name.</summary>
		/// <param name="name">The name of a culture. <paramref name="name" /> is not case-sensitive.</param>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> specifies a culture that is not supported. See the Notes to Callers section for more information.</exception>
		// Token: 0x06005939 RID: 22841 RVA: 0x00130150 File Offset: 0x0012E350
		public static CultureInfo GetCultureInfo(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = CultureInfo.shared_table_lock;
			CultureInfo result;
			lock (obj)
			{
				CultureInfo cultureInfo;
				if (CultureInfo.shared_by_name != null && CultureInfo.shared_by_name.TryGetValue(name, out cultureInfo))
				{
					result = cultureInfo;
				}
				else
				{
					cultureInfo = new CultureInfo(name, false, true);
					CultureInfo.insert_into_shared_tables(cultureInfo);
					result = cultureInfo;
				}
			}
			return result;
		}

		/// <summary>Retrieves a cached, read-only instance of a culture. Parameters specify a culture that is initialized with the <see cref="T:System.Globalization.TextInfo" /> and <see cref="T:System.Globalization.CompareInfo" /> objects specified by another culture.</summary>
		/// <param name="name">The name of a culture. <paramref name="name" /> is not case-sensitive.</param>
		/// <param name="altName">The name of a culture that supplies the <see cref="T:System.Globalization.TextInfo" /> and <see cref="T:System.Globalization.CompareInfo" /> objects used to initialize <paramref name="name" />. <paramref name="altName" /> is not case-sensitive.</param>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="altName" /> is null.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> or <paramref name="altName" /> specifies a culture that is not supported. See the Notes to Callers section for more information.</exception>
		// Token: 0x0600593A RID: 22842 RVA: 0x001301C4 File Offset: 0x0012E3C4
		[MonoTODO("Currently it ignores the altName parameter")]
		public static CultureInfo GetCultureInfo(string name, string altName)
		{
			if (name == null)
			{
				throw new ArgumentNullException("null");
			}
			if (altName == null)
			{
				throw new ArgumentNullException("null");
			}
			return CultureInfo.GetCultureInfo(name);
		}

		/// <summary>Deprecated. Retrieves a read-only <see cref="T:System.Globalization.CultureInfo" /> object having linguistic characteristics that are identified by the specified RFC 4646 language tag.</summary>
		/// <param name="name">The name of a language as specified by the RFC 4646 standard.</param>
		/// <returns>A read-only <see cref="T:System.Globalization.CultureInfo" /> object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.</exception>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> does not correspond to a supported culture.</exception>
		// Token: 0x0600593B RID: 22843 RVA: 0x001301E8 File Offset: 0x0012E3E8
		public static CultureInfo GetCultureInfoByIetfLanguageTag(string name)
		{
			if (name == "zh-Hans")
			{
				return CultureInfo.GetCultureInfo("zh-CHS");
			}
			if (!(name == "zh-Hant"))
			{
				return CultureInfo.GetCultureInfo(name);
			}
			return CultureInfo.GetCultureInfo("zh-CHT");
		}

		// Token: 0x0600593C RID: 22844 RVA: 0x00130224 File Offset: 0x0012E424
		internal static CultureInfo CreateCulture(string name, bool reference)
		{
			bool flag = name.Length == 0;
			bool useUserOverride;
			bool read_only;
			if (reference)
			{
				useUserOverride = !flag;
				read_only = false;
			}
			else
			{
				read_only = false;
				useUserOverride = !flag;
			}
			return new CultureInfo(name, useUserOverride, read_only);
		}

		/// <summary>Creates a <see cref="T:System.Globalization.CultureInfo" /> that represents the specific culture that is associated with the specified name.</summary>
		/// <param name="name">A predefined <see cref="T:System.Globalization.CultureInfo" /> name or the name of an existing <see cref="T:System.Globalization.CultureInfo" /> object. <paramref name="name" /> is not case-sensitive.</param>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> object that represents:  
		///  The invariant culture, if <paramref name="name" /> is an empty string ("").  
		///  -or-  
		///  The specific culture associated with <paramref name="name" />, if <paramref name="name" /> is a neutral culture.  
		///  -or-  
		///  The culture specified by <paramref name="name" />, if <paramref name="name" /> is already a specific culture.</returns>
		/// <exception cref="T:System.Globalization.CultureNotFoundException">
		///   <paramref name="name" /> is not a valid culture name.  
		/// -or-  
		/// The culture specified by <paramref name="name" /> does not have a specific culture associated with it.</exception>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="name" /> is null.</exception>
		// Token: 0x0600593D RID: 22845 RVA: 0x0013025C File Offset: 0x0012E45C
		public static CultureInfo CreateSpecificCulture(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				return CultureInfo.InvariantCulture;
			}
			string name2 = name;
			name = name.ToLowerInvariant();
			CultureInfo cultureInfo = new CultureInfo();
			if (!cultureInfo.ConstructLocaleFromName(name))
			{
				throw CultureInfo.CreateNotFoundException(name2);
			}
			if (cultureInfo.IsNeutralCulture)
			{
				cultureInfo = CultureInfo.CreateSpecificCultureFromNeutral(cultureInfo.Name);
			}
			CultureInfo.Data textInfoData = cultureInfo.GetTextInfoData();
			CultureInfo cultureInfo2 = cultureInfo;
			string name3 = cultureInfo.m_name;
			bool useUserOverride = false;
			int datetimeIndex = cultureInfo.datetime_index;
			int calendarType = cultureInfo.CalendarType;
			int numberIndex = cultureInfo.number_index;
			string text = cultureInfo.iso2lang;
			int ansi = textInfoData.ansi;
			int oem = textInfoData.oem;
			int mac = textInfoData.mac;
			int ebcdic = textInfoData.ebcdic;
			bool right_to_left = textInfoData.right_to_left;
			char list_sep = (char)textInfoData.list_sep;
			cultureInfo2.m_cultureData = CultureData.GetCultureData(name3, useUserOverride, datetimeIndex, calendarType, numberIndex, text, ansi, oem, mac, ebcdic, right_to_left, list_sep.ToString());
			return cultureInfo;
		}

		// Token: 0x0600593E RID: 22846 RVA: 0x00130318 File Offset: 0x0012E518
		private bool ConstructLocaleFromName(string name)
		{
			if (this.construct_internal_locale_from_name(name))
			{
				return true;
			}
			int num = name.Length - 1;
			if (num > 0)
			{
				while ((num = name.LastIndexOf('-', num - 1)) > 0)
				{
					if (this.construct_internal_locale_from_name(name.Substring(0, num)))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x00130364 File Offset: 0x0012E564
		private static CultureInfo CreateSpecificCultureFromNeutral(string name)
		{
			string text = name.ToLowerInvariant();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			int culture;
			if (num <= 1344898993U)
			{
				if (num <= 1128614327U)
				{
					if (num <= 1025408520U)
					{
						if (num <= 975938470U)
						{
							if (num <= 926444256U)
							{
								if (num <= 896475900U)
								{
									if (num != 275533995U)
									{
										if (num == 896475900U)
										{
											if (text == "arn")
											{
												culture = 1146;
												goto IL_1B49;
											}
										}
									}
									else if (text == "nso")
									{
										culture = 1132;
										goto IL_1B49;
									}
								}
								else if (num != 925484199U)
								{
									if (num == 926444256U)
									{
										if (text == "id")
										{
											culture = 1057;
											goto IL_1B49;
										}
									}
								}
								else if (text == "mn-cyrl")
								{
									culture = 1104;
									goto IL_1B49;
								}
							}
							else if (num <= 944060518U)
							{
								if (num != 942383232U)
								{
									if (num == 944060518U)
									{
										if (text == "ta")
										{
											culture = 1097;
											goto IL_1B49;
										}
									}
								}
								else if (text == "be")
								{
									culture = 1059;
									goto IL_1B49;
								}
							}
							else if (num != 944899161U)
							{
								if (num == 975938470U)
								{
									if (text == "bg")
									{
										culture = 1026;
										goto IL_1B49;
									}
								}
							}
							else if (text == "sa")
							{
								culture = 1103;
								goto IL_1B49;
							}
						}
						else if (num <= 996684602U)
						{
							if (num <= 977615756U)
							{
								if (num != 976777113U)
								{
									if (num == 977615756U)
									{
										if (text == "tg")
										{
											culture = 1064;
											goto IL_1B49;
										}
									}
								}
								else if (text == "ig")
								{
									culture = 1136;
									goto IL_1B49;
								}
							}
							else if (num != 991980614U)
							{
								if (num == 996684602U)
								{
									if (text == "mn-mong")
									{
										culture = 2128;
										goto IL_1B49;
									}
								}
							}
							else if (text == "gd")
							{
								culture = 1169;
								goto IL_1B49;
							}
						}
						else if (num <= 1011170994U)
						{
							if (num != 1009493708U)
							{
								if (num == 1011170994U)
								{
									if (text == "te")
									{
										culture = 1098;
										goto IL_1B49;
									}
								}
							}
							else if (text == "ba")
							{
								culture = 1133;
								goto IL_1B49;
							}
						}
						else if (num != 1011465184U)
						{
							if (num != 1012009637U)
							{
								if (num == 1025408520U)
								{
									if (text == "tzm-latn")
									{
										culture = 2143;
										goto IL_1B49;
									}
								}
							}
							else if (text == "se")
							{
								culture = 1083;
								goto IL_1B49;
							}
						}
						else if (text == "vi")
						{
							culture = 1066;
							goto IL_1B49;
						}
					}
					else if (num <= 1092248970U)
					{
						if (num <= 1058693732U)
						{
							if (num <= 1044726232U)
							{
								if (num != 1044181779U)
								{
									if (num == 1044726232U)
									{
										if (text == "tk")
										{
											culture = 1090;
											goto IL_1B49;
										}
									}
								}
								else if (text == "kk")
								{
									culture = 1087;
									goto IL_1B49;
								}
							}
							else if (num != 1045564875U)
							{
								if (num == 1058693732U)
								{
									if (text == "el")
									{
										culture = 1032;
										goto IL_1B49;
									}
								}
							}
							else if (text == "sk")
							{
								culture = 1051;
								goto IL_1B49;
							}
						}
						else if (num <= 1076162899U)
						{
							if (num != 1075868709U)
							{
								if (num == 1076162899U)
								{
									if (text == "am")
									{
										culture = 1118;
										goto IL_1B49;
									}
								}
							}
							else if (text == "ga")
							{
								culture = 2108;
								goto IL_1B49;
							}
						}
						else if (num != 1079120113U)
						{
							if (num != 1087741671U)
							{
								if (num == 1092248970U)
								{
									if (text == "en")
									{
										culture = 1033;
										goto IL_1B49;
									}
								}
							}
							else if (text == "az-cyrl")
							{
								culture = 2092;
								goto IL_1B49;
							}
						}
						else if (text == "si")
						{
							culture = 1115;
							goto IL_1B49;
						}
					}
					else if (num <= 1110556780U)
					{
						if (num <= 1095059089U)
						{
							if (num != 1094514636U)
							{
								if (num == 1095059089U)
								{
									if (text == "th")
									{
										culture = 1054;
										goto IL_1B49;
									}
								}
							}
							else if (text == "kn")
							{
								culture = 1099;
								goto IL_1B49;
							}
						}
						else if (num != 1110159422U)
						{
							if (num == 1110556780U)
							{
								if (text == "lo")
								{
									culture = 1108;
									goto IL_1B49;
								}
							}
						}
						else if (text == "bo")
						{
							culture = 1105;
							goto IL_1B49;
						}
					}
					else if (num <= 1126201566U)
					{
						if (num != 1111292255U)
						{
							if (num == 1126201566U)
							{
								if (text == "gl")
								{
									culture = 1110;
									goto IL_1B49;
								}
							}
						}
						else if (text == "ko")
						{
							culture = 1042;
							goto IL_1B49;
						}
					}
					else if (num != 1126937041U)
					{
						if (num != 1128069874U)
						{
							if (num == 1128614327U)
							{
								if (text == "tn")
								{
									culture = 1074;
									goto IL_1B49;
								}
							}
						}
						else if (text == "kl")
						{
							culture = 1135;
							goto IL_1B49;
						}
					}
					else if (text == "bn")
					{
						culture = 1093;
						goto IL_1B49;
					}
				}
				else if (num <= 1213341065U)
				{
					if (num <= 1177122803U)
					{
						if (num <= 1162022470U)
						{
							if (num <= 1144553303U)
							{
								if (num != 1129452970U)
								{
									if (num == 1144553303U)
									{
										if (text == "ii")
										{
											culture = 1144;
											goto IL_1B49;
										}
									}
								}
								else if (text == "sl")
								{
									culture = 1060;
									goto IL_1B49;
								}
							}
							else if (num != 1144847493U)
							{
								if (num == 1162022470U)
								{
									if (text == "ur")
									{
										culture = 1056;
										goto IL_1B49;
									}
								}
							}
							else if (text == "km")
							{
								culture = 1107;
								goto IL_1B49;
							}
						}
						else if (num <= 1163008208U)
						{
							if (num != 1162757945U)
							{
								if (num == 1163008208U)
								{
									if (text == "sr")
									{
										culture = 9242;
										goto IL_1B49;
									}
								}
							}
							else if (text == "pl")
							{
								culture = 1045;
								goto IL_1B49;
							}
						}
						else if (num != 1164435231U)
						{
							if (num != 1176137065U)
							{
								if (num == 1177122803U)
								{
									if (text == "cs")
									{
										culture = 1029;
										goto IL_1B49;
									}
								}
							}
							else if (text == "es")
							{
								culture = 3082;
								goto IL_1B49;
							}
						}
						else if (text == "zh")
						{
							culture = 2052;
							goto IL_1B49;
						}
					}
					else if (num <= 1195724803U)
					{
						if (num <= 1194444875U)
						{
							if (num != 1192914684U)
							{
								if (num == 1194444875U)
								{
									if (text == "lb")
									{
										culture = 1134;
										goto IL_1B49;
									}
								}
							}
							else if (text == "et")
							{
								culture = 1061;
								goto IL_1B49;
							}
						}
						else if (num != 1194886160U)
						{
							if (num == 1195724803U)
							{
								if (text == "tr")
								{
									culture = 1055;
									goto IL_1B49;
								}
							}
						}
						else if (text == "it")
						{
							culture = 1040;
							goto IL_1B49;
						}
					}
					else if (num <= 1211324057U)
					{
						if (num != 1209692303U)
						{
							if (num == 1211324057U)
							{
								if (text == "iu-cans")
								{
									culture = 1117;
									goto IL_1B49;
								}
							}
						}
						else if (text == "eu")
						{
							culture = 1069;
							goto IL_1B49;
						}
					}
					else if (num != 1211663779U)
					{
						if (num != 1211957969U)
						{
							if (num == 1213341065U)
							{
								if (text == "sq")
								{
									culture = 1052;
									goto IL_1B49;
								}
							}
						}
						else if (text == "ka")
						{
							culture = 1079;
							goto IL_1B49;
						}
					}
					else if (text == "iu")
					{
						culture = 2141;
						goto IL_1B49;
					}
				}
				else if (num <= 1277200137U)
				{
					if (num <= 1231251517U)
					{
						if (num <= 1227161470U)
						{
							if (num != 1213488160U)
							{
								if (num == 1227161470U)
								{
									if (text == "af")
									{
										culture = 1078;
										goto IL_1B49;
									}
								}
							}
							else if (text == "ru")
							{
								culture = 1049;
								goto IL_1B49;
							}
						}
						else if (num != 1230118684U)
						{
							if (num == 1231251517U)
							{
								if (text == "xh")
								{
									culture = 1076;
									goto IL_1B49;
								}
							}
						}
						else if (text == "sv")
						{
							culture = 1053;
							goto IL_1B49;
						}
					}
					else if (num <= 1246896303U)
					{
						if (num != 1237973804U)
						{
							if (num == 1246896303U)
							{
								if (text == "sw")
								{
									culture = 1089;
									goto IL_1B49;
								}
							}
						}
						else if (text == "uz-latn")
						{
							culture = 1091;
							goto IL_1B49;
						}
					}
					else if (num != 1247043398U)
					{
						if (num != 1260172255U)
						{
							if (num == 1277200137U)
							{
								if (text == "gu")
								{
									culture = 1095;
									goto IL_1B49;
								}
							}
						}
						else if (text == "dv")
						{
							culture = 1125;
							goto IL_1B49;
						}
					}
					else if (text == "rw")
					{
						culture = 1159;
						goto IL_1B49;
					}
				}
				else if (num <= 1296390517U)
				{
					if (num <= 1278921350U)
					{
						if (num != 1277347232U)
						{
							if (num == 1278921350U)
							{
								if (text == "hu")
								{
									culture = 1038;
									goto IL_1B49;
								}
							}
						}
						else if (text == "fy")
						{
							culture = 1122;
							goto IL_1B49;
						}
					}
					else if (num != 1296243422U)
					{
						if (num == 1296390517U)
						{
							if (text == "tt")
							{
								culture = 1092;
								goto IL_1B49;
							}
						}
					}
					else if (text == "uz")
					{
						culture = 1091;
						goto IL_1B49;
					}
				}
				else if (num <= 1312329493U)
				{
					if (num != 1311490850U)
					{
						if (num == 1312329493U)
						{
							if (text == "is")
							{
								culture = 1039;
								goto IL_1B49;
							}
						}
					}
					else if (text == "bs")
					{
						culture = 5146;
						goto IL_1B49;
					}
				}
				else if (num != 1328268469U)
				{
					if (num != 1329254207U)
					{
						if (num == 1344898993U)
						{
							if (text == "cy")
							{
								culture = 1106;
								goto IL_1B49;
							}
						}
					}
					else if (text == "hr")
					{
						culture = 1050;
						goto IL_1B49;
					}
				}
				else if (text == "br")
				{
					culture = 1150;
					goto IL_1B49;
				}
			}
			else if (num <= 1646454850U)
			{
				if (num <= 1545391778U)
				{
					if (num <= 1462636516U)
					{
						if (num <= 1428492898U)
						{
							if (num <= 1347311754U)
							{
								if (num != 1346178921U)
								{
									if (num == 1347311754U)
									{
										if (text == "pa")
										{
											culture = 1094;
											goto IL_1B49;
										}
									}
								}
								else if (text == "ky")
								{
									culture = 1088;
									goto IL_1B49;
								}
							}
							else if (num != 1424802581U)
							{
								if (num == 1428492898U)
								{
									if (text == "az")
									{
										culture = 1068;
										goto IL_1B49;
									}
								}
							}
							else if (text == "tg-cyrl")
							{
								culture = 1064;
								goto IL_1B49;
							}
						}
						else if (num <= 1429850248U)
						{
							if (num != 1429081278U)
							{
								if (num == 1429850248U)
								{
									if (text == "gsw")
									{
										culture = 1156;
										goto IL_1B49;
									}
								}
							}
							else if (text == "mr")
							{
								culture = 1102;
								goto IL_1B49;
							}
						}
						else if (num != 1445858897U)
						{
							if (num != 1461901041U)
							{
								if (num == 1462636516U)
								{
									if (text == "mt")
									{
										culture = 1082;
										goto IL_1B49;
									}
								}
							}
							else if (text == "fr")
							{
								culture = 1036;
								goto IL_1B49;
							}
						}
						else if (text == "ms")
						{
							culture = 1086;
							goto IL_1B49;
						}
					}
					else if (num <= 1479958588U)
					{
						if (num <= 1478281302U)
						{
							if (num != 1463180969U)
							{
								if (num == 1478281302U)
								{
									if (text == "da")
									{
										culture = 1030;
										goto IL_1B49;
									}
								}
							}
							else if (text == "nb")
							{
								culture = 1044;
								goto IL_1B49;
							}
						}
						else if (num != 1479119945U)
						{
							if (num == 1479958588U)
							{
								if (text == "ne")
								{
									culture = 1121;
									goto IL_1B49;
								}
							}
						}
						else if (text == "ca")
						{
							culture = 1027;
							goto IL_1B49;
						}
					}
					else if (num <= 1483209992U)
					{
						if (num != 1480252778U)
						{
							if (num == 1483209992U)
							{
								if (text == "zu")
								{
									culture = 1077;
									goto IL_1B49;
								}
							}
						}
						else if (text == "hy")
						{
							culture = 1067;
							goto IL_1B49;
						}
					}
					else if (num != 1514352469U)
					{
						if (num != 1529997255U)
						{
							if (num == 1545391778U)
							{
								if (text == "de")
								{
									culture = 1031;
									goto IL_1B49;
								}
							}
						}
						else if (text == "lv")
						{
							culture = 1062;
							goto IL_1B49;
						}
					}
					else if (text == "ug")
					{
						culture = 1152;
						goto IL_1B49;
					}
				}
				else if (num <= 1579491469U)
				{
					if (num <= 1551553596U)
					{
						if (num <= 1546524611U)
						{
							if (num != 1545789136U)
							{
								if (num == 1546524611U)
								{
									if (text == "mi")
									{
										culture = 1153;
										goto IL_1B49;
									}
								}
							}
							else if (text == "fi")
							{
								culture = 1035;
								goto IL_1B49;
							}
						}
						else if (num != 1547363254U)
						{
							if (num == 1551553596U)
							{
								if (text == "prs")
								{
									culture = 1164;
									goto IL_1B49;
								}
							}
						}
						else if (text == "he")
						{
							culture = 1037;
							goto IL_1B49;
						}
					}
					else if (num <= 1563552493U)
					{
						if (num != 1562713850U)
						{
							if (num == 1563552493U)
							{
								if (text == "lt")
								{
									culture = 1063;
									goto IL_1B49;
								}
							}
						}
						else if (text == "ar")
						{
							culture = 1025;
							goto IL_1B49;
						}
					}
					else if (num != 1563699588U)
					{
						if (num != 1565420801U)
						{
							if (num == 1579491469U)
							{
								if (text == "as")
								{
									culture = 1101;
									goto IL_1B49;
								}
							}
						}
						else if (text == "pt")
						{
							culture = 1046;
							goto IL_1B49;
						}
					}
					else if (text == "or")
					{
						culture = 1096;
						goto IL_1B49;
					}
				}
				else if (num <= 1596857468U)
				{
					if (num <= 1581462945U)
					{
						if (num != 1580079849U)
						{
							if (num == 1581462945U)
							{
								if (text == "uk")
								{
									culture = 1058;
									goto IL_1B49;
								}
							}
						}
						else if (text == "mk")
						{
							culture = 1071;
							goto IL_1B49;
						}
					}
					else if (num != 1582198420U)
					{
						if (num == 1596857468U)
						{
							if (text == "ml")
							{
								culture = 1100;
								goto IL_1B49;
							}
						}
					}
					else if (text == "ps")
					{
						culture = 1123;
						goto IL_1B49;
					}
				}
				else if (num <= 1616151016U)
				{
					if (num != 1614473730U)
					{
						if (num == 1616151016U)
						{
							if (text == "rm")
							{
								culture = 1047;
								goto IL_1B49;
							}
						}
					}
					else if (text == "ha")
					{
						culture = 1128;
						goto IL_1B49;
					}
				}
				else if (num != 1630412706U)
				{
					if (num != 1630957159U)
					{
						if (num == 1646454850U)
						{
							if (text == "fo")
							{
								culture = 1080;
								goto IL_1B49;
							}
						}
					}
					else if (text == "nl")
					{
						culture = 1043;
						goto IL_1B49;
					}
				}
				else if (text == "mn")
				{
					culture = 1104;
					goto IL_1B49;
				}
			}
			else if (num <= 3012500870U)
			{
				if (num <= 1748694682U)
				{
					if (num <= 1649706254U)
					{
						if (num <= 1647734778U)
						{
							if (num != 1646896135U)
							{
								if (num == 1647734778U)
								{
									if (text == "no")
									{
										culture = 1044;
										goto IL_1B49;
									}
								}
							}
							else if (text == "co")
							{
								culture = 1155;
								goto IL_1B49;
							}
						}
						else if (num != 1648867611U)
						{
							if (num == 1649706254U)
							{
								if (text == "ro")
								{
									culture = 1048;
									goto IL_1B49;
								}
							}
						}
						else if (text == "wo")
						{
							culture = 1160;
							goto IL_1B49;
						}
					}
					else if (num <= 1664512397U)
					{
						if (num != 1650441729U)
						{
							if (num == 1664512397U)
							{
								if (text == "nn")
								{
									culture = 2068;
									goto IL_1B49;
								}
							}
						}
						else if (text == "yo")
						{
							culture = 1130;
							goto IL_1B49;
						}
					}
					else if (num != 1680010088U)
					{
						if (num != 1680473867U)
						{
							if (num == 1748694682U)
							{
								if (text == "hi")
								{
									culture = 1081;
									goto IL_1B49;
								}
							}
						}
						else if (text == "iu-latn")
						{
							culture = 2141;
							goto IL_1B49;
						}
					}
					else if (text == "fa")
					{
						culture = 1065;
						goto IL_1B49;
					}
				}
				else if (num <= 2046577884U)
				{
					if (num <= 1816099348U)
					{
						if (num != 1790977000U)
						{
							if (num == 1816099348U)
							{
								if (text == "ja")
								{
									culture = 1041;
									goto IL_1B49;
								}
							}
						}
						else if (text == "bs-latn")
						{
							culture = 5146;
							goto IL_1B49;
						}
					}
					else if (num != 1848919111U)
					{
						if (num == 2046577884U)
						{
							if (text == "kok")
							{
								culture = 1111;
								goto IL_1B49;
							}
						}
					}
					else if (text == "oc")
					{
						culture = 1154;
						goto IL_1B49;
					}
				}
				else
				{
					if (num <= 2197937899U)
					{
						if (num != 2180460995U)
						{
							if (num != 2197937899U)
							{
								goto IL_1B38;
							}
							if (!(text == "zh-hant"))
							{
								goto IL_1B38;
							}
						}
						else if (!(text == "zh-cht"))
						{
							goto IL_1B38;
						}
						culture = 3076;
						goto IL_1B49;
					}
					if (num != 2264349090U)
					{
						if (num != 2281825994U)
						{
							if (num != 3012500870U)
							{
								goto IL_1B38;
							}
							if (!(text == "sr-latn"))
							{
								goto IL_1B38;
							}
							culture = 9242;
							goto IL_1B49;
						}
						else if (!(text == "zh-hans"))
						{
							goto IL_1B38;
						}
					}
					else if (!(text == "zh-chs"))
					{
						goto IL_1B38;
					}
					culture = 2052;
					goto IL_1B49;
				}
			}
			else if (num <= 3795602801U)
			{
				if (num <= 3294142633U)
				{
					if (num <= 3224459074U)
					{
						if (num != 3174420263U)
						{
							if (num == 3224459074U)
							{
								if (text == "tzm")
								{
									culture = 2143;
									goto IL_1B49;
								}
							}
						}
						else if (text == "bs-cyrl")
						{
							culture = 8218;
							goto IL_1B49;
						}
					}
					else if (num != 3240320582U)
					{
						if (num == 3294142633U)
						{
							if (text == "syr")
							{
								culture = 1114;
								goto IL_1B49;
							}
						}
					}
					else if (text == "dsb")
					{
						culture = 2094;
						goto IL_1B49;
					}
				}
				else if (num <= 3659307299U)
				{
					if (num != 3336872436U)
					{
						if (num == 3659307299U)
						{
							if (text == "sah")
							{
								culture = 1157;
								goto IL_1B49;
							}
						}
					}
					else if (text == "fil")
					{
						culture = 1124;
						goto IL_1B49;
					}
				}
				else if (num != 3678056394U)
				{
					if (num != 3761944489U)
					{
						if (num == 3795602801U)
						{
							if (text == "sr-cyrl")
							{
								culture = 10266;
								goto IL_1B49;
							}
						}
					}
					else if (text == "smn")
					{
						culture = 9275;
						goto IL_1B49;
					}
				}
				else if (text == "sms")
				{
					culture = 8251;
					goto IL_1B49;
				}
			}
			else if (num <= 3953034599U)
			{
				if (num <= 3912943060U)
				{
					if (num != 3829054965U)
					{
						if (num == 3912943060U)
						{
							if (text == "sma")
							{
								culture = 7227;
								goto IL_1B49;
							}
						}
					}
					else if (text == "smj")
					{
						culture = 5179;
						goto IL_1B49;
					}
				}
				else if (num != 3918412059U)
				{
					if (num == 3953034599U)
					{
						if (text == "moh")
						{
							culture = 1148;
							goto IL_1B49;
						}
					}
				}
				else if (text == "uz-cyrl")
				{
					culture = 2115;
					goto IL_1B49;
				}
			}
			else if (num <= 4041297251U)
			{
				if (num != 3999162536U)
				{
					if (num == 4041297251U)
					{
						if (text == "quz")
						{
							culture = 1131;
							goto IL_1B49;
						}
					}
				}
				else if (text == "az-latn")
				{
					culture = 1068;
					goto IL_1B49;
				}
			}
			else if (num != 4103207754U)
			{
				if (num != 4276183917U)
				{
					if (num == 4280271688U)
					{
						if (text == "ha-latn")
						{
							culture = 1128;
							goto IL_1B49;
						}
					}
				}
				else if (text == "qut")
				{
					culture = 1158;
					goto IL_1B49;
				}
			}
			else if (text == "hsb")
			{
				culture = 1070;
				goto IL_1B49;
			}
			IL_1B38:
			throw new NotImplementedException("Mapping for neutral culture " + name);
			IL_1B49:
			return new CultureInfo(culture);
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06005940 RID: 22848 RVA: 0x00131EC0 File Offset: 0x001300C0
		internal int CalendarType
		{
			get
			{
				switch (this.default_calendar_type >> 8)
				{
				case 1:
					return 1;
				case 2:
					return 7;
				case 3:
					return 23;
				case 4:
					return 6;
				default:
					throw new NotImplementedException("CalendarType");
				}
			}
		}

		// Token: 0x06005941 RID: 22849 RVA: 0x00131F04 File Offset: 0x00130104
		private static Calendar CreateCalendar(int calendarType)
		{
			string typeName;
			switch (calendarType >> 8)
			{
			case 1:
				return new GregorianCalendar((GregorianCalendarTypes)(calendarType & 255));
			case 2:
				typeName = "System.Globalization.ThaiBuddhistCalendar";
				break;
			case 3:
				typeName = "System.Globalization.UmAlQuraCalendar";
				break;
			case 4:
				typeName = "System.Globalization.HijriCalendar";
				break;
			default:
				throw new NotImplementedException("Unknown calendar type: " + calendarType.ToString());
			}
			Type type = Type.GetType(typeName, false);
			if (type == null)
			{
				return new GregorianCalendar(GregorianCalendarTypes.Localized);
			}
			return (Calendar)Activator.CreateInstance(type);
		}

		// Token: 0x06005942 RID: 22850 RVA: 0x00131F90 File Offset: 0x00130190
		private static Exception CreateNotFoundException(string name)
		{
			return new CultureNotFoundException("name", "Culture name " + name + " is not supported.");
		}

		/// <summary>Gets or sets the default culture for threads in the current application domain.</summary>
		/// <returns>The default culture for threads in the current application domain, or <see langword="null" /> if the current system culture is the default thread culture in the application domain.</returns>
		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06005943 RID: 22851 RVA: 0x00131FAC File Offset: 0x001301AC
		// (set) Token: 0x06005944 RID: 22852 RVA: 0x00131FB5 File Offset: 0x001301B5
		public static CultureInfo DefaultThreadCurrentCulture
		{
			get
			{
				return CultureInfo.s_DefaultThreadCurrentCulture;
			}
			set
			{
				CultureInfo.s_DefaultThreadCurrentCulture = value;
			}
		}

		/// <summary>Gets or sets the default UI culture for threads in the current application domain.</summary>
		/// <returns>The default UI culture for threads in the current application domain, or <see langword="null" /> if the current system UI culture is the default thread UI culture in the application domain.</returns>
		/// <exception cref="T:System.ArgumentException">In a set operation, the <see cref="P:System.Globalization.CultureInfo.Name" /> property value is invalid.</exception>
		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06005945 RID: 22853 RVA: 0x00131FBF File Offset: 0x001301BF
		// (set) Token: 0x06005946 RID: 22854 RVA: 0x00131FC8 File Offset: 0x001301C8
		public static CultureInfo DefaultThreadCurrentUICulture
		{
			get
			{
				return CultureInfo.s_DefaultThreadCurrentUICulture;
			}
			set
			{
				CultureInfo.s_DefaultThreadCurrentUICulture = value;
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06005947 RID: 22855 RVA: 0x0012EEA3 File Offset: 0x0012D0A3
		internal string SortName
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06005948 RID: 22856 RVA: 0x00131FD2 File Offset: 0x001301D2
		internal static CultureInfo UserDefaultUICulture
		{
			get
			{
				return CultureInfo.ConstructCurrentUICulture();
			}
		}

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06005949 RID: 22857 RVA: 0x0012EE94 File Offset: 0x0012D094
		internal static CultureInfo UserDefaultCulture
		{
			get
			{
				return CultureInfo.ConstructCurrentCulture();
			}
		}

		// Token: 0x0600594A RID: 22858
		[DllImport("__Internal")]
		private static extern void InitializeUserPreferredCultureInfoInAppX(CultureInfo.OnCultureInfoChangedDelegate onCultureInfoChangedInAppX);

		// Token: 0x0600594B RID: 22859
		[DllImport("__Internal")]
		private static extern void SetUserPreferredCultureInfoInAppX([MarshalAs(UnmanagedType.LPWStr)] string name);

		// Token: 0x0600594C RID: 22860 RVA: 0x00131FD9 File Offset: 0x001301D9
		[MonoPInvokeCallback(typeof(CultureInfo.OnCultureInfoChangedDelegate))]
		private static void OnCultureInfoChangedInAppX([MarshalAs(UnmanagedType.LPWStr)] string language)
		{
			if (language != null)
			{
				CultureInfo.s_UserPreferredCultureInfoInAppX = new CultureInfo(language);
				return;
			}
			CultureInfo.s_UserPreferredCultureInfoInAppX = null;
		}

		// Token: 0x0600594D RID: 22861 RVA: 0x00131FF0 File Offset: 0x001301F0
		internal static CultureInfo GetCultureInfoForUserPreferredLanguageInAppX()
		{
			if (CultureInfo.s_UserPreferredCultureInfoInAppX == null)
			{
				CultureInfo.InitializeUserPreferredCultureInfoInAppX(new CultureInfo.OnCultureInfoChangedDelegate(CultureInfo.OnCultureInfoChangedInAppX));
			}
			return CultureInfo.s_UserPreferredCultureInfoInAppX;
		}

		// Token: 0x0600594E RID: 22862 RVA: 0x0013200F File Offset: 0x0013020F
		internal static void SetCultureInfoForUserPreferredLanguageInAppX(CultureInfo cultureInfo)
		{
			if (CultureInfo.s_UserPreferredCultureInfoInAppX == null)
			{
				CultureInfo.InitializeUserPreferredCultureInfoInAppX(new CultureInfo.OnCultureInfoChangedDelegate(CultureInfo.OnCultureInfoChangedInAppX));
			}
			CultureInfo.SetUserPreferredCultureInfoInAppX(cultureInfo.Name);
			CultureInfo.s_UserPreferredCultureInfoInAppX = cultureInfo;
		}

		// Token: 0x0600594F RID: 22863 RVA: 0x0013203C File Offset: 0x0013023C
		internal static void CheckDomainSafetyObject(object obj, object container)
		{
			if (obj.GetType().Assembly != typeof(CultureInfo).Assembly)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cannot set sub-classed {0} object to {1} object."), obj.GetType(), container.GetType()));
			}
		}

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06005950 RID: 22864 RVA: 0x00132090 File Offset: 0x00130290
		internal bool HasInvariantCultureName
		{
			get
			{
				return this.Name == CultureInfo.InvariantCulture.Name;
			}
		}

		// Token: 0x06005951 RID: 22865 RVA: 0x001320A8 File Offset: 0x001302A8
		internal static bool VerifyCultureName(string cultureName, bool throwException)
		{
			int i = 0;
			while (i < cultureName.Length)
			{
				char c = cultureName[i];
				if (!char.IsLetterOrDigit(c) && c != '-' && c != '_')
				{
					if (throwException)
					{
						throw new ArgumentException(Environment.GetResourceString("The given culture name '{0}' cannot be used to locate a resource file. Resource filenames must consist of only letters, numbers, hyphens or underscores.", new object[]
						{
							cultureName
						}));
					}
					return false;
				}
				else
				{
					i++;
				}
			}
			return true;
		}

		// Token: 0x06005952 RID: 22866 RVA: 0x00132100 File Offset: 0x00130300
		internal static bool VerifyCultureName(CultureInfo culture, bool throwException)
		{
			return !culture.m_isInherited || CultureInfo.VerifyCultureName(culture.Name, throwException);
		}

		// Token: 0x06005953 RID: 22867 RVA: 0x00132118 File Offset: 0x00130318
		// Note: this type is marked as 'beforefieldinit'.
		static CultureInfo()
		{
		}

		// Token: 0x04003728 RID: 14120
		private static volatile CultureInfo invariant_culture_info = new CultureInfo(127, false, true);

		// Token: 0x04003729 RID: 14121
		private static object shared_table_lock = new object();

		// Token: 0x0400372A RID: 14122
		private static CultureInfo default_current_culture;

		// Token: 0x0400372B RID: 14123
		private bool m_isReadOnly;

		// Token: 0x0400372C RID: 14124
		private int cultureID;

		// Token: 0x0400372D RID: 14125
		[NonSerialized]
		private int parent_lcid;

		// Token: 0x0400372E RID: 14126
		[NonSerialized]
		private int datetime_index;

		// Token: 0x0400372F RID: 14127
		[NonSerialized]
		private int number_index;

		// Token: 0x04003730 RID: 14128
		[NonSerialized]
		private int default_calendar_type;

		// Token: 0x04003731 RID: 14129
		private bool m_useUserOverride;

		// Token: 0x04003732 RID: 14130
		internal volatile NumberFormatInfo numInfo;

		// Token: 0x04003733 RID: 14131
		internal volatile DateTimeFormatInfo dateTimeInfo;

		// Token: 0x04003734 RID: 14132
		private volatile TextInfo textInfo;

		// Token: 0x04003735 RID: 14133
		internal string m_name;

		// Token: 0x04003736 RID: 14134
		[NonSerialized]
		private string englishname;

		// Token: 0x04003737 RID: 14135
		[NonSerialized]
		private string nativename;

		// Token: 0x04003738 RID: 14136
		[NonSerialized]
		private string iso3lang;

		// Token: 0x04003739 RID: 14137
		[NonSerialized]
		private string iso2lang;

		// Token: 0x0400373A RID: 14138
		[NonSerialized]
		private string win3lang;

		// Token: 0x0400373B RID: 14139
		[NonSerialized]
		private string territory;

		// Token: 0x0400373C RID: 14140
		[NonSerialized]
		private string[] native_calendar_names;

		// Token: 0x0400373D RID: 14141
		private volatile CompareInfo compareInfo;

		// Token: 0x0400373E RID: 14142
		[NonSerialized]
		private unsafe readonly void* textinfo_data;

		// Token: 0x0400373F RID: 14143
		private int m_dataItem;

		// Token: 0x04003740 RID: 14144
		private Calendar calendar;

		// Token: 0x04003741 RID: 14145
		[NonSerialized]
		private CultureInfo parent_culture;

		// Token: 0x04003742 RID: 14146
		[NonSerialized]
		private bool constructed;

		// Token: 0x04003743 RID: 14147
		[NonSerialized]
		internal byte[] cached_serialized_form;

		// Token: 0x04003744 RID: 14148
		[NonSerialized]
		internal CultureData m_cultureData;

		// Token: 0x04003745 RID: 14149
		[NonSerialized]
		internal bool m_isInherited;

		// Token: 0x04003746 RID: 14150
		internal const int InvariantCultureId = 127;

		// Token: 0x04003747 RID: 14151
		private const int CalendarTypeBits = 8;

		// Token: 0x04003748 RID: 14152
		internal const int LOCALE_INVARIANT = 127;

		// Token: 0x04003749 RID: 14153
		private const string MSG_READONLY = "This instance is read only";

		// Token: 0x0400374A RID: 14154
		private static volatile CultureInfo s_DefaultThreadCurrentUICulture;

		// Token: 0x0400374B RID: 14155
		private static volatile CultureInfo s_DefaultThreadCurrentCulture;

		// Token: 0x0400374C RID: 14156
		private static Dictionary<int, CultureInfo> shared_by_number;

		// Token: 0x0400374D RID: 14157
		private static Dictionary<string, CultureInfo> shared_by_name;

		// Token: 0x0400374E RID: 14158
		private static CultureInfo s_UserPreferredCultureInfoInAppX;

		// Token: 0x0400374F RID: 14159
		internal static readonly bool IsTaiwanSku;

		// Token: 0x020009A8 RID: 2472
		private struct Data
		{
			// Token: 0x04003750 RID: 14160
			public int ansi;

			// Token: 0x04003751 RID: 14161
			public int ebcdic;

			// Token: 0x04003752 RID: 14162
			public int mac;

			// Token: 0x04003753 RID: 14163
			public int oem;

			// Token: 0x04003754 RID: 14164
			public bool right_to_left;

			// Token: 0x04003755 RID: 14165
			public byte list_sep;
		}

		// Token: 0x020009A9 RID: 2473
		// (Invoke) Token: 0x06005955 RID: 22869
		private delegate void OnCultureInfoChangedDelegate([MarshalAs(UnmanagedType.LPWStr)] string language);
	}
}
