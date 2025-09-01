using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono
{
	// Token: 0x02000045 RID: 69
	internal static class Runtime
	{
		// Token: 0x060000D3 RID: 211
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void mono_runtime_install_handlers();

		// Token: 0x060000D4 RID: 212 RVA: 0x000040F0 File Offset: 0x000022F0
		internal static void InstallSignalHandlers()
		{
			Runtime.mono_runtime_install_handlers();
		}

		// Token: 0x060000D5 RID: 213
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetDisplayName();

		// Token: 0x060000D6 RID: 214
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string GetNativeStackTrace(Exception exception);

		// Token: 0x060000D7 RID: 215 RVA: 0x000040F7 File Offset: 0x000022F7
		public static bool SetGCAllowSynchronousMajor(bool flag)
		{
			return true;
		}

		// Token: 0x060000D8 RID: 216
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string ExceptionToState_internal(Exception exc, out ulong portable_hash, out ulong unportable_hash);

		// Token: 0x060000D9 RID: 217 RVA: 0x000040FC File Offset: 0x000022FC
		private static Tuple<string, ulong, ulong> ExceptionToState(Exception exc)
		{
			ulong item;
			ulong item2;
			return new Tuple<string, ulong, ulong>(Runtime.ExceptionToState_internal(exc, out item, out item2), item, item2);
		}

		// Token: 0x060000DA RID: 218
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableMicrosoftTelemetry();

		// Token: 0x060000DB RID: 219
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableMicrosoftTelemetry_internal(IntPtr appBundleID, IntPtr appSignature, IntPtr appVersion, IntPtr merpGUIPath, IntPtr appPath, IntPtr configDir);

		// Token: 0x060000DC RID: 220
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SendMicrosoftTelemetry_internal(IntPtr payload, ulong portable_hash, ulong unportable_hash);

		// Token: 0x060000DD RID: 221
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WriteStateToFile_internal(IntPtr payload, ulong portable_hash, ulong unportable_hash);

		// Token: 0x060000DE RID: 222 RVA: 0x0000411C File Offset: 0x0000231C
		private static void WriteStateToFile(Exception exc)
		{
			ulong portable_hash;
			ulong unportable_hash;
			using (SafeStringMarshal safeStringMarshal = RuntimeMarshal.MarshalString(Runtime.ExceptionToState_internal(exc, out portable_hash, out unportable_hash)))
			{
				Runtime.WriteStateToFile_internal(safeStringMarshal.Value, portable_hash, unportable_hash);
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004168 File Offset: 0x00002368
		private static void SendMicrosoftTelemetry(string payload_str, ulong portable_hash, ulong unportable_hash)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				using (SafeStringMarshal safeStringMarshal = RuntimeMarshal.MarshalString(payload_str))
				{
					Runtime.SendMicrosoftTelemetry_internal(safeStringMarshal.Value, portable_hash, unportable_hash);
					return;
				}
			}
			throw new PlatformNotSupportedException("Merp support is currently only supported on OSX.");
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000041C4 File Offset: 0x000023C4
		private static void SendExceptionToTelemetry(Exception exc)
		{
			object obj = Runtime.dump;
			lock (obj)
			{
				ulong portable_hash;
				ulong unportable_hash;
				Runtime.SendMicrosoftTelemetry(Runtime.ExceptionToState_internal(exc, out portable_hash, out unportable_hash), portable_hash, unportable_hash);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004210 File Offset: 0x00002410
		private static void EnableMicrosoftTelemetry(string appBundleID_str, string appSignature_str, string appVersion_str, string merpGUIPath_str, string unused, string appPath_str, string configDir_str)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				using (SafeStringMarshal safeStringMarshal = RuntimeMarshal.MarshalString(appBundleID_str))
				{
					using (SafeStringMarshal safeStringMarshal2 = RuntimeMarshal.MarshalString(appSignature_str))
					{
						using (SafeStringMarshal safeStringMarshal3 = RuntimeMarshal.MarshalString(appVersion_str))
						{
							using (SafeStringMarshal safeStringMarshal4 = RuntimeMarshal.MarshalString(merpGUIPath_str))
							{
								using (SafeStringMarshal safeStringMarshal5 = RuntimeMarshal.MarshalString(appPath_str))
								{
									using (SafeStringMarshal safeStringMarshal6 = RuntimeMarshal.MarshalString(configDir_str))
									{
										Runtime.EnableMicrosoftTelemetry_internal(safeStringMarshal.Value, safeStringMarshal2.Value, safeStringMarshal3.Value, safeStringMarshal4.Value, safeStringMarshal5.Value, safeStringMarshal6.Value);
										return;
									}
								}
							}
						}
					}
				}
			}
			throw new PlatformNotSupportedException("Merp support is currently only supported on OSX.");
		}

		// Token: 0x060000E2 RID: 226
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string DumpStateSingle_internal(out ulong portable_hash, out ulong unportable_hash);

		// Token: 0x060000E3 RID: 227
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string DumpStateTotal_internal(out ulong portable_hash, out ulong unportable_hash);

		// Token: 0x060000E4 RID: 228 RVA: 0x00004338 File Offset: 0x00002538
		private static Tuple<string, ulong, ulong> DumpStateSingle()
		{
			object obj = Runtime.dump;
			ulong item2;
			ulong item3;
			string item;
			lock (obj)
			{
				item = Runtime.DumpStateSingle_internal(out item2, out item3);
			}
			return new Tuple<string, ulong, ulong>(item, item2, item3);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004388 File Offset: 0x00002588
		private static Tuple<string, ulong, ulong> DumpStateTotal()
		{
			object obj = Runtime.dump;
			ulong item2;
			ulong item3;
			string item;
			lock (obj)
			{
				item = Runtime.DumpStateTotal_internal(out item2, out item3);
			}
			return new Tuple<string, ulong, ulong>(item, item2, item3);
		}

		// Token: 0x060000E6 RID: 230
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterReportingForAllNativeLibs_internal();

		// Token: 0x060000E7 RID: 231 RVA: 0x000043D8 File Offset: 0x000025D8
		private static void RegisterReportingForAllNativeLibs()
		{
			Runtime.RegisterReportingForAllNativeLibs_internal();
		}

		// Token: 0x060000E8 RID: 232
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RegisterReportingForNativeLib_internal(IntPtr modulePathSuffix, IntPtr moduleName);

		// Token: 0x060000E9 RID: 233 RVA: 0x000043E0 File Offset: 0x000025E0
		private static void RegisterReportingForNativeLib(string modulePathSuffix_str, string moduleName_str)
		{
			using (SafeStringMarshal safeStringMarshal = RuntimeMarshal.MarshalString(modulePathSuffix_str))
			{
				using (SafeStringMarshal safeStringMarshal2 = RuntimeMarshal.MarshalString(moduleName_str))
				{
					Runtime.RegisterReportingForNativeLib_internal(safeStringMarshal.Value, safeStringMarshal2.Value);
				}
			}
		}

		// Token: 0x060000EA RID: 234
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableCrashReportLog_internal(IntPtr directory);

		// Token: 0x060000EB RID: 235 RVA: 0x00004448 File Offset: 0x00002648
		private static void EnableCrashReportLog(string directory_str)
		{
			using (SafeStringMarshal safeStringMarshal = RuntimeMarshal.MarshalString(directory_str))
			{
				Runtime.EnableCrashReportLog_internal(safeStringMarshal.Value);
			}
		}

		// Token: 0x060000EC RID: 236
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CheckCrashReportLog_internal(IntPtr directory, bool clear);

		// Token: 0x060000ED RID: 237 RVA: 0x00004488 File Offset: 0x00002688
		private static Runtime.CrashReportLogLevel CheckCrashReportLog(string directory_str, bool clear)
		{
			Runtime.CrashReportLogLevel result;
			using (SafeStringMarshal safeStringMarshal = RuntimeMarshal.MarshalString(directory_str))
			{
				result = (Runtime.CrashReportLogLevel)Runtime.CheckCrashReportLog_internal(safeStringMarshal.Value, clear);
			}
			return result;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000044CC File Offset: 0x000026CC
		private static string get_breadcrumb_value(string file_prefix, string directory_str, bool clear)
		{
			string[] files = Directory.GetFiles(directory_str, file_prefix + "_*");
			if (files.Length == 0)
			{
				return string.Empty;
			}
			if (files.Length > 1)
			{
				try
				{
					Array.ForEach<string>(files, delegate(string f)
					{
						File.Delete(f);
					});
				}
				catch (Exception)
				{
				}
				return string.Empty;
			}
			if (clear)
			{
				File.Delete(files[0]);
			}
			return Path.GetFileName(files[0]).Substring(file_prefix.Length + 1);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000455C File Offset: 0x0000275C
		private static long CheckCrashReportHash(string directory_str, bool clear)
		{
			string text = Runtime.get_breadcrumb_value("crash_hash", directory_str, clear);
			if (text == string.Empty)
			{
				return 0L;
			}
			return Convert.ToInt64(text, 16);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000458E File Offset: 0x0000278E
		private static string CheckCrashReportReason(string directory_str, bool clear)
		{
			return Runtime.get_breadcrumb_value("crash_reason", directory_str, clear);
		}

		// Token: 0x060000F1 RID: 241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AnnotateMicrosoftTelemetry_internal(IntPtr key, IntPtr val);

		// Token: 0x060000F2 RID: 242 RVA: 0x0000459C File Offset: 0x0000279C
		private static void AnnotateMicrosoftTelemetry(string key, string val)
		{
			using (SafeStringMarshal safeStringMarshal = RuntimeMarshal.MarshalString(key))
			{
				using (SafeStringMarshal safeStringMarshal2 = RuntimeMarshal.MarshalString(val))
				{
					object obj = Runtime.dump;
					lock (obj)
					{
						Runtime.AnnotateMicrosoftTelemetry_internal(safeStringMarshal.Value, safeStringMarshal2.Value);
					}
				}
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000462C File Offset: 0x0000282C
		// Note: this type is marked as 'beforefieldinit'.
		static Runtime()
		{
		}

		// Token: 0x04000DCF RID: 3535
		private static object dump = new object();

		// Token: 0x02000046 RID: 70
		private enum CrashReportLogLevel
		{
			// Token: 0x04000DD1 RID: 3537
			MonoSummaryNone,
			// Token: 0x04000DD2 RID: 3538
			MonoSummarySetup,
			// Token: 0x04000DD3 RID: 3539
			MonoSummarySuspendHandshake,
			// Token: 0x04000DD4 RID: 3540
			MonoSummaryUnmanagedStacks,
			// Token: 0x04000DD5 RID: 3541
			MonoSummaryManagedStacks,
			// Token: 0x04000DD6 RID: 3542
			MonoSummaryStateWriter,
			// Token: 0x04000DD7 RID: 3543
			MonoSummaryStateWriterDone,
			// Token: 0x04000DD8 RID: 3544
			MonoSummaryMerpWriter,
			// Token: 0x04000DD9 RID: 3545
			MonoSummaryMerpInvoke,
			// Token: 0x04000DDA RID: 3546
			MonoSummaryCleanup,
			// Token: 0x04000DDB RID: 3547
			MonoSummaryDone,
			// Token: 0x04000DDC RID: 3548
			MonoSummaryDoubleFault
		}

		// Token: 0x02000047 RID: 71
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060000F4 RID: 244 RVA: 0x00004638 File Offset: 0x00002838
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060000F5 RID: 245 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x060000F6 RID: 246 RVA: 0x00004644 File Offset: 0x00002844
			internal void <get_breadcrumb_value>b__29_0(string f)
			{
				File.Delete(f);
			}

			// Token: 0x04000DDD RID: 3549
			public static readonly Runtime.<>c <>9 = new Runtime.<>c();

			// Token: 0x04000DDE RID: 3550
			public static Action<string> <>9__29_0;
		}
	}
}
