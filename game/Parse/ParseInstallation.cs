using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Parse.Infrastructure.Utilities;

namespace Parse
{
	// Token: 0x0200000B RID: 11
	[ParseClassName("_Installation")]
	public class ParseInstallation : ParseObject
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002490 File Offset: 0x00000690
		private static HashSet<string> ImmutableKeys
		{
			[CompilerGenerated]
			get
			{
				return ParseInstallation.<ImmutableKeys>k__BackingField;
			}
		} = new HashSet<string>
		{
			"deviceType",
			"deviceUris",
			"installationId",
			"timeZone",
			"localeIdentifier",
			"parseVersion",
			"appName",
			"appIdentifier",
			"appVersion",
			"pushType"
		};

		// Token: 0x0600002A RID: 42 RVA: 0x00002497 File Offset: 0x00000697
		public ParseInstallation() : base(null)
		{
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000024A0 File Offset: 0x000006A0
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000024EC File Offset: 0x000006EC
		[ParseFieldName("installationId")]
		public Guid InstallationId
		{
			get
			{
				string property = base.GetProperty<string>("InstallationId");
				Guid? guid = null;
				try
				{
					guid = new Guid?(new Guid(property));
				}
				catch (Exception)
				{
				}
				return guid.Value;
			}
			internal set
			{
				Guid guid = value;
				base.SetProperty<string>(guid.ToString(), "InstallationId");
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002513 File Offset: 0x00000713
		// (set) Token: 0x0600002E RID: 46 RVA: 0x00002520 File Offset: 0x00000720
		[ParseFieldName("deviceType")]
		public string DeviceType
		{
			get
			{
				return base.GetProperty<string>("DeviceType");
			}
			internal set
			{
				base.SetProperty<string>(value, "DeviceType");
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000252E File Offset: 0x0000072E
		// (set) Token: 0x06000030 RID: 48 RVA: 0x0000253B File Offset: 0x0000073B
		[ParseFieldName("appName")]
		public string AppName
		{
			get
			{
				return base.GetProperty<string>("AppName");
			}
			internal set
			{
				base.SetProperty<string>(value, "AppName");
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002549 File Offset: 0x00000749
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002556 File Offset: 0x00000756
		[ParseFieldName("appVersion")]
		public string AppVersion
		{
			get
			{
				return base.GetProperty<string>("AppVersion");
			}
			internal set
			{
				base.SetProperty<string>(value, "AppVersion");
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002564 File Offset: 0x00000764
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002571 File Offset: 0x00000771
		[ParseFieldName("appIdentifier")]
		public string AppIdentifier
		{
			get
			{
				return base.GetProperty<string>("AppIdentifier");
			}
			internal set
			{
				base.SetProperty<string>(value, "AppIdentifier");
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000257F File Offset: 0x0000077F
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000258C File Offset: 0x0000078C
		[ParseFieldName("timeZone")]
		public string TimeZone
		{
			get
			{
				return base.GetProperty<string>("TimeZone");
			}
			private set
			{
				base.SetProperty<string>(value, "TimeZone");
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000259A File Offset: 0x0000079A
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000025A7 File Offset: 0x000007A7
		[ParseFieldName("localeIdentifier")]
		public string LocaleIdentifier
		{
			get
			{
				return base.GetProperty<string>("LocaleIdentifier");
			}
			private set
			{
				base.SetProperty<string>(value, "LocaleIdentifier");
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000025B8 File Offset: 0x000007B8
		private string GetLocaleIdentifier()
		{
			string text = null;
			string text2 = null;
			if (CultureInfo.CurrentCulture != null)
			{
				text = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
			}
			if (RegionInfo.CurrentRegion != null)
			{
				text2 = RegionInfo.CurrentRegion.TwoLetterISORegionName;
			}
			if (string.IsNullOrEmpty(text2))
			{
				return text;
			}
			return string.Format("{0}-{1}", text, text2);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002604 File Offset: 0x00000804
		// (set) Token: 0x0600003B RID: 59 RVA: 0x0000263C File Offset: 0x0000083C
		[ParseFieldName("parseVersion")]
		public Version ParseVersion
		{
			get
			{
				string property = base.GetProperty<string>("ParseVersion");
				Version result = null;
				try
				{
					result = new Version(property);
				}
				catch (Exception)
				{
				}
				return result;
			}
			private set
			{
				base.SetProperty<string>(value.ToString(), "ParseVersion");
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000265C File Offset: 0x0000085C
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002669 File Offset: 0x00000869
		[ParseFieldName("channels")]
		public IList<string> Channels
		{
			get
			{
				return base.GetProperty<IList<string>>("Channels");
			}
			set
			{
				base.SetProperty<IList<string>>(value, "Channels");
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002677 File Offset: 0x00000877
		protected override bool CheckKeyMutable(string key)
		{
			return !ParseInstallation.ImmutableKeys.Contains(key);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002688 File Offset: 0x00000888
		protected override Task SaveAsync(Task toAwait, CancellationToken cancellationToken)
		{
			Task task = null;
			if (base.Services.CurrentInstallationController.IsCurrent(this))
			{
				base.SetIfDifferent<string>("deviceType", base.Services.MetadataController.EnvironmentData.Platform);
				base.SetIfDifferent<string>("timeZone", base.Services.MetadataController.EnvironmentData.TimeZone);
				base.SetIfDifferent<string>("localeIdentifier", this.GetLocaleIdentifier());
				base.SetIfDifferent<string>("parseVersion", ParseClient.Version);
				base.SetIfDifferent<string>("appVersion", base.Services.MetadataController.HostManifestData.Version);
				base.SetIfDifferent<string>("appIdentifier", base.Services.MetadataController.HostManifestData.Identifier);
				base.SetIfDifferent<string>("appName", base.Services.MetadataController.HostManifestData.Name);
			}
			return task.Safe().OnSuccess((Task _) => this.<>n__0(toAwait, cancellationToken)).Unwrap().OnSuccess(delegate(Task _)
			{
				if (!this.Services.CurrentInstallationController.IsCurrent(this))
				{
					return this.Services.CurrentInstallationController.SetAsync(this, cancellationToken);
				}
				return Task.CompletedTask;
			}).Unwrap();
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000027B9 File Offset: 0x000009B9
		internal static Dictionary<string, string> TimeZoneNameMap
		{
			[CompilerGenerated]
			get
			{
				return ParseInstallation.<TimeZoneNameMap>k__BackingField;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000027C0 File Offset: 0x000009C0
		internal static Dictionary<TimeSpan, string> TimeZoneOffsetMap
		{
			[CompilerGenerated]
			get
			{
				return ParseInstallation.<TimeZoneOffsetMap>k__BackingField;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000027C8 File Offset: 0x000009C8
		// Note: this type is marked as 'beforefieldinit'.
		static ParseInstallation()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["Dateline Standard Time"] = "Etc/GMT+12";
			dictionary["UTC-11"] = "Etc/GMT+11";
			dictionary["Hawaiian Standard Time"] = "Pacific/Honolulu";
			dictionary["Alaskan Standard Time"] = "America/Anchorage";
			dictionary["Pacific Standard Time (Mexico)"] = "America/Santa_Isabel";
			dictionary["Pacific Standard Time"] = "America/Los_Angeles";
			dictionary["US Mountain Standard Time"] = "America/Phoenix";
			dictionary["Mountain Standard Time (Mexico)"] = "America/Chihuahua";
			dictionary["Mountain Standard Time"] = "America/Denver";
			dictionary["Central America Standard Time"] = "America/Guatemala";
			dictionary["Central Standard Time"] = "America/Chicago";
			dictionary["Central Standard Time (Mexico)"] = "America/Mexico_City";
			dictionary["Canada Central Standard Time"] = "America/Regina";
			dictionary["SA Pacific Standard Time"] = "America/Bogota";
			dictionary["Eastern Standard Time"] = "America/New_York";
			dictionary["US Eastern Standard Time"] = "America/Indianapolis";
			dictionary["Venezuela Standard Time"] = "America/Caracas";
			dictionary["Paraguay Standard Time"] = "America/Asuncion";
			dictionary["Atlantic Standard Time"] = "America/Halifax";
			dictionary["Central Brazilian Standard Time"] = "America/Cuiaba";
			dictionary["SA Western Standard Time"] = "America/La_Paz";
			dictionary["Pacific SA Standard Time"] = "America/Santiago";
			dictionary["Newfoundland Standard Time"] = "America/St_Johns";
			dictionary["E. South America Standard Time"] = "America/Sao_Paulo";
			dictionary["Argentina Standard Time"] = "America/Buenos_Aires";
			dictionary["SA Eastern Standard Time"] = "America/Cayenne";
			dictionary["Greenland Standard Time"] = "America/Godthab";
			dictionary["Montevideo Standard Time"] = "America/Montevideo";
			dictionary["Bahia Standard Time"] = "America/Bahia";
			dictionary["UTC-02"] = "Etc/GMT+2";
			dictionary["Azores Standard Time"] = "Atlantic/Azores";
			dictionary["Cape Verde Standard Time"] = "Atlantic/Cape_Verde";
			dictionary["Morocco Standard Time"] = "Africa/Casablanca";
			dictionary["UTC"] = "Etc/GMT";
			dictionary["GMT Standard Time"] = "Europe/London";
			dictionary["Greenwich Standard Time"] = "Atlantic/Reykjavik";
			dictionary["W. Europe Standard Time"] = "Europe/Berlin";
			dictionary["Central Europe Standard Time"] = "Europe/Budapest";
			dictionary["Romance Standard Time"] = "Europe/Paris";
			dictionary["Central European Standard Time"] = "Europe/Warsaw";
			dictionary["W. Central Africa Standard Time"] = "Africa/Lagos";
			dictionary["Namibia Standard Time"] = "Africa/Windhoek";
			dictionary["GTB Standard Time"] = "Europe/Bucharest";
			dictionary["Middle East Standard Time"] = "Asia/Beirut";
			dictionary["Egypt Standard Time"] = "Africa/Cairo";
			dictionary["Syria Standard Time"] = "Asia/Damascus";
			dictionary["E. Europe Standard Time"] = "Asia/Nicosia";
			dictionary["South Africa Standard Time"] = "Africa/Johannesburg";
			dictionary["FLE Standard Time"] = "Europe/Kiev";
			dictionary["Turkey Standard Time"] = "Europe/Istanbul";
			dictionary["Israel Standard Time"] = "Asia/Jerusalem";
			dictionary["Jordan Standard Time"] = "Asia/Amman";
			dictionary["Arabic Standard Time"] = "Asia/Baghdad";
			dictionary["Kaliningrad Standard Time"] = "Europe/Kaliningrad";
			dictionary["Arab Standard Time"] = "Asia/Riyadh";
			dictionary["E. Africa Standard Time"] = "Africa/Nairobi";
			dictionary["Iran Standard Time"] = "Asia/Tehran";
			dictionary["Arabian Standard Time"] = "Asia/Dubai";
			dictionary["Azerbaijan Standard Time"] = "Asia/Baku";
			dictionary["Russian Standard Time"] = "Europe/Moscow";
			dictionary["Mauritius Standard Time"] = "Indian/Mauritius";
			dictionary["Georgian Standard Time"] = "Asia/Tbilisi";
			dictionary["Caucasus Standard Time"] = "Asia/Yerevan";
			dictionary["Afghanistan Standard Time"] = "Asia/Kabul";
			dictionary["Pakistan Standard Time"] = "Asia/Karachi";
			dictionary["West Asia Standard Time"] = "Asia/Tashkent";
			dictionary["India Standard Time"] = "Asia/Calcutta";
			dictionary["Sri Lanka Standard Time"] = "Asia/Colombo";
			dictionary["Nepal Standard Time"] = "Asia/Katmandu";
			dictionary["Central Asia Standard Time"] = "Asia/Almaty";
			dictionary["Bangladesh Standard Time"] = "Asia/Dhaka";
			dictionary["Ekaterinburg Standard Time"] = "Asia/Yekaterinburg";
			dictionary["Myanmar Standard Time"] = "Asia/Rangoon";
			dictionary["SE Asia Standard Time"] = "Asia/Bangkok";
			dictionary["N. Central Asia Standard Time"] = "Asia/Novosibirsk";
			dictionary["China Standard Time"] = "Asia/Shanghai";
			dictionary["North Asia Standard Time"] = "Asia/Krasnoyarsk";
			dictionary["Singapore Standard Time"] = "Asia/Singapore";
			dictionary["W. Australia Standard Time"] = "Australia/Perth";
			dictionary["Taipei Standard Time"] = "Asia/Taipei";
			dictionary["Ulaanbaatar Standard Time"] = "Asia/Ulaanbaatar";
			dictionary["North Asia East Standard Time"] = "Asia/Irkutsk";
			dictionary["Tokyo Standard Time"] = "Asia/Tokyo";
			dictionary["Korea Standard Time"] = "Asia/Seoul";
			dictionary["Cen. Australia Standard Time"] = "Australia/Adelaide";
			dictionary["AUS Central Standard Time"] = "Australia/Darwin";
			dictionary["E. Australia Standard Time"] = "Australia/Brisbane";
			dictionary["AUS Eastern Standard Time"] = "Australia/Sydney";
			dictionary["West Pacific Standard Time"] = "Pacific/Port_Moresby";
			dictionary["Tasmania Standard Time"] = "Australia/Hobart";
			dictionary["Yakutsk Standard Time"] = "Asia/Yakutsk";
			dictionary["Central Pacific Standard Time"] = "Pacific/Guadalcanal";
			dictionary["Vladivostok Standard Time"] = "Asia/Vladivostok";
			dictionary["New Zealand Standard Time"] = "Pacific/Auckland";
			dictionary["UTC+12"] = "Etc/GMT-12";
			dictionary["Fiji Standard Time"] = "Pacific/Fiji";
			dictionary["Magadan Standard Time"] = "Asia/Magadan";
			dictionary["Tonga Standard Time"] = "Pacific/Tongatapu";
			dictionary["Samoa Standard Time"] = "Pacific/Apia";
			ParseInstallation.TimeZoneNameMap = dictionary;
			Dictionary<TimeSpan, string> dictionary2 = new Dictionary<TimeSpan, string>();
			TimeSpan key = new TimeSpan(12, 45, 0);
			dictionary2[key] = "Pacific/Chatham";
			TimeSpan key2 = new TimeSpan(10, 30, 0);
			dictionary2[key2] = "Australia/Lord_Howe";
			TimeSpan key3 = new TimeSpan(9, 30, 0);
			dictionary2[key3] = "Australia/Adelaide";
			TimeSpan key4 = new TimeSpan(8, 45, 0);
			dictionary2[key4] = "Australia/Eucla";
			TimeSpan key5 = new TimeSpan(8, 30, 0);
			dictionary2[key5] = "Asia/Pyongyang";
			TimeSpan key6 = new TimeSpan(6, 30, 0);
			dictionary2[key6] = "Asia/Rangoon";
			TimeSpan key7 = new TimeSpan(5, 45, 0);
			dictionary2[key7] = "Asia/Kathmandu";
			TimeSpan key8 = new TimeSpan(5, 30, 0);
			dictionary2[key8] = "Asia/Colombo";
			TimeSpan key9 = new TimeSpan(4, 30, 0);
			dictionary2[key9] = "Asia/Kabul";
			TimeSpan key10 = new TimeSpan(3, 30, 0);
			dictionary2[key10] = "Asia/Tehran";
			TimeSpan key11 = new TimeSpan(-3, 30, 0);
			dictionary2[key11] = "America/St_Johns";
			TimeSpan key12 = new TimeSpan(-4, 30, 0);
			dictionary2[key12] = "America/Caracas";
			TimeSpan key13 = new TimeSpan(-9, 30, 0);
			dictionary2[key13] = "Pacific/Marquesas";
			ParseInstallation.TimeZoneOffsetMap = dictionary2;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002FD5 File Offset: 0x000011D5
		[CompilerGenerated]
		[DebuggerHidden]
		private Task <>n__0(Task toAwait, CancellationToken cancellationToken)
		{
			return base.SaveAsync(toAwait, cancellationToken);
		}

		// Token: 0x0400000A RID: 10
		[CompilerGenerated]
		private static readonly HashSet<string> <ImmutableKeys>k__BackingField;

		// Token: 0x0400000B RID: 11
		[CompilerGenerated]
		private static readonly Dictionary<string, string> <TimeZoneNameMap>k__BackingField;

		// Token: 0x0400000C RID: 12
		[CompilerGenerated]
		private static readonly Dictionary<TimeSpan, string> <TimeZoneOffsetMap>k__BackingField;

		// Token: 0x020000A2 RID: 162
		[CompilerGenerated]
		private sealed class <>c__DisplayClass33_0
		{
			// Token: 0x060005C0 RID: 1472 RVA: 0x00012D25 File Offset: 0x00010F25
			public <>c__DisplayClass33_0()
			{
			}

			// Token: 0x060005C1 RID: 1473 RVA: 0x00012D2D File Offset: 0x00010F2D
			internal Task <SaveAsync>b__0(Task _)
			{
				return this.<>4__this.<>n__0(this.toAwait, this.cancellationToken);
			}

			// Token: 0x060005C2 RID: 1474 RVA: 0x00012D48 File Offset: 0x00010F48
			internal Task <SaveAsync>b__1(Task _)
			{
				if (!this.<>4__this.Services.CurrentInstallationController.IsCurrent(this.<>4__this))
				{
					return this.<>4__this.Services.CurrentInstallationController.SetAsync(this.<>4__this, this.cancellationToken);
				}
				return Task.CompletedTask;
			}

			// Token: 0x0400010D RID: 269
			public ParseInstallation <>4__this;

			// Token: 0x0400010E RID: 270
			public Task toAwait;

			// Token: 0x0400010F RID: 271
			public CancellationToken cancellationToken;
		}
	}
}
