using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000007 RID: 7
	[NativeConditional("ENABLE_VR")]
	public static class XRDevice
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000020DB File Offset: 0x000002DB
		[Obsolete("This is obsolete, and should no longer be used. Instead, find the active XRDisplaySubsystem and check that the running property is true (for details, see XRDevice.isPresent documentation).", true)]
		public static bool isPresent
		{
			get
			{
				throw new NotSupportedException("XRDevice is Obsolete. Instead, find the active XRDisplaySubsystem and check to see if it is running.");
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000020 RID: 32
		[StaticAccessor("GetIVRDeviceSwapChain()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[NativeName("DeviceRefreshRate")]
		public static extern float refreshRate { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000021 RID: 33
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr GetNativePtr();

		// Token: 0x06000022 RID: 34
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[Obsolete("This is obsolete, and should no longer be used.  Please use XRInputSubsystem.GetTrackingOriginMode.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern TrackingSpaceType GetTrackingSpaceType();

		// Token: 0x06000023 RID: 35
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[Obsolete("This is obsolete, and should no longer be used.  Please use XRInputSubsystem.TrySetTrackingOriginMode.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool SetTrackingSpaceType(TrackingSpaceType trackingSpaceType);

		// Token: 0x06000024 RID: 36
		[NativeName("DisableAutoVRCameraTracking")]
		[StaticAccessor("GetIVRDevice()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DisableAutoXRCameraTracking([NotNull("ArgumentNullException")] Camera camera, bool disabled);

		// Token: 0x06000025 RID: 37
		[NativeName("UpdateEyeTextureMSAASetting")]
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UpdateEyeTextureMSAASetting();

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000026 RID: 38
		// (set) Token: 0x06000027 RID: 39
		public static extern float fovZoomFactor { [MethodImpl(MethodImplOptions.InternalCall)] get; [StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)] [NativeName("SetProjectionZoomFactor")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000028 RID: 40 RVA: 0x000020E8 File Offset: 0x000002E8
		// (remove) Token: 0x06000029 RID: 41 RVA: 0x0000211C File Offset: 0x0000031C
		public static event Action<string> deviceLoaded
		{
			[CompilerGenerated]
			add
			{
				Action<string> action = XRDevice.deviceLoaded;
				Action<string> action2;
				do
				{
					action2 = action;
					Action<string> value2 = (Action<string>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<string>>(ref XRDevice.deviceLoaded, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<string> action = XRDevice.deviceLoaded;
				Action<string> action2;
				do
				{
					action2 = action;
					Action<string> value2 = (Action<string>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<string>>(ref XRDevice.deviceLoaded, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002150 File Offset: 0x00000350
		[RequiredByNativeCode]
		private static void InvokeDeviceLoaded(string loadedDeviceName)
		{
			bool flag = XRDevice.deviceLoaded != null;
			if (flag)
			{
				XRDevice.deviceLoaded(loadedDeviceName);
			}
		}

		// Token: 0x0400000F RID: 15
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<string> deviceLoaded;
	}
}
