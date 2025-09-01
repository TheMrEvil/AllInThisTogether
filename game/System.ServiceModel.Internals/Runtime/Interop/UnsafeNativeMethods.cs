using System;
using System.ComponentModel;
using System.Runtime.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace System.Runtime.Interop
{
	// Token: 0x0200003D RID: 61
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x0600020E RID: 526
		[SecurityCritical]
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Auto)]
		public static extern SafeWaitHandle CreateWaitableTimer(IntPtr mustBeZero, bool manualReset, string timerName);

		// Token: 0x0600020F RID: 527
		[SecurityCritical]
		[DllImport("kernel32.dll", ExactSpelling = true)]
		public static extern bool SetWaitableTimer(SafeWaitHandle handle, ref long dueTime, int period, IntPtr mustBeZero, IntPtr mustBeZeroAlso, bool resume);

		// Token: 0x06000210 RID: 528
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern int QueryPerformanceCounter(out long time);

		// Token: 0x06000211 RID: 529
		[SecurityCritical]
		[DllImport("kernel32.dll")]
		public static extern uint GetSystemTimeAdjustment(out int adjustment, out uint increment, out uint adjustmentDisabled);

		// Token: 0x06000212 RID: 530
		[SecurityCritical]
		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern void GetSystemTimeAsFileTime(out System.Runtime.InteropServices.ComTypes.FILETIME time);

		// Token: 0x06000213 RID: 531 RVA: 0x00008AC8 File Offset: 0x00006CC8
		[SecurityCritical]
		public static void GetSystemTimeAsFileTime(out long time)
		{
			System.Runtime.InteropServices.ComTypes.FILETIME filetime;
			UnsafeNativeMethods.GetSystemTimeAsFileTime(out filetime);
			time = 0L;
			time |= (long)((ulong)filetime.dwHighDateTime);
			time <<= 32;
			time |= (long)((ulong)filetime.dwLowDateTime);
		}

		// Token: 0x06000214 RID: 532
		[SecurityCritical]
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetComputerNameEx([In] ComputerNameFormat nameType, [MarshalAs(UnmanagedType.LPTStr)] [In] [Out] StringBuilder lpBuffer, [In] [Out] ref int size);

		// Token: 0x06000215 RID: 533 RVA: 0x00008B00 File Offset: 0x00006D00
		[SecurityCritical]
		internal static string GetComputerName(ComputerNameFormat nameType)
		{
			int num = 0;
			if (!UnsafeNativeMethods.GetComputerNameEx(nameType, null, ref num))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 234)
				{
					throw Fx.Exception.AsError(new Win32Exception(lastWin32Error));
				}
			}
			if (num < 0)
			{
				Fx.AssertAndThrow("GetComputerName returned an invalid length: " + num.ToString());
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			if (!UnsafeNativeMethods.GetComputerNameEx(nameType, stringBuilder, ref num))
			{
				int lastWin32Error2 = Marshal.GetLastWin32Error();
				throw Fx.Exception.AsError(new Win32Exception(lastWin32Error2));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000216 RID: 534
		[SecurityCritical]
		[DllImport("kernel32.dll")]
		internal static extern bool IsDebuggerPresent();

		// Token: 0x06000217 RID: 535
		[SecurityCritical]
		[DllImport("kernel32.dll")]
		internal static extern void DebugBreak();

		// Token: 0x06000218 RID: 536
		[SecurityCritical]
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern void OutputDebugString(string lpOutputString);

		// Token: 0x06000219 RID: 537
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern uint EventRegister([In] ref Guid providerId, [In] UnsafeNativeMethods.EtwEnableCallback enableCallback, [In] void* callbackContext, [In] [Out] ref long registrationHandle);

		// Token: 0x0600021A RID: 538
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern uint EventUnregister([In] long registrationHandle);

		// Token: 0x0600021B RID: 539
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern bool EventEnabled([In] long registrationHandle, [In] ref System.Runtime.Diagnostics.EventDescriptor eventDescriptor);

		// Token: 0x0600021C RID: 540
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern uint EventWrite([In] long registrationHandle, [In] ref System.Runtime.Diagnostics.EventDescriptor eventDescriptor, [In] uint userDataCount, [In] UnsafeNativeMethods.EventData* userData);

		// Token: 0x0600021D RID: 541
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern uint EventWriteTransfer([In] long registrationHandle, [In] ref System.Runtime.Diagnostics.EventDescriptor eventDescriptor, [In] ref Guid activityId, [In] ref Guid relatedActivityId, [In] uint userDataCount, [In] UnsafeNativeMethods.EventData* userData);

		// Token: 0x0600021E RID: 542
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal unsafe static extern uint EventWriteString([In] long registrationHandle, [In] byte level, [In] long keywords, [In] char* message);

		// Token: 0x0600021F RID: 543
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern uint EventActivityIdControl([In] int ControlCode, [In] [Out] ref Guid ActivityId);

		// Token: 0x06000220 RID: 544
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool ReportEvent(SafeHandle hEventLog, ushort type, ushort category, uint eventID, byte[] userSID, ushort numStrings, uint dataLen, HandleRef strings, byte[] rawData);

		// Token: 0x06000221 RID: 545
		[SecurityCritical]
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern SafeEventLogWriteHandle RegisterEventSource(string uncServerName, string sourceName);

		// Token: 0x0400012D RID: 301
		public const string KERNEL32 = "kernel32.dll";

		// Token: 0x0400012E RID: 302
		public const string ADVAPI32 = "advapi32.dll";

		// Token: 0x0400012F RID: 303
		public const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x04000130 RID: 304
		public const int ERROR_MORE_DATA = 234;

		// Token: 0x04000131 RID: 305
		public const int ERROR_ARITHMETIC_OVERFLOW = 534;

		// Token: 0x04000132 RID: 306
		public const int ERROR_NOT_ENOUGH_MEMORY = 8;

		// Token: 0x0200008C RID: 140
		[StructLayout(LayoutKind.Explicit, Size = 16)]
		public struct EventData
		{
			// Token: 0x040002DC RID: 732
			[FieldOffset(0)]
			internal ulong DataPointer;

			// Token: 0x040002DD RID: 733
			[FieldOffset(8)]
			internal uint Size;

			// Token: 0x040002DE RID: 734
			[FieldOffset(12)]
			internal int Reserved;
		}

		// Token: 0x0200008D RID: 141
		// (Invoke) Token: 0x0600040C RID: 1036
		[SecurityCritical]
		internal unsafe delegate void EtwEnableCallback([In] ref Guid sourceId, [In] int isEnabled, [In] byte level, [In] long matchAnyKeywords, [In] long matchAllKeywords, [In] void* filterData, [In] void* callbackContext);
	}
}
