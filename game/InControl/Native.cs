using System;
using System.Runtime.InteropServices;

namespace InControl
{
	// Token: 0x02000045 RID: 69
	internal static class Native
	{
		// Token: 0x06000375 RID: 885
		[DllImport("InControlNative", CallingConvention = CallingConvention.Cdecl, EntryPoint = "InControl_Init")]
		public static extern void Init(NativeInputOptions options);

		// Token: 0x06000376 RID: 886
		[DllImport("InControlNative", CallingConvention = CallingConvention.Cdecl, EntryPoint = "InControl_Stop")]
		public static extern void Stop();

		// Token: 0x06000377 RID: 887
		[DllImport("InControlNative", CallingConvention = CallingConvention.Cdecl, EntryPoint = "InControl_GetVersionInfo")]
		public static extern void GetVersionInfo(out NativeVersionInfo versionInfo);

		// Token: 0x06000378 RID: 888
		[DllImport("InControlNative", CallingConvention = CallingConvention.Cdecl, EntryPoint = "InControl_GetDeviceInfo")]
		public static extern bool GetDeviceInfo(uint handle, out InputDeviceInfo deviceInfo);

		// Token: 0x06000379 RID: 889
		[DllImport("InControlNative", CallingConvention = CallingConvention.Cdecl, EntryPoint = "InControl_GetDeviceState")]
		public static extern bool GetDeviceState(uint handle, out IntPtr deviceState);

		// Token: 0x0600037A RID: 890
		[DllImport("InControlNative", CallingConvention = CallingConvention.Cdecl, EntryPoint = "InControl_GetDeviceEvents")]
		public static extern int GetDeviceEvents(out IntPtr deviceEvents);

		// Token: 0x0600037B RID: 891
		[DllImport("InControlNative", CallingConvention = CallingConvention.Cdecl, EntryPoint = "InControl_SetHapticState")]
		public static extern void SetHapticState(uint handle, byte lowFrequency, byte highFrequency);

		// Token: 0x0600037C RID: 892
		[DllImport("InControlNative", CallingConvention = CallingConvention.Cdecl, EntryPoint = "InControl_SetTriggersHapticState")]
		public static extern void SetTriggersHapticState(uint handle, byte leftTrigger, byte rightTrigger);

		// Token: 0x0600037D RID: 893
		[DllImport("InControlNative", CallingConvention = CallingConvention.Cdecl, EntryPoint = "InControl_SetLightColor")]
		public static extern void SetLightColor(uint handle, byte red, byte green, byte blue);

		// Token: 0x0600037E RID: 894
		[DllImport("InControlNative", CallingConvention = CallingConvention.Cdecl, EntryPoint = "InControl_SetLightFlash")]
		public static extern void SetLightFlash(uint handle, byte flashOnDuration, byte flashOffDuration);

		// Token: 0x0600037F RID: 895
		[DllImport("InControlNative", CallingConvention = CallingConvention.Cdecl, EntryPoint = "InControl_GetAnalogGlyphName")]
		public static extern uint GetAnalogGlyphName(uint handle, uint index, out IntPtr glyphName);

		// Token: 0x06000380 RID: 896
		[DllImport("InControlNative", CallingConvention = CallingConvention.Cdecl, EntryPoint = "InControl_GetButtonGlyphName")]
		public static extern uint GetButtonGlyphName(uint handle, uint index, out IntPtr glyphName);

		// Token: 0x0400031E RID: 798
		private const string libraryName = "InControlNative";

		// Token: 0x0400031F RID: 799
		private const CallingConvention callingConvention = CallingConvention.Cdecl;
	}
}
