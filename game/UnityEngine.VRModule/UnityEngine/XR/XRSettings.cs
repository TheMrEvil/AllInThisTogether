using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine.XR
{
	// Token: 0x02000004 RID: 4
	[NativeHeader("Modules/VR/ScriptBindings/XR.bindings.h")]
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	[NativeHeader("Modules/VR/VRModule.h")]
	[NativeHeader("Runtime/Interfaces/IVRDevice.h")]
	[NativeConditional("ENABLE_VR")]
	public static class XRSettings
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4
		// (set) Token: 0x06000005 RID: 5
		public static extern bool enabled { [StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)] [MethodImpl(MethodImplOptions.InternalCall)] get; [Obsolete("XRSettings.enabled{set;} is deprecated and should no longer be used. Instead, call Start() and Stop() on an XRDisplaySubystem instance.")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6
		// (set) Token: 0x06000007 RID: 7
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern GameViewRenderMode gameViewRenderMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8
		[NativeName("Active")]
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern bool isDeviceActive { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9
		// (set) Token: 0x0600000A RID: 10
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern bool showDeviceView { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11
		// (set) Token: 0x0600000C RID: 12
		[NativeName("RenderScale")]
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern float eyeTextureResolutionScale { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern int eyeTextureWidth { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern int eyeTextureHeight { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000205C File Offset: 0x0000025C
		[NativeConditional("ENABLE_VR", "RenderTextureDesc()")]
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[NativeName("IntermediateEyeTextureDesc")]
		public static RenderTextureDescriptor eyeTextureDesc
		{
			get
			{
				RenderTextureDescriptor result;
				XRSettings.get_eyeTextureDesc_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000010 RID: 16
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[NativeName("DeviceEyeTextureDimension")]
		public static extern TextureDimension deviceEyeTextureDimension { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002074 File Offset: 0x00000274
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000208C File Offset: 0x0000028C
		public static float renderViewportScale
		{
			get
			{
				return XRSettings.renderViewportScaleInternal;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("value", "Render viewport scale should be between 0 and 1.");
				}
				XRSettings.renderViewportScaleInternal = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000013 RID: 19
		// (set) Token: 0x06000014 RID: 20
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		[NativeName("RenderViewportScale")]
		internal static extern float renderViewportScaleInternal { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000015 RID: 21
		// (set) Token: 0x06000016 RID: 22
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern float occlusionMaskScale { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000017 RID: 23
		// (set) Token: 0x06000018 RID: 24
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern bool useOcclusionMesh { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000019 RID: 25
		[NativeName("DeviceName")]
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern string loadedDeviceName { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600001A RID: 26 RVA: 0x000020C8 File Offset: 0x000002C8
		[Obsolete("XRSettings.LoadDeviceByName is deprecated and should no longer be used. Instead, use the SubsystemManager to load XR devices by querying subsystem descriptors to create and start the subsystems of your choice.")]
		public static void LoadDeviceByName(string deviceName)
		{
			XRSettings.LoadDeviceByName(new string[]
			{
				deviceName
			});
		}

		// Token: 0x0600001B RID: 27
		[Obsolete("XRSettings.LoadDeviceByName is deprecated and should no longer be used. Instead, use the SubsystemManager to load XR devices by querying subsystem descriptors to create and start the subsystems of your choice.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void LoadDeviceByName(string[] prioritizedDeviceNameList);

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001C RID: 28
		public static extern string[] supportedDevices { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001D RID: 29
		[StaticAccessor("GetIVRDeviceScripting()", StaticAccessorType.ArrowWithDefaultReturnIfNull)]
		public static extern XRSettings.StereoRenderingMode stereoRenderingMode { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x0600001E RID: 30
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_eyeTextureDesc_Injected(out RenderTextureDescriptor ret);

		// Token: 0x02000005 RID: 5
		public enum StereoRenderingMode
		{
			// Token: 0x04000008 RID: 8
			MultiPass,
			// Token: 0x04000009 RID: 9
			SinglePass,
			// Token: 0x0400000A RID: 10
			SinglePassInstanced,
			// Token: 0x0400000B RID: 11
			SinglePassMultiview
		}
	}
}
