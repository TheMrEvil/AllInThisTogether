using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	internal static class RemoteConfigSettingsHelper
	{
		// Token: 0x0600004A RID: 74
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSafeMap(IntPtr m, string key);

		// Token: 0x0600004B RID: 75
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string[] GetSafeMapKeys(IntPtr m);

		// Token: 0x0600004C RID: 76
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RemoteConfigSettingsHelper.Tag[] GetSafeMapTypes(IntPtr m);

		// Token: 0x0600004D RID: 77
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetSafeNumber(IntPtr m, string key, long defaultValue);

		// Token: 0x0600004E RID: 78
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float GetSafeFloat(IntPtr m, string key, float defaultValue);

		// Token: 0x0600004F RID: 79
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetSafeBool(IntPtr m, string key, bool defaultValue);

		// Token: 0x06000050 RID: 80
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetSafeStringValue(IntPtr m, string key, string defaultValue);

		// Token: 0x06000051 RID: 81
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSafeArray(IntPtr m, string key);

		// Token: 0x06000052 RID: 82
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetSafeArraySize(IntPtr a);

		// Token: 0x06000053 RID: 83
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSafeArrayArray(IntPtr a, long i);

		// Token: 0x06000054 RID: 84
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSafeArrayMap(IntPtr a, long i);

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RemoteConfigSettingsHelper.Tag GetSafeArrayType(IntPtr a, long i);

		// Token: 0x06000056 RID: 86
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern long GetSafeNumberArray(IntPtr a, long i);

		// Token: 0x06000057 RID: 87
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern float GetSafeArrayFloat(IntPtr a, long i);

		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool GetSafeArrayBool(IntPtr a, long i);

		// Token: 0x06000059 RID: 89
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetSafeArrayStringValue(IntPtr a, long i);

		// Token: 0x0600005A RID: 90 RVA: 0x000026C4 File Offset: 0x000008C4
		public static IDictionary<string, object> GetDictionary(IntPtr m, string key)
		{
			bool flag = m == IntPtr.Zero;
			IDictionary<string, object> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				bool flag2 = !string.IsNullOrEmpty(key);
				if (flag2)
				{
					m = RemoteConfigSettingsHelper.GetSafeMap(m, key);
					bool flag3 = m == IntPtr.Zero;
					if (flag3)
					{
						return null;
					}
				}
				result = RemoteConfigSettingsHelper.GetDictionary(m);
			}
			return result;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000271C File Offset: 0x0000091C
		internal static IDictionary<string, object> GetDictionary(IntPtr m)
		{
			bool flag = m == IntPtr.Zero;
			IDictionary<string, object> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				RemoteConfigSettingsHelper.Tag[] safeMapTypes = RemoteConfigSettingsHelper.GetSafeMapTypes(m);
				string[] safeMapKeys = RemoteConfigSettingsHelper.GetSafeMapKeys(m);
				for (int i = 0; i < safeMapKeys.Length; i++)
				{
					RemoteConfigSettingsHelper.SetDictKeyType(m, dictionary, safeMapKeys[i], safeMapTypes[i]);
				}
				result = dictionary;
			}
			return result;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002784 File Offset: 0x00000984
		internal static object GetArrayArrayEntries(IntPtr a, long i)
		{
			return RemoteConfigSettingsHelper.GetArrayEntries(RemoteConfigSettingsHelper.GetSafeArrayArray(a, i));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000027A4 File Offset: 0x000009A4
		internal static IDictionary<string, object> GetArrayMapEntries(IntPtr a, long i)
		{
			return RemoteConfigSettingsHelper.GetDictionary(RemoteConfigSettingsHelper.GetSafeArrayMap(a, i));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000027C4 File Offset: 0x000009C4
		internal static T[] GetArrayEntriesType<T>(IntPtr a, long size, Func<IntPtr, long, T> f)
		{
			T[] array = new T[size];
			for (long num = 0L; num < size; num += 1L)
			{
				array[(int)(checked((IntPtr)num))] = f(a, num);
			}
			return array;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002804 File Offset: 0x00000A04
		internal static object GetArrayEntries(IntPtr a)
		{
			long safeArraySize = RemoteConfigSettingsHelper.GetSafeArraySize(a);
			bool flag = safeArraySize == 0L;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				switch (RemoteConfigSettingsHelper.GetSafeArrayType(a, 0L))
				{
				case RemoteConfigSettingsHelper.Tag.kIntVal:
				case RemoteConfigSettingsHelper.Tag.kInt64Val:
					return RemoteConfigSettingsHelper.GetArrayEntriesType<long>(a, safeArraySize, new Func<IntPtr, long, long>(RemoteConfigSettingsHelper.GetSafeNumberArray));
				case RemoteConfigSettingsHelper.Tag.kDoubleVal:
					return RemoteConfigSettingsHelper.GetArrayEntriesType<float>(a, safeArraySize, new Func<IntPtr, long, float>(RemoteConfigSettingsHelper.GetSafeArrayFloat));
				case RemoteConfigSettingsHelper.Tag.kBoolVal:
					return RemoteConfigSettingsHelper.GetArrayEntriesType<bool>(a, safeArraySize, new Func<IntPtr, long, bool>(RemoteConfigSettingsHelper.GetSafeArrayBool));
				case RemoteConfigSettingsHelper.Tag.kStringVal:
					return RemoteConfigSettingsHelper.GetArrayEntriesType<string>(a, safeArraySize, new Func<IntPtr, long, string>(RemoteConfigSettingsHelper.GetSafeArrayStringValue));
				case RemoteConfigSettingsHelper.Tag.kArrayVal:
					return RemoteConfigSettingsHelper.GetArrayEntriesType<object>(a, safeArraySize, new Func<IntPtr, long, object>(RemoteConfigSettingsHelper.GetArrayArrayEntries));
				case RemoteConfigSettingsHelper.Tag.kMapVal:
					return RemoteConfigSettingsHelper.GetArrayEntriesType<IDictionary<string, object>>(a, safeArraySize, new Func<IntPtr, long, IDictionary<string, object>>(RemoteConfigSettingsHelper.GetArrayMapEntries));
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000028F0 File Offset: 0x00000AF0
		internal static object GetMixedArrayEntries(IntPtr a)
		{
			long safeArraySize = RemoteConfigSettingsHelper.GetSafeArraySize(a);
			bool flag = safeArraySize == 0L;
			object result;
			if (flag)
			{
				result = null;
			}
			else
			{
				object[] array = new object[safeArraySize];
				for (long num = 0L; num < safeArraySize; num += 1L)
				{
					checked
					{
						switch (RemoteConfigSettingsHelper.GetSafeArrayType(a, num))
						{
						case RemoteConfigSettingsHelper.Tag.kIntVal:
						case RemoteConfigSettingsHelper.Tag.kInt64Val:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetSafeNumberArray(a, num);
							break;
						case RemoteConfigSettingsHelper.Tag.kDoubleVal:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetSafeArrayFloat(a, num);
							break;
						case RemoteConfigSettingsHelper.Tag.kBoolVal:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetSafeArrayBool(a, num);
							break;
						case RemoteConfigSettingsHelper.Tag.kStringVal:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetSafeArrayStringValue(a, num);
							break;
						case RemoteConfigSettingsHelper.Tag.kArrayVal:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetArrayArrayEntries(a, num);
							break;
						case RemoteConfigSettingsHelper.Tag.kMapVal:
							array[(int)((IntPtr)num)] = RemoteConfigSettingsHelper.GetArrayMapEntries(a, num);
							break;
						}
					}
				}
				result = array;
			}
			return result;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000029EC File Offset: 0x00000BEC
		internal static void SetDictKeyType(IntPtr m, IDictionary<string, object> dict, string key, RemoteConfigSettingsHelper.Tag tag)
		{
			switch (tag)
			{
			case RemoteConfigSettingsHelper.Tag.kIntVal:
			case RemoteConfigSettingsHelper.Tag.kInt64Val:
				dict[key] = RemoteConfigSettingsHelper.GetSafeNumber(m, key, 0L);
				break;
			case RemoteConfigSettingsHelper.Tag.kDoubleVal:
				dict[key] = RemoteConfigSettingsHelper.GetSafeFloat(m, key, 0f);
				break;
			case RemoteConfigSettingsHelper.Tag.kBoolVal:
				dict[key] = RemoteConfigSettingsHelper.GetSafeBool(m, key, false);
				break;
			case RemoteConfigSettingsHelper.Tag.kStringVal:
				dict[key] = RemoteConfigSettingsHelper.GetSafeStringValue(m, key, "");
				break;
			case RemoteConfigSettingsHelper.Tag.kArrayVal:
				dict[key] = RemoteConfigSettingsHelper.GetArrayEntries(RemoteConfigSettingsHelper.GetSafeArray(m, key));
				break;
			case RemoteConfigSettingsHelper.Tag.kMixedArrayVal:
				dict[key] = RemoteConfigSettingsHelper.GetMixedArrayEntries(RemoteConfigSettingsHelper.GetSafeArray(m, key));
				break;
			case RemoteConfigSettingsHelper.Tag.kMapVal:
				dict[key] = RemoteConfigSettingsHelper.GetDictionary(RemoteConfigSettingsHelper.GetSafeMap(m, key));
				break;
			}
		}

		// Token: 0x02000006 RID: 6
		[RequiredByNativeCode]
		internal enum Tag
		{
			// Token: 0x04000007 RID: 7
			kUnknown,
			// Token: 0x04000008 RID: 8
			kIntVal,
			// Token: 0x04000009 RID: 9
			kInt64Val,
			// Token: 0x0400000A RID: 10
			kUInt64Val,
			// Token: 0x0400000B RID: 11
			kDoubleVal,
			// Token: 0x0400000C RID: 12
			kBoolVal,
			// Token: 0x0400000D RID: 13
			kStringVal,
			// Token: 0x0400000E RID: 14
			kArrayVal,
			// Token: 0x0400000F RID: 15
			kMixedArrayVal,
			// Token: 0x04000010 RID: 16
			kMapVal,
			// Token: 0x04000011 RID: 17
			kMaxTags
		}
	}
}
