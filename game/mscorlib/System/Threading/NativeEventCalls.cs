using System;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x020002F4 RID: 756
	internal static class NativeEventCalls
	{
		// Token: 0x060020EB RID: 8427 RVA: 0x00076EAC File Offset: 0x000750AC
		public unsafe static IntPtr CreateEvent_internal(bool manual, bool initial, string name, out int errorCode)
		{
			char* ptr = name;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return NativeEventCalls.CreateEvent_icall(manual, initial, ptr, (name != null) ? name.Length : 0, out errorCode);
		}

		// Token: 0x060020EC RID: 8428
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr CreateEvent_icall(bool manual, bool initial, char* name, int name_length, out int errorCode);

		// Token: 0x060020ED RID: 8429 RVA: 0x00076EE0 File Offset: 0x000750E0
		public static bool SetEvent(SafeWaitHandle handle)
		{
			bool flag = false;
			bool result;
			try
			{
				handle.DangerousAddRef(ref flag);
				result = NativeEventCalls.SetEvent_internal(handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x060020EE RID: 8430
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetEvent_internal(IntPtr handle);

		// Token: 0x060020EF RID: 8431 RVA: 0x00076F20 File Offset: 0x00075120
		public static bool ResetEvent(SafeWaitHandle handle)
		{
			bool flag = false;
			bool result;
			try
			{
				handle.DangerousAddRef(ref flag);
				result = NativeEventCalls.ResetEvent_internal(handle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					handle.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x060020F0 RID: 8432
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ResetEvent_internal(IntPtr handle);

		// Token: 0x060020F1 RID: 8433
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void CloseEvent_internal(IntPtr handle);

		// Token: 0x060020F2 RID: 8434 RVA: 0x00076F60 File Offset: 0x00075160
		public unsafe static IntPtr OpenEvent_internal(string name, EventWaitHandleRights rights, out int errorCode)
		{
			char* ptr = name;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return NativeEventCalls.OpenEvent_icall(ptr, (name != null) ? name.Length : 0, rights, out errorCode);
		}

		// Token: 0x060020F3 RID: 8435
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr OpenEvent_icall(char* name, int name_length, EventWaitHandleRights rights, out int errorCode);
	}
}
