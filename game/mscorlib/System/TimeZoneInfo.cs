using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using Unity;

namespace System
{
	/// <summary>Represents any time zone in the world.</summary>
	// Token: 0x020000D0 RID: 208
	[TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class TimeZoneInfo : IEquatable<TimeZoneInfo>, ISerializable, IDeserializationCallback
	{
		/// <summary>Retrieves an array of <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> objects that apply to the current <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <returns>An array of objects for this time zone.</returns>
		/// <exception cref="T:System.OutOfMemoryException">The system does not have enough memory to make an in-memory copy of the adjustment rules.</exception>
		// Token: 0x060005DC RID: 1500 RVA: 0x0001BE73 File Offset: 0x0001A073
		public TimeZoneInfo.AdjustmentRule[] GetAdjustmentRules()
		{
			if (this._adjustmentRules == null)
			{
				return Array.Empty<TimeZoneInfo.AdjustmentRule>();
			}
			return (TimeZoneInfo.AdjustmentRule[])this._adjustmentRules.Clone();
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x0001BE93 File Offset: 0x0001A093
		private static void PopulateAllSystemTimeZones(TimeZoneInfo.CachedData cachedData)
		{
			if (TimeZoneInfo.HaveRegistry)
			{
				TimeZoneInfo.PopulateAllSystemTimeZonesFromRegistry(cachedData);
				return;
			}
			TimeZoneInfo.GetSystemTimeZonesWinRTFallback(cachedData);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001BEAC File Offset: 0x0001A0AC
		private static void PopulateAllSystemTimeZonesFromRegistry(TimeZoneInfo.CachedData cachedData)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", false))
			{
				if (registryKey != null)
				{
					string[] subKeyNames = registryKey.GetSubKeyNames();
					for (int i = 0; i < subKeyNames.Length; i++)
					{
						TimeZoneInfo timeZoneInfo;
						Exception ex;
						TimeZoneInfo.TryGetTimeZone(subKeyNames[i], false, out timeZoneInfo, out ex, cachedData, false);
					}
				}
			}
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001BF10 File Offset: 0x0001A110
		private TimeZoneInfo(in Interop.Kernel32.TIME_ZONE_INFORMATION zone, bool dstDisabled)
		{
			Interop.Kernel32.TIME_ZONE_INFORMATION time_ZONE_INFORMATION = zone;
			string standardName = time_ZONE_INFORMATION.GetStandardName();
			if (standardName.Length == 0)
			{
				this._id = "Local";
			}
			else
			{
				this._id = standardName;
			}
			this._baseUtcOffset = new TimeSpan(0, -zone.Bias, 0);
			if (!dstDisabled)
			{
				Interop.Kernel32.REG_TZI_FORMAT reg_TZI_FORMAT = new Interop.Kernel32.REG_TZI_FORMAT(ref zone);
				TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(reg_TZI_FORMAT, DateTime.MinValue.Date, DateTime.MaxValue.Date, zone.Bias);
				if (adjustmentRule != null)
				{
					this._adjustmentRules = new TimeZoneInfo.AdjustmentRule[]
					{
						adjustmentRule
					};
				}
			}
			TimeZoneInfo.ValidateTimeZoneInfo(this._id, this._baseUtcOffset, this._adjustmentRules, out this._supportsDaylightSavingTime);
			this._displayName = standardName;
			this._standardDisplayName = standardName;
			time_ZONE_INFORMATION = zone;
			this._daylightDisplayName = time_ZONE_INFORMATION.GetDaylightName();
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x0001BFE0 File Offset: 0x0001A1E0
		private static bool CheckDaylightSavingTimeNotSupported(in Interop.Kernel32.TIME_ZONE_INFORMATION timeZone)
		{
			Interop.Kernel32.SYSTEMTIME daylightDate = timeZone.DaylightDate;
			return daylightDate.Equals(timeZone.StandardDate);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x0001C004 File Offset: 0x0001A204
		private static TimeZoneInfo.AdjustmentRule CreateAdjustmentRuleFromTimeZoneInformation(in Interop.Kernel32.REG_TZI_FORMAT timeZoneInformation, DateTime startDate, DateTime endDate, int defaultBaseUtcOffset)
		{
			if (timeZoneInformation.StandardDate.Month <= 0)
			{
				if (timeZoneInformation.Bias == defaultBaseUtcOffset)
				{
					return null;
				}
				return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, TimeSpan.Zero, TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue, 1, 1), TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue.AddMilliseconds(1.0), 1, 1), new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.Bias, 0), false);
			}
			else
			{
				TimeZoneInfo.TransitionTime daylightTransitionStart;
				if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out daylightTransitionStart, true))
				{
					return null;
				}
				TimeZoneInfo.TransitionTime transitionTime;
				if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out transitionTime, false))
				{
					return null;
				}
				if (daylightTransitionStart.Equals(transitionTime))
				{
					return null;
				}
				return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, new TimeSpan(0, -timeZoneInformation.DaylightBias, 0), daylightTransitionStart, transitionTime, new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.Bias, 0), false);
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001C0BC File Offset: 0x0001A2BC
		private static string FindIdFromTimeZoneInformation(in Interop.Kernel32.TIME_ZONE_INFORMATION timeZone, out bool dstDisabled)
		{
			dstDisabled = false;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones", false))
			{
				if (registryKey == null)
				{
					return null;
				}
				foreach (string text in registryKey.GetSubKeyNames())
				{
					if (TimeZoneInfo.TryCompareTimeZoneInformationToRegistry(timeZone, text, out dstDisabled))
					{
						return text;
					}
				}
			}
			return null;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001C130 File Offset: 0x0001A330
		private static TimeZoneInfo GetLocalTimeZone(TimeZoneInfo.CachedData cachedData)
		{
			if (!TimeZoneInfo.HaveRegistry)
			{
				return TimeZoneInfo.GetLocalTimeZoneInfoWinRTFallback();
			}
			Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION time_DYNAMIC_ZONE_INFORMATION = default(Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION);
			if (Interop.Kernel32.GetDynamicTimeZoneInformation(out time_DYNAMIC_ZONE_INFORMATION) == 4294967295U)
			{
				return TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
			}
			string timeZoneKeyName = time_DYNAMIC_ZONE_INFORMATION.GetTimeZoneKeyName();
			TimeZoneInfo result;
			Exception ex;
			if (timeZoneKeyName.Length != 0 && TimeZoneInfo.TryGetTimeZone(timeZoneKeyName, time_DYNAMIC_ZONE_INFORMATION.DynamicDaylightTimeDisabled > 0, out result, out ex, cachedData, false) == TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				return result;
			}
			Interop.Kernel32.TIME_ZONE_INFORMATION time_ZONE_INFORMATION = new Interop.Kernel32.TIME_ZONE_INFORMATION(ref time_DYNAMIC_ZONE_INFORMATION);
			bool dstDisabled;
			string text = TimeZoneInfo.FindIdFromTimeZoneInformation(time_ZONE_INFORMATION, out dstDisabled);
			TimeZoneInfo result2;
			Exception ex2;
			if (text != null && TimeZoneInfo.TryGetTimeZone(text, dstDisabled, out result2, out ex2, cachedData, false) == TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				return result2;
			}
			return TimeZoneInfo.GetLocalTimeZoneFromWin32Data(time_ZONE_INFORMATION, dstDisabled);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x0001C1D4 File Offset: 0x0001A3D4
		private static TimeZoneInfo GetLocalTimeZoneFromWin32Data(in Interop.Kernel32.TIME_ZONE_INFORMATION timeZoneInformation, bool dstDisabled)
		{
			try
			{
				return new TimeZoneInfo(ref timeZoneInformation, dstDisabled);
			}
			catch (ArgumentException)
			{
			}
			catch (InvalidTimeZoneException)
			{
			}
			if (!dstDisabled)
			{
				try
				{
					return new TimeZoneInfo(ref timeZoneInformation, true);
				}
				catch (ArgumentException)
				{
				}
				catch (InvalidTimeZoneException)
				{
				}
			}
			return TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
		}

		/// <summary>Instantiates a new <see cref="T:System.TimeZoneInfo" /> object based on its identifier.</summary>
		/// <param name="id">The time zone identifier, which corresponds to the <see cref="P:System.TimeZoneInfo.Id" /> property.</param>
		/// <returns>An object whose identifier is the value of the <paramref name="id" /> parameter.</returns>
		/// <exception cref="T:System.OutOfMemoryException">The system does not have enough memory to hold information about the time zone.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="id" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.TimeZoneNotFoundException">The time zone identifier specified by <paramref name="id" /> was not found. This means that a time zone identifier whose name matches <paramref name="id" /> does not exist, or that the identifier exists but does not contain any time zone data.</exception>
		/// <exception cref="T:System.Security.SecurityException">The process does not have the permissions required to read from the registry key that contains the time zone information.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The time zone identifier was found, but the registry data is corrupted.</exception>
		// Token: 0x060005E5 RID: 1509 RVA: 0x0001C254 File Offset: 0x0001A454
		public static TimeZoneInfo FindSystemTimeZoneById(string id)
		{
			if (string.Equals(id, "UTC", StringComparison.OrdinalIgnoreCase))
			{
				return TimeZoneInfo.Utc;
			}
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id.Length == 0 || id.Length > 255 || id.Contains('\0'))
			{
				throw new TimeZoneNotFoundException(SR.Format("The time zone ID '{0}' was not found on the local computer.", id));
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			TimeZoneInfo.CachedData obj = cachedData;
			TimeZoneInfo result;
			Exception ex;
			TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult;
			lock (obj)
			{
				timeZoneInfoResult = TimeZoneInfo.TryGetTimeZone(id, false, out result, out ex, cachedData, false);
			}
			if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				return result;
			}
			if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException)
			{
				throw new InvalidTimeZoneException(SR.Format("The time zone ID '{0}' was found on the local computer, but the registry information was corrupt.", id), ex);
			}
			if (timeZoneInfoResult == TimeZoneInfo.TimeZoneInfoResult.SecurityException)
			{
				throw new SecurityException(SR.Format("The time zone ID '{0}' was found on the local computer, but the application does not have permission to read the registry information.", id), ex);
			}
			throw new TimeZoneNotFoundException(SR.Format("The time zone ID '{0}' was not found on the local computer.", id), ex);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x0001C338 File Offset: 0x0001A538
		internal static TimeSpan GetDateTimeNowUtcOffsetFromUtc(DateTime time, out bool isAmbiguousLocalDst)
		{
			isAmbiguousLocalDst = false;
			int year = time.Year;
			TimeZoneInfo.OffsetAndRule oneYearLocalFromUtc = TimeZoneInfo.s_cachedData.GetOneYearLocalFromUtc(year);
			TimeSpan timeSpan = oneYearLocalFromUtc.Offset;
			if (oneYearLocalFromUtc.Rule != null)
			{
				timeSpan += oneYearLocalFromUtc.Rule.BaseUtcOffsetDelta;
				if (oneYearLocalFromUtc.Rule.HasDaylightSaving)
				{
					bool isDaylightSavingsFromUtc = TimeZoneInfo.GetIsDaylightSavingsFromUtc(time, year, oneYearLocalFromUtc.Offset, oneYearLocalFromUtc.Rule, null, out isAmbiguousLocalDst, TimeZoneInfo.Local);
					timeSpan += (isDaylightSavingsFromUtc ? oneYearLocalFromUtc.Rule.DaylightDelta : TimeSpan.Zero);
				}
			}
			return timeSpan;
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x0001C3D0 File Offset: 0x0001A5D0
		private static bool TransitionTimeFromTimeZoneInformation(in Interop.Kernel32.REG_TZI_FORMAT timeZoneInformation, out TimeZoneInfo.TransitionTime transitionTime, bool readStartDate)
		{
			if (timeZoneInformation.StandardDate.Month <= 0)
			{
				transitionTime = default(TimeZoneInfo.TransitionTime);
				return false;
			}
			if (readStartDate)
			{
				if (timeZoneInformation.DaylightDate.Year == 0)
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.DaylightDate.Hour, (int)timeZoneInformation.DaylightDate.Minute, (int)timeZoneInformation.DaylightDate.Second, (int)timeZoneInformation.DaylightDate.Milliseconds), (int)timeZoneInformation.DaylightDate.Month, (int)timeZoneInformation.DaylightDate.Day, (DayOfWeek)timeZoneInformation.DaylightDate.DayOfWeek);
				}
				else
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.DaylightDate.Hour, (int)timeZoneInformation.DaylightDate.Minute, (int)timeZoneInformation.DaylightDate.Second, (int)timeZoneInformation.DaylightDate.Milliseconds), (int)timeZoneInformation.DaylightDate.Month, (int)timeZoneInformation.DaylightDate.Day);
				}
			}
			else if (timeZoneInformation.StandardDate.Year == 0)
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.StandardDate.Hour, (int)timeZoneInformation.StandardDate.Minute, (int)timeZoneInformation.StandardDate.Second, (int)timeZoneInformation.StandardDate.Milliseconds), (int)timeZoneInformation.StandardDate.Month, (int)timeZoneInformation.StandardDate.Day, (DayOfWeek)timeZoneInformation.StandardDate.DayOfWeek);
			}
			else
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.StandardDate.Hour, (int)timeZoneInformation.StandardDate.Minute, (int)timeZoneInformation.StandardDate.Second, (int)timeZoneInformation.StandardDate.Milliseconds), (int)timeZoneInformation.StandardDate.Month, (int)timeZoneInformation.StandardDate.Day);
			}
			return true;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x0001C590 File Offset: 0x0001A790
		private static bool TryCreateAdjustmentRules(string id, in Interop.Kernel32.REG_TZI_FORMAT defaultTimeZoneInformation, out TimeZoneInfo.AdjustmentRule[] rules, out Exception e, int defaultBaseUtcOffset)
		{
			rules = null;
			e = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones\\" + id + "\\Dynamic DST", false))
				{
					if (registryKey == null)
					{
						TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(defaultTimeZoneInformation, DateTime.MinValue.Date, DateTime.MaxValue.Date, defaultBaseUtcOffset);
						if (adjustmentRule != null)
						{
							rules = new TimeZoneInfo.AdjustmentRule[]
							{
								adjustmentRule
							};
						}
						return true;
					}
					int num = (int)registryKey.GetValue("FirstEntry", -1, RegistryValueOptions.None);
					int num2 = (int)registryKey.GetValue("LastEntry", -1, RegistryValueOptions.None);
					if (num == -1 || num2 == -1 || num > num2)
					{
						return false;
					}
					Interop.Kernel32.REG_TZI_FORMAT reg_TZI_FORMAT;
					if (!TimeZoneInfo.TryGetTimeZoneEntryFromRegistry(registryKey, num.ToString(CultureInfo.InvariantCulture), out reg_TZI_FORMAT))
					{
						return false;
					}
					if (num == num2)
					{
						TimeZoneInfo.AdjustmentRule adjustmentRule2 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(reg_TZI_FORMAT, DateTime.MinValue.Date, DateTime.MaxValue.Date, defaultBaseUtcOffset);
						if (adjustmentRule2 != null)
						{
							rules = new TimeZoneInfo.AdjustmentRule[]
							{
								adjustmentRule2
							};
						}
						return true;
					}
					List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>(1);
					TimeZoneInfo.AdjustmentRule adjustmentRule3 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(reg_TZI_FORMAT, DateTime.MinValue.Date, new DateTime(num, 12, 31), defaultBaseUtcOffset);
					if (adjustmentRule3 != null)
					{
						list.Add(adjustmentRule3);
					}
					for (int i = num + 1; i < num2; i++)
					{
						if (!TimeZoneInfo.TryGetTimeZoneEntryFromRegistry(registryKey, i.ToString(CultureInfo.InvariantCulture), out reg_TZI_FORMAT))
						{
							return false;
						}
						TimeZoneInfo.AdjustmentRule adjustmentRule4 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(reg_TZI_FORMAT, new DateTime(i, 1, 1), new DateTime(i, 12, 31), defaultBaseUtcOffset);
						if (adjustmentRule4 != null)
						{
							list.Add(adjustmentRule4);
						}
					}
					if (!TimeZoneInfo.TryGetTimeZoneEntryFromRegistry(registryKey, num2.ToString(CultureInfo.InvariantCulture), out reg_TZI_FORMAT))
					{
						return false;
					}
					TimeZoneInfo.AdjustmentRule adjustmentRule5 = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(reg_TZI_FORMAT, new DateTime(num2, 1, 1), DateTime.MaxValue.Date, defaultBaseUtcOffset);
					if (adjustmentRule5 != null)
					{
						list.Add(adjustmentRule5);
					}
					if (list.Count != 0)
					{
						rules = list.ToArray();
					}
				}
			}
			catch (InvalidCastException ex)
			{
				e = ex;
				return false;
			}
			catch (ArgumentOutOfRangeException ex2)
			{
				e = ex2;
				return false;
			}
			catch (ArgumentException ex3)
			{
				e = ex3;
				return false;
			}
			return true;
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x0001C818 File Offset: 0x0001AA18
		private unsafe static bool TryGetTimeZoneEntryFromRegistry(RegistryKey key, string name, out Interop.Kernel32.REG_TZI_FORMAT dtzi)
		{
			byte[] array = key.GetValue(name, null, RegistryValueOptions.None) as byte[];
			if (array == null || array.Length != sizeof(Interop.Kernel32.REG_TZI_FORMAT))
			{
				dtzi = default(Interop.Kernel32.REG_TZI_FORMAT);
				return false;
			}
			fixed (byte* ptr = &array[0])
			{
				byte* ptr2 = ptr;
				dtzi = *(Interop.Kernel32.REG_TZI_FORMAT*)ptr2;
			}
			return true;
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x0001C868 File Offset: 0x0001AA68
		private static bool TryCompareStandardDate(in Interop.Kernel32.TIME_ZONE_INFORMATION timeZone, in Interop.Kernel32.REG_TZI_FORMAT registryTimeZoneInfo)
		{
			if (timeZone.Bias == registryTimeZoneInfo.Bias && timeZone.StandardBias == registryTimeZoneInfo.StandardBias)
			{
				Interop.Kernel32.SYSTEMTIME standardDate = timeZone.StandardDate;
				return standardDate.Equals(registryTimeZoneInfo.StandardDate);
			}
			return false;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x0001C8A8 File Offset: 0x0001AAA8
		private static bool TryCompareTimeZoneInformationToRegistry(in Interop.Kernel32.TIME_ZONE_INFORMATION timeZone, string id, out bool dstDisabled)
		{
			dstDisabled = false;
			bool result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones\\" + id, false))
			{
				Interop.Kernel32.REG_TZI_FORMAT reg_TZI_FORMAT;
				if (registryKey == null)
				{
					result = false;
				}
				else if (!TimeZoneInfo.TryGetTimeZoneEntryFromRegistry(registryKey, "TZI", out reg_TZI_FORMAT))
				{
					result = false;
				}
				else if (!TimeZoneInfo.TryCompareStandardDate(timeZone, reg_TZI_FORMAT))
				{
					result = false;
				}
				else
				{
					bool flag;
					if (!dstDisabled && !TimeZoneInfo.CheckDaylightSavingTimeNotSupported(timeZone))
					{
						if (timeZone.DaylightBias == reg_TZI_FORMAT.DaylightBias)
						{
							Interop.Kernel32.SYSTEMTIME daylightDate = timeZone.DaylightDate;
							flag = daylightDate.Equals(reg_TZI_FORMAT.DaylightDate);
						}
						else
						{
							flag = false;
						}
					}
					else
					{
						flag = true;
					}
					bool flag2 = flag;
					if (flag2)
					{
						string a = registryKey.GetValue("Std", string.Empty, RegistryValueOptions.None) as string;
						Interop.Kernel32.TIME_ZONE_INFORMATION time_ZONE_INFORMATION = timeZone;
						flag2 = string.Equals(a, time_ZONE_INFORMATION.GetStandardName(), StringComparison.Ordinal);
					}
					result = flag2;
				}
			}
			return result;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0001C984 File Offset: 0x0001AB84
		private static string TryGetLocalizedNameByMuiNativeResource(string resource)
		{
			if (string.IsNullOrEmpty(resource))
			{
				return string.Empty;
			}
			string[] array = resource.Split(',', StringSplitOptions.None);
			if (array.Length != 2)
			{
				return string.Empty;
			}
			string systemDirectory = Environment.SystemDirectory;
			string path = array[0].TrimStart('@');
			string filePath;
			try
			{
				filePath = Path.Combine(systemDirectory, path);
			}
			catch (ArgumentException)
			{
				return string.Empty;
			}
			int num;
			if (!int.TryParse(array[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				return string.Empty;
			}
			num = -num;
			string result;
			try
			{
				StringBuilder stringBuilder = StringBuilderCache.Acquire(260);
				stringBuilder.Length = 260;
				int num2 = 260;
				int num3 = 0;
				long num4 = 0L;
				if (!Interop.Kernel32.GetFileMUIPath(16U, filePath, null, ref num3, stringBuilder, ref num2, ref num4))
				{
					StringBuilderCache.Release(stringBuilder);
					result = string.Empty;
				}
				else
				{
					result = TimeZoneInfo.TryGetLocalizedNameByNativeResource(StringBuilderCache.GetStringAndRelease(stringBuilder), num);
				}
			}
			catch (EntryPointNotFoundException)
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0001CA7C File Offset: 0x0001AC7C
		private static string TryGetLocalizedNameByNativeResource(string filePath, int resource)
		{
			using (SafeLibraryHandle safeLibraryHandle = Interop.Kernel32.LoadLibraryEx(filePath, IntPtr.Zero, 2))
			{
				if (!safeLibraryHandle.IsInvalid)
				{
					StringBuilder stringBuilder = StringBuilderCache.Acquire(500);
					if (Interop.User32.LoadString(safeLibraryHandle, resource, stringBuilder, 500) != 0)
					{
						return StringBuilderCache.GetStringAndRelease(stringBuilder);
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0001CAE4 File Offset: 0x0001ACE4
		private static void GetLocalizedNamesByRegistryKey(RegistryKey key, out string displayName, out string standardName, out string daylightName)
		{
			displayName = string.Empty;
			standardName = string.Empty;
			daylightName = string.Empty;
			string text = key.GetValue("MUI_Display", string.Empty, RegistryValueOptions.None) as string;
			string text2 = key.GetValue("MUI_Std", string.Empty, RegistryValueOptions.None) as string;
			string text3 = key.GetValue("MUI_Dlt", string.Empty, RegistryValueOptions.None) as string;
			if (!string.IsNullOrEmpty(text))
			{
				displayName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(text);
			}
			if (!string.IsNullOrEmpty(text2))
			{
				standardName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(text2);
			}
			if (!string.IsNullOrEmpty(text3))
			{
				daylightName = TimeZoneInfo.TryGetLocalizedNameByMuiNativeResource(text3);
			}
			if (string.IsNullOrEmpty(displayName))
			{
				displayName = (key.GetValue("Display", string.Empty, RegistryValueOptions.None) as string);
			}
			if (string.IsNullOrEmpty(standardName))
			{
				standardName = (key.GetValue("Std", string.Empty, RegistryValueOptions.None) as string);
			}
			if (string.IsNullOrEmpty(daylightName))
			{
				daylightName = (key.GetValue("Dlt", string.Empty, RegistryValueOptions.None) as string);
			}
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0001CBDE File Offset: 0x0001ADDE
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZoneFromLocalMachine(string id, out TimeZoneInfo value, out Exception e)
		{
			if (TimeZoneInfo.HaveRegistry)
			{
				return TimeZoneInfo.TryGetTimeZoneFromLocalRegistry(id, out value, out e);
			}
			e = null;
			value = TimeZoneInfo.FindSystemTimeZoneByIdWinRTFallback(id);
			return TimeZoneInfo.TimeZoneInfoResult.Success;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0001CBFC File Offset: 0x0001ADFC
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZoneFromLocalRegistry(string id, out TimeZoneInfo value, out Exception e)
		{
			e = null;
			TimeZoneInfo.TimeZoneInfoResult result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones\\" + id, false))
			{
				Interop.Kernel32.REG_TZI_FORMAT reg_TZI_FORMAT;
				TimeZoneInfo.AdjustmentRule[] adjustmentRules;
				if (registryKey == null)
				{
					value = null;
					result = TimeZoneInfo.TimeZoneInfoResult.TimeZoneNotFoundException;
				}
				else if (!TimeZoneInfo.TryGetTimeZoneEntryFromRegistry(registryKey, "TZI", out reg_TZI_FORMAT))
				{
					value = null;
					result = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
				}
				else if (!TimeZoneInfo.TryCreateAdjustmentRules(id, reg_TZI_FORMAT, out adjustmentRules, out e, reg_TZI_FORMAT.Bias))
				{
					value = null;
					result = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
				}
				else
				{
					string displayName;
					string standardDisplayName;
					string daylightDisplayName;
					TimeZoneInfo.GetLocalizedNamesByRegistryKey(registryKey, out displayName, out standardDisplayName, out daylightDisplayName);
					try
					{
						value = new TimeZoneInfo(id, new TimeSpan(0, -reg_TZI_FORMAT.Bias, 0), displayName, standardDisplayName, daylightDisplayName, adjustmentRules, false);
						result = TimeZoneInfo.TimeZoneInfoResult.Success;
					}
					catch (ArgumentException ex)
					{
						value = null;
						e = ex;
						result = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
					}
					catch (InvalidTimeZoneException ex2)
					{
						value = null;
						e = ex2;
						result = TimeZoneInfo.TimeZoneInfoResult.InvalidTimeZoneException;
					}
				}
			}
			return result;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0001CCE4 File Offset: 0x0001AEE4
		private static bool HaveRegistry
		{
			get
			{
				return TimeZoneInfo.lazyHaveRegistry.Value;
			}
		}

		// Token: 0x060005F2 RID: 1522
		[DllImport("api-ms-win-core-timezone-l1-1-0.dll")]
		internal static extern uint EnumDynamicTimeZoneInformation(uint dwIndex, out TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION lpTimeZoneInformation);

		// Token: 0x060005F3 RID: 1523
		[DllImport("api-ms-win-core-timezone-l1-1-0.dll")]
		internal static extern uint GetDynamicTimeZoneInformation(out TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION pTimeZoneInformation);

		// Token: 0x060005F4 RID: 1524
		[DllImport("api-ms-win-core-timezone-l1-1-0.dll")]
		internal static extern uint GetDynamicTimeZoneInformationEffectiveYears(ref TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION lpTimeZoneInformation, out uint FirstYear, out uint LastYear);

		// Token: 0x060005F5 RID: 1525
		[DllImport("api-ms-win-core-timezone-l1-1-0.dll")]
		internal static extern bool GetTimeZoneInformationForYear(ushort wYear, ref TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION pdtzi, out Interop.Kernel32.TIME_ZONE_INFORMATION ptzi);

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001CCF0 File Offset: 0x0001AEF0
		internal static TimeZoneInfo.AdjustmentRule CreateAdjustmentRuleFromTimeZoneInformation(ref TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION timeZoneInformation, DateTime startDate, DateTime endDate, int defaultBaseUtcOffset)
		{
			if (timeZoneInformation.TZI.StandardDate.Month <= 0)
			{
				if (timeZoneInformation.TZI.Bias == defaultBaseUtcOffset)
				{
					return null;
				}
				return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, TimeSpan.Zero, TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue, 1, 1), TimeZoneInfo.TransitionTime.CreateFixedDateRule(DateTime.MinValue.AddMilliseconds(1.0), 1, 1), new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.TZI.Bias, 0), false);
			}
			else
			{
				TimeZoneInfo.TransitionTime daylightTransitionStart;
				if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out daylightTransitionStart, true))
				{
					return null;
				}
				TimeZoneInfo.TransitionTime transitionTime;
				if (!TimeZoneInfo.TransitionTimeFromTimeZoneInformation(timeZoneInformation, out transitionTime, false))
				{
					return null;
				}
				if (daylightTransitionStart.Equals(transitionTime))
				{
					return null;
				}
				return TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(startDate, endDate, new TimeSpan(0, -timeZoneInformation.TZI.DaylightBias, 0), daylightTransitionStart, transitionTime, new TimeSpan(0, defaultBaseUtcOffset - timeZoneInformation.TZI.Bias, 0), false);
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x0001CDCC File Offset: 0x0001AFCC
		private static bool TransitionTimeFromTimeZoneInformation(TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION timeZoneInformation, out TimeZoneInfo.TransitionTime transitionTime, bool readStartDate)
		{
			if (timeZoneInformation.TZI.StandardDate.Month <= 0)
			{
				transitionTime = default(TimeZoneInfo.TransitionTime);
				return false;
			}
			if (readStartDate)
			{
				if (timeZoneInformation.TZI.DaylightDate.Year == 0)
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.TZI.DaylightDate.Hour, (int)timeZoneInformation.TZI.DaylightDate.Minute, (int)timeZoneInformation.TZI.DaylightDate.Second, (int)timeZoneInformation.TZI.DaylightDate.Milliseconds), (int)timeZoneInformation.TZI.DaylightDate.Month, (int)timeZoneInformation.TZI.DaylightDate.Day, (DayOfWeek)timeZoneInformation.TZI.DaylightDate.DayOfWeek);
				}
				else
				{
					transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.TZI.DaylightDate.Hour, (int)timeZoneInformation.TZI.DaylightDate.Minute, (int)timeZoneInformation.TZI.DaylightDate.Second, (int)timeZoneInformation.TZI.DaylightDate.Milliseconds), (int)timeZoneInformation.TZI.DaylightDate.Month, (int)timeZoneInformation.TZI.DaylightDate.Day);
				}
			}
			else if (timeZoneInformation.TZI.StandardDate.Year == 0)
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.TZI.StandardDate.Hour, (int)timeZoneInformation.TZI.StandardDate.Minute, (int)timeZoneInformation.TZI.StandardDate.Second, (int)timeZoneInformation.TZI.StandardDate.Milliseconds), (int)timeZoneInformation.TZI.StandardDate.Month, (int)timeZoneInformation.TZI.StandardDate.Day, (DayOfWeek)timeZoneInformation.TZI.StandardDate.DayOfWeek);
			}
			else
			{
				transitionTime = TimeZoneInfo.TransitionTime.CreateFixedDateRule(new DateTime(1, 1, 1, (int)timeZoneInformation.TZI.StandardDate.Hour, (int)timeZoneInformation.TZI.StandardDate.Minute, (int)timeZoneInformation.TZI.StandardDate.Second, (int)timeZoneInformation.TZI.StandardDate.Milliseconds), (int)timeZoneInformation.TZI.StandardDate.Month, (int)timeZoneInformation.TZI.StandardDate.Day);
			}
			return true;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x0001D020 File Offset: 0x0001B220
		internal static TimeZoneInfo TryCreateTimeZone(TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION timeZoneInformation)
		{
			uint num = 0U;
			uint num2 = 0U;
			TimeZoneInfo.AdjustmentRule[] adjustmentRules = null;
			int bias = timeZoneInformation.TZI.Bias;
			if (string.IsNullOrEmpty(timeZoneInformation.TimeZoneKeyName))
			{
				return null;
			}
			try
			{
				if (TimeZoneInfo.GetDynamicTimeZoneInformationEffectiveYears(ref timeZoneInformation, out num, out num2) != 0U)
				{
					num2 = (num = 0U);
				}
			}
			catch
			{
				num2 = (num = 0U);
			}
			if (num == num2)
			{
				TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(ref timeZoneInformation, DateTime.MinValue.Date, DateTime.MaxValue.Date, bias);
				if (adjustmentRule != null)
				{
					adjustmentRules = new TimeZoneInfo.AdjustmentRule[]
					{
						adjustmentRule
					};
				}
			}
			else
			{
				TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION dynamic_TIME_ZONE_INFORMATION = default(TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION);
				List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>();
				if (!TimeZoneInfo.GetTimeZoneInformationForYear((ushort)num, ref timeZoneInformation, out dynamic_TIME_ZONE_INFORMATION.TZI))
				{
					return null;
				}
				TimeZoneInfo.AdjustmentRule adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(ref dynamic_TIME_ZONE_INFORMATION, DateTime.MinValue.Date, new DateTime((int)num, 12, 31), bias);
				if (adjustmentRule != null)
				{
					list.Add(adjustmentRule);
				}
				for (uint num3 = num + 1U; num3 < num2; num3 += 1U)
				{
					if (!TimeZoneInfo.GetTimeZoneInformationForYear((ushort)num3, ref timeZoneInformation, out dynamic_TIME_ZONE_INFORMATION.TZI))
					{
						return null;
					}
					adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(ref dynamic_TIME_ZONE_INFORMATION, new DateTime((int)num3, 1, 1), new DateTime((int)num3, 12, 31), bias);
					if (adjustmentRule != null)
					{
						list.Add(adjustmentRule);
					}
				}
				if (!TimeZoneInfo.GetTimeZoneInformationForYear((ushort)num2, ref timeZoneInformation, out dynamic_TIME_ZONE_INFORMATION.TZI))
				{
					return null;
				}
				adjustmentRule = TimeZoneInfo.CreateAdjustmentRuleFromTimeZoneInformation(ref dynamic_TIME_ZONE_INFORMATION, new DateTime((int)num2, 1, 1), DateTime.MaxValue.Date, bias);
				if (adjustmentRule != null)
				{
					list.Add(adjustmentRule);
				}
				if (list.Count > 0)
				{
					adjustmentRules = list.ToArray();
				}
			}
			return new TimeZoneInfo(timeZoneInformation.TimeZoneKeyName, new TimeSpan(0, -timeZoneInformation.TZI.Bias, 0), timeZoneInformation.TZI.GetStandardName(), timeZoneInformation.TZI.GetStandardName(), timeZoneInformation.TZI.GetDaylightName(), adjustmentRules, false);
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001D1E0 File Offset: 0x0001B3E0
		internal static TimeZoneInfo GetLocalTimeZoneInfoWinRTFallback()
		{
			TimeZoneInfo result;
			try
			{
				TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION timeZoneInformation;
				if (TimeZoneInfo.GetDynamicTimeZoneInformation(out timeZoneInformation) == 4294967295U)
				{
					result = TimeZoneInfo.Utc;
				}
				else
				{
					TimeZoneInfo timeZoneInfo = TimeZoneInfo.TryCreateTimeZone(timeZoneInformation);
					result = ((timeZoneInfo != null) ? timeZoneInfo : TimeZoneInfo.Utc);
				}
			}
			catch
			{
				result = TimeZoneInfo.Utc;
			}
			return result;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x0001D230 File Offset: 0x0001B430
		internal static TimeZoneInfo FindSystemTimeZoneByIdWinRTFallback(string id)
		{
			foreach (TimeZoneInfo timeZoneInfo in TimeZoneInfo.GetSystemTimeZones())
			{
				if (string.Compare(id, timeZoneInfo.Id, StringComparison.Ordinal) == 0)
				{
					return timeZoneInfo;
				}
			}
			throw new TimeZoneNotFoundException();
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x0001D290 File Offset: 0x0001B490
		private static void GetSystemTimeZonesWinRTFallback(TimeZoneInfo.CachedData cachedData)
		{
			List<TimeZoneInfo> list = new List<TimeZoneInfo>();
			try
			{
				uint num = 0U;
				TimeZoneInfo.DYNAMIC_TIME_ZONE_INFORMATION timeZoneInformation;
				while (TimeZoneInfo.EnumDynamicTimeZoneInformation(num++, out timeZoneInformation) != 259U)
				{
					TimeZoneInfo timeZoneInfo = TimeZoneInfo.TryCreateTimeZone(timeZoneInformation);
					if (timeZoneInfo != null)
					{
						list.Add(timeZoneInfo);
					}
				}
			}
			catch
			{
			}
			if (list.Count == 0)
			{
				list.Add(TimeZoneInfo.Local);
				list.Add(TimeZoneInfo.Utc);
			}
			list.Sort(delegate(TimeZoneInfo x, TimeZoneInfo y)
			{
				int num2 = x.BaseUtcOffset.CompareTo(y.BaseUtcOffset);
				if (num2 != 0)
				{
					return num2;
				}
				return string.CompareOrdinal(x.DisplayName, y.DisplayName);
			});
			foreach (TimeZoneInfo timeZoneInfo2 in list)
			{
				if (cachedData._systemTimeZones == null)
				{
					cachedData._systemTimeZones = new Dictionary<string, TimeZoneInfo>(StringComparer.OrdinalIgnoreCase);
				}
				cachedData._systemTimeZones.Add(timeZoneInfo2.Id, timeZoneInfo2);
			}
		}

		/// <summary>Gets the time zone identifier.</summary>
		/// <returns>The time zone identifier.</returns>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060005FC RID: 1532 RVA: 0x0001D388 File Offset: 0x0001B588
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		/// <summary>Gets the general display name that represents the time zone.</summary>
		/// <returns>The time zone's general display name.</returns>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0001D390 File Offset: 0x0001B590
		public string DisplayName
		{
			get
			{
				return this._displayName ?? string.Empty;
			}
		}

		/// <summary>Gets the display name for the time zone's standard time.</summary>
		/// <returns>The display name of the time zone's standard time.</returns>
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060005FE RID: 1534 RVA: 0x0001D3A1 File Offset: 0x0001B5A1
		public string StandardName
		{
			get
			{
				return this._standardDisplayName ?? string.Empty;
			}
		}

		/// <summary>Gets the display name for the current time zone's daylight saving time.</summary>
		/// <returns>The display name for the time zone's daylight saving time.</returns>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0001D3B2 File Offset: 0x0001B5B2
		public string DaylightName
		{
			get
			{
				return this._daylightDisplayName ?? string.Empty;
			}
		}

		/// <summary>Gets the time difference between the current time zone's standard time and Coordinated Universal Time (UTC).</summary>
		/// <returns>An object that indicates the time difference between the current time zone's standard time and Coordinated Universal Time (UTC).</returns>
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x0001D3C3 File Offset: 0x0001B5C3
		public TimeSpan BaseUtcOffset
		{
			get
			{
				return this._baseUtcOffset;
			}
		}

		/// <summary>Gets a value indicating whether the time zone has any daylight saving time rules.</summary>
		/// <returns>
		///   <see langword="true" /> if the time zone supports daylight saving time; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0001D3CB File Offset: 0x0001B5CB
		public bool SupportsDaylightSavingTime
		{
			get
			{
				return this._supportsDaylightSavingTime;
			}
		}

		/// <summary>Returns information about the possible dates and times that an ambiguous date and time can be mapped to.</summary>
		/// <param name="dateTimeOffset">A date and time.</param>
		/// <returns>An array of objects that represents possible Coordinated Universal Time (UTC) offsets that a particular date and time can be mapped to.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateTimeOffset" /> is not an ambiguous time.</exception>
		// Token: 0x06000602 RID: 1538 RVA: 0x0001D3D4 File Offset: 0x0001B5D4
		public TimeSpan[] GetAmbiguousTimeOffsets(DateTimeOffset dateTimeOffset)
		{
			if (!this.SupportsDaylightSavingTime)
			{
				throw new ArgumentException("The supplied DateTimeOffset is not in an ambiguous time range.", "dateTimeOffset");
			}
			DateTime dateTime = TimeZoneInfo.ConvertTime(dateTimeOffset, this).DateTime;
			bool flag = false;
			int? ruleIndex;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForAmbiguousOffsets = this.GetAdjustmentRuleForAmbiguousOffsets(dateTime, out ruleIndex);
			if (adjustmentRuleForAmbiguousOffsets != null && adjustmentRuleForAmbiguousOffsets.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = this.GetDaylightTime(dateTime.Year, adjustmentRuleForAmbiguousOffsets, ruleIndex);
				flag = TimeZoneInfo.GetIsAmbiguousTime(dateTime, adjustmentRuleForAmbiguousOffsets, daylightTime);
			}
			if (!flag)
			{
				throw new ArgumentException("The supplied DateTimeOffset is not in an ambiguous time range.", "dateTimeOffset");
			}
			TimeSpan[] array = new TimeSpan[2];
			TimeSpan timeSpan = this._baseUtcOffset + adjustmentRuleForAmbiguousOffsets.BaseUtcOffsetDelta;
			if (adjustmentRuleForAmbiguousOffsets.DaylightDelta > TimeSpan.Zero)
			{
				array[0] = timeSpan;
				array[1] = timeSpan + adjustmentRuleForAmbiguousOffsets.DaylightDelta;
			}
			else
			{
				array[0] = timeSpan + adjustmentRuleForAmbiguousOffsets.DaylightDelta;
				array[1] = timeSpan;
			}
			return array;
		}

		/// <summary>Returns information about the possible dates and times that an ambiguous date and time can be mapped to.</summary>
		/// <param name="dateTime">A date and time.</param>
		/// <returns>An array of objects that represents possible Coordinated Universal Time (UTC) offsets that a particular date and time can be mapped to.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateTime" /> is not an ambiguous time.</exception>
		// Token: 0x06000603 RID: 1539 RVA: 0x0001D4C0 File Offset: 0x0001B6C0
		public TimeSpan[] GetAmbiguousTimeOffsets(DateTime dateTime)
		{
			if (!this.SupportsDaylightSavingTime)
			{
				throw new ArgumentException("The supplied DateTime is not in an ambiguous time range.", "dateTime");
			}
			DateTime dateTime2;
			if (dateTime.Kind == DateTimeKind.Local)
			{
				TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, TimeZoneInfoOptions.None, cachedData);
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				TimeZoneInfo.CachedData cachedData2 = TimeZoneInfo.s_cachedData;
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.s_utcTimeZone, this, TimeZoneInfoOptions.None, cachedData2);
			}
			else
			{
				dateTime2 = dateTime;
			}
			bool flag = false;
			int? ruleIndex;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForAmbiguousOffsets = this.GetAdjustmentRuleForAmbiguousOffsets(dateTime2, out ruleIndex);
			if (adjustmentRuleForAmbiguousOffsets != null && adjustmentRuleForAmbiguousOffsets.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = this.GetDaylightTime(dateTime2.Year, adjustmentRuleForAmbiguousOffsets, ruleIndex);
				flag = TimeZoneInfo.GetIsAmbiguousTime(dateTime2, adjustmentRuleForAmbiguousOffsets, daylightTime);
			}
			if (!flag)
			{
				throw new ArgumentException("The supplied DateTime is not in an ambiguous time range.", "dateTime");
			}
			TimeSpan[] array = new TimeSpan[2];
			TimeSpan timeSpan = this._baseUtcOffset + adjustmentRuleForAmbiguousOffsets.BaseUtcOffsetDelta;
			if (adjustmentRuleForAmbiguousOffsets.DaylightDelta > TimeSpan.Zero)
			{
				array[0] = timeSpan;
				array[1] = timeSpan + adjustmentRuleForAmbiguousOffsets.DaylightDelta;
			}
			else
			{
				array[0] = timeSpan + adjustmentRuleForAmbiguousOffsets.DaylightDelta;
				array[1] = timeSpan;
			}
			return array;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001D5E4 File Offset: 0x0001B7E4
		private TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForAmbiguousOffsets(DateTime adjustedTime, out int? ruleIndex)
		{
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(adjustedTime, out ruleIndex);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.NoDaylightTransitions && !adjustmentRuleForTime.HasDaylightSaving)
			{
				return this.GetPreviousAdjustmentRule(adjustmentRuleForTime, ruleIndex);
			}
			return adjustmentRuleForTime;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001D61C File Offset: 0x0001B81C
		private TimeZoneInfo.AdjustmentRule GetPreviousAdjustmentRule(TimeZoneInfo.AdjustmentRule rule, int? ruleIndex)
		{
			if (ruleIndex != null && 0 < ruleIndex.Value && ruleIndex.Value < this._adjustmentRules.Length)
			{
				return this._adjustmentRules[ruleIndex.Value - 1];
			}
			TimeZoneInfo.AdjustmentRule result = rule;
			for (int i = 1; i < this._adjustmentRules.Length; i++)
			{
				if (rule == this._adjustmentRules[i])
				{
					result = this._adjustmentRules[i - 1];
					break;
				}
			}
			return result;
		}

		/// <summary>Calculates the offset or difference between the time in this time zone and Coordinated Universal Time (UTC) for a particular date and time.</summary>
		/// <param name="dateTimeOffset">The date and time to determine the offset for.</param>
		/// <returns>An object that indicates the time difference between Coordinated Universal Time (UTC) and the current time zone.</returns>
		// Token: 0x06000606 RID: 1542 RVA: 0x0001D68C File Offset: 0x0001B88C
		public TimeSpan GetUtcOffset(DateTimeOffset dateTimeOffset)
		{
			return TimeZoneInfo.GetUtcOffsetFromUtc(dateTimeOffset.UtcDateTime, this);
		}

		/// <summary>Calculates the offset or difference between the time in this time zone and Coordinated Universal Time (UTC) for a particular date and time.</summary>
		/// <param name="dateTime">The date and time to determine the offset for.</param>
		/// <returns>An object that indicates the time difference between the two time zones.</returns>
		// Token: 0x06000607 RID: 1543 RVA: 0x0001D69B File Offset: 0x0001B89B
		public TimeSpan GetUtcOffset(DateTime dateTime)
		{
			return this.GetUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001D6AC File Offset: 0x0001B8AC
		internal static TimeSpan GetLocalUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return cachedData.Local.GetUtcOffset(dateTime, flags, cachedData);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001D6CD File Offset: 0x0001B8CD
		internal TimeSpan GetUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			return this.GetUtcOffset(dateTime, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001D6DC File Offset: 0x0001B8DC
		private TimeSpan GetUtcOffset(DateTime dateTime, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (dateTime.Kind == DateTimeKind.Local)
			{
				if (cachedData.GetCorrespondingKind(this) != DateTimeKind.Local)
				{
					return TimeZoneInfo.GetUtcOffsetFromUtc(TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.s_utcTimeZone, flags), this);
				}
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				if (cachedData.GetCorrespondingKind(this) == DateTimeKind.Utc)
				{
					return this._baseUtcOffset;
				}
				return TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, this);
			}
			return TimeZoneInfo.GetUtcOffset(dateTime, this, flags);
		}

		/// <summary>Determines whether a particular date and time in a particular time zone is ambiguous and can be mapped to two or more Coordinated Universal Time (UTC) times.</summary>
		/// <param name="dateTimeOffset">A date and time.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="dateTimeOffset" /> parameter is ambiguous in the current time zone; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600060B RID: 1547 RVA: 0x0001D744 File Offset: 0x0001B944
		public bool IsAmbiguousTime(DateTimeOffset dateTimeOffset)
		{
			return this._supportsDaylightSavingTime && this.IsAmbiguousTime(TimeZoneInfo.ConvertTime(dateTimeOffset, this).DateTime);
		}

		/// <summary>Determines whether a particular date and time in a particular time zone is ambiguous and can be mapped to two or more Coordinated Universal Time (UTC) times.</summary>
		/// <param name="dateTime">A date and time value.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="dateTime" /> parameter is ambiguous; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> value is <see cref="F:System.DateTimeKind.Local" /> and <paramref name="dateTime" /> is an invalid time.</exception>
		// Token: 0x0600060C RID: 1548 RVA: 0x0001D770 File Offset: 0x0001B970
		public bool IsAmbiguousTime(DateTime dateTime)
		{
			return this.IsAmbiguousTime(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001D77C File Offset: 0x0001B97C
		internal bool IsAmbiguousTime(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			if (!this._supportsDaylightSavingTime)
			{
				return false;
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			DateTime dateTime2 = (dateTime.Kind == DateTimeKind.Local) ? TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, flags, cachedData) : ((dateTime.Kind == DateTimeKind.Utc) ? TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.s_utcTimeZone, this, flags, cachedData) : dateTime);
			int? ruleIndex;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime2, out ruleIndex);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = this.GetDaylightTime(dateTime2.Year, adjustmentRuleForTime, ruleIndex);
				return TimeZoneInfo.GetIsAmbiguousTime(dateTime2, adjustmentRuleForTime, daylightTime);
			}
			return false;
		}

		/// <summary>Indicates whether a specified date and time falls in the range of daylight saving time for the time zone of the current <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <param name="dateTimeOffset">A date and time value.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="dateTimeOffset" /> parameter is a daylight saving time; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600060E RID: 1550 RVA: 0x0001D804 File Offset: 0x0001BA04
		public bool IsDaylightSavingTime(DateTimeOffset dateTimeOffset)
		{
			bool result;
			TimeZoneInfo.GetUtcOffsetFromUtc(dateTimeOffset.UtcDateTime, this, out result);
			return result;
		}

		/// <summary>Indicates whether a specified date and time falls in the range of daylight saving time for the time zone of the current <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <param name="dateTime">A date and time value.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="dateTime" /> parameter is a daylight saving time; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> value is <see cref="F:System.DateTimeKind.Local" /> and <paramref name="dateTime" /> is an invalid time.</exception>
		// Token: 0x0600060F RID: 1551 RVA: 0x0001D822 File Offset: 0x0001BA22
		public bool IsDaylightSavingTime(DateTime dateTime)
		{
			return this.IsDaylightSavingTime(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001D831 File Offset: 0x0001BA31
		internal bool IsDaylightSavingTime(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			return this.IsDaylightSavingTime(dateTime, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001D840 File Offset: 0x0001BA40
		private bool IsDaylightSavingTime(DateTime dateTime, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (!this._supportsDaylightSavingTime || this._adjustmentRules == null)
			{
				return false;
			}
			DateTime dateTime2;
			if (dateTime.Kind == DateTimeKind.Local)
			{
				dateTime2 = TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, this, flags, cachedData);
			}
			else if (dateTime.Kind == DateTimeKind.Utc)
			{
				if (cachedData.GetCorrespondingKind(this) == DateTimeKind.Utc)
				{
					return false;
				}
				bool result;
				TimeZoneInfo.GetUtcOffsetFromUtc(dateTime, this, out result);
				return result;
			}
			else
			{
				dateTime2 = dateTime;
			}
			int? ruleIndex;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime2, out ruleIndex);
			if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
			{
				DaylightTimeStruct daylightTime = this.GetDaylightTime(dateTime2.Year, adjustmentRuleForTime, ruleIndex);
				return TimeZoneInfo.GetIsDaylightSavings(dateTime2, adjustmentRuleForTime, daylightTime, flags);
			}
			return false;
		}

		/// <summary>Indicates whether a particular date and time is invalid.</summary>
		/// <param name="dateTime">A date and time value.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="dateTime" /> is invalid; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000612 RID: 1554 RVA: 0x0001D8D4 File Offset: 0x0001BAD4
		public bool IsInvalidTime(DateTime dateTime)
		{
			bool result = false;
			if (dateTime.Kind == DateTimeKind.Unspecified || (dateTime.Kind == DateTimeKind.Local && TimeZoneInfo.s_cachedData.GetCorrespondingKind(this) == DateTimeKind.Local))
			{
				int? ruleIndex;
				TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = this.GetAdjustmentRuleForTime(dateTime, out ruleIndex);
				if (adjustmentRuleForTime != null && adjustmentRuleForTime.HasDaylightSaving)
				{
					DaylightTimeStruct daylightTime = this.GetDaylightTime(dateTime.Year, adjustmentRuleForTime, ruleIndex);
					result = TimeZoneInfo.GetIsInvalidTime(dateTime, adjustmentRuleForTime, daylightTime);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		/// <summary>Clears cached time zone data.</summary>
		// Token: 0x06000613 RID: 1555 RVA: 0x0001D937 File Offset: 0x0001BB37
		public static void ClearCachedData()
		{
			TimeZoneInfo.s_cachedData = new TimeZoneInfo.CachedData();
		}

		/// <summary>Converts a time to the time in another time zone based on the time zone's identifier.</summary>
		/// <param name="dateTimeOffset">The date and time to convert.</param>
		/// <param name="destinationTimeZoneId">The identifier of the destination time zone.</param>
		/// <returns>The date and time in the destination time zone.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationTimeZoneId" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The time zone identifier was found but the registry data is corrupted.</exception>
		/// <exception cref="T:System.Security.SecurityException">The process does not have the permissions required to read from the registry key that contains the time zone information.</exception>
		/// <exception cref="T:System.TimeZoneNotFoundException">The <paramref name="destinationTimeZoneId" /> identifier was not found on the local system.</exception>
		// Token: 0x06000614 RID: 1556 RVA: 0x0001D943 File Offset: 0x0001BB43
		public static DateTimeOffset ConvertTimeBySystemTimeZoneId(DateTimeOffset dateTimeOffset, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTimeOffset, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		/// <summary>Converts a time to the time in another time zone based on the time zone's identifier.</summary>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="destinationTimeZoneId">The identifier of the destination time zone.</param>
		/// <returns>The date and time in the destination time zone.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationTimeZoneId" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The time zone identifier was found, but the registry data is corrupted.</exception>
		/// <exception cref="T:System.Security.SecurityException">The process does not have the permissions required to read from the registry key that contains the time zone information.</exception>
		/// <exception cref="T:System.TimeZoneNotFoundException">The <paramref name="destinationTimeZoneId" /> identifier was not found on the local system.</exception>
		// Token: 0x06000615 RID: 1557 RVA: 0x0001D951 File Offset: 0x0001BB51
		public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string destinationTimeZoneId)
		{
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		/// <summary>Converts a time from one time zone to another based on time zone identifiers.</summary>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="sourceTimeZoneId">The identifier of the source time zone.</param>
		/// <param name="destinationTimeZoneId">The identifier of the destination time zone.</param>
		/// <returns>The date and time in the destination time zone that corresponds to the <paramref name="dateTime" /> parameter in the source time zone.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> parameter does not correspond to the source time zone.  
		///  -or-  
		///  <paramref name="dateTime" /> is an invalid time in the source time zone.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceTimeZoneId" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="destinationTimeZoneId" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The time zone identifiers were found, but the registry data is corrupted.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have the permissions required to read from the registry keys that hold time zone data.</exception>
		/// <exception cref="T:System.TimeZoneNotFoundException">The <paramref name="sourceTimeZoneId" /> identifier was not found on the local system.  
		///  -or-  
		///  The <paramref name="destinationTimeZoneId" /> identifier was not found on the local system.</exception>
		// Token: 0x06000616 RID: 1558 RVA: 0x0001D960 File Offset: 0x0001BB60
		public static DateTime ConvertTimeBySystemTimeZoneId(DateTime dateTime, string sourceTimeZoneId, string destinationTimeZoneId)
		{
			if (dateTime.Kind == DateTimeKind.Local && string.Equals(sourceTimeZoneId, TimeZoneInfo.Local.Id, StringComparison.OrdinalIgnoreCase))
			{
				TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
				return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId), TimeZoneInfoOptions.None, cachedData);
			}
			if (dateTime.Kind == DateTimeKind.Utc && string.Equals(sourceTimeZoneId, TimeZoneInfo.Utc.Id, StringComparison.OrdinalIgnoreCase))
			{
				return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.s_utcTimeZone, TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId), TimeZoneInfoOptions.None, TimeZoneInfo.s_cachedData);
			}
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.FindSystemTimeZoneById(sourceTimeZoneId), TimeZoneInfo.FindSystemTimeZoneById(destinationTimeZoneId));
		}

		/// <summary>Converts a time to the time in a particular time zone.</summary>
		/// <param name="dateTimeOffset">The date and time to convert.</param>
		/// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
		/// <returns>The date and time in the destination time zone.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="destinationTimeZone" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06000617 RID: 1559 RVA: 0x0001D9EC File Offset: 0x0001BBEC
		public static DateTimeOffset ConvertTime(DateTimeOffset dateTimeOffset, TimeZoneInfo destinationTimeZone)
		{
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			DateTime utcDateTime = dateTimeOffset.UtcDateTime;
			TimeSpan utcOffsetFromUtc = TimeZoneInfo.GetUtcOffsetFromUtc(utcDateTime, destinationTimeZone);
			long num = utcDateTime.Ticks + utcOffsetFromUtc.Ticks;
			if (num > DateTimeOffset.MaxValue.Ticks)
			{
				return DateTimeOffset.MaxValue;
			}
			if (num >= DateTimeOffset.MinValue.Ticks)
			{
				return new DateTimeOffset(num, utcOffsetFromUtc);
			}
			return DateTimeOffset.MinValue;
		}

		/// <summary>Converts a time to the time in a particular time zone.</summary>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
		/// <returns>The date and time in the destination time zone.</returns>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="dateTime" /> parameter represents an invalid time.</exception>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="destinationTimeZone" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06000618 RID: 1560 RVA: 0x0001DA54 File Offset: 0x0001BC54
		public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo destinationTimeZone)
		{
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			if (dateTime.Ticks == 0L)
			{
				TimeZoneInfo.ClearCachedData();
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			TimeZoneInfo sourceTimeZone = (dateTime.Kind == DateTimeKind.Utc) ? TimeZoneInfo.s_utcTimeZone : cachedData.Local;
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, TimeZoneInfoOptions.None, cachedData);
		}

		/// <summary>Converts a time from one time zone to another.</summary>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="sourceTimeZone">The time zone of <paramref name="dateTime" />.</param>
		/// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
		/// <returns>The date and time in the destination time zone that corresponds to the <paramref name="dateTime" /> parameter in the source time zone.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> parameter is <see cref="F:System.DateTimeKind.Local" />, but the <paramref name="sourceTimeZone" /> parameter does not equal <see cref="F:System.DateTimeKind.Local" />.  
		///  -or-  
		///  The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateTime" /> parameter is <see cref="F:System.DateTimeKind.Utc" />, but the <paramref name="sourceTimeZone" /> parameter does not equal <see cref="P:System.TimeZoneInfo.Utc" />.  
		///  -or-  
		///  The <paramref name="dateTime" /> parameter is an invalid time (that is, it represents a time that does not exist because of a time zone's adjustment rules).</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="sourceTimeZone" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="destinationTimeZone" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06000619 RID: 1561 RVA: 0x0001DAA5 File Offset: 0x0001BCA5
		public static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone)
		{
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, TimeZoneInfoOptions.None, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001DAB5 File Offset: 0x0001BCB5
		internal static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone, TimeZoneInfoOptions flags)
		{
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, destinationTimeZone, flags, TimeZoneInfo.s_cachedData);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001DAC8 File Offset: 0x0001BCC8
		private static DateTime ConvertTime(DateTime dateTime, TimeZoneInfo sourceTimeZone, TimeZoneInfo destinationTimeZone, TimeZoneInfoOptions flags, TimeZoneInfo.CachedData cachedData)
		{
			if (sourceTimeZone == null)
			{
				throw new ArgumentNullException("sourceTimeZone");
			}
			if (destinationTimeZone == null)
			{
				throw new ArgumentNullException("destinationTimeZone");
			}
			DateTimeKind correspondingKind = cachedData.GetCorrespondingKind(sourceTimeZone);
			if ((flags & TimeZoneInfoOptions.NoThrowOnInvalidTime) == (TimeZoneInfoOptions)0 && dateTime.Kind != DateTimeKind.Unspecified && dateTime.Kind != correspondingKind)
			{
				throw new ArgumentException("The conversion could not be completed because the supplied DateTime did not have the Kind property set correctly.  For example, when the Kind property is DateTimeKind.Local, the source time zone must be TimeZoneInfo.Local.", "sourceTimeZone");
			}
			int? ruleIndex;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = sourceTimeZone.GetAdjustmentRuleForTime(dateTime, out ruleIndex);
			TimeSpan t = sourceTimeZone.BaseUtcOffset;
			if (adjustmentRuleForTime != null)
			{
				t += adjustmentRuleForTime.BaseUtcOffsetDelta;
				if (adjustmentRuleForTime.HasDaylightSaving)
				{
					DaylightTimeStruct daylightTime = sourceTimeZone.GetDaylightTime(dateTime.Year, adjustmentRuleForTime, ruleIndex);
					if ((flags & TimeZoneInfoOptions.NoThrowOnInvalidTime) == (TimeZoneInfoOptions)0 && TimeZoneInfo.GetIsInvalidTime(dateTime, adjustmentRuleForTime, daylightTime))
					{
						throw new ArgumentException("The supplied DateTime represents an invalid time.  For example, when the clock is adjusted forward, any time in the period that is skipped is invalid.", "dateTime");
					}
					bool isDaylightSavings = TimeZoneInfo.GetIsDaylightSavings(dateTime, adjustmentRuleForTime, daylightTime, flags);
					t += (isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero);
				}
			}
			DateTimeKind correspondingKind2 = cachedData.GetCorrespondingKind(destinationTimeZone);
			if (dateTime.Kind != DateTimeKind.Unspecified && correspondingKind != DateTimeKind.Unspecified && correspondingKind == correspondingKind2)
			{
				return dateTime;
			}
			bool isAmbiguousDst;
			DateTime dateTime2 = TimeZoneInfo.ConvertUtcToTimeZone(dateTime.Ticks - t.Ticks, destinationTimeZone, out isAmbiguousDst);
			if (correspondingKind2 == DateTimeKind.Local)
			{
				return new DateTime(dateTime2.Ticks, DateTimeKind.Local, isAmbiguousDst);
			}
			return new DateTime(dateTime2.Ticks, correspondingKind2);
		}

		/// <summary>Converts a Coordinated Universal Time (UTC) to the time in a specified time zone.</summary>
		/// <param name="dateTime">The Coordinated Universal Time (UTC).</param>
		/// <param name="destinationTimeZone">The time zone to convert <paramref name="dateTime" /> to.</param>
		/// <returns>The date and time in the destination time zone. Its <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Utc" /> if <paramref name="destinationTimeZone" /> is <see cref="P:System.TimeZoneInfo.Utc" />; otherwise, its <see cref="P:System.DateTime.Kind" /> property is <see cref="F:System.DateTimeKind.Unspecified" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of <paramref name="dateTime" /> is <see cref="F:System.DateTimeKind.Local" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationTimeZone" /> is <see langword="null" />.</exception>
		// Token: 0x0600061C RID: 1564 RVA: 0x0001DBFC File Offset: 0x0001BDFC
		public static DateTime ConvertTimeFromUtc(DateTime dateTime, TimeZoneInfo destinationTimeZone)
		{
			return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.s_utcTimeZone, destinationTimeZone, TimeZoneInfoOptions.None, TimeZoneInfo.s_cachedData);
		}

		/// <summary>Converts the specified date and time to Coordinated Universal Time (UTC).</summary>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <returns>The Coordinated Universal Time (UTC) that corresponds to the <paramref name="dateTime" /> parameter. The <see cref="T:System.DateTime" /> value's <see cref="P:System.DateTime.Kind" /> property is always set to <see cref="F:System.DateTimeKind.Utc" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see langword="TimeZoneInfo.Local.IsInvalidDateTime(" />
		///   <paramref name="dateTime" />
		///   <see langword=")" /> returns <see langword="true" />.</exception>
		// Token: 0x0600061D RID: 1565 RVA: 0x0001DC10 File Offset: 0x0001BE10
		public static DateTime ConvertTimeToUtc(DateTime dateTime)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime;
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.s_utcTimeZone, TimeZoneInfoOptions.None, cachedData);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001DC44 File Offset: 0x0001BE44
		internal static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfoOptions flags)
		{
			if (dateTime.Kind == DateTimeKind.Utc)
			{
				return dateTime;
			}
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			return TimeZoneInfo.ConvertTime(dateTime, cachedData.Local, TimeZoneInfo.s_utcTimeZone, flags, cachedData);
		}

		/// <summary>Converts the time in a specified time zone to Coordinated Universal Time (UTC).</summary>
		/// <param name="dateTime">The date and time to convert.</param>
		/// <param name="sourceTimeZone">The time zone of <paramref name="dateTime" />.</param>
		/// <returns>The Coordinated Universal Time (UTC) that corresponds to the <paramref name="dateTime" /> parameter. The <see cref="T:System.DateTime" /> object's <see cref="P:System.DateTime.Kind" /> property is always set to <see cref="F:System.DateTimeKind.Utc" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="dateTime" />.<see langword="Kind" /> is <see cref="F:System.DateTimeKind.Utc" /> and <paramref name="sourceTimeZone" /> does not equal <see cref="P:System.TimeZoneInfo.Utc" />.  
		/// -or-  
		/// <paramref name="dateTime" />.<see langword="Kind" /> is <see cref="F:System.DateTimeKind.Local" /> and <paramref name="sourceTimeZone" /> does not equal <see cref="P:System.TimeZoneInfo.Local" />.  
		/// -or-  
		/// <paramref name="sourceTimeZone" /><see langword=".IsInvalidDateTime(" /><paramref name="dateTime" /><see langword=")" /> returns <see langword="true" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceTimeZone" /> is <see langword="null" />.</exception>
		// Token: 0x0600061F RID: 1567 RVA: 0x0001DC76 File Offset: 0x0001BE76
		public static DateTime ConvertTimeToUtc(DateTime dateTime, TimeZoneInfo sourceTimeZone)
		{
			return TimeZoneInfo.ConvertTime(dateTime, sourceTimeZone, TimeZoneInfo.s_utcTimeZone, TimeZoneInfoOptions.None, TimeZoneInfo.s_cachedData);
		}

		/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo" /> object and another <see cref="T:System.TimeZoneInfo" /> object are equal.</summary>
		/// <param name="other">A second object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.TimeZoneInfo" /> objects are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000620 RID: 1568 RVA: 0x0001DC8A File Offset: 0x0001BE8A
		public bool Equals(TimeZoneInfo other)
		{
			return other != null && string.Equals(this._id, other._id, StringComparison.OrdinalIgnoreCase) && this.HasSameRules(other);
		}

		/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo" /> object and another object are equal.</summary>
		/// <param name="obj">A second object to compare with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is a <see cref="T:System.TimeZoneInfo" /> object that is equal to the current instance; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000621 RID: 1569 RVA: 0x0001DCAC File Offset: 0x0001BEAC
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TimeZoneInfo);
		}

		/// <summary>Deserializes a string to re-create an original serialized <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <param name="source">The string representation of the serialized <see cref="T:System.TimeZoneInfo" /> object.</param>
		/// <returns>The original serialized object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="source" /> parameter is <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> parameter is a null string.</exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The source parameter cannot be deserialized back into a <see cref="T:System.TimeZoneInfo" /> object.</exception>
		// Token: 0x06000622 RID: 1570 RVA: 0x0001DCBA File Offset: 0x0001BEBA
		public static TimeZoneInfo FromSerializedString(string source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (source.Length == 0)
			{
				throw new ArgumentException(SR.Format("The specified serialized string '{0}' is not supported.", source), "source");
			}
			return TimeZoneInfo.StringSerializer.GetDeserializedTimeZoneInfo(source);
		}

		/// <summary>Serves as a hash function for hashing algorithms and data structures such as hash tables.</summary>
		/// <returns>A 32-bit signed integer that serves as the hash code for this <see cref="T:System.TimeZoneInfo" /> object.</returns>
		// Token: 0x06000623 RID: 1571 RVA: 0x0001DCEE File Offset: 0x0001BEEE
		public override int GetHashCode()
		{
			return StringComparer.OrdinalIgnoreCase.GetHashCode(this._id);
		}

		/// <summary>Returns a sorted collection of all the time zones about which information is available on the local system.</summary>
		/// <returns>A read-only collection of <see cref="T:System.TimeZoneInfo" /> objects.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to store all time zone information.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to read from the registry keys that contain time zone information.</exception>
		// Token: 0x06000624 RID: 1572 RVA: 0x0001DD00 File Offset: 0x0001BF00
		public static ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones()
		{
			TimeZoneInfo.CachedData cachedData = TimeZoneInfo.s_cachedData;
			TimeZoneInfo.CachedData obj = cachedData;
			lock (obj)
			{
				if (cachedData._readOnlySystemTimeZones == null)
				{
					TimeZoneInfo.PopulateAllSystemTimeZones(cachedData);
					cachedData._allSystemTimeZonesRead = true;
					List<TimeZoneInfo> list;
					if (cachedData._systemTimeZones != null)
					{
						list = new List<TimeZoneInfo>(cachedData._systemTimeZones.Values);
					}
					else
					{
						list = new List<TimeZoneInfo>();
					}
					list.Sort(delegate(TimeZoneInfo x, TimeZoneInfo y)
					{
						int num = x.BaseUtcOffset.CompareTo(y.BaseUtcOffset);
						if (num != 0)
						{
							return num;
						}
						return string.CompareOrdinal(x.DisplayName, y.DisplayName);
					});
					cachedData._readOnlySystemTimeZones = new ReadOnlyCollection<TimeZoneInfo>(list);
				}
			}
			return cachedData._readOnlySystemTimeZones;
		}

		/// <summary>Indicates whether the current object and another <see cref="T:System.TimeZoneInfo" /> object have the same adjustment rules.</summary>
		/// <param name="other">A second object to compare with the current <see cref="T:System.TimeZoneInfo" /> object.</param>
		/// <returns>
		///   <see langword="true" /> if the two time zones have identical adjustment rules and an identical base offset; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="other" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06000625 RID: 1573 RVA: 0x0001DDA8 File Offset: 0x0001BFA8
		public bool HasSameRules(TimeZoneInfo other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (this._baseUtcOffset != other._baseUtcOffset || this._supportsDaylightSavingTime != other._supportsDaylightSavingTime)
			{
				return false;
			}
			TimeZoneInfo.AdjustmentRule[] adjustmentRules = this._adjustmentRules;
			TimeZoneInfo.AdjustmentRule[] adjustmentRules2 = other._adjustmentRules;
			bool flag = (adjustmentRules == null && adjustmentRules2 == null) || (adjustmentRules != null && adjustmentRules2 != null);
			if (!flag)
			{
				return false;
			}
			if (adjustmentRules != null)
			{
				if (adjustmentRules.Length != adjustmentRules2.Length)
				{
					return false;
				}
				for (int i = 0; i < adjustmentRules.Length; i++)
				{
					if (!adjustmentRules[i].Equals(adjustmentRules2[i]))
					{
						return false;
					}
				}
			}
			return flag;
		}

		/// <summary>Gets a <see cref="T:System.TimeZoneInfo" /> object that represents the local time zone.</summary>
		/// <returns>An object that represents the local time zone.</returns>
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x0001DE38 File Offset: 0x0001C038
		public static TimeZoneInfo Local
		{
			get
			{
				return TimeZoneInfo.s_cachedData.Local;
			}
		}

		/// <summary>Converts the current <see cref="T:System.TimeZoneInfo" /> object to a serialized string.</summary>
		/// <returns>A string that represents the current <see cref="T:System.TimeZoneInfo" /> object.</returns>
		// Token: 0x06000627 RID: 1575 RVA: 0x0001DE44 File Offset: 0x0001C044
		public string ToSerializedString()
		{
			return TimeZoneInfo.StringSerializer.GetSerializedString(this);
		}

		/// <summary>Returns the current <see cref="T:System.TimeZoneInfo" /> object's display name.</summary>
		/// <returns>The value of the <see cref="P:System.TimeZoneInfo.DisplayName" /> property of the current <see cref="T:System.TimeZoneInfo" /> object.</returns>
		// Token: 0x06000628 RID: 1576 RVA: 0x0001DE4C File Offset: 0x0001C04C
		public override string ToString()
		{
			return this.DisplayName;
		}

		/// <summary>Gets a <see cref="T:System.TimeZoneInfo" /> object that represents the Coordinated Universal Time (UTC) zone.</summary>
		/// <returns>An object that represents the Coordinated Universal Time (UTC) zone.</returns>
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0001DE54 File Offset: 0x0001C054
		public static TimeZoneInfo Utc
		{
			get
			{
				return TimeZoneInfo.s_utcTimeZone;
			}
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001DE5C File Offset: 0x0001C05C
		private TimeZoneInfo(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			bool flag;
			TimeZoneInfo.ValidateTimeZoneInfo(id, baseUtcOffset, adjustmentRules, out flag);
			this._id = id;
			this._baseUtcOffset = baseUtcOffset;
			this._displayName = displayName;
			this._standardDisplayName = standardDisplayName;
			this._daylightDisplayName = (disableDaylightSavingTime ? null : daylightDisplayName);
			this._supportsDaylightSavingTime = (flag && !disableDaylightSavingTime);
			this._adjustmentRules = adjustmentRules;
		}

		/// <summary>Creates a custom time zone with a specified identifier, an offset from Coordinated Universal Time (UTC), a display name, and a standard time display name.</summary>
		/// <param name="id">The time zone's identifier.</param>
		/// <param name="baseUtcOffset">An object that represents the time difference between this time zone and Coordinated Universal Time (UTC).</param>
		/// <param name="displayName">The display name of the new time zone.</param>
		/// <param name="standardDisplayName">The name of the new time zone's standard time.</param>
		/// <returns>The new time zone.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="id" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="id" /> parameter is an empty string ("").  
		///  -or-  
		///  The <paramref name="baseUtcOffset" /> parameter does not represent a whole number of minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="baseUtcOffset" /> parameter is greater than 14 hours or less than -14 hours.</exception>
		// Token: 0x0600062B RID: 1579 RVA: 0x0001DEBF File Offset: 0x0001C0BF
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName)
		{
			return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, standardDisplayName, null, false);
		}

		/// <summary>Creates a custom time zone with a specified identifier, an offset from Coordinated Universal Time (UTC), a display name, a standard time name, a daylight saving time name, and daylight saving time rules.</summary>
		/// <param name="id">The time zone's identifier.</param>
		/// <param name="baseUtcOffset">An object that represents the time difference between this time zone and Coordinated Universal Time (UTC).</param>
		/// <param name="displayName">The display name of the new time zone.</param>
		/// <param name="standardDisplayName">The new time zone's standard time name.</param>
		/// <param name="daylightDisplayName">The daylight saving time name of the new time zone.</param>
		/// <param name="adjustmentRules">An array that augments the base UTC offset for a particular period.</param>
		/// <returns>A <see cref="T:System.TimeZoneInfo" /> object that represents the new time zone.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="id" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="id" /> parameter is an empty string ("").  
		///  -or-  
		///  The <paramref name="baseUtcOffset" /> parameter does not represent a whole number of minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="baseUtcOffset" /> parameter is greater than 14 hours or less than -14 hours.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The adjustment rules specified in the <paramref name="adjustmentRules" /> parameter overlap.  
		///  -or-  
		///  The adjustment rules specified in the <paramref name="adjustmentRules" /> parameter are not in chronological order.  
		///  -or-  
		///  One or more elements in <paramref name="adjustmentRules" /> are <see langword="null" />.  
		///  -or-  
		///  A date can have multiple adjustment rules applied to it.  
		///  -or-  
		///  The sum of the <paramref name="baseUtcOffset" /> parameter and the <see cref="P:System.TimeZoneInfo.AdjustmentRule.DaylightDelta" /> value of one or more objects in the <paramref name="adjustmentRules" /> array is greater than 14 hours or less than -14 hours.</exception>
		// Token: 0x0600062C RID: 1580 RVA: 0x0001DECD File Offset: 0x0001C0CD
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules)
		{
			return TimeZoneInfo.CreateCustomTimeZone(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, false);
		}

		/// <summary>Creates a custom time zone with a specified identifier, an offset from Coordinated Universal Time (UTC), a display name, a standard time name, a daylight saving time name, daylight saving time rules, and a value that indicates whether the returned object reflects daylight saving time information.</summary>
		/// <param name="id">The time zone's identifier.</param>
		/// <param name="baseUtcOffset">A <see cref="T:System.TimeSpan" /> object that represents the time difference between this time zone and Coordinated Universal Time (UTC).</param>
		/// <param name="displayName">The display name of the new time zone.</param>
		/// <param name="standardDisplayName">The standard time name of the new time zone.</param>
		/// <param name="daylightDisplayName">The daylight saving time name of the new time zone.</param>
		/// <param name="adjustmentRules">An array of <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> objects that augment the base UTC offset for a particular period.</param>
		/// <param name="disableDaylightSavingTime">
		///   <see langword="true" /> to discard any daylight saving time-related information present in <paramref name="adjustmentRules" /> with the new object; otherwise, <see langword="false" />.</param>
		/// <returns>The new time zone. If the <paramref name="disableDaylightSavingTime" /> parameter is <see langword="true" />, the returned object has no daylight saving time data.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="id" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="id" /> parameter is an empty string ("").  
		///  -or-  
		///  The <paramref name="baseUtcOffset" /> parameter does not represent a whole number of minutes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="baseUtcOffset" /> parameter is greater than 14 hours or less than -14 hours.</exception>
		/// <exception cref="T:System.InvalidTimeZoneException">The adjustment rules specified in the <paramref name="adjustmentRules" /> parameter overlap.  
		///  -or-  
		///  The adjustment rules specified in the <paramref name="adjustmentRules" /> parameter are not in chronological order.  
		///  -or-  
		///  One or more elements in <paramref name="adjustmentRules" /> are <see langword="null" />.  
		///  -or-  
		///  A date can have multiple adjustment rules applied to it.  
		///  -or-  
		///  The sum of the <paramref name="baseUtcOffset" /> parameter and the <see cref="P:System.TimeZoneInfo.AdjustmentRule.DaylightDelta" /> value of one or more objects in the <paramref name="adjustmentRules" /> array is greater than 14 hours or less than -14 hours.</exception>
		// Token: 0x0600062D RID: 1581 RVA: 0x0001DEDD File Offset: 0x0001C0DD
		public static TimeZoneInfo CreateCustomTimeZone(string id, TimeSpan baseUtcOffset, string displayName, string standardDisplayName, string daylightDisplayName, TimeZoneInfo.AdjustmentRule[] adjustmentRules, bool disableDaylightSavingTime)
		{
			if (!disableDaylightSavingTime && adjustmentRules != null && adjustmentRules.Length != 0)
			{
				adjustmentRules = (TimeZoneInfo.AdjustmentRule[])adjustmentRules.Clone();
			}
			return new TimeZoneInfo(id, baseUtcOffset, displayName, standardDisplayName, daylightDisplayName, adjustmentRules, disableDaylightSavingTime);
		}

		/// <summary>Runs when the deserialization of an object has been completed.</summary>
		/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The <see cref="T:System.TimeZoneInfo" /> object contains invalid or corrupted data.</exception>
		// Token: 0x0600062E RID: 1582 RVA: 0x0001DF0C File Offset: 0x0001C10C
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			try
			{
				bool flag;
				TimeZoneInfo.ValidateTimeZoneInfo(this._id, this._baseUtcOffset, this._adjustmentRules, out flag);
				if (flag != this._supportsDaylightSavingTime)
				{
					throw new SerializationException(SR.Format("The value of the field '{0}' is invalid.  The serialized data is corrupt.", "SupportsDaylightSavingTime"));
				}
			}
			catch (ArgumentException innerException)
			{
				throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", innerException);
			}
			catch (InvalidTimeZoneException innerException2)
			{
				throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", innerException2);
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data needed to serialize the current <see cref="T:System.TimeZoneInfo" /> object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to populate with data.</param>
		/// <param name="context">The destination for this serialization (see <see cref="T:System.Runtime.Serialization.StreamingContext" />).</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600062F RID: 1583 RVA: 0x0001DF88 File Offset: 0x0001C188
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Id", this._id);
			info.AddValue("DisplayName", this._displayName);
			info.AddValue("StandardName", this._standardDisplayName);
			info.AddValue("DaylightName", this._daylightDisplayName);
			info.AddValue("BaseUtcOffset", this._baseUtcOffset);
			info.AddValue("AdjustmentRules", this._adjustmentRules);
			info.AddValue("SupportsDaylightSavingTime", this._supportsDaylightSavingTime);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001E020 File Offset: 0x0001C220
		private TimeZoneInfo(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._id = (string)info.GetValue("Id", typeof(string));
			this._displayName = (string)info.GetValue("DisplayName", typeof(string));
			this._standardDisplayName = (string)info.GetValue("StandardName", typeof(string));
			this._daylightDisplayName = (string)info.GetValue("DaylightName", typeof(string));
			this._baseUtcOffset = (TimeSpan)info.GetValue("BaseUtcOffset", typeof(TimeSpan));
			this._adjustmentRules = (TimeZoneInfo.AdjustmentRule[])info.GetValue("AdjustmentRules", typeof(TimeZoneInfo.AdjustmentRule[]));
			this._supportsDaylightSavingTime = (bool)info.GetValue("SupportsDaylightSavingTime", typeof(bool));
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001E121 File Offset: 0x0001C321
		private TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForTime(DateTime dateTime, out int? ruleIndex)
		{
			return this.GetAdjustmentRuleForTime(dateTime, false, out ruleIndex);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001E12C File Offset: 0x0001C32C
		private TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForTime(DateTime dateTime, bool dateTimeisUtc, out int? ruleIndex)
		{
			if (this._adjustmentRules == null || this._adjustmentRules.Length == 0)
			{
				ruleIndex = null;
				return null;
			}
			DateTime dateOnly = dateTimeisUtc ? (dateTime + this.BaseUtcOffset).Date : dateTime.Date;
			int i = 0;
			int num = this._adjustmentRules.Length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				TimeZoneInfo.AdjustmentRule adjustmentRule = this._adjustmentRules[num2];
				TimeZoneInfo.AdjustmentRule previousRule = (num2 > 0) ? this._adjustmentRules[num2 - 1] : adjustmentRule;
				int num3 = this.CompareAdjustmentRuleToDateTime(adjustmentRule, previousRule, dateTime, dateOnly, dateTimeisUtc);
				if (num3 == 0)
				{
					ruleIndex = new int?(num2);
					return adjustmentRule;
				}
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			ruleIndex = null;
			return null;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001E1F0 File Offset: 0x0001C3F0
		private int CompareAdjustmentRuleToDateTime(TimeZoneInfo.AdjustmentRule rule, TimeZoneInfo.AdjustmentRule previousRule, DateTime dateTime, DateTime dateOnly, bool dateTimeisUtc)
		{
			bool flag;
			if (rule.DateStart.Kind == DateTimeKind.Utc)
			{
				flag = ((dateTimeisUtc ? dateTime : this.ConvertToUtc(dateTime, previousRule.DaylightDelta, previousRule.BaseUtcOffsetDelta)) >= rule.DateStart);
			}
			else
			{
				flag = (dateOnly >= rule.DateStart);
			}
			if (!flag)
			{
				return 1;
			}
			bool flag2;
			if (rule.DateEnd.Kind == DateTimeKind.Utc)
			{
				flag2 = ((dateTimeisUtc ? dateTime : this.ConvertToUtc(dateTime, rule.DaylightDelta, rule.BaseUtcOffsetDelta)) <= rule.DateEnd);
			}
			else
			{
				flag2 = (dateOnly <= rule.DateEnd);
			}
			if (!flag2)
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001E296 File Offset: 0x0001C496
		private DateTime ConvertToUtc(DateTime dateTime, TimeSpan daylightDelta, TimeSpan baseUtcOffsetDelta)
		{
			return this.ConvertToFromUtc(dateTime, daylightDelta, baseUtcOffsetDelta, true);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0001E2A2 File Offset: 0x0001C4A2
		private DateTime ConvertFromUtc(DateTime dateTime, TimeSpan daylightDelta, TimeSpan baseUtcOffsetDelta)
		{
			return this.ConvertToFromUtc(dateTime, daylightDelta, baseUtcOffsetDelta, false);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x0001E2B0 File Offset: 0x0001C4B0
		private DateTime ConvertToFromUtc(DateTime dateTime, TimeSpan daylightDelta, TimeSpan baseUtcOffsetDelta, bool convertToUtc)
		{
			TimeSpan timeSpan = this.BaseUtcOffset + daylightDelta + baseUtcOffsetDelta;
			if (convertToUtc)
			{
				timeSpan = timeSpan.Negate();
			}
			long num = dateTime.Ticks + timeSpan.Ticks;
			if (num > DateTime.MaxValue.Ticks)
			{
				return DateTime.MaxValue;
			}
			if (num >= DateTime.MinValue.Ticks)
			{
				return new DateTime(num);
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x0001E318 File Offset: 0x0001C518
		private static DateTime ConvertUtcToTimeZone(long ticks, TimeZoneInfo destinationTimeZone, out bool isAmbiguousLocalDst)
		{
			ticks += TimeZoneInfo.GetUtcOffsetFromUtc((ticks > DateTime.MaxValue.Ticks) ? DateTime.MaxValue : ((ticks < DateTime.MinValue.Ticks) ? DateTime.MinValue : new DateTime(ticks)), destinationTimeZone, out isAmbiguousLocalDst).Ticks;
			if (ticks > DateTime.MaxValue.Ticks)
			{
				return DateTime.MaxValue;
			}
			if (ticks >= DateTime.MinValue.Ticks)
			{
				return new DateTime(ticks);
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x0001E394 File Offset: 0x0001C594
		private DaylightTimeStruct GetDaylightTime(int year, TimeZoneInfo.AdjustmentRule rule, int? ruleIndex)
		{
			TimeSpan daylightDelta = rule.DaylightDelta;
			DateTime start;
			DateTime end;
			if (rule.NoDaylightTransitions)
			{
				TimeZoneInfo.AdjustmentRule previousAdjustmentRule = this.GetPreviousAdjustmentRule(rule, ruleIndex);
				start = this.ConvertFromUtc(rule.DateStart, previousAdjustmentRule.DaylightDelta, previousAdjustmentRule.BaseUtcOffsetDelta);
				end = this.ConvertFromUtc(rule.DateEnd, rule.DaylightDelta, rule.BaseUtcOffsetDelta);
			}
			else
			{
				start = TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionStart);
				end = TimeZoneInfo.TransitionTimeToDateTime(year, rule.DaylightTransitionEnd);
			}
			return new DaylightTimeStruct(start, end, daylightDelta);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001E410 File Offset: 0x0001C610
		private static bool GetIsDaylightSavings(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime, TimeZoneInfoOptions flags)
		{
			if (rule == null)
			{
				return false;
			}
			DateTime startTime;
			DateTime endTime;
			if (time.Kind == DateTimeKind.Local)
			{
				startTime = (rule.IsStartDateMarkerForBeginningOfYear() ? new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) : (daylightTime.Start + daylightTime.Delta));
				endTime = (rule.IsEndDateMarkerForEndOfYear() ? new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) : daylightTime.End);
			}
			else
			{
				bool flag = rule.DaylightDelta > TimeSpan.Zero;
				startTime = (rule.IsStartDateMarkerForBeginningOfYear() ? new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) : (daylightTime.Start + (flag ? rule.DaylightDelta : TimeSpan.Zero)));
				endTime = (rule.IsEndDateMarkerForEndOfYear() ? new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) : (daylightTime.End + (flag ? (-rule.DaylightDelta) : TimeSpan.Zero)));
			}
			bool flag2 = TimeZoneInfo.CheckIsDst(startTime, time, endTime, false, rule);
			if (flag2 && time.Kind == DateTimeKind.Local && TimeZoneInfo.GetIsAmbiguousTime(time, rule, daylightTime))
			{
				flag2 = time.IsAmbiguousDaylightSavingTime();
			}
			return flag2;
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001E560 File Offset: 0x0001C760
		private TimeSpan GetDaylightSavingsStartOffsetFromUtc(TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule rule, int? ruleIndex)
		{
			if (rule.NoDaylightTransitions)
			{
				TimeZoneInfo.AdjustmentRule previousAdjustmentRule = this.GetPreviousAdjustmentRule(rule, ruleIndex);
				return baseUtcOffset + previousAdjustmentRule.BaseUtcOffsetDelta + previousAdjustmentRule.DaylightDelta;
			}
			return baseUtcOffset + rule.BaseUtcOffsetDelta;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001E5A2 File Offset: 0x0001C7A2
		private TimeSpan GetDaylightSavingsEndOffsetFromUtc(TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule rule)
		{
			return baseUtcOffset + rule.BaseUtcOffsetDelta + rule.DaylightDelta;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001E5BC File Offset: 0x0001C7BC
		private static bool GetIsDaylightSavingsFromUtc(DateTime time, int year, TimeSpan utc, TimeZoneInfo.AdjustmentRule rule, int? ruleIndex, out bool isAmbiguousLocalDst, TimeZoneInfo zone)
		{
			isAmbiguousLocalDst = false;
			if (rule == null)
			{
				return false;
			}
			DaylightTimeStruct daylightTime = zone.GetDaylightTime(year, rule, ruleIndex);
			bool ignoreYearAdjustment = false;
			TimeSpan daylightSavingsStartOffsetFromUtc = zone.GetDaylightSavingsStartOffsetFromUtc(utc, rule, ruleIndex);
			DateTime dateTime;
			if (rule.IsStartDateMarkerForBeginningOfYear() && daylightTime.Start.Year > DateTime.MinValue.Year)
			{
				int? ruleIndex2;
				TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(new DateTime(daylightTime.Start.Year - 1, 12, 31), out ruleIndex2);
				if (adjustmentRuleForTime != null && adjustmentRuleForTime.IsEndDateMarkerForEndOfYear())
				{
					dateTime = zone.GetDaylightTime(daylightTime.Start.Year - 1, adjustmentRuleForTime, ruleIndex2).Start - utc - adjustmentRuleForTime.BaseUtcOffsetDelta;
					ignoreYearAdjustment = true;
				}
				else
				{
					dateTime = new DateTime(daylightTime.Start.Year, 1, 1, 0, 0, 0) - daylightSavingsStartOffsetFromUtc;
				}
			}
			else
			{
				dateTime = daylightTime.Start - daylightSavingsStartOffsetFromUtc;
			}
			TimeSpan daylightSavingsEndOffsetFromUtc = zone.GetDaylightSavingsEndOffsetFromUtc(utc, rule);
			DateTime dateTime2;
			if (rule.IsEndDateMarkerForEndOfYear() && daylightTime.End.Year < DateTime.MaxValue.Year)
			{
				int? ruleIndex3;
				TimeZoneInfo.AdjustmentRule adjustmentRuleForTime2 = zone.GetAdjustmentRuleForTime(new DateTime(daylightTime.End.Year + 1, 1, 1), out ruleIndex3);
				if (adjustmentRuleForTime2 != null && adjustmentRuleForTime2.IsStartDateMarkerForBeginningOfYear())
				{
					if (adjustmentRuleForTime2.IsEndDateMarkerForEndOfYear())
					{
						dateTime2 = new DateTime(daylightTime.End.Year + 1, 12, 31) - utc - adjustmentRuleForTime2.BaseUtcOffsetDelta - adjustmentRuleForTime2.DaylightDelta;
					}
					else
					{
						dateTime2 = zone.GetDaylightTime(daylightTime.End.Year + 1, adjustmentRuleForTime2, ruleIndex3).End - utc - adjustmentRuleForTime2.BaseUtcOffsetDelta - adjustmentRuleForTime2.DaylightDelta;
					}
					ignoreYearAdjustment = true;
				}
				else
				{
					dateTime2 = new DateTime(daylightTime.End.Year + 1, 1, 1, 0, 0, 0).AddTicks(-1L) - daylightSavingsEndOffsetFromUtc;
				}
			}
			else
			{
				dateTime2 = daylightTime.End - daylightSavingsEndOffsetFromUtc;
			}
			DateTime t;
			DateTime t2;
			if (daylightTime.Delta.Ticks > 0L)
			{
				t = dateTime2 - daylightTime.Delta;
				t2 = dateTime2;
			}
			else
			{
				t = dateTime;
				t2 = dateTime - daylightTime.Delta;
			}
			bool flag = TimeZoneInfo.CheckIsDst(dateTime, time, dateTime2, ignoreYearAdjustment, rule);
			if (flag)
			{
				isAmbiguousLocalDst = (time >= t && time < t2);
				if (!isAmbiguousLocalDst && t.Year != t2.Year)
				{
					try
					{
						t.AddYears(1);
						t2.AddYears(1);
						isAmbiguousLocalDst = (time >= t && time < t2);
					}
					catch (ArgumentOutOfRangeException)
					{
					}
					if (!isAmbiguousLocalDst)
					{
						try
						{
							t.AddYears(-1);
							t2.AddYears(-1);
							isAmbiguousLocalDst = (time >= t && time < t2);
						}
						catch (ArgumentOutOfRangeException)
						{
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x0001E8C0 File Offset: 0x0001CAC0
		private static bool CheckIsDst(DateTime startTime, DateTime time, DateTime endTime, bool ignoreYearAdjustment, TimeZoneInfo.AdjustmentRule rule)
		{
			if (!ignoreYearAdjustment && !rule.NoDaylightTransitions)
			{
				int year = startTime.Year;
				int year2 = endTime.Year;
				if (year != year2)
				{
					endTime = endTime.AddYears(year - year2);
				}
				int year3 = time.Year;
				if (year != year3)
				{
					time = time.AddYears(year - year3);
				}
			}
			if (startTime > endTime)
			{
				return time < endTime || time >= startTime;
			}
			if (rule.NoDaylightTransitions)
			{
				return time >= startTime && time <= endTime;
			}
			return time >= startTime && time < endTime;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0001E95C File Offset: 0x0001CB5C
		private static bool GetIsAmbiguousTime(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime)
		{
			bool flag = false;
			if (rule == null || rule.DaylightDelta == TimeSpan.Zero)
			{
				return flag;
			}
			DateTime t;
			DateTime t2;
			if (rule.DaylightDelta > TimeSpan.Zero)
			{
				if (rule.IsEndDateMarkerForEndOfYear())
				{
					return false;
				}
				t = daylightTime.End;
				t2 = daylightTime.End - rule.DaylightDelta;
			}
			else
			{
				if (rule.IsStartDateMarkerForBeginningOfYear())
				{
					return false;
				}
				t = daylightTime.Start;
				t2 = daylightTime.Start + rule.DaylightDelta;
			}
			flag = (time >= t2 && time < t);
			if (!flag && t.Year != t2.Year)
			{
				try
				{
					DateTime t3 = t.AddYears(1);
					DateTime t4 = t2.AddYears(1);
					flag = (time >= t4 && time < t3);
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				if (!flag)
				{
					try
					{
						DateTime t3 = t.AddYears(-1);
						DateTime t4 = t2.AddYears(-1);
						flag = (time >= t4 && time < t3);
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
			}
			return flag;
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0001EA80 File Offset: 0x0001CC80
		private static bool GetIsInvalidTime(DateTime time, TimeZoneInfo.AdjustmentRule rule, DaylightTimeStruct daylightTime)
		{
			bool flag = false;
			if (rule == null || rule.DaylightDelta == TimeSpan.Zero)
			{
				return flag;
			}
			DateTime t;
			DateTime t2;
			if (rule.DaylightDelta < TimeSpan.Zero)
			{
				if (rule.IsEndDateMarkerForEndOfYear())
				{
					return false;
				}
				t = daylightTime.End;
				t2 = daylightTime.End - rule.DaylightDelta;
			}
			else
			{
				if (rule.IsStartDateMarkerForBeginningOfYear())
				{
					return false;
				}
				t = daylightTime.Start;
				t2 = daylightTime.Start + rule.DaylightDelta;
			}
			flag = (time >= t && time < t2);
			if (!flag && t.Year != t2.Year)
			{
				try
				{
					DateTime t3 = t.AddYears(1);
					DateTime t4 = t2.AddYears(1);
					flag = (time >= t3 && time < t4);
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				if (!flag)
				{
					try
					{
						DateTime t3 = t.AddYears(-1);
						DateTime t4 = t2.AddYears(-1);
						flag = (time >= t3 && time < t4);
					}
					catch (ArgumentOutOfRangeException)
					{
					}
				}
			}
			return flag;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0001EBA4 File Offset: 0x0001CDA4
		private static TimeSpan GetUtcOffset(DateTime time, TimeZoneInfo zone, TimeZoneInfoOptions flags)
		{
			TimeSpan timeSpan = zone.BaseUtcOffset;
			int? ruleIndex;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(time, out ruleIndex);
			if (adjustmentRuleForTime != null)
			{
				timeSpan += adjustmentRuleForTime.BaseUtcOffsetDelta;
				if (adjustmentRuleForTime.HasDaylightSaving)
				{
					DaylightTimeStruct daylightTime = zone.GetDaylightTime(time.Year, adjustmentRuleForTime, ruleIndex);
					bool isDaylightSavings = TimeZoneInfo.GetIsDaylightSavings(time, adjustmentRuleForTime, daylightTime, flags);
					timeSpan += (isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero);
				}
			}
			return timeSpan;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x0001EC10 File Offset: 0x0001CE10
		private static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone)
		{
			bool flag;
			return TimeZoneInfo.GetUtcOffsetFromUtc(time, zone, out flag);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0001EC28 File Offset: 0x0001CE28
		private static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone, out bool isDaylightSavings)
		{
			bool flag;
			return TimeZoneInfo.GetUtcOffsetFromUtc(time, zone, out isDaylightSavings, out flag);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001EC40 File Offset: 0x0001CE40
		internal static TimeSpan GetUtcOffsetFromUtc(DateTime time, TimeZoneInfo zone, out bool isDaylightSavings, out bool isAmbiguousLocalDst)
		{
			isDaylightSavings = false;
			isAmbiguousLocalDst = false;
			TimeSpan timeSpan = zone.BaseUtcOffset;
			int? ruleIndex;
			TimeZoneInfo.AdjustmentRule adjustmentRuleForTime;
			int year;
			if (time > TimeZoneInfo.s_maxDateOnly)
			{
				adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(DateTime.MaxValue, out ruleIndex);
				year = 9999;
			}
			else if (time < TimeZoneInfo.s_minDateOnly)
			{
				adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(DateTime.MinValue, out ruleIndex);
				year = 1;
			}
			else
			{
				adjustmentRuleForTime = zone.GetAdjustmentRuleForTime(time, true, out ruleIndex);
				year = (time + timeSpan).Year;
			}
			if (adjustmentRuleForTime != null)
			{
				timeSpan += adjustmentRuleForTime.BaseUtcOffsetDelta;
				if (adjustmentRuleForTime.HasDaylightSaving)
				{
					isDaylightSavings = TimeZoneInfo.GetIsDaylightSavingsFromUtc(time, year, zone._baseUtcOffset, adjustmentRuleForTime, ruleIndex, out isAmbiguousLocalDst, zone);
					timeSpan += (isDaylightSavings ? adjustmentRuleForTime.DaylightDelta : TimeSpan.Zero);
				}
			}
			return timeSpan;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0001ECFC File Offset: 0x0001CEFC
		internal static DateTime TransitionTimeToDateTime(int year, TimeZoneInfo.TransitionTime transitionTime)
		{
			DateTime timeOfDay = transitionTime.TimeOfDay;
			DateTime result;
			if (transitionTime.IsFixedDateRule)
			{
				int num = DateTime.DaysInMonth(year, transitionTime.Month);
				result = new DateTime(year, transitionTime.Month, (num < transitionTime.Day) ? num : transitionTime.Day, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
			}
			else if (transitionTime.Week <= 4)
			{
				result = new DateTime(year, transitionTime.Month, 1, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
				int dayOfWeek = (int)result.DayOfWeek;
				int num2 = transitionTime.DayOfWeek - (DayOfWeek)dayOfWeek;
				if (num2 < 0)
				{
					num2 += 7;
				}
				num2 += 7 * (transitionTime.Week - 1);
				if (num2 > 0)
				{
					result = result.AddDays((double)num2);
				}
			}
			else
			{
				int day = DateTime.DaysInMonth(year, transitionTime.Month);
				result = new DateTime(year, transitionTime.Month, day, timeOfDay.Hour, timeOfDay.Minute, timeOfDay.Second, timeOfDay.Millisecond);
				int num3 = result.DayOfWeek - transitionTime.DayOfWeek;
				if (num3 < 0)
				{
					num3 += 7;
				}
				if (num3 > 0)
				{
					result = result.AddDays((double)(-(double)num3));
				}
			}
			return result;
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001EE4C File Offset: 0x0001D04C
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZone(string id, bool dstDisabled, out TimeZoneInfo value, out Exception e, TimeZoneInfo.CachedData cachedData, bool alwaysFallbackToLocalMachine = false)
		{
			TimeZoneInfo.TimeZoneInfoResult result = TimeZoneInfo.TimeZoneInfoResult.Success;
			e = null;
			TimeZoneInfo timeZoneInfo = null;
			if (cachedData._systemTimeZones != null && cachedData._systemTimeZones.TryGetValue(id, out timeZoneInfo))
			{
				if (dstDisabled && timeZoneInfo._supportsDaylightSavingTime)
				{
					value = TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName);
				}
				else
				{
					value = new TimeZoneInfo(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName, timeZoneInfo._daylightDisplayName, timeZoneInfo._adjustmentRules, false);
				}
				return result;
			}
			if (!cachedData._allSystemTimeZonesRead || alwaysFallbackToLocalMachine)
			{
				result = TimeZoneInfo.TryGetTimeZoneFromLocalMachine(id, dstDisabled, out value, out e, cachedData);
			}
			else
			{
				result = TimeZoneInfo.TimeZoneInfoResult.TimeZoneNotFoundException;
				value = null;
			}
			return result;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0001EEF8 File Offset: 0x0001D0F8
		private static TimeZoneInfo.TimeZoneInfoResult TryGetTimeZoneFromLocalMachine(string id, bool dstDisabled, out TimeZoneInfo value, out Exception e, TimeZoneInfo.CachedData cachedData)
		{
			TimeZoneInfo timeZoneInfo;
			TimeZoneInfo.TimeZoneInfoResult timeZoneInfoResult = TimeZoneInfo.TryGetTimeZoneFromLocalMachine(id, out timeZoneInfo, out e);
			if (timeZoneInfoResult != TimeZoneInfo.TimeZoneInfoResult.Success)
			{
				value = null;
				return timeZoneInfoResult;
			}
			if (cachedData._systemTimeZones == null)
			{
				cachedData._systemTimeZones = new Dictionary<string, TimeZoneInfo>(StringComparer.OrdinalIgnoreCase);
			}
			if (!cachedData._systemTimeZones.ContainsKey(id))
			{
				cachedData._systemTimeZones.Add(id, timeZoneInfo);
			}
			if (dstDisabled && timeZoneInfo._supportsDaylightSavingTime)
			{
				value = TimeZoneInfo.CreateCustomTimeZone(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName);
				return timeZoneInfoResult;
			}
			value = new TimeZoneInfo(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName, timeZoneInfo._daylightDisplayName, timeZoneInfo._adjustmentRules, false);
			return timeZoneInfoResult;
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001EFA8 File Offset: 0x0001D1A8
		private static void ValidateTimeZoneInfo(string id, TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule[] adjustmentRules, out bool adjustmentRulesSupportDst)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id.Length == 0)
			{
				throw new ArgumentException(SR.Format("The specified ID parameter '{0}' is not supported.", id), "id");
			}
			if (TimeZoneInfo.UtcOffsetOutOfRange(baseUtcOffset))
			{
				throw new ArgumentOutOfRangeException("baseUtcOffset", "The TimeSpan parameter must be within plus or minus 14.0 hours.");
			}
			if (baseUtcOffset.Ticks % 600000000L != 0L)
			{
				throw new ArgumentException("The TimeSpan parameter cannot be specified more precisely than whole minutes.", "baseUtcOffset");
			}
			adjustmentRulesSupportDst = false;
			if (adjustmentRules != null && adjustmentRules.Length != 0)
			{
				adjustmentRulesSupportDst = true;
				TimeZoneInfo.AdjustmentRule adjustmentRule = null;
				for (int i = 0; i < adjustmentRules.Length; i++)
				{
					TimeZoneInfo.AdjustmentRule adjustmentRule2 = adjustmentRule;
					adjustmentRule = adjustmentRules[i];
					if (adjustmentRule == null)
					{
						throw new InvalidTimeZoneException("The AdjustmentRule array cannot contain null elements.");
					}
					if (!TimeZoneInfo.IsValidAdjustmentRuleOffest(baseUtcOffset, adjustmentRule))
					{
						throw new InvalidTimeZoneException("The sum of the BaseUtcOffset and DaylightDelta properties must within plus or minus 14.0 hours.");
					}
					if (adjustmentRule2 != null && adjustmentRule.DateStart <= adjustmentRule2.DateEnd)
					{
						throw new InvalidTimeZoneException("The elements of the AdjustmentRule array must be in chronological order and must not overlap.");
					}
				}
			}
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001F081 File Offset: 0x0001D281
		internal static bool UtcOffsetOutOfRange(TimeSpan offset)
		{
			return offset < TimeZoneInfo.MinOffset || offset > TimeZoneInfo.MaxOffset;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001F09D File Offset: 0x0001D29D
		private static TimeSpan GetUtcOffset(TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule adjustmentRule)
		{
			return baseUtcOffset + adjustmentRule.BaseUtcOffsetDelta + (adjustmentRule.HasDaylightSaving ? adjustmentRule.DaylightDelta : TimeSpan.Zero);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001F0C5 File Offset: 0x0001D2C5
		private static bool IsValidAdjustmentRuleOffest(TimeSpan baseUtcOffset, TimeZoneInfo.AdjustmentRule adjustmentRule)
		{
			return !TimeZoneInfo.UtcOffsetOutOfRange(TimeZoneInfo.GetUtcOffset(baseUtcOffset, adjustmentRule));
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001F0D8 File Offset: 0x0001D2D8
		private static void NormalizeAdjustmentRuleOffset(TimeSpan baseUtcOffset, ref TimeZoneInfo.AdjustmentRule adjustmentRule)
		{
			TimeSpan utcOffset = TimeZoneInfo.GetUtcOffset(baseUtcOffset, adjustmentRule);
			TimeSpan timeSpan = TimeSpan.Zero;
			if (utcOffset > TimeZoneInfo.MaxOffset)
			{
				timeSpan = TimeZoneInfo.MaxOffset - utcOffset;
			}
			else if (utcOffset < TimeZoneInfo.MinOffset)
			{
				timeSpan = TimeZoneInfo.MinOffset - utcOffset;
			}
			if (timeSpan != TimeSpan.Zero)
			{
				adjustmentRule = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(adjustmentRule.DateStart, adjustmentRule.DateEnd, adjustmentRule.DaylightDelta, adjustmentRule.DaylightTransitionStart, adjustmentRule.DaylightTransitionEnd, adjustmentRule.BaseUtcOffsetDelta + timeSpan, adjustmentRule.NoDaylightTransitions);
			}
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001F174 File Offset: 0x0001D374
		// Note: this type is marked as 'beforefieldinit'.
		static TimeZoneInfo()
		{
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000173AD File Offset: 0x000155AD
		internal TimeZoneInfo()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000FF5 RID: 4085
		private const string TimeZonesRegistryHive = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones";

		// Token: 0x04000FF6 RID: 4086
		private const string DisplayValue = "Display";

		// Token: 0x04000FF7 RID: 4087
		private const string DaylightValue = "Dlt";

		// Token: 0x04000FF8 RID: 4088
		private const string StandardValue = "Std";

		// Token: 0x04000FF9 RID: 4089
		private const string MuiDisplayValue = "MUI_Display";

		// Token: 0x04000FFA RID: 4090
		private const string MuiDaylightValue = "MUI_Dlt";

		// Token: 0x04000FFB RID: 4091
		private const string MuiStandardValue = "MUI_Std";

		// Token: 0x04000FFC RID: 4092
		private const string TimeZoneInfoValue = "TZI";

		// Token: 0x04000FFD RID: 4093
		private const string FirstEntryValue = "FirstEntry";

		// Token: 0x04000FFE RID: 4094
		private const string LastEntryValue = "LastEntry";

		// Token: 0x04000FFF RID: 4095
		private const int MaxKeyLength = 255;

		// Token: 0x04001000 RID: 4096
		private static Lazy<bool> lazyHaveRegistry = new Lazy<bool>(delegate()
		{
			bool result;
			try
			{
				using (Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\TimeZoneInformation", false))
				{
					result = true;
				}
			}
			catch
			{
				result = false;
			}
			return result;
		});

		// Token: 0x04001001 RID: 4097
		internal const uint TIME_ZONE_ID_INVALID = 4294967295U;

		// Token: 0x04001002 RID: 4098
		internal const uint ERROR_NO_MORE_ITEMS = 259U;

		// Token: 0x04001003 RID: 4099
		private readonly string _id;

		// Token: 0x04001004 RID: 4100
		private readonly string _displayName;

		// Token: 0x04001005 RID: 4101
		private readonly string _standardDisplayName;

		// Token: 0x04001006 RID: 4102
		private readonly string _daylightDisplayName;

		// Token: 0x04001007 RID: 4103
		private readonly TimeSpan _baseUtcOffset;

		// Token: 0x04001008 RID: 4104
		private readonly bool _supportsDaylightSavingTime;

		// Token: 0x04001009 RID: 4105
		private readonly TimeZoneInfo.AdjustmentRule[] _adjustmentRules;

		// Token: 0x0400100A RID: 4106
		private const string UtcId = "UTC";

		// Token: 0x0400100B RID: 4107
		private const string LocalId = "Local";

		// Token: 0x0400100C RID: 4108
		private static readonly TimeZoneInfo s_utcTimeZone = TimeZoneInfo.CreateCustomTimeZone("UTC", TimeSpan.Zero, "UTC", "UTC");

		// Token: 0x0400100D RID: 4109
		private static TimeZoneInfo.CachedData s_cachedData = new TimeZoneInfo.CachedData();

		// Token: 0x0400100E RID: 4110
		private static readonly DateTime s_maxDateOnly = new DateTime(9999, 12, 31);

		// Token: 0x0400100F RID: 4111
		private static readonly DateTime s_minDateOnly = new DateTime(1, 1, 2);

		// Token: 0x04001010 RID: 4112
		private static readonly TimeSpan MaxOffset = TimeSpan.FromHours(14.0);

		// Token: 0x04001011 RID: 4113
		private static readonly TimeSpan MinOffset = -TimeZoneInfo.MaxOffset;

		// Token: 0x020000D1 RID: 209
		private sealed class CachedData
		{
			// Token: 0x0600064E RID: 1614 RVA: 0x0001F208 File Offset: 0x0001D408
			private static TimeZoneInfo GetCurrentOneYearLocal()
			{
				Interop.Kernel32.TIME_ZONE_INFORMATION time_ZONE_INFORMATION;
				if (Interop.Kernel32.GetTimeZoneInformation(out time_ZONE_INFORMATION) != 4294967295U)
				{
					return TimeZoneInfo.GetLocalTimeZoneFromWin32Data(time_ZONE_INFORMATION, false);
				}
				return TimeZoneInfo.CreateCustomTimeZone("Local", TimeSpan.Zero, "Local", "Local");
			}

			// Token: 0x0600064F RID: 1615 RVA: 0x0001F244 File Offset: 0x0001D444
			public TimeZoneInfo.OffsetAndRule GetOneYearLocalFromUtc(int year)
			{
				TimeZoneInfo.OffsetAndRule offsetAndRule = this._oneYearLocalFromUtc;
				if (offsetAndRule == null || offsetAndRule.Year != year)
				{
					TimeZoneInfo currentOneYearLocal = TimeZoneInfo.CachedData.GetCurrentOneYearLocal();
					TimeZoneInfo.AdjustmentRule rule = (currentOneYearLocal._adjustmentRules == null) ? null : currentOneYearLocal._adjustmentRules[0];
					offsetAndRule = new TimeZoneInfo.OffsetAndRule(year, currentOneYearLocal.BaseUtcOffset, rule);
					this._oneYearLocalFromUtc = offsetAndRule;
				}
				return offsetAndRule;
			}

			// Token: 0x06000650 RID: 1616 RVA: 0x0001F298 File Offset: 0x0001D498
			private TimeZoneInfo CreateLocal()
			{
				TimeZoneInfo result;
				lock (this)
				{
					TimeZoneInfo timeZoneInfo = this._localTimeZone;
					if (timeZoneInfo == null)
					{
						timeZoneInfo = TimeZoneInfo.GetLocalTimeZone(this);
						timeZoneInfo = new TimeZoneInfo(timeZoneInfo._id, timeZoneInfo._baseUtcOffset, timeZoneInfo._displayName, timeZoneInfo._standardDisplayName, timeZoneInfo._daylightDisplayName, timeZoneInfo._adjustmentRules, false);
						this._localTimeZone = timeZoneInfo;
					}
					result = timeZoneInfo;
				}
				return result;
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x06000651 RID: 1617 RVA: 0x0001F318 File Offset: 0x0001D518
			public TimeZoneInfo Local
			{
				get
				{
					TimeZoneInfo timeZoneInfo = this._localTimeZone;
					if (timeZoneInfo == null)
					{
						timeZoneInfo = this.CreateLocal();
					}
					return timeZoneInfo;
				}
			}

			// Token: 0x06000652 RID: 1618 RVA: 0x0001F339 File Offset: 0x0001D539
			public DateTimeKind GetCorrespondingKind(TimeZoneInfo timeZone)
			{
				if (timeZone == TimeZoneInfo.s_utcTimeZone)
				{
					return DateTimeKind.Utc;
				}
				if (timeZone != this._localTimeZone)
				{
					return DateTimeKind.Unspecified;
				}
				return DateTimeKind.Local;
			}

			// Token: 0x06000653 RID: 1619 RVA: 0x0000259F File Offset: 0x0000079F
			public CachedData()
			{
			}

			// Token: 0x04001012 RID: 4114
			private volatile TimeZoneInfo.OffsetAndRule _oneYearLocalFromUtc;

			// Token: 0x04001013 RID: 4115
			private volatile TimeZoneInfo _localTimeZone;

			// Token: 0x04001014 RID: 4116
			public Dictionary<string, TimeZoneInfo> _systemTimeZones;

			// Token: 0x04001015 RID: 4117
			public ReadOnlyCollection<TimeZoneInfo> _readOnlySystemTimeZones;

			// Token: 0x04001016 RID: 4118
			public bool _allSystemTimeZonesRead;
		}

		// Token: 0x020000D2 RID: 210
		private sealed class OffsetAndRule
		{
			// Token: 0x06000654 RID: 1620 RVA: 0x0001F353 File Offset: 0x0001D553
			public OffsetAndRule(int year, TimeSpan offset, TimeZoneInfo.AdjustmentRule rule)
			{
				this.Year = year;
				this.Offset = offset;
				this.Rule = rule;
			}

			// Token: 0x04001017 RID: 4119
			public readonly int Year;

			// Token: 0x04001018 RID: 4120
			public readonly TimeSpan Offset;

			// Token: 0x04001019 RID: 4121
			public readonly TimeZoneInfo.AdjustmentRule Rule;
		}

		// Token: 0x020000D3 RID: 211
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct DYNAMIC_TIME_ZONE_INFORMATION
		{
			// Token: 0x0400101A RID: 4122
			internal Interop.Kernel32.TIME_ZONE_INFORMATION TZI;

			// Token: 0x0400101B RID: 4123
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string TimeZoneKeyName;

			// Token: 0x0400101C RID: 4124
			internal byte DynamicDaylightTimeDisabled;
		}

		// Token: 0x020000D4 RID: 212
		private enum TimeZoneInfoResult
		{
			// Token: 0x0400101E RID: 4126
			Success,
			// Token: 0x0400101F RID: 4127
			TimeZoneNotFoundException,
			// Token: 0x04001020 RID: 4128
			InvalidTimeZoneException,
			// Token: 0x04001021 RID: 4129
			SecurityException
		}

		/// <summary>Provides information about a time zone adjustment, such as the transition to and from daylight saving time.</summary>
		// Token: 0x020000D5 RID: 213
		[Serializable]
		public sealed class AdjustmentRule : IEquatable<TimeZoneInfo.AdjustmentRule>, ISerializable, IDeserializationCallback
		{
			/// <summary>Gets the date when the adjustment rule takes effect.</summary>
			/// <returns>A <see cref="T:System.DateTime" /> value that indicates when the adjustment rule takes effect.</returns>
			// Token: 0x1700008A RID: 138
			// (get) Token: 0x06000655 RID: 1621 RVA: 0x0001F370 File Offset: 0x0001D570
			public DateTime DateStart
			{
				get
				{
					return this._dateStart;
				}
			}

			/// <summary>Gets the date when the adjustment rule ceases to be in effect.</summary>
			/// <returns>A <see cref="T:System.DateTime" /> value that indicates the end date of the adjustment rule.</returns>
			// Token: 0x1700008B RID: 139
			// (get) Token: 0x06000656 RID: 1622 RVA: 0x0001F378 File Offset: 0x0001D578
			public DateTime DateEnd
			{
				get
				{
					return this._dateEnd;
				}
			}

			/// <summary>Gets the amount of time that is required to form the time zone's daylight saving time. This amount of time is added to the time zone's offset from Coordinated Universal Time (UTC).</summary>
			/// <returns>A <see cref="T:System.TimeSpan" /> object that indicates the amount of time to add to the standard time changes as a result of the adjustment rule.</returns>
			// Token: 0x1700008C RID: 140
			// (get) Token: 0x06000657 RID: 1623 RVA: 0x0001F380 File Offset: 0x0001D580
			public TimeSpan DaylightDelta
			{
				get
				{
					return this._daylightDelta;
				}
			}

			/// <summary>Gets information about the annual transition from standard time to daylight saving time.</summary>
			/// <returns>A <see cref="T:System.TimeZoneInfo.TransitionTime" /> object that defines the annual transition from a time zone's standard time to daylight saving time.</returns>
			// Token: 0x1700008D RID: 141
			// (get) Token: 0x06000658 RID: 1624 RVA: 0x0001F388 File Offset: 0x0001D588
			public TimeZoneInfo.TransitionTime DaylightTransitionStart
			{
				get
				{
					return this._daylightTransitionStart;
				}
			}

			/// <summary>Gets information about the annual transition from daylight saving time back to standard time.</summary>
			/// <returns>A <see cref="T:System.TimeZoneInfo.TransitionTime" /> object that defines the annual transition from daylight saving time back to the time zone's standard time.</returns>
			// Token: 0x1700008E RID: 142
			// (get) Token: 0x06000659 RID: 1625 RVA: 0x0001F390 File Offset: 0x0001D590
			public TimeZoneInfo.TransitionTime DaylightTransitionEnd
			{
				get
				{
					return this._daylightTransitionEnd;
				}
			}

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x0600065A RID: 1626 RVA: 0x0001F398 File Offset: 0x0001D598
			internal TimeSpan BaseUtcOffsetDelta
			{
				get
				{
					return this._baseUtcOffsetDelta;
				}
			}

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x0600065B RID: 1627 RVA: 0x0001F3A0 File Offset: 0x0001D5A0
			internal bool NoDaylightTransitions
			{
				get
				{
					return this._noDaylightTransitions;
				}
			}

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x0600065C RID: 1628 RVA: 0x0001F3A8 File Offset: 0x0001D5A8
			internal bool HasDaylightSaving
			{
				get
				{
					return this.DaylightDelta != TimeSpan.Zero || (this.DaylightTransitionStart != default(TimeZoneInfo.TransitionTime) && this.DaylightTransitionStart.TimeOfDay != DateTime.MinValue) || (this.DaylightTransitionEnd != default(TimeZoneInfo.TransitionTime) && this.DaylightTransitionEnd.TimeOfDay != DateTime.MinValue.AddMilliseconds(1.0));
				}
			}

			/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object is equal to a second <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object.</summary>
			/// <param name="other">The object to compare with the current object.</param>
			/// <returns>
			///   <see langword="true" /> if both <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> objects have equal values; otherwise, <see langword="false" />.</returns>
			// Token: 0x0600065D RID: 1629 RVA: 0x0001F438 File Offset: 0x0001D638
			public bool Equals(TimeZoneInfo.AdjustmentRule other)
			{
				return other != null && this._dateStart == other._dateStart && this._dateEnd == other._dateEnd && this._daylightDelta == other._daylightDelta && this._baseUtcOffsetDelta == other._baseUtcOffsetDelta && this._daylightTransitionEnd.Equals(other._daylightTransitionEnd) && this._daylightTransitionStart.Equals(other._daylightTransitionStart);
			}

			/// <summary>Serves as a hash function for hashing algorithms and data structures such as hash tables.</summary>
			/// <returns>A 32-bit signed integer that serves as the hash code for the current <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object.</returns>
			// Token: 0x0600065E RID: 1630 RVA: 0x0001F4BA File Offset: 0x0001D6BA
			public override int GetHashCode()
			{
				return this._dateStart.GetHashCode();
			}

			// Token: 0x0600065F RID: 1631 RVA: 0x0001F4C8 File Offset: 0x0001D6C8
			private AdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd, TimeSpan baseUtcOffsetDelta, bool noDaylightTransitions)
			{
				TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd, noDaylightTransitions);
				this._dateStart = dateStart;
				this._dateEnd = dateEnd;
				this._daylightDelta = daylightDelta;
				this._daylightTransitionStart = daylightTransitionStart;
				this._daylightTransitionEnd = daylightTransitionEnd;
				this._baseUtcOffsetDelta = baseUtcOffsetDelta;
				this._noDaylightTransitions = noDaylightTransitions;
			}

			/// <summary>Creates a new adjustment rule for a particular time zone.</summary>
			/// <param name="dateStart">The effective date of the adjustment rule. If the value of the <paramref name="dateStart" /> parameter is <see langword="DateTime.MinValue.Date" />, this is the first adjustment rule in effect for a time zone.</param>
			/// <param name="dateEnd">The last date that the adjustment rule is in force. If the value of the <paramref name="dateEnd" /> parameter is <see langword="DateTime.MaxValue.Date" />, the adjustment rule has no end date.</param>
			/// <param name="daylightDelta">The time change that results from the adjustment. This value is added to the time zone's <see cref="P:System.TimeZoneInfo.BaseUtcOffset" /> property to obtain the correct daylight offset from Coordinated Universal Time (UTC). This value can range from -14 to 14.</param>
			/// <param name="daylightTransitionStart">An object that defines the start of daylight saving time.</param>
			/// <param name="daylightTransitionEnd">An object that defines the end of daylight saving time.</param>
			/// <returns>An object that represents the new adjustment rule.</returns>
			/// <exception cref="T:System.ArgumentException">The <see cref="P:System.DateTime.Kind" /> property of the <paramref name="dateStart" /> or <paramref name="dateEnd" /> parameter does not equal <see cref="F:System.DateTimeKind.Unspecified" />.  
			///  -or-  
			///  The <paramref name="daylightTransitionStart" /> parameter is equal to the <paramref name="daylightTransitionEnd" /> parameter.  
			///  -or-  
			///  The <paramref name="dateStart" /> or <paramref name="dateEnd" /> parameter includes a time of day value.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="dateEnd" /> is earlier than <paramref name="dateStart" />.  
			/// -or-  
			/// <paramref name="daylightDelta" /> is less than -14 or greater than 14.  
			/// -or-  
			/// The <see cref="P:System.TimeSpan.Milliseconds" /> property of the <paramref name="daylightDelta" /> parameter is not equal to 0.  
			/// -or-  
			/// The <see cref="P:System.TimeSpan.Ticks" /> property of the <paramref name="daylightDelta" /> parameter does not equal a whole number of seconds.</exception>
			// Token: 0x06000660 RID: 1632 RVA: 0x0001F51E File Offset: 0x0001D71E
			public static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd)
			{
				return new TimeZoneInfo.AdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd, TimeSpan.Zero, false);
			}

			// Token: 0x06000661 RID: 1633 RVA: 0x0001F531 File Offset: 0x0001D731
			internal static TimeZoneInfo.AdjustmentRule CreateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd, TimeSpan baseUtcOffsetDelta, bool noDaylightTransitions)
			{
				return new TimeZoneInfo.AdjustmentRule(dateStart, dateEnd, daylightDelta, daylightTransitionStart, daylightTransitionEnd, baseUtcOffsetDelta, noDaylightTransitions);
			}

			// Token: 0x06000662 RID: 1634 RVA: 0x0001F544 File Offset: 0x0001D744
			internal bool IsStartDateMarkerForBeginningOfYear()
			{
				return !this.NoDaylightTransitions && this.DaylightTransitionStart.Month == 1 && this.DaylightTransitionStart.Day == 1 && this.DaylightTransitionStart.TimeOfDay.Hour == 0 && this.DaylightTransitionStart.TimeOfDay.Minute == 0 && this.DaylightTransitionStart.TimeOfDay.Second == 0 && this._dateStart.Year == this._dateEnd.Year;
			}

			// Token: 0x06000663 RID: 1635 RVA: 0x0001F5E0 File Offset: 0x0001D7E0
			internal bool IsEndDateMarkerForEndOfYear()
			{
				return !this.NoDaylightTransitions && this.DaylightTransitionEnd.Month == 1 && this.DaylightTransitionEnd.Day == 1 && this.DaylightTransitionEnd.TimeOfDay.Hour == 0 && this.DaylightTransitionEnd.TimeOfDay.Minute == 0 && this.DaylightTransitionEnd.TimeOfDay.Second == 0 && this._dateStart.Year == this._dateEnd.Year;
			}

			// Token: 0x06000664 RID: 1636 RVA: 0x0001F67C File Offset: 0x0001D87C
			private static void ValidateAdjustmentRule(DateTime dateStart, DateTime dateEnd, TimeSpan daylightDelta, TimeZoneInfo.TransitionTime daylightTransitionStart, TimeZoneInfo.TransitionTime daylightTransitionEnd, bool noDaylightTransitions)
			{
				if (dateStart.Kind != DateTimeKind.Unspecified && dateStart.Kind != DateTimeKind.Utc)
				{
					throw new ArgumentException("The supplied DateTime must have the Kind property set to DateTimeKind.Unspecified or DateTimeKind.Utc.", "dateStart");
				}
				if (dateEnd.Kind != DateTimeKind.Unspecified && dateEnd.Kind != DateTimeKind.Utc)
				{
					throw new ArgumentException("The supplied DateTime must have the Kind property set to DateTimeKind.Unspecified or DateTimeKind.Utc.", "dateEnd");
				}
				if (daylightTransitionStart.Equals(daylightTransitionEnd) && !noDaylightTransitions)
				{
					throw new ArgumentException("The DaylightTransitionStart property must not equal the DaylightTransitionEnd property.", "daylightTransitionEnd");
				}
				if (dateStart > dateEnd)
				{
					throw new ArgumentException("The DateStart property must come before the DateEnd property.", "dateStart");
				}
				if (daylightDelta.TotalHours < -23.0 || daylightDelta.TotalHours > 14.0)
				{
					throw new ArgumentOutOfRangeException("daylightDelta", daylightDelta, "The TimeSpan parameter must be within plus or minus 14.0 hours.");
				}
				if (daylightDelta.Ticks % 600000000L != 0L)
				{
					throw new ArgumentException("The TimeSpan parameter cannot be specified more precisely than whole minutes.", "daylightDelta");
				}
				if (dateStart != DateTime.MinValue && dateStart.Kind == DateTimeKind.Unspecified && dateStart.TimeOfDay != TimeSpan.Zero)
				{
					throw new ArgumentException("The supplied DateTime includes a TimeOfDay setting.   This is not supported.", "dateStart");
				}
				if (dateEnd != DateTime.MaxValue && dateEnd.Kind == DateTimeKind.Unspecified && dateEnd.TimeOfDay != TimeSpan.Zero)
				{
					throw new ArgumentException("The supplied DateTime includes a TimeOfDay setting.   This is not supported.", "dateEnd");
				}
			}

			/// <summary>Runs when the deserialization of a <see cref="T:System.TimeZoneInfo.AdjustmentRule" /> object is completed.</summary>
			/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
			// Token: 0x06000665 RID: 1637 RVA: 0x0001F7D4 File Offset: 0x0001D9D4
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				try
				{
					TimeZoneInfo.AdjustmentRule.ValidateAdjustmentRule(this._dateStart, this._dateEnd, this._daylightDelta, this._daylightTransitionStart, this._daylightTransitionEnd, this._noDaylightTransitions);
				}
				catch (ArgumentException innerException)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", innerException);
				}
			}

			/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data that is required to serialize this object.</summary>
			/// <param name="info">The object to populate with data.</param>
			/// <param name="context">The destination for this serialization (see <see cref="T:System.Runtime.Serialization.StreamingContext" />).</param>
			// Token: 0x06000666 RID: 1638 RVA: 0x0001F82C File Offset: 0x0001DA2C
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("DateStart", this._dateStart);
				info.AddValue("DateEnd", this._dateEnd);
				info.AddValue("DaylightDelta", this._daylightDelta);
				info.AddValue("DaylightTransitionStart", this._daylightTransitionStart);
				info.AddValue("DaylightTransitionEnd", this._daylightTransitionEnd);
				info.AddValue("BaseUtcOffsetDelta", this._baseUtcOffsetDelta);
				info.AddValue("NoDaylightTransitions", this._noDaylightTransitions);
			}

			// Token: 0x06000667 RID: 1639 RVA: 0x0001F8D4 File Offset: 0x0001DAD4
			private AdjustmentRule(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this._dateStart = (DateTime)info.GetValue("DateStart", typeof(DateTime));
				this._dateEnd = (DateTime)info.GetValue("DateEnd", typeof(DateTime));
				this._daylightDelta = (TimeSpan)info.GetValue("DaylightDelta", typeof(TimeSpan));
				this._daylightTransitionStart = (TimeZoneInfo.TransitionTime)info.GetValue("DaylightTransitionStart", typeof(TimeZoneInfo.TransitionTime));
				this._daylightTransitionEnd = (TimeZoneInfo.TransitionTime)info.GetValue("DaylightTransitionEnd", typeof(TimeZoneInfo.TransitionTime));
				object valueNoThrow = info.GetValueNoThrow("BaseUtcOffsetDelta", typeof(TimeSpan));
				if (valueNoThrow != null)
				{
					this._baseUtcOffsetDelta = (TimeSpan)valueNoThrow;
				}
				valueNoThrow = info.GetValueNoThrow("NoDaylightTransitions", typeof(bool));
				if (valueNoThrow != null)
				{
					this._noDaylightTransitions = (bool)valueNoThrow;
				}
			}

			// Token: 0x06000668 RID: 1640 RVA: 0x000173AD File Offset: 0x000155AD
			internal AdjustmentRule()
			{
				ThrowStub.ThrowNotSupportedException();
			}

			// Token: 0x04001022 RID: 4130
			private readonly DateTime _dateStart;

			// Token: 0x04001023 RID: 4131
			private readonly DateTime _dateEnd;

			// Token: 0x04001024 RID: 4132
			private readonly TimeSpan _daylightDelta;

			// Token: 0x04001025 RID: 4133
			private readonly TimeZoneInfo.TransitionTime _daylightTransitionStart;

			// Token: 0x04001026 RID: 4134
			private readonly TimeZoneInfo.TransitionTime _daylightTransitionEnd;

			// Token: 0x04001027 RID: 4135
			private readonly TimeSpan _baseUtcOffsetDelta;

			// Token: 0x04001028 RID: 4136
			private readonly bool _noDaylightTransitions;
		}

		// Token: 0x020000D6 RID: 214
		private struct StringSerializer
		{
			// Token: 0x06000669 RID: 1641 RVA: 0x0001F9E0 File Offset: 0x0001DBE0
			public static string GetSerializedString(TimeZoneInfo zone)
			{
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.Id, stringBuilder);
				stringBuilder.Append(';');
				stringBuilder.Append(zone.BaseUtcOffset.TotalMinutes.ToString(CultureInfo.InvariantCulture));
				stringBuilder.Append(';');
				TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.DisplayName, stringBuilder);
				stringBuilder.Append(';');
				TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.StandardName, stringBuilder);
				stringBuilder.Append(';');
				TimeZoneInfo.StringSerializer.SerializeSubstitute(zone.DaylightName, stringBuilder);
				stringBuilder.Append(';');
				foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in zone.GetAdjustmentRules())
				{
					stringBuilder.Append('[');
					stringBuilder.Append(adjustmentRule.DateStart.ToString("MM:dd:yyyy", DateTimeFormatInfo.InvariantInfo));
					stringBuilder.Append(';');
					stringBuilder.Append(adjustmentRule.DateEnd.ToString("MM:dd:yyyy", DateTimeFormatInfo.InvariantInfo));
					stringBuilder.Append(';');
					stringBuilder.Append(adjustmentRule.DaylightDelta.TotalMinutes.ToString(CultureInfo.InvariantCulture));
					stringBuilder.Append(';');
					TimeZoneInfo.StringSerializer.SerializeTransitionTime(adjustmentRule.DaylightTransitionStart, stringBuilder);
					stringBuilder.Append(';');
					TimeZoneInfo.StringSerializer.SerializeTransitionTime(adjustmentRule.DaylightTransitionEnd, stringBuilder);
					stringBuilder.Append(';');
					if (adjustmentRule.BaseUtcOffsetDelta != TimeSpan.Zero)
					{
						stringBuilder.Append(adjustmentRule.BaseUtcOffsetDelta.TotalMinutes.ToString(CultureInfo.InvariantCulture));
						stringBuilder.Append(';');
					}
					if (adjustmentRule.NoDaylightTransitions)
					{
						stringBuilder.Append('1');
						stringBuilder.Append(';');
					}
					stringBuilder.Append(']');
				}
				stringBuilder.Append(';');
				return StringBuilderCache.GetStringAndRelease(stringBuilder);
			}

			// Token: 0x0600066A RID: 1642 RVA: 0x0001FBC0 File Offset: 0x0001DDC0
			public static TimeZoneInfo GetDeserializedTimeZoneInfo(string source)
			{
				TimeZoneInfo.StringSerializer stringSerializer = new TimeZoneInfo.StringSerializer(source);
				string nextStringValue = stringSerializer.GetNextStringValue();
				TimeSpan nextTimeSpanValue = stringSerializer.GetNextTimeSpanValue();
				string nextStringValue2 = stringSerializer.GetNextStringValue();
				string nextStringValue3 = stringSerializer.GetNextStringValue();
				string nextStringValue4 = stringSerializer.GetNextStringValue();
				TimeZoneInfo.AdjustmentRule[] nextAdjustmentRuleArrayValue = stringSerializer.GetNextAdjustmentRuleArrayValue();
				TimeZoneInfo result;
				try
				{
					result = new TimeZoneInfo(nextStringValue, nextTimeSpanValue, nextStringValue2, nextStringValue3, nextStringValue4, nextAdjustmentRuleArrayValue, false);
				}
				catch (ArgumentException innerException)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", innerException);
				}
				catch (InvalidTimeZoneException innerException2)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", innerException2);
				}
				return result;
			}

			// Token: 0x0600066B RID: 1643 RVA: 0x0001FC58 File Offset: 0x0001DE58
			private StringSerializer(string str)
			{
				this._serializedText = str;
				this._currentTokenStartIndex = 0;
				this._state = TimeZoneInfo.StringSerializer.State.StartOfToken;
			}

			// Token: 0x0600066C RID: 1644 RVA: 0x0001FC70 File Offset: 0x0001DE70
			private static void SerializeSubstitute(string text, StringBuilder serializedText)
			{
				foreach (char c in text)
				{
					if (c == '\\' || c == '[' || c == ']' || c == ';')
					{
						serializedText.Append('\\');
					}
					serializedText.Append(c);
				}
			}

			// Token: 0x0600066D RID: 1645 RVA: 0x0001FCC0 File Offset: 0x0001DEC0
			private static void SerializeTransitionTime(TimeZoneInfo.TransitionTime time, StringBuilder serializedText)
			{
				serializedText.Append('[');
				serializedText.Append(time.IsFixedDateRule ? '1' : '0');
				serializedText.Append(';');
				serializedText.Append(time.TimeOfDay.ToString("HH:mm:ss.FFF", DateTimeFormatInfo.InvariantInfo));
				serializedText.Append(';');
				serializedText.Append(time.Month.ToString(CultureInfo.InvariantCulture));
				serializedText.Append(';');
				if (time.IsFixedDateRule)
				{
					serializedText.Append(time.Day.ToString(CultureInfo.InvariantCulture));
					serializedText.Append(';');
				}
				else
				{
					serializedText.Append(time.Week.ToString(CultureInfo.InvariantCulture));
					serializedText.Append(';');
					serializedText.Append(((int)time.DayOfWeek).ToString(CultureInfo.InvariantCulture));
					serializedText.Append(';');
				}
				serializedText.Append(']');
			}

			// Token: 0x0600066E RID: 1646 RVA: 0x0001FDC2 File Offset: 0x0001DFC2
			private static void VerifyIsEscapableCharacter(char c)
			{
				if (c != '\\' && c != ';' && c != '[' && c != ']')
				{
					throw new SerializationException(SR.Format("The serialized data contained an invalid escape sequence '\\\\{0}'.", c));
				}
			}

			// Token: 0x0600066F RID: 1647 RVA: 0x0001FDF0 File Offset: 0x0001DFF0
			private void SkipVersionNextDataFields(int depth)
			{
				if (this._currentTokenStartIndex < 0 || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				TimeZoneInfo.StringSerializer.State state = TimeZoneInfo.StringSerializer.State.NotEscaped;
				for (int i = this._currentTokenStartIndex; i < this._serializedText.Length; i++)
				{
					if (state == TimeZoneInfo.StringSerializer.State.Escaped)
					{
						TimeZoneInfo.StringSerializer.VerifyIsEscapableCharacter(this._serializedText[i]);
						state = TimeZoneInfo.StringSerializer.State.NotEscaped;
					}
					else if (state == TimeZoneInfo.StringSerializer.State.NotEscaped)
					{
						char c = this._serializedText[i];
						if (c == '\0')
						{
							throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
						}
						switch (c)
						{
						case '[':
							depth++;
							break;
						case '\\':
							state = TimeZoneInfo.StringSerializer.State.Escaped;
							break;
						case ']':
							depth--;
							if (depth == 0)
							{
								this._currentTokenStartIndex = i + 1;
								if (this._currentTokenStartIndex >= this._serializedText.Length)
								{
									this._state = TimeZoneInfo.StringSerializer.State.EndOfLine;
									return;
								}
								this._state = TimeZoneInfo.StringSerializer.State.StartOfToken;
								return;
							}
							break;
						}
					}
				}
				throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
			}

			// Token: 0x06000670 RID: 1648 RVA: 0x0001FEE0 File Offset: 0x0001E0E0
			private string GetNextStringValue()
			{
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (this._currentTokenStartIndex < 0 || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				TimeZoneInfo.StringSerializer.State state = TimeZoneInfo.StringSerializer.State.NotEscaped;
				StringBuilder stringBuilder = StringBuilderCache.Acquire(64);
				for (int i = this._currentTokenStartIndex; i < this._serializedText.Length; i++)
				{
					if (state == TimeZoneInfo.StringSerializer.State.Escaped)
					{
						TimeZoneInfo.StringSerializer.VerifyIsEscapableCharacter(this._serializedText[i]);
						stringBuilder.Append(this._serializedText[i]);
						state = TimeZoneInfo.StringSerializer.State.NotEscaped;
					}
					else if (state == TimeZoneInfo.StringSerializer.State.NotEscaped)
					{
						char c = this._serializedText[i];
						if (c == '\0')
						{
							throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
						}
						if (c == ';')
						{
							this._currentTokenStartIndex = i + 1;
							if (this._currentTokenStartIndex >= this._serializedText.Length)
							{
								this._state = TimeZoneInfo.StringSerializer.State.EndOfLine;
							}
							else
							{
								this._state = TimeZoneInfo.StringSerializer.State.StartOfToken;
							}
							return StringBuilderCache.GetStringAndRelease(stringBuilder);
						}
						switch (c)
						{
						case '[':
							throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
						case '\\':
							state = TimeZoneInfo.StringSerializer.State.Escaped;
							break;
						case ']':
							throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
						default:
							stringBuilder.Append(this._serializedText[i]);
							break;
						}
					}
				}
				if (state == TimeZoneInfo.StringSerializer.State.Escaped)
				{
					throw new SerializationException(SR.Format("The serialized data contained an invalid escape sequence '\\\\{0}'.", string.Empty));
				}
				throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
			}

			// Token: 0x06000671 RID: 1649 RVA: 0x00020044 File Offset: 0x0001E244
			private DateTime GetNextDateTimeValue(string format)
			{
				DateTime result;
				if (!DateTime.TryParseExact(this.GetNextStringValue(), format, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out result))
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				return result;
			}

			// Token: 0x06000672 RID: 1650 RVA: 0x00020074 File Offset: 0x0001E274
			private TimeSpan GetNextTimeSpanValue()
			{
				int nextInt32Value = this.GetNextInt32Value();
				TimeSpan result;
				try
				{
					result = new TimeSpan(0, nextInt32Value, 0);
				}
				catch (ArgumentOutOfRangeException innerException)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", innerException);
				}
				return result;
			}

			// Token: 0x06000673 RID: 1651 RVA: 0x000200B4 File Offset: 0x0001E2B4
			private int GetNextInt32Value()
			{
				int result;
				if (!int.TryParse(this.GetNextStringValue(), NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out result))
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				return result;
			}

			// Token: 0x06000674 RID: 1652 RVA: 0x000200E4 File Offset: 0x0001E2E4
			private TimeZoneInfo.AdjustmentRule[] GetNextAdjustmentRuleArrayValue()
			{
				List<TimeZoneInfo.AdjustmentRule> list = new List<TimeZoneInfo.AdjustmentRule>(1);
				int num = 0;
				for (TimeZoneInfo.AdjustmentRule nextAdjustmentRuleValue = this.GetNextAdjustmentRuleValue(); nextAdjustmentRuleValue != null; nextAdjustmentRuleValue = this.GetNextAdjustmentRuleValue())
				{
					list.Add(nextAdjustmentRuleValue);
					num++;
				}
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (this._currentTokenStartIndex < 0 || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (num == 0)
				{
					return null;
				}
				return list.ToArray();
			}

			// Token: 0x06000675 RID: 1653 RVA: 0x00020160 File Offset: 0x0001E360
			private TimeZoneInfo.AdjustmentRule GetNextAdjustmentRuleValue()
			{
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine)
				{
					return null;
				}
				if (this._currentTokenStartIndex < 0 || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (this._serializedText[this._currentTokenStartIndex] == ';')
				{
					return null;
				}
				if (this._serializedText[this._currentTokenStartIndex] != '[')
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				this._currentTokenStartIndex++;
				DateTime nextDateTimeValue = this.GetNextDateTimeValue("MM:dd:yyyy");
				DateTime nextDateTimeValue2 = this.GetNextDateTimeValue("MM:dd:yyyy");
				TimeSpan nextTimeSpanValue = this.GetNextTimeSpanValue();
				TimeZoneInfo.TransitionTime nextTransitionTimeValue = this.GetNextTransitionTimeValue();
				TimeZoneInfo.TransitionTime nextTransitionTimeValue2 = this.GetNextTransitionTimeValue();
				TimeSpan baseUtcOffsetDelta = TimeSpan.Zero;
				int num = 0;
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if ((this._serializedText[this._currentTokenStartIndex] >= '0' && this._serializedText[this._currentTokenStartIndex] <= '9') || this._serializedText[this._currentTokenStartIndex] == '-' || this._serializedText[this._currentTokenStartIndex] == '+')
				{
					baseUtcOffsetDelta = this.GetNextTimeSpanValue();
				}
				if (this._serializedText[this._currentTokenStartIndex] >= '0' && this._serializedText[this._currentTokenStartIndex] <= '1')
				{
					num = this.GetNextInt32Value();
				}
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (this._serializedText[this._currentTokenStartIndex] != ']')
				{
					this.SkipVersionNextDataFields(1);
				}
				else
				{
					this._currentTokenStartIndex++;
				}
				TimeZoneInfo.AdjustmentRule result;
				try
				{
					result = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(nextDateTimeValue, nextDateTimeValue2, nextTimeSpanValue, nextTransitionTimeValue, nextTransitionTimeValue2, baseUtcOffsetDelta, num > 0);
				}
				catch (ArgumentException innerException)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", innerException);
				}
				if (this._currentTokenStartIndex >= this._serializedText.Length)
				{
					this._state = TimeZoneInfo.StringSerializer.State.EndOfLine;
				}
				else
				{
					this._state = TimeZoneInfo.StringSerializer.State.StartOfToken;
				}
				return result;
			}

			// Token: 0x06000676 RID: 1654 RVA: 0x00020380 File Offset: 0x0001E580
			private TimeZoneInfo.TransitionTime GetNextTransitionTimeValue()
			{
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine || (this._currentTokenStartIndex < this._serializedText.Length && this._serializedText[this._currentTokenStartIndex] == ']'))
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (this._currentTokenStartIndex < 0 || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (this._serializedText[this._currentTokenStartIndex] != '[')
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				this._currentTokenStartIndex++;
				int nextInt32Value = this.GetNextInt32Value();
				if (nextInt32Value != 0 && nextInt32Value != 1)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				DateTime nextDateTimeValue = this.GetNextDateTimeValue("HH:mm:ss.FFF");
				nextDateTimeValue = new DateTime(1, 1, 1, nextDateTimeValue.Hour, nextDateTimeValue.Minute, nextDateTimeValue.Second, nextDateTimeValue.Millisecond);
				int nextInt32Value2 = this.GetNextInt32Value();
				TimeZoneInfo.TransitionTime result;
				if (nextInt32Value == 1)
				{
					int nextInt32Value3 = this.GetNextInt32Value();
					try
					{
						result = TimeZoneInfo.TransitionTime.CreateFixedDateRule(nextDateTimeValue, nextInt32Value2, nextInt32Value3);
						goto IL_137;
					}
					catch (ArgumentException innerException)
					{
						throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", innerException);
					}
				}
				int nextInt32Value4 = this.GetNextInt32Value();
				int nextInt32Value5 = this.GetNextInt32Value();
				try
				{
					result = TimeZoneInfo.TransitionTime.CreateFloatingDateRule(nextDateTimeValue, nextInt32Value2, nextInt32Value4, (DayOfWeek)nextInt32Value5);
				}
				catch (ArgumentException innerException2)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", innerException2);
				}
				IL_137:
				if (this._state == TimeZoneInfo.StringSerializer.State.EndOfLine || this._currentTokenStartIndex >= this._serializedText.Length)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (this._serializedText[this._currentTokenStartIndex] != ']')
				{
					this.SkipVersionNextDataFields(1);
				}
				else
				{
					this._currentTokenStartIndex++;
				}
				bool flag = false;
				if (this._currentTokenStartIndex < this._serializedText.Length && this._serializedText[this._currentTokenStartIndex] == ';')
				{
					this._currentTokenStartIndex++;
					flag = true;
				}
				if (!flag)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.");
				}
				if (this._currentTokenStartIndex >= this._serializedText.Length)
				{
					this._state = TimeZoneInfo.StringSerializer.State.EndOfLine;
				}
				else
				{
					this._state = TimeZoneInfo.StringSerializer.State.StartOfToken;
				}
				return result;
			}

			// Token: 0x04001029 RID: 4137
			private readonly string _serializedText;

			// Token: 0x0400102A RID: 4138
			private int _currentTokenStartIndex;

			// Token: 0x0400102B RID: 4139
			private TimeZoneInfo.StringSerializer.State _state;

			// Token: 0x0400102C RID: 4140
			private const int InitialCapacityForString = 64;

			// Token: 0x0400102D RID: 4141
			private const char Esc = '\\';

			// Token: 0x0400102E RID: 4142
			private const char Sep = ';';

			// Token: 0x0400102F RID: 4143
			private const char Lhs = '[';

			// Token: 0x04001030 RID: 4144
			private const char Rhs = ']';

			// Token: 0x04001031 RID: 4145
			private const string DateTimeFormat = "MM:dd:yyyy";

			// Token: 0x04001032 RID: 4146
			private const string TimeOfDayFormat = "HH:mm:ss.FFF";

			// Token: 0x020000D7 RID: 215
			private enum State
			{
				// Token: 0x04001034 RID: 4148
				Escaped,
				// Token: 0x04001035 RID: 4149
				NotEscaped,
				// Token: 0x04001036 RID: 4150
				StartOfToken,
				// Token: 0x04001037 RID: 4151
				EndOfLine
			}
		}

		/// <summary>Provides information about a specific time change, such as the change from daylight saving time to standard time or vice versa, in a particular time zone.</summary>
		// Token: 0x020000D8 RID: 216
		[Serializable]
		public readonly struct TransitionTime : IEquatable<TimeZoneInfo.TransitionTime>, ISerializable, IDeserializationCallback
		{
			/// <summary>Gets the hour, minute, and second at which the time change occurs.</summary>
			/// <returns>The time of day at which the time change occurs.</returns>
			// Token: 0x17000092 RID: 146
			// (get) Token: 0x06000677 RID: 1655 RVA: 0x000205A4 File Offset: 0x0001E7A4
			public DateTime TimeOfDay
			{
				get
				{
					return this._timeOfDay;
				}
			}

			/// <summary>Gets the month in which the time change occurs.</summary>
			/// <returns>The month in which the time change occurs.</returns>
			// Token: 0x17000093 RID: 147
			// (get) Token: 0x06000678 RID: 1656 RVA: 0x000205AC File Offset: 0x0001E7AC
			public int Month
			{
				get
				{
					return (int)this._month;
				}
			}

			/// <summary>Gets the week of the month in which a time change occurs.</summary>
			/// <returns>The week of the month in which the time change occurs.</returns>
			// Token: 0x17000094 RID: 148
			// (get) Token: 0x06000679 RID: 1657 RVA: 0x000205B4 File Offset: 0x0001E7B4
			public int Week
			{
				get
				{
					return (int)this._week;
				}
			}

			/// <summary>Gets the day on which the time change occurs.</summary>
			/// <returns>The day on which the time change occurs.</returns>
			// Token: 0x17000095 RID: 149
			// (get) Token: 0x0600067A RID: 1658 RVA: 0x000205BC File Offset: 0x0001E7BC
			public int Day
			{
				get
				{
					return (int)this._day;
				}
			}

			/// <summary>Gets the day of the week on which the time change occurs.</summary>
			/// <returns>The day of the week on which the time change occurs.</returns>
			// Token: 0x17000096 RID: 150
			// (get) Token: 0x0600067B RID: 1659 RVA: 0x000205C4 File Offset: 0x0001E7C4
			public DayOfWeek DayOfWeek
			{
				get
				{
					return this._dayOfWeek;
				}
			}

			/// <summary>Gets a value indicating whether the time change occurs at a fixed date and time (such as November 1) or a floating date and time (such as the last Sunday of October).</summary>
			/// <returns>
			///   <see langword="true" /> if the time change rule is fixed-date; <see langword="false" /> if the time change rule is floating-date.</returns>
			// Token: 0x17000097 RID: 151
			// (get) Token: 0x0600067C RID: 1660 RVA: 0x000205CC File Offset: 0x0001E7CC
			public bool IsFixedDateRule
			{
				get
				{
					return this._isFixedDateRule;
				}
			}

			/// <summary>Determines whether an object has identical values to the current <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</summary>
			/// <param name="obj">An object to compare with the current <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</param>
			/// <returns>
			///   <see langword="true" /> if the two objects are equal; otherwise, <see langword="false" />.</returns>
			// Token: 0x0600067D RID: 1661 RVA: 0x000205D4 File Offset: 0x0001E7D4
			public override bool Equals(object obj)
			{
				return obj is TimeZoneInfo.TransitionTime && this.Equals((TimeZoneInfo.TransitionTime)obj);
			}

			/// <summary>Determines whether two specified <see cref="T:System.TimeZoneInfo.TransitionTime" /> objects are equal.</summary>
			/// <param name="t1">The first object to compare.</param>
			/// <param name="t2">The second object to compare.</param>
			/// <returns>
			///   <see langword="true" /> if <paramref name="t1" /> and <paramref name="t2" /> have identical values; otherwise, <see langword="false" />.</returns>
			// Token: 0x0600067E RID: 1662 RVA: 0x000205EC File Offset: 0x0001E7EC
			public static bool operator ==(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return t1.Equals(t2);
			}

			/// <summary>Determines whether two specified <see cref="T:System.TimeZoneInfo.TransitionTime" /> objects are not equal.</summary>
			/// <param name="t1">The first object to compare.</param>
			/// <param name="t2">The second object to compare.</param>
			/// <returns>
			///   <see langword="true" /> if <paramref name="t1" /> and <paramref name="t2" /> have any different member values; otherwise, <see langword="false" />.</returns>
			// Token: 0x0600067F RID: 1663 RVA: 0x000205F6 File Offset: 0x0001E7F6
			public static bool operator !=(TimeZoneInfo.TransitionTime t1, TimeZoneInfo.TransitionTime t2)
			{
				return !t1.Equals(t2);
			}

			/// <summary>Determines whether the current <see cref="T:System.TimeZoneInfo.TransitionTime" /> object has identical values to a second <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</summary>
			/// <param name="other">An object to compare to the current instance.</param>
			/// <returns>
			///   <see langword="true" /> if the two objects have identical property values; otherwise, <see langword="false" />.</returns>
			// Token: 0x06000680 RID: 1664 RVA: 0x00020604 File Offset: 0x0001E804
			public bool Equals(TimeZoneInfo.TransitionTime other)
			{
				if (this._isFixedDateRule != other._isFixedDateRule || !(this._timeOfDay == other._timeOfDay) || this._month != other._month)
				{
					return false;
				}
				if (!other._isFixedDateRule)
				{
					return this._week == other._week && this._dayOfWeek == other._dayOfWeek;
				}
				return this._day == other._day;
			}

			/// <summary>Serves as a hash function for hashing algorithms and data structures such as hash tables.</summary>
			/// <returns>A 32-bit signed integer that serves as the hash code for this <see cref="T:System.TimeZoneInfo.TransitionTime" /> object.</returns>
			// Token: 0x06000681 RID: 1665 RVA: 0x00020677 File Offset: 0x0001E877
			public override int GetHashCode()
			{
				return (int)this._month ^ (int)this._week << 8;
			}

			// Token: 0x06000682 RID: 1666 RVA: 0x00020688 File Offset: 0x0001E888
			private TransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek, bool isFixedDateRule)
			{
				TimeZoneInfo.TransitionTime.ValidateTransitionTime(timeOfDay, month, week, day, dayOfWeek);
				this._timeOfDay = timeOfDay;
				this._month = (byte)month;
				this._week = (byte)week;
				this._day = (byte)day;
				this._dayOfWeek = dayOfWeek;
				this._isFixedDateRule = isFixedDateRule;
			}

			/// <summary>Defines a time change that uses a fixed-date rule (that is, a time change that occurs on a specific day of a specific month).</summary>
			/// <param name="timeOfDay">The time at which the time change occurs. This parameter corresponds to the <see cref="P:System.TimeZoneInfo.TransitionTime.TimeOfDay" /> property.</param>
			/// <param name="month">The month in which the time change occurs. This parameter corresponds to the <see cref="P:System.TimeZoneInfo.TransitionTime.Month" /> property.</param>
			/// <param name="day">The day of the month on which the time change occurs. This parameter corresponds to the <see cref="P:System.TimeZoneInfo.TransitionTime.Day" /> property.</param>
			/// <returns>Data about the time change.</returns>
			/// <exception cref="T:System.ArgumentException">The <paramref name="timeOfDay" /> parameter has a non-default date component.  
			///  -or-  
			///  The <paramref name="timeOfDay" /> parameter's <see cref="P:System.DateTime.Kind" /> property is not <see cref="F:System.DateTimeKind.Unspecified" />.  
			///  -or-  
			///  The <paramref name="timeOfDay" /> parameter does not represent a whole number of milliseconds.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="month" /> parameter is less than 1 or greater than 12.  
			///  -or-  
			///  The <paramref name="day" /> parameter is less than 1 or greater than 31.</exception>
			// Token: 0x06000683 RID: 1667 RVA: 0x000206C6 File Offset: 0x0001E8C6
			public static TimeZoneInfo.TransitionTime CreateFixedDateRule(DateTime timeOfDay, int month, int day)
			{
				return new TimeZoneInfo.TransitionTime(timeOfDay, month, 1, day, DayOfWeek.Sunday, true);
			}

			/// <summary>Defines a time change that uses a floating-date rule (that is, a time change that occurs on a specific day of a specific week of a specific month).</summary>
			/// <param name="timeOfDay">The time at which the time change occurs. This parameter corresponds to the <see cref="P:System.TimeZoneInfo.TransitionTime.TimeOfDay" /> property.</param>
			/// <param name="month">The month in which the time change occurs. This parameter corresponds to the <see cref="P:System.TimeZoneInfo.TransitionTime.Month" /> property.</param>
			/// <param name="week">The week of the month in which the time change occurs. Its value can range from 1 to 5, with 5 representing the last week of the month. This parameter corresponds to the <see cref="P:System.TimeZoneInfo.TransitionTime.Week" /> property.</param>
			/// <param name="dayOfWeek">The day of the week on which the time change occurs. This parameter corresponds to the <see cref="P:System.TimeZoneInfo.TransitionTime.DayOfWeek" /> property.</param>
			/// <returns>Data about the time change.</returns>
			/// <exception cref="T:System.ArgumentException">The <paramref name="timeOfDay" /> parameter has a non-default date component.  
			///  -or-  
			///  The <paramref name="timeOfDay" /> parameter does not represent a whole number of milliseconds.  
			///  -or-  
			///  The <paramref name="timeOfDay" /> parameter's <see cref="P:System.DateTime.Kind" /> property is not <see cref="F:System.DateTimeKind.Unspecified" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="month" /> is less than 1 or greater than 12.  
			/// -or-  
			/// <paramref name="week" /> is less than 1 or greater than 5.  
			/// -or-  
			/// The <paramref name="dayOfWeek" /> parameter is not a member of the <see cref="T:System.DayOfWeek" /> enumeration.</exception>
			// Token: 0x06000684 RID: 1668 RVA: 0x000206D3 File Offset: 0x0001E8D3
			public static TimeZoneInfo.TransitionTime CreateFloatingDateRule(DateTime timeOfDay, int month, int week, DayOfWeek dayOfWeek)
			{
				return new TimeZoneInfo.TransitionTime(timeOfDay, month, week, 1, dayOfWeek, false);
			}

			// Token: 0x06000685 RID: 1669 RVA: 0x000206E0 File Offset: 0x0001E8E0
			private static void ValidateTransitionTime(DateTime timeOfDay, int month, int week, int day, DayOfWeek dayOfWeek)
			{
				if (timeOfDay.Kind != DateTimeKind.Unspecified)
				{
					throw new ArgumentException("The supplied DateTime must have the Kind property set to DateTimeKind.Unspecified.", "timeOfDay");
				}
				if (month < 1 || month > 12)
				{
					throw new ArgumentOutOfRangeException("month", "The Month parameter must be in the range 1 through 12.");
				}
				if (day < 1 || day > 31)
				{
					throw new ArgumentOutOfRangeException("day", "The Day parameter must be in the range 1 through 31.");
				}
				if (week < 1 || week > 5)
				{
					throw new ArgumentOutOfRangeException("week", "The Week parameter must be in the range 1 through 5.");
				}
				if (dayOfWeek < DayOfWeek.Sunday || dayOfWeek > DayOfWeek.Saturday)
				{
					throw new ArgumentOutOfRangeException("dayOfWeek", "The DayOfWeek enumeration must be in the range 0 through 6.");
				}
				int num;
				int num2;
				int num3;
				timeOfDay.GetDatePart(out num, out num2, out num3);
				if (num != 1 || num2 != 1 || num3 != 1 || timeOfDay.Ticks % 10000L != 0L)
				{
					throw new ArgumentException("The supplied DateTime must have the Year, Month, and Day properties set to 1.  The time cannot be specified more precisely than whole milliseconds.", "timeOfDay");
				}
			}

			/// <summary>Runs when the deserialization of an object has been completed.</summary>
			/// <param name="sender">The object that initiated the callback. The functionality for this parameter is not currently implemented.</param>
			// Token: 0x06000686 RID: 1670 RVA: 0x000207A4 File Offset: 0x0001E9A4
			void IDeserializationCallback.OnDeserialization(object sender)
			{
				try
				{
					TimeZoneInfo.TransitionTime.ValidateTransitionTime(this._timeOfDay, (int)this._month, (int)this._week, (int)this._day, this._dayOfWeek);
				}
				catch (ArgumentException innerException)
				{
					throw new SerializationException("An error occurred while deserializing the object.  The serialized data is corrupt.", innerException);
				}
			}

			/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the data that is required to serialize this object.</summary>
			/// <param name="info">The object to populate with data.</param>
			/// <param name="context">The destination for this serialization (see <see cref="T:System.Runtime.Serialization.StreamingContext" />).</param>
			// Token: 0x06000687 RID: 1671 RVA: 0x000207F4 File Offset: 0x0001E9F4
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("TimeOfDay", this._timeOfDay);
				info.AddValue("Month", this._month);
				info.AddValue("Week", this._week);
				info.AddValue("Day", this._day);
				info.AddValue("DayOfWeek", this._dayOfWeek);
				info.AddValue("IsFixedDateRule", this._isFixedDateRule);
			}

			// Token: 0x06000688 RID: 1672 RVA: 0x0002087C File Offset: 0x0001EA7C
			private TransitionTime(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this._timeOfDay = (DateTime)info.GetValue("TimeOfDay", typeof(DateTime));
				this._month = (byte)info.GetValue("Month", typeof(byte));
				this._week = (byte)info.GetValue("Week", typeof(byte));
				this._day = (byte)info.GetValue("Day", typeof(byte));
				this._dayOfWeek = (DayOfWeek)info.GetValue("DayOfWeek", typeof(DayOfWeek));
				this._isFixedDateRule = (bool)info.GetValue("IsFixedDateRule", typeof(bool));
			}

			// Token: 0x04001038 RID: 4152
			private readonly DateTime _timeOfDay;

			// Token: 0x04001039 RID: 4153
			private readonly byte _month;

			// Token: 0x0400103A RID: 4154
			private readonly byte _week;

			// Token: 0x0400103B RID: 4155
			private readonly byte _day;

			// Token: 0x0400103C RID: 4156
			private readonly DayOfWeek _dayOfWeek;

			// Token: 0x0400103D RID: 4157
			private readonly bool _isFixedDateRule;
		}

		// Token: 0x020000D9 RID: 217
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000689 RID: 1673 RVA: 0x00020957 File Offset: 0x0001EB57
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600068A RID: 1674 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x0600068B RID: 1675 RVA: 0x00020964 File Offset: 0x0001EB64
			internal int <GetSystemTimeZonesWinRTFallback>b__49_0(TimeZoneInfo x, TimeZoneInfo y)
			{
				int num = x.BaseUtcOffset.CompareTo(y.BaseUtcOffset);
				if (num != 0)
				{
					return num;
				}
				return string.CompareOrdinal(x.DisplayName, y.DisplayName);
			}

			// Token: 0x0600068C RID: 1676 RVA: 0x0002099C File Offset: 0x0001EB9C
			internal int <GetSystemTimeZones>b__110_0(TimeZoneInfo x, TimeZoneInfo y)
			{
				int num = x.BaseUtcOffset.CompareTo(y.BaseUtcOffset);
				if (num != 0)
				{
					return num;
				}
				return string.CompareOrdinal(x.DisplayName, y.DisplayName);
			}

			// Token: 0x0600068D RID: 1677 RVA: 0x000209D4 File Offset: 0x0001EBD4
			internal bool <.cctor>b__157_0()
			{
				bool result;
				try
				{
					using (Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\TimeZoneInformation", false))
					{
						result = true;
					}
				}
				catch
				{
					result = false;
				}
				return result;
			}

			// Token: 0x0400103E RID: 4158
			public static readonly TimeZoneInfo.<>c <>9 = new TimeZoneInfo.<>c();

			// Token: 0x0400103F RID: 4159
			public static Comparison<TimeZoneInfo> <>9__49_0;

			// Token: 0x04001040 RID: 4160
			public static Comparison<TimeZoneInfo> <>9__110_0;
		}
	}
}
