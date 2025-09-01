using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngineInternal.Input
{
	// Token: 0x02000007 RID: 7
	[NativeHeader("Modules/Input/Private/InputInternal.h")]
	[NativeHeader("Modules/Input/Private/InputModuleBindings.h")]
	internal class NativeInputSystem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000207C File Offset: 0x0000027C
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002093 File Offset: 0x00000293
		public static Action<int, string> onDeviceDiscovered
		{
			get
			{
				return NativeInputSystem.s_OnDeviceDiscoveredCallback;
			}
			set
			{
				NativeInputSystem.s_OnDeviceDiscoveredCallback = value;
				NativeInputSystem.hasDeviceDiscoveredCallback = (NativeInputSystem.s_OnDeviceDiscoveredCallback != null);
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020AA File Offset: 0x000002AA
		static NativeInputSystem()
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020B4 File Offset: 0x000002B4
		[RequiredByNativeCode]
		internal static void NotifyBeforeUpdate(NativeInputUpdateType updateType)
		{
			Action<NativeInputUpdateType> action = NativeInputSystem.onBeforeUpdate;
			bool flag = action != null;
			if (flag)
			{
				action(updateType);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020D8 File Offset: 0x000002D8
		[RequiredByNativeCode]
		internal unsafe static void NotifyUpdate(NativeInputUpdateType updateType, IntPtr eventBuffer)
		{
			NativeUpdateCallback nativeUpdateCallback = NativeInputSystem.onUpdate;
			NativeInputEventBuffer* ptr = (NativeInputEventBuffer*)eventBuffer.ToPointer();
			bool flag = nativeUpdateCallback == null;
			if (flag)
			{
				ptr->eventCount = 0;
				ptr->sizeInBytes = 0;
			}
			else
			{
				nativeUpdateCallback(updateType, ptr);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000211C File Offset: 0x0000031C
		[RequiredByNativeCode]
		internal static void NotifyDeviceDiscovered(int deviceId, string deviceDescriptor)
		{
			Action<int, string> action = NativeInputSystem.s_OnDeviceDiscoveredCallback;
			bool flag = action != null;
			if (flag)
			{
				action(deviceId, deviceDescriptor);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002144 File Offset: 0x00000344
		[RequiredByNativeCode]
		internal static void ShouldRunUpdate(NativeInputUpdateType updateType, out bool retval)
		{
			Func<NativeInputUpdateType, bool> func = NativeInputSystem.onShouldRunUpdate;
			retval = (func == null || func(updateType));
		}

		// Token: 0x17000002 RID: 2
		// (set) Token: 0x0600000D RID: 13
		internal static extern bool hasDeviceDiscoveredCallback { [MethodImpl(MethodImplOptions.InternalCall)] set; } = false;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14
		[NativeProperty(IsThreadSafe = true)]
		public static extern double currentTime { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15
		[NativeProperty(IsThreadSafe = true)]
		public static extern double currentTimeOffsetToRealtimeSinceStartup { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000010 RID: 16
		[FreeFunction("AllocateInputDeviceId")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int AllocateDeviceId();

		// Token: 0x06000011 RID: 17 RVA: 0x00002167 File Offset: 0x00000367
		public static void QueueInputEvent<TInputEvent>(ref TInputEvent inputEvent) where TInputEvent : struct
		{
			NativeInputSystem.QueueInputEvent((IntPtr)UnsafeUtility.AddressOf<TInputEvent>(ref inputEvent));
		}

		// Token: 0x06000012 RID: 18
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void QueueInputEvent(IntPtr inputEvent);

		// Token: 0x06000013 RID: 19
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long IOCTL(int deviceId, int code, IntPtr data, int sizeInBytes);

		// Token: 0x06000014 RID: 20
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetPollingFrequency(float hertz);

		// Token: 0x06000015 RID: 21
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Update(NativeInputUpdateType updateType);

		// Token: 0x06000016 RID: 22 RVA: 0x0000217B File Offset: 0x0000037B
		[Obsolete("This is not needed any longer.")]
		public static void SetUpdateMask(NativeInputUpdateType mask)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000217E File Offset: 0x0000037E
		public NativeInputSystem()
		{
		}

		// Token: 0x04000018 RID: 24
		public static NativeUpdateCallback onUpdate;

		// Token: 0x04000019 RID: 25
		public static Action<NativeInputUpdateType> onBeforeUpdate;

		// Token: 0x0400001A RID: 26
		public static Func<NativeInputUpdateType, bool> onShouldRunUpdate;

		// Token: 0x0400001B RID: 27
		private static Action<int, string> s_OnDeviceDiscoveredCallback;
	}
}
